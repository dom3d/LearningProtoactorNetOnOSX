using System;
using System.Diagnostics;
using Proto;
using Proto.Cluster;
using Proto.Remote;

namespace ProtoExtensions
{
	// extend props
	public static class DomsProtoExtensions
	{
		public static void RegisterAs(this Props prop, string name)
		{
			Remote.RegisterKnownKind(name, prop);
		}
	}

	public static class ClusterHelpers
	{
		public static (PID, ResponseStatusCode) WaitUntilSpawned(string kindName, string instanceName, TimeSpan retryDuration)
		{
			System.Console.WriteLine("\n Hepler Cluster Spawn for: " + kindName);
			bool timedOut = false;
			Stopwatch timer = new Stopwatch();
			timer.Start();
			(PID, ResponseStatusCode) taskResult = (null, ResponseStatusCode.Unavailable);
			while (taskResult.Item2 == ResponseStatusCode.Unavailable && timedOut == false)
			{
				taskResult = Cluster.GetAsync(instanceName, kindName).Result;
				timedOut = timer.Elapsed > retryDuration ? true : false;
			}
			if(timedOut)
				System.Console.WriteLine("   Timedout ! -> " + kindName + " spawn duration: " + timer.ElapsedMilliseconds.ToString());
			else
				System.Console.WriteLine("   OK: " + kindName + " spawn duration: " + timer.ElapsedMilliseconds.ToString());
			return taskResult;
		}
	}
}
