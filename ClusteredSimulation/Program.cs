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

			// Register protobuf messages
			Serialization.RegisterFileDescriptor(Messages.MessagesReflection.Descriptor);

			// start choosen node and join cluster
			if (args[0] == "-h")
			{
				Console.WriteLine("-h Mode: Heartbeat Node");

				// actor setup definition and registration of that definition using a custom method extension
				Props heartBeatSetup = Actor.FromProducer(() => new Heartbeat()).RegisterAs(Heartbeat.TypeName);
				Props intChannel = Actor.FromProducer(() => new IntChannel()).RegisterAs(IntChannel.TypeName);

				// Join cluster
				Console.WriteLine("Joining cluster ...");
				Cluster.Start(ClusterConfig.Name, HeartbeatNodeConfig.ip, HeartbeatNodeConfig.port, new ConsulProvider(new ConsulProviderOptions()));

				Console.WriteLine("Spawning core actors ...");

				// spawning both as root so they are visible from a Cluster.GetAsync perspective
				PID beatPID = Actor.SpawnNamed(heartBeatSetup, Heartbeat.TypeName);
				PID channelPID = Actor.SpawnNamed(intChannel, IntChannel.TypeName);
				Console.WriteLine("... telling heartbeat about int channel ...");
				beatPID.Tell(new Messages.TargetPID() { Target = channelPID });
				Console.WriteLine("OK");

			}
			else if (args[0] == "-s")
			{
				Console.WriteLine("-s Mode: summariser. Using Cluster.GetASync");
				Cluster.Start(ClusterConfig.Name, SumNodeConfig.ip, SumNodeConfig.port, new ConsulProvider(new ConsulProviderOptions()));
				var props = Actor.FromProducer(() => new Summariser());
				Actor.Spawn(props);
				Console.WriteLine("OK");
			}
			else if (args[0] == "-m")
			{
				Console.WriteLine("-m Mode: multiplier. Using hardcoded PID via Remote");
				Cluster.Start(ClusterConfig.Name, MultipNodeConfig.ip, MultipNodeConfig.port, new ConsulProvider(new ConsulProviderOptions()));
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
