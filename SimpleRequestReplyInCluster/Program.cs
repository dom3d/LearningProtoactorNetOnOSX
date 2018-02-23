using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Messages;
using Proto;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Remote;
using ExampleProtocol = Messages.BaseReflection; // generated meta data. Name based of used proto file-name (here base.proto)

namespace SimpleRequestReplyInCluster
{
	class ClusterConfig
	{
		public static readonly string Name = "ProtoActorCluster";
	}

	class RequesterNodeConfig
	{
		public static string ip = "127.0.0.1";
		public static int port = 12005;
	}

	class AnsweringNodeConfig
	{
		public static string ip = "127.0.0.1";
		public static int port = 12006;
	}

	class App
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello ProtoActor example. Make sure Answerer Node (-a) is running first as this is a one-time 'send and answer' example!");
			if (args.Length == 0)
			{
				Console.WriteLine("Missing command line args. Please specify node type by using the param -r or -a.");
				return;
			}

			// From here on, asuming that Consul Service is running

			// Register protobuf messages
			Serialization.RegisterFileDescriptor(ExampleProtocol.Descriptor);

			// start choosen node and join cluster
			if (args[0] == "-r")
			{
				Console.WriteLine("-r Mode: Requester Node");

				// actor setup definition and registration of that definition
				Props requesterKind = Actor.FromProducer(() => new RequesterActor());
				Remote.RegisterKnownKind(RequesterActor.TypeName, requesterKind);

				// Start the server and join the cluster. Known Actors will be spawned automatically
				Cluster.Start(ClusterConfig.Name, RequesterNodeConfig.ip, RequesterNodeConfig.port, new ConsulProvider(new ConsulProviderOptions()));

				PID requesterActor = Actor.Spawn(requesterKind);
				requesterActor.Tell(new RequestTarget() { TargetActorName = AnsweringActor.TypeName, Repeat = true});
			}
			else if (args[0] == "-a")
			{
				Console.WriteLine("-a Mode: Answerer Node");
				// actor
				Props answeringKind = Actor.FromProducer(() => new AnsweringActor());
				Remote.RegisterKnownKind(AnsweringActor.TypeName, answeringKind);
				// Start the server and join the cluster. Known Actors will be spawned automatically
				Cluster.Start(ClusterConfig.Name, AnsweringNodeConfig.ip, AnsweringNodeConfig.port, new ConsulProvider(new ConsulProviderOptions()));
			}
			else
			{
				Console.WriteLine("Wrong command line args. Please specify node type by using the param -r or -a.");
			}

			Console.WriteLine("Hit ENTER to allow this node to terminate.");
			Console.ReadKey();
		}
	}

	sealed class RequesterActor : IActor
	{
		public static readonly string TypeName = "RequesterAct";
		private int counter = 0;

		public Task ReceiveAsync(IContext context)
		{
			switch (context.Message)
			{
				case RequestTarget kickoff:
					++counter;
					string targetActorName = kickoff.TargetActorName;
					Console.WriteLine("Received a Target for a Hello Request, which is " + targetActorName);

					// get target reference
					Stopwatch timer = Stopwatch.StartNew();
					timer.Start();
					string dummy = "noideaWhatThisIsFor";
					var (pid, getStatus) = Cluster.GetAsync(dummy, AnsweringActor.TypeName).Result;
					while (getStatus == ResponseStatusCode.Unavailable)
					{
						(pid, getStatus) = Cluster.GetAsync(dummy, AnsweringActor.TypeName).Result;
					}

					timer.Stop();
					Console.WriteLine("\nGot hold on target reference in (ms): " +  timer.ElapsedMilliseconds);
					if (getStatus == ResponseStatusCode.OK)
					{
						timer.Restart();
						HelloResponse answer = pid.RequestAsync<HelloResponse>(new HelloRequest()).Result;
						timer.Stop();
						Console.WriteLine("Received as answer: " + answer.Message);
						Console.WriteLine("Received answer in (ms): " + timer.ElapsedMilliseconds);
						if (kickoff.Repeat)
						{
							context.Self.Tell(new RequestTarget() { TargetActorName = AnsweringActor.TypeName, Repeat = false });
						}
					}
					else
					{
						Console.WriteLine("Failed to get a hold on target: " + getStatus.ToString());
					}
					break;
			}
			return Actor.Done;
		}
	}

	sealed class AnsweringActor : IActor
	{
		public static readonly string TypeName = "AnsweringAct";
		
		public Task ReceiveAsync(IContext context)
		{
			switch (context.Message)
			{
				case HelloRequest _:
					context.Respond
					(
						new HelloResponse
						{
							Message = "Hello, this is a reply message from Anwsering Node."
						}
					);
					Console.WriteLine("Received a request and replied.");
					break;
			}
			return Actor.Done;
		}
	}
}
