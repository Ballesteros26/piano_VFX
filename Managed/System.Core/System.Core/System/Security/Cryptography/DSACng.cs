using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	// Token: 0x0200035B RID: 859
	public sealed class DSACng : DSA
	{
		// Token: 0x06001A03 RID: 6659 RVA: 0x0000220F File Offset: 0x0000040F
		public DSACng()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0000220F File Offset: 0x0000040F
		public DSACng(int keySize)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0000220F File Offset: 0x0000040F
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public DSACng(CngKey key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x000560B4 File Offset: 0x000542B4
		public CngKey Key
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000560B4 File Offset: 0x000542B4
		[SecuritySafeCritical]
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000560BC File Offset: 0x000542BC
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(DSAParameters);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0000220F File Offset: 0x0000040F
		public override void ImportParameters(DSAParameters parameters)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000560D8 File Offset: 0x000542D8
		[SecuritySafeCritical]
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
