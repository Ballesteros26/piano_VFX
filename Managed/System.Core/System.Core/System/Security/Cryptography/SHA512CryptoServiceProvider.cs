using System;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.SHA512" /> algorithm. </summary>
	// Token: 0x02000086 RID: 134
	public sealed class SHA512CryptoServiceProvider : SHA512
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA512CryptoServiceProvider" /> class. </summary>
		// Token: 0x06000327 RID: 807 RVA: 0x00008444 File Offset: 0x00006644
		[SecurityCritical]
		public SHA512CryptoServiceProvider()
		{
			this.hash = new SHA512Managed();
		}

		/// <summary>Initializes, or reinitializes, an instance of a hash algorithm.</summary>
		// Token: 0x06000328 RID: 808 RVA: 0x00008457 File Offset: 0x00006657
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008464 File Offset: 0x00006664
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008477 File Offset: 0x00006677
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA512CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000084A3 File Offset: 0x000066A3
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000312 RID: 786
		private static byte[] Empty = new byte[0];

		// Token: 0x04000313 RID: 787
		private SHA512 hash;
	}
}
