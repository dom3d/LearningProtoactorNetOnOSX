using System;
namespace Issue_SenderIsNull.Config
{
	class Cluster
	{
		public static readonly string Name = "ProtoActorCluster";
	}

	class Node1
	{
		public static string ip = "127.0.0.1";
		public static int port = 12005;
	}

	class Node2
	{
		public static string ip = "127.0.0.1";
		public static int port = 12006;
	}
}
