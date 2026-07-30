using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E2 RID: 994
	internal struct XVisibilityEvent
	{
		// Token: 0x04001E67 RID: 7783
		internal XEventName type;

		// Token: 0x04001E68 RID: 7784
		internal IntPtr serial;

		// Token: 0x04001E69 RID: 7785
		internal bool send_event;

		// Token: 0x04001E6A RID: 7786
		internal IntPtr display;

		// Token: 0x04001E6B RID: 7787
		internal IntPtr window;

		// Token: 0x04001E6C RID: 7788
		internal int state;
	}
}
