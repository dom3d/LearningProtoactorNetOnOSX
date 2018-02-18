using System;
using System.Threading.Tasks;
using Proto;

namespace Issue_SenderIsNull.Actors
{
	sealed class ReceivingActor : IActor
	{
		public Task ReceiveAsync(IContext ctx)
		{
			Console.WriteLine($"Receiver got message {ctx.Message}");
			if (ctx.Message is Messages.EmptyMessage empty)
			{
				Console.WriteLine("Received empty message, let's check sender in context ...");
				if (ctx.Sender == null)
				{
					Console.WriteLine("... Sender is null ...");
				}
				else
				{
					Console.WriteLine($"... Sender is {ctx.Sender.ToString()}");
				}
			}
			return Actor.Done;
		}
	}
}
