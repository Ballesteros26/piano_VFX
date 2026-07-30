using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E9 RID: 1001
	internal struct XConfigureEvent
	{
		// Token: 0x04001E9D RID: 7837
		internal XEventName type;

		// Token: 0x04001E9E RID: 7838
		internal IntPtr serial;

		// Token: 0x04001E9F RID: 7839
		internal bool send_event;

		// Token: 0x04001EA0 RID: 7840
		internal IntPtr display;

		// Token: 0x04001EA1 RID: 7841
		internal IntPtr xevent;

		// Token: 0x04001EA2 RID: 7842
		internal IntPtr window;

		// Token: 0x04001EA3 RID: 7843
		internal int x;

		// Token: 0x04001EA4 RID: 7844
		internal int y;

		// Token: 0x04001EA5 RID: 7845
		internal int width;

		// Token: 0x04001EA6 RID: 7846
		internal int height;

		// Token: 0x04001EA7 RID: 7847
		internal int border_width;

		// Token: 0x04001EA8 RID: 7848
		internal IntPtr above;

		// Token: 0x04001EA9 RID: 7849
		internal bool override_redirect;
	}
}
