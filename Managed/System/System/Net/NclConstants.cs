using System;

namespace System.Net
{
	// Token: 0x02000439 RID: 1081
	internal static class NclConstants
	{
		// Token: 0x04001CB6 RID: 7350
		internal static readonly object Sentinel = new object();

		// Token: 0x04001CB7 RID: 7351
		internal static readonly object[] EmptyObjectArray = new object[0];

		// Token: 0x04001CB8 RID: 7352
		internal static readonly Uri[] EmptyUriArray = new Uri[0];

		// Token: 0x04001CB9 RID: 7353
		internal static readonly byte[] CRLF = new byte[] { 13, 10 };

		// Token: 0x04001CBA RID: 7354
		internal static readonly byte[] ChunkTerminator = new byte[] { 48, 13, 10, 13, 10 };
	}
}
