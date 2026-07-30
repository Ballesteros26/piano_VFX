using System;

namespace Mono.WebBrowser
{
	// Token: 0x02000016 RID: 22
	public class LoadCommitedEventArgs : EventArgs
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000023B2 File Offset: 0x000005B2
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000023BA File Offset: 0x000005BA
		public LoadCommitedEventArgs(string uri)
		{
			this.uri = uri;
		}

		// Token: 0x0400006A RID: 106
		private string uri;
	}
}
