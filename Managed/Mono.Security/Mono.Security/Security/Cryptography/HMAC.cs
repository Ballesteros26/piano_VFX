using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000A2 RID: 162
	internal class HMAC : KeyedHashAlgorithm
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001CC48 File Offset: 0x0001AE48
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0001CC5C File Offset: 0x0001AE5C
		public override byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.hashing)
				{
					throw new Exception("Cannot change key during hash operation.");
				}
				if (value.Length > 64)
				{
					this.KeyValue = this.hash.ComputeHash(value);
				}
				else
				{
					this.KeyValue = (byte[])value.Clone();
				}
				this.initializePad();
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001CCB0 File Offset: 0x0001AEB0
		public HMAC()
		{
			this.hash = MD5.Create();
			this.HashSizeValue = this.hash.HashSize;
			byte[] array = new byte[64];
			new RNGCryptoServiceProvider().GetNonZeroBytes(array);
			this.KeyValue = (byte[])array.Clone();
			this.Initialize();
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		public HMAC(HashAlgorithm ha, byte[] rgbKey)
		{
			this.hash = ha;
			this.HashSizeValue = this.hash.HashSize;
			if (rgbKey.Length > 64)
			{
				this.KeyValue = this.hash.ComputeHash(rgbKey);
			}
			else
			{
				this.KeyValue = (byte[])rgbKey.Clone();
			}
			this.Initialize();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001CD69 File Offset: 0x0001AF69
		public override void Initialize()
		{
			this.hash.Initialize();
			this.initializePad();
			this.hashing = false;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001CD84 File Offset: 0x0001AF84
		protected override byte[] HashFinal()
		{
			if (!this.hashing)
			{
				this.hash.TransformBlock(this.innerPad, 0, this.innerPad.Length, this.innerPad, 0);
				this.hashing = true;
			}
			this.hash.TransformFinalBlock(new byte[0], 0, 0);
			byte[] array = this.hash.Hash;
			this.hash.Initialize();
			this.hash.TransformBlock(this.outerPad, 0, this.outerPad.Length, this.outerPad, 0);
			this.hash.TransformFinalBlock(array, 0, array.Length);
			this.Initialize();
			return this.hash.Hash;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001CE34 File Offset: 0x0001B034
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if (!this.hashing)
			{
				this.hash.TransformBlock(this.innerPad, 0, this.innerPad.Length, this.innerPad, 0);
				this.hashing = true;
			}
			this.hash.TransformBlock(array, ibStart, cbSize, array, ibStart);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001CE84 File Offset: 0x0001B084
		private void initializePad()
		{
			this.innerPad = new byte[64];
			this.outerPad = new byte[64];
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				this.innerPad[i] = this.KeyValue[i] ^ 54;
				this.outerPad[i] = this.KeyValue[i] ^ 92;
			}
			for (int j = this.KeyValue.Length; j < 64; j++)
			{
				this.innerPad[j] = 54;
				this.outerPad[j] = 92;
			}
		}

		// Token: 0x040003F6 RID: 1014
		private HashAlgorithm hash;

		// Token: 0x040003F7 RID: 1015
		private bool hashing;

		// Token: 0x040003F8 RID: 1016
		private byte[] innerPad;

		// Token: 0x040003F9 RID: 1017
		private byte[] outerPad;
	}
}
