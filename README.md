# Learning Protoactor-Net On OSX
Set of example projects for me to explore Protoactor-Net on OSX using Visual Studio for Mac and DotNetCore. Taking notes as I go as the documentation is quit thin. Sharing my notes and code in case it helps somebody else to pick this up faster. I am starting with the examples in the Protoactor library but changing them a bit to make them easier run or understand for a newbie. Btw. I don't guarantee that everything I write here is accurate.

# Preparation work to get started with Protoactor-Net on OSX
The Protoactor source code doesn’t build out of the box on OSX despite using the Cake Build system as of Jun 2017. I am using Visual Studio for Mac (MacVS) and prebuilt libs instead. (Note that I am not using docker/containers or any VM here in my examples)

## In the OSX / iterm2 terminal window: 

### Installing the Protobuffer tools we need
	brew install protobuf
	brew install --with-plugins grpc

Understand where the plugin is installed as you will need the full path when using the protobuffer compiler.

	which grpc_csharp_plugin

### Installing Consul
	brew install consul

## In MacVS:
	create dotnet core console app
	project > add nuget packages  ? switch from all sources to configure sources > add and use "https://www.myget.org/F/protoactor/“ as location

# How to run things on OSX
Compiling protobuffer with gRPC: go into the directory with your message definitions that end with .proto. Have a script that does the following or do it manually (if the plugin is installed in a different location on your system, adjust as needed):

	protoc *.proto -I=./ --csharp_out=. --csharp_opt=file_extension=.g.cs --grpc_out . --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin

DotNetCore console app is not an exe like hen using Mono but a DLL. Run it like this

	dotnet MyAppName.dll

Consul.io is used for service discovery etc. Get it up and running locally from the terminal like this:

	consul agent -dev -advertise 127.0.0.1


# Terminology
Protoactor uses a set of 3rd party solutions and is also inspired by various other programming frameworks. Depending on your background you may alread know all of this (or not ;).

Simplified, *"Actors"* are a way to decompose your application logic into small units that don't use each other directly but only interact through messages. Primary goal here is stability and the ability to scale easier across cores and machines. "Akka" for Java was inspired by Erlang and it's OTP. The authors ported *Akka* to DotNet but also take inspirations from Microsoft's *Orleans*. Protoactors seems to adapt names from various frameworks, so in doubt, checkout their documentation. For example "Props" seems to come from the Akka world.

Google's *Protobuffers* is used for encoding and decoding messages. You describe the structure of a message ahead of time and use the compiler to generate the code you need to serialise and derserialise the efficiently encoded message fast.

*gRPC* is a Remote Procedure Call framework using Protobuffers that provides a plugin for the Protobuffer compiler to generate the server and client code for Protoactor. Looks like you can also define "services" in your protobuf description files to create "Grain" Actors (?).

Although Protoactor talks about Actor mostly ( http://proto.actor/docs/actors ) under the hood it seems that the concept is realised by a combination of elements (Actor, Context, Process, Props)

Although this is from the go-lang implementation, it's a recommended read: https://github.com/AsynkronIT/protoactor-go/blob/dev/actor/doc.go

## To be done (might be wrong here)

*Actor:* Unit that processes messages. A message is not passed directly but via a context object. If the actor is a stateless function, the same instance could be used to process messages from different "protocactor-process" mailboxes. Otherwise a new instance has to be created for each "protoactor-process".

*Mailbox:* is the thing that receives and holds messages.

*Process / LocalProcess* (strange name): is an interaction wrapper around the mailbox for posting messages to the mailbox. Any interaction with other Actors is via messages

*Context / LocalContext:* Context contains contextual information for an actor and provides an API to interact with that data (such as actor children). Feels like the real core of an Actor.

*Props* (strange name btw): "Recipe" for the creation of an Actor. (Actor)Producer: creates a new Actor. MailboxProducer creates a new mailbox. Message routing middleware configuration, Supervision Strategy.

*Actor Spawning*: creation of a LocalContext and a LocalProcess (with a new Mailbox) that is registered in the ProcessRegistry to get a unique PID. A first SystemMessage (Started) is sent to the Actor.


*PID*: string ID given by the ProcessRegistry. ID to identify an process but also holds a reference to the process. Spawning creates the ID. Each time an actor is spawned, a new mailbox is created and associated with the PID. Messages are sent to the mailbox.

*ProcessRegistry*: 

*Grain (Virtual Actor)*: http://proto.actor/docs/what%20is%20protoactor   Automatic placement strategy for Actors in a Cluster based on hash table


*Message:* http://proto.actor/docs/messages

*SystemMessage:* a predefined message by the Protoactor framework such as Stop. Use the "SendSystemMessage" API
*UserMessage:* custom messages defined for the app. Use the "Tell" or "SendUserMessage" API.
*MessageEnvelope:* holds references to the message, the sender and the message header
*MessageHeader:* String-based Key-Value store with meta data for the message. Confusion trap due to bad naming imho: Header vs Headers; it's a single header dictionary but setting a value means "setting the header"
*DeadLetter:* http://proto.actor/docs/durability
*Dispatcher:* ?


*IContext:* self PID, parent PID, reference Actor, children PIDs, reference to sender of last message, stash for message to allow restart, spawing of children
*ISenderContext:* Message meta data (Headers) + reference to Message it self.
*ActorClient / RootContext:* ?
*ContextState:* None, Alive, Restarting, Stopping

*Router:* message forwarder or multiplier
*Middleware:* message handler or transformator that plugs into the path of a message to it's end destination.

*Supervision:* Ability of an actor to spawn or despawn other actors and thus restart them on failure or for other scnearios. Directives are: Resume, Restart, Stop, Escalate
*Supervision Strategy:* a pattern or recipe for how to handle failure in children. For example restart all children when one child fails. etc.
*Watcher / Watching:* if an Actors dies that is watched, the Watcher received a notification about the Termination.
*Parent:* Parents are implicitly watching their children (parent Actors that is).

*Cluster Kind, Prop Kind* ?
*Cluster Partition* ?

http://getakka.net/docs/concepts/actors

http://proto.actor/docs/actors  :  An actor is a container for State, Behavior, a Mailbox, Children and a Supervisor Strategy.
