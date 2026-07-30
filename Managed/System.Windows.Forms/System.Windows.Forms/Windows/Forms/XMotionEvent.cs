using System;

namespace System.Windows.Forms
{
	// Token: 0x020003DB RID: 987
	internal struct XMotionEvent
	{
		// Token: 0x04001DFE RID: 7678
		internal XEventName type;

		// Token: 0x04001DFF RID: 7679
		internal IntPtr serial;

		// Token: 0x04001E00 RID: 7680
		internal bool send_event;

		// Token: 0x04001E01 RID: 7681
		internal IntPtr display;

		// Token: 0x04001E02 RID: 7682
		internal IntPtr window;

		// Token: 0x04001E03 RID: 7683
		internal IntPtr root;

		// Token: 0x04001E04 RID: 7684
		internal IntPtr subwindow;

		// Token: 0x04001E05 RID: 7685
		internal IntPtr time;

		// Token: 0x04001E06 RID: 7686
		internal int x;

		// Token: 0x04001E07 RID: 7687
		internal int y;

		// Token: 0x04001E08 RID: 7688
		internal int x_root;

		// Token: 0x04001E09 RID: 7689
		internal int y_root;

		// Token: 0x04001E0A RID: 7690
		internal int state;

		// Token: 0x04001E0B RID: 7691
		internal byte is_hint;

		// Token: 0x04001E0C RID: 7692
		internal bool same_screen;
	}
}
