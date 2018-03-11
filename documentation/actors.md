

## Actor
Explained in a simplified way: Actors are a way to decompose your application logic into small units that don't use/reference each other directly but only interact through messages. Actors use addresses that potentially represent other actors and those are used to define where a message should be send to.

Actor is a reactive unit that processes messages from a mailbox. A message is not passed directly into an ProtoActor but via a context object.

Although ProtoActor talks about Actors mostly, under the hood the concept of "actors" require a composition of systems such Mailbox, Context, Process, Props etc.

Axioms of Actors:
- an actor can send messages to other actors (send)
- an actor can create new actors (create)
- an actor can decide how to handle the next message (become)

Recommended to read:
- http://proto.actor/docs/actors
- https://github.com/AsynkronIT/protoactor-go/blob/dev/actor/doc.go
- https://www.infoq.com/br/presentations/failing-gracefully-with-the-actor-model
- https://github.com/rogeralsing/Presentations/tree/master/FailingGracefully/QConSP

### Process
In ProtoActor a Process is defined as something that processes messages. One could think of actors being a specialised form of a Processes. One can send User Messages as well as System Messages to a Process. Messages are handled via the Mailbox of a Process.

It's a very blurry line between Actor and Process in ProtoActor and that's reflected by various places using both names in an exchangable way. But technically the Process is actually only the thing that you can send messages to. It's the target of a message.

There are LocalProcesses, RemoteProcesses, GuardianProcesses, FutureProcess and a RouterProcess. RemoteProcesses represent a Process (Actor) in a different ProtoActor instance. See Remote and Cluster for more details. A RouterProcess handles message forwarding in special way, depending of the router type.

## PID
ID to identify a process (actor) but could also hold a reference if the process (actor) is local. Spawning creates the ID.

	PID("127.0.0.1:8000", "actor_name");

It's composed by the IP:Port and the actor (process) instance name.
When an Actor is spawned by another Actor as child, the given ID is the hierachical path like the following:

	{ "Address": "127.0.0.1:12005", "Id": "ParentName/ChildName" }

PID has an API to send message but those messages hit the mailbox directly and ignore any "Props" configuration of the actor and therefore middlewares are ignored.

PID is somewhat "polluted" with some conviences methods such as: Tell, Request, SendSystemMessage, Stop. Eventually the use the ProcessRegistry to get a reference to the Process instance itself.

An ID has to be unique across the system and across a manual remoting setup or a cluster.

## ProcessRegistry
Provides IDs for new Process/Actor instances. If a name is not given for a new actor, then the ProcessRegistry will create one based on "$" + internal counter.

### Mailbox
The Mailbox is the thing that stores messages that a Process received. It actually also covers the triggering of reactive processing as Tasks across Threads.

### Context
Context is passed to an Actor's receive method and contains the Actor's setup and message and related APIs, such as:

- received message
- configuration (middleware etc)
- hierachy

While processing a message in an actor, you can access the following via the Context object:

- the actor's PID via context.Self
- the actor's parent PID via context.Parent
- ability to send messages. Doing this via the context makes message go through the configure middleware
- sender's PID for request based messages: context.Sender. WARNING can be NULL !!! Only send when reply expect thus only when Request is used. Should be named: RequestSender
- methods to spawn actors as children (spawn method names of the context are not explicitly saying so unfortnuately.)
- stash pending messages e.g. for recovery purposes
etc.

## Props
Props is the configurator of an Actor - it holds the recipe of how to configure a specific Actor when the Actor gets spawned.

The 'Props' is taken from movies and theather where an object, that is used (on stage or on screen) by actors during a performance or screen production, is called a prop.

Sometimes Props is also referred to as "kind" in ProtoActor. You register a Kind by handing over a Prop and it's name.

Props define what creates the actor, what kind of messaging middleware is to use, what supervision strategy to put in place, and even what kind of mailbox to use.

A cluster is patitioned automatically by 'kind' using a PartitionActor.

Alternative name you can keep in your mind for Props that could be suiteable are: Configurator, Template, Receipe, Setup, Builder, Schema.

