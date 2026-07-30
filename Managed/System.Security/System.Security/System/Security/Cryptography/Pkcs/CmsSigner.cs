using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class provides signing functionality.</summary>
	// Token: 0x02000020 RID: 32
	public sealed class CmsSigner
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class by using a default subject identifier type.</summary>
		// Token: 0x0600007F RID: 127 RVA: 0x0000362C File Offset: 0x0000182C
		public CmsSigner()
		{
			this._signer = SubjectIdentifierType.IssuerAndSerialNumber;
			this._digest = new Oid("1.3.14.3.2.26");
			this._options = X509IncludeOption.ExcludeRoot;
			this._signed = new CryptographicAttributeObjectCollection();
			this._unsigned = new CryptographicAttributeObjectCollection();
			this._coll = new X509Certificate2Collection();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified subject identifier type.</summary>
		/// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the signer identifier type.</param>
		// Token: 0x06000080 RID: 128 RVA: 0x0000367E File Offset: 0x0000187E
		public CmsSigner(SubjectIdentifierType signerIdentifierType)
			: this()
		{
			if (signerIdentifierType == SubjectIdentifierType.Unknown)
			{
				this._signer = SubjectIdentifierType.IssuerAndSerialNumber;
				return;
			}
			this._signer = signerIdentifierType;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified signer identifier type and signing certificate.</summary>
		/// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the signer identifier type.</param>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</param>
		// Token: 0x06000081 RID: 129 RVA: 0x00003698 File Offset: 0x00001898
		public CmsSigner(SubjectIdentifierType signerIdentifierType, X509Certificate2 certificate)
			: this(signerIdentifierType)
		{
			this._certificate = certificate;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified signing certificate.</summary>
		/// <param name="certificate">An    <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</param>
		// Token: 0x06000082 RID: 130 RVA: 0x000036A8 File Offset: 0x000018A8
		public CmsSigner(X509Certificate2 certificate)
			: this()
		{
			this._certificate = certificate;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified cryptographic service provider (CSP) parameters. <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)" /> is useful when you know the specific CSP and private key to use for signing.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" />  object that represents the set of CSP parameters to use.</param>
		// Token: 0x06000083 RID: 131 RVA: 0x000036B7 File Offset: 0x000018B7
		[MonoTODO]
		public CmsSigner(CspParameters parameters)
			: this()
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of signed attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Signed attributes are signed along with the specified content.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000036BF File Offset: 0x000018BF
		public CryptographicAttributeObjectCollection SignedAttributes
		{
			get
			{
				return this._signed;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificate" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</summary>
		/// <returns>An  <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000036C7 File Offset: 0x000018C7
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000036CF File Offset: 0x000018CF
		public X509Certificate2 Certificate
		{
			get
			{
				return this._certificate;
			}
			set
			{
				this._certificate = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificates" /> property retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that contains certificates associated with the message to be signed.  </summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the collection of  certificates associated with the message to be signed.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000036D8 File Offset: 0x000018D8
		public X509Certificate2Collection Certificates
		{
			get
			{
				return this._coll;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.DigestAlgorithm" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid" /> that represents the hash algorithm used with the signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used with the signature.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000036E0 File Offset: 0x000018E0
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000036E8 File Offset: 0x000018E8
		public Oid DigestAlgorithm
		{
			get
			{
				return this._digest;
			}
			set
			{
				this._digest = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption" /> property sets or retrieves the option that controls whether the root and entire chain associated with the signing certificate are included with the created CMS/PKCS #7 message.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> enumeration that specifies how much of the X509 certificate chain should be included in the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object. The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption" /> property can be one of the following <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> members.NameValueMeaning<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.None" />0The certificate chain is not included.<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.ExcludeRoot" />1The certificate chain, except for the root certificate, is included.<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.EndCertOnly" />2Only the end certificate is included.<see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.WholeChain" />3The certificate chain, including the root certificate, is included.</returns>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000036F1 File Offset: 0x000018F1
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000036F9 File Offset: 0x000018F9
		public X509IncludeOption IncludeOption
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignerIdentifierType" /> property sets or retrieves the type of the identifier of the signer.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the signer.</returns>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003702 File Offset: 0x00001902
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000370A File Offset: 0x0000190A
		public SubjectIdentifierType SignerIdentifierType
		{
			get
			{
				return this._signer;
			}
			set
			{
				if (value == SubjectIdentifierType.Unknown)
				{
					throw new ArgumentException("value");
				}
				this._signer = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.UnsignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of unsigned PKCS #9 attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Unsigned attributes can be modified without invalidating the signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003721 File Offset: 0x00001921
		public CryptographicAttributeObjectCollection UnsignedAttributes
		{
			get
			{
				return this._unsigned;
			}
		}

		// Token: 0x040000B7 RID: 183
		private SubjectIdentifierType _signer;

		// Token: 0x040000B8 RID: 184
		private X509Certificate2 _certificate;

		// Token: 0x040000B9 RID: 185
		private X509Certificate2Collection _coll;

		// Token: 0x040000BA RID: 186
		private Oid _digest;

		// Token: 0x040000BB RID: 187
		private X509IncludeOption _options;

		// Token: 0x040000BC RID: 188
		private CryptographicAttributeObjectCollection _signed;

		// Token: 0x040000BD RID: 189
		private CryptographicAttributeObjectCollection _unsigned;
	}
}
