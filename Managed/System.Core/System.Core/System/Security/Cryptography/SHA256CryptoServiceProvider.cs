using System;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.SHA256" /> algorithm. </summary>
	// Token: 0x02000082 RID: 130
	public sealed class SHA256CryptoServiceProvider : SHA256
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA256CryptoServiceProvider" /> class. </summary>
		// Token: 0x0600030F RID: 783 RVA: 0x00008244 File Offset: 0x00006444
		[SecurityCritical]
		public SHA256CryptoServiceProvider()
		{
			this.hash = new SHA256Managed();
		}

		/// <summary>Initializes, or reinitializes, an instance of a hash algorithm.</summary>
		// Token: 0x06000310 RID: 784 RVA: 0x00008257 File Offset: 0x00006457
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00008264 File Offset: 0x00006464
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00008277 File Offset: 0x00006477
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA256CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000082A3 File Offset: 0x000064A3
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400030A RID: 778
		private static byte[] Empty = new byte[0];

		// Token: 0x0400030B RID: 779
		private SHA256 hash;
	}
}
