using System;
using System.Security.Cryptography;
using Mono.Security.Protocol.Tls;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000099 RID: 153
	internal class MD5SHA1 : HashAlgorithm
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public MD5SHA1()
		{
			this.md5 = MD5.Create();
			this.sha = SHA1.Create();
			this.HashSizeValue = this.md5.HashSize + this.sha.HashSize;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001A1FB File Offset: 0x000183FB
		public override void Initialize()
		{
			this.md5.Initialize();
			this.sha.Initialize();
			this.hashing = false;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001A21C File Offset: 0x0001841C
		protected override byte[] HashFinal()
		{
			if (!this.hashing)
			{
				this.hashing = true;
			}
			this.md5.TransformFinalBlock(new byte[0], 0, 0);
			this.sha.TransformFinalBlock(new byte[0], 0, 0);
			byte[] array = new byte[36];
			Buffer.BlockCopy(this.md5.Hash, 0, array, 0, 16);
			Buffer.BlockCopy(this.sha.Hash, 0, array, 16, 20);
			return array;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001A294 File Offset: 0x00018494
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if (!this.hashing)
			{
				this.hashing = true;
			}
			this.md5.TransformBlock(array, ibStart, cbSize, array, ibStart);
			this.sha.TransformBlock(array, ibStart, cbSize, array, ibStart);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001A2C7 File Offset: 0x000184C7
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

		// Token: 0x06000597 RID: 1431 RVA: 0x0001A2F3 File Offset: 0x000184F3
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

		// Token: 0x040003CC RID: 972
		private HashAlgorithm md5;

		// Token: 0x040003CD RID: 973
		private HashAlgorithm sha;

		// Token: 0x040003CE RID: 974
		private bool hashing;
	}
}
