using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000016 RID: 22
	public class Score : IScore
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000032FC File Offset: 0x000014FC
		public Score()
			: this("unkown", -1L)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000330D File Offset: 0x0000150D
		public Score(string leaderboardID, long value)
			: this(leaderboardID, value, "0", DateTime.Now, "", -1)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003329 File Offset: 0x00001529
		public Score(string leaderboardID, long value, string userID, DateTime date, string formattedValue, int rank)
		{
			this.leaderboardID = leaderboardID;
			this.value = value;
			this.m_UserID = userID;
			this.m_Date = date;
			this.m_FormattedValue = formattedValue;
			this.m_Rank = rank;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003364 File Offset: 0x00001564
		public override string ToString()
		{
			return string.Concat(new object[] { "Rank: '", this.m_Rank, "' Value: '", this.value, "' Category: '", this.leaderboardID, "' PlayerID: '", this.m_UserID, "' Date: '", this.m_Date });
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000033E7 File Offset: 0x000015E7
		public void ReportScore(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportScore(this.value, this.leaderboardID, callback);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003402 File Offset: 0x00001602
		public void SetDate(DateTime date)
		{
			this.m_Date = date;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000340C File Offset: 0x0000160C
		public void SetFormattedValue(string value)
		{
			this.m_FormattedValue = value;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003416 File Offset: 0x00001616
		public void SetUserID(string userID)
		{
			this.m_UserID = userID;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003420 File Offset: 0x00001620
		public void SetRank(int rank)
		{
			this.m_Rank = rank;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000342A File Offset: 0x0000162A
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003432 File Offset: 0x00001632
		public string leaderboardID { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000343B File Offset: 0x0000163B
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003443 File Offset: 0x00001643
		public long value { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000344C File Offset: 0x0000164C
		public DateTime date
		{
			get
			{
				return this.m_Date;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003464 File Offset: 0x00001664
		public string formattedValue
		{
			get
			{
				return this.m_FormattedValue;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000347C File Offset: 0x0000167C
		public string userID
		{
			get
			{
				return this.m_UserID;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003494 File Offset: 0x00001694
		public int rank
		{
			get
			{
				return this.m_Rank;
			}
		}

		// Token: 0x04000030 RID: 48
		private DateTime m_Date;

		// Token: 0x04000031 RID: 49
		private string m_FormattedValue;

		// Token: 0x04000032 RID: 50
		private string m_UserID;

		// Token: 0x04000033 RID: 51
		private int m_Rank;
	}
}
