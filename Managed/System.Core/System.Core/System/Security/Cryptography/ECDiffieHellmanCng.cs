using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm. This class is used to perform cryptographic operations.</summary>
	// Token: 0x0200035C RID: 860
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ECDiffieHellmanCng : ECDiffieHellman
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class with a random key pair.</summary>
		// Token: 0x06001A0B RID: 6667 RVA: 0x0000220F File Offset: 0x0000040F
		public ECDiffieHellmanCng()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class with a random key pair, using the specified key size.</summary>
		/// <param name="keySize">The size of the key. Valid key sizes are 256, 384, and 521 bits.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keySize" /> specifies an invalid length.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) classes are not supported on this system.</exception>
		// Token: 0x06001A0C RID: 6668 RVA: 0x0000220F File Offset: 0x0000040F
		public ECDiffieHellmanCng(int keySize)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class by using the specified <see cref="T:System.Security.Cryptography.CngKey" /> object.</summary>
		/// <param name="key">The key that will be used as input to the cryptographic operations performed by the current object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> does not specify an Elliptic Curve Diffie-Hellman (ECDH) algorithm group.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) classes are not supported on this system.</exception>
		// Token: 0x06001A0D RID: 6669 RVA: 0x0000220F File Offset: 0x0000040F
		[SecuritySafeCritical]
		public ECDiffieHellmanCng(CngKey key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0000220F File Offset: 0x0000040F
		public ECDiffieHellmanCng(ECCurve curve)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the hash algorithm to use when generating key material.</summary>
		/// <returns>An object that specifies the hash algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x0000220F File Offset: 0x0000040F
		public CngAlgorithm HashAlgorithm
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the Hash-based Message Authentication Code (HMAC) key to use when deriving key material.</summary>
		/// <returns>The Hash-based Message Authentication Code (HMAC) key to use when deriving key material.</returns>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x0000220F File Offset: 0x0000040F
		public byte[] HmacKey
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Specifies the <see cref="T:System.Security.Cryptography.CngKey" /> that is used by the current object for cryptographic operations.</summary>
		/// <returns>The key pair used by this object to perform cryptographic operations.</returns>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000560B4 File Offset: 0x000542B4
		public CngKey Key
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the key derivation function for the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction" /> enumeration values: <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Hash" />, <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Hmac" />, or <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Tls" />. The default value is <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Hash" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The enumeration value is out of range.</exception>
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x000560F4 File Offset: 0x000542F4
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x0000220F File Offset: 0x0000040F
		public ECDiffieHellmanKeyDerivationFunction KeyDerivationFunction
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ECDiffieHellmanKeyDerivationFunction.Hash;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the label value that is used for key derivation.</summary>
		/// <returns>The label value.</returns>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x0000220F File Offset: 0x0000040F
		public byte[] Label
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the public key that can be used by another <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> object to generate a shared secret agreement.</summary>
		/// <returns>The public key that is associated with this instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> object.</returns>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x000560B4 File Offset: 0x000542B4
		public override ECDiffieHellmanPublicKey PublicKey
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that will be appended to the secret agreement when generating key material.</summary>
		/// <returns>The value that is appended to the secret agreement.</returns>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x0000220F File Offset: 0x0000040F
		public byte[] SecretAppend
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that will be added to the beginning of the secret agreement when deriving key material.</summary>
		/// <returns>The value that is appended to the beginning of the secret agreement during key derivation.</returns>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x0000220F File Offset: 0x0000040F
		public byte[] SecretPrepend
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the seed value that will be used when deriving key material.</summary>
		/// <returns>The seed value.</returns>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x0000220F File Offset: 0x0000040F
		public byte[] Seed
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the secret agreement is used as a Hash-based Message Authentication Code (HMAC) key to derive key material.</summary>
		/// <returns>true if the secret agreement is used as an HMAC key to derive key material; otherwise, false.</returns>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x00056110 File Offset: 0x00054310
		public bool UseSecretAgreementAsHmacKey
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Derives the key material that is generated from the secret agreement between two parties, given a <see cref="T:System.Security.Cryptography.CngKey" /> object that contains the second party's public key. </summary>
		/// <returns>A byte array that contains the key material. This information is generated from the secret agreement that is calculated from the current object's private key and the specified public key.</returns>
		/// <param name="otherPartyPublicKey">An object that contains the public part of the Elliptic Curve Diffie-Hellman (ECDH) key from the other party in the key exchange.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="otherPartyPublicKey" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="otherPartyPublicKey" /> is invalid. Either its <see cref="P:System.Security.Cryptography.CngKey.AlgorithmGroup" /> property does not specify <see cref="P:System.Security.Cryptography.CngAlgorithmGroup.ECDiffieHellman" /> or its key size does not match the key size of this instance.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object's <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.KeyDerivationFunction" /> property specifies the <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Tls" /> key derivation function, but either <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.Label" /> or <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.Seed" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06001A20 RID: 6688 RVA: 0x000560B4 File Offset: 0x000542B4
		[SecuritySafeCritical]
		public byte[] DeriveKeyMaterial(CngKey otherPartyPublicKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a handle to the secret agreement generated between two parties, given a <see cref="T:System.Security.Cryptography.CngKey" /> object that contains the second party's public key.</summary>
		/// <returns>A handle to the secret agreement. This information is calculated from the current object's private key and the specified public key.</returns>
		/// <param name="otherPartyPublicKey">An object that contains the public part of the Elliptic Curve Diffie-Hellman (ECDH) key from the other party in the key exchange.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="otherPartyPublicKey" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="otherPartyPublicKey" /> is not an ECDH key, or it is not the correct size.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06001A21 RID: 6689 RVA: 0x000560B4 File Offset: 0x000542B4
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public SafeNCryptSecretHandle DeriveSecretAgreementHandle(CngKey otherPartyPublicKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a handle to the secret agreement generated between two parties, given an <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> object that contains the second party's public key.</summary>
		/// <returns>A handle to the secret agreement. This information is calculated from the current object's private key and the specified public key.</returns>
		/// <param name="otherPartyPublicKey">The public key from the other party in the key exchange.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="otherPartyPublicKey" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="otherPartyPublicKey" /> is not an <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> key. </exception>
		// Token: 0x06001A22 RID: 6690 RVA: 0x000560B4 File Offset: 0x000542B4
		public SafeNCryptSecretHandle DeriveSecretAgreementHandle(ECDiffieHellmanPublicKey otherPartyPublicKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Deserializes the key information from an XML string by using the specified format.</summary>
		/// <param name="xml">The XML-based key information to be deserialized.</param>
		/// <param name="format">One of the enumeration values that specifies the format of the XML string. The only currently accepted format is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xml" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="xml" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="format" /> specifies an invalid format. The only accepted value is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06001A23 RID: 6691 RVA: 0x0000220F File Offset: 0x0000040F
		public void FromXmlString(string xml, ECKeyXmlFormat format)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Serializes the key information to an XML string by using the specified format.</summary>
		/// <returns>A string object that contains the key information, serialized to an XML string, according to the requested format.</returns>
		/// <param name="format">One of the enumeration values that specifies the format of the XML string. The only currently accepted format is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="format" /> specifies an invalid format. The only accepted value is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</exception>
		// Token: 0x06001A24 RID: 6692 RVA: 0x000560B4 File Offset: 0x000542B4
		public string ToXmlString(ECKeyXmlFormat format)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
