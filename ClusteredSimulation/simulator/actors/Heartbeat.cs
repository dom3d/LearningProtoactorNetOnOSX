using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Proto;
using Proto.Cluster;
using Proto.Schedulers.SimpleScheduler;
using ProtoExtensions;

sealed class Heartbeat : IActor
{
	public static readonly string TypeName = MethodBase.GetCurrentMethod().DeclaringType.Name;
	SimpleScheduler _scheduler = new SimpleScheduler();
	CancellationTokenSource _cancelScheduler;
	PID _channel;

	public Task ReceiveAsync(IContext context)
	{
		PID sender = context.Sender;
		switch (context.Message)
		{
			case Started _:
				System.Console.WriteLine("Hearbeat started, my PID: " + context.Self.ToString());
				_channel = context.SpawnNamed(Actor.FromProducer(() => new IntChannel()), IntChannel.MainChannelName);
				Messages.IntValue msg = new Messages.IntValue() { Number = 1 };
				this._scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(250), _channel, msg, out _cancelScheduler);
				break;
		}
		return Actor.Done;
	}
}