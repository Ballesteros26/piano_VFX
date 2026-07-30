using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200009C RID: 156
	public abstract class RC4 : SymmetricAlgorithm
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0001AD74 File Offset: 0x00018F74
		public RC4()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC4.s_legalBlockSizes;
			this.LegalKeySizesValue = RC4.s_legalKeySizes;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001ADB1 File Offset: 0x00018FB1
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001ADB9 File Offset: 0x00018FB9
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

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001ADBB File Offset: 0x00018FBB
		public new static RC4 Create()
		{
			return RC4.Create("RC4");
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		public new static RC4 Create(string algName)
		{
			object obj = CryptoConfig.CreateFromName(algName);
			if (obj == null)
			{
				obj = new ARC4Managed();
			}
			return (RC4)obj;
		}

		// Token: 0x040003D3 RID: 979
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x040003D4 RID: 980
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 2048, 8)
		};
	}
}
