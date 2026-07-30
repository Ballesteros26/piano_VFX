using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Decrypts the PKCS #1 key exchange data.</summary>
	// Token: 0x02000680 RID: 1664
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" /> class.</summary>
		// Token: 0x06004780 RID: 18304 RVA: 0x000FE9B0 File Offset: 0x000FCBB0
		public RSAPKCS1KeyExchangeDeformatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1KeyExchangeDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004781 RID: 18305 RVA: 0x000FEC20 File Offset: 0x000FCE20
		public RSAPKCS1KeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		/// <summary>Gets or sets the random number generator algorithm to use in the creation of the key exchange.</summary>
		/// <returns>The instance of a random number generator algorithm to use.</returns>
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06004782 RID: 18306 RVA: 0x000FEC42 File Offset: 0x000FCE42
		// (set) Token: 0x06004783 RID: 18307 RVA: 0x000FEC4A File Offset: 0x000FCE4A
		public RandomNumberGenerator RNG
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

		/// <summary>Gets the parameters for the PKCS #1 key exchange.</summary>
		/// <returns>An XML string containing the parameters of the PKCS #1 key exchange operation.</returns>
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06004785 RID: 18309 RVA: 0x00002194 File Offset: 0x00000394
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
		/// <param name="rgbIn">The key exchange data within which the secret information is hidden. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is missing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004786 RID: 18310 RVA: 0x000FEC54 File Offset: 0x000FCE54
		public override byte[] DecryptKeyExchange(byte[] rgbIn)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			byte[] array;
			if (this.OverridesDecrypt)
			{
				array = this._rsaKey.Decrypt(rgbIn, RSAEncryptionPadding.Pkcs1);
			}
			else
			{
				byte[] array2 = this._rsaKey.DecryptValue(rgbIn);
				int num = 2;
				while (num < array2.Length && array2[num] != 0)
				{
					num++;
				}
				if (num >= array2.Length)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Error occurred while decoding PKCS1 padding."));
				}
				num++;
				array = new byte[array2.Length - num];
				Buffer.InternalBlockCopy(array2, num, array, 0, array.Length);
			}
			return array;
		}

		/// <summary>Sets the private key to use for decrypting the secret information.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004787 RID: 18311 RVA: 0x000FECE8 File Offset: 0x000FCEE8
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesDecrypt = null;
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x000FED10 File Offset: 0x000FCF10
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

		// Token: 0x040024AB RID: 9387
		private RSA _rsaKey;

		// Token: 0x040024AC RID: 9388
		private bool? _rsaOverridesDecrypt;

		// Token: 0x040024AD RID: 9389
		private RandomNumberGenerator RngValue;
	}
}
