using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E0 RID: 992
	internal struct XGraphicsExposeEvent
	{
		// Token: 0x04001E54 RID: 7764
		internal XEventName type;

		// Token: 0x04001E55 RID: 7765
		internal IntPtr serial;

		// Token: 0x04001E56 RID: 7766
		internal bool send_event;

		// Token: 0x04001E57 RID: 7767
		internal IntPtr display;

		// Token: 0x04001E58 RID: 7768
		internal IntPtr drawable;

		// Token: 0x04001E59 RID: 7769
		internal int x;

		// Token: 0x04001E5A RID: 7770
		internal int y;

		// Token: 0x04001E5B RID: 7771
		internal int width;

		// Token: 0x04001E5C RID: 7772
		internal int height;

		// Token: 0x04001E5D RID: 7773
		internal int count;

		// Token: 0x04001E5E RID: 7774
		internal int major_code;

		// Token: 0x04001E5F RID: 7775
		internal int minor_code;
	}
}
