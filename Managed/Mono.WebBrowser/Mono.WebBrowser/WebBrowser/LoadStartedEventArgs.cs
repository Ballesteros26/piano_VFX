using System;
using System.ComponentModel;

namespace Mono.WebBrowser
{
	// Token: 0x02000014 RID: 20
	public class LoadStartedEventArgs : CancelEventArgs
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000238C File Offset: 0x0000058C
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002394 File Offset: 0x00000594
		public string FrameName
		{
			get
			{
				return this.frameName;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000239C File Offset: 0x0000059C
		public LoadStartedEventArgs(string uri, string frameName)
		{
			this.uri = uri;
			this.frameName = frameName;
		}

		// Token: 0x04000068 RID: 104
		private string uri;

		// Token: 0x04000069 RID: 105
		private string frameName;
	}
}
