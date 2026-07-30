using System;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.SHA384" /> algorithm. </summary>
	// Token: 0x02000084 RID: 132
	public sealed class SHA384CryptoServiceProvider : SHA384
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA384CryptoServiceProvider" /> class. </summary>
		// Token: 0x0600031B RID: 795 RVA: 0x00008344 File Offset: 0x00006544
		[SecurityCritical]
		public SHA384CryptoServiceProvider()
		{
			this.hash = new SHA384Managed();
		}

		/// <summary>Initializes, or reinitializes, an instance of a hash algorithm.</summary>
		// Token: 0x0600031C RID: 796 RVA: 0x00008357 File Offset: 0x00006557
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008364 File Offset: 0x00006564
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00008377 File Offset: 0x00006577
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA384CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000083A3 File Offset: 0x000065A3
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400030E RID: 782
		private static byte[] Empty = new byte[0];

		// Token: 0x0400030F RID: 783
		private SHA384 hash;
	}
}
