using System;

namespace System.Windows.Forms
{
	// Token: 0x020003F9 RID: 1017
	internal struct XSetWindowAttributes
	{
		// Token: 0x04001F53 RID: 8019
		internal IntPtr background_pixmap;

		// Token: 0x04001F54 RID: 8020
		internal IntPtr background_pixel;

		// Token: 0x04001F55 RID: 8021
		internal IntPtr border_pixmap;

		// Token: 0x04001F56 RID: 8022
		internal IntPtr border_pixel;

		// Token: 0x04001F57 RID: 8023
		internal Gravity bit_gravity;

		// Token: 0x04001F58 RID: 8024
		internal Gravity win_gravity;

		// Token: 0x04001F59 RID: 8025
		internal int backing_store;

		// Token: 0x04001F5A RID: 8026
		internal IntPtr backing_planes;

		// Token: 0x04001F5B RID: 8027
		internal IntPtr backing_pixel;

		// Token: 0x04001F5C RID: 8028
		internal bool save_under;

		// Token: 0x04001F5D RID: 8029
		internal IntPtr event_mask;

		// Token: 0x04001F5E RID: 8030
		internal IntPtr do_not_propagate_mask;

		// Token: 0x04001F5F RID: 8031
		internal bool override_redirect;

		// Token: 0x04001F60 RID: 8032
		internal IntPtr colormap;

		// Token: 0x04001F61 RID: 8033
		internal IntPtr cursor;
	}
}
