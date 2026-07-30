using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008C RID: 140
	internal abstract class RC4 : SymmetricAlgorithm
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x0001965D File Offset: 0x0001785D
		public RC4()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC4.s_legalBlockSizes;
			this.LegalKeySizesValue = RC4.s_legalKeySizes;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001969A File Offset: 0x0001789A
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00002194 File Offset: 0x00000394
		public override byte[] IV
		{
			get
			{
				return new byte[0];
			}
			set
			{
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000196A2 File Offset: 0x000178A2
		public new static RC4 Create()
		{
			return RC4.Create("RC4");
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000196B0 File Offset: 0x000178B0
		public new static RC4 Create(string algName)
		{
			object obj = CryptoConfig.CreateFromName(algName);
			if (obj == null)
			{
				obj = new ARC4Managed();
			}
			return (RC4)obj;
		}

		// Token: 0x04000585 RID: 1413
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000586 RID: 1414
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 2048, 8)
		};
	}
}
