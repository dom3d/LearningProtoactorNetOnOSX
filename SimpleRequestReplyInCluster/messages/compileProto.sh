#!/bin/bash
protoc *.proto -I=./ --csharp_out=. --csharp_opt=file_extension=.g.cs --grpc_out . --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin

