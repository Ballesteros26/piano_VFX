using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E3 RID: 995
	internal struct XCreateWindowEvent
	{
		// Token: 0x04001E6D RID: 7789
		internal XEventName type;

		// Token: 0x04001E6E RID: 7790
		internal IntPtr serial;

		// Token: 0x04001E6F RID: 7791
		internal bool send_event;

		// Token: 0x04001E70 RID: 7792
		internal IntPtr display;

		// Token: 0x04001E71 RID: 7793
		internal IntPtr parent;

		// Token: 0x04001E72 RID: 7794
		internal IntPtr window;

		// Token: 0x04001E73 RID: 7795
		internal int x;

		// Token: 0x04001E74 RID: 7796
		internal int y;

		// Token: 0x04001E75 RID: 7797
		internal int width;

		// Token: 0x04001E76 RID: 7798
		internal int height;

		// Token: 0x04001E77 RID: 7799
		internal int border_width;

		// Token: 0x04001E78 RID: 7800
		internal bool override_redirect;
	}
}
