using System;
using System.Threading.Tasks;
using Proto;
using RPC;


namespace TypedChannelsExperiment
{
	public class TypedChannelGrain : IBasicChannel
	{
		private MemberList _visibleMembers = new MemberList();
		private MemberList _invisbleMembers = new MemberList();
		private MemberList _allMembers = new MemberList();

		public Task<OpSuccess> Add(OpTarget target)
		{
			if (_visibleMembers.PidList.Contains(target.Pid) == false)
			{
				_visibleMembers.PidList.Add(target.Pid);
				_allMembers.PidList.Add(target.Pid);
			}
			return Task.FromResult ( new OpSuccess() { Success = true } );
		}

		public Task<OpSuccess> AddInvisible(OpTarget target)
		{
			if (_invisbleMembers.PidList.Contains(target.Pid) == false)
			{
				_invisbleMembers.PidList.Add(target.Pid);
				_allMembers.PidList.Add(target.Pid);
			}
			return Task.FromResult(new OpSuccess() { Success = true });
		}

		public Task<OpSuccess> Remove(OpTarget target)
		{
			if (_visibleMembers.PidList.Contains(target.Pid) == true)
			{
				_visibleMembers.PidList.Remove(target.Pid);
				_allMembers.PidList.Remove(target.Pid);
			}
			else if (_invisbleMembers.PidList.Contains(target.Pid) == true)
			{
				_invisbleMembers.PidList.Remove(target.Pid);
				_allMembers.PidList.Remove(target.Pid);
			}

			return Task.FromResult(new OpSuccess() { Success = true });
		}

		public Task<MemberList> GetVisibleMembers(Empty _)
		{
			//var replyMsg = new MemberList()
			//{
			//	PidList = { new PID() { Address = "", Id = "" } }
			//};
			return Task.FromResult ( _visibleMembers );
		}

		public Task<MemberList> GetAllMembers(Empty _)
		{
			return Task.FromResult(_allMembers);
		}

		public Task<OpSuccess> BroadcastString(StringMsg msg)
		{
			var memberList = _allMembers.PidList;
			foreach(var member in memberList)
			{
				member.Tell(msg);
			}
			return Task.FromResult(new OpSuccess() { Success = true });

		}
	}
}
