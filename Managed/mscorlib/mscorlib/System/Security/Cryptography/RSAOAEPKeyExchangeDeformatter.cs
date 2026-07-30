using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Decrypts Optimal Asymmetric Encryption Padding (OAEP) key exchange data.</summary>
	// Token: 0x0200067E RID: 1662
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter" /> class.</summary>
		// Token: 0x0600476E RID: 18286 RVA: 0x000FE9B0 File Offset: 0x000FCBB0
		public RSAOAEPKeyExchangeDeformatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x0600476F RID: 18287 RVA: 0x000FE9B8 File Offset: 0x000FCBB8
		public RSAOAEPKeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		/// <summary>Gets the parameters for the Optimal Asymmetric Encryption Padding (OAEP) key exchange.</summary>
		/// <returns>An XML string containing the parameters of the OAEP key exchange operation.</returns>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06004771 RID: 18289 RVA: 0x00002194 File Offset: 0x00000394
		public override string Parameters
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>Extracts secret information from the encrypted key exchange data.</summary>
		/// <returns>The secret information derived from the key exchange data.</returns>
		/// <param name="rgbData">The key exchange data within which the secret information is hidden. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key exchange data verification has failed. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is missing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004772 RID: 18290 RVA: 0x000FE9DC File Offset: 0x000FCBDC
		[SecuritySafeCritical]
		public override byte[] DecryptKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			if (this.OverridesDecrypt)
			{
				return this._rsaKey.Decrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
			}
			return Utils.RsaOaepDecrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), rgbData);
		}

		/// <summary>Sets the private key to use for decrypting the secret information.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004773 RID: 18291 RVA: 0x000FEA31 File Offset: 0x000FCC31
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesDecrypt = null;
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004774 RID: 18292 RVA: 0x000FEA5C File Offset: 0x000FCC5C
		private bool OverridesDecrypt
		{
			get
			{
				if (this._rsaOverridesDecrypt == null)
				{
					this._rsaOverridesDecrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Decrypt", new Type[]
					{
						typeof(byte[]),
						typeof(RSAEncryptionPadding)
					}));
				}
				return this._rsaOverridesDecrypt.Value;
			}
		}

		// Token: 0x040024A5 RID: 9381
		private RSA _rsaKey;

		// Token: 0x040024A6 RID: 9382
		private bool? _rsaOverridesDecrypt;
	}
}
