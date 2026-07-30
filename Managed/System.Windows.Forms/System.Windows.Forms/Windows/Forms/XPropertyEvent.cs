using System;

namespace System.Windows.Forms
{
	// Token: 0x020003EF RID: 1007
	internal struct XPropertyEvent
	{
		// Token: 0x04001ED5 RID: 7893
		internal XEventName type;

		// Token: 0x04001ED6 RID: 7894
		internal IntPtr serial;

		// Token: 0x04001ED7 RID: 7895
		internal bool send_event;

		// Token: 0x04001ED8 RID: 7896
		internal IntPtr display;

		// Token: 0x04001ED9 RID: 7897
		internal IntPtr window;

		// Token: 0x04001EDA RID: 7898
		internal IntPtr atom;

		// Token: 0x04001EDB RID: 7899
		internal IntPtr time;

		// Token: 0x04001EDC RID: 7900
		internal int state;
	}
}
