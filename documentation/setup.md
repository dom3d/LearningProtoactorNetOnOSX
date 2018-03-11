# Preparation work to get started developing with Protoactor-Net on OSX
You can build from ProtocActor from source or add it as package to your solution in e.g. Visual Studio (for Mac). If you want to use Grains to will need to compile as tool directly from the source code as it's not available for download otherwise.

## In the OSX iterm2 / terminal window:
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

## Want to work with Grains?:
Get source code
```sh
git clone https://github.com/AsynkronIT/protoactor-dotnet.git
```

Build the source
```sh
build.sh
```

Grab the tool (all the content) under
```
/protobuf/ProtoGrainGenerator/bin/Release/netcoreapp2.0/
```
and put into a subfolder of your project.

# How to run things on OSX
## compiling protobuf files as actor messages:
Compiling protobuffer files: go into the directory with your message definitions that end with .proto. Have a script that does the following or do it manually (if the plugin is installed in a different location on your system, adjust paths as needed):

	protoc *.proto -I=./ --csharp_out=. --csharp_opt=file_extension=.g.cs --grpc_out . --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin

Be aware that you may want to references other proto files and add different directories for imports. In that situation add them via additional -I {path}. This can be a bit tricky. My recommendation (how I got it working) is to make e.g. 3rd protos that you want to import reside in a subfolder of your own protos.

```
- folder with your protos
--  subfolder with the protos you want to import
```

Example setup where library protos are in a subfolder. Also having the shell script at root level and the protos in a 'messages' subfolder as convenience setup:
```
#!/bin/bash
protoc ./messages/*.proto -I=./messages/ --csharp_out=./messages/ --csharp_opt=file_extension=.g.cs --grpc_out ./messages/ --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin
```

Import would then look like this
```
import "lib/Proto.Actor/protos.proto";
```

If you want to reference ProtoActor data types, you need to copy the corresponding proto files from the ProtoActor source and put them into your Lib folder. For example take this one:

```
src/Proto.Actor/Protos.proto
```

## Grain compilation
Creating Virtual Actors (aka Grains) requires an additional codegen step using ProtoGrainGenerator.dll
See higher up how to get it by compiling the ProtoActor source code.

## Starting the Console service locally for local Cluster tests
Consul.io is used for the automatic Clustering aspect of ProtoActor. Get it up and running locally from the terminal like this:

	consul agent -dev -advertise 127.0.0.1

## Running your app
DotNetCore console app is not an exe like when using Mono but a DLL. Run it like this

	dotnet MyAppName.dll


