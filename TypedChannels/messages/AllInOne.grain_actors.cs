
using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Proto;
using Proto.Cluster;
using Proto.Remote;

namespace Chat
{
    public static class Grains
    {
        internal static Func<IChannelGrain> _ChannelGrainFactory;

        public static void ChannelGrainFactory(Func<IChannelGrain> factory) 
        {
            _ChannelGrainFactory = factory;
            Remote.RegisterKnownKind("ChannelGrain", Actor.FromProducer(() => new ChannelGrainActor()));
        } 

        public static ChannelGrainClient ChannelGrain(string id) => new ChannelGrainClient(id);
        internal static Func<IUserGrain> _UserGrainFactory;

        public static void UserGrainFactory(Func<IUserGrain> factory) 
        {
            _UserGrainFactory = factory;
            Remote.RegisterKnownKind("UserGrain", Actor.FromProducer(() => new UserGrainActor()));
        } 

        public static UserGrainClient UserGrain(string id) => new UserGrainClient(id);
    }

    public interface IChannelGrain
    {
        Task<OpSuccess> Add(TargetUser request);
        Task<OpSuccess> Remove(TargetUser request);
        Task<MemberList> GetMembers(Empty request);
        Task<OpSuccess> BroadcastChatMsg(ChannelMsg request);
    }

    public class ChannelGrainClient
    {
        private readonly string _id;

        public ChannelGrainClient(string id)
        {
            _id = id;
        }

        public Task<OpSuccess> Add(TargetUser request) => Add(request, CancellationToken.None);

        public async Task<OpSuccess> Add(TargetUser request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 0,
                MessageData = request.ToByteString()
            };

            async Task<OpSuccess> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "ChannelGrain", ct);

                if (statusCode != ResponseStatusCode.OK)
                {
                    throw new Exception($"Get PID failed with StatusCode: {statusCode}");  
                }

                //request the RPC method to be invoked
                var res = await pid.RequestAsync<object>(gr, ct);

                //did we get a response?
                if (res is GrainResponse grainResponse)
                {
                    return OpSuccess.Parser.ParseFrom(grainResponse.MessageData);
                }

