using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.Authenticode
{
	// Token: 0x020000A7 RID: 167
	public class PrivateKey
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0001E6B5 File Offset: 0x0001C8B5
		public PrivateKey()
		{
			this.keyType = 2;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
		public PrivateKey(byte[] data, string password)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!this.Decode(data, password))
			{
				throw new CryptographicException(Locale.GetText("Invalid data and/or password"));
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001E6F4 File Offset: 0x0001C8F4
		public bool Encrypted
		{
			get
			{
				return this.encrypted;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0001E704 File Offset: 0x0001C904
		public int KeyType
		{
			get
			{
				return this.keyType;
			}
			set
			{
				this.keyType = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001E70D File Offset: 0x0001C90D
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0001E715 File Offset: 0x0001C915
		public RSA RSA
		{
			get
			{
				return this.rsa;
			}
			set
			{
				this.rsa = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001E71E File Offset: 0x0001C91E
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0001E730 File Offset: 0x0001C930
		public bool Weak
		{
			get
			{
				return !this.encrypted || this.weak;
			}
			set
			{
				this.weak = value;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001E73C File Offset: 0x0001C93C
		private byte[] DeriveKey(byte[] salt, string password)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			SHA1 sha = SHA1.Create();
			sha.TransformBlock(salt, 0, salt.Length, salt, 0);
			sha.TransformFinalBlock(bytes, 0, bytes.Length);
			byte[] array = new byte[16];
			Buffer.BlockCopy(sha.Hash, 0, array, 0, 16);
			sha.Clear();
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001E79C File Offset: 0x0001C99C
		private bool Decode(byte[] pvk, string password)
		{
			if (BitConverterLE.ToUInt32(pvk, 0) != 2964713758U)
			{
				return false;
			}
			if (BitConverterLE.ToUInt32(pvk, 4) != 0U)
			{
				return false;
			}
			this.keyType = BitConverterLE.ToInt32(pvk, 8);
			this.encrypted = BitConverterLE.ToUInt32(pvk, 12) == 1U;
			int num = BitConverterLE.ToInt32(pvk, 16);
			int num2 = BitConverterLE.ToInt32(pvk, 20);
			byte[] array = new byte[num2];
			Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
			if (num > 0)
			{
				if (password == null)
				{
					return false;
				}
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(pvk, 24, array2, 0, num);
				byte[] array3 = this.DeriveKey(array2, password);
				RC4.Create().CreateDecryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
				try
				{
					this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
					this.weak = false;
				}
				catch (CryptographicException)
				{
					this.weak = true;
					Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
					Array.Clear(array3, 5, 11);
					RC4.Create().CreateDecryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
					this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				}
				Array.Clear(array3, 0, array3.Length);
			}
			else
			{
				this.weak = true;
				this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				Array.Clear(array, 0, array.Length);
			}
			Array.Clear(pvk, 0, pvk.Length);
			return this.rsa != null;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001E8F8 File Offset: 0x0001CAF8
		public void Save(string filename)
		{
			this.Save(filename, null);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001E904 File Offset: 0x0001CB04
		public void Save(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			FileStream fileStream = File.Open(filename, FileMode.Create, FileAccess.Write);
			try
			{
				byte[] array2 = new byte[4];
				byte[] array3 = BitConverterLE.GetBytes(2964713758U);
				fileStream.Write(array3, 0, 4);
				fileStream.Write(array2, 0, 4);
				array3 = BitConverterLE.GetBytes(this.keyType);
				fileStream.Write(array3, 0, 4);
				this.encrypted = password != null;
				array = CryptoConvert.ToCapiPrivateKeyBlob(this.rsa);
				if (this.encrypted)
				{
					array3 = BitConverterLE.GetBytes(1);
					fileStream.Write(array3, 0, 4);
					array3 = BitConverterLE.GetBytes(16);
					fileStream.Write(array3, 0, 4);
					array3 = BitConverterLE.GetBytes(array.Length);
					fileStream.Write(array3, 0, 4);
					byte[] array4 = new byte[16];
					RC4 rc = RC4.Create();
					byte[] array5 = null;
					try
					{
						RandomNumberGenerator.Create().GetBytes(array4);
						fileStream.Write(array4, 0, array4.Length);
						array5 = this.DeriveKey(array4, password);
						if (this.Weak)
						{
							Array.Clear(array5, 5, 11);
						}
						rc.CreateEncryptor(array5, null).TransformBlock(array, 8, array.Length - 8, array, 8);
						goto IL_014E;
					}
					finally
					{
						Array.Clear(array4, 0, array4.Length);
						Array.Clear(array5, 0, array5.Length);
						rc.Clear();
					}
				}
				fileStream.Write(array2, 0, 4);
				fileStream.Write(array2, 0, 4);
				array3 = BitConverterLE.GetBytes(array.Length);
				fileStream.Write(array3, 0, 4);
				IL_014E:
				fileStream.Write(array, 0, array.Length);
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
				fileStream.Close();
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001EAB4 File Offset: 0x0001CCB4
		public static PrivateKey CreateFromFile(string filename)
		{
			return PrivateKey.CreateFromFile(filename, null);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		public static PrivateKey CreateFromFile(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return new PrivateKey(array, password);
		}

		// Token: 0x04000425 RID: 1061
		private const uint magic = 2964713758U;

		// Token: 0x04000426 RID: 1062
		private bool encrypted;

		// Token: 0x04000427 RID: 1063
		private RSA rsa;

		// Token: 0x04000428 RID: 1064
		private bool weak;

		// Token: 0x04000429 RID: 1065
		private int keyType;
	}
}
