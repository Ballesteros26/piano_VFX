using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Mono.Security.Authenticode;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides methods that help you use X.509 v.3 certificates.</summary>
	// Token: 0x020006AD RID: 1709
	[ComVisible(true)]
	[MonoTODO("X509ContentType.SerializedCert isn't supported (anywhere in the class)")]
	[Serializable]
	public class X509Certificate : IDeserializationCallback, ISerializable, IDisposable
	{
		/// <summary>Creates an X.509v3 certificate from the specified PKCS7 signed file.</summary>
		/// <returns>The newly created X.509 certificate.</returns>
		/// <param name="filename">The path of the PKCS7 signed file from which to create the X.509 certificate. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="filename" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x060048C8 RID: 18632 RVA: 0x00106712 File Offset: 0x00104912
		public static X509Certificate CreateFromCertFile(string filename)
		{
			return new X509Certificate(File.ReadAllBytes(filename));
		}

		/// <summary>Creates an X.509v3 certificate from the specified signed file.</summary>
		/// <returns>The newly created X.509 certificate.</returns>
		/// <param name="filename">The path of the signed file from which to create the X.509 certificate. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x060048C9 RID: 18633 RVA: 0x00106720 File Offset: 0x00104920
		[MonoTODO("Incomplete - minimal validation in this version")]
		public static X509Certificate CreateFromSignedFile(string filename)
		{
			try
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(filename);
				if (authenticodeDeformatter.SigningCertificate != null)
				{
					return new X509Certificate(authenticodeDeformatter.SigningCertificate.RawData);
				}
			}
			catch (SecurityException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new COMException(Locale.GetText("Couldn't extract digital signature from {0}.", new object[] { filename }), ex);
			}
			throw new CryptographicException(Locale.GetText("{0} isn't signed.", new object[] { filename }));
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x001067A8 File Offset: 0x001049A8
		internal X509Certificate(byte[] data, bool dates)
		{
			if (data != null)
			{
				this.Import(data, null, X509KeyStorageFlags.DefaultKeySet);
				this.hideDates = !dates;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class defined from a sequence of bytes representing an X.509v3 certificate.</summary>
		/// <param name="data">A byte array containing data from an X.509 certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060048CB RID: 18635 RVA: 0x001067C6 File Offset: 0x001049C6
		public X509Certificate(byte[] data)
			: this(data, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a handle to an unmanaged PCCERT_CONTEXT structure.</summary>
		/// <param name="handle">A handle to an unmanaged PCCERT_CONTEXT structure.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The handle parameter does not represent a valid PCCERT_CONTEXT structure.</exception>
		// Token: 0x060048CC RID: 18636 RVA: 0x001067D0 File Offset: 0x001049D0
		public X509Certificate(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException("Invalid handle.");
			}
			this.impl = X509Helper.InitFromHandle(handle);
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x001067FC File Offset: 0x001049FC
		internal X509Certificate(X509CertificateImpl impl)
		{
			if (impl == null)
			{
				throw new ArgumentNullException("impl");
			}
			this.impl = X509Helper.InitFromCertificate(impl);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class.</summary>
		/// <param name="cert">A <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class from which to initialize this class. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="cert" /> parameter is null.</exception>
		// Token: 0x060048CE RID: 18638 RVA: 0x0010681E File Offset: 0x00104A1E
		public X509Certificate(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			this.impl = X509Helper.InitFromCertificate(cert);
			this.hideDates = false;
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x00106847 File Offset: 0x00104A47
		internal void ImportHandle(X509CertificateImpl impl)
		{
			this.Reset();
			this.impl = impl;
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060048D0 RID: 18640 RVA: 0x00106856 File Offset: 0x00104A56
		internal X509CertificateImpl Impl
		{
			get
			{
				X509Helper.ThrowIfContextInvalid(this.impl);
				return this.impl;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x00106869 File Offset: 0x00104A69
		internal bool IsValid
		{
			get
			{
				return X509Helper.IsValid(this.impl);
			}
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x00106876 File Offset: 0x00104A76
		internal void ThrowIfContextInvalid()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
		}

		/// <summary>Compares two <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects for equality.</summary>
		/// <returns>true if the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object is equal to the object specified by the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="other">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to compare to the current object.</param>
		// Token: 0x060048D3 RID: 18643 RVA: 0x00106884 File Offset: 0x00104A84
		public virtual bool Equals(X509Certificate other)
		{
			if (other == null)
			{
				return false;
			}
			if (X509Helper.IsValid(other.impl))
			{
				return object.Equals(this.impl, other.impl);
			}
			if (!X509Helper.IsValid(this.impl))
			{
				return true;
			}
			throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
		}

		/// <summary>Returns the hash value for the X.509v3 certificate as an array of bytes.</summary>
		/// <returns>The hash value for the X.509 certificate.</returns>
		// Token: 0x060048D4 RID: 18644 RVA: 0x001068D3 File Offset: 0x00104AD3
		public virtual byte[] GetCertHash()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
			return this.impl.GetCertHash();
		}

		/// <summary>Returns the SHA1 hash value for the X.509v3 certificate as a hexadecimal string.</summary>
		/// <returns>The hexadecimal string representation of the X.509 certificate hash value.</returns>
		// Token: 0x060048D5 RID: 18645 RVA: 0x001068EB File Offset: 0x00104AEB
		public virtual string GetCertHashString()
		{
			return X509Helper.ToHexString(this.GetCertHash());
		}

		/// <summary>Returns the effective date of this X.509v3 certificate.</summary>
		/// <returns>The effective date for this X.509 certificate.</returns>
		// Token: 0x060048D6 RID: 18646 RVA: 0x001068F8 File Offset: 0x00104AF8
		public virtual string GetEffectiveDateString()
		{
			if (this.hideDates)
			{
				return null;
			}
			X509Helper.ThrowIfContextInvalid(this.impl);
			return this.impl.GetValidFrom().ToLocalTime().ToString();
		}

		/// <summary>Returns the expiration date of this X.509v3 certificate.</summary>
		/// <returns>The expiration date for this X.509 certificate.</returns>
		// Token: 0x060048D7 RID: 18647 RVA: 0x00106938 File Offset: 0x00104B38
		public virtual string GetExpirationDateString()
		{
			if (this.hideDates)
			{
				return null;
			}
			X509Helper.ThrowIfContextInvalid(this.impl);
			return this.impl.GetValidUntil().ToLocalTime().ToString();
		}

		/// <summary>Returns the name of the format of this X.509v3 certificate.</summary>
		/// <returns>The format of this X.509 certificate.</returns>
		// Token: 0x060048D8 RID: 18648 RVA: 0x00106975 File Offset: 0x00104B75
		public virtual string GetFormat()
		{
			return "X509";
		}

		/// <summary>Returns the hash code for the X.509v3 certificate as an integer.</summary>
		/// <returns>The hash code for the X.509 certificate as an integer.</returns>
		// Token: 0x060048D9 RID: 18649 RVA: 0x0010697C File Offset: 0x00104B7C
		public override int GetHashCode()
		{
			if (!X509Helper.IsValid(this.impl))
			{
				return 0;
			}
			return this.impl.GetHashCode();
		}

		/// <summary>Returns the name of the certification authority that issued the X.509v3 certificate.</summary>
		/// <returns>The name of the certification authority that issued the X.509 certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x060048DA RID: 18650 RVA: 0x00106998 File Offset: 0x00104B98
		[Obsolete("Use the Issuer property.")]
		public virtual string GetIssuerName()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
			return this.impl.GetIssuerName(true);
		}

		/// <summary>Returns the key algorithm information for this X.509v3 certificate.</summary>
		/// <returns>The key algorithm information for this X.509 certificate as a string.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060048DB RID: 18651 RVA: 0x001069B1 File Offset: 0x00104BB1
		public virtual string GetKeyAlgorithm()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
			return this.impl.GetKeyAlgorithm();
		}

		/// <summary>Returns the key algorithm parameters for the X.509v3 certificate.</summary>
		/// <returns>The key algorithm parameters for the X.509 certificate as an array of bytes.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060048DC RID: 18652 RVA: 0x001069C9 File Offset: 0x00104BC9
		public virtual byte[] GetKeyAlgorithmParameters()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
			byte[] keyAlgorithmParameters = this.impl.GetKeyAlgorithmParameters();
			if (keyAlgorithmParameters == null)
			{
				throw new CryptographicException(Locale.GetText("Parameters not part of the certificate"));
			}
			return keyAlgorithmParameters;
		}

		/// <summary>Returns the key algorithm parameters for the X.509v3 certificate.</summary>
		/// <returns>The key algorithm parameters for the X.509 certificate as a hexadecimal string.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060048DD RID: 18653 RVA: 0x001069F4 File Offset: 0x00104BF4
		public virtual string GetKeyAlgorithmParametersString()
		{
			return X509Helper.ToHexString(this.GetKeyAlgorithmParameters());
		}

		/// <summary>Returns the name of the principal to which the certificate was issued.</summary>
		/// <returns>The name of the principal to which the certificate was issued.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060048DE RID: 18654 RVA: 0x00106A01 File Offset: 0x00104C01
		[Obsolete("Use the Subject property.")]
		public virtual string GetName()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
			return this.impl.GetSubjectName(true);
		}

		/// <summary>Returns the public key for the X.509v3 certificate.</summary>
		/// <returns>The public key for the X.509 certificate as an array of bytes.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060048DF RID: 18655 RVA: 0x00106A1A File Offset: 0x00104C1A
		public virtual byte[] GetPublicKey()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
			return this.impl.GetPublicKey();
		}

		/// <summary>Returns the public key for the X.509v3 certificate.</summary>
		/// <returns>The public key for the X.509 certificate as a hexadecimal string.</returns>
		// Token: 0x060048E0 RID: 18656 RVA: 0x00106A32 File Offset: 0x00104C32
		public virtual string GetPublicKeyString()
		{
			return X509Helper.ToHexString(this.GetPublicKey());
		}

		/// <summary>Returns the raw data for the entire X.509v3 certificate.</summary>
		/// <returns>A byte array containing the X.509 certificate data.</returns>
		// Token: 0x060048E1 RID: 18657 RVA: 0x00106A3F File Offset: 0x00104C3F
		public virtual byte[] GetRawCertData()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
			return this.impl.GetRawCertData();
		}

		/// <summary>Returns the raw data for the entire X.509v3 certificate.</summary>
		/// <returns>The X.509 certificate data as a hexadecimal string.</returns>
		// Token: 0x060048E2 RID: 18658 RVA: 0x00106A57 File Offset: 0x00104C57
		public virtual string GetRawCertDataString()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
			return X509Helper.ToHexString(this.impl.GetRawCertData());
		}

		/// <summary>Returns the serial number of the X.509v3 certificate.</summary>
		/// <returns>The serial number of the X.509 certificate as an array of bytes.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060048E3 RID: 18659 RVA: 0x00106A74 File Offset: 0x00104C74
		public virtual byte[] GetSerialNumber()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
			return this.impl.GetSerialNumber();
		}

		/// <summary>Returns the serial number of the X.509v3 certificate.</summary>
		/// <returns>The serial number of the X.509 certificate as a hexadecimal string.</returns>
		// Token: 0x060048E4 RID: 18660 RVA: 0x00106A8C File Offset: 0x00104C8C
		public virtual string GetSerialNumberString()
		{
			byte[] serialNumber = this.GetSerialNumber();
			Array.Reverse<byte>(serialNumber);
			return X509Helper.ToHexString(serialNumber);
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		// Token: 0x060048E5 RID: 18661 RVA: 0x00106A9F File Offset: 0x00104C9F
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object, with extra information, if specified.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="fVerbose">true to produce the verbose form of the string representation; otherwise, false. </param>
		// Token: 0x060048E6 RID: 18662 RVA: 0x00106AA7 File Offset: 0x00104CA7
		public virtual string ToString(bool fVerbose)
		{
			if (!fVerbose || !X509Helper.IsValid(this.impl))
			{
				return base.ToString();
			}
			return this.impl.ToString(true);
		}

		/// <summary>Converts the specified date and time to a string.</summary>
		/// <returns>A string representation of the value of the <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="date">The date and time to convert.</param>
		// Token: 0x060048E7 RID: 18663 RVA: 0x0002126B File Offset: 0x0001F46B
		protected static string FormatDate(DateTime date)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class. </summary>
		// Token: 0x060048E8 RID: 18664 RVA: 0x00002111 File Offset: 0x00000311
		public X509Certificate()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060048E9 RID: 18665 RVA: 0x00106ACC File Offset: 0x00104CCC
		public X509Certificate(byte[] rawData, string password)
		{
			this.Import(rawData, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060048EA RID: 18666 RVA: 0x00106ADD File Offset: 0x00104CDD
		[MonoTODO("SecureString support is incomplete")]
		public X509Certificate(byte[] rawData, SecureString password)
		{
			this.Import(rawData, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060048EB RID: 18667 RVA: 0x00106AEE File Offset: 0x00104CEE
		public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060048EC RID: 18668 RVA: 0x00106AFF File Offset: 0x00104CFF
		[MonoTODO("SecureString support is incomplete")]
		public X509Certificate(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using the name of a PKCS7 signed file. </summary>
		/// <param name="fileName">The name of a PKCS7 signed file.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060048ED RID: 18669 RVA: 0x00106B10 File Offset: 0x00104D10
		public X509Certificate(string fileName)
		{
			this.Import(fileName, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using the name of a PKCS7 signed file and a password to access the certificate.</summary>
		/// <param name="fileName">The name of a PKCS7 signed file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060048EE RID: 18670 RVA: 0x00106B21 File Offset: 0x00104D21
		public X509Certificate(string fileName, string password)
		{
			this.Import(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a certificate file name and a password.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060048EF RID: 18671 RVA: 0x00106B32 File Offset: 0x00104D32
		[MonoTODO("SecureString support is incomplete")]
		public X509Certificate(string fileName, SecureString password)
		{
			this.Import(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using the name of a PKCS7 signed file, a password to access the certificate, and a key storage flag. </summary>
		/// <param name="fileName">The name of a PKCS7 signed file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060048F0 RID: 18672 RVA: 0x00106B43 File Offset: 0x00104D43
		public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(fileName, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a certificate file name, a password, and a key storage flag. </summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060048F1 RID: 18673 RVA: 0x00106B54 File Offset: 0x00104D54
		[MonoTODO("SecureString support is incomplete")]
		public X509Certificate(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(fileName, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object and a <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes serialization information.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that describes how serialization should be performed.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x060048F2 RID: 18674 RVA: 0x00106B68 File Offset: 0x00104D68
		public X509Certificate(SerializationInfo info, StreamingContext context)
		{
			byte[] array = (byte[])info.GetValue("RawData", typeof(byte[]));
			this.Import(array, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Gets the name of the certificate authority that issued the X.509v3 certificate.</summary>
		/// <returns>The name of the certificate authority that issued the X.509v3 certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate handle is invalid.</exception>
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x00106B9F File Offset: 0x00104D9F
		public string Issuer
		{
			get
			{
				X509Helper.ThrowIfContextInvalid(this.impl);
				if (this.issuer_name == null)
				{
					this.issuer_name = this.impl.GetIssuerName(false);
				}
				return this.issuer_name;
			}
		}

		/// <summary>Gets the subject distinguished name from the certificate.</summary>
		/// <returns>The subject distinguished name from the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate handle is invalid.</exception>
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x00106BCC File Offset: 0x00104DCC
		public string Subject
		{
			get
			{
				X509Helper.ThrowIfContextInvalid(this.impl);
				if (this.subject_name == null)
				{
					this.subject_name = this.impl.GetSubjectName(false);
				}
				return this.subject_name;
			}
		}

		/// <summary>Gets a handle to a Microsoft Cryptographic API certificate context described by an unmanaged PCCERT_CONTEXT structure. </summary>
		/// <returns>An <see cref="T:System.IntPtr" /> structure that represents an unmanaged PCCERT_CONTEXT structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060048F5 RID: 18677 RVA: 0x00106BF9 File Offset: 0x00104DF9
		[ComVisible(false)]
		public IntPtr Handle
		{
			get
			{
				if (X509Helper.IsValid(this.impl))
				{
					return this.impl.Handle;
				}
				return IntPtr.Zero;
			}
		}

		/// <summary>Compares two <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects for equality.</summary>
		/// <returns>true if the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object is equal to the object specified by the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to compare to the current object. </param>
		// Token: 0x060048F6 RID: 18678 RVA: 0x00106C1C File Offset: 0x00104E1C
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			X509Certificate x509Certificate = obj as X509Certificate;
			return x509Certificate != null && this.Equals(x509Certificate);
		}

		/// <summary>Exports the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to a byte array in a format described by one of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values. </summary>
		/// <returns>An array of bytes that represents the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="contentType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values that describes how to format the output data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A value other than <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, or <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> was passed to the <paramref name="contentType" /> parameter.-or-The certificate could not be exported.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Open, Export" />
		/// </PermissionSet>
		// Token: 0x060048F7 RID: 18679 RVA: 0x00106C3C File Offset: 0x00104E3C
		[ComVisible(false)]
		[MonoTODO("X509ContentType.Pfx/Pkcs12 and SerializedCert are not supported")]
		public virtual byte[] Export(X509ContentType contentType)
		{
			return this.Export(contentType, null);
		}

		/// <summary>Exports the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to a byte array in a format described by one of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values, and using the specified password.</summary>
		/// <returns>An array of bytes that represents the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="contentType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values that describes how to format the output data.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A value other than <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, or <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> was passed to the <paramref name="contentType" /> parameter.-or-The certificate could not be exported.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Open, Export" />
		/// </PermissionSet>
		// Token: 0x060048F8 RID: 18680 RVA: 0x00106C48 File Offset: 0x00104E48
		[MonoTODO("X509ContentType.Pfx/Pkcs12 and SerializedCert are not supported")]
		[ComVisible(false)]
		public virtual byte[] Export(X509ContentType contentType, string password)
		{
			byte[] array = ((password == null) ? null : Encoding.UTF8.GetBytes(password));
			return this.Export(contentType, array);
		}

		/// <summary>Exports the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to a byte array using the specified format and a password.</summary>
		/// <returns>A byte array that represents the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="contentType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values that describes how to format the output data.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A value other than <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, or <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> was passed to the <paramref name="contentType" /> parameter.-or-The certificate could not be exported.</exception>
		// Token: 0x060048F9 RID: 18681 RVA: 0x00106C70 File Offset: 0x00104E70
		[MonoTODO("X509ContentType.Pfx/Pkcs12 and SerializedCert are not supported. SecureString support is incomplete.")]
		public virtual byte[] Export(X509ContentType contentType, SecureString password)
		{
			byte[] array = ((password == null) ? null : password.GetBuffer());
			return this.Export(contentType, array);
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x00106C94 File Offset: 0x00104E94
		internal byte[] Export(X509ContentType contentType, byte[] password)
		{
			byte[] array;
			try
			{
				X509Helper.ThrowIfContextInvalid(this.impl);
				array = this.impl.Export(contentType, password);
			}
			finally
			{
				if (password != null)
				{
					Array.Clear(password, 0, password.Length);
				}
			}
			return array;
		}

		/// <summary>Populates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object with data from a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x060048FB RID: 18683 RVA: 0x00106CDC File Offset: 0x00104EDC
		[ComVisible(false)]
		public virtual void Import(byte[] rawData)
		{
			this.Import(rawData, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Populates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object using data from a byte array, a password, and flags for determining how the private key is imported.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x060048FC RID: 18684 RVA: 0x00106CE7 File Offset: 0x00104EE7
		[ComVisible(false)]
		[MonoTODO("missing KeyStorageFlags support")]
		public virtual void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			this.impl = X509Helper.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object using data from a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060048FD RID: 18685 RVA: 0x00106CFD File Offset: 0x00104EFD
		[MonoTODO("SecureString support is incomplete")]
		public virtual void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(rawData, null, keyStorageFlags);
		}

		/// <summary>Populates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object with information from a certificate file.</summary>
		/// <param name="fileName">The name of a certificate file represented as a string. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x060048FE RID: 18686 RVA: 0x00106D08 File Offset: 0x00104F08
		[ComVisible(false)]
		public virtual void Import(string fileName)
		{
			byte[] array = File.ReadAllBytes(fileName);
			this.Import(array, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Populates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object with information from a certificate file, a password, and a <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> value.</summary>
		/// <param name="fileName">The name of a certificate file represented as a string. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x060048FF RID: 18687 RVA: 0x00106D28 File Offset: 0x00104F28
		[ComVisible(false)]
		[MonoTODO("missing KeyStorageFlags support")]
		public virtual void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			byte[] array = File.ReadAllBytes(fileName);
			this.Import(array, password, keyStorageFlags);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object with information from a certificate file, a password, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x06004900 RID: 18688 RVA: 0x00106D48 File Offset: 0x00104F48
		[MonoTODO("SecureString support is incomplete, missing KeyStorageFlags support")]
		public virtual void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			byte[] array = File.ReadAllBytes(fileName);
			this.Import(array, null, keyStorageFlags);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.  </summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x06004901 RID: 18689 RVA: 0x00002194 File Offset: 0x00000394
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Gets serialization information with all the data needed to recreate an instance of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06004902 RID: 18690 RVA: 0x00106D65 File Offset: 0x00104F65
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (!X509Helper.IsValid(this.impl))
			{
				throw new NullReferenceException();
			}
			info.AddValue("RawData", this.impl.GetRawCertData());
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x00106D90 File Offset: 0x00104F90
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x00106D99 File Offset: 0x00104F99
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Reset();
			}
		}

		/// <summary>Resets the state of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		// Token: 0x06004905 RID: 18693 RVA: 0x00106DA4 File Offset: 0x00104FA4
		[ComVisible(false)]
		public virtual void Reset()
		{
			if (this.impl != null)
			{
				this.impl.Dispose();
				this.impl = null;
			}
			this.issuer_name = null;
			this.subject_name = null;
			this.hideDates = false;
		}

		// Token: 0x04002662 RID: 9826
		private X509CertificateImpl impl;

		// Token: 0x04002663 RID: 9827
		private bool hideDates;

		// Token: 0x04002664 RID: 9828
		private string issuer_name;

		// Token: 0x04002665 RID: 9829
		private string subject_name;
	}
}
