using System;

namespace Mono.WebBrowser
{
	// Token: 0x0200000C RID: 12
	public class CreateNewWindowEventArgs : EventArgs
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000021D7 File Offset: 0x000003D7
		public CreateNewWindowEventArgs(bool isModal)
		{
			this.isModal = isModal;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000021E6 File Offset: 0x000003E6
		public bool IsModal
		{
			get
			{
				return this.isModal;
			}
		}

		// Token: 0x04000057 RID: 87
		private bool isModal;
	}
}
