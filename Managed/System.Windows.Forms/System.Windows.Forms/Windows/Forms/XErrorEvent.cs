using System;

namespace System.Windows.Forms
{
	// Token: 0x020003F6 RID: 1014
	internal struct XErrorEvent
	{
		// Token: 0x04001F13 RID: 7955
		internal XEventName type;

		// Token: 0x04001F14 RID: 7956
		internal IntPtr display;

		// Token: 0x04001F15 RID: 7957
		internal IntPtr resourceid;

		// Token: 0x04001F16 RID: 7958
		internal IntPtr serial;

		// Token: 0x04001F17 RID: 7959
		internal byte error_code;

		// Token: 0x04001F18 RID: 7960
		internal XRequest request_code;

		// Token: 0x04001F19 RID: 7961
		internal byte minor_code;
	}
}
