using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005A8 RID: 1448
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06004078 RID: 16504 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public PublisherIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a certification file containing an Authenticode X.509v3 certificate.</summary>
		/// <returns>The file path of an X.509 certificate file (usually has the extension.cer).</returns>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x000E5BC9 File Offset: 0x000E3DC9
		// (set) Token: 0x0600407A RID: 16506 RVA: 0x000E5BD1 File Offset: 0x000E3DD1
		public string CertFile
		{
			get
			{
				return this.certFile;
			}
			set
			{
				this.certFile = value;
			}
		}

		/// <summary>Gets or sets a signed file from which to extract an Authenticode X.509v3 certificate.</summary>
		/// <returns>The file path of a file signed with the Authenticode signature.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x000E5BDA File Offset: 0x000E3DDA
		// (set) Token: 0x0600407C RID: 16508 RVA: 0x000E5BE2 File Offset: 0x000E3DE2
		public string SignedFile
		{
			get
			{
				return this.signedFile;
			}
			set
			{
				this.signedFile = value;
			}
		}

		/// <summary>Gets or sets an Authenticode X.509v3 certificate that identifies the publisher of the calling code.</summary>
		/// <returns>A hexadecimal representation of the X.509 certificate.</returns>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x000E5BEB File Offset: 0x000E3DEB
		// (set) Token: 0x0600407E RID: 16510 RVA: 0x000E5BF3 File Offset: 0x000E3DF3
		public string X509Certificate
		{
			get
			{
				return this.x509data;
			}
			set
			{
				this.x509data = value;
			}
		}

		/// <summary>Creates and returns a new instance of <see cref="T:System.Security.Permissions.PublisherIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> that corresponds to this attribute.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x0600407F RID: 16511 RVA: 0x000E5BFC File Offset: 0x000E3DFC
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PublisherIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.x509data != null)
			{
				return new PublisherIdentityPermission(new X509Certificate(CryptoConvert.FromHex(this.x509data)));
			}
			if (this.certFile != null)
			{
				return new PublisherIdentityPermission(global::System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.certFile));
			}
			if (this.signedFile != null)
			{
				return new PublisherIdentityPermission(global::System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.signedFile));
			}
			return new PublisherIdentityPermission(PermissionState.None);
		}

		// Token: 0x040020AA RID: 8362
		private string certFile;

		// Token: 0x040020AB RID: 8363
		private string signedFile;

		// Token: 0x040020AC RID: 8364
		private string x509data;
	}
}
