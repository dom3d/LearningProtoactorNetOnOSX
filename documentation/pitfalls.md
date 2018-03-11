

# Using ProtoActor ...

## The many (and thus confusing) ways to send a message

**pid.Tell**
```
this posts the message to the maibox directly and ignores any middleware.
```

**Remote.SendMessage**
```
wraps the message into an evelope end sends it out right away via RemoteDelivery of the EndpointManager
```

**context.tell**
```
This sends a message to the target PID and respects the middleware setup as defined via Props/Kind.
```

**context.RequestAsync**
```
Request-Response based message. The message gets packed into a MessagEnvelope. This is the request part.
```

**context.Respond**
```
Request-Response based message. This is the response part. context.Sender is actually not null in this case and can be used. This is only for received Request messages (RPCs).
```

## The many (and thus confusing) ways to spawn an actor
**Actor.Spawn** (and it's variations)
```
spawns it as root Actor
```

**context.Spawn** (and it's variations)
```
spawns it as child of the actor (actor.sef)
```

## Retrieving a remote root actor via the Cluster
```cs
// example approach. In my tests I often needed to wait a good chunk of time for the first get, so here is one way to handle that
(PID, ResponseStatusCode) taskResult = (null, ResponseStatusCode.Unavailable);
while (taskResult.Item2 == ResponseStatusCode.Unavailable)
{
	taskResult = Cluster.GetAsync("ActorInstanceName", "ActorRegisteredKindName").Result;
}
```