                //did we get an error response?
                if (res is GrainErrorResponse grainErrorResponse)
                {
                    throw new Exception(grainErrorResponse.Err);
                }
                throw new NotSupportedException();
            }

            for(int i= 0;i < options.RetryCount; i++)
            {
                try
                {
                    return await Inner();
                }
                catch(Exception x)
                {
                    if (options.RetryAction != null)
                    {
                        await options.RetryAction(i);
                    }
                }
            }
            return await Inner();
        }
        public Task<OpSuccess> Remove(TargetUser request) => Remove(request, CancellationToken.None);

        public async Task<OpSuccess> Remove(TargetUser request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 1,
                MessageData = request.ToByteString()
            };

            async Task<OpSuccess> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "ChannelGrain", ct);

                if (statusCode != ResponseStatusCode.OK)
                {
                    throw new Exception($"Get PID failed with StatusCode: {statusCode}");  
                }

                //request the RPC method to be invoked
                var res = await pid.RequestAsync<object>(gr, ct);

                //did we get a response?
                if (res is GrainResponse grainResponse)
                {
                    return OpSuccess.Parser.ParseFrom(grainResponse.MessageData);
                }

                //did we get an error response?
                if (res is GrainErrorResponse grainErrorResponse)
                {
                    throw new Exception(grainErrorResponse.Err);
                }
                throw new NotSupportedException();
            }

            for(int i= 0;i < options.RetryCount; i++)
            {
                try
                {
                    return await Inner();
                }
                catch(Exception x)
                {
                    if (options.RetryAction != null)
                    {
                        await options.RetryAction(i);
                    }
                }
            }
            return await Inner();
        }
        public Task<MemberList> GetMembers(Empty request) => GetMembers(request, CancellationToken.None);

        public async Task<MemberList> GetMembers(Empty request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 2,
                MessageData = request.ToByteString()
            };

            async Task<MemberList> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "ChannelGrain", ct);

                if (statusCode != ResponseStatusCode.OK)
                {
                    throw new Exception($"Get PID failed with StatusCode: {statusCode}");  
                }

                //request the RPC method to be invoked
                var res = await pid.RequestAsync<object>(gr, ct);

                //did we get a response?
                if (res is GrainResponse grainResponse)
                {
                    return MemberList.Parser.ParseFrom(grainResponse.MessageData);
                }

                //did we get an error response?
                if (res is GrainErrorResponse grainErrorResponse)
                {
                    throw new Exception(grainErrorResponse.Err);
                }
                throw new NotSupportedException();
            }

            for(int i= 0;i < options.RetryCount; i++)
            {
                try
                {
                    return await Inner();
                }
                catch(Exception x)
                {
                    if (options.RetryAction != null)
                    {
                        await options.RetryAction(i);
                    }
                }
            }
            return await Inner();
        }
        public Task<OpSuccess> BroadcastChatMsg(ChannelMsg request) => BroadcastChatMsg(request, CancellationToken.None);

        public async Task<OpSuccess> BroadcastChatMsg(ChannelMsg request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 3,
                MessageData = request.ToByteString()
            };

            async Task<OpSuccess> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "ChannelGrain", ct);

                if (statusCode != ResponseStatusCode.OK)
                {
                    throw new Exception($"Get PID failed with StatusCode: {statusCode}");  
                }

                //request the RPC method to be invoked
                var res = await pid.RequestAsync<object>(gr, ct);

                //did we get a response?
                if (res is GrainResponse grainResponse)
                {
                    return OpSuccess.Parser.ParseFrom(grainResponse.MessageData);
                }

                //did we get an error response?
                if (res is GrainErrorResponse grainErrorResponse)
                {
                    throw new Exception(grainErrorResponse.Err);
                }
                throw new NotSupportedException();
            }

            for(int i= 0;i < options.RetryCount; i++)
            {
                try
                {
                    return await Inner();
                }
                catch(Exception x)
                {
                    if (options.RetryAction != null)
                    {
                        await options.RetryAction(i);
                    }
                }
            }
            return await Inner();
        }
    }

    public class ChannelGrainActor : IActor
    {
        private IChannelGrain _inner;

        public async Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Started _:
                {
                    _inner = Grains._ChannelGrainFactory();
                    context.SetReceiveTimeout(TimeSpan.FromSeconds(30));
                    break;
                }
                case ReceiveTimeout _:
                {
                    context.Self.Stop();
                    break;
                }
                case GrainRequest request:
                {
                    switch (request.MethodIndex)
                    {
                        case 0:
                        {
                            var r = TargetUser.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.Add(r);
                                var grainResponse = new GrainResponse
                                {
                                    MessageData = res.ToByteString(),
                                };
                                context.Respond(grainResponse);
                            }
                            catch (Exception x)
                            {
                                var grainErrorResponse = new GrainErrorResponse
                                {
                                    Err = x.ToString()
                                };
                                context.Respond(grainErrorResponse);
                            }

                            break;
                        }
                        case 1:
                        {
                            var r = TargetUser.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.Remove(r);
                                var grainResponse = new GrainResponse
                                {
                                    MessageData = res.ToByteString(),
                                };
                                context.Respond(grainResponse);
                            }
                            catch (Exception x)
                            {
                                var grainErrorResponse = new GrainErrorResponse
                                {
                                    Err = x.ToString()
                                };
                                context.Respond(grainErrorResponse);
                            }

                            break;
                        }
                        case 2:
                        {
                            var r = Empty.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.GetMembers(r);
                                var grainResponse = new GrainResponse
                                {
                                    MessageData = res.ToByteString(),
                                };
                                context.Respond(grainResponse);
                            }
                            catch (Exception x)
                            {
                                var grainErrorResponse = new GrainErrorResponse
                                {
                                    Err = x.ToString()
                                };
                                context.Respond(grainErrorResponse);
                            }

                            break;
                        }
                        case 3:
                        {
                            var r = ChannelMsg.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.BroadcastChatMsg(r);
                                var grainResponse = new GrainResponse
                                {
                                    MessageData = res.ToByteString(),
                                };
                                context.Respond(grainResponse);
                            }
                            catch (Exception x)
                            {
                                var grainErrorResponse = new GrainErrorResponse
                                {
                                    Err = x.ToString()
                                };
                                context.Respond(grainErrorResponse);
                            }

                            break;
                        }
                    }

                    break;
                }
            }
        }
    }
    public interface IUserGrain
    {
        Task<OpSuccess> JoinChannel(TargetChannel request);
        Task<OpSuccess> LeaveChannel(TargetChannel request);
        Task<Empty> TellChannelMsg(ChannelMsg request);
        Task<Empty> TellSomeoneLeftChannel(UserLeftUpdate request);
        Task<Empty> TellSomeoneJoinedChannel(UserJoinedUpdate request);
    }

    public class UserGrainClient
    {
        private readonly string _id;

        public UserGrainClient(string id)
        {
            _id = id;
        }

        public Task<OpSuccess> JoinChannel(TargetChannel request) => JoinChannel(request, CancellationToken.None);

        public async Task<OpSuccess> JoinChannel(TargetChannel request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 0,
                MessageData = request.ToByteString()
            };

            async Task<OpSuccess> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "UserGrain", ct);

                if (statusCode != ResponseStatusCode.OK)
                {
                    throw new Exception($"Get PID failed with StatusCode: {statusCode}");  
                }

                //request the RPC method to be invoked
                var res = await pid.RequestAsync<object>(gr, ct);

                //did we get a response?
                if (res is GrainResponse grainResponse)
                {
                    return OpSuccess.Parser.ParseFrom(grainResponse.MessageData);
                }

                //did we get an error response?
                if (res is GrainErrorResponse grainErrorResponse)
                {
                    throw new Exception(grainErrorResponse.Err);
                }
                throw new NotSupportedException();
            }

            for(int i= 0;i < options.RetryCount; i++)
            {
                try
                {
                    return await Inner();
                }
                catch(Exception x)
                {
                    if (options.RetryAction != null)
                    {
                        await options.RetryAction(i);
                    }
                }
            }
            return await Inner();
        }
        public Task<OpSuccess> LeaveChannel(TargetChannel request) => LeaveChannel(request, CancellationToken.None);

        public async Task<OpSuccess> LeaveChannel(TargetChannel request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 1,
                MessageData = request.ToByteString()
            };

            async Task<OpSuccess> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "UserGrain", ct);

                if (statusCode != ResponseStatusCode.OK)
                {
                    throw new Exception($"Get PID failed with StatusCode: {statusCode}");  
                }

                //request the RPC method to be invoked
                var res = await pid.RequestAsync<object>(gr, ct);

                //did we get a response?
                if (res is GrainResponse grainResponse)
                {
                    return OpSuccess.Parser.ParseFrom(grainResponse.MessageData);
                }

                //did we get an error response?
                if (res is GrainErrorResponse grainErrorResponse)
                {
                    throw new Exception(grainErrorResponse.Err);
                }
                throw new NotSupportedException();
            }

            for(int i= 0;i < options.RetryCount; i++)
            {
                try
                {
                    return await Inner();
                }
                catch(Exception x)
                {
                    if (options.RetryAction != null)
                    {
                        await options.RetryAction(i);
                    }
                }
            }
            return await Inner();
        }
        public Task<Empty> TellChannelMsg(ChannelMsg request) => TellChannelMsg(request, CancellationToken.None);

        public async Task<Empty> TellChannelMsg(ChannelMsg request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 2,
                MessageData = request.ToByteString()
            };

            async Task<Empty> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "UserGrain", ct);

                if (statusCode != ResponseStatusCode.OK)
                {
                    throw new Exception($"Get PID failed with StatusCode: {statusCode}");  
                }

                //request the RPC method to be invoked
                var res = await pid.RequestAsync<object>(gr, ct);

                //did we get a response?
                if (res is GrainResponse grainResponse)
                {
                    return Empty.Parser.ParseFrom(grainResponse.MessageData);
                }

                //did we get an error response?
                if (res is GrainErrorResponse grainErrorResponse)
                {
                    throw new Exception(grainErrorResponse.Err);
                }
                throw new NotSupportedException();
            }

            for(int i= 0;i < options.RetryCount; i++)
            {
                try
                {
                    return await Inner();
                }
                catch(Exception x)
                {
                    if (options.RetryAction != null)
                    {
                        await options.RetryAction(i);
                    }
                }
            }
            return await Inner();
        }
        public Task<Empty> TellSomeoneLeftChannel(UserLeftUpdate request) => TellSomeoneLeftChannel(request, CancellationToken.None);

        public async Task<Empty> TellSomeoneLeftChannel(UserLeftUpdate request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 3,
                MessageData = request.ToByteString()
            };

            async Task<Empty> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "UserGrain", ct);

                if (statusCode != ResponseStatusCode.OK)
                {
                    throw new Exception($"Get PID failed with StatusCode: {statusCode}");  
                }

                //request the RPC method to be invoked
                var res = await pid.RequestAsync<object>(gr, ct);

                //did we get a response?
                if (res is GrainResponse grainResponse)
                {
                    return Empty.Parser.ParseFrom(grainResponse.MessageData);
                }

                //did we get an error response?
                if (res is GrainErrorResponse grainErrorResponse)
                {
                    throw new Exception(grainErrorResponse.Err);
                }
                throw new NotSupportedException();
            }

            for(int i= 0;i < options.RetryCount; i++)
            {
                try
                {
                    return await Inner();
                }
                catch(Exception x)
                {
                    if (options.RetryAction != null)
                    {
                        await options.RetryAction(i);
                    }
                }
            }
            return await Inner();
        }
        public Task<Empty> TellSomeoneJoinedChannel(UserJoinedUpdate request) => TellSomeoneJoinedChannel(request, CancellationToken.None);

        public async Task<Empty> TellSomeoneJoinedChannel(UserJoinedUpdate request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 4,
                MessageData = request.ToByteString()
            };

            async Task<Empty> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "UserGrain", ct);

                if (statusCode != ResponseStatusCode.OK)
                {
                    throw new Exception($"Get PID failed with StatusCode: {statusCode}");  
                }

                //request the RPC method to be invoked
                var res = await pid.RequestAsync<object>(gr, ct);

                //did we get a response?
                if (res is GrainResponse grainResponse)
                {
                    return Empty.Parser.ParseFrom(grainResponse.MessageData);
                }

                //did we get an error response?
                if (res is GrainErrorResponse grainErrorResponse)
                {
                    throw new Exception(grainErrorResponse.Err);
                }
                throw new NotSupportedException();
            }

            for(int i= 0;i < options.RetryCount; i++)
            {
                try
                {
                    return await Inner();
                }
                catch(Exception x)
                {
                    if (options.RetryAction != null)
                    {
                        await options.RetryAction(i);
                    }
                }
            }
            return await Inner();
        }
    }

    public class UserGrainActor : IActor
    {
        private IUserGrain _inner;

        public async Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Started _:
                {
                    _inner = Grains._UserGrainFactory();
                    context.SetReceiveTimeout(TimeSpan.FromSeconds(30));
                    break;
                }
                case ReceiveTimeout _:
                {
                    context.Self.Stop();
                    break;
                }
                case GrainRequest request:
                {
                    switch (request.MethodIndex)
                    {
                        case 0:
                        {
                            var r = TargetChannel.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.JoinChannel(r);
                                var grainResponse = new GrainResponse
                                {
                                    MessageData = res.ToByteString(),
                                };
                                context.Respond(grainResponse);
                            }
                            catch (Exception x)
                            {
                                var grainErrorResponse = new GrainErrorResponse
                                {
                                    Err = x.ToString()
                                };
                                context.Respond(grainErrorResponse);
                            }

                            break;
                        }
                        case 1:
                        {
                            var r = TargetChannel.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.LeaveChannel(r);
                                var grainResponse = new GrainResponse
                                {
                                    MessageData = res.ToByteString(),
                                };
                                context.Respond(grainResponse);
                            }
                            catch (Exception x)
                            {
                                var grainErrorResponse = new GrainErrorResponse
                                {
                                    Err = x.ToString()
                                };
                                context.Respond(grainErrorResponse);
                            }

                            break;
                        }
                        case 2:
                        {
                            var r = ChannelMsg.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.TellChannelMsg(r);
                                var grainResponse = new GrainResponse
                                {
                                    MessageData = res.ToByteString(),
                                };
                                context.Respond(grainResponse);
                            }
                            catch (Exception x)
                            {
                                var grainErrorResponse = new GrainErrorResponse
                                {
                                    Err = x.ToString()
                                };
                                context.Respond(grainErrorResponse);
                            }

                            break;
                        }
                        case 3:
                        {
                            var r = UserLeftUpdate.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.TellSomeoneLeftChannel(r);
                                var grainResponse = new GrainResponse
                                {
                                    MessageData = res.ToByteString(),
                                };
                                context.Respond(grainResponse);
                            }
                            catch (Exception x)
                            {
                                var grainErrorResponse = new GrainErrorResponse
                                {
                                    Err = x.ToString()
                                };
                                context.Respond(grainErrorResponse);
                            }

                            break;
                        }
                        case 4:
                        {
                            var r = UserJoinedUpdate.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.TellSomeoneJoinedChannel(r);
                                var grainResponse = new GrainResponse
                                {
                                    MessageData = res.ToByteString(),
                                };
                                context.Respond(grainResponse);
                            }
                            catch (Exception x)
                            {
                                var grainErrorResponse = new GrainErrorResponse
                                {
                                    Err = x.ToString()
                                };
                                context.Respond(grainErrorResponse);
                            }

                            break;
                        }
                    }

                    break;
                }
            }
        }
    }
}

