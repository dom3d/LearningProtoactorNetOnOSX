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

DotNetCore console app is not an exe like when using Mono but a DLL. Run it like this

	dotnet MyAppName.dll

Consul.io is used for the automatic Clustering aspect of ProtoActor. Get it up and running locally from the terminal like this:

	consul agent -dev -advertise 127.0.0.1

# Understanding the API Layers
ProtoActor is build in stages as layers and each layer sits ontop of the previous one:

## Actor
'Actor' is the basic and core layer allowing one to create a system of actors that message each other within a single CPU/OS process.

## Remote
The 'Remote' layer allows you to manually connect ProtoActor Instances. It provides the means to
- messagage across 'remote' instances,
- spawn actors on remote instances,
- observe actors on remote instances
- etc

## Cluster
'Cluster' automates the aspect of building and managing a grid of connected ProtoActor instances. (Intance is sometimes referred to as Node or Server). It relies on 3rd party solutions such Consul. An instances joins and leaves a Cluster via the Cluster API.

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

It's a very blurry line between Actor and Process in ProtoActor and that's reflected by various places using both names in an exchangable way. But technically the Process is actually only the thing that you can send messages to. It's the target of a message.

Maybe "Target" would actually have been a better name.

### Mailbox
The Mailbox is the thing that stores messages that a Process received. It actually also covers the triggering of reactive processing as Tasks across Threads.

### Actor
Explained in a simplified way: Actors are a way to decompose your application logic into small units that don't use/reference each other directly but only interact through messages. Actors use addresses representing other actors to define where a message should be send to.

Actor is a reactive unit that processes messages from a mailbox. A message is not passed directly into an ProtoActor but via a context object.

Although ProtoActor talks about Actors mostly under the hood the concept of "actors" require a composition of systems such Mailbox, Context, Process, Props etc.

Axioms of Actors:
- an actor can send messages to other actors (send)
- an actor can create new actors (create)
- an actor can decide how to handle the next message (become)

Recommended to read:
- http://proto.actor/docs/actors
- https://github.com/AsynkronIT/protoactor-go/blob/dev/actor/doc.go
- https://www.infoq.com/br/presentations/failing-gracefully-with-the-actor-model

## PID
ID to identify a process (actor) but could also hold a reference if the process (actor) is local. Spawning creates the ID.

	PID("127.0.0.1:8000", "actor_name");

It's composed by the IP:Port and the actor (proces) instance name.
When an Actor is spawn by another Actor, the given ID is the hierachical path like th following:

	{ "Address": "127.0.0.1:12005", "Id": "ParentName/ChildName" }

PID is somewhat "polluted" with some conviences methods such as: Tell, Request, SendSystemMessage, Stop. Eventually the use the ProcessRegistry to get a reference to the Process instance itself.

## ProcessRegistry
Provides IDs for new Process/Actor instances. If a name is not given for a new actor, then the ProcessRegistry will create one based on "$" + internal ounter.

### Context
Context is passed to an Actor and contains contextual information and provides an API to interact with that data (such as actor children). While processing a message in an actor, you can access the following via the Context object:

- the actor's PID via context.Self
- the actor's parent PID via context.Parent
- message sender's PID: context.Sender. WARNING can be NULL !!!
- methods to spawn actors as children
- stash pending messages e.g. for recovery purposes
etc.

## Props
The 'Props' is taken from movies and theather where an object used on stage or on screen by actors during a performance or screen production.

For ProtoActor think of Props being a "Recipe/Configuration" for the creation of an Actor, sometimes it's also referred to as "kind". You register a kind by handing over a Prop and give it a name.

(Actor)Producer: creates a new Actor. MailboxProducer creates a new mailbox. Message routing middleware configuration, Supervision Strategy. A cluster is patitioned automatically by 'kind' using a PartitionActor.
('Kind' as a partition is also referred as Grain?)

Actors combined with a setup are referred to as "kind". "Named Actors" helps when there a multiple of the same kind to distinguish between them when it's needed'

## Remote
Remote is the layer that allows to connect ProtoActor instances with each other. Each instance becomes a node with an Endpoint; a server with an adresss.

You can

