using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebSocketChat
{
	public class Program
	{
		public static void Main(string[] args)
		{
			
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory()) // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files
				.UseStartup<Startup>()
				.UseUrls("http://localhost:58642")
				.Build();

			host.Run();
		}
	}
}
