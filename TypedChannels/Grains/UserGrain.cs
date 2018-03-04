using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat;


namespace ChatWithGrainsExperiment
{
	public class UserGrain : IUserGrain
	{
		public Task<OpSuccess> JoinChannel(TargetChannel request)
		{
			throw new NotImplementedException();
		}

		public Task<OpSuccess> LeaveChannel(TargetChannel request)
		{
			throw new NotImplementedException();
		}

		public Task<Empty> TellChannelMsg(ChannelMsg request)
		{
			throw new NotImplementedException();
		}

		public Task<Empty> TellSomeoneJoinedChannel(UserJoinedUpdate request)
		{
			throw new NotImplementedException();
		}

		public Task<Empty> TellSomeoneLeftChannel(UserLeftUpdate request)
		{
			throw new NotImplementedException();
		}
	}
}
