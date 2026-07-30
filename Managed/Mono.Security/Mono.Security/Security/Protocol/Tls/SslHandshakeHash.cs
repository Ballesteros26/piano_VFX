using System;
using System.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200004B RID: 75
	internal class SslHandshakeHash : HashAlgorithm
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00010FDC File Offset: 0x0000F1DC
		public SslHandshakeHash(byte[] secret)
		{
			this.md5 = MD5.Create();
			this.sha = SHA1.Create();
			this.HashSizeValue = this.md5.HashSize + this.sha.HashSize;
			this.secret = secret;
			this.Initialize();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001102F File Offset: 0x0000F22F
		public override void Initialize()
		{
			this.md5.Initialize();
			this.sha.Initialize();
			this.initializePad();
			this.hashing = false;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00011054 File Offset: 0x0000F254
		protected override byte[] HashFinal()
		{
			if (!this.hashing)
			{
				this.hashing = true;
			}
			this.md5.TransformBlock(this.secret, 0, this.secret.Length, this.secret, 0);
			this.md5.TransformFinalBlock(this.innerPadMD5, 0, this.innerPadMD5.Length);
			byte[] hash = this.md5.Hash;
			this.md5.Initialize();
			this.md5.TransformBlock(this.secret, 0, this.secret.Length, this.secret, 0);
			this.md5.TransformBlock(this.outerPadMD5, 0, this.outerPadMD5.Length, this.outerPadMD5, 0);
			this.md5.TransformFinalBlock(hash, 0, hash.Length);
			this.sha.TransformBlock(this.secret, 0, this.secret.Length, this.secret, 0);
			this.sha.TransformFinalBlock(this.innerPadSHA, 0, this.innerPadSHA.Length);
			byte[] hash2 = this.sha.Hash;
			this.sha.Initialize();
			this.sha.TransformBlock(this.secret, 0, this.secret.Length, this.secret, 0);
			this.sha.TransformBlock(this.outerPadSHA, 0, this.outerPadSHA.Length, this.outerPadSHA, 0);
			this.sha.TransformFinalBlock(hash2, 0, hash2.Length);
			this.Initialize();
			byte[] array = new byte[36];
			Buffer.BlockCopy(this.md5.Hash, 0, array, 0, 16);
			Buffer.BlockCopy(this.sha.Hash, 0, array, 16, 20);
			return array;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000111FC File Offset: 0x0000F3FC
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if (!this.hashing)
			{
				this.hashing = true;
			}
			this.md5.TransformBlock(array, ibStart, cbSize, array, ibStart);
			this.sha.TransformBlock(array, ibStart, cbSize, array, ibStart);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001122F File Offset: 0x0000F42F
		public byte[] CreateSignature(RSA rsa)
		{
			if (rsa == null)
			{
				throw new CryptographicUnexpectedOperationException("missing key");
			}
			RSASslSignatureFormatter rsasslSignatureFormatter = new RSASslSignatureFormatter(rsa);
			rsasslSignatureFormatter.SetHashAlgorithm("MD5SHA1");
			return rsasslSignatureFormatter.CreateSignature(this.Hash);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001125B File Offset: 0x0000F45B
		public bool VerifySignature(RSA rsa, byte[] rgbSignature)
		{
			if (rsa == null)
			{
				throw new CryptographicUnexpectedOperationException("missing key");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			RSASslSignatureDeformatter rsasslSignatureDeformatter = new RSASslSignatureDeformatter(rsa);
			rsasslSignatureDeformatter.SetHashAlgorithm("MD5SHA1");
			return rsasslSignatureDeformatter.VerifySignature(this.Hash, rgbSignature);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011298 File Offset: 0x0000F498
		private void initializePad()
		{
			this.innerPadMD5 = new byte[48];
			this.outerPadMD5 = new byte[48];
			for (int i = 0; i < 48; i++)
			{
				this.innerPadMD5[i] = 54;
				this.outerPadMD5[i] = 92;
			}
			this.innerPadSHA = new byte[40];
			this.outerPadSHA = new byte[40];
			for (int j = 0; j < 40; j++)
			{
				this.innerPadSHA[j] = 54;
				this.outerPadSHA[j] = 92;
			}
		}

		// Token: 0x04000197 RID: 407
		private HashAlgorithm md5;

		// Token: 0x04000198 RID: 408
		private HashAlgorithm sha;

		// Token: 0x04000199 RID: 409
		private bool hashing;

		// Token: 0x0400019A RID: 410
		private byte[] secret;

		// Token: 0x0400019B RID: 411
		private byte[] innerPadMD5;

		// Token: 0x0400019C RID: 412
		private byte[] outerPadMD5;

		// Token: 0x0400019D RID: 413
		private byte[] innerPadSHA;

		// Token: 0x0400019E RID: 414
		private byte[] outerPadSHA;
	}
}
