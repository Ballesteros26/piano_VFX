using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E6 RID: 998
	internal struct XMapEvent
	{
		// Token: 0x04001E86 RID: 7814
		internal XEventName type;

		// Token: 0x04001E87 RID: 7815
		internal IntPtr serial;

		// Token: 0x04001E88 RID: 7816
		internal bool send_event;

		// Token: 0x04001E89 RID: 7817
		internal IntPtr display;

		// Token: 0x04001E8A RID: 7818
		internal IntPtr xevent;

		// Token: 0x04001E8B RID: 7819
		internal IntPtr window;

		// Token: 0x04001E8C RID: 7820
		internal bool override_redirect;
	}
}
