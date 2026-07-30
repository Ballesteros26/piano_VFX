using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	/// <summary>Creates a Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature.</summary>
	// Token: 0x0200065D RID: 1629
	[ComVisible(true)]
	public class DSASignatureFormatter : AsymmetricSignatureFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureFormatter" /> class.</summary>
		// Token: 0x0600463B RID: 17979 RVA: 0x000F77A2 File Offset: 0x000F59A2
		public DSASignatureFormatter()
		{
			this._oid = CryptoConfig.MapNameToOID("SHA1", OidGroup.HashAlgorithm);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureFormatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) that holds the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600463C RID: 17980 RVA: 0x000F77BB File Offset: 0x000F59BB
		public DSASignatureFormatter(AsymmetricAlgorithm key)
			: this()
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		/// <summary>Specifies the key to be used for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature formatter.</summary>
		/// <param name="key">The instance of <see cref="T:System.Security.Cryptography.DSA" /> that holds the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600463D RID: 17981 RVA: 0x000F77DD File Offset: 0x000F59DD
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		/// <summary>Specifies the hash algorithm for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature formatter.</summary>
		/// <param name="strName">The name of the hash algorithm to use for the signature formatter. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <paramref name="strName" /> parameter does not map to the <see cref="T:System.Security.Cryptography.SHA1" /> hash algorithm. </exception>
		// Token: 0x0600463E RID: 17982 RVA: 0x000F77F9 File Offset: 0x000F59F9
		public override void SetHashAlgorithm(string strName)
		{
			if (CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm) != this._oid)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("This operation is not supported for this class."));
			}
		}

		/// <summary>Creates the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) PKCS #1 signature for the specified data.</summary>
		/// <returns>The digital signature for the specified data.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgbHash" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The OID is null.-or-The DSA key is null.</exception>
		// Token: 0x0600463F RID: 17983 RVA: 0x000F7820 File Offset: 0x000F5A20
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (this._oid == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Required object identifier (OID) cannot be found."));
			}
			if (this._dsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			return this._dsaKey.CreateSignature(rgbHash);
		}

		// Token: 0x04002431 RID: 9265
		private DSA _dsaKey;

		// Token: 0x04002432 RID: 9266
		private string _oid;
	}
}
