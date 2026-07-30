using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA) for 256-bit hash values.</summary>
	// Token: 0x02000081 RID: 129
	public sealed class SHA256Cng : SHA256
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA256Cng" /> class. </summary>
		// Token: 0x06000309 RID: 777 RVA: 0x000081C4 File Offset: 0x000063C4
		[SecurityCritical]
		public SHA256Cng()
		{
			this.hash = new SHA256Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x0600030A RID: 778 RVA: 0x000081D7 File Offset: 0x000063D7
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000081E4 File Offset: 0x000063E4
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000081F7 File Offset: 0x000063F7
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA256Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00008223 File Offset: 0x00006423
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000308 RID: 776
		private static byte[] Empty = new byte[0];

		// Token: 0x04000309 RID: 777
		private SHA256 hash;
	}
}
