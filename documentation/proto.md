
# Quick intro to working with .proto files
ProtoActor uses Google's Protobuffer which is a serialisation solution that uses code-generation. One has to define the messages in a .proto file and then compile them with a standalone tool.

Below a simple example for a message. ( filename: messages.proto )
```protobuf
// format / version header
syntax = "proto3";
// namespace comes from the package name unless cssharp_namespace is used
package messages;
// it's a bit redundant here but more complex namescpaes possible if desired
option csharp_namespace = "Messages";

message MessageWithoutData {}
message MessageWithSingleStringField
{
	// the =1 part is the field position
	string StringData=1;
}
```

Make sure that you include into your project the generated **xyz.g.cs** file.

Then in C#
```CS

// "Messages." part comes from the declared namespace in protobuf
// "MessagesReflection" part is based on protobuf file-name (here messages.proto)
using ExampleProtocol = Messages.MessagesReflection;

// (...)
	// Register protobuf messages in ProtoActor
	Serialization.RegisterFileDescriptor(ExampleProtocol.Descriptor);

```
If you want to reference other Protobuf message types from within a Protobuf file, use imports. Example:
```protobuf
// header
syntax = "proto3";
package messages;
import "shared/shared.proto";
option csharp_namespace = "Messages";
```

If you want to reference ProtoActor data types (e.g. PID) or message types, download the 3rd party folders and put them into a subfolder. Example:
```protobuf
syntax = "proto3";
package messages;
option csharp_namespace = "Messages";
// import
import "lib/Proto.Actor/protos.proto";
// using the imported type via it's namespace
message JoinChannel { actor.PID sender = 1; }
```

Documentation for Proto3 is here: https://developers.google.com/protocol-buffers/docs/proto3
Well known datatypes: https://github.com/google/protobuf/blob/master/src/google/protobuf/unittest_well_known_types.proto

[back](../README.md)