// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Messages.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Messages {

  /// <summary>Holder for reflection information generated from Messages.proto</summary>
  public static partial class MessagesReflection {

    #region Descriptor
    /// <summary>File descriptor for Messages.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MessagesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg5NZXNzYWdlcy5wcm90bxIIbWVzc2FnZXMiJgoHUGlkSW5mbxIPCgdhZGRy",
            "ZXNzGAEgASgJEgoKAmlkGAIgASgJIg4KDEVtcHR5TWVzc2FnZSI2Cg5NZXNz",
            "YWdlV2l0aFBJRBIkCglzZW5kZXJQSUQYASABKAsyES5tZXNzYWdlcy5QaWRJ",
            "bmZvIj0KFVRyaWdnZXJTZW5kRW1wdHlNc2dUbxIkCgl0YXJnZXRQSUQYASAB",
            "KAsyES5tZXNzYWdlcy5QaWRJbmZvIj8KF1RyaWdnZXJTZW5kTXNnV2l0aFBJ",
            "RFRvEiQKCXRhcmdldFBJRBgBIAEoCzIRLm1lc3NhZ2VzLlBpZEluZm9CC6oC",
            "CE1lc3NhZ2VzYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.PidInfo), global::Messages.PidInfo.Parser, new[]{ "Address", "Id" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.EmptyMessage), global::Messages.EmptyMessage.Parser, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.MessageWithPID), global::Messages.MessageWithPID.Parser, new[]{ "SenderPID" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.TriggerSendEmptyMsgTo), global::Messages.TriggerSendEmptyMsgTo.Parser, new[]{ "TargetPID" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.TriggerSendMsgWithPIDTo), global::Messages.TriggerSendMsgWithPIDTo.Parser, new[]{ "TargetPID" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class PidInfo : pb::IMessage<PidInfo> {
    private static readonly pb::MessageParser<PidInfo> _parser = new pb::MessageParser<PidInfo>(() => new PidInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PidInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PidInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PidInfo(PidInfo other) : this() {
      address_ = other.address_;
      id_ = other.id_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PidInfo Clone() {
      return new PidInfo(this);
    }

    /// <summary>Field number for the "address" field.</summary>
    public const int AddressFieldNumber = 1;
    private string address_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Address {
      get { return address_; }
      set {
        address_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 2;
    private string id_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Id {
      get { return id_; }
      set {
        id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PidInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PidInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Address != other.Address) return false;
      if (Id != other.Id) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Address.Length != 0) hash ^= Address.GetHashCode();
      if (Id.Length != 0) hash ^= Id.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Address.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Address);
      }
      if (Id.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Id);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Address.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Address);
      }
      if (Id.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PidInfo other) {
      if (other == null) {
        return;
      }
      if (other.Address.Length != 0) {
        Address = other.Address;
      }
      if (other.Id.Length != 0) {
        Id = other.Id;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Address = input.ReadString();
            break;
          }
          case 18: {
            Id = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class EmptyMessage : pb::IMessage<EmptyMessage> {
    private static readonly pb::MessageParser<EmptyMessage> _parser = new pb::MessageParser<EmptyMessage>(() => new EmptyMessage());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<EmptyMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EmptyMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EmptyMessage(EmptyMessage other) : this() {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EmptyMessage Clone() {
      return new EmptyMessage(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as EmptyMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(EmptyMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(EmptyMessage other) {
      if (other == null) {
        return;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  public sealed partial class MessageWithPID : pb::IMessage<MessageWithPID> {
    private static readonly pb::MessageParser<MessageWithPID> _parser = new pb::MessageParser<MessageWithPID>(() => new MessageWithPID());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MessageWithPID> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageWithPID() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageWithPID(MessageWithPID other) : this() {
      SenderPID = other.senderPID_ != null ? other.SenderPID.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageWithPID Clone() {
      return new MessageWithPID(this);
    }

    /// <summary>Field number for the "senderPID" field.</summary>
    public const int SenderPIDFieldNumber = 1;
    private global::Messages.PidInfo senderPID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Messages.PidInfo SenderPID {
      get { return senderPID_; }
      set {
        senderPID_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MessageWithPID);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MessageWithPID other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(SenderPID, other.SenderPID)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (senderPID_ != null) hash ^= SenderPID.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (senderPID_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(SenderPID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (senderPID_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(SenderPID);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MessageWithPID other) {
      if (other == null) {
        return;
      }
      if (other.senderPID_ != null) {
        if (senderPID_ == null) {
          senderPID_ = new global::Messages.PidInfo();
        }
        SenderPID.MergeFrom(other.SenderPID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (senderPID_ == null) {
              senderPID_ = new global::Messages.PidInfo();
            }
            input.ReadMessage(senderPID_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class TriggerSendEmptyMsgTo : pb::IMessage<TriggerSendEmptyMsgTo> {
    private static readonly pb::MessageParser<TriggerSendEmptyMsgTo> _parser = new pb::MessageParser<TriggerSendEmptyMsgTo>(() => new TriggerSendEmptyMsgTo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TriggerSendEmptyMsgTo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TriggerSendEmptyMsgTo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TriggerSendEmptyMsgTo(TriggerSendEmptyMsgTo other) : this() {
      TargetPID = other.targetPID_ != null ? other.TargetPID.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TriggerSendEmptyMsgTo Clone() {
      return new TriggerSendEmptyMsgTo(this);
    }

    /// <summary>Field number for the "targetPID" field.</summary>
    public const int TargetPIDFieldNumber = 1;
    private global::Messages.PidInfo targetPID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Messages.PidInfo TargetPID {
      get { return targetPID_; }
      set {
        targetPID_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TriggerSendEmptyMsgTo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TriggerSendEmptyMsgTo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(TargetPID, other.TargetPID)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (targetPID_ != null) hash ^= TargetPID.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (targetPID_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(TargetPID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (targetPID_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(TargetPID);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TriggerSendEmptyMsgTo other) {
      if (other == null) {
        return;
      }
      if (other.targetPID_ != null) {
        if (targetPID_ == null) {
          targetPID_ = new global::Messages.PidInfo();
        }
        TargetPID.MergeFrom(other.TargetPID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (targetPID_ == null) {
              targetPID_ = new global::Messages.PidInfo();
            }
            input.ReadMessage(targetPID_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class TriggerSendMsgWithPIDTo : pb::IMessage<TriggerSendMsgWithPIDTo> {
    private static readonly pb::MessageParser<TriggerSendMsgWithPIDTo> _parser = new pb::MessageParser<TriggerSendMsgWithPIDTo>(() => new TriggerSendMsgWithPIDTo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TriggerSendMsgWithPIDTo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TriggerSendMsgWithPIDTo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TriggerSendMsgWithPIDTo(TriggerSendMsgWithPIDTo other) : this() {
      TargetPID = other.targetPID_ != null ? other.TargetPID.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TriggerSendMsgWithPIDTo Clone() {
      return new TriggerSendMsgWithPIDTo(this);
    }

    /// <summary>Field number for the "targetPID" field.</summary>
    public const int TargetPIDFieldNumber = 1;
    private global::Messages.PidInfo targetPID_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Messages.PidInfo TargetPID {
      get { return targetPID_; }
      set {
        targetPID_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TriggerSendMsgWithPIDTo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TriggerSendMsgWithPIDTo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(TargetPID, other.TargetPID)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (targetPID_ != null) hash ^= TargetPID.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (targetPID_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(TargetPID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (targetPID_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(TargetPID);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TriggerSendMsgWithPIDTo other) {
      if (other == null) {
        return;
      }
      if (other.targetPID_ != null) {
        if (targetPID_ == null) {
          targetPID_ = new global::Messages.PidInfo();
        }
        TargetPID.MergeFrom(other.TargetPID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (targetPID_ == null) {
              targetPID_ = new global::Messages.PidInfo();
            }
            input.ReadMessage(targetPID_);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code