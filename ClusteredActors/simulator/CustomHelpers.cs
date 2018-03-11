using System;
using System.Diagnostics;
using Proto;
using Proto.Cluster;
using Proto.Remote;

namespace DomsProtoUtils
{
	// extend props
	public static class ProtoExtensions
	{
		public static Props RegisterAs(this Props prop, string name)
		{
			Remote.RegisterKnownKind(name, prop);
			return prop;
		}
	}

	public static class ClusterEx
	{
		public static (PID, ResponseStatusCode) BlockingGetActorBy(string kindName, string instanceName, TimeSpan retryDuration)
		{
			bool timedOut = false;
			Stopwatch timer = new Stopwatch();
			timer.Start();
			(PID, ResponseStatusCode) taskResult = (null, ResponseStatusCode.Unavailable);
			while (taskResult.Item2 == ResponseStatusCode.Unavailable && timedOut == false)
			{
				taskResult = Cluster.GetAsync(instanceName, kindName).Result;
				timedOut = timer.Elapsed > retryDuration ? true : false;
			}
			if(timedOut)
				System.Console.WriteLine("   Timedout ! -> " + kindName + " Cluster.GetAsync duration: " + timer.ElapsedMilliseconds.ToString());
			else
				System.Console.WriteLine("   OK: " + kindName + " Cluster.GetAsync duration: " + timer.ElapsedMilliseconds.ToString());
			return taskResult;
		}
	}

	public static class EventsEx
	{
		public static void HookIntoStream()
		{
			// From here on, asuming that Consul Service is running
			// DeadLetterEvent
			// MemberStatusEvent
			// EndpointConnectedEvent
			// EndpointTerminatedEvent
			// MemberJoinedEvent
			// MemberRejoinedEvent
			// MemberLeftEvent
			Actor.EventStream.Subscribe<ClusterTopologyEvent>
			(
				clusterTopologyEvent =>
				{
					Console.Out.WriteLine($"++ ClusterTopologyEvent: ");
					foreach (var status in clusterTopologyEvent.Statuses)
					{
						Console.Out.WriteLine($"++++ For {status.Address}");
						foreach (var kindNme in status.Kinds)
						{
							Console.Out.WriteLine($"++++++ Has Kind {kindNme}");
						}
					}
				}
			);

			Actor.EventStream.Subscribe<DeadLetterEvent>
			(
				letter =>
				{
				Console.Out.WriteLine($"++ Deadletter from {letter.Sender} to {letter.Pid} saying {letter.Message} ");
				}
			);
		}
	}
}
