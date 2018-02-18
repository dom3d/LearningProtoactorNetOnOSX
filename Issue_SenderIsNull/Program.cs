using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Proto;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Remote;

namespace Issue_SenderIsNull
{

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello to Issue_SenderIsNull");
			Console.WriteLine("Start with -1 = Node 1. Then -2 = Node 2.");

			if (args.Length == 0)
			{
				Console.WriteLine("Missing command line args. Please specify node type by using one of the params");
				return;
			}

			// From here on, asuming that Consul Service is running

			// Register protobuf messages
			Serialization.RegisterFileDescriptor(Messages.MessagesReflection.Descriptor);

			// start choosen node and join cluster
			if (args[0] == "-1")
			{
				Console.WriteLine("-1 Mode: starting as Node 1");
				Console.WriteLine("> Test A) checking sender in a purely local mode");

				Console.WriteLine(">> Spawning receiver ");
				Props receiverProp = Actor.FromProducer(() => new Actors.ReceivingActor());
				PID receiverPID = Actor.SpawnNamed(receiverProp, "Receiver");

				Console.WriteLine(">> Spawning sender ");
				Props senderProp = Actor.FromProducer(() => new Actors.SendingActor());
				PID senderPID = Actor.SpawnNamed(senderProp, "Sender");

				Task.Delay(2000).Wait();

				Console.WriteLine(">> telling sender to send msg to receiver... ");
				Messages.PidInfo info = new Messages.PidInfo();
				info.Address = receiverPID.Address;
				info.Id = receiverPID.Id;
				senderPID.Tell(new Messages.TriggerSendEmptyMsgTo() { TargetPID = info }); // boom !!
				// 
			}

			Console.WriteLine("Hit ENTER to allow this node to terminate.");
			Console.ReadKey();
		}
	}
}
