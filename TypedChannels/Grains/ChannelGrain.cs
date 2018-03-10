using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat;


namespace ChatWithGrainsExperiment
{
	public class ChannelGrain : IChannelGrain
	{
		private List<string> _chatHistory = new List<string>(20);
		private List<string> _members = new List<string>(4);

		public Task<OpSuccess> Add(TargetUser target)
		{
			if (_members.Contains(target.User) == false)
			{
				_members.Add(target.User);
			}
			return Task.FromResult(new OpSuccess() { Success = true });
		}

		public Task<OpSuccess> Remove(TargetUser target)
		{
			if (_members.Contains(target.User) == true)
			{
				_members.Remove(target.User);
			}

			return Task.FromResult(new OpSuccess() { Success = true });
		}

		public Task<MemberList> GetMembers(Empty request)
		{
			return Task.FromResult ( new MemberList() { UserList = { _members.ToArray() } } );
		}

		public Task<OpSuccess> BroadcastChatMsg(ChannelMsg msg)
		{
			foreach (string member in _members)
			{
				// should be cached
				UserGrainClient userGrain = Grains.UserGrain("u:"+member);
				userGrain.TellChannelMsg(msg);
			}
			return Task.FromResult(new OpSuccess() { Success = true });
		}
	}
}
