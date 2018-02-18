# Notes about what I would change from my personal opinion with my current & limited understanding of ProtoActorNet


## Kinds Registry
I would move the concept of Kinds out of "Remote" and make it it's own, standalone thing. I also would rename it for simplicty to PropsRegistry - althought "Props" is not a great name.

PropRegistry would allow to register Props by name and can be used independantly of Remote and would be used by Remote.

## Remove convience methods from PID
I would remove Tell, SendMessage etc from PID and move that all into the Actor name space. One dirty side-effect I am seeing is with the PID parameter passed around with SendMessage although it barely ever used. And it feels like it's relating.

So I would have

```cs
Actor.SendTo(PID targetPID, object message) // tell
Actor.Stop(PID targetPID)
Actor.SendSystemMessageTo(...)
Actor.RequestBlocking ... 
Actor.Request ... etc
```

problem is the middleware :\

similar change to Context. I would move thing out of Context into Actor
```cs
Actor.Spawn
Actor.SpawnPrefixed
Actor.SpwanNamed
Actor.Watch
Actor.Unwatch
```

## Have context.sender always the sender PID


## Renaming things
I would leaver the Akk world behind. 
Props -> Template or Setup
Remote -> Peering
Kind ->  RegisteredTemplate in TemplateRegistry
            RegisteredSetup in SetupRegistry


## context
sender -> RequestSender // as this is null when used with Tell / standard messaging