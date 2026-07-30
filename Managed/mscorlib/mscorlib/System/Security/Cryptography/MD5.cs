using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm inherit. </summary>
	// Token: 0x0200066E RID: 1646
	[ComVisible(true)]
	public abstract class MD5 : HashAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.MD5" />.</summary>
		// Token: 0x060046AF RID: 18095 RVA: 0x00017949 File Offset: 0x00015B49
		protected MD5()
		{
			this.HashSizeValue = 128;
		}

		/// <summary>Creates an instance of the default implementation of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm.</summary>
		/// <returns>A new instance of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060046B0 RID: 18096 RVA: 0x000F890C File Offset: 0x000F6B0C
		public new static MD5 Create()
		{
			return MD5.Create("System.Security.Cryptography.MD5");
		}

		/// <summary>Creates an instance of the specified implementation of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm.</summary>
		/// <returns>A new instance of the specified implementation of <see cref="T:System.Security.Cryptography.MD5" />.</returns>
		/// <param name="algName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.MD5" /> to use. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="algName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x060046B1 RID: 18097 RVA: 0x000F8918 File Offset: 0x000F6B18
		public new static MD5 Create(string algName)
		{
			return (MD5)CryptoConfig.CreateFromName(algName);
		}
	}
}
