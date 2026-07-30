using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains information about the properties of a digital signature.</summary>
	// Token: 0x0200068A RID: 1674
	[ComVisible(true)]
	public class SignatureDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SignatureDescription" /> class.</summary>
		// Token: 0x060047DA RID: 18394 RVA: 0x00002111 File Offset: 0x00000311
		public SignatureDescription()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SignatureDescription" /> class from the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="el">The <see cref="T:System.Security.SecurityElement" /> from which to get the algorithms for the signature description. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="el" /> parameter is null.</exception>
		// Token: 0x060047DB RID: 18395 RVA: 0x00100D54 File Offset: 0x000FEF54
		public SignatureDescription(SecurityElement el)
		{
			if (el == null)
			{
				throw new ArgumentNullException("el");
			}
			this._strKey = el.SearchForTextOfTag("Key");
			this._strDigest = el.SearchForTextOfTag("Digest");
			this._strFormatter = el.SearchForTextOfTag("Formatter");
			this._strDeformatter = el.SearchForTextOfTag("Deformatter");
		}

		/// <summary>Gets or sets the key algorithm for the signature description.</summary>
		/// <returns>The key algorithm for the signature description.</returns>
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x060047DC RID: 18396 RVA: 0x00100DB9 File Offset: 0x000FEFB9
		// (set) Token: 0x060047DD RID: 18397 RVA: 0x00100DC1 File Offset: 0x000FEFC1
		public string KeyAlgorithm
		{
			get
			{
				return this._strKey;
			}
			set
			{
				this._strKey = value;
			}
		}

		/// <summary>Gets or sets the digest algorithm for the signature description.</summary>
		/// <returns>The digest algorithm for the signature description.</returns>
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x00100DCA File Offset: 0x000FEFCA
		// (set) Token: 0x060047DF RID: 18399 RVA: 0x00100DD2 File Offset: 0x000FEFD2
		public string DigestAlgorithm
		{
			get
			{
				return this._strDigest;
			}
			set
			{
				this._strDigest = value;
			}
		}

		/// <summary>Gets or sets the formatter algorithm for the signature description.</summary>
		/// <returns>The formatter algorithm for the signature description.</returns>
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x00100DDB File Offset: 0x000FEFDB
		// (set) Token: 0x060047E1 RID: 18401 RVA: 0x00100DE3 File Offset: 0x000FEFE3
		public string FormatterAlgorithm
		{
			get
			{
				return this._strFormatter;
			}
			set
			{
				this._strFormatter = value;
			}
		}

		/// <summary>Gets or sets the deformatter algorithm for the signature description.</summary>
		/// <returns>The deformatter algorithm for the signature description.</returns>
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x00100DEC File Offset: 0x000FEFEC
		// (set) Token: 0x060047E3 RID: 18403 RVA: 0x00100DF4 File Offset: 0x000FEFF4
		public string DeformatterAlgorithm
		{
			get
			{
				return this._strDeformatter;
			}
			set
			{
				this._strDeformatter = value;
			}
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> instance with the specified key using the <see cref="P:System.Security.Cryptography.SignatureDescription.DeformatterAlgorithm" /> property.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> instance.</returns>
		/// <param name="key">The key to use in the <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" />. </param>
		// Token: 0x060047E4 RID: 18404 RVA: 0x00100DFD File Offset: 0x000FEFFD
		public virtual AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this._strDeformatter);
			asymmetricSignatureDeformatter.SetKey(key);
			return asymmetricSignatureDeformatter;
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> instance with the specified key using the <see cref="P:System.Security.Cryptography.SignatureDescription.FormatterAlgorithm" /> property.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> instance.</returns>
		/// <param name="key">The key to use in the <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" />. </param>
		// Token: 0x060047E5 RID: 18405 RVA: 0x00100E16 File Offset: 0x000FF016
		public virtual AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this._strFormatter);
			asymmetricSignatureFormatter.SetKey(key);
			return asymmetricSignatureFormatter;
		}

		/// <summary>Creates a <see cref="T:System.Security.Cryptography.HashAlgorithm" /> instance using the <see cref="P:System.Security.Cryptography.SignatureDescription.DigestAlgorithm" /> property.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.HashAlgorithm" /> instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060047E6 RID: 18406 RVA: 0x00100E2F File Offset: 0x000FF02F
		public virtual HashAlgorithm CreateDigest()
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(this._strDigest);
		}

		// Token: 0x040024C4 RID: 9412
		private string _strKey;

		// Token: 0x040024C5 RID: 9413
		private string _strDigest;

		// Token: 0x040024C6 RID: 9414
		private string _strFormatter;

		// Token: 0x040024C7 RID: 9415
		private string _strDeformatter;
	}
}
