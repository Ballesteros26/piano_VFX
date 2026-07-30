using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000014 RID: 20
	public class Achievement : IAchievement
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00002FE0 File Offset: 0x000011E0
		public Achievement(string id, double percentCompleted, bool completed, bool hidden, DateTime lastReportedDate)
		{
			this.id = id;
			this.percentCompleted = percentCompleted;
			this.m_Completed = completed;
			this.m_Hidden = hidden;
			this.m_LastReportedDate = lastReportedDate;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003011 File Offset: 0x00001211
		public Achievement(string id, double percent)
		{
			this.id = id;
			this.percentCompleted = percent;
			this.m_Hidden = false;
			this.m_Completed = false;
			this.m_LastReportedDate = DateTime.MinValue;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003044 File Offset: 0x00001244
		public Achievement()
			: this("unknown", 0.0)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000305C File Offset: 0x0000125C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.id,
				" - ",
				this.percentCompleted,
				" - ",
				this.completed.ToString(),
				" - ",
				this.hidden.ToString(),
				" - ",
				this.lastReportedDate
			});
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000030E1 File Offset: 0x000012E1
		public void ReportProgress(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportProgress(this.id, this.percentCompleted, callback);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000030FC File Offset: 0x000012FC
		public void SetCompleted(bool value)
		{
			this.m_Completed = value;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003106 File Offset: 0x00001306
		public void SetHidden(bool value)
		{
			this.m_Hidden = value;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003110 File Offset: 0x00001310
		public void SetLastReportedDate(DateTime date)
		{
			this.m_LastReportedDate = date;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000311A File Offset: 0x0000131A
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003122 File Offset: 0x00001322
		public string id { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000312B File Offset: 0x0000132B
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003133 File Offset: 0x00001333
		public double percentCompleted { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000313C File Offset: 0x0000133C
		public bool completed
		{
			get
			{
				return this.m_Completed;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003154 File Offset: 0x00001354
		public bool hidden
		{
			get
			{
				return this.m_Hidden;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000316C File Offset: 0x0000136C
		public DateTime lastReportedDate
		{
			get
			{
				return this.m_LastReportedDate;
			}
		}

		// Token: 0x04000024 RID: 36
		private bool m_Completed;

		// Token: 0x04000025 RID: 37
		private bool m_Hidden;

		// Token: 0x04000026 RID: 38
		private DateTime m_LastReportedDate;
	}
}
