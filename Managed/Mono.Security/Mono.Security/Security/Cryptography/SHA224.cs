using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200009F RID: 159
	public abstract class SHA224 : HashAlgorithm
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x0001BB37 File Offset: 0x00019D37
		public SHA224()
		{
			this.HashSizeValue = 224;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001BB4A File Offset: 0x00019D4A
		public new static SHA224 Create()
		{
			return SHA224.Create("SHA224");
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001BB58 File Offset: 0x00019D58
		public new static SHA224 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new SHA224Managed();
			}
			return (SHA224)obj;
		}
	}
}
