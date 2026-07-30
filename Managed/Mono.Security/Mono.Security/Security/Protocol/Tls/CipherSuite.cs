using System;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200002F RID: 47
	internal abstract class CipherSuite
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000D4CE File Offset: 0x0000B6CE
		protected ICryptoTransform EncryptionCipher
		{
			get
			{
				return this.encryptionCipher;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000D4D6 File Offset: 0x0000B6D6
		protected ICryptoTransform DecryptionCipher
		{
			get
			{
				return this.decryptionCipher;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000D4DE File Offset: 0x0000B6DE
		protected KeyedHashAlgorithm ClientHMAC
		{
			get
			{
				return this.clientHMAC;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000D4E6 File Offset: 0x0000B6E6
		protected KeyedHashAlgorithm ServerHMAC
		{
			get
			{
				return this.serverHMAC;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000D4EE File Offset: 0x0000B6EE
		public CipherAlgorithmType CipherAlgorithmType
		{
			get
			{
				return this.cipherAlgorithmType;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		public string HashAlgorithmName
		{
			get
			{
				HashAlgorithmType hashAlgorithmType = this.hashAlgorithmType;
				if (hashAlgorithmType == HashAlgorithmType.Md5)
				{
					return "MD5";
				}
				if (hashAlgorithmType != HashAlgorithmType.Sha1)
				{
					return "None";
				}
				return "SHA1";
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000D528 File Offset: 0x0000B728
		internal HashAlgorithm CreateHashAlgorithm()
		{
			HashAlgorithmType hashAlgorithmType = this.hashAlgorithmType;
			if (hashAlgorithmType == HashAlgorithmType.Md5)
			{
				return MD5.Create();
			}
			if (hashAlgorithmType != HashAlgorithmType.Sha1)
			{
				return null;
			}
			return SHA1.Create();
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000D552 File Offset: 0x0000B752
		public HashAlgorithmType HashAlgorithmType
		{
			get
			{
				return this.hashAlgorithmType;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000D55C File Offset: 0x0000B75C
		public int HashSize
		{
			get
			{
				HashAlgorithmType hashAlgorithmType = this.hashAlgorithmType;
				if (hashAlgorithmType == HashAlgorithmType.Md5)
				{
					return 16;
				}
				if (hashAlgorithmType != HashAlgorithmType.Sha1)
				{
					return 0;
				}
				return 20;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000D580 File Offset: 0x0000B780
		public ExchangeAlgorithmType ExchangeAlgorithmType
		{
			get
			{
				return this.exchangeAlgorithmType;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000D588 File Offset: 0x0000B788
		public CipherMode CipherMode
		{
			get
			{
				return this.cipherMode;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000D590 File Offset: 0x0000B790
		public short Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000D598 File Offset: 0x0000B798
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000D5A0 File Offset: 0x0000B7A0
		public bool IsExportable
		{
			get
			{
				return this.isExportable;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public byte KeyMaterialSize
		{
			get
			{
				return this.keyMaterialSize;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		public int KeyBlockSize
		{
			get
			{
				return this.keyBlockSize;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		public byte ExpandedKeyMaterialSize
		{
			get
			{
				return this.expandedKeyMaterialSize;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		public short EffectiveKeyBits
		{
			get
			{
				return this.effectiveKeyBits;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		public byte IvSize
		{
			get
			{
				return this.ivSize;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		public Context Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public CipherSuite(short code, string name, CipherAlgorithmType cipherAlgorithmType, HashAlgorithmType hashAlgorithmType, ExchangeAlgorithmType exchangeAlgorithmType, bool exportable, bool blockMode, byte keyMaterialSize, byte expandedKeyMaterialSize, short effectiveKeyBits, byte ivSize, byte blockSize)
		{
			this.code = code;
			this.name = name;
			this.cipherAlgorithmType = cipherAlgorithmType;
			this.hashAlgorithmType = hashAlgorithmType;
			this.exchangeAlgorithmType = exchangeAlgorithmType;
			this.isExportable = exportable;
			if (blockMode)
			{
				this.cipherMode = CipherMode.CBC;
			}
			this.keyMaterialSize = keyMaterialSize;
			this.expandedKeyMaterialSize = expandedKeyMaterialSize;
			this.effectiveKeyBits = effectiveKeyBits;
			this.ivSize = ivSize;
			this.blockSize = blockSize;
			this.keyBlockSize = (int)this.keyMaterialSize + this.HashSize + (int)this.ivSize << 1;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000D673 File Offset: 0x0000B873
		internal void Write(byte[] array, int offset, short value)
		{
			if (offset > array.Length - 2)
			{
				throw new ArgumentException("offset");
			}
			array[offset] = (byte)(value >> 8);
			array[offset + 1] = (byte)value;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000D698 File Offset: 0x0000B898
		internal void Write(byte[] array, int offset, ulong value)
		{
			if (offset > array.Length - 8)
			{
				throw new ArgumentException("offset");
			}
			array[offset] = (byte)(value >> 56);
			array[offset + 1] = (byte)(value >> 48);
			array[offset + 2] = (byte)(value >> 40);
			array[offset + 3] = (byte)(value >> 32);
			array[offset + 4] = (byte)(value >> 24);
			array[offset + 5] = (byte)(value >> 16);
			array[offset + 6] = (byte)(value >> 8);
			array[offset + 7] = (byte)value;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000D702 File Offset: 0x0000B902
		public void InitializeCipher()
		{
			this.createEncryptionCipher();
			this.createDecryptionCipher();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000D710 File Offset: 0x0000B910
		public byte[] EncryptRecord(byte[] fragment, byte[] mac)
		{
			int num = fragment.Length + mac.Length;
			int num2 = 0;
			if (this.CipherMode == CipherMode.CBC)
			{
				num++;
				num2 = (int)this.blockSize - num % (int)this.blockSize;
				if (num2 == (int)this.blockSize)
				{
					num2 = 0;
				}
				num += num2;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(fragment, 0, array, 0, fragment.Length);
			Buffer.BlockCopy(mac, 0, array, fragment.Length, mac.Length);
			if (num2 > 0)
			{
				int num3 = fragment.Length + mac.Length;
				for (int i = num3; i < num3 + num2 + 1; i++)
				{
					array[i] = (byte)num2;
				}
			}
			this.EncryptionCipher.TransformBlock(array, 0, array.Length, array, 0);
			return array;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		public void DecryptRecord(byte[] fragment, out byte[] dcrFragment, out byte[] dcrMAC)
		{
			this.DecryptionCipher.TransformBlock(fragment, 0, fragment.Length, fragment, 0);
			int num2;
			if (this.CipherMode == CipherMode.CBC)
			{
				int num = (int)fragment[fragment.Length - 1];
				num2 = fragment.Length - (num + 1) - this.HashSize;
			}
			else
			{
				num2 = fragment.Length - this.HashSize;
			}
			dcrFragment = new byte[num2];
			dcrMAC = new byte[this.HashSize];
			Buffer.BlockCopy(fragment, 0, dcrFragment, 0, dcrFragment.Length);
			Buffer.BlockCopy(fragment, dcrFragment.Length, dcrMAC, 0, dcrMAC.Length);
		}

		// Token: 0x0600020C RID: 524
		public abstract byte[] ComputeClientRecordMAC(ContentType contentType, byte[] fragment);

		// Token: 0x0600020D RID: 525
		public abstract byte[] ComputeServerRecordMAC(ContentType contentType, byte[] fragment);

		// Token: 0x0600020E RID: 526
		public abstract void ComputeMasterSecret(byte[] preMasterSecret);

		// Token: 0x0600020F RID: 527
		public abstract void ComputeKeys();

		// Token: 0x06000210 RID: 528 RVA: 0x0000D838 File Offset: 0x0000BA38
		public byte[] CreatePremasterSecret()
		{
			ClientContext clientContext = (ClientContext)this.context;
			byte[] secureRandomBytes = this.context.GetSecureRandomBytes(48);
			secureRandomBytes[0] = (byte)(clientContext.ClientHelloProtocol >> 8);
			secureRandomBytes[1] = (byte)clientContext.ClientHelloProtocol;
			return secureRandomBytes;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000D874 File Offset: 0x0000BA74
		public byte[] PRF(byte[] secret, string label, byte[] data, int length)
		{
			int num = secret.Length >> 1;
			if ((secret.Length & 1) == 1)
			{
				num++;
			}
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(Encoding.ASCII.GetBytes(label));
			tlsStream.Write(data);
			byte[] array = tlsStream.ToArray();
			tlsStream.Reset();
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(secret, 0, array2, 0, num);
			byte[] array3 = new byte[num];
			Buffer.BlockCopy(secret, secret.Length - num, array3, 0, num);
			byte[] array4 = this.Expand(MD5.Create(), array2, array, length);
			byte[] array5 = this.Expand(SHA1.Create(), array3, array, length);
			byte[] array6 = new byte[length];
			for (int i = 0; i < array6.Length; i++)
			{
				array6[i] = array4[i] ^ array5[i];
			}
			return array6;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000D934 File Offset: 0x0000BB34
		public byte[] Expand(HashAlgorithm hash, byte[] secret, byte[] seed, int length)
		{
			int num = hash.HashSize / 8;
			int num2 = length / num;
			if (length % num > 0)
			{
				num2++;
			}
			Mono.Security.Cryptography.HMAC hmac = new Mono.Security.Cryptography.HMAC(hash, secret);
			TlsStream tlsStream = new TlsStream();
			byte[][] array = new byte[num2 + 1][];
			array[0] = seed;
			for (int i = 1; i <= num2; i++)
			{
				TlsStream tlsStream2 = new TlsStream();
				hmac.TransformFinalBlock(array[i - 1], 0, array[i - 1].Length);
				array[i] = hmac.Hash;
				tlsStream2.Write(array[i]);
				tlsStream2.Write(seed);
				hmac.TransformFinalBlock(tlsStream2.ToArray(), 0, (int)tlsStream2.Length);
				tlsStream.Write(hmac.Hash);
				tlsStream2.Reset();
			}
			byte[] array2 = new byte[length];
			Buffer.BlockCopy(tlsStream.ToArray(), 0, array2, 0, array2.Length);
			tlsStream.Reset();
			return array2;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000DA14 File Offset: 0x0000BC14
		private void createEncryptionCipher()
		{
			switch (this.cipherAlgorithmType)
			{
			case CipherAlgorithmType.Des:
				this.encryptionAlgorithm = DES.Create();
				break;
			case CipherAlgorithmType.Rc2:
				this.encryptionAlgorithm = RC2.Create();
				break;
			case CipherAlgorithmType.Rc4:
				this.encryptionAlgorithm = new ARC4Managed();
				break;
			case CipherAlgorithmType.Rijndael:
				this.encryptionAlgorithm = Aes.Create();
				break;
			case CipherAlgorithmType.TripleDes:
				this.encryptionAlgorithm = TripleDES.Create();
				break;
			}
			if (this.cipherMode == CipherMode.CBC)
			{
				this.encryptionAlgorithm.Mode = this.cipherMode;
				this.encryptionAlgorithm.Padding = PaddingMode.None;
				this.encryptionAlgorithm.KeySize = (int)(this.expandedKeyMaterialSize * 8);
				this.encryptionAlgorithm.BlockSize = (int)(this.blockSize * 8);
			}
			if (this.context is ClientContext)
			{
				this.encryptionAlgorithm.Key = this.context.ClientWriteKey;
				this.encryptionAlgorithm.IV = this.context.ClientWriteIV;
			}
			else
			{
				this.encryptionAlgorithm.Key = this.context.ServerWriteKey;
				this.encryptionAlgorithm.IV = this.context.ServerWriteIV;
			}
			this.encryptionCipher = this.encryptionAlgorithm.CreateEncryptor();
			if (this.context is ClientContext)
			{
				this.clientHMAC = new Mono.Security.Cryptography.HMAC(this.CreateHashAlgorithm(), this.context.Negotiating.ClientWriteMAC);
				return;
			}
			this.serverHMAC = new Mono.Security.Cryptography.HMAC(this.CreateHashAlgorithm(), this.context.Negotiating.ServerWriteMAC);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		private void createDecryptionCipher()
		{
			switch (this.cipherAlgorithmType)
			{
			case CipherAlgorithmType.Des:
				this.decryptionAlgorithm = DES.Create();
				break;
			case CipherAlgorithmType.Rc2:
				this.decryptionAlgorithm = RC2.Create();
				break;
			case CipherAlgorithmType.Rc4:
				this.decryptionAlgorithm = new ARC4Managed();
				break;
			case CipherAlgorithmType.Rijndael:
				this.decryptionAlgorithm = Aes.Create();
				break;
			case CipherAlgorithmType.TripleDes:
				this.decryptionAlgorithm = TripleDES.Create();
				break;
			}
			if (this.cipherMode == CipherMode.CBC)
			{
				this.decryptionAlgorithm.Mode = this.cipherMode;
				this.decryptionAlgorithm.Padding = PaddingMode.None;
				this.decryptionAlgorithm.KeySize = (int)(this.expandedKeyMaterialSize * 8);
				this.decryptionAlgorithm.BlockSize = (int)(this.blockSize * 8);
			}
			if (this.context is ClientContext)
			{
				this.decryptionAlgorithm.Key = this.context.ServerWriteKey;
				this.decryptionAlgorithm.IV = this.context.ServerWriteIV;
			}
			else
			{
				this.decryptionAlgorithm.Key = this.context.ClientWriteKey;
				this.decryptionAlgorithm.IV = this.context.ClientWriteIV;
			}
			this.decryptionCipher = this.decryptionAlgorithm.CreateDecryptor();
			if (this.context is ClientContext)
			{
				this.serverHMAC = new Mono.Security.Cryptography.HMAC(this.CreateHashAlgorithm(), this.context.Negotiating.ServerWriteMAC);
				return;
			}
			this.clientHMAC = new Mono.Security.Cryptography.HMAC(this.CreateHashAlgorithm(), this.context.Negotiating.ClientWriteMAC);
		}

		// Token: 0x0400011D RID: 285
		public static byte[] EmptyArray = new byte[0];

		// Token: 0x0400011E RID: 286
		private short code;

		// Token: 0x0400011F RID: 287
		private string name;

		// Token: 0x04000120 RID: 288
		private CipherAlgorithmType cipherAlgorithmType;

		// Token: 0x04000121 RID: 289
		private HashAlgorithmType hashAlgorithmType;

		// Token: 0x04000122 RID: 290
		private ExchangeAlgorithmType exchangeAlgorithmType;

		// Token: 0x04000123 RID: 291
		private bool isExportable;

		// Token: 0x04000124 RID: 292
		private CipherMode cipherMode;

		// Token: 0x04000125 RID: 293
		private byte keyMaterialSize;

		// Token: 0x04000126 RID: 294
		private int keyBlockSize;

		// Token: 0x04000127 RID: 295
		private byte expandedKeyMaterialSize;

		// Token: 0x04000128 RID: 296
		private short effectiveKeyBits;

		// Token: 0x04000129 RID: 297
		private byte ivSize;

		// Token: 0x0400012A RID: 298
		private byte blockSize;

		// Token: 0x0400012B RID: 299
		private Context context;

		// Token: 0x0400012C RID: 300
		private SymmetricAlgorithm encryptionAlgorithm;

		// Token: 0x0400012D RID: 301
		private ICryptoTransform encryptionCipher;

		// Token: 0x0400012E RID: 302
		private SymmetricAlgorithm decryptionAlgorithm;

		// Token: 0x0400012F RID: 303
		private ICryptoTransform decryptionCipher;

		// Token: 0x04000130 RID: 304
		private KeyedHashAlgorithm clientHMAC;

		// Token: 0x04000131 RID: 305
		private KeyedHashAlgorithm serverHMAC;
	}
}
