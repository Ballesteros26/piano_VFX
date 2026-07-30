using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000085 RID: 133
	internal abstract class MD4 : HashAlgorithm
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00017949 File Offset: 0x00015B49
		protected MD4()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00017C36 File Offset: 0x00015E36
		public new static MD4 Create()
		{
			return MD4.Create("MD4");
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00017C44 File Offset: 0x00015E44
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
