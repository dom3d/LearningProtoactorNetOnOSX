using System.Reflection;
using System.Threading.Tasks;
using Proto;
using Proto.Cluster;
using Proto.Remote;

sealed class Multiplier : IActor
{
	public static readonly string TypeName = MethodBase.GetCurrentMethod().DeclaringType.Name;
	private int _total = 1;

	Task IActor.ReceiveAsync(IContext context)
	{
		PID sender = context.Sender;
		switch (context.Message)
		{
			case Started _:
				System.Console.WriteLine("Multiplier started, my PID: " + context.Self.ToString());
				// Here we use a hardcoded address. See Summariser for using the Cluster API to get hold on the PID
				PID channel = new PID("127.0.0.1:12005", IntChannel.TypeName);
				System.Console.WriteLine("Targeting PID: " + channel.ToString());
				Remote.SendMessage(channel, new Messages.JoinChannel() { Sender = context.Self }, Serialization.DefaultSerializerId);
				System.Console.WriteLine("Sent join message");
				break;
			case Messages.IntValue msg:
				_total *= msg.Number;
				int squared = msg.Number * msg.Number;
				System.Console.WriteLine($"Got Int heartbeat message. Pump: {msg.Number} Squared: {squared} Total: {_total} ");
				break;
		}
		return Actor.Done;
	}
}
