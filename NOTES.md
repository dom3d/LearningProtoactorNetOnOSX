docs
http://proto.actor/docs/what%20is%20protoactor#relation-to-microsoft-orleans
===================================================================================================================


code snipped from the internet to checkout / inspect / learn from

===================================================================================================================


var props = Actor.FromProducer(() => new GatewayActor(logger))
              .WithReceiveMiddleware(Monitoring.ForReceiveMiddlewareUsing(monitoringProvider))
              .WithSenderMiddleware(Monitoring.ForSenderMiddlewareUsing(monitoringProvider));



 public GatewayActorProvider(IServiceProvider serviceProvider, IMonitoringProvider monitoringProvider)
        {
            var logger = serviceProvider.GetService<ILogger<GatewayActor>>();

            var props = Actor.FromProducer(() => new GatewayActor(logger))
                .WithReceiveMiddleware(Monitoring.ForReceiveMiddlewareUsing(monitoringProvider))
                .WithSenderMiddleware(Monitoring.ForSenderMiddlewareUsing(monitoringProvider));

            ActorInstance = Actor.SpawnNamed(props, "Gateway");

       
        }


============================================================================================

Cluster.GetAsync(”SomeGrainId”, ”SomeKnownProps”) will give you access to a grain, autospawned on some node, with the Id SomeGrainId


============================================================================================
Libs


https://github.com/MiloszKrajewski/Proto.Persistence.AnySql
https://github.com/MiloszKrajewski/Proto.Serialization.Json

-----

I use cluster to get actors and organizations, so they will be distributed across nodes
Use AWS ELB to distribute incoming websock connections across nodes
so sessions actors are created on the node the websocket request was started, talking to User actors which will be located via cluster.Get
Same with organizations
I didn't try to optimize Users and Organizations together, because if we have a huge org with 10,000's users, I'd want them evenly distributed anyway
So we'll ultimately be relying on the remoting layer very heavily 
flow is this:

Stuart Carnie @stuartcarnie Jan 23 2017 20:39
When a user sends a message to the server,
messages is transformed into protobuf message
message is sent to organization PID
organization PID checks chat type and forwards to team / group actor or target user actor
team / user actor verifies they will accept message from user, if so, proceed
sends message to archive actor, which is a round-robin router storing messages in Elasticsearch
upon reply from archive actor, then forward message to target sessions
in the case of the team, it uses a broadcast group router
------

@raskolnikoov yes, we only set the sender if there is ment to be a reply, we do not set sender for fire and forget messaging
--

turns out, Proto.Actor C# is 30 times faster in terms of msg throughput in process.
10 times faster over network..

One of the Erlang consultants had a hard time believing the perf results

-> Is Server GC True for max speed
------


errors not caught:
- reusing the same actor name


-------
other peoples projects:
https://github.com/esbencarlsen/vivego
https://github.com/CodingMilitia/ProtoActorSample/blob/master/src/CodingMilitia.ProtoActorSample.Client/Program.cs
https://github.com/Vip56/FixNetAndCorefxDemo/tree/master/Middleware

evenstream example
https://github.com/copypastedeveloper/Proto.Actor.Demo/blob/e5d3936f078e1425574c8162e01ca5efb85ce92a/RemoteTodoAdder/Program.cs

https://github.com/joaofbantunes/ActorPerEntityProtoActorSample/blob/68816de504dab8a2bde63310013d712b3bbea4b4/src/CodingMilitia.ActorPerEntityProtoActorSample.Client/Program.cs


weakish error message. can't find the partition for the Props kind?

            //Get Pid
            var address = MemberList.GetPartition(name, kind);

            if (string.IsNullOrEmpty(address))
            {
                return (null, ResponseStatusCode.Unavailable);
            }

=====

performance pitsladd to check?

Async Timeouts
https://youtu.be/r738tcIstck?t=1438

Timeout Cancellation tokens use timer under the hood which uses a global lock. So cancel those timers upon task success.
https://youtu.be/r738tcIstck?t=1588

Concurrent Dictionary: massive locking when using Count
https://youtu.be/r738tcIstck?t=1683

Overwrite Equals and GetHascode to avoid boxing
https://youtu.be/r738tcIstck?t=2244

Avoid locking where possible, e.g. central config data
https://youtu.be/r738tcIstck?t=2675

---

https://www.youtube.com/watch?v=CSPSvBeqJ9c

-------


broadcast
so:
Hash all names, finding out all partitions
Ask all partitions for the location of all actors in the list for that partition
Wait for all partitions to respond
group by host
send a special message to a special actor on each host, that broadcasts the message to the target actors.
and just hash all names, group by partition, send a broadcast message to each partition.
each partition then groups by target host, and forwards the broadcast


each node registers what actor "Kinds" its capable of spawning,
when you say Get("Roger","User") it will know exactly what nodes can spawn that
"what actor kinds do I support"

untyped Receive actors, or to code-genned typed actors


This is a sketch on how it works. instead of having one partition actor, there is now one partition actor per registered kind for that node.
Membership status changes/events are sent via the actor.EventStream. so the cluster provider publishes events, the Membership actor subscribes to them and publishes more finegrained events
those finegrained events are then routed to the relevant partition actors


remotewatch: : This shows how you can watch remote actors and get notified if the node crashes



ctx.Spawn(routing.FromGroupRouter(routing.NewBroadcastGroup(pids...)))




-------
# Mechanics
Spawning : Local, Remote, Cluster, Grain



