using System;

namespace System.Windows.Forms
{
	// Token: 0x020003EC RID: 1004
	internal struct XConfigureRequestEvent
	{
		// Token: 0x04001EB9 RID: 7865
		internal XEventName type;

		// Token: 0x04001EBA RID: 7866
		internal IntPtr serial;

		// Token: 0x04001EBB RID: 7867
		internal bool send_event;

		// Token: 0x04001EBC RID: 7868
		internal IntPtr display;

		// Token: 0x04001EBD RID: 7869
		internal IntPtr parent;

		// Token: 0x04001EBE RID: 7870
		internal IntPtr window;

		// Token: 0x04001EBF RID: 7871
		internal int x;

		// Token: 0x04001EC0 RID: 7872
		internal int y;

		// Token: 0x04001EC1 RID: 7873
		internal int width;

		// Token: 0x04001EC2 RID: 7874
		internal int height;

		// Token: 0x04001EC3 RID: 7875
		internal int border_width;

		// Token: 0x04001EC4 RID: 7876
		internal IntPtr above;

		// Token: 0x04001EC5 RID: 7877
		internal int detail;

		// Token: 0x04001EC6 RID: 7878
		internal IntPtr value_mask;
	}
}
