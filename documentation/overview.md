
# What is ProtoActor-Net ?
ProtoActor is a distributed Actor framework with focus on simplicity, performance, re-use of existing solutions and language interoperatability. ProtoActor merges aspects from Akka, Orleans and Erlang but terminology is heavily Akka driven.

Actor oriented architecture is all about interacting between program units via messages. Actor solutions prioritise availability and scalability.

# Understanding the API Layers
ProtoActor is build in stages. More recent layers sit somewhat ontop of the previous one but with blurry intersections.

## Actor
'Actor' is the basic and core layer allowing one to create a system of actors that message each other within a single CPU/OS process. It

## Remote
The 'Remote' layer allows you to manually connect ProtoActor Instances over the network. It provides the means to
- messagage across 'remote' instances,
- spawn actors on remote instances,
- observe actors on remote instances
- etc

## Cluster
'Cluster' automates the aspect of building and managing a grid of connected ProtoActor instances. (Intance is sometimes referred to as Node or Server). It relies on 3rd party solutions such as Consul. An instances joins and leaves a Cluster via the Cluster API.

## Grains (Virtual Actors)
Using Grains in ProtoActor is not really just another feature layer. It's overall a different approach where you don't manage actors directly.

This is automatic actor creation, cluster placement and spawning. This is mostly achieved by defining the RPC interfaces via protobuf definition files and using a custom code generator.

The code generator creates the actor automatically including message encoding and decoding and calling the grain methods. Key difference is that actors use untyped message passing and grains are using only Remote Procedure Calls (RPCs)

# Terminology
The main author of ProtoActor ported **Akka** to DotNet. "Akka" for Java/JVM was inspired by Erlang and it's OTP. This time ProtoActor also took inspirations from Microsoft's **Orleans** and the simplicity experienced when working with go-lang.

Protoactors uses terms from various frameworks but first of all from Akka / Akka.Net. Example: "Props". Grains comes from the Orleans world and Process and Mailbox from the Erlang world.

[back](../README.md)

