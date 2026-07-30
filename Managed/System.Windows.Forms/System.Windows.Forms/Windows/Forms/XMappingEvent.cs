using System;

namespace System.Windows.Forms
{
	// Token: 0x020003F5 RID: 1013
	internal struct XMappingEvent
	{
		// Token: 0x04001F0B RID: 7947
		internal XEventName type;

		// Token: 0x04001F0C RID: 7948
		internal IntPtr serial;

		// Token: 0x04001F0D RID: 7949
		internal bool send_event;

		// Token: 0x04001F0E RID: 7950
		internal IntPtr display;

		// Token: 0x04001F0F RID: 7951
		internal IntPtr window;

		// Token: 0x04001F10 RID: 7952
		internal int request;

		// Token: 0x04001F11 RID: 7953
		internal int first_keycode;

		// Token: 0x04001F12 RID: 7954
		internal int count;
	}
}
