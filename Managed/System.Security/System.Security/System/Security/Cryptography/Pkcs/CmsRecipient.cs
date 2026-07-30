using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class defines the recipient of a CMS/PKCS #7 message.</summary>
	// Token: 0x0200001D RID: 29
	public sealed class CmsRecipient
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipient.#ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor constructs an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class by using the specified recipient certificate.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the recipient certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000068 RID: 104 RVA: 0x000034A0 File Offset: 0x000016A0
		public CmsRecipient(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			this._recipient = SubjectIdentifierType.IssuerAndSerialNumber;
			this._certificate = certificate;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipient.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor constructs an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class by using the specified recipient identifier type and recipient certificate.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the recipient.</param>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the recipient certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000069 RID: 105 RVA: 0x000034C4 File Offset: 0x000016C4
		public CmsRecipient(SubjectIdentifierType recipientIdentifierType, X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (recipientIdentifierType == SubjectIdentifierType.Unknown)
			{
				this._recipient = SubjectIdentifierType.IssuerAndSerialNumber;
			}
			else
			{
				this._recipient = recipientIdentifierType;
			}
			this._certificate = certificate;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipient.Certificate" /> property retrieves the certificate associated with the recipient.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that holds the certificate associated with the recipient.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000034F4 File Offset: 0x000016F4
		public X509Certificate2 Certificate
		{
			get
			{
				return this._certificate;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipient.RecipientIdentifierType" /> property retrieves the type of the identifier of the recipient.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the recipient.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000034FC File Offset: 0x000016FC
		public SubjectIdentifierType RecipientIdentifierType
		{
			get
			{
				return this._recipient;
			}
		}

		// Token: 0x040000B3 RID: 179
		private SubjectIdentifierType _recipient;

		// Token: 0x040000B4 RID: 180
		private X509Certificate2 _certificate;
	}
}
