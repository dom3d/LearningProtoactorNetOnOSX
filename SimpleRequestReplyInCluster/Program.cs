using System;
using Messages;
using Proto;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Remote;
using ProtosReflection = Messages.BaseReflection; // generated meta data. Name based of used proto file-name (here base.proto)

// variation of https://github.com/tomliversidge/protoactor-dotnet/tree/business_handshake/examples/ClusterHelloWorld
namespace SimpleRequestReplyInCluster
{
	class App
	{
		static void Main(string[] args)
		{
			if(args.Length == 0)
			{
				Console.WriteLine("Missing command line args. Please specify node type by using the param -r or -a.");
				return;
			}

			// Asuming that Consul Service is running 

			if(args[0] == "-r")
			{
				RequesterNode program = new RequesterNode();
				program.Run();
			}
			else if (args[0] == "-a")
			{
				AnsweringNode program = new AnsweringNode();
				program.Run();
			}
			else
			{
				Console.WriteLine("Wrong command line args. Please specify node type by using the param -r or -a.");	
			}
        }
    }

	class ClusterConfig
	{
		public static readonly string ClusterName = "ProtoactorCluster";
		public static readonly string ClusterKind = "SimpleRequestReplyInCluster"; // some kind of partitioning to speed up or is it the Consul tags to tagg services
	}

	class RequesterNode
	{
		public void Run()
		{
			Console.WriteLine("Hello World, firing up the engine with Requester Node. Make sure Replier is running first !");

			Serialization.RegisterFileDescriptor(ProtosReflection.Descriptor);
			Remote.Start("127.0.0.1", 12001);
			Cluster.Start(ClusterConfig.ClusterName, new ConsulProvider(new ConsulProviderOptions()));
			Proto.PID pid = Cluster.GetAsync(ClusterConfig.ClusterName, ClusterConfig.ClusterKind).Result;

			Console.WriteLine("Press ENTER to send a request to Replier Node (make sure it's up and running first)");
			Console.ReadLine();

			Console.WriteLine("Sending out a request message to Replier Node ...");

			HelloResponse res = pid.RequestAsync<HelloResponse>(new HelloRequest()).Result;
			Console.WriteLine(res.Message);
			Console.WriteLine("Hit ENTER to allow this node to terminate.");
		}
	}

	class AnsweringNode
	{
		public void Run()
		{
			Console.WriteLine("Hello World, firing up the engine with Answering Node ...");

			Serialization.RegisterFileDescriptor(ProtosReflection.Descriptor);
			Props props = Actor.FromFunc(ctx =>
			{
				switch (ctx.Message)
				{
					case HelloRequest _:
						ctx.Respond(new HelloResponse
						{
							Message = "Hello, this is a reply message from Anwsering Node."
						});
						Console.WriteLine("Received a request and replied.");
						Console.WriteLine("Hit ENTER to allow this node to terminate.");
						break;
				}
				return Actor.Done;
			});

			Remote.RegisterKnownKind(ClusterConfig.ClusterKind, props);
			Remote.Start("127.0.0.1", 12000);
			Cluster.Start(ClusterConfig.ClusterName, new ConsulProvider(new ConsulProviderOptions()));

			Console.WriteLine("Now start Requester, waiting for the message");
			Console.ReadLine();
		}
	}
}
