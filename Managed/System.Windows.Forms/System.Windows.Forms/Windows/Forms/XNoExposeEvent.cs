using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E1 RID: 993
	internal struct XNoExposeEvent
	{
		// Token: 0x04001E60 RID: 7776
		internal XEventName type;

		// Token: 0x04001E61 RID: 7777
		internal IntPtr serial;

		// Token: 0x04001E62 RID: 7778
		internal bool send_event;

		// Token: 0x04001E63 RID: 7779
		internal IntPtr display;

		// Token: 0x04001E64 RID: 7780
		internal IntPtr drawable;

		// Token: 0x04001E65 RID: 7781
		internal int major_code;

		// Token: 0x04001E66 RID: 7782
		internal int minor_code;
	}
}
