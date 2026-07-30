using System;

namespace System.Windows.Forms
{
	// Token: 0x020003F2 RID: 1010
	internal struct XSelectionEvent
	{
		// Token: 0x04001EEE RID: 7918
		internal XEventName type;

		// Token: 0x04001EEF RID: 7919
		internal IntPtr serial;

		// Token: 0x04001EF0 RID: 7920
		internal bool send_event;

		// Token: 0x04001EF1 RID: 7921
		internal IntPtr display;

		// Token: 0x04001EF2 RID: 7922
		internal IntPtr requestor;

		// Token: 0x04001EF3 RID: 7923
		internal IntPtr selection;

		// Token: 0x04001EF4 RID: 7924
		internal IntPtr target;

		// Token: 0x04001EF5 RID: 7925
		internal IntPtr property;

		// Token: 0x04001EF6 RID: 7926
		internal IntPtr time;
	}
}
