using System;
using System.Security.Cryptography.X509Certificates;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> class represents a signer associated with a <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object that represents a CMS/PKCS #7 message.</summary>
	// Token: 0x02000032 RID: 50
	public sealed class SignerInfo
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000493C File Offset: 0x00002B3C
		internal SignerInfo(string hashName, X509Certificate2 certificate, SubjectIdentifierType type, object o, int version)
		{
			this._digest = new Oid(CryptoConfig.MapNameToOID(hashName));
			this._certificate = certificate;
			this._counter = new SignerInfoCollection();
			this._signed = new CryptographicAttributeObjectCollection();
			this._unsigned = new CryptographicAttributeObjectCollection();
			this._signer = new SubjectIdentifier(type, o);
			this._version = version;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of signed attributes that is associated with the signer information. Signed attributes are signed along with the rest of the message content.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000499E File Offset: 0x00002B9E
		public CryptographicAttributeObjectCollection SignedAttributes
		{
			get
			{
				return this._signed;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Certificate" /> property retrieves the signing certificate associated with the signer information.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000049A6 File Offset: 0x00002BA6
		public X509Certificate2 Certificate
		{
			get
			{
				return this._certificate;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos" /> property retrieves the set of counter signers associated with the signer information. </summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection that represents the counter signers for the signer information. If there are no counter signers, the property is an empty collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000049AE File Offset: 0x00002BAE
		public SignerInfoCollection CounterSignerInfos
		{
			get
			{
				return this._counter;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.DigestAlgorithm" /> property retrieves the <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used in the computation of the signatures.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used with the signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000049B6 File Offset: 0x00002BB6
		public Oid DigestAlgorithm
		{
			get
			{
				return this._digest;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignerIdentifier" /> property retrieves the certificate identifier of the signer associated with the signer information.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that uniquely identifies the certificate associated with the signer information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000049BE File Offset: 0x00002BBE
		public SubjectIdentifier SignerIdentifier
		{
			get
			{
				return this._signer;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.UnsignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of unsigned attributes that is associated with the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Unsigned attributes can be modified without invalidating the signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000049C6 File Offset: 0x00002BC6
		public CryptographicAttributeObjectCollection UnsignedAttributes
		{
			get
			{
				return this._unsigned;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Version" /> property retrieves the signer information version.</summary>
		/// <returns>An int value that specifies the signer information version.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000049CE File Offset: 0x00002BCE
		public int Version
		{
			get
			{
				return this._version;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> method verifies the data integrity of the CMS/PKCS #7 message signer information. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> is a specialized method used in specific security infrastructure applications in which the subject uses the HashOnly member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration when setting up a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> does not authenticate the signer information because this method does not involve verifying a digital signature. For general-purpose checking of the integrity and authenticity of CMS/PKCS #7 message signer information and countersignatures, use the <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> or <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> methods.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600011B RID: 283 RVA: 0x00004938 File Offset: 0x00002B38
		[MonoTODO]
		public void CheckHash()
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> method verifies the digital signature of the message and, optionally, validates the certificate.</summary>
		/// <param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly" /> is true, only the signature is verified. If <paramref name="verifySignatureOnly" /> is false, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x0600011C RID: 284 RVA: 0x00004938 File Offset: 0x00002B38
		[MonoTODO]
		public void CheckSignature(bool verifySignatureOnly)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> method verifies the digital signature of the message by using the specified collection of certificates and, optionally, validates the certificate.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that can be used to validate the chain. If no additional certificates are to be used to validate the chain, use <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> instead of <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" />.</param>
		/// <param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly" /> is true, only the signature is verified. If <paramref name="verifySignatureOnly" /> is false, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x0600011D RID: 285 RVA: 0x00004938 File Offset: 0x00002B38
		[MonoTODO]
		public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature" /> method prompts the user to select a signing certificate, creates a countersignature, and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.StorePermission, System.Security, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Flags="CreateStore, DeleteStore, OpenStore, EnumerateCertificates" />
		/// </PermissionSet>
		// Token: 0x0600011E RID: 286 RVA: 0x00004938 File Offset: 0x00002B38
		[MonoTODO]
		public void ComputeCounterSignature()
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature(System.Security.Cryptography.Pkcs.CmsSigner)" /> method creates a countersignature by using the specified signer and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.</summary>
		/// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object that represents the counter signer.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600011F RID: 287 RVA: 0x00004938 File Offset: 0x00002B38
		[MonoTODO]
		public void ComputeCounterSignature(CmsSigner signer)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Security.Cryptography.Pkcs.SignerInfo)" /> method removes the countersignature for the specified <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <param name="counterSignerInfo">A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the countersignature being removed.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000120 RID: 288 RVA: 0x00004938 File Offset: 0x00002B38
		[MonoTODO]
		public void RemoveCounterSignature(SignerInfo counterSignerInfo)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Int32)" /> method removes the countersignature at the specified index of the <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos" /> collection.</summary>
		/// <param name="index">The zero-based index of the countersignature to remove.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000121 RID: 289 RVA: 0x00004938 File Offset: 0x00002B38
		[MonoTODO]
		public void RemoveCounterSignature(int index)
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal SignerInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000EF RID: 239
		private SubjectIdentifier _signer;

		// Token: 0x040000F0 RID: 240
		private X509Certificate2 _certificate;

		// Token: 0x040000F1 RID: 241
		private Oid _digest;

		// Token: 0x040000F2 RID: 242
		private SignerInfoCollection _counter;

		// Token: 0x040000F3 RID: 243
		private CryptographicAttributeObjectCollection _signed;

		// Token: 0x040000F4 RID: 244
		private CryptographicAttributeObjectCollection _unsigned;

		// Token: 0x040000F5 RID: 245
		private int _version;
	}
}
