#!/bin/bash
protoc ./simulator/messages/*.proto -I=./simulator/messages/ --csharp_out=./simulator/messages/ --csharp_opt=file_extension=.g.cs --grpc_out . --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin

