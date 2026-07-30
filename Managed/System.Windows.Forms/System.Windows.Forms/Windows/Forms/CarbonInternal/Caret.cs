using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004BD RID: 1213
	internal struct Caret
	{
		// Token: 0x0400297B RID: 10619
		internal Timer Timer;

		// Token: 0x0400297C RID: 10620
		internal IntPtr Hwnd;

		// Token: 0x0400297D RID: 10621
		internal int X;

		// Token: 0x0400297E RID: 10622
		internal int Y;

		// Token: 0x0400297F RID: 10623
		internal int Width;

		// Token: 0x04002980 RID: 10624
		internal int Height;

		// Token: 0x04002981 RID: 10625
		internal int Visible;

		// Token: 0x04002982 RID: 10626
		internal bool On;

		// Token: 0x04002983 RID: 10627
		internal bool Paused;
	}
}
