using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a CNG (Cryptography Next Generation) implementation of the MD5 (Message Digest 5) 128-bit hashing algorithm.</summary>
	// Token: 0x0200007F RID: 127
	public sealed class MD5Cng : MD5
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MD5Cng" /> class. </summary>
		/// <exception cref="T:System.InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
		// Token: 0x060002FD RID: 765 RVA: 0x000080C4 File Offset: 0x000062C4
		[SecurityCritical]
		public MD5Cng()
		{
			this.hash = new MD5CryptoServiceProvider();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x060002FE RID: 766 RVA: 0x000080D7 File Offset: 0x000062D7
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000080E4 File Offset: 0x000062E4
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000080F7 File Offset: 0x000062F7
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(MD5Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00008123 File Offset: 0x00006323
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000304 RID: 772
		private static byte[] Empty = new byte[0];

		// Token: 0x04000305 RID: 773
		private MD5 hash;
	}
}
