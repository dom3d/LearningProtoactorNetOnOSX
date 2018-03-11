using System.Reflection;
using System.Threading.Tasks;
using Proto;
using Proto.Cluster;
using Proto.Remote;
using DomsProtoUtils;

sealed class Summariser : IActor
{
	public static readonly string TypeName = MethodBase.GetCurrentMethod().DeclaringType.Name;
	private int _summary = 0;

	public Task ReceiveAsync(IContext context)
	{
		PID sender = context.Sender;
		switch (context.Message)
		{
			case Started _:
				System.Console.WriteLine("Summariser started, my PID: " + context.Self.ToString());
				(PID, ResponseStatusCode) taskResult = (null, ResponseStatusCode.Unavailable);
				taskResult = ClusterEx.BlockingGetActorBy(IntChannel.TypeName, IntChannel.TypeName, System.TimeSpan.FromSeconds(9));
				/*
				var result = Cluster.GetAsync(IntChannel.TypeName, IntChannel.TypeName).Result;
				System.Console.WriteLine($">> Tried to get IntChannel PID, got {result.Item2}");
				// For some reason this takes ages to get hold on, so we wait. Not sure how to do this less with less code
				(PID, ResponseStatusCode) taskResult = (null, ResponseStatusCode.Unavailable);
				while (taskResult.Item2 == ResponseStatusCode.Unavailable)
				{
					System.Console.Write(".");
					taskResult = Cluster.GetAsync("IntChannel", IntChannel.TypeName).Result;
				}
				*/
				System.Console.WriteLine("Got int-channel reference for: " + taskResult.Item1);
				taskResult.Item1.Tell(new Messages.JoinChannel() { Sender = context.Self });
				break;
			case Messages.IntValue msg:
				_summary += msg.Number;
				System.Console.WriteLine($"Got Int message {_summary}");
				break;
		}
		return Actor.Done;
	}
}
