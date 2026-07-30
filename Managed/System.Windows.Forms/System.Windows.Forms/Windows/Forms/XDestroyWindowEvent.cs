using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E4 RID: 996
	internal struct XDestroyWindowEvent
	{
		// Token: 0x04001E79 RID: 7801
		internal XEventName type;

		// Token: 0x04001E7A RID: 7802
		internal IntPtr serial;

		// Token: 0x04001E7B RID: 7803
		internal bool send_event;

		// Token: 0x04001E7C RID: 7804
		internal IntPtr display;

		// Token: 0x04001E7D RID: 7805
		internal IntPtr xevent;

		// Token: 0x04001E7E RID: 7806
		internal IntPtr window;
	}
}
