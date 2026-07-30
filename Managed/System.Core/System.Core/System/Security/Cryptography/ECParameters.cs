using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200007D RID: 125
	public struct ECParameters
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0000227E File Offset: 0x0000047E
		public void Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040002FF RID: 767
		public ECCurve Curve;

		// Token: 0x04000300 RID: 768
		public byte[] D;

		// Token: 0x04000301 RID: 769
		public ECPoint Q;
	}
}
