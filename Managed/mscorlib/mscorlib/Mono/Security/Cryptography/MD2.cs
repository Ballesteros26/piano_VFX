using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000083 RID: 131
	internal abstract class MD2 : HashAlgorithm
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x00017949 File Offset: 0x00015B49
		protected MD2()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001795C File Offset: 0x00015B5C
		public new static MD2 Create()
		{
			return MD2.Create("MD2");
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00017968 File Offset: 0x00015B68
		public new static MD2 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD2Managed();
			}
			return (MD2)obj;
		}
	}
}