- "activate" a Remote Process/Actor/Props (Remote.SpawnNamedAsync)
- send Messages to a Remote Process/Actor (Remote.SendMessage)
- watch Remote Processes/Actor (e.g. to be informed about it's termination)

In order to be able to spawn Actors from remote nodes, the remote ProtoActor instance has to register Actor Props ('Actor Setups') via Remote.RegisterKnownKinds first. So only pre-registered Props can be spawned remotely.

Remote Start will start the gRPC server on the given IP and Port.

Ability to watch a remote Actor: When a node is disconnected, and if someone is watching an actor on that node, then the EndpointWatcher is supposed to fire a Terminated event for that actor.

ProtoActor dont support remote supervision, when you spawn remote actors they are root actors on the remote system.

----------------------------------------------------------------
----------------------------------------------------------------
END of revised notes. Below is most likely inaccurate or wrong
----------------------------------------------------------------
----------------------------------------------------------------

## Cluster
'Cluster' is a Remote System Server Node with per Actor-Prop-Kind partition that joins the 3rd party cluster provider.
Clustering where the name caching / ownership lives

Cluster.get does a remote activation if needed. Duplication possible.

in clustering a remote node would be a "Member" as its a member of the cluster... while in remoting its just a , what? endpoint?

all nodes in the cluster will register as the same service in consul, where the cluster name is the service name in consul
and each of those entries have tags for known kinds

Cluster.Start: It stats the cluster module and joins the cluster. but it is dependant on Consul so you need to have a consul agent running also

Shutdown should really be called "Leave" as it informs your app to leave the cluster
or Deregister

## Grain (Virtual Actor)
http://proto.actor/docs/what%20is%20protoactor   Automatic placement strategy for Actors in a Cluster based on hash table
Actors under the hood with a typed protobuf RPC ontop
Automatic Activation and placement. Grain is a specialised form of Actor.
The concept of virtual actors only exists in the cluster lib. it's purpose is mainly so that you don't need to know where or if an actor exists, because the node that hosts it could die at any time. So if it disappears it will automatically be recreated
Virtual Actors/Grains are RPC based
types grains from protobuf definitions via code-gen.
https://files.gitter.im/AsynkronIT/protoactor/kepm/blob
https://files.gitter.im/AsynkronIT/protoactor/YqnA/blob
A grain is a virtual actor, same as MS Orleans.. we can now specify RPC services in the proto files, and generate typed C# code and the Proto.Actor cluster will connect everything for us. so node1 can ask for a grain of a specific type, and it will be found or created on another node
If you are using cluster, you can generate "Grains" using the protoc grain generator.. those are typed actors with an RPC like interface (much like Microsoft Orleans)
when you use that, it will handle failover for you
Grains in ProtoActor works like this: you generate grains from a Protobuf Service dfinition.. this gives you two things, a client that can talk to the grain , and a server interface which you need to implement

## Deeper topics



**Actor Spawning**: creation of a LocalContext and a LocalProcess (with a new Mailbox) that is registered in the ProcessRegistry to get a unique PID. A first SystemMessage (Started) is sent to the Actor.

**Message:** http://proto.actor/docs/messages

**SystemMessage:** a predefined message by the Protoactor framework such as Stop. Use the "SendSystemMessage" API
**UserMessage:** custom messages defined for the app. Use the "Tell" or "SendUserMessage" API.
**MessageEnvelope:** holds references to the message, the sender and the message header
**MessageHeader:** String-based Key-Value store with meta data for the message. Confusion trap due to bad naming imho: Header vs Headers; it's a single header dictionary but setting a value means "setting the header"
**DeadLetter:** http://proto.actor/docs/durability
**Dispatcher:** ?
**EventBus** is the central message bus used by the cluster or system (?). Access via Instance Singleton. subscribe to member status events and the like. e.g. EventStream.Instance.Subscribe(...)

**EventStream**
The EventStream is used internally in Proto.Actor to broadcast framework events.
http://proto.actor/docs/golang/eventstream#usages
eventstream is somewhat of a infrastructure utility, you can absolutely use it, we use it internally in ProtoActor, e.g. Remote and Cluster makes use of it to distribute events in a pub sub way.
But do note that it is only in process. it is not a full fledged cluster pub sub

**ActorClient / RootContext:** ?
**ContextState:** None, Alive, Restarting, Stopping

**Router:** message forwarder or multiplier
I'm using round robin, consistent hash and broadcast routers in my chat server

**Middleware:** message handler or transformator that plugs into the path of a message to it's end destination.

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

http://getakka.net/docs/concepts/actors

http://proto.actor/docs/actors  :  An actor is a container for State, Behavior, a Mailbox, Children and a Supervisor Strategy.

**system actors**
Partition Actor  (partion for host). Partion owns actor names and knows the location of the actor?
Activation Actor

**endpointwriter**
endpoint writer do batch messages that are intended for the same host

Google's **Protobuffers** is used for encoding and decoding messages. You describe the structure of a message ahead of time and use the compiler to generate the code you need to serialise and derserialise the efficiently encoded message fast.

**gRPC** is a Remote Procedure Call framework using Protobuffers that provides a plugin for the Protobuffer compiler to generate the server and client code for Protoactor to create "Grain" Actors.

## **general things to know**
Proto.Actor is non bocking via  Async on C# and Coroutines in Kotlin
rotoactor is a library for implementing actor-based systems. it uses gRPC as a transport to handle communication between actors over the network. if you need actors talking over the network, protoactor (with remote) is a possible solution.
one thing to consider: while protoactor uses queues internally to route messages to actors, those queues are just in-memory (meaning if the process dies your messages are gone). 

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

http://proto.actor/docs/golang/eventstream#usages
http://proto.actor/docs/golang/eventstream

ctx.Spawn(routing.FromGroupRouter(routing.NewBroadcastGroup(pids...)))


Actor self-destructoin: context.Self().Stop() also works
Actor destruction: pid.Stop()

-------
# Alias in your head
Cluster.Join
Cluster.Leave

PID / ProcessRegistry  / Vs Actor:  processregistry has a variable called localActor referencing processes.

# Mechanics
Spawning : Local, Remote, Cluster, Grain

