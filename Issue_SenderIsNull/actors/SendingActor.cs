using System;
using System.Threading.Tasks;
using Proto;

namespace Issue_SenderIsNull.Actors
{
	sealed class SendingActor : IActor
	{
		public Task ReceiveAsync(IContext ctx)
		{
			var receivedMsg = ctx.Message;
			Console.WriteLine("Sender got message");
			try
			{
				if (receivedMsg is Messages.TriggerSendEmptyMsgTo destinationData)
				{
					Console.WriteLine("Asked to send empty to destination ...");
					PID target = new PID(destinationData.TargetPID.Address, destinationData.TargetPID.Id);
					Console.WriteLine($"Sending Message to {target.ToString()}");
					target.Tell(new Messages.EmptyMessage());

				}
				else if (receivedMsg is Messages.TriggerSendMsgWithPIDTo targetData)
				{

					PID target = new PID(targetData.TargetPID.Address, targetData.TargetPID.Id);
					Console.WriteLine($"Sending Message incl. sender PID to {target.ToString()}");
					var pidInfoMsg = new Messages.PidInfo() { Address = ctx.Self.Address, Id = ctx.Self.Id };
					var messageToSend = new Messages.MessageWithPID() { SenderPID = pidInfoMsg };
					target.Tell(messageToSend);
					Console.WriteLine((".. sent msg."));

				}
			}
			catch(Exception e)
			{
				Console.WriteLine($"Exception: {e.ToString()}");
			}

			return Actor.Done;
		}
	}
}
