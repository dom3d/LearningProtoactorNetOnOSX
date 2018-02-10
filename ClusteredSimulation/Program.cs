using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Proto;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Remote;
using ProtoExtensions;

namespace ClusteredSimulation
{
	/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

	class ClusterConfig
	{
		public static readonly string Name = "ProtoActorCluster";
	}

	class HeartbeatNodeConfig
	{
		public static string ip = "127.0.0.1";
		public static int port = 12005;
	}

	class SumNodeConfig
	{
		public static string ip = "127.0.0.1";
		public static int port = 12006;
	}

	class MultipNodeConfig
	{
		public static string ip = "127.0.0.1";
		public static int port = 12007;
	}

	/// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 

	class App
	{
		static void Main(string[] args)
		{
			Console.WriteLine("-h = Heartbeat. Then -s = Summariser. Then -m Multiplier");
			if (args.Length == 0)
			{
				Console.WriteLine("Missing command line args. Please specify node type by using one of the params: -h -s -m.");
				return;
			}

			// From here on, asuming that Consul Service is running

			// Register protobuf messages
			Serialization.RegisterFileDescriptor(Messages.MessagesReflection.Descriptor);

			// start choosen node and join cluster
			if (args[0] == "-h")
			{
				Console.WriteLine("-h Mode: Heartbeat Node");

				// actor setup definition and registration of that definition using a custom method extension
				//Actor.FromProducer(() => new Heartbeat()).RegisterAs(Heartbeat.TypeName);
				//Actor.FromProducer(() => new IntChannel()).RegisterAs(IntChannel.TypeName);

				// Join cluster
				Cluster.Start(ClusterConfig.Name, HeartbeatNodeConfig.ip, HeartbeatNodeConfig.port, new ConsulProvider(new ConsulProviderOptions()));

				// Cluster Spawn with a timeout

				Props heartBeatSetup = Actor.FromProducer(() => new Heartbeat());
				Actor.SpawnNamed(heartBeatSetup, "HeartBeat");
				//Remote.SpawnAsync(ClusterConfig.Name, Heartbeat.TypeName, TimeSpan.FromSeconds(5));
				//Actor.Spawn(heartBeatKind); // this works but the Remote one not !
				// Forcing actor creation by retrieven it via Cluster API
				//ClusterHelpers.WaitUntilSpawned(Heartbeat.TypeName, "HeartBeatName", new TimeSpan(0, 0, 5));
				Console.WriteLine("OK");

			}
			else if (args[0] == "-s")
			{
				Console.WriteLine("-s Mode");
				// actor
				//Actor.FromProducer(() => new Summariser()).RegisterAs(Summariser.TypeName);
				//Remote.RegisterKnownKind(, summariserKind);

				// Start the server and join the cluster.
				Cluster.Start(ClusterConfig.Name, SumNodeConfig.ip, SumNodeConfig.port, new ConsulProvider(new ConsulProviderOptions()));
				//Remote.SpawnAsync(ClusterConfig.Name, Summariser.TypeName, TimeSpan.FromSeconds(2));
				//Actor.Spawn(summariserKind);

				/*
				(PID, ResponseStatusCode) taskResult = (null, ResponseStatusCode.Unavailable);
				while (taskResult.Item2 == ResponseStatusCode.Unavailable)
				{
					taskResult = Cluster.GetAsync("", Summariser.TypeName).Result;
					System.Console.Write("?");
				}
				*/
				//ClusterHelpers.WaitUntilSpawned(Summariser.TypeName, "", new TimeSpan(0, 0, 5));
				var props = Actor.FromProducer(() => new Summariser());
				Actor.Spawn(props);
				Console.WriteLine("OK");
			}
			else if (args[0] == "-m")
			{
				Console.WriteLine("-m Mode");
				// actor
				//Props multiplierKind = Actor.FromProducer(() => new Multiplier());
				//Remote.RegisterKnownKind(Multiplier.TypeName, multiplierKind);
				// Start the server and join the cluster.
				Cluster.Start(ClusterConfig.Name, MultipNodeConfig.ip, MultipNodeConfig.port, new ConsulProvider(new ConsulProviderOptions()));
				//Remote.SpawnAsync(ClusterConfig.Name, Multiplier.TypeName, TimeSpan.FromSeconds(2));
				Props multiplierProps = Actor.FromProducer(() => new Multiplier());
				Actor.Spawn(multiplierProps);

			}
			else
			{
				Console.WriteLine("Wrong command line args. Please specify node type by using the param -r or -a.");
			}

			Console.WriteLine("Hit ENTER to allow this node to terminate.");
			Console.ReadKey();
		}
	}
}
