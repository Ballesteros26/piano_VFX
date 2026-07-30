using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	/// <summary>Verifies a Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) PKCS#1 v1.5 signature.</summary>
	// Token: 0x0200065C RID: 1628
	[ComVisible(true)]
	public class DSASignatureDeformatter : AsymmetricSignatureDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> class.</summary>
		// Token: 0x06004636 RID: 17974 RVA: 0x000F76D4 File Offset: 0x000F58D4
		public DSASignatureDeformatter()
		{
			this._oid = CryptoConfig.MapNameToOID("SHA1", OidGroup.HashAlgorithm);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) that holds the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004637 RID: 17975 RVA: 0x000F76ED File Offset: 0x000F58ED
		public DSASignatureDeformatter(AsymmetricAlgorithm key)
			: this()
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		/// <summary>Specifies the key to be used for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature deformatter.</summary>
		/// <param name="key">The instance of <see cref="T:System.Security.Cryptography.DSA" /> that holds the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004638 RID: 17976 RVA: 0x000F770F File Offset: 0x000F590F
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		/// <summary>Specifies the hash algorithm for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature deformatter.</summary>
		/// <param name="strName">The name of the hash algorithm to use for the signature deformatter. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <paramref name="strName" /> parameter does not map to the <see cref="T:System.Security.Cryptography.SHA1" /> hash algorithm. </exception>
		// Token: 0x06004639 RID: 17977 RVA: 0x000F772B File Offset: 0x000F592B
		public override void SetHashAlgorithm(string strName)
		{
			if (CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm) != this._oid)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("This operation is not supported for this class."));
			}
		}

		/// <summary>Verifies the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature on the data.</summary>
		/// <returns>true if the signature is valid for the data; otherwise, false.</returns>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbHash" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgbHash" /> is null.-or-<paramref name="rgbSignature" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The DSA key is missing.</exception>
		// Token: 0x0600463A RID: 17978 RVA: 0x000F7754 File Offset: 0x000F5954
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			if (this._dsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			return this._dsaKey.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x0400242F RID: 9263
		private DSA _dsaKey;

		// Token: 0x04002430 RID: 9264
		private string _oid;
	}
}
