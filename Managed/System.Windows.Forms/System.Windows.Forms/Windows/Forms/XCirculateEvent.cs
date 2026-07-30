using System;

namespace System.Windows.Forms
{
	// Token: 0x020003ED RID: 1005
	internal struct XCirculateEvent
	{
		// Token: 0x04001EC7 RID: 7879
		internal XEventName type;

		// Token: 0x04001EC8 RID: 7880
		internal IntPtr serial;

		// Token: 0x04001EC9 RID: 7881
		internal bool send_event;

		// Token: 0x04001ECA RID: 7882
		internal IntPtr display;

		// Token: 0x04001ECB RID: 7883
		internal IntPtr xevent;

		// Token: 0x04001ECC RID: 7884
		internal IntPtr window;

		// Token: 0x04001ECD RID: 7885
		internal int place;
	}
}
