using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000021 RID: 33
	public class WindowEventArgs : EventArgs
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00002517 File Offset: 0x00000717
		public WindowEventArgs(IWindow window)
		{
			this.window = window;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002526 File Offset: 0x00000726
		public IWindow Window
		{
			get
			{
				return this.window;
			}
		}

		// Token: 0x04000071 RID: 113
		private IWindow window;
	}
}
