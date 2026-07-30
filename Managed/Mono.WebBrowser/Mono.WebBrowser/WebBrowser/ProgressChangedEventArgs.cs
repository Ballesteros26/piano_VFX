using System;

namespace Mono.WebBrowser
{
	// Token: 0x02000012 RID: 18
	public class ProgressChangedEventArgs : EventArgs
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002366 File Offset: 0x00000566
		public int Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000236E File Offset: 0x0000056E
		public int MaxProgress
		{
			get
			{
				return this.maxProgress;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002376 File Offset: 0x00000576
		public ProgressChangedEventArgs(int progress, int maxProgress)
		{
			this.progress = progress;
			this.maxProgress = maxProgress;
		}

		// Token: 0x04000066 RID: 102
		private int progress;

		// Token: 0x04000067 RID: 103
		private int maxProgress;
	}
}
