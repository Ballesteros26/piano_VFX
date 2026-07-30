using System;

namespace System.Windows.Forms
{
	// Token: 0x020003DF RID: 991
	internal struct XExposeEvent
	{
		// Token: 0x04001E4A RID: 7754
		internal XEventName type;

		// Token: 0x04001E4B RID: 7755
		internal IntPtr serial;

		// Token: 0x04001E4C RID: 7756
		internal bool send_event;

		// Token: 0x04001E4D RID: 7757
		internal IntPtr display;

		// Token: 0x04001E4E RID: 7758
		internal IntPtr window;

		// Token: 0x04001E4F RID: 7759
		internal int x;

		// Token: 0x04001E50 RID: 7760
		internal int y;

		// Token: 0x04001E51 RID: 7761
		internal int width;

		// Token: 0x04001E52 RID: 7762
		internal int height;

		// Token: 0x04001E53 RID: 7763
		internal int count;
	}
}
