using System;

namespace Mono.WebBrowser
{
	// Token: 0x02000018 RID: 24
	public class LoadFinishedEventArgs : EventArgs
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000023C9 File Offset: 0x000005C9
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000023D1 File Offset: 0x000005D1
		public LoadFinishedEventArgs(string uri)
		{
			this.uri = uri;
		}

		// Token: 0x0400006B RID: 107
		private string uri;
	}
}
