using System;

namespace Ookii.Dialogs
{
	// Token: 0x0200000D RID: 13
	public class HyperlinkClickedEventArgs : EventArgs
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003FE4 File Offset: 0x000021E4
		public HyperlinkClickedEventArgs(string href)
		{
			this._href = href;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003FF8 File Offset: 0x000021F8
		public string Href
		{
			get
			{
				return this._href;
			}
		}

		// Token: 0x04000029 RID: 41
		private string _href;
	}
}
