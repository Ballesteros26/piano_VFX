using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Specifies an Elliptic Curve Diffie-Hellman (ECDH) public key for use with the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class.</summary>
	// Token: 0x0200035E RID: 862
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class ECDiffieHellmanCngPublicKey : ECDiffieHellmanPublicKey
	{
		// Token: 0x06001A25 RID: 6693 RVA: 0x0000220F File Offset: 0x0000040F
		internal ECDiffieHellmanCngPublicKey()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the key BLOB format for a <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object.</summary>
		/// <returns>The format that the key BLOB is expressed in.</returns>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x000560B4 File Offset: 0x000542B4
		public CngKeyBlobFormat BlobFormat
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Converts a byte array that contains a public key to a <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object according to the specified format.</summary>
		/// <returns>An object that contains the ECDH public key that is serialized in the byte array.</returns>
		/// <param name="publicKeyBlob">A byte array that contains an Elliptic Curve Diffie-Hellman (ECDH) public key.</param>
		/// <param name="format">An object that specifies the format of the key BLOB.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="publicKeyBlob" /> or <paramref name="format" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="publicKeyBlob" /> parameter does not contain an <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> key. </exception>
		// Token: 0x06001A27 RID: 6695 RVA: 0x000560B4 File Offset: 0x000542B4
		[SecuritySafeCritical]
		public static ECDiffieHellmanPublicKey FromByteArray(byte[] publicKeyBlob, CngKeyBlobFormat format)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Converts an XML string to an <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object.</summary>
		/// <returns>An object that contains the ECDH public key that is specified by the given XML.</returns>
		/// <param name="xml">An XML string that contains an Elliptic Curve Diffie-Hellman (ECDH) key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xml" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="xml" /> parameter does not specify an <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> key.</exception>
		// Token: 0x06001A28 RID: 6696 RVA: 0x000560B4 File Offset: 0x000542B4
		[SecuritySafeCritical]
		public static ECDiffieHellmanCngPublicKey FromXmlString(string xml)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Converts the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object to a <see cref="T:System.Security.Cryptography.CngKey" /> object.</summary>
		/// <returns>An object that contains the key represented by the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCngPublicKey" /> object.</returns>
		// Token: 0x06001A29 RID: 6697 RVA: 0x000560B4 File Offset: 0x000542B4
		public CngKey Import()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
