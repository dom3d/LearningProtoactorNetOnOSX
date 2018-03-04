using System;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Remote;
using Chat;

namespace ChatWithGrainsExperiment
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello Grains example!");
			if (args.Length == 0)
			{
				Console.WriteLine("Missing command line args. Please specify node type by using the param -a or -b.");
				return;
			}

			Serialization.RegisterFileDescriptor(Chat.AllInOneReflection.Descriptor);

			if (args[0] == "-a")
			{
				Console.WriteLine("Node A Node");
				// Start the server and join the cluster. Known Actors will be spawned automatically
				Cluster.Start(NodeConfigA.ClusterName, NodeConfigA.ip, NodeConfigA.port, new ConsulProvider(new ConsulProviderOptions()));
				Console.ReadLine();
				Console.WriteLine("Shutting Down...");
				Cluster.Shutdown();
			}
			else if (args[0] == "-b")
			{
				Console.WriteLine("Node B Mode");
				// declare grains
				Grains.ChannelGrainFactory(() => new ChannelGrain());
				// start cluster
				Cluster.Start(NodeConfigA.ClusterName, NodeConfigA.ip, NodeConfigA.port, new ConsulProvider(new ConsulProviderOptions()));
				Console.ReadLine();
				Console.WriteLine("Shutting Down...");
				Cluster.Shutdown();
			}
			else
			{
				Console.WriteLine("Wrong command line args. Please specify node type by using the param -r or -a.");
			}
		}
	}
}
