using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000406 RID: 1030
	[StructLayout(0, Pack = 2)]
	internal struct XColor
	{
		// Token: 0x0400200D RID: 8205
		internal IntPtr pixel;

		// Token: 0x0400200E RID: 8206
		internal ushort red;

		// Token: 0x0400200F RID: 8207
		internal ushort green;

		// Token: 0x04002010 RID: 8208
		internal ushort blue;

		// Token: 0x04002011 RID: 8209
		internal byte flags;

		// Token: 0x04002012 RID: 8210
		internal byte pad;
	}
}
