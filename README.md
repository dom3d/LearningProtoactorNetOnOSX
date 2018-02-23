# Learning Protoactor-Net On OSX
This project is mostly documenting my journey of learning and understanding ProtoActor-Net and sharing my findings. Hopefully this makes it easier for others that also don't have a background in e.g. Akka.

# What is ProtoActor-Net ?
ProtoActor is a distributed Actor framework with focus on simplicity, performance, re-use of existing solutions and language interoperatability. ProtoActor merges aspects from Akka, Orleans and Erlang but terminology is heavily Akka driven.

Actor oriented architecture is all about interacting between program units via messages. Actor solutions prioritise availability and scalability.

# Preparation work to get started with Protoactor-Net on OSX
The Protoactor source code doesn’t build out of the box on OSX despite using the Cake Build system as of Jun 2017. I am using Visual Studio for Mac (MacVS) and prebuilt libs instead. (Note that I am not using docker/containers or any VM here in my examples)

## In the OSX / iterm2 terminal window:
### Installing the required Protobuffer tools
	brew install protobuf
	brew install --with-plugins grpc

Understand where the plugin is installed as you will need the full path when using the protobuffer compiler.

	which grpc_csharp_plugin

### Installing Consul (if you want to cluster)
	brew install consul

## In MacVS:
	create dotnet core console app
	project > add nuget packages  ? switch from all sources to configure sources > add and use "https://www.myget.org/F/protoactor/“ as location

# How to run things on OSX
Compiling protobuffer with gRPC: go into the directory with your message definitions that end with .proto. Have a script that does the following or do it manually (if the plugin is installed in a different location on your system, adjust as needed):

	protoc *.proto -I=./ --csharp_out=. --csharp_opt=file_extension=.g.cs --grpc_out . --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin

Be aware that you may want to references other proto files and add different directories for imports. In that situation add them via additional -I {path}. This can be a bit tricky. My recommendation (how I got it working) is to make e.g. 3rd protos that you want to import reside in a subfolder of your own protos.

```
- folder with your protos
--  subfolder with the protos you want to import
```

Example setup where library protos are in a subfolder:
```
#!/bin/bash
protoc ./messages/*.proto -I=./messages/ --csharp_out=./messages/ --csharp_opt=file_extension=.g.cs --grpc_out . --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin
```

Import would then like this
```
import "lib/Proto.Actor/protos.proto";
```

DotNetCore console app is not an exe like when using Mono but a DLL. Run it like this

	dotnet MyAppName.dll

Consul.io is used for the automatic Clustering aspect of ProtoActor. Get it up and running locally from the terminal like this:

	consul agent -dev -advertise 127.0.0.1

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

Grains, the typsafe request-response messaging approach in ProtoActor is using code-gen via a custom gRPC extension. Declare an Grain's (Virtual Actor)'s RPC interface via gRPC services like this:
```protobuf
// (...)
service HelloGrain
{
	// SayHello method accepts a HelloRequest message and returns a HelloReply message
	rpc SayHello (HelloRequest) returns (HelloReply) {}
}
```
Calling example usage in C# via ProtoActor Grains:
```cs
	// (TODO comment)
	var client = Grains.HelloGrain("GrainName");
	// calling a method of a ProtoActor Grain
	var res = client.SayHello(new HelloRequest()).Result;
```

Grain (gRPC service) implementation example:
```cs
public class HelloGrain : IHelloGrain
{
	public Task<HelloResponse> SayHello(HelloRequest request)
	{
		return Task.FromResult
		(
			new HelloResponse
			{
				Message = "Hello from typed grain"
			}
		);
	}
}
```

# Understanding the API Layers
ProtoActor is build in stages. More recent layers sit somewhat ontop of the previous one but with blurry intersections.

## Actor
'Actor' is the basic and core layer allowing one to create a system of actors that message each other within a single CPU/OS process.

## Remote
The 'Remote' layer allows you to manually connect ProtoActor Instances. It provides the means to
- messagage across 'remote' instances,
- spawn actors on remote instances,
- observe actors on remote instances
- etc

## Cluster
'Cluster' automates the aspect of building and managing a grid of connected ProtoActor instances. (Intance is sometimes referred to as Node or Server). It relies on 3rd party solutions such as Consul. An instances joins and leaves a Cluster via the Cluster API.

## Virtual Actors (Grains)
This feature automates the placement and spawning of Actors in a Cluster. This is mostly achieved by defining the RPC interfaces via protobuf definition files.

# Terminology
**(Warning might not always be accurate)**

## Influences
Protoactors uses terms from various frameworks, so in doubt, checkout their documentation. For example "Props" comes from the Akka world, Grains from the Orleans world and Process and Mailbox from the Erlang world.

The main author of ProtoActor ported **Akka** to DotNet. "Akka" for Java/JVM was inspired by Erlang and it's OTP. This time ProtoActor also took inspirations from Microsoft's **Orleans** and the simplicity experienced when working with  go-lang.

## Breakdown of Elements

### Process
In ProtoActor a Process is defined as something that processes messages. One could think of actors being a specialised form of a Processes. One can send User Messages as well as System Messages to a Process. Messages are handled via the Mailbox of a Process.

