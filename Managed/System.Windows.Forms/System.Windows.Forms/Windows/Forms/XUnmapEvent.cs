using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E5 RID: 997
	internal struct XUnmapEvent
	{
		// Token: 0x04001E7F RID: 7807
		internal XEventName type;

		// Token: 0x04001E80 RID: 7808
		internal IntPtr serial;

		// Token: 0x04001E81 RID: 7809
		internal bool send_event;

		// Token: 0x04001E82 RID: 7810
		internal IntPtr display;

		// Token: 0x04001E83 RID: 7811
		internal IntPtr xevent;

		// Token: 0x04001E84 RID: 7812
		internal IntPtr window;

		// Token: 0x04001E85 RID: 7813
		internal bool from_configure;
	}
}
