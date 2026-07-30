using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E8 RID: 1000
	internal struct XReparentEvent
	{
		// Token: 0x04001E93 RID: 7827
		internal XEventName type;

		// Token: 0x04001E94 RID: 7828
		internal IntPtr serial;

		// Token: 0x04001E95 RID: 7829
		internal bool send_event;

		// Token: 0x04001E96 RID: 7830
		internal IntPtr display;

		// Token: 0x04001E97 RID: 7831
		internal IntPtr xevent;

		// Token: 0x04001E98 RID: 7832
		internal IntPtr window;

		// Token: 0x04001E99 RID: 7833
		internal IntPtr parent;

		// Token: 0x04001E9A RID: 7834
		internal int x;

		// Token: 0x04001E9B RID: 7835
		internal int y;

		// Token: 0x04001E9C RID: 7836
		internal bool override_redirect;
	}
}
