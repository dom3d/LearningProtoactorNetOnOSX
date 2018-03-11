#!/bin/bash
protoc ./messages/*.proto -I=./messages/ --csharp_out=./messages/compiled/ --csharp_opt=file_extension=.g.cs --grpc_out . --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin

