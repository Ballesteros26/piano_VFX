using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000097 RID: 151
	public abstract class MD4 : HashAlgorithm
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x000199EA File Offset: 0x00017BEA
		protected MD4()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000199FD File Offset: 0x00017BFD
		public new static MD4 Create()
		{
			return MD4.Create("MD4");
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00019A0C File Offset: 0x00017C0C
		public new static MD4 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD4Managed();
			}
			return (MD4)obj;
		}
	}
}
