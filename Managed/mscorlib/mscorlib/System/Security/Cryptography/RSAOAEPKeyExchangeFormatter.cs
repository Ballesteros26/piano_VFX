using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Creates Optimal Asymmetric Encryption Padding (OAEP) key exchange data using <see cref="T:System.Security.Cryptography.RSA" />.</summary>
	// Token: 0x0200067F RID: 1663
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" /> class.</summary>
		// Token: 0x06004775 RID: 18293 RVA: 0x000FEABC File Offset: 0x000FCCBC
		public RSAOAEPKeyExchangeFormatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x06004776 RID: 18294 RVA: 0x000FEAC4 File Offset: 0x000FCCC4
		public RSAOAEPKeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		/// <summary>Gets or sets the parameter used to create padding in the key exchange creation process.</summary>
		/// <returns>The parameter value.</returns>
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004777 RID: 18295 RVA: 0x000FEAE6 File Offset: 0x000FCCE6
		// (set) Token: 0x06004778 RID: 18296 RVA: 0x000FEB02 File Offset: 0x000FCD02
		public byte[] Parameter
		{
			get
			{
				if (this.ParameterValue != null)
				{
					return (byte[])this.ParameterValue.Clone();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.ParameterValue = (byte[])value.Clone();
					return;
				}
				this.ParameterValue = null;
			}
		}

		/// <summary>Gets the parameters for the Optimal Asymmetric Encryption Padding (OAEP) key exchange.</summary>
		/// <returns>An XML string containing the parameters of the OAEP key exchange operation.</returns>
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06004779 RID: 18297 RVA: 0x0000A42E File Offset: 0x0000862E
		public override string Parameters
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the random number generator algorithm to use in the creation of the key exchange.</summary>
		/// <returns>The instance of a random number generator algorithm to use.</returns>
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x000FEB20 File Offset: 0x000FCD20
		// (set) Token: 0x0600477B RID: 18299 RVA: 0x000FEB28 File Offset: 0x000FCD28
		public RandomNumberGenerator Rng
		{
			get
			{
				return this.RngValue;
			}
			set
			{
				this.RngValue = value;
			}
		}

		/// <summary>Sets the public key to use for encrypting the key exchange data.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x0600477C RID: 18300 RVA: 0x000FEB31 File Offset: 0x000FCD31
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesEncrypt = null;
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <param name="rgbData">The secret information to be passed in the key exchange. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is missing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600477D RID: 18301 RVA: 0x000FEB5C File Offset: 0x000FCD5C
		[SecuritySafeCritical]
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			if (this.OverridesEncrypt)
			{
				return this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
			}
			return Utils.RsaOaepEncrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), RandomNumberGenerator.Create(), rgbData);
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <param name="rgbData">The secret information to be passed in the key exchange. </param>
		/// <param name="symAlgType">This parameter is not used in the current version. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600477E RID: 18302 RVA: 0x000FEBB6 File Offset: 0x000FCDB6
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x0600477F RID: 18303 RVA: 0x000FEBC0 File Offset: 0x000FCDC0
		private bool OverridesEncrypt
		{
			get
			{
				if (this._rsaOverridesEncrypt == null)
				{
					this._rsaOverridesEncrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Encrypt", new Type[]
					{
						typeof(byte[]),
						typeof(RSAEncryptionPadding)
					}));
				}
				return this._rsaOverridesEncrypt.Value;
			}
		}

		// Token: 0x040024A7 RID: 9383
		private byte[] ParameterValue;

		// Token: 0x040024A8 RID: 9384
		private RSA _rsaKey;

		// Token: 0x040024A9 RID: 9385
		private bool? _rsaOverridesEncrypt;

		// Token: 0x040024AA RID: 9386
		private RandomNumberGenerator RngValue;
	}
}
