using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA).</summary>
	// Token: 0x02000080 RID: 128
	public sealed class SHA1Cng : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1Cng" /> class. </summary>
		// Token: 0x06000303 RID: 771 RVA: 0x00008144 File Offset: 0x00006344
		[SecurityCritical]
		public SHA1Cng()
		{
			this.hash = new SHA1Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x06000304 RID: 772 RVA: 0x00008157 File Offset: 0x00006357
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00008164 File Offset: 0x00006364
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008177 File Offset: 0x00006377
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA1Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000081A3 File Offset: 0x000063A3
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000306 RID: 774
		private static byte[] Empty = new byte[0];

		// Token: 0x04000307 RID: 775
		private SHA1 hash;
	}
}
