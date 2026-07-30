using System;

namespace System.Windows.Forms
{
	// Token: 0x020003FA RID: 1018
	internal struct XWindowAttributes
	{
		// Token: 0x06004626 RID: 17958 RVA: 0x00114608 File Offset: 0x00112808
		public override string ToString()
		{
			return XEvent.ToString(this);
		}

		// Token: 0x04001F62 RID: 8034
		internal int x;

		// Token: 0x04001F63 RID: 8035
		internal int y;

		// Token: 0x04001F64 RID: 8036
		internal int width;

		// Token: 0x04001F65 RID: 8037
		internal int height;

		// Token: 0x04001F66 RID: 8038
		internal int border_width;

		// Token: 0x04001F67 RID: 8039
		internal int depth;

		// Token: 0x04001F68 RID: 8040
		internal IntPtr visual;

		// Token: 0x04001F69 RID: 8041
		internal IntPtr root;

		// Token: 0x04001F6A RID: 8042
		internal int c_class;

		// Token: 0x04001F6B RID: 8043
		internal Gravity bit_gravity;

		// Token: 0x04001F6C RID: 8044
		internal Gravity win_gravity;

		// Token: 0x04001F6D RID: 8045
		internal int backing_store;

		// Token: 0x04001F6E RID: 8046
		internal IntPtr backing_planes;

		// Token: 0x04001F6F RID: 8047
		internal IntPtr backing_pixel;

		// Token: 0x04001F70 RID: 8048
		internal bool save_under;

		// Token: 0x04001F71 RID: 8049
		internal IntPtr colormap;

		// Token: 0x04001F72 RID: 8050
		internal bool map_installed;

		// Token: 0x04001F73 RID: 8051
		internal MapState map_state;

		// Token: 0x04001F74 RID: 8052
		internal IntPtr all_event_masks;

		// Token: 0x04001F75 RID: 8053
		internal IntPtr your_event_mask;

		// Token: 0x04001F76 RID: 8054
		internal IntPtr do_not_propagate_mask;

		// Token: 0x04001F77 RID: 8055
		internal bool override_direct;

		// Token: 0x04001F78 RID: 8056
		internal IntPtr screen;
	}
}
