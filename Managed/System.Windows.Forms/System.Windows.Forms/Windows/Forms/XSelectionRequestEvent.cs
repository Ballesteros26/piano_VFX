using System;

namespace System.Windows.Forms
{
	// Token: 0x020003F1 RID: 1009
	internal struct XSelectionRequestEvent
	{
		// Token: 0x04001EE4 RID: 7908
		internal XEventName type;

		// Token: 0x04001EE5 RID: 7909
		internal IntPtr serial;

		// Token: 0x04001EE6 RID: 7910
		internal bool send_event;

		// Token: 0x04001EE7 RID: 7911
		internal IntPtr display;

		// Token: 0x04001EE8 RID: 7912
		internal IntPtr owner;

		// Token: 0x04001EE9 RID: 7913
		internal IntPtr requestor;

		// Token: 0x04001EEA RID: 7914
		internal IntPtr selection;

		// Token: 0x04001EEB RID: 7915
		internal IntPtr target;

		// Token: 0x04001EEC RID: 7916
		internal IntPtr property;

		// Token: 0x04001EED RID: 7917
		internal IntPtr time;
	}
}
