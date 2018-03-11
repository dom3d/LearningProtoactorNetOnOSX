
## Cluster
'Cluster' is a more automatic way to do Remoting / Peering. The Cluster layer takes care of connecting ProtocActor instances over the network - this way you no longer need to maintain ip addresses and ports of Remote-instances / remote-nodes manually. The Cluster handles the discovery.

Cluster uses Remote under the hood, so no need to do a Remote.Start. Just Cluster.Start which basically means your new ProtoActor instance is joining the cluster and becomes a member.

Clusters have a name, so you could run multiple ProtoActor clusters on the same Clustering system without theses "virtual clusters" seeing each other.

Cluster.get does a remote activation if needed. Duplication possible.

The default solution to provide cluserting is Consul. In consul the cluster name is the service name and each of those entries have tags for known actor kinds (actor configurators).

Cluster.Start: It starts the cluster module and joins the cluster. It is dependant on Consul so you need to have a consul agent running first. A better name would probably be "Cluster.Join"

Cluster.Shutdown should really be called "Leave" as it informs your app to leave the cluster.

Calls to Cluster.GetAsync may need to be repeated until success. In my test it took a while each time to actually have Get succeed. You can only get Root actors but not a child actor.

A cluster is patitioned automatically by 'kind' using a PartitionActor.


## Retrieving a remote root actor via the Cluster
```cs
// example approach. In my tests I often needed to wait a good chunk of time for the first get, so here is one way to handle that
(PID, ResponseStatusCode) taskResult = (null, ResponseStatusCode.Unavailable);
while (taskResult.Item2 == ResponseStatusCode.Unavailable)
{
	taskResult = Cluster.GetAsync("ActorInstanceName", "ActorRegisteredKindName").Result;
}
```

[back](../README.md)