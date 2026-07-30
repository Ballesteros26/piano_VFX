using System;
using System.ComponentModel;

namespace Mono.WebBrowser
{
	// Token: 0x0200001E RID: 30
	public class NavigationRequestedEventArgs : CancelEventArgs
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002426 File Offset: 0x00000626
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000242E File Offset: 0x0000062E
		public NavigationRequestedEventArgs(string uri)
		{
			this.uri = uri;
		}

		// Token: 0x0400006F RID: 111
		private string uri;
	}
}
