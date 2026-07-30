using System;

namespace System.Windows.Forms
{
	// Token: 0x020003F3 RID: 1011
	internal struct XColormapEvent
	{
		// Token: 0x04001EF7 RID: 7927
		internal XEventName type;

		// Token: 0x04001EF8 RID: 7928
		internal IntPtr serial;

		// Token: 0x04001EF9 RID: 7929
		internal bool send_event;

		// Token: 0x04001EFA RID: 7930
		internal IntPtr display;

		// Token: 0x04001EFB RID: 7931
		internal IntPtr window;

		// Token: 0x04001EFC RID: 7932
		internal IntPtr colormap;

		// Token: 0x04001EFD RID: 7933
		internal bool c_new;

		// Token: 0x04001EFE RID: 7934
		internal int state;
	}
}