## Remote
Remote is the layer that allows to connect ProtoActor instances with each other. Peering ProtoActor instanecs over network basically. Each instance becomes a node with an Endpoint; a server with an adresss.

You can

- "activate" a Remote Process/Actor/Props (Remote.SpawnNamedAsync)
- send Messages to a Remote Process/Actor (Remote.SendMessage)
- watch Remote Processes/Actor (e.g. to be informed about it's termination)

In order to be able to spawn Actors from remote nodes, the remote ProtoActor instance has to register Actor Props ('Actor Setups') via Remote.RegisterKnownKinds first. So only pre-registered Props can be spawned remotely. Be aware that you can register different kinds using the same name - but that's a problem because the name has to be unique across the cluster and across all kinds registered.

Remote Start will start the gRPC server on the given IP and Port. Maybe "Peering" or something like that would be a better name.

Ability to watch a remote Actor: When a node is disconnected, and if someone is watching an actor on that node, then the EndpointWatcher is supposed to fire a Terminated event for that actor.

ProtoActor dont support remote supervision, when you spawn remote actors they are root actors on the remote system.

## Activations / Activator
"Named" Activations - is spawning an Actor via it's kind using unique name across kinds and acts as global ID of the register kind. It's possible to register multiple kinds with the same name without errors, unfortunately.

Spawning is done by a special system actor called Activator. It's one of those hidden things that appear here and there in the API but isn't very explicit.


# Quick intro to working with .proto files
ProtoActor uses Google's Protobuffer which is a serialisation solution that uses code-generation. One has to define the messages in a .proto file and then compile them with a standalone tool.

Below a simple example for a message. ( filename: messages.proto )
```protobuf
// format / version header
syntax = "proto3";
// namespace comes from the package name unless cssharp_namespace is used
package messages;
// it's a bit redundant here but more complex namescpaes possible if desired
option csharp_namespace = "Messages";

message MessageWithoutData {}
message MessageWithSingleStringField
{
	// the =1 part is the field position
	string StringData=1;
}
```

Make sure that you include into your project the generated **xyz.g.cs** file.

Then in C#
```CS

// "Messages." part comes from the declared namespace in protobuf
// "MessagesReflection" part is based on protobuf file-name (here messages.proto)
using ExampleProtocol = Messages.MessagesReflection;

// (...)
	// Register protobuf messages in ProtoActor
	Serialization.RegisterFileDescriptor(ExampleProtocol.Descriptor);

```
If you want to reference other Protobuf message types from within a Protobuf file, use imports. Example:
```protobuf
// header
syntax = "proto3";
package messages;
import "shared/shared.proto";
option csharp_namespace = "Messages";
```

If you want to reference ProtoActor data types (e.g. PID) or message types, download the 3rd party folders and put them into a subfolder. Example:
```protobuf
syntax = "proto3";
package messages;
option csharp_namespace = "Messages";
// import
import "lib/Proto.Actor/protos.proto";
// using the imported type via it's namespace
message JoinChannel { actor.PID sender = 1; }
```

Documentation for Proto3 is here: https://developers.google.com/protocol-buffers/docs/proto3

Well known datatypes:
https://github.com/google/protobuf/blob/master/src/google/protobuf/unittest_well_known_types.proto




## Flexibility via Props

**Router:** message forwarder or multiplier
Somebody said: "I'm using round robin, consistent hash and broadcast routers in my chat server"

**Middleware:** message handler or transformator that plugs into the path of a message to it's end destination.

Middleware example: adding debug infos into the messaging flow of an actor:
https://github.com/bnayae/ProtoActorPlayground/blob/master/src/HeloWorld/HelloProto/SupervisorActor.cs

**Supervision:** Ability of an actor to spawn or despawn other actors and thus restart them on failure or for other scnearios. Directives are: Resume, Restart, Stop, Escalate
If I restart an actor with the supervision strategy is the PID the same? Yes, same PID, messages should still be in the mailbox and start coming as soon as actor is restarted
the message that caused the failure will be lost however unless you explicitly stash it
 proper way to kill a child actor from a parent? PID.Stop();
 Parent is only available when you spawn via the Context and the primary reason to do that is for supervision. If the child fails, that is escalated to the parent. If you don't plan to use supervision then you can also just embed Self in a message

**Supervision Strategy:** a pattern or recipe for how to handle failure in children. For example restart all children when one child fails. etc.
**Watcher / Watching:** if an Actors dies that is watched, the Watcher received a notification about the Termination.
**Parent:** Parents are implicitly watching their children (parent Actors that is).
**Schedulers:**
Have an interface in case persitent schedulers are required.

-------------

.md)


