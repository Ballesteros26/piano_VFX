using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000012 RID: 18
	public class LocalUser : UserProfile, ILocalUser, IUserProfile
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002D48 File Offset: 0x00000F48
		public LocalUser()
		{
			IUserProfile[] array = new UserProfile[0];
			this.m_Friends = array;
			this.m_Authenticated = false;
			this.m_Underage = false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D79 File Offset: 0x00000F79
		public void Authenticate(Action<bool> callback)
		{
			ActivePlatform.Instance.Authenticate(this, callback);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D89 File Offset: 0x00000F89
		public void Authenticate(Action<bool, string> callback)
		{
			ActivePlatform.Instance.Authenticate(this, callback);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002D99 File Offset: 0x00000F99
		public void LoadFriends(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadFriends(this, callback);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public void SetFriends(IUserProfile[] friends)
		{
			this.m_Friends = friends;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002DB3 File Offset: 0x00000FB3
		public void SetAuthenticated(bool value)
		{
			this.m_Authenticated = value;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002DBD File Offset: 0x00000FBD
		public void SetUnderage(bool value)
		{
			this.m_Underage = value;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public IUserProfile[] friends
		{
			get
			{
				return this.m_Friends;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public bool authenticated
		{
			get
			{
				return this.m_Authenticated;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public bool underage
		{
			get
			{
				return this.m_Underage;
			}
		}

		// Token: 0x0400001B RID: 27
		private IUserProfile[] m_Friends;

		// Token: 0x0400001C RID: 28
		private bool m_Authenticated;

		// Token: 0x0400001D RID: 29
		private bool m_Underage;
	}
}
