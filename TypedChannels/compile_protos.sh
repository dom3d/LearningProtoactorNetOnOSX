#!/bin/bash
echo "compiling protobufs ..."
protoc ./messages/*.proto -I=./messages/ --csharp_out=./messages/ --csharp_opt=file_extension=.g.cs --grpc_out ./messages/ --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin
echo "codgen for grains ..."
dotnet ../protoactor_tools/protograin.dll ./messages/Messages.proto ./messages/Messages.grains.cs
echo "completed."