------------

# TBD

http://getakka.net/docs/concepts/actors
http://proto.actor/docs/actors  :  An actor is a container for State, Behavior, a Mailbox, Children and a Supervisor Strategy.

## Deeper topics, unsorted notes. Under the hood stuff:

**Actor Spawning**: creation of a LocalContext and a LocalProcess (with a new Mailbox) that is registered in the ProcessRegistry to get a unique PID. A first SystemMessage (Started) is sent to the Actor.

**Message:** http://proto.actor/docs/messages

one thing to consider: while protoactor uses queues internally to route messages to actors, those queues are just in-memory (meaning if the process dies your messages are gone).

**SystemMessage:** a predefined message by the Protoactor framework such as Stop. Use the "SendSystemMessage" API
**UserMessage:** custom messages defined for the app. Use the "Tell" or "SendUserMessage" API.
**MessageEnvelope:** The meta-data of a message. This is used for Request-Reply based messaging (only?). It holds references to the message, the sender, the target and the message header
**MessageHeader:** String-based Key-Value store with meta data for the message. Confusion trap due to bad naming imho: Header vs Headers; it's a single header dictionary but setting a value means "setting the header"
**DeadLetter:** http://proto.actor/docs/durability
**Dispatcher:** ?
**EventBus** is the central message bus used by the cluster or system (?). Access via Instance Singleton. subscribe to member status events and the like. e.g. EventStream.Instance.Subscribe(...)

**EventStream**
The EventStream is used internally in Proto.Actor to broadcast framework events.
http://proto.actor/docs/golang/eventstream#usages
eventstream is somewhat of a infrastructure utility, you can absolutely use it, we use it internally in ProtoActor, e.g. Remote and Cluster makes use of it to distribute events in a pub sub way.
But do note that it is only in process. it is not a full fledged cluster pub sub

Eventstream is pretty much just a pubsub direct invocation thing
anything passed to the eventstream will directly find its subscribers and synchronously call the subscribers
those subscribers can ofc be lambdas that do Tell to some actor to make it async

http://proto.actor/docs/golang/eventstream#usages
http://proto.actor/docs/golang/eventstream

**ActorClient / RootContext:** ?
**ContextState:** None, Alive, Restarting, Stopping

**system actors**
Partition Actor  (partion for host). Partion owns actor names and knows the location of the actor?
Activation Actor

**endpointwriter**
endpoint writer do batch messages that are intended for the same host

Google's **Protobuffers** is used for encoding and decoding messages. You describe the structure of a message ahead of time and use the compiler to generate the code you need to serialise and derserialise the efficiently encoded message fast.

**gRPC** is a Remote Procedure Call framework using Protobuffers that provides a plugin for the Protobuffer compiler to generate the server and client code for Protoactor to create "Grain" Actors.

## **general things to know**
Proto.Actor is non-bocking via Async in C# and Coroutines in Kotlin (why not use Coroutines in c# e.g. via this https://github.com/cschladetsch/Flow)

Protoactor is a library for implementing actor-based systems. it uses gRPC as a transport to handle communication between actors over the network. if you need actors talking over the network, protoactor (with remote) is a possible solution.


----------------------------------------------------------------
----------------------------------------------------------------
END of old notes. Below is most is just a scratchpad 
----------------------------------------------------------------
----------------------------------------------------------------

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


Actor self-destructoin: context.Self().Stop() also works
Actor destruction: pid.Stop()

-------
# Mechanics
Spawning : Local, Remote, Cluster, Grain




