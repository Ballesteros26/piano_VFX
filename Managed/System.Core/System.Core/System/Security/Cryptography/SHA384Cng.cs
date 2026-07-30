using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA) for 384-bit hash values.</summary>
	// Token: 0x02000083 RID: 131
	public sealed class SHA384Cng : SHA384
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA384Cng" /> class. </summary>
		// Token: 0x06000315 RID: 789 RVA: 0x000082C4 File Offset: 0x000064C4
		[SecurityCritical]
		public SHA384Cng()
		{
			this.hash = new SHA384Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x06000316 RID: 790 RVA: 0x000082D7 File Offset: 0x000064D7
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000082E4 File Offset: 0x000064E4
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000082F7 File Offset: 0x000064F7
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA384Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008323 File Offset: 0x00006523
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400030C RID: 780
		private static byte[] Empty = new byte[0];

		// Token: 0x0400030D RID: 781
		private SHA384 hash;
	}
}
