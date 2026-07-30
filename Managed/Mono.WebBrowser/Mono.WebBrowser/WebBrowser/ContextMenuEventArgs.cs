using System;

namespace Mono.WebBrowser
{
	// Token: 0x0200001C RID: 28
	public class ContextMenuEventArgs : EventArgs
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002400 File Offset: 0x00000600
		public int X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002408 File Offset: 0x00000608
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002410 File Offset: 0x00000610
		public ContextMenuEventArgs(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x0400006D RID: 109
		private int x;

		// Token: 0x0400006E RID: 110
		private int y;
	}
}
