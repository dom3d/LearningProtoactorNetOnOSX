# Learning Protoactor-Net On OSX
Set of example projects for me to explore Protoactor-Net on OSX using Visual Studio for Mac and DotNetCore. Documenting as I go as the documentation is quit thin.

# Preparation work to get started with Protoactor-Net on OSX
Source code doesn’t build out of the box on OSX despite using the Cake Build system as of Jun 2017. Using Visual Studio for Mac (MacVS) and prebuilt libs instead. (Note that I am not using docker/containers or any VM here)

## In the OSX / iterm2 terminal window: 

### Installing the Protobuffer tools we need
— brew install protobuf
— brew install --with-plugins grpc
— which grpc_csharp_plugin

### Installing Consul
- brew install consul

## In MacVS:
— create dotnet core console app
— project > add nuget packages  ? switch from all sources to configure sources > add and use "https://www.myget.org/F/protoactor/“ as location

# How to run things on OSX
Compiling protobuffer with gRPC: go into the directory with your message definitions that end with .proto. Have a script that does the following or do it manually:

	protoc *.proto -I=./ --csharp_out=. --csharp_opt=file_extension=.g.cs --grpc_out . --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin

DotNetCore console app is not an exe like hen using Mono but a DLL. Run it like this

	dotnet MyAppName.dll

Consul.io is used for service discovery etc. Get it up and running locally from the terminal like this:

	consul agent -dev -advertise 127.0.0.1