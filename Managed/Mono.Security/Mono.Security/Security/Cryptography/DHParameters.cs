using System;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	public struct DHParameters
	{
		// Token: 0x040003A2 RID: 930
		public byte[] P;

		// Token: 0x040003A3 RID: 931
		public byte[] G;

		// Token: 0x040003A4 RID: 932
		[NonSerialized]
		public byte[] X;
	}
}
