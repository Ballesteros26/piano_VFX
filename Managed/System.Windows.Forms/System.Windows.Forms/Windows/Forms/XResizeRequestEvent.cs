using System;

namespace System.Windows.Forms
{
	// Token: 0x020003EB RID: 1003
	internal struct XResizeRequestEvent
	{
		// Token: 0x04001EB2 RID: 7858
		internal XEventName type;

		// Token: 0x04001EB3 RID: 7859
		internal IntPtr serial;

		// Token: 0x04001EB4 RID: 7860
		internal bool send_event;

		// Token: 0x04001EB5 RID: 7861
		internal IntPtr display;

		// Token: 0x04001EB6 RID: 7862
		internal IntPtr window;

		// Token: 0x04001EB7 RID: 7863
		internal int width;

		// Token: 0x04001EB8 RID: 7864
		internal int height;
	}
}
