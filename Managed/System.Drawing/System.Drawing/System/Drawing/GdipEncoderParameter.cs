using System;
using System.Drawing.Imaging;

namespace System.Drawing
{
	// Token: 0x020000A0 RID: 160
	internal struct GdipEncoderParameter
	{
		// Token: 0x04000601 RID: 1537
		internal Guid guid;

		// Token: 0x04000602 RID: 1538
		internal uint numberOfValues;

		// Token: 0x04000603 RID: 1539
		internal EncoderParameterValueType type;

		// Token: 0x04000604 RID: 1540
		internal IntPtr value;
	}
}
