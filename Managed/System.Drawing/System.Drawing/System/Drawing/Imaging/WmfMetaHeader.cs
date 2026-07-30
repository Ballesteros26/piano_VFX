using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x02000115 RID: 277
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct WmfMetaHeader
	{
		// Token: 0x04000A55 RID: 2645
		public short file_type;

		// Token: 0x04000A56 RID: 2646
		public short header_size;

		// Token: 0x04000A57 RID: 2647
		public short version;

		// Token: 0x04000A58 RID: 2648
		public ushort file_size_low;

		// Token: 0x04000A59 RID: 2649
		public ushort file_size_high;

		// Token: 0x04000A5A RID: 2650
		public short num_of_objects;

		// Token: 0x04000A5B RID: 2651
		public int max_record_size;

		// Token: 0x04000A5C RID: 2652
		public short num_of_params;
	}
}
