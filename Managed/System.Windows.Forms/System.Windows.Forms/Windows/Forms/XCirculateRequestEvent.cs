using System;

namespace System.Windows.Forms
{
	// Token: 0x020003EE RID: 1006
	internal struct XCirculateRequestEvent
	{
		// Token: 0x04001ECE RID: 7886
		internal XEventName type;

		// Token: 0x04001ECF RID: 7887
		internal IntPtr serial;

		// Token: 0x04001ED0 RID: 7888
		internal bool send_event;

		// Token: 0x04001ED1 RID: 7889
		internal IntPtr display;

		// Token: 0x04001ED2 RID: 7890
		internal IntPtr parent;

		// Token: 0x04001ED3 RID: 7891
		internal IntPtr window;

		// Token: 0x04001ED4 RID: 7892
		internal int place;
	}
}
