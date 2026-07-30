using System;

namespace System.Diagnostics
{
	// Token: 0x020001DA RID: 474
	internal class ProcessThreadTimes
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0004644E File Offset: 0x0004464E
		public DateTime StartTime
		{
			get
			{
				return DateTime.FromFileTime(this.create);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0004645B File Offset: 0x0004465B
		public DateTime ExitTime
		{
			get
			{
				return DateTime.FromFileTime(this.exit);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00046468 File Offset: 0x00044668
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(this.kernel);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00046475 File Offset: 0x00044675
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(this.user);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00046482 File Offset: 0x00044682
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(this.user + this.kernel);
			}
		}

		// Token: 0x040010EA RID: 4330
		internal long create;

		// Token: 0x040010EB RID: 4331
		internal long exit;

		// Token: 0x040010EC RID: 4332
		internal long kernel;

		// Token: 0x040010ED RID: 4333
		internal long user;
	}
}
