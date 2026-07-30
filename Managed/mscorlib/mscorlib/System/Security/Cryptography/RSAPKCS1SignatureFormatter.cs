using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Creates an <see cref="T:System.Security.Cryptography.RSA" /> PKCS #1 version 1.5 signature.</summary>
	// Token: 0x020006A3 RID: 1699
	[ComVisible(true)]
	public class RSAPKCS1SignatureFormatter : AsymmetricSignatureFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" /> class.</summary>
		// Token: 0x060048AF RID: 18607 RVA: 0x00105958 File Offset: 0x00103B58
		public RSAPKCS1SignatureFormatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060048B0 RID: 18608 RVA: 0x00105960 File Offset: 0x00103B60
		public RSAPKCS1SignatureFormatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		/// <summary>Creates the <see cref="T:System.Security.Cryptography.RSA" /> PKCS #1 signature for the specified data.</summary>
		/// <returns>The digital signature for <paramref name="rgbHash" />.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060048B1 RID: 18609 RVA: 0x00105970 File Offset: 0x00103B70
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (this.rsa == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("No key pair available."));
			}
			if (this.hash == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("Missing hash algorithm."));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			return PKCS1.Sign_v15(this.rsa, this.hash, rgbHash);
		}

		/// <summary>Sets the hash algorithm to use for creating the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for creating the signature. </param>
		// Token: 0x060048B2 RID: 18610 RVA: 0x001059CD File Offset: 0x00103BCD
		public override void SetHashAlgorithm(string strName)
		{
			if (strName == null)
			{
				throw new ArgumentNullException("strName");
			}
			this.hash = strName;
		}

		/// <summary>Sets the private key to use for creating the signature.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060048B3 RID: 18611 RVA: 0x001059E4 File Offset: 0x00103BE4
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.rsa = (RSA)key;
		}

		// Token: 0x04002606 RID: 9734
		private RSA rsa;

		// Token: 0x04002607 RID: 9735
		private string hash;
	}
}
