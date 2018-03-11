

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
- http://getakka.net/docs/concepts/actors
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

PID has an API to send messages but those messages hit the mailbox directly and ignore any "Props" configuration of the actor and therefore middlewares are ignored. PID is somewhat "polluted" with some conviences methods such as: Tell, Request, SendSystemMessage, Stop. Eventually it uses the ProcessRegistry under the hood to get a reference to the Process instance itself.

An actor instance name, the second part of the PID, has to be unique across the system and across a manual remoting setup or a cluster.

## ProcessRegistry
Provides IDs for new Process/Actor instances. If a name is not given for a new actor, then the ProcessRegistry will create a name based on "$" + internal counter.

### Mailbox
The Mailbox is the thing that stores messages that a Process received. It actually also covers the triggering of reactive processing as .Net Tasks across Threads. Mailbox is replacable with a custom implementation if needed via the Props mechanic.

### Context
Context is passed to an Actor's receive method and contains APIs around

- the next received message to process
- it's Props configuration (middleware, routing etc)
- hierarchy (children, parent)
- ability to watch other Actors

While processing a message inside an actor, you can access the following via the Context object:

- the actor's PID via context.Self
- the actor's parent PID via context.Parent
- ability to send messages. Doing this via the context makes message go through the configure middleware
- sender's PID for request based messages: context.Sender. WARNING can be NULL !!! Only set when a reply expected - in other words only set for Request-Reply based actor messaing. In your head think of: RequestSenderPID
- methods to spawn actors as children (the spawn method names of the context are not explicitly stating this unfortnuately)
- stash pending messages e.g. for recovery purposes
etc.

### Actor / Context Lifecycle states

A context and therefore an actor has the following states

	* None,
	* Alive,
	* Restarting,
	* Stopping,
	* Stopped

In your Actor receive routine you can react to those lifecycle "events" as they are send as message.

```cs
public Task ReceiveAsync(IContext context)
{
	var msg = context.Message;

	switch (context.Message)
	{
		case Started r:
			Console.WriteLine("Started, initialize actor here");
			break;
		case Stopping r:
			Console.WriteLine("Stopping, actor is about shut down");
			break;
		case Stopped r:
			Console.WriteLine("Stopped, actor and it's children are stopped");
			break;
		case Restarting r:
			Console.WriteLine("Restarting, actor is about restart");
			break;
	}
	return Actor.Done;
}
```

## Messaging
Any .Net object can be an message. A message is simply an Object. So if you only ever send messages within a process and no serialisation and deserialisation is need, then simply define classes use those as messages.

Otherwise Google's Protobuffers is used by default for serialisation and deserialisation of ProtoActor Messages. One defines messages via proto files which you need to compile via the command line. This will create C# code that has to be added to your solution.

For Grains gRPC aka Services are used which is a Remote Procedure Call framework using Protobuffers.

Details are on a separate page:

[Protobuf for ProtoActor details](proto.md)

One thing to keep in mind: while protoactor uses queues internally to route messages to actors, those queues are just in-memory (meaning if the process dies your messages are gone).

