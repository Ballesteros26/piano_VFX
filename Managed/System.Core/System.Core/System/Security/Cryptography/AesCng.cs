using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000077 RID: 119
	public sealed class AesCng : Aes
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000624D File Offset: 0x0000444D
		public AesCng()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000624D File Offset: 0x0000444D
		public AesCng(string keyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000624D File Offset: 0x0000444D
		public AesCng(string keyName, CngProvider provider)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000624D File Offset: 0x0000444D
		public AesCng(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000227E File Offset: 0x0000047E
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000227E File Offset: 0x0000047E
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000227E File Offset: 0x0000047E
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000227E File Offset: 0x0000047E
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

		// Token: 0x060002C0 RID: 704 RVA: 0x0000227E File Offset: 0x0000047E
		public override ICryptoTransform CreateDecryptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000227E File Offset: 0x0000047E
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000227E File Offset: 0x0000047E
		public override ICryptoTransform CreateEncryptor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00005E51 File Offset: 0x00004051
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return null;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000227E File Offset: 0x0000047E
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000227E File Offset: 0x0000047E
		public override void GenerateIV()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000227E File Offset: 0x0000047E
		public override void GenerateKey()
		{
			throw new NotImplementedException();
		}
	}
}
