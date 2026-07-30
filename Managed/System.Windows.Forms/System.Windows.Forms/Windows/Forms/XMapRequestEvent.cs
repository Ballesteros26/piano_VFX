using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E7 RID: 999
	internal struct XMapRequestEvent
	{
		// Token: 0x04001E8D RID: 7821
		internal XEventName type;

		// Token: 0x04001E8E RID: 7822
		internal IntPtr serial;

		// Token: 0x04001E8F RID: 7823
		internal bool send_event;

		// Token: 0x04001E90 RID: 7824
		internal IntPtr display;

		// Token: 0x04001E91 RID: 7825
		internal IntPtr parent;

		// Token: 0x04001E92 RID: 7826
		internal IntPtr window;
	}
}
