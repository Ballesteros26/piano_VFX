using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Creates the PKCS#1 key exchange data using <see cref="T:System.Security.Cryptography.RSA" />.</summary>
	// Token: 0x02000681 RID: 1665
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" /> class.</summary>
		// Token: 0x06004789 RID: 18313 RVA: 0x000FEABC File Offset: 0x000FCCBC
		public RSAPKCS1KeyExchangeFormatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeFormatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x0600478A RID: 18314 RVA: 0x000FED70 File Offset: 0x000FCF70
		public RSAPKCS1KeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		/// <summary>Gets the parameters for the PKCS #1 key exchange.</summary>
		/// <returns>An XML string containing the parameters of the PKCS #1 key exchange operation.</returns>
		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600478B RID: 18315 RVA: 0x000FED92 File Offset: 0x000FCF92
		public override string Parameters
		{
			get
			{
				return "<enc:KeyEncryptionMethod enc:Algorithm=\"http://www.microsoft.com/xml/security/algorithm/PKCS1-v1.5-KeyEx\" xmlns:enc=\"http://www.microsoft.com/xml/security/encryption/v1.0\" />";
			}
		}

		/// <summary>Gets or sets the random number generator algorithm to use in the creation of the key exchange.</summary>
		/// <returns>The instance of a random number generator algorithm to use.</returns>
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x000FED99 File Offset: 0x000FCF99
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x000FEDA1 File Offset: 0x000FCFA1
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
		// Token: 0x0600478E RID: 18318 RVA: 0x000FEDAA File Offset: 0x000FCFAA
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
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="rgbData " />is too big.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600478F RID: 18319 RVA: 0x000FEDD4 File Offset: 0x000FCFD4
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (rgbData == null)
			{
				throw new ArgumentNullException("rgbData");
			}
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			byte[] array;
			if (this.OverridesEncrypt)
			{
				array = this._rsaKey.Encrypt(rgbData, RSAEncryptionPadding.Pkcs1);
			}
			else
			{
				int num = this._rsaKey.KeySize / 8;
				if (rgbData.Length + 11 > num)
				{
					throw new CryptographicException(Environment.GetResourceString("The data to be encrypted exceeds the maximum for this modulus of {0} bytes.", new object[] { num - 11 }));
				}
				byte[] array2 = new byte[num];
				if (this.RngValue == null)
				{
					this.RngValue = RandomNumberGenerator.Create();
				}
				this.Rng.GetNonZeroBytes(array2);
				array2[0] = 0;
				array2[1] = 2;
				array2[num - rgbData.Length - 1] = 0;
				Buffer.InternalBlockCopy(rgbData, 0, array2, num - rgbData.Length, rgbData.Length);
				array = this._rsaKey.EncryptValue(array2);
			}
			return array;
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <param name="rgbData">The secret information to be passed in the key exchange. </param>
		/// <param name="symAlgType">This parameter is not used in the current version. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004790 RID: 18320 RVA: 0x000FEBB6 File Offset: 0x000FCDB6
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06004791 RID: 18321 RVA: 0x000FEEB8 File Offset: 0x000FD0B8
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

		// Token: 0x040024AE RID: 9390
		private RandomNumberGenerator RngValue;

		// Token: 0x040024AF RID: 9391
		private RSA _rsaKey;

		// Token: 0x040024B0 RID: 9392
		private bool? _rsaOverridesEncrypt;
	}
}
