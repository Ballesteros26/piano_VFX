using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides simple data protection methods.</summary>
	// Token: 0x02000092 RID: 146
	public sealed class DpapiDataProtector : DataProtector
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Security.Cryptography.DpapiDataProtector" /> class by using the specified application name, primary purpose, and specific purposes.</summary>
		/// <param name="appName">The name of the application.</param>
		/// <param name="primaryPurpose">The primary purpose for the data protector.</param>
		/// <param name="specificPurpose">The specific purpose(s) for the data protector.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="appName" /> is an empty string or null.-or-<paramref name="primaryPurpose" /> is an empty string or null.-or-<paramref name="specificPurposes" /> contains an empty string or null.</exception>
		// Token: 0x06000460 RID: 1120 RVA: 0x00002FF8 File Offset: 0x000011F8
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Demand, Unrestricted = true)]
		public DpapiDataProtector(string appName, string primaryPurpose, string[] specificPurpose)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x000123B4 File Offset: 0x000105B4
		protected override bool PrependHashedPurposeToPlaintext
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the scope of the data protection.</summary>
		/// <returns>One of the enumeration values that specifies the scope of the data protection (either the current user or the local machine). The default is <see cref="F:System.Security.Cryptography.DataProtectionScope.CurrentUser" />.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000123D0 File Offset: 0x000105D0
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00002FF8 File Offset: 0x000011F8
		public DataProtectionScope Scope
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return DataProtectionScope.CurrentUser;
			}
			[CompilerGenerated]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Determines if the data must be re-encrypted.</summary>
		/// <returns>true if the data must be re-encrypted; otherwise, false.</returns>
		/// <param name="encryptedData">The encrypted data to be checked.</param>
		// Token: 0x06000464 RID: 1124 RVA: 0x000123EC File Offset: 0x000105EC
		public override bool IsReprotectRequired(byte[] encryptedData)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00012390 File Offset: 0x00010590
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Assert, ProtectData = true)]
		protected override byte[] ProviderProtect(byte[] userData)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00012390 File Offset: 0x00010590
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Assert, UnprotectData = true)]
		protected override byte[] ProviderUnprotect(byte[] encryptedData)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
