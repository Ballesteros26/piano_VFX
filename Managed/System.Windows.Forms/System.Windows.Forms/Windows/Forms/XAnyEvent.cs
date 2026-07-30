using System;

namespace System.Windows.Forms
{
	// Token: 0x020003D8 RID: 984
	internal struct XAnyEvent
	{
		// Token: 0x04001DDB RID: 7643
		internal XEventName type;

		// Token: 0x04001DDC RID: 7644
		internal IntPtr serial;

		// Token: 0x04001DDD RID: 7645
		internal bool send_event;

		// Token: 0x04001DDE RID: 7646
		internal IntPtr display;

		// Token: 0x04001DDF RID: 7647
		internal IntPtr window;
	}
}
