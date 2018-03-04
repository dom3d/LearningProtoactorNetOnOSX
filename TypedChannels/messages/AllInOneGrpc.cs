// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: AllInOne.proto
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using grpc = global::Grpc.Core;

namespace Chat {
  public static partial class ChannelGrain
  {
    static readonly string __ServiceName = "messages.ChannelGrain";

    static readonly grpc::Marshaller<global::Chat.TargetUser> __Marshaller_TargetUser = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.TargetUser.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Chat.OpSuccess> __Marshaller_OpSuccess = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.OpSuccess.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Chat.Empty> __Marshaller_Empty = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.Empty.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Chat.MemberList> __Marshaller_MemberList = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.MemberList.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Chat.ChannelMsg> __Marshaller_ChannelMsg = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.ChannelMsg.Parser.ParseFrom);

    static readonly grpc::Method<global::Chat.TargetUser, global::Chat.OpSuccess> __Method_Add = new grpc::Method<global::Chat.TargetUser, global::Chat.OpSuccess>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Add",
        __Marshaller_TargetUser,
        __Marshaller_OpSuccess);

    static readonly grpc::Method<global::Chat.TargetUser, global::Chat.OpSuccess> __Method_Remove = new grpc::Method<global::Chat.TargetUser, global::Chat.OpSuccess>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Remove",
        __Marshaller_TargetUser,
        __Marshaller_OpSuccess);

    static readonly grpc::Method<global::Chat.Empty, global::Chat.MemberList> __Method_GetMembers = new grpc::Method<global::Chat.Empty, global::Chat.MemberList>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetMembers",
        __Marshaller_Empty,
        __Marshaller_MemberList);

    static readonly grpc::Method<global::Chat.ChannelMsg, global::Chat.OpSuccess> __Method_BroadcastChatMsg = new grpc::Method<global::Chat.ChannelMsg, global::Chat.OpSuccess>(
        grpc::MethodType.Unary,
        __ServiceName,
        "BroadcastChatMsg",
        __Marshaller_ChannelMsg,
        __Marshaller_OpSuccess);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Chat.AllInOneReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ChannelGrain</summary>
    public abstract partial class ChannelGrainBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Chat.OpSuccess> Add(global::Chat.TargetUser request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Chat.OpSuccess> Remove(global::Chat.TargetUser request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Chat.MemberList> GetMembers(global::Chat.Empty request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Chat.OpSuccess> BroadcastChatMsg(global::Chat.ChannelMsg request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for ChannelGrain</summary>
    public partial class ChannelGrainClient : grpc::ClientBase<ChannelGrainClient>
    {
      /// <summary>Creates a new client for ChannelGrain</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ChannelGrainClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ChannelGrain that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ChannelGrainClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ChannelGrainClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ChannelGrainClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Chat.OpSuccess Add(global::Chat.TargetUser request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return Add(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Chat.OpSuccess Add(global::Chat.TargetUser request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Add, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> AddAsync(global::Chat.TargetUser request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return AddAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> AddAsync(global::Chat.TargetUser request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Add, null, options, request);
      }
      public virtual global::Chat.OpSuccess Remove(global::Chat.TargetUser request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return Remove(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Chat.OpSuccess Remove(global::Chat.TargetUser request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Remove, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> RemoveAsync(global::Chat.TargetUser request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return RemoveAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> RemoveAsync(global::Chat.TargetUser request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Remove, null, options, request);
      }
      public virtual global::Chat.MemberList GetMembers(global::Chat.Empty request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetMembers(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Chat.MemberList GetMembers(global::Chat.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetMembers, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.MemberList> GetMembersAsync(global::Chat.Empty request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetMembersAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.MemberList> GetMembersAsync(global::Chat.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetMembers, null, options, request);
      }
      public virtual global::Chat.OpSuccess BroadcastChatMsg(global::Chat.ChannelMsg request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return BroadcastChatMsg(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Chat.OpSuccess BroadcastChatMsg(global::Chat.ChannelMsg request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_BroadcastChatMsg, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> BroadcastChatMsgAsync(global::Chat.ChannelMsg request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return BroadcastChatMsgAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> BroadcastChatMsgAsync(global::Chat.ChannelMsg request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_BroadcastChatMsg, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ChannelGrainClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ChannelGrainClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ChannelGrainBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Add, serviceImpl.Add)
          .AddMethod(__Method_Remove, serviceImpl.Remove)
          .AddMethod(__Method_GetMembers, serviceImpl.GetMembers)
          .AddMethod(__Method_BroadcastChatMsg, serviceImpl.BroadcastChatMsg).Build();
    }

  }
  public static partial class UserGrain
  {
    static readonly string __ServiceName = "messages.UserGrain";

    static readonly grpc::Marshaller<global::Chat.TargetChannel> __Marshaller_TargetChannel = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.TargetChannel.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Chat.OpSuccess> __Marshaller_OpSuccess = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.OpSuccess.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Chat.ChannelMsg> __Marshaller_ChannelMsg = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.ChannelMsg.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Chat.Empty> __Marshaller_Empty = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.Empty.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Chat.UserLeftUpdate> __Marshaller_UserLeftUpdate = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.UserLeftUpdate.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Chat.UserJoinedUpdate> __Marshaller_UserJoinedUpdate = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Chat.UserJoinedUpdate.Parser.ParseFrom);

    static readonly grpc::Method<global::Chat.TargetChannel, global::Chat.OpSuccess> __Method_JoinChannel = new grpc::Method<global::Chat.TargetChannel, global::Chat.OpSuccess>(
        grpc::MethodType.Unary,
        __ServiceName,
        "JoinChannel",
        __Marshaller_TargetChannel,
        __Marshaller_OpSuccess);

    static readonly grpc::Method<global::Chat.TargetChannel, global::Chat.OpSuccess> __Method_LeaveChannel = new grpc::Method<global::Chat.TargetChannel, global::Chat.OpSuccess>(
        grpc::MethodType.Unary,
        __ServiceName,
        "LeaveChannel",
        __Marshaller_TargetChannel,
        __Marshaller_OpSuccess);

    static readonly grpc::Method<global::Chat.ChannelMsg, global::Chat.Empty> __Method_TellChannelMsg = new grpc::Method<global::Chat.ChannelMsg, global::Chat.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "TellChannelMsg",
        __Marshaller_ChannelMsg,
        __Marshaller_Empty);

    static readonly grpc::Method<global::Chat.UserLeftUpdate, global::Chat.Empty> __Method_TellSomeoneLeftChannel = new grpc::Method<global::Chat.UserLeftUpdate, global::Chat.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "TellSomeoneLeftChannel",
        __Marshaller_UserLeftUpdate,
        __Marshaller_Empty);

    static readonly grpc::Method<global::Chat.UserJoinedUpdate, global::Chat.Empty> __Method_TellSomeoneJoinedChannel = new grpc::Method<global::Chat.UserJoinedUpdate, global::Chat.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "TellSomeoneJoinedChannel",
        __Marshaller_UserJoinedUpdate,
        __Marshaller_Empty);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Chat.AllInOneReflection.Descriptor.Services[1]; }
    }

    /// <summary>Base class for server-side implementations of UserGrain</summary>
    public abstract partial class UserGrainBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Chat.OpSuccess> JoinChannel(global::Chat.TargetChannel request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Chat.OpSuccess> LeaveChannel(global::Chat.TargetChannel request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Chat.Empty> TellChannelMsg(global::Chat.ChannelMsg request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Chat.Empty> TellSomeoneLeftChannel(global::Chat.UserLeftUpdate request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Chat.Empty> TellSomeoneJoinedChannel(global::Chat.UserJoinedUpdate request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for UserGrain</summary>
    public partial class UserGrainClient : grpc::ClientBase<UserGrainClient>
    {
      /// <summary>Creates a new client for UserGrain</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public UserGrainClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for UserGrain that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public UserGrainClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected UserGrainClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected UserGrainClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Chat.OpSuccess JoinChannel(global::Chat.TargetChannel request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return JoinChannel(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Chat.OpSuccess JoinChannel(global::Chat.TargetChannel request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_JoinChannel, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> JoinChannelAsync(global::Chat.TargetChannel request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return JoinChannelAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> JoinChannelAsync(global::Chat.TargetChannel request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_JoinChannel, null, options, request);
      }
      public virtual global::Chat.OpSuccess LeaveChannel(global::Chat.TargetChannel request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return LeaveChannel(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Chat.OpSuccess LeaveChannel(global::Chat.TargetChannel request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_LeaveChannel, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> LeaveChannelAsync(global::Chat.TargetChannel request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return LeaveChannelAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.OpSuccess> LeaveChannelAsync(global::Chat.TargetChannel request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_LeaveChannel, null, options, request);
      }
      public virtual global::Chat.Empty TellChannelMsg(global::Chat.ChannelMsg request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return TellChannelMsg(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Chat.Empty TellChannelMsg(global::Chat.ChannelMsg request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_TellChannelMsg, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.Empty> TellChannelMsgAsync(global::Chat.ChannelMsg request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return TellChannelMsgAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.Empty> TellChannelMsgAsync(global::Chat.ChannelMsg request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_TellChannelMsg, null, options, request);
      }
      public virtual global::Chat.Empty TellSomeoneLeftChannel(global::Chat.UserLeftUpdate request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return TellSomeoneLeftChannel(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Chat.Empty TellSomeoneLeftChannel(global::Chat.UserLeftUpdate request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_TellSomeoneLeftChannel, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.Empty> TellSomeoneLeftChannelAsync(global::Chat.UserLeftUpdate request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return TellSomeoneLeftChannelAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.Empty> TellSomeoneLeftChannelAsync(global::Chat.UserLeftUpdate request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_TellSomeoneLeftChannel, null, options, request);
      }
      public virtual global::Chat.Empty TellSomeoneJoinedChannel(global::Chat.UserJoinedUpdate request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return TellSomeoneJoinedChannel(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Chat.Empty TellSomeoneJoinedChannel(global::Chat.UserJoinedUpdate request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_TellSomeoneJoinedChannel, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.Empty> TellSomeoneJoinedChannelAsync(global::Chat.UserJoinedUpdate request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return TellSomeoneJoinedChannelAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Chat.Empty> TellSomeoneJoinedChannelAsync(global::Chat.UserJoinedUpdate request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_TellSomeoneJoinedChannel, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override UserGrainClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new UserGrainClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(UserGrainBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_JoinChannel, serviceImpl.JoinChannel)
          .AddMethod(__Method_LeaveChannel, serviceImpl.LeaveChannel)
          .AddMethod(__Method_TellChannelMsg, serviceImpl.TellChannelMsg)
          .AddMethod(__Method_TellSomeoneLeftChannel, serviceImpl.TellSomeoneLeftChannel)
          .AddMethod(__Method_TellSomeoneJoinedChannel, serviceImpl.TellSomeoneJoinedChannel).Build();
    }

  }
}
#endregion
