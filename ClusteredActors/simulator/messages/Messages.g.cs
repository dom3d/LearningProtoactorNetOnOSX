// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: messages.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Messages {

  /// <summary>Holder for reflection information generated from messages.proto</summary>
  public static partial class MessagesReflection {

    #region Descriptor
    /// <summary>File descriptor for messages.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MessagesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg5tZXNzYWdlcy5wcm90bxIIbWVzc2FnZXMaHGxpYi9Qcm90by5BY3Rvci9w",
            "cm90b3MucHJvdG8iDgoMU2ltSGVhcnRCZWF0IicKCVRhcmdldFBJRBIaCgZ0",
            "YXJnZXQYASABKAsyCi5hY3Rvci5QSUQiKQoLSm9pbkNoYW5uZWwSGgoGc2Vu",
            "ZGVyGAEgASgLMgouYWN0b3IuUElEIioKDExlYXZlQ2hhbm5lbBIaCgZzZW5k",
            "ZXIYASABKAsyCi5hY3Rvci5QSUQiGgoISW50VmFsdWUSDgoGbnVtYmVyGAEg",
            "ASgFQguqAghNZXNzYWdlc2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Proto.ProtosReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.SimHeartBeat), global::Messages.SimHeartBeat.Parser, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.TargetPID), global::Messages.TargetPID.Parser, new[]{ "Target" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.JoinChannel), global::Messages.JoinChannel.Parser, new[]{ "Sender" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.LeaveChannel), global::Messages.LeaveChannel.Parser, new[]{ "Sender" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Messages.IntValue), global::Messages.IntValue.Parser, new[]{ "Number" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SimHeartBeat : pb::IMessage<SimHeartBeat> {
    private static readonly pb::MessageParser<SimHeartBeat> _parser = new pb::MessageParser<SimHeartBeat>(() => new SimHeartBeat());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SimHeartBeat> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SimHeartBeat() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SimHeartBeat(SimHeartBeat other) : this() {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SimHeartBeat Clone() {
      return new SimHeartBeat(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SimHeartBeat);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SimHeartBeat other) {
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
    public void MergeFrom(SimHeartBeat other) {
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

  public sealed partial class TargetPID : pb::IMessage<TargetPID> {
    private static readonly pb::MessageParser<TargetPID> _parser = new pb::MessageParser<TargetPID>(() => new TargetPID());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TargetPID> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TargetPID() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TargetPID(TargetPID other) : this() {
      Target = other.target_ != null ? other.Target.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TargetPID Clone() {
      return new TargetPID(this);
    }

    /// <summary>Field number for the "target" field.</summary>
    public const int TargetFieldNumber = 1;
    private global::Proto.PID target_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Proto.PID Target {
      get { return target_; }
      set {
        target_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TargetPID);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TargetPID other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Target, other.Target)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (target_ != null) hash ^= Target.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (target_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Target);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (target_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Target);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TargetPID other) {
      if (other == null) {
        return;
      }
      if (other.target_ != null) {
        if (target_ == null) {
          target_ = new global::Proto.PID();
        }
        Target.MergeFrom(other.Target);
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
            if (target_ == null) {
              target_ = new global::Proto.PID();
            }
            input.ReadMessage(target_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class JoinChannel : pb::IMessage<JoinChannel> {
    private static readonly pb::MessageParser<JoinChannel> _parser = new pb::MessageParser<JoinChannel>(() => new JoinChannel());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<JoinChannel> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public JoinChannel() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public JoinChannel(JoinChannel other) : this() {
      Sender = other.sender_ != null ? other.Sender.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public JoinChannel Clone() {
      return new JoinChannel(this);
    }

    /// <summary>Field number for the "sender" field.</summary>
    public const int SenderFieldNumber = 1;
    private global::Proto.PID sender_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Proto.PID Sender {
      get { return sender_; }
      set {
        sender_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as JoinChannel);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(JoinChannel other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Sender, other.Sender)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (sender_ != null) hash ^= Sender.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (sender_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Sender);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (sender_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Sender);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(JoinChannel other) {
      if (other == null) {
        return;
      }
      if (other.sender_ != null) {
        if (sender_ == null) {
          sender_ = new global::Proto.PID();
        }
        Sender.MergeFrom(other.Sender);
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
            if (sender_ == null) {
              sender_ = new global::Proto.PID();
            }
            input.ReadMessage(sender_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class LeaveChannel : pb::IMessage<LeaveChannel> {
    private static readonly pb::MessageParser<LeaveChannel> _parser = new pb::MessageParser<LeaveChannel>(() => new LeaveChannel());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<LeaveChannel> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LeaveChannel() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LeaveChannel(LeaveChannel other) : this() {
      Sender = other.sender_ != null ? other.Sender.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LeaveChannel Clone() {
      return new LeaveChannel(this);
    }

    /// <summary>Field number for the "sender" field.</summary>
    public const int SenderFieldNumber = 1;
    private global::Proto.PID sender_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Proto.PID Sender {
      get { return sender_; }
      set {
        sender_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as LeaveChannel);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(LeaveChannel other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Sender, other.Sender)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (sender_ != null) hash ^= Sender.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (sender_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Sender);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (sender_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Sender);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(LeaveChannel other) {
      if (other == null) {
        return;
      }
      if (other.sender_ != null) {
        if (sender_ == null) {
          sender_ = new global::Proto.PID();
        }
        Sender.MergeFrom(other.Sender);
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
            if (sender_ == null) {
              sender_ = new global::Proto.PID();
            }
            input.ReadMessage(sender_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class IntValue : pb::IMessage<IntValue> {
    private static readonly pb::MessageParser<IntValue> _parser = new pb::MessageParser<IntValue>(() => new IntValue());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<IntValue> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Messages.MessagesReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public IntValue() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public IntValue(IntValue other) : this() {
      number_ = other.number_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public IntValue Clone() {
      return new IntValue(this);
    }

    /// <summary>Field number for the "number" field.</summary>
    public const int NumberFieldNumber = 1;
    private int number_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Number {
      get { return number_; }
      set {
        number_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as IntValue);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(IntValue other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Number != other.Number) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Number != 0) hash ^= Number.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Number != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Number);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Number != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Number);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(IntValue other) {
      if (other == null) {
        return;
      }
      if (other.Number != 0) {
        Number = other.Number;
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
          case 8: {
            Number = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
