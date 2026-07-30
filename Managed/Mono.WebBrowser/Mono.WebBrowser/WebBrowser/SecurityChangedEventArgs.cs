using System;

namespace Mono.WebBrowser
{
	// Token: 0x0200001A RID: 26
	public class SecurityChangedEventArgs : EventArgs
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000023E0 File Offset: 0x000005E0
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000023E8 File Offset: 0x000005E8
		public SecurityLevel State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000023F1 File Offset: 0x000005F1
		public SecurityChangedEventArgs(SecurityLevel state)
		{
			this.state = state;
		}

		// Token: 0x0400006C RID: 108
		private SecurityLevel state;
	}
}
