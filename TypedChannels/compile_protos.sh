#!/bin/bash
echo "compiling protobufs ..."
# high light errors in red
echo -e "\033[0;31m"
protoc ./messages/*.proto -I=./messages/ --csharp_out=./messages/ --csharp_opt=file_extension=.pbuf.cs --grpc_out ./messages/ --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin
echo -e "\033[0m"  
echo "codgen for grains ..."
echo -e "\033[0;31m"
dotnet ../protoactor_tools/protograin.dll ./messages/AllInOne.proto ./messages/AllInOne.grain_actors.cs
echo -e "\033[0m"  
echo "completed."
