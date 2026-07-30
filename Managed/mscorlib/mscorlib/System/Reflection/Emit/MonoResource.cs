using System;
using System.IO;

namespace System.Reflection.Emit
{
	// Token: 0x02000345 RID: 837
	internal struct MonoResource
	{
		// Token: 0x0400138A RID: 5002
		public byte[] data;

		// Token: 0x0400138B RID: 5003
		public string name;

		// Token: 0x0400138C RID: 5004
		public string filename;

		// Token: 0x0400138D RID: 5005
		public ResourceAttributes attrs;

		// Token: 0x0400138E RID: 5006
		public int offset;

		// Token: 0x0400138F RID: 5007
		public Stream stream;
	}
}
