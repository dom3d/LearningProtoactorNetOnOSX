using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Proto;

sealed class IntChannel : IActor
{
	public static readonly string TypeName = MethodBase.GetCurrentMethod().DeclaringType.Name;
	private List<PID> _subscribers = new List<PID>();

	public static readonly string MainChannelName = "IntChannel";

	public Task ReceiveAsync(IContext context)
	{
		PID sender = context.Sender;
		if(context.Message.GetType() != typeof(Messages.IntValue))
			System.Console.WriteLine("Actor Hash: " + context.Actor.GetHashCode().ToString() + " got msg: " + context.Message.GetType().ToString());
		
		switch (context.Message)
		{
			case Started _:
				System.Console.WriteLine("Int Channel started, my PID: " + context.Self.ToString());
				break;

			case Restarting _:
				System.Console.WriteLine("Int Channel restarting, current and soon old PID: " + context.Self.ToString());
				break;

			case Messages.JoinChannel join:
				System.Console.WriteLine("Hola, join message received");
				if (sender != null)
				{
					System.Console.WriteLine("Got join message from " + sender.ToString());
					if (!_subscribers.Contains(sender))
					{
						_subscribers.Add(sender);
						System.Console.WriteLine("Added subscriber with PID: " + sender.ToString());
					}
				}
				else
				{
					System.Console.WriteLine("Sender is actually null, so i can't add him :|");
				}
				break;

			case Messages.LeaveChannel leave:
				_subscribers.Remove(sender);
				break;

			case Messages.IntValue inputNumber:
				System.Console.Write(( _subscribers.Count > 0 ? "X" : "."));
				Messages.IntValue msg = new Messages.IntValue() { Number = inputNumber.Number };
				// is this the most efficient way? feels like duplication is taking place under the hood
				foreach(PID pid in _subscribers)
				{
					pid.Tell(msg);
				}
				break;
		}
		return Actor.Done;
	}
}