using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Verifies an <see cref="T:System.Security.Cryptography.RSA" /> PKCS #1 version 1.5 signature.</summary>
	// Token: 0x020006A2 RID: 1698
	[ComVisible(true)]
	public class RSAPKCS1SignatureDeformatter : AsymmetricSignatureDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureDeformatter" /> class.</summary>
		// Token: 0x060048AA RID: 18602 RVA: 0x001058A1 File Offset: 0x00103AA1
		public RSAPKCS1SignatureDeformatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x060048AB RID: 18603 RVA: 0x001058A9 File Offset: 0x00103AA9
		public RSAPKCS1SignatureDeformatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		/// <summary>Sets the hash algorithm to use for verifying the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for verifying the signature. </param>
		// Token: 0x060048AC RID: 18604 RVA: 0x001058B8 File Offset: 0x00103AB8
		public override void SetHashAlgorithm(string strName)
		{
			if (strName == null)
			{
				throw new ArgumentNullException("strName");
			}
			this.hashName = strName;
		}

		/// <summary>Sets the public key to use for verifying the signature.</summary>
		/// <param name="key">The instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x060048AD RID: 18605 RVA: 0x001058CF File Offset: 0x00103ACF
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.rsa = (RSA)key;
		}

		/// <summary>Verifies the <see cref="T:System.Security.Cryptography.RSA" /> PKCS#1 signature for the specified data.</summary>
		/// <returns>true if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, false.</returns>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbHash" />. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null.-or- The <paramref name="rgbSignature" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060048AE RID: 18606 RVA: 0x001058EC File Offset: 0x00103AEC
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (this.rsa == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("No public key available."));
			}
			if (this.hashName == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("Missing hash algorithm."));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			return PKCS1.Verify_v15(this.rsa, this.hashName, rgbHash, rgbSignature);
		}

		// Token: 0x04002604 RID: 9732
		private RSA rsa;

		// Token: 0x04002605 RID: 9733
		private string hashName;
	}
}
