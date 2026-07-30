using System;

namespace System.Windows.Forms
{
	// Token: 0x020003DA RID: 986
	internal struct XButtonEvent
	{
		// Token: 0x04001DEF RID: 7663
		internal XEventName type;

		// Token: 0x04001DF0 RID: 7664
		internal IntPtr serial;

		// Token: 0x04001DF1 RID: 7665
		internal bool send_event;

		// Token: 0x04001DF2 RID: 7666
		internal IntPtr display;

		// Token: 0x04001DF3 RID: 7667
		internal IntPtr window;

		// Token: 0x04001DF4 RID: 7668
		internal IntPtr root;

		// Token: 0x04001DF5 RID: 7669
		internal IntPtr subwindow;

		// Token: 0x04001DF6 RID: 7670
		internal IntPtr time;

		// Token: 0x04001DF7 RID: 7671
		internal int x;

		// Token: 0x04001DF8 RID: 7672
		internal int y;

		// Token: 0x04001DF9 RID: 7673
		internal int x_root;

		// Token: 0x04001DFA RID: 7674
		internal int y_root;

		// Token: 0x04001DFB RID: 7675
		internal int state;

		// Token: 0x04001DFC RID: 7676
		internal int button;

		// Token: 0x04001DFD RID: 7677
		internal bool same_screen;
	}
}
