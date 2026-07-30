using System;

namespace System.Net
{
	// Token: 0x02000447 RID: 1095
	internal struct SecurityBufferStruct
	{
		// Token: 0x04001D3B RID: 7483
		public int count;

		// Token: 0x04001D3C RID: 7484
		public BufferType type;

		// Token: 0x04001D3D RID: 7485
		public IntPtr token;

		// Token: 0x04001D3E RID: 7486
		public static readonly int Size = sizeof(SecurityBufferStruct);
	}
}
