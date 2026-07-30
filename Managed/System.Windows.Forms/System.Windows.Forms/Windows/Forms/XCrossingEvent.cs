using System;

namespace System.Windows.Forms
{
	// Token: 0x020003DC RID: 988
	internal struct XCrossingEvent
	{
		// Token: 0x04001E0D RID: 7693
		internal XEventName type;

		// Token: 0x04001E0E RID: 7694
		internal IntPtr serial;

		// Token: 0x04001E0F RID: 7695
		internal bool send_event;

		// Token: 0x04001E10 RID: 7696
		internal IntPtr display;

		// Token: 0x04001E11 RID: 7697
		internal IntPtr window;

		// Token: 0x04001E12 RID: 7698
		internal IntPtr root;

		// Token: 0x04001E13 RID: 7699
		internal IntPtr subwindow;

		// Token: 0x04001E14 RID: 7700
		internal IntPtr time;

		// Token: 0x04001E15 RID: 7701
		internal int x;

		// Token: 0x04001E16 RID: 7702
		internal int y;

		// Token: 0x04001E17 RID: 7703
		internal int x_root;

		// Token: 0x04001E18 RID: 7704
		internal int y_root;

		// Token: 0x04001E19 RID: 7705
		internal NotifyMode mode;

		// Token: 0x04001E1A RID: 7706
		internal NotifyDetail detail;

		// Token: 0x04001E1B RID: 7707
		internal bool same_screen;

		// Token: 0x04001E1C RID: 7708
		internal bool focus;

		// Token: 0x04001E1D RID: 7709
		internal int state;
	}
}
