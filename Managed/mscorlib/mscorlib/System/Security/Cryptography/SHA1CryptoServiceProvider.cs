using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the input data using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited. </summary>
	// Token: 0x020006A5 RID: 1701
	[ComVisible(true)]
	public sealed class SHA1CryptoServiceProvider : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> class.</summary>
		// Token: 0x060048BD RID: 18621 RVA: 0x001064E8 File Offset: 0x001046E8
		public SHA1CryptoServiceProvider()
		{
			this.sha = new SHA1Internal();
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x001064FC File Offset: 0x001046FC
		~SHA1CryptoServiceProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0010652C File Offset: 0x0010472C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x00106535 File Offset: 0x00104735
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this.State = 1;
			this.sha.HashCore(rgb, ibStart, cbSize);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x0010654C File Offset: 0x0010474C
		protected override byte[] HashFinal()
		{
			this.State = 0;
			return this.sha.HashFinal();
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060048C2 RID: 18626 RVA: 0x00106560 File Offset: 0x00104760
		public override void Initialize()
		{
			this.sha.Initialize();
		}

		// Token: 0x0400260E RID: 9742
		private SHA1Internal sha;
	}
}
