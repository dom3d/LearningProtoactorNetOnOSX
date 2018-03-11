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

			if (args[0] == "-b")
			{
				Console.WriteLine("Node B Node: users");
				// registering UserGrains as spawnable grain for this Cluster node. It does do a Remote.RegisterKind under the hood.
				Grains.UserGrainFactory(() => new UserGrain());
				Console.WriteLine("User grain registered, joining cluster...");

				// Start the server and join the cluster. Known Actors will be spawned automatically
				Cluster.Start(NodeConfigA.ClusterName, NodeConfigA.ip, NodeConfigA.port, new ConsulProvider(new ConsulProviderOptions()));

				Console.WriteLine("Arnold Grain...");
				var arnold = Grains.UserGrain("Arnold");
				Console.WriteLine("... asking him to join Global...");
				var result = arnold.JoinChannel(new TargetChannel() { ChannelName = "Global"} ).Result;
				Console.WriteLine("... ok ... what's the result?? ...");
				Console.WriteLine("Success? " + result.Success.ToString());

				// wait for user input to shutdown the node
				Console.ReadLine();
				Console.WriteLine("Shutdown of Node B");
				Cluster.Shutdown();
			}
			else if (args[0] == "-a")
			{
				Console.WriteLine("Node A Mode: channels");
				// register ChannelGrain as spawnable for this Cluster Node
				Grains.ChannelGrainFactory(() => new ChannelGrain());
				Console.WriteLine("Channel grain registered, joining cluster...");

				// join cluster
				Cluster.Start(NodeConfigA.ClusterName, NodeConfigA.ip, NodeConfigA.port, new ConsulProvider(new ConsulProviderOptions()));
				Console.WriteLine("OK. Start now the node B with users");

				// wait for user input to shutdown the node
				Console.ReadLine();
				Console.WriteLine("Shutdown of Node A");
				Cluster.Shutdown();
			}
			else
			{
				Console.WriteLine("Wrong command line args. Please specify node type by using the param -r or -a.");
			}
		}
	}
}
