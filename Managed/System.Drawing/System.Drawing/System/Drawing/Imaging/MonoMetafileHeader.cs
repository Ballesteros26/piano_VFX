using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x02000119 RID: 281
	[StructLayout(LayoutKind.Explicit)]
	internal struct MonoMetafileHeader
	{
		// Token: 0x04000A6D RID: 2669
		[FieldOffset(0)]
		public MetafileType type;

		// Token: 0x04000A6E RID: 2670
		[FieldOffset(4)]
		public int size;

		// Token: 0x04000A6F RID: 2671
		[FieldOffset(8)]
		public int version;

		// Token: 0x04000A70 RID: 2672
		[FieldOffset(12)]
		public int emf_plus_flags;

		// Token: 0x04000A71 RID: 2673
		[FieldOffset(16)]
		public float dpi_x;

		// Token: 0x04000A72 RID: 2674
		[FieldOffset(20)]
		public float dpi_y;

		// Token: 0x04000A73 RID: 2675
		[FieldOffset(24)]
		public int x;

		// Token: 0x04000A74 RID: 2676
		[FieldOffset(28)]
		public int y;

		// Token: 0x04000A75 RID: 2677
		[FieldOffset(32)]
		public int width;

		// Token: 0x04000A76 RID: 2678
		[FieldOffset(36)]
		public int height;

		// Token: 0x04000A77 RID: 2679
		[FieldOffset(40)]
		public WmfMetaHeader wmf_header;

		// Token: 0x04000A78 RID: 2680
		[FieldOffset(40)]
		public EnhMetafileHeader emf_header;

		// Token: 0x04000A79 RID: 2681
		[FieldOffset(128)]
		public int emfplus_header_size;

		// Token: 0x04000A7A RID: 2682
		[FieldOffset(132)]
		public int logical_dpi_x;

		// Token: 0x04000A7B RID: 2683
		[FieldOffset(136)]
		public int logical_dpi_y;
	}
}
