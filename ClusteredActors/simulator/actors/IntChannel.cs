using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Proto;

sealed class IntChannel : IActor
{
	public static readonly string TypeName = MethodBase.GetCurrentMethod().DeclaringType.Name;
	private List<PID> _subscribers = new List<PID>();

	public static readonly string MainChannelName = "IntChannel";
	private int _counter = 0;

	public Task ReceiveAsync(IContext context)
	{
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
				// context sender is empty as it's not a request/reply message but just a simple send mesage
				PID subscriber = join.Sender;
				if (!_subscribers.Contains(subscriber))
				{
					_subscribers.Add(join.Sender);
					System.Console.WriteLine("Added subscriber with PID: " + join.Sender.ToString());
				}
				break;

			case Messages.LeaveChannel leave:
				// context sender is empty as it's not a request/reply message but just a simple send mesage
				_subscribers.Remove(leave.Sender);
				break;

			case Messages.IntValue inputNumber:
				System.Console.Write(( _subscribers.Count > 0 ? "X" : "."));
				_counter += inputNumber.Number;
				Messages.IntValue msg = new Messages.IntValue() { Number = _counter };
				// is this the most efficient way? feels like duplication is taking place under the hood. routers better i guess? ...
				foreach(PID pid in _subscribers)
				{
					context.Tell(pid, msg);
				}
				break;
		}
		return Actor.Done;
	}
}