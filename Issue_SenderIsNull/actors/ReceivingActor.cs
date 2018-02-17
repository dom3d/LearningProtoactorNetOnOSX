using System;
using System.Threading.Tasks;
using Proto;

namespace Issue_SenderIsNull.Actors
{
	sealed class ReceivingActor : IActor
	{
		public Task ReceiveAsync(IContext ctx)
		{
			var receivedMsg = ctx.Message;
			if (receivedMsg is Messages.EmptyMessage empty)
			{
				Console.WriteLine("Received empty message, let's check context ...");
				if(ctx.Sender == null)
				{
					Console.WriteLine("Sender is null ...");
				}
				else
				{
					Console.WriteLine($"Sender is {ctx.Sender.ToString()}");
				}
			}

			return Actor.Done;
		}
	}
}
