using System;

namespace System.Windows.Forms
{
	// Token: 0x020003EA RID: 1002
	internal struct XGravityEvent
	{
		// Token: 0x04001EAA RID: 7850
		internal XEventName type;

		// Token: 0x04001EAB RID: 7851
		internal IntPtr serial;

		// Token: 0x04001EAC RID: 7852
		internal bool send_event;

		// Token: 0x04001EAD RID: 7853
		internal IntPtr display;

		// Token: 0x04001EAE RID: 7854
		internal IntPtr xevent;

		// Token: 0x04001EAF RID: 7855
		internal IntPtr window;

		// Token: 0x04001EB0 RID: 7856
		internal int x;

		// Token: 0x04001EB1 RID: 7857
		internal int y;
	}
}
