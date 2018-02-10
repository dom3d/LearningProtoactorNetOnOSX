using System.Reflection;
using System.Threading.Tasks;
using Proto;
using Proto.Cluster;
using Proto.Remote;

sealed class Multiplier : IActor
{
	public static readonly string TypeName = MethodBase.GetCurrentMethod().DeclaringType.Name;
	private int _total = 0;

	Task IActor.ReceiveAsync(IContext context)
	{
		PID sender = context.Sender;
		switch (context.Message)
		{
			case Started _:
				System.Console.WriteLine("Multiplier started, my PID: " + context.Self.ToString());
				/*
				System.Console.WriteLine("Multiplier started, my PID: " + context.Self.ToString());
				(PID, ResponseStatusCode) taskResult = (null, ResponseStatusCode.Unavailable);
				while (taskResult.Item2 == ResponseStatusCode.Unavailable)
				{
					System.Console.Write(".");
					taskResult = Cluster.GetAsync("HeartBeat/IntChannel", IntChannel.TypeName).Result;
				}
				System.Console.WriteLine("Got int-channel reference");
				taskResult.Item1.Tell(new Messages.JoinChannel());
				*/

				PID channel = new PID("127.0.0.1:12005", "HeartBeat/IntChannel");
				System.Console.WriteLine("Targeting PID: " + channel.ToString());
				Remote.SendMessage(channel, new Messages.JoinChannel(), Serialization.DefaultSerializerId);

				//channel.Tell(new Messages.JoinChannel());
				System.Console.WriteLine("Sent join message");
				break;
			case Messages.IntValue msg:
				_total *= msg.Number;
				System.Console.WriteLine("Got Int heartbeat message");
				break;
		}
		System.Console.WriteLine(string.Format("Total: {0}", _total));
		return Actor.Done;
	}
}
