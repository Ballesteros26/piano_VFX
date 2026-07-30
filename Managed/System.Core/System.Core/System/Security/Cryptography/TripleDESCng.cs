using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000087 RID: 135
	public sealed class TripleDESCng : TripleDES
	{
		// Token: 0x0600032D RID: 813 RVA: 0x000084C4 File Offset: 0x000066C4
		public TripleDESCng()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000084C4 File Offset: 0x000066C4
		public TripleDESCng(string keyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000084C4 File Offset: 0x000066C4
		public TripleDESCng(string keyName, CngProvider provider)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000084C4 File Offset: 0x000066C4
		public TripleDESCng(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000227E File Offset: 0x0000047E
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000227E File Offset: 0x0000047E
		public override byte[] Key
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000227E File Offset: 0x0000047E
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000227E File Offset: 0x0000047E
		public override int KeySize
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000227E File Offset: 0x0000047E
		public override ICryptoTransform CreateDecryptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000227E File Offset: 0x0000047E
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000227E File Offset: 0x0000047E
		public override ICryptoTransform CreateEncryptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00005E51 File Offset: 0x00004051
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return null;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000227E File Offset: 0x0000047E
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000227E File Offset: 0x0000047E
		public override void GenerateIV()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000227E File Offset: 0x0000047E
		public override void GenerateKey()
		{
			throw new NotImplementedException();
		}
	}
}
