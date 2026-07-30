using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000076 RID: 118
	public sealed class RSACng : RSA
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x00006233 File Offset: 0x00004433
		public RSACng()
			: this(2048)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00006240 File Offset: 0x00004440
		public RSACng(int keySize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00006240 File Offset: 0x00004440
		public RSACng(CngKey key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000227E File Offset: 0x0000047E
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000227E File Offset: 0x0000047E
		public CngKey Key
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000227E File Offset: 0x0000047E
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000227E File Offset: 0x0000047E
		public override void ImportParameters(RSAParameters parameters)
		{
			throw new NotImplementedException();
		}
	}
}
