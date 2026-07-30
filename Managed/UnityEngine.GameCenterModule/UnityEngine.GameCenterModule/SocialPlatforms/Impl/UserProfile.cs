using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000013 RID: 19
	public class UserProfile : IUserProfile
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002E10 File Offset: 0x00001010
		public UserProfile()
		{
			this.m_UserName = "Uninitialized";
			this.m_teamID = "0";
			this.m_IsFriend = false;
			this.m_State = UserState.Offline;
			this.m_Image = new Texture2D(32, 32);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002E4D File Offset: 0x0000104D
		public UserProfile(string name, string id, bool friend)
			: this(name, id, friend, UserState.Offline, new Texture2D(0, 0))
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002E62 File Offset: 0x00001062
		public UserProfile(string name, string id, bool friend, UserState state, Texture2D image)
			: this(name, id, id, friend, state, image)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002E74 File Offset: 0x00001074
		public UserProfile(string name, string teamId, string gameId, bool friend, UserState state, Texture2D image)
		{
			this.m_UserName = name;
			this.m_teamID = teamId;
			this.m_gameID = gameId;
			this.m_IsFriend = friend;
			this.m_State = state;
			this.m_Image = image;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002EAC File Offset: 0x000010AC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.id,
				" - ",
				this.userName,
				" - ",
				this.isFriend.ToString(),
				" - ",
				this.state
			});
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002F12 File Offset: 0x00001112
		public void SetUserName(string name)
		{
			this.m_UserName = name;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002F1C File Offset: 0x0000111C
		public void SetUserID(string id)
		{
			this.m_teamID = id;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002F26 File Offset: 0x00001126
		public void SetUserGameID(string id)
		{
			this.m_gameID = id;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002F30 File Offset: 0x00001130
		public void SetImage(Texture2D image)
		{
			this.m_Image = image;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002F3A File Offset: 0x0000113A
		public void SetIsFriend(bool value)
		{
			this.m_IsFriend = value;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002F44 File Offset: 0x00001144
		public void SetState(UserState state)
		{
			this.m_State = state;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002F50 File Offset: 0x00001150
		public string userName
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002F68 File Offset: 0x00001168
		public string id
		{
			get
			{
				return this.m_teamID;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002F80 File Offset: 0x00001180
		public string gameId
		{
			get
			{
				return this.m_gameID;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002F98 File Offset: 0x00001198
		public bool isFriend
		{
			get
			{
				return this.m_IsFriend;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002FB0 File Offset: 0x000011B0
		public UserState state
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002FC8 File Offset: 0x000011C8
		public Texture2D image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x0400001E RID: 30
		protected string m_UserName;

		// Token: 0x0400001F RID: 31
		protected string m_teamID;

		// Token: 0x04000020 RID: 32
		protected string m_gameID;

		// Token: 0x04000021 RID: 33
		protected bool m_IsFriend;

		// Token: 0x04000022 RID: 34
		protected UserState m_State;

		// Token: 0x04000023 RID: 35
		protected Texture2D m_Image;
	}
}
