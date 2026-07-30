using System;

namespace System.Windows.Forms
{
	// Token: 0x020003F4 RID: 1012
	internal struct XClientMessageEvent
	{
		// Token: 0x04001EFF RID: 7935
		internal XEventName type;

		// Token: 0x04001F00 RID: 7936
		internal IntPtr serial;

		// Token: 0x04001F01 RID: 7937
		internal bool send_event;

		// Token: 0x04001F02 RID: 7938
		internal IntPtr display;

		// Token: 0x04001F03 RID: 7939
		internal IntPtr window;

		// Token: 0x04001F04 RID: 7940
		internal IntPtr message_type;

		// Token: 0x04001F05 RID: 7941
		internal int format;

		// Token: 0x04001F06 RID: 7942
		internal IntPtr ptr1;

		// Token: 0x04001F07 RID: 7943
		internal IntPtr ptr2;

		// Token: 0x04001F08 RID: 7944
		internal IntPtr ptr3;

		// Token: 0x04001F09 RID: 7945
		internal IntPtr ptr4;

		// Token: 0x04001F0A RID: 7946
		internal IntPtr ptr5;
	}
}
