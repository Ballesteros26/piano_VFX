using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Mono.Security;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X.509 certificate.  </summary>
	// Token: 0x020003A9 RID: 937
	[Serializable]
	public class X509Certificate2 : global::System.Security.Cryptography.X509Certificates.X509Certificate
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0006FE0D File Offset: 0x0006E00D
		internal new X509Certificate2Impl Impl
		{
			get
			{
				X509Certificate2Impl x509Certificate2Impl = base.Impl as X509Certificate2Impl;
				X509Helper2.ThrowIfContextInvalid(x509Certificate2Impl);
				return x509Certificate2Impl;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class.</summary>
		// Token: 0x06001C05 RID: 7173 RVA: 0x0006FE20 File Offset: 0x0006E020
		public X509Certificate2()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using information from a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C06 RID: 7174 RVA: 0x0006FE33 File Offset: 0x0006E033
		public X509Certificate2(byte[] rawData)
		{
			this.Import(rawData, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C07 RID: 7175 RVA: 0x0006FE4F File Offset: 0x0006E04F
		public X509Certificate2(byte[] rawData, string password)
		{
			this.Import(rawData, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C08 RID: 7176 RVA: 0x0006FE6B File Offset: 0x0006E06B
		public X509Certificate2(byte[] rawData, SecureString password)
		{
			this.Import(rawData, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C09 RID: 7177 RVA: 0x0006FE87 File Offset: 0x0006E087
		public X509Certificate2(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C0A RID: 7178 RVA: 0x0006FEA3 File Offset: 0x0006E0A3
		public X509Certificate2(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C0B RID: 7179 RVA: 0x0006FEBF File Offset: 0x0006E0BF
		public X509Certificate2(string fileName)
		{
			this.Import(fileName, string.Empty, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name and a password used to access the certificate.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C0C RID: 7180 RVA: 0x0006FEDF File Offset: 0x0006E0DF
		public X509Certificate2(string fileName, string password)
		{
			this.Import(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name and a password.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C0D RID: 7181 RVA: 0x0006FEFB File Offset: 0x0006E0FB
		public X509Certificate2(string fileName, SecureString password)
		{
			this.Import(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name, a password used to access the certificate, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C0E RID: 7182 RVA: 0x0006FF17 File Offset: 0x0006E117
		public X509Certificate2(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(fileName, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name, a password, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C0F RID: 7183 RVA: 0x0006FF33 File Offset: 0x0006E133
		public X509Certificate2(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(fileName, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using an unmanaged handle.</summary>
		/// <param name="handle">A pointer to a certificate context in unmanaged code. The C structure is called PCCERT_CONTEXT.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C10 RID: 7184 RVA: 0x0006FF4F File Offset: 0x0006E14F
		public X509Certificate2(IntPtr handle)
			: base(handle)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x06001C11 RID: 7185 RVA: 0x0006FF68 File Offset: 0x0006E168
		public X509Certificate2(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate)
			: base(X509Helper2.Import(certificate, false))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using the specified serialization and stream context information. </summary>
		/// <param name="info">The serialization information required to deserialize the new <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />.</param>
		/// <param name="context">Contextual information about the source of the stream to be deserialized.</param>
		// Token: 0x06001C12 RID: 7186 RVA: 0x0006FF82 File Offset: 0x0006E182
		protected X509Certificate2(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0006FF97 File Offset: 0x0006E197
		internal X509Certificate2(X509Certificate2Impl impl)
			: base(impl)
		{
		}

		/// <summary>Gets or sets a value indicating that an X.509 certificate is archived.</summary>
		/// <returns>true if the certificate is archived, false if the certificate is not archived.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0006FFAB File Offset: 0x0006E1AB
		// (set) Token: 0x06001C15 RID: 7189 RVA: 0x0006FFB8 File Offset: 0x0006E1B8
		public bool Archived
		{
			get
			{
				return this.Impl.Archived;
			}
			set
			{
				this.Impl.Archived = true;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0006FFC6 File Offset: 0x0006E1C6
		public global::System.Security.Cryptography.X509Certificates.X509ExtensionCollection Extensions
		{
			get
			{
				return this.Impl.Extensions;
			}
		}

		/// <summary>Gets or sets the associated alias for a certificate.</summary>
		/// <returns>The certificate's friendly name.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0006FFD3 File Offset: 0x0006E1D3
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x0006FFE1 File Offset: 0x0006E1E1
		public string FriendlyName
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this.friendlyName;
			}
			set
			{
				base.ThrowIfContextInvalid();
				this.friendlyName = value;
			}
		}

		/// <summary>Gets a value that indicates whether an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object contains a private key. </summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object contains a private key; otherwise, false. </returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0006FFF0 File Offset: 0x0006E1F0
		public bool HasPrivateKey
		{
			get
			{
				return this.Impl.HasPrivateKey;
			}
		}

		/// <summary>Gets the distinguished name of the certificate issuer.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object that contains the name of the certificate issuer.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0006FFFD File Offset: 0x0006E1FD
		public X500DistinguishedName IssuerName
		{
			get
			{
				return this.Impl.IssuerName;
			}
		}

		/// <summary>Gets the date in local time after which a certificate is no longer valid.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the expiration date for the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0007000C File Offset: 0x0006E20C
		public DateTime NotAfter
		{
			get
			{
				return this.Impl.GetValidUntil().ToLocalTime();
			}
		}

		/// <summary>Gets the date in local time on which a certificate becomes valid.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the effective date of the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0007002C File Offset: 0x0006E22C
		public DateTime NotBefore
		{
			get
			{
				return this.Impl.GetValidFrom().ToLocalTime();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object that represents the private key associated with a certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object, which is either an RSA or DSA cryptographic service provider.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key value is not an RSA or DSA key, or the key is unreadable. </exception>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The key algorithm for this private key is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The X.509 keys do not match.</exception>
		/// <exception cref="T:System.ArgumentException">The cryptographic service provider key is null.</exception>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x0007004C File Offset: 0x0006E24C
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x00070059 File Offset: 0x0006E259
		public AsymmetricAlgorithm PrivateKey
		{
			get
			{
				return this.Impl.PrivateKey;
			}
			set
			{
				this.Impl.PrivateKey = value;
			}
		}

		/// <summary>Gets a <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.PublicKey" /> object associated with a certificate.</summary>
		/// <returns>A <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.PublicKey" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key value is not an RSA or DSA key, or the key is unreadable. </exception>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x00070067 File Offset: 0x0006E267
		public PublicKey PublicKey
		{
			get
			{
				return this.Impl.PublicKey;
			}
		}

		/// <summary>Gets the raw data of a certificate.</summary>
		/// <returns>The raw data of the certificate as a byte array.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x00070074 File Offset: 0x0006E274
		public byte[] RawData
		{
			get
			{
				return this.GetRawCertData();
			}
		}

		/// <summary>Gets the serial number of a certificate.</summary>
		/// <returns>The serial number of the certificate.</returns>
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0007007C File Offset: 0x0006E27C
		public string SerialNumber
		{
			get
			{
				return this.GetSerialNumberString();
			}
		}

		/// <summary>Gets the algorithm used to create the signature of a certificate.</summary>
		/// <returns>Returns the object identifier (<see cref="T:System.Security.Cryptography.Oid" />) of the signature algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00070084 File Offset: 0x0006E284
		public Oid SignatureAlgorithm
		{
			get
			{
				return this.Impl.SignatureAlgorithm;
			}
		}

		/// <summary>Gets the subject distinguished name from a certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object that represents the name of the certificate subject.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00070091 File Offset: 0x0006E291
		public X500DistinguishedName SubjectName
		{
			get
			{
				return this.Impl.SubjectName;
			}
		}

		/// <summary>Gets the thumbprint of a certificate.</summary>
		/// <returns>The thumbprint of the certificate.</returns>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0007009E File Offset: 0x0006E29E
		public string Thumbprint
		{
			get
			{
				return this.GetCertHashString();
			}
		}

		/// <summary>Gets the X.509 format version of a certificate.</summary>
		/// <returns>The certificate format.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x000700A6 File Offset: 0x0006E2A6
		public int Version
		{
			get
			{
				return this.Impl.Version;
			}
		}

		/// <summary>Gets the subject and issuer names from a certificate.</summary>
		/// <returns>The name of the certificate.</returns>
		/// <param name="nameType">The <see cref="T:System.Security.Cryptography.X509Certificates.X509NameType" /> value for the subject. </param>
		/// <param name="forIssuer">true to include the issuer name; otherwise, false. </param>
		// Token: 0x06001C26 RID: 7206 RVA: 0x000700B3 File Offset: 0x0006E2B3
		[MonoTODO("always return String.Empty for UpnName, DnsFromAlternativeName and UrlName")]
		public string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			return this.Impl.GetNameInfo(nameType, forIssuer);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with data from a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		// Token: 0x06001C27 RID: 7207 RVA: 0x000700C2 File Offset: 0x0006E2C2
		public override void Import(byte[] rawData)
		{
			this.Import(rawData, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object using data from a byte array, a password, and flags for determining how to import the private key.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		// Token: 0x06001C28 RID: 7208 RVA: 0x000700D0 File Offset: 0x0006E2D0
		[MonoTODO("missing KeyStorageFlags support")]
		public override void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			X509Certificate2Impl x509Certificate2Impl = X509Helper2.Import(rawData, password, keyStorageFlags, false);
			base.ImportHandle(x509Certificate2Impl);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object using data from a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		// Token: 0x06001C29 RID: 7209 RVA: 0x000700EE File Offset: 0x0006E2EE
		[MonoTODO("SecureString is incomplete")]
		public override void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(rawData, null, keyStorageFlags);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with information from a certificate file.</summary>
		/// <param name="fileName">The name of a certificate. </param>
		// Token: 0x06001C2A RID: 7210 RVA: 0x000700FC File Offset: 0x0006E2FC
		public override void Import(string fileName)
		{
			byte[] array = File.ReadAllBytes(fileName);
			this.Import(array, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with information from a certificate file, a password, and a <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> value.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		// Token: 0x06001C2B RID: 7211 RVA: 0x0007011C File Offset: 0x0006E31C
		[MonoTODO("missing KeyStorageFlags support")]
		public override void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			byte[] array = File.ReadAllBytes(fileName);
			this.Import(array, password, keyStorageFlags);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with information from a certificate file, a password, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		// Token: 0x06001C2C RID: 7212 RVA: 0x0007013C File Offset: 0x0006E33C
		[MonoTODO("SecureString is incomplete")]
		public override void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			byte[] array = File.ReadAllBytes(fileName);
			this.Import(array, null, keyStorageFlags);
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00070159 File Offset: 0x0006E359
		[MonoTODO("X509ContentType.SerializedCert is not supported")]
		public override byte[] Export(X509ContentType contentType, string password)
		{
			return this.Impl.Export(contentType, password);
		}

		/// <summary>Resets the state of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		// Token: 0x06001C2E RID: 7214 RVA: 0x00070168 File Offset: 0x0006E368
		public override void Reset()
		{
			this.friendlyName = string.Empty;
			base.Reset();
		}

		/// <summary>Displays an X.509 certificate in text format.</summary>
		/// <returns>The certificate information.</returns>
		// Token: 0x06001C2F RID: 7215 RVA: 0x0007017B File Offset: 0x0006E37B
		public override string ToString()
		{
			if (!base.IsValid)
			{
				return "System.Security.Cryptography.X509Certificates.X509Certificate2";
			}
			return base.ToString(true);
		}

		/// <summary>Displays an X.509 certificate in text format.</summary>
		/// <returns>The certificate information.</returns>
		/// <param name="verbose">true to display the public key, private key, extensions, and so forth; false to display information that is similar to the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class, including thumbprint, serial number, subject and issuer names, and so on. </param>
		// Token: 0x06001C30 RID: 7216 RVA: 0x00070194 File Offset: 0x0006E394
		public override string ToString(bool verbose)
		{
			if (!base.IsValid)
			{
				return "System.Security.Cryptography.X509Certificates.X509Certificate2";
			}
			if (!verbose)
			{
				return base.ToString(true);
			}
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[Version]{0}  V{1}{0}{0}", newLine, this.Version);
			stringBuilder.AppendFormat("[Subject]{0}  {1}{0}{0}", newLine, base.Subject);
			stringBuilder.AppendFormat("[Issuer]{0}  {1}{0}{0}", newLine, base.Issuer);
			stringBuilder.AppendFormat("[Serial Number]{0}  {1}{0}{0}", newLine, this.SerialNumber);
			stringBuilder.AppendFormat("[Not Before]{0}  {1}{0}{0}", newLine, this.NotBefore);
			stringBuilder.AppendFormat("[Not After]{0}  {1}{0}{0}", newLine, this.NotAfter);
			stringBuilder.AppendFormat("[Thumbprint]{0}  {1}{0}{0}", newLine, this.Thumbprint);
			stringBuilder.AppendFormat("[Signature Algorithm]{0}  {1}({2}){0}{0}", newLine, this.SignatureAlgorithm.FriendlyName, this.SignatureAlgorithm.Value);
			AsymmetricAlgorithm key = this.PublicKey.Key;
			stringBuilder.AppendFormat("[Public Key]{0}  Algorithm: ", newLine);
			if (key is RSA)
			{
				stringBuilder.Append("RSA");
			}
			else if (key is DSA)
			{
				stringBuilder.Append("DSA");
			}
			else
			{
				stringBuilder.Append(key.ToString());
			}
			stringBuilder.AppendFormat("{0}  Length: {1}{0}  Key Blob: ", newLine, key.KeySize);
			X509Certificate2.AppendBuffer(stringBuilder, this.PublicKey.EncodedKeyValue.RawData);
			stringBuilder.AppendFormat("{0}  Parameters: ", newLine);
			X509Certificate2.AppendBuffer(stringBuilder, this.PublicKey.EncodedParameters.RawData);
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00070330 File Offset: 0x0006E530
		private static void AppendBuffer(StringBuilder sb, byte[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				sb.Append(buffer[i].ToString("x2"));
				if (i < buffer.Length - 1)
				{
					sb.Append(" ");
				}
			}
		}

		/// <summary>Performs a X.509 chain validation using basic validation policy.</summary>
		/// <returns>true if the validation succeeds; false if the validation fails.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable. </exception>
		// Token: 0x06001C32 RID: 7218 RVA: 0x0007037B File Offset: 0x0006E57B
		[MonoTODO("by default this depends on the incomplete X509Chain")]
		public bool Verify()
		{
			return this.Impl.Verify(this);
		}

		/// <summary>Indicates the type of certificate contained in a byte array.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</returns>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rawData" /> has a zero length or is null. </exception>
		// Token: 0x06001C33 RID: 7219 RVA: 0x0007038C File Offset: 0x0006E58C
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12, Pkcs7 and Unknown")]
		public static X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("rawData");
			}
			X509ContentType x509ContentType = X509ContentType.Unknown;
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(rawData);
				if (asn.Tag != 48)
				{
					throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."));
				}
				if (asn.Count == 0)
				{
					return x509ContentType;
				}
				if (asn.Count == 3)
				{
					byte tag = asn[0].Tag;
					if (tag != 2)
					{
						if (tag == 48 && asn[1].Tag == 48 && asn[2].Tag == 3)
						{
							x509ContentType = X509ContentType.Cert;
						}
					}
					else if (asn[1].Tag == 48 && asn[2].Tag == 48)
					{
						x509ContentType = X509ContentType.Pfx;
					}
				}
				if (asn[0].Tag == 6 && asn[0].CompareValue(X509Certificate2.signedData))
				{
					x509ContentType = X509ContentType.Pkcs7;
				}
			}
			catch (Exception ex)
			{
				throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."), ex);
			}
			return x509ContentType;
		}

		/// <summary>Indicates the type of certificate contained in a file.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</returns>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		// Token: 0x06001C34 RID: 7220 RVA: 0x00070490 File Offset: 0x0006E690
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12 and Unknown")]
		public static X509ContentType GetCertContentType(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("fileName");
			}
			return X509Certificate2.GetCertContentType(File.ReadAllBytes(fileName));
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x000704BE File Offset: 0x0006E6BE
		[MonoTODO("See comment in X509Helper2.GetMonoCertificate().")]
		internal Mono.Security.X509.X509Certificate MonoCertificate
		{
			get
			{
				return X509Helper2.GetMonoCertificate(this);
			}
		}

		// Token: 0x04001990 RID: 6544
		private string friendlyName = string.Empty;

		// Token: 0x04001991 RID: 6545
		private static byte[] signedData = new byte[] { 42, 134, 72, 134, 247, 13, 1, 7, 2 };
	}
}
