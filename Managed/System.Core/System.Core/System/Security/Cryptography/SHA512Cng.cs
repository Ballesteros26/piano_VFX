using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA) for 512-bit hash values.</summary>
	// Token: 0x02000085 RID: 133
	public sealed class SHA512Cng : SHA512
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA512Cng" /> class. </summary>
		// Token: 0x06000321 RID: 801 RVA: 0x000083C4 File Offset: 0x000065C4
		[SecurityCritical]
		public SHA512Cng()
		{
			this.hash = new SHA512Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x06000322 RID: 802 RVA: 0x000083D7 File Offset: 0x000065D7
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000083E4 File Offset: 0x000065E4
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000083F7 File Offset: 0x000065F7
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA512Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00008423 File Offset: 0x00006623
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000310 RID: 784
		private static byte[] Empty = new byte[0];

		// Token: 0x04000311 RID: 785
		private SHA512 hash;
	}
}
