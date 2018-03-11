using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Proto;
using Proto.Remote;
using Proto.Cluster;
using Proto.Schedulers.SimpleScheduler;
using DomsProtoUtils;

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
				// Won't spawn IntChannel with context as it does spawn it as child. But childs are not visible in a cluster, only root nodes
				break;
			case Messages.TargetPID targetMsg:
				_channel = targetMsg.Target;
				System.Console.WriteLine("Hearbeat got target channel PID: " + _channel.ToString());
				Messages.IntValue msg = new Messages.IntValue() { Number = 1 };
				this._scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(250), _channel, msg, out _cancelScheduler);
				break;
		}
		return Actor.Done;
	}
}