It's a very blurry line between Actor and Process in ProtoActor and that's reflected by various places using both names in an exchangable way. But technically the Process is actually only the thing that you can send messages to. It's the target of a message. Maybe "Target" would actually have been a better name.

There are LocalProcesses, RemoteProcesses, GuardianProcesses, FutureProcess and a RouterProcess. RemoteProcesses represent a Process (Actor) in a different ProtoActor instance. See Remote and Cluster for more details. A RouterProcess handles message forwarding in special way, depending of the router type.

### Mailbox
The Mailbox is the thing that stores messages that a Process received. It actually also covers the triggering of reactive processing as Tasks across Threads.

### Actor
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

## PID
ID to identify a process (actor) but could also hold a reference if the process (actor) is local. Spawning creates the ID.

	PID("127.0.0.1:8000", "actor_name");

It's composed by the IP:Port and the actor (proces) instance name.
When an Actor is spawn by another Actor, the given ID is the hierachical path like the following:

	{ "Address": "127.0.0.1:12005", "Id": "ParentName/ChildName" }

PID has an API to send message but those messages hit the mailbox directly and ignore any "Props" configuration - saying middlewares are ignore.

PID is somewhat "polluted" with some conviences methods such as: Tell, Request, SendSystemMessage, Stop. Eventually the use the ProcessRegistry to get a reference to the Process instance itself.

An ID has to be unique across the system and across a manual remoting setup or a cluster.

## ProcessRegistry
Provides IDs for new Process/Actor instances. If a name is not given for a new actor, then the ProcessRegistry will create one based on "$" + internal counter.

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

## Cluster
'Cluster' is a more automatic way to do Remoting / Peering. The Cluster layer takes care of connecting ProtocActor instances over the network - this way you no longer need to maintain ip addresses and ports of Remote-instances / remote-nodes manually. The Cluster handles the discovery.

Cluster uses Remote under the hood, so no need to do a Remote.Start. Just Cluster.Start which basically means your new ProtoActor instance is joining the cluster and becomes a member.

Clusters have a name, so you could run multiple ProtoActor clusters on the same Clustering system without theses "virtual clusters" seeing each other.

Cluster.get does a remote activation if needed. Duplication possible.

The default solution to provide cluserting is Consul. In consul the cluster name is the service name and each of those entries have tags for known actor kinds (actor configurators).

Cluster.Start: It starts the cluster module and joins the cluster. It is dependant on Consul so you need to have a consul agent running first. A better name would probably be "Cluster.Join"

Cluster.Shutdown should really be called "Leave" as it informs your app to leave the cluster.

Calls to Cluster.GetAsync may need to be repeated until success. In my test it took a while each time to actually have Get succeed. You can only get Root actors but not a child actor.

## Grain (Virtual Actor)
Grains are an additional convienience layer that does a lot of things under the automatically. It requires to use ProtoActor Cluster.

- automatic activation
- automatic placement (based on Props Kind)
- typed RPCs

Grains are created via a custom Protobuf extension provided by ProtoActor. This extenion does all the "glueing" via code generation. Grains allow to define a "typed" request & response experience via Probotuf RPCs.

Virtual actor means, you don't know exactly where in the cluster the ActorGrain is and if it's was actually spawned or not. When you request a reference (PID) to an actor, it may get spawned as consequence in case there was no instance alive. The cluster is partitioned by Prop type / aka kind.

Grains register themselves as Kind in the Remote layer.

The RPC layer send the method name as string, increase the message size and amount of generated garbage.

All grains are accessible via the Gain namespace.


http://proto.actor/docs/what%20is%20protoactor   
https://files.gitter.im/AsynkronIT/protoactor/kepm/blob
https://files.gitter.im/AsynkronIT/protoactor/YqnA/blob
A grain is a virtual actor, same as MS Orleans.. One can  specify RPC services in the proto files, and generate typed C# code and the Proto.Actor cluster will connect everything automatically. so node1 can ask for a grain of a specific type, and it will be found or created on another node
If you are using cluster, you can generate "Grains" using the protoc grain generator.. those are typed actors with an RPC like interface (much like Microsoft Orleans)
when you use that, it will handle failover for you
Grains in ProtoActor works like this: you generate grains from a Protobuf Service dfinition.. this gives you two things, a client that can talk to the grain , and a server interface which you need to implement

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

# Using ProtoActor ...

## The many (and thus confusing) ways to send a message
**pid.Tell** this posts the message to the maibox directly and ignores any middleware.

**Remote.SendMessage**  wraps the message into an evelope end sends it out right away via RemoteDelivery of the EndpointManager

**context.tell**  This sends a message to the target PID and respects the middleware setup as defined via Props/Kind.

**context.RequestAsync** Request-Response based message. The message gets packed into a MessagEnvelope. This is the request part.

**context.Respond** Request-Response based message. This is the response part. context.Sender is actually not null in this case and can be used. This is only for received Request messages (RPCs).

## The many (and thus confusing) ways to send spawn an actor
**Actor.Spawn** (and it's variations) spawns it as root Actor

**context.Spawn** (and it's variations) spawns it as child of the actor (actor.sef)


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

