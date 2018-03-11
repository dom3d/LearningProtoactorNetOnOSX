using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.FileProviders;
using System.Text;

// code from https://docs.microsoft.com/en-us/aspnet/core/fundamentals/websockets
namespace WebSocketChat
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}

			SetFileServingUp((app));
			SetWebsocketUp(app);
		}

		private void SetFileServingUp(IApplicationBuilder app)
		{
			// requires package: Microsoft.AspNetCore.StaticFiles
			// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files

			// index.htm, index.html, default.htm, default.html
			app.UseDefaultFiles();

			// For the wwwroot folder
			app.UseStaticFiles();

			// alternative to the both above combined: app.UseFileServer(enableDirectoryBrowsing: false);
		}

		private void SetWebsocketUp(IApplicationBuilder app)
		{
			// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/websockets
			var webSocketOptions = new WebSocketOptions()
			{
				KeepAliveInterval = TimeSpan.FromSeconds(10),
				ReceiveBufferSize = 4 * 1024
			};
			app.UseWebSockets(webSocketOptions);
			app.Use
			(
				async (context, next) =>
				{
					if (context.Request.Path == "/ws")
					{
						if (context.WebSockets.IsWebSocketRequest)
						{
							WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
							await Echo(context, webSocket);
						}
						else
						{
							context.Response.StatusCode = 400;
						}
					}
					else
					{
						await next();
					}

				}
			);
		}

		private async Task Echo(HttpContext context, WebSocket webSocket)
		{
			var buffer = new byte[1024 * 4]; // not good, need to be taken from pool and reserved for task
			WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			while (!result.CloseStatus.HasValue)
			{
				string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
				Console.WriteLine($"Received a Websocket msg, content: {message}");
				await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

				// i should switch to SocketAsyncEventArgs later
				result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			}
			await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
		}
	}
}
