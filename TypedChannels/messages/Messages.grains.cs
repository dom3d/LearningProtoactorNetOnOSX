
using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Proto;
using Proto.Cluster;
using Proto.Remote;

namespace RPC
{
    public static class Grains
    {
        internal static Func<IBasicChannel> _BasicChannelFactory;

        public static void BasicChannelFactory(Func<IBasicChannel> factory) 
        {
            _BasicChannelFactory = factory;
            Remote.RegisterKnownKind("BasicChannel", Actor.FromProducer(() => new BasicChannelActor()));
        } 

        public static BasicChannelClient BasicChannel(string id) => new BasicChannelClient(id);
    }

    public interface IBasicChannel
    {
        Task<OpSuccess> Add(OpTarget request);
        Task<OpSuccess> AddInvisible(OpTarget request);
        Task<OpSuccess> Remove(OpTarget request);
        Task<MemberList> GetVisibleMembers(Empty request);
        Task<MemberList> GetAllMembers(Empty request);
        Task<OpSuccess> BroadcastString(StringMsg request);
    }

    public class BasicChannelClient
    {
        private readonly string _id;

        public BasicChannelClient(string id)
        {
            _id = id;
        }

        public Task<OpSuccess> Add(OpTarget request) => Add(request, CancellationToken.None);

        public async Task<OpSuccess> Add(OpTarget request, CancellationToken ct, GrainCallOptions options = null)
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
                var (pid, statusCode) = await Cluster.GetAsync(_id, "BasicChannel", ct);

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
        public Task<OpSuccess> AddInvisible(OpTarget request) => AddInvisible(request, CancellationToken.None);

        public async Task<OpSuccess> AddInvisible(OpTarget request, CancellationToken ct, GrainCallOptions options = null)
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
                var (pid, statusCode) = await Cluster.GetAsync(_id, "BasicChannel", ct);

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
        public Task<OpSuccess> Remove(OpTarget request) => Remove(request, CancellationToken.None);

        public async Task<OpSuccess> Remove(OpTarget request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 2,
                MessageData = request.ToByteString()
            };

            async Task<OpSuccess> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "BasicChannel", ct);

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
        public Task<MemberList> GetVisibleMembers(Empty request) => GetVisibleMembers(request, CancellationToken.None);

        public async Task<MemberList> GetVisibleMembers(Empty request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 3,
                MessageData = request.ToByteString()
            };

            async Task<MemberList> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "BasicChannel", ct);

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
        public Task<MemberList> GetAllMembers(Empty request) => GetAllMembers(request, CancellationToken.None);

        public async Task<MemberList> GetAllMembers(Empty request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 4,
                MessageData = request.ToByteString()
            };

            async Task<MemberList> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "BasicChannel", ct);

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
        public Task<OpSuccess> BroadcastString(StringMsg request) => BroadcastString(request, CancellationToken.None);

        public async Task<OpSuccess> BroadcastString(StringMsg request, CancellationToken ct, GrainCallOptions options = null)
        {
            options = options ?? GrainCallOptions.Default;
            
            var gr = new GrainRequest
            {
                MethodIndex = 5,
                MessageData = request.ToByteString()
            };

            async Task<OpSuccess> Inner() 
            {
                //resolve the grain
                var (pid, statusCode) = await Cluster.GetAsync(_id, "BasicChannel", ct);

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

    public class BasicChannelActor : IActor
    {
        private IBasicChannel _inner;

        public async Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Started _:
                {
                    _inner = Grains._BasicChannelFactory();
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
                            var r = OpTarget.Parser.ParseFrom(request.MessageData);
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
                            var r = OpTarget.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.AddInvisible(r);
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
                            var r = OpTarget.Parser.ParseFrom(request.MessageData);
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
                        case 3:
                        {
                            var r = Empty.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.GetVisibleMembers(r);
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
                            var r = Empty.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.GetAllMembers(r);
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
                        case 5:
                        {
                            var r = StringMsg.Parser.ParseFrom(request.MessageData);
                            try
                            {
                                var res = await _inner.BroadcastString(r);
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

