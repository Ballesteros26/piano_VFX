using System;

namespace System.Windows.Forms
{
	// Token: 0x020003D9 RID: 985
	internal struct XKeyEvent
	{
		// Token: 0x04001DE0 RID: 7648
		internal XEventName type;

		// Token: 0x04001DE1 RID: 7649
		internal IntPtr serial;

		// Token: 0x04001DE2 RID: 7650
		internal bool send_event;

		// Token: 0x04001DE3 RID: 7651
		internal IntPtr display;

		// Token: 0x04001DE4 RID: 7652
		internal IntPtr window;

		// Token: 0x04001DE5 RID: 7653
		internal IntPtr root;

		// Token: 0x04001DE6 RID: 7654
		internal IntPtr subwindow;

		// Token: 0x04001DE7 RID: 7655
		internal IntPtr time;

		// Token: 0x04001DE8 RID: 7656
		internal int x;

		// Token: 0x04001DE9 RID: 7657
		internal int y;

		// Token: 0x04001DEA RID: 7658
		internal int x_root;

		// Token: 0x04001DEB RID: 7659
		internal int y_root;

		// Token: 0x04001DEC RID: 7660
		internal int state;

		// Token: 0x04001DED RID: 7661
		internal int keycode;

		// Token: 0x04001DEE RID: 7662
		internal bool same_screen;
	}
}
