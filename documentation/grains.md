
Grains, the typsafe request-response messaging approach in ProtoActor is using code-gen via a gRPC extension ( grpc_csharp_plugin ) as well as an additional custom code-generator called ProtoGrainGenerator.dll.

You can declare an Grain's (Virtual Actor)'s RPC interface via gRPC services like this:
```protobuf
// (...)
service HelloGrain
{
	// SayHello method accepts a HelloRequest message and returns a HelloReply message
	rpc SayHello (HelloRequest) returns (HelloReply) {}
}
```
Make sure to include another generated .cs file which may at root level instead of being inside your messaging folder. File name is in the style of: **xyzGrpc.cs**

You also need a code-gen tool doesn't come via NuGet.

```
Clone the ProtoActor-dotnet respository and run build.sh
```

Then copy the content of

```
protobuf/ProtoGrainGenerator/bin/Release/netcoreapp2.0/
```

to some location of your choice, e.g. into your project. Unfortunately the tools doesn't accept wildcard and will only compile one proto file. You can do that for example like this:

```
dotnet ../protoactor_tools/protograin.dll ./messages/Messages.proto ./messages/Messages.grains.cs
```

Dont forget to add the generated file to your project. Here is a calling example usage in C# via ProtoActor Grains:
```cs
	// All RPC classes are in the Grains namespace automatically
	var client = Grains.HelloGrain("GrainName");
	// calling a method of a ProtoActor Grain
	var res = client.SayHello(new HelloRequest()).Result;
```

Grain implementation example below. The interface implementation comes from the custom code generator and lives in the same namespace given by the proto file.
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

When using RPCs you may want to have "void" input parameters. Protobuf doesn't not allow that and you have to use an empty message instead

```protobuf
message Empty { }
```


## Grain (Virtual Actor)
Grains are a convienience mecahnic that does a lot of the actor management under the hood automatically for you. It also strictly uses typed RPC for communicationa and it requires to use ProtoActor Cluster.

- automatic creation in ProtoActor nodes that called the Grain's factory
- automatic activation
- typed RPCs

Grains are created via a custom Protobuf extension provided by ProtoActor. This extenion does all the "glueing" via code generation. Grains allow to define a "typed" request & response experience via Probotuf RPCs.

Virtual actor means, you don't know exactly where in the cluster the ActorGrain is and if it's was actually spawned or not. When you request a reference (PID) to an actor, it may get spawned as consequence in case there was no instance alive. The cluster is partitioned by Prop type / aka kind.

Grains register themselves as Kind in the Remote layer when the corresponding factory is called.

The RPC layer send the method name as string, increase the message size and amount of generated garbage.

All grains are accessible via the Gain namespace.


http://proto.actor/docs/what%20is%20protoactor
https://files.gitter.im/AsynkronIT/protoactor/kepm/blob
https://files.gitter.im/AsynkronIT/protoactor/YqnA/blob
A grain is a virtual actor, same as MS Orleans.. One can  specify RPC services in the proto files, and generate typed C# code and the Proto.Actor cluster will connect everything automatically. so node1 can ask for a grain of a specific type, and it will be found or created on another node
If you are using cluster, you can generate "Grains" using the protoc grain generator.. those are typed actors with an RPC like interface (much like Microsoft Orleans)
when you use that, it will handle failover for you
Grains in ProtoActor works like this: you generate grains from a Protobuf Service dfinition.. this gives you two things, a client that can talk to the grain , and a server interface which you need to implement

To tell in which cluster node a specific grain can be spawned, you need to call the factory in code of that node:

```cs
Grains.MyGrainFactory(() => new MyGrain());
```
I think it would be better to name this:  Grains.Register.MyGrain(...factory ...)



## Working with Grains
Grain code is generated via the custom ProtoActor code generator. For detail on that, see futher up in the protobuf section.

Once that generatoin been done and the files have been included in your project, you can register your grains in the cluster nodes that you want to be able to spawn them.
```cs
Grains.MyGrainNameFactory(() => new MyGrainName())
```
Grains are actually classes that get wrapped by the code generated actor. The factory function allows you to customise how your grain is created when the corresponding, wrapping actor is started.

Anywhere in the cluster you can spawn or get a reference to the grain (client) like hits
```cs
var client = Grains.MyGrainName("NameOfGrainInstance");
```
Now you can call methods on it directly by triggering correspoding async requests.

Please note thay depsite grains using the request/reply pattern, that grains are not given any context object and therefore the sender PID is unknown within the grain. Workaround is to send it explicitly andthis of course increases the message size overall.

[back](../README.md)