using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000017 RID: 23
	public class Leaderboard : ILeaderboard
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000034AC File Offset: 0x000016AC
		public Leaderboard()
		{
			this.id = "Invalid";
			this.range = new Range(1, 10);
			this.userScope = UserScope.Global;
			this.timeScope = TimeScope.AllTime;
			this.m_Loading = false;
			this.m_LocalUserScore = new Score("Invalid", 0L);
			this.m_MaxRange = 0U;
			IScore[] array = new Score[0];
			this.m_Scores = array;
			this.m_Title = "Invalid";
			this.m_UserIDs = new string[0];
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003531 File Offset: 0x00001731
		public void SetUserFilter(string[] userIDs)
		{
			this.m_UserIDs = userIDs;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000353C File Offset: 0x0000173C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ID: '",
				this.id,
				"' Title: '",
				this.m_Title,
				"' Loading: '",
				this.m_Loading.ToString(),
				"' Range: [",
				this.range.from,
				",",
				this.range.count,
				"] MaxRange: '",
				this.m_MaxRange,
				"' Scores: '",
				this.m_Scores.Length,
				"' UserScope: '",
				this.userScope,
				"' TimeScope: '",
				this.timeScope,
				"' UserFilter: '",
				this.m_UserIDs.Length
			});
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003645 File Offset: 0x00001845
		public void LoadScores(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadScores(this, callback);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003658 File Offset: 0x00001858
		public bool loading
		{
			get
			{
				return ActivePlatform.Instance.GetLoading(this);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003675 File Offset: 0x00001875
		public void SetLocalUserScore(IScore score)
		{
			this.m_LocalUserScore = score;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000367F File Offset: 0x0000187F
		public void SetMaxRange(uint maxRange)
		{
			this.m_MaxRange = maxRange;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003689 File Offset: 0x00001889
		public void SetScores(IScore[] scores)
		{
			this.m_Scores = scores;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003693 File Offset: 0x00001893
		public void SetTitle(string title)
		{
			this.m_Title = title;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000036A0 File Offset: 0x000018A0
		public string[] GetUserFilter()
		{
			return this.m_UserIDs;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000036B8 File Offset: 0x000018B8
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000036C0 File Offset: 0x000018C0
		public string id { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000036C9 File Offset: 0x000018C9
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000036D1 File Offset: 0x000018D1
		public UserScope userScope { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000036DA File Offset: 0x000018DA
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000036E2 File Offset: 0x000018E2
		public Range range { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000036EB File Offset: 0x000018EB
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000036F3 File Offset: 0x000018F3
		public TimeScope timeScope { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000036FC File Offset: 0x000018FC
		public IScore localUserScore
		{
			get
			{
				return this.m_LocalUserScore;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003714 File Offset: 0x00001914
		public uint maxRange
		{
			get
			{
				return this.m_MaxRange;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000372C File Offset: 0x0000192C
		public IScore[] scores
		{
			get
			{
				return this.m_Scores;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003744 File Offset: 0x00001944
		public string title
		{
			get
			{
				return this.m_Title;
			}
		}

		// Token: 0x04000036 RID: 54
		private bool m_Loading;

		// Token: 0x04000037 RID: 55
		private IScore m_LocalUserScore;

		// Token: 0x04000038 RID: 56
		private uint m_MaxRange;

		// Token: 0x04000039 RID: 57
		private IScore[] m_Scores;

		// Token: 0x0400003A RID: 58
		private string m_Title;

		// Token: 0x0400003B RID: 59
		private string[] m_UserIDs;
	}
}