Checkout the [official documentation for messages](http://proto.actor/docs/messages)

---

### The many (and thus confusing) ways to send a message

**pid.Tell**
```
this posts the message to the maibox directly and ignores any middleware.
```

**Remote.SendMessage**
```
wraps the message into an evelope end sends it out right away via RemoteDelivery of the EndpointManager
```

**context.tell**
```
This sends a message to the target PID and respects the middleware setup as defined via Props/Kind.
```

**context.RequestAsync**
```
Request-Response based message. The message gets packed into a MessagEnvelope. This is the request part.
```

**context.Respond**
```
Request-Response based message. This is the response part. context.Sender is actually not null in this case and can be used. This is only for received Request messages (RPCs).
```
---
### Actor self destruction
```cs
context.Self().Stop()
```
or if you have the PID already:
```cs
pid.Stop()
```
---
### ProtoActor's message types

**UserMessage:** custom messages defined by you for your app. Use the "Tell" or "SendUserMessage" API.

**SystemMessage:**
ProtoActor uses actors itself to manage all kind of aspects. So a SystemMessage is a predefined message by the Protoactor framework such as 'Stop'. Use the "SendSystemMessage" API.

### ProtoActor's builtin messages

* Watch
* Unwatch
* Terminated
* PoisonPill
* Stop

### Message Envelope / Header
User messages are wrapped into an envelop. When using Request & Reply messages (instead of of the faster fire & forget ) then extra meta data is attached such as the sender's PID

**MessageEnvelope:**
Is the wrapper around a user message with access to the header and the message itself

```cs
messageEnvelope.Message
```

**MessageHeader:**
Header is part of the Envelope but only available (non empty) for user messages.

It's string-based Key-Value store with meta data for the message. Some confusion risk due how names are used: Header vs Headers; it's a single header dictionary but setting a value means "setting the header"

```cs
messageEnvelope.Header
```
---

## Props & Kinds
Props is the configurator of an Actor - it holds the recipe of how to configure a specific Actor when the Actor gets spawned.

The 'Props' is taken from movies and theather where an object, that is used (on stage or on screen) by actors during a performance or screen production, is called a prop.

Sometimes Props is also referred to as "kind" in ProtoActor. You register a Kind by handing over a Prop and give it a name.

Props define what creates the actor, what kind of messaging middleware is to use, what supervision strategy to put in place, and even what kind of mailbox to use.

Alternative names you can keep in your mind for Props that could be suiteable are: Configurator, Template, Receipe, Setup, Builder, Schema, Blueprint

### Props: Middleware
message handler or transformator that plugs into the path of a message to it's end destination.

Middleware example: adding debug infos into the messaging flow of an actor:
https://github.com/bnayae/ProtoActorPlayground/blob/master/src/HeloWorld/HelloProto/SupervisorActor.cs

### Props: Routing
message forwarder or multiplier
Somebody said: "I'm using round robin, consistent hash and broadcast routers in my chat server"

Under the hood routers are actors and non special memory optimisation is done. For example broadcasting a message will duplicate the message per target

---

## Supervision and Hierarchies

**Supervision:** Ability of an actor to spawn or despawn other actors and thus restart them on failure or for other scnearios. Directives are: Resume, Restart, Stop, Escalate
If I restart an actor with the supervision strategy is the PID the same? Yes, same PID, messages should still be in the mailbox and start coming as soon as actor is restarted
the message that caused the failure will be lost however unless you explicitly stash it
 proper way to kill a child actor from a parent? PID.Stop();
 Parent is only available when you spawn via the Context and the primary reason to do that is for supervision. If the child fails, that is escalated to the parent. If you don't plan to use supervision then you can also just embed Self in a message

**Supervision Strategy:** a pattern or recipe for how to handle failure in children. For example restart all children when one child fails. etc.
**Watcher / Watching:** if an Actors dies that is watched, the Watcher received a notification about the Termination.
**Parent:** Parents are implicitly watching their children (parent Actors that is).

### The many (and thus confusing) ways to spawn an actor
**Actor.Spawn** (and it's variations)
```
spawns it as root Actor
```

**context.Spawn** (and it's variations)
```
spawns it as child of the actor (actor.sef)
```

---

## Schedulers
Actors you can use to send messages repeatedly based on given interval.

---

## Remote
Remote is the layer that allows to connect ProtoActor instances with each other. Peering ProtoActor instanecs over network basically. Each instance becomes a node with an Endpoint; a server with an adresss.

You can

- "activate" a Remote Process/Actor/Props (Remote.SpawnNamedAsync)
- send Messages to a Remote Process/Actor (Remote.SendMessage)
- watch Remote Processes/Actor (e.g. to be informed about it's termination)

In order to be able to spawn Actors from remote nodes, the remote ProtoActor instance has to register Actor Props ('Actor Setups') via Remote.RegisterKnownKinds first. So only pre-registered Props can be spawned remotely. Be aware that you can register different kinds using the same name - but that's a problem because the name has to be unique across the cluster and across all kinds registered.

Remote Start will start the gRPC server on the given IP and Port. Maybe "Peering" or something like that would be a better name.

Ability to watch a remote Actor: When a node is disconnected, and if someone is watching an actor on that node, then the EndpointWatcher is supposed to fire a Terminated event for that actor.

ProtoActor doesn't support remote supervision, when you spawn remote actors they are root actors on the remote system.

## Activations / Activator
"Named" Activations - is spawning an Actor via it's kind using unique name across kinds and acts as global ID of the register kind. It's possible to register multiple kinds with the same name without errors, unfortunately.

Spawning is done by a special system actor called Activator. It's one of those hidden things that appear here and there in the API but isn't very explicit.

# Events
## EventStream
EventStream is the central event bus of ProtoActor where system events are published to. One is able to subscrie to events to get notifications.

Eventstream is somewhat of a infrastructure utility, and one can absolutely use it. ProtoActor uses it internally e.g. Remote and Cluster makes use of it to distribute events in a pub sub way.
But do note that it is only in process. it is not a full fledged cluster pub sub

Eventstream is pretty much just a pubsub direct invocation thing
anything passed to the eventstream will directly find its subscribers and synchronously call the subscribers
those subscribers can ofc be lambdas that do Tell to some actor to make it async

Reading material

* http://proto.actor/docs/golang/eventstream#usages
* http://proto.actor/docs/golang/eventstream

It's for example used to DeadLetters

**DeadLetter:** Event saying an actor doesn't exist any more. More details here: http://proto.actor/docs/durability
A actor/process that doesn't exist anymore but still receives messages is represented by a DeadLetterProcess

Here is an example how the system publishes the DeadLetter event

```cs
EventStream.Instance.Publish(new DeadLetterEvent(pid, message, null));
```

# Builtin Actors


# TBD

http://proto.actor/docs/actors  :  An actor is a container for State, Behavior, a Mailbox, Children and a Supervisor Strategy.

**Dispatcher:** ?

**system actors**
Partition Actor  (partion for host). Partion owns actor names and knows the location of the actor?
Activation Actor

**endpointwriter**
endpoint writer do batch messages that are intended for the same host


[back](../README.md)

