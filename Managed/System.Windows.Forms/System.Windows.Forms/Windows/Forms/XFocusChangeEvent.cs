using System;

namespace System.Windows.Forms
{
	// Token: 0x020003DD RID: 989
	internal struct XFocusChangeEvent
	{
		// Token: 0x04001E1E RID: 7710
		internal XEventName type;

		// Token: 0x04001E1F RID: 7711
		internal IntPtr serial;

		// Token: 0x04001E20 RID: 7712
		internal bool send_event;

		// Token: 0x04001E21 RID: 7713
		internal IntPtr display;

		// Token: 0x04001E22 RID: 7714
		internal IntPtr window;

		// Token: 0x04001E23 RID: 7715
		internal int mode;

		// Token: 0x04001E24 RID: 7716
		internal NotifyDetail detail;
	}
}
