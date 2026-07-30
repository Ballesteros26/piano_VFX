using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000095 RID: 149
	public abstract class MD2 : HashAlgorithm
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x000196FD File Offset: 0x000178FD
		protected MD2()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00019710 File Offset: 0x00017910
		public new static MD2 Create()
		{
			return MD2.Create("MD2");
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001971C File Offset: 0x0001791C
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
