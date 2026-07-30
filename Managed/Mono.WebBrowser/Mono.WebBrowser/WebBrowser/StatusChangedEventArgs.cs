using System;

namespace Mono.WebBrowser
{
	// Token: 0x02000010 RID: 16
	public class StatusChangedEventArgs : EventArgs
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000232E File Offset: 0x0000052E
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002336 File Offset: 0x00000536
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000233F File Offset: 0x0000053F
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002347 File Offset: 0x00000547
		public int Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002350 File Offset: 0x00000550
		public StatusChangedEventArgs(string message, int status)
		{
			this.message = message;
			this.status = status;
		}

		// Token: 0x04000064 RID: 100
		private string message;

		// Token: 0x04000065 RID: 101
		private int status;
	}
}
