using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class represents a CMS/PKCS #7 structure for enveloped data.</summary>
	// Token: 0x02000022 RID: 34
	public sealed class EnvelopedCms
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x06000095 RID: 149 RVA: 0x0000386C File Offset: 0x00001A6C
		public EnvelopedCms()
		{
			this._certs = new X509Certificate2Collection();
			this._recipients = new RecipientInfoCollection();
			this._uattribs = new CryptographicAttributeObjectCollection();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified content information as the inner content type.</summary>
		/// <param name="contentInfo">An instance of the <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo" /> class that represents the content and its type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x06000096 RID: 150 RVA: 0x00003895 File Offset: 0x00001A95
		public EnvelopedCms(ContentInfo contentInfo)
			: this()
		{
			if (contentInfo == null)
			{
				throw new ArgumentNullException("contentInfo");
			}
			this._content = contentInfo;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified content information and encryption algorithm. The specified content information is to be used as the inner content type.</summary>
		/// <param name="contentInfo">A  <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that specifies the encryption algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x06000097 RID: 151 RVA: 0x000038B2 File Offset: 0x00001AB2
		public EnvelopedCms(ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm)
			: this(contentInfo)
		{
			if (encryptionAlgorithm == null)
			{
				throw new ArgumentNullException("encryptionAlgorithm");
			}
			this._identifier = encryptionAlgorithm;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified subject identifier type and content information. The specified content information is to be used as the inner content type.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the means of identifying the recipient.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x06000098 RID: 152 RVA: 0x000038D0 File Offset: 0x00001AD0
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo)
			: this(contentInfo)
		{
			this._idType = recipientIdentifierType;
			if (this._idType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				this._version = 2;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified subject identifier type, content information, and encryption algorithm. The specified content information is to be used as the inner content type.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the means of identifying the recipient.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that specifies the encryption algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		// Token: 0x06000099 RID: 153 RVA: 0x000038F0 File Offset: 0x00001AF0
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm)
			: this(contentInfo, encryptionAlgorithm)
		{
			this._idType = recipientIdentifierType;
			if (this._idType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				this._version = 2;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Certificates" /> property retrieves the set of certificates associated with the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the X.509 certificates used with the enveloped CMS/PKCS #7 message. If no certificates exist, the property value is an empty collection.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003911 File Offset: 0x00001B11
		public X509Certificate2Collection Certificates
		{
			get
			{
				return this._certs;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentEncryptionAlgorithm" /> property retrieves the identifier of the algorithm used to encrypt the content.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that represents the algorithm identifier.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003919 File Offset: 0x00001B19
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			get
			{
				if (this._identifier == null)
				{
					this._identifier = new AlgorithmIdentifier();
				}
				return this._identifier;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo" /> property retrieves the inner content information for the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the inner content information from the enveloped CMS/PKCS #7 message.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003934 File Offset: 0x00001B34
		public ContentInfo ContentInfo
		{
			get
			{
				if (this._content == null)
				{
					Oid oid = new Oid("1.2.840.113549.1.7.1");
					this._content = new ContentInfo(oid, new byte[0]);
				}
				return this._content;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.RecipientInfos" /> property retrieves the recipient information associated with the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection that represents the recipient information. If no recipients exist, the property value is an empty collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000396C File Offset: 0x00001B6C
		public RecipientInfoCollection RecipientInfos
		{
			get
			{
				return this._recipients;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.UnprotectedAttributes" /> property retrieves the unprotected (unencrypted) attributes associated with the enveloped CMS/PKCS #7 message. Unprotected attributes are not encrypted, and so do not have data confidentiality within an <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the unprotected attributes. If no unprotected attributes exist, the property value is an empty collection.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003974 File Offset: 0x00001B74
		public CryptographicAttributeObjectCollection UnprotectedAttributes
		{
			get
			{
				return this._uattribs;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Version" /> property retrieves the version of the enveloped CMS/PKCS #7 message.  </summary>
		/// <returns>An int value that represents the version of the enveloped CMS/PKCS #7 message.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000397C File Offset: 0x00001B7C
		public int Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003984 File Offset: 0x00001B84
		private X509IssuerSerial GetIssuerSerial(string issuer, byte[] serial)
		{
			X509IssuerSerial x509IssuerSerial = default(X509IssuerSerial);
			x509IssuerSerial.IssuerName = issuer;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in serial)
			{
				stringBuilder.Append(b.ToString("X2"));
			}
			x509IssuerSerial.SerialNumber = stringBuilder.ToString();
			return x509IssuerSerial;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decode(System.Byte[])" /> method decodes the specified enveloped CMS/PKCS #7 message and resets all member variables in the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> object.</summary>
		/// <param name="encodedMessage">An array of byte values that represent the information to be decoded.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		// Token: 0x060000A1 RID: 161 RVA: 0x000039E0 File Offset: 0x00001BE0
		[MonoTODO]
		public void Decode(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("encodedMessage");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(encodedMessage);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.3")
			{
				throw new Exception("");
			}
			PKCS7.EnvelopedData envelopedData = new PKCS7.EnvelopedData(contentInfo.Content);
			Oid oid = new Oid(envelopedData.ContentInfo.ContentType);
			this._content = new ContentInfo(oid, new byte[0]);
			foreach (object obj in envelopedData.RecipientInfos)
			{
				PKCS7.RecipientInfo recipientInfo = (PKCS7.RecipientInfo)obj;
				AlgorithmIdentifier algorithmIdentifier = new AlgorithmIdentifier(new Oid(recipientInfo.Oid));
				SubjectIdentifier subjectIdentifier = null;
				if (recipientInfo.SubjectKeyIdentifier != null)
				{
					subjectIdentifier = new SubjectIdentifier(SubjectIdentifierType.SubjectKeyIdentifier, recipientInfo.SubjectKeyIdentifier);
				}
				else if (recipientInfo.Issuer != null && recipientInfo.Serial != null)
				{
					X509IssuerSerial issuerSerial = this.GetIssuerSerial(recipientInfo.Issuer, recipientInfo.Serial);
					subjectIdentifier = new SubjectIdentifier(SubjectIdentifierType.IssuerAndSerialNumber, issuerSerial);
				}
				KeyTransRecipientInfo keyTransRecipientInfo = new KeyTransRecipientInfo(recipientInfo.Key, algorithmIdentifier, subjectIdentifier, recipientInfo.Version);
				this._recipients.Add(keyTransRecipientInfo);
			}
			this._version = (int)envelopedData.Version;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt" /> method searches the current user and computer My stores for the appropriate certificate and private key.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates" />
		/// </PermissionSet>
		// Token: 0x060000A2 RID: 162 RVA: 0x00003B2C File Offset: 0x00001D2C
		[MonoTODO]
		public void Decrypt()
		{
			throw new InvalidOperationException("not encrypted");
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information.</summary>
		/// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the recipient information that identifies the certificate associated with the private key to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060000A3 RID: 163 RVA: 0x00003B38 File Offset: 0x00001D38
		[MonoTODO]
		public void Decrypt(RecipientInfo recipientInfo)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			this.Decrypt();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information and by using the specified certificate collection.  The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.</summary>
		/// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the recipient information to use for the decryption.</param>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060000A4 RID: 164 RVA: 0x00003B4E File Offset: 0x00001D4E
		[MonoTODO]
		public void Decrypt(RecipientInfo recipientInfo, X509Certificate2Collection extraStore)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			this.Decrypt();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the specified certificate collection. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060000A5 RID: 165 RVA: 0x00003B72 File Offset: 0x00001D72
		[MonoTODO]
		public void Decrypt(X509Certificate2Collection extraStore)
		{
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			this.Decrypt();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encode" /> method encodes the contents of the enveloped CMS/PKCS #7 message and returns it as an array of byte values. Encryption must be done before encoding.</summary>
		/// <returns>If the method succeeds, the method returns an array of byte values that represent the encoded information.If the method fails, it throws an exception.</returns>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060000A6 RID: 166 RVA: 0x00003B2C File Offset: 0x00001D2C
		[MonoTODO]
		public byte[] Encode()
		{
			throw new InvalidOperationException("not encrypted");
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt" /> method encrypts the contents of the CMS/PKCS #7 message.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates" />
		/// </PermissionSet>
		// Token: 0x060000A7 RID: 167 RVA: 0x00003B88 File Offset: 0x00001D88
		[MonoTODO]
		public void Encrypt()
		{
			if (this._content == null || this._content.Content == null || this._content.Content.Length == 0)
			{
				throw new CryptographicException("no content to encrypt");
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method encrypts the contents of the CMS/PKCS #7 message by using the specified recipient information.</summary>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient information.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x060000A8 RID: 168 RVA: 0x00003BB8 File Offset: 0x00001DB8
		[MonoTODO]
		public void Encrypt(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			this.Encrypt();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipientCollection)" /> method encrypts the contents of the CMS/PKCS #7 message by using the information for the specified list of recipients. The message is encrypted by using a message encryption key with a symmetric encryption algorithm such as triple DES. The message encryption key is then encrypted with the public key of each recipient.</summary>
		/// <param name="recipients">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection that represents the information for the list of recipients.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x060000A9 RID: 169 RVA: 0x00003BCE File Offset: 0x00001DCE
		[MonoTODO]
		public void Encrypt(CmsRecipientCollection recipients)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
		}

		// Token: 0x040000C0 RID: 192
		private ContentInfo _content;

		// Token: 0x040000C1 RID: 193
		private AlgorithmIdentifier _identifier;

		// Token: 0x040000C2 RID: 194
		private X509Certificate2Collection _certs;

		// Token: 0x040000C3 RID: 195
		private RecipientInfoCollection _recipients;

		// Token: 0x040000C4 RID: 196
		private CryptographicAttributeObjectCollection _uattribs;

		// Token: 0x040000C5 RID: 197
		private SubjectIdentifierType _idType;

		// Token: 0x040000C6 RID: 198
		private int _version;
	}
}
