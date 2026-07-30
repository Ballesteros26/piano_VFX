using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000008 RID: 8
	public interface ILocalUser : IUserProfile
	{
		// Token: 0x0600003D RID: 61
		void Authenticate(Action<bool> callback);

		// Token: 0x0600003E RID: 62
		void Authenticate(Action<bool, string> callback);

		// Token: 0x0600003F RID: 63
		void LoadFriends(Action<bool> callback);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000040 RID: 64
		IUserProfile[] friends { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000041 RID: 65
		bool authenticated { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000042 RID: 66
		bool underage { get; }
	}
}
