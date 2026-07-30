using System;

namespace System.Windows.Forms
{
	// Token: 0x02000408 RID: 1032
	internal struct XScreen
	{
		// Token: 0x0400205A RID: 8282
		internal IntPtr ext_data;

		// Token: 0x0400205B RID: 8283
		internal IntPtr display;

		// Token: 0x0400205C RID: 8284
		internal IntPtr root;

		// Token: 0x0400205D RID: 8285
		internal int width;

		// Token: 0x0400205E RID: 8286
		internal int height;

		// Token: 0x0400205F RID: 8287
		internal int mwidth;

		// Token: 0x04002060 RID: 8288
		internal int mheight;

		// Token: 0x04002061 RID: 8289
		internal int ndepths;

		// Token: 0x04002062 RID: 8290
		internal IntPtr depths;

		// Token: 0x04002063 RID: 8291
		internal int root_depth;

		// Token: 0x04002064 RID: 8292
		internal IntPtr root_visual;

		// Token: 0x04002065 RID: 8293
		internal IntPtr default_gc;

		// Token: 0x04002066 RID: 8294
		internal IntPtr cmap;

		// Token: 0x04002067 RID: 8295
		internal IntPtr white_pixel;

		// Token: 0x04002068 RID: 8296
		internal IntPtr black_pixel;

		// Token: 0x04002069 RID: 8297
		internal int max_maps;

		// Token: 0x0400206A RID: 8298
		internal int min_maps;

		// Token: 0x0400206B RID: 8299
		internal int backing_store;

		// Token: 0x0400206C RID: 8300
		internal bool save_unders;

		// Token: 0x0400206D RID: 8301
		internal IntPtr root_input_mask;
	}
}
