using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x02000118 RID: 280
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct EnhMetafileHeader
	{
		// Token: 0x04000A5E RID: 2654
		public int type;

		// Token: 0x04000A5F RID: 2655
		public int size;

		// Token: 0x04000A60 RID: 2656
		public Rectangle bounds;

		// Token: 0x04000A61 RID: 2657
		public Rectangle frame;

		// Token: 0x04000A62 RID: 2658
		public int signature;

		// Token: 0x04000A63 RID: 2659
		public int version;

		// Token: 0x04000A64 RID: 2660
		public int bytes;

		// Token: 0x04000A65 RID: 2661
		public int records;

		// Token: 0x04000A66 RID: 2662
		public short handles;

		// Token: 0x04000A67 RID: 2663
		public short reserved;

		// Token: 0x04000A68 RID: 2664
		public int description;

		// Token: 0x04000A69 RID: 2665
		public int off_description;

		// Token: 0x04000A6A RID: 2666
		public int palette_entires;

		// Token: 0x04000A6B RID: 2667
		public Size device;

		// Token: 0x04000A6C RID: 2668
		public Size millimeters;
	}
}
