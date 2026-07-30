using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.RC2" /> algorithm. This class cannot be inherited.</summary>
	// Token: 0x02000673 RID: 1651
	[ComVisible(true)]
	public sealed class RC2CryptoServiceProvider : RC2
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RC2CryptoServiceProvider" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		/// <exception cref="T:System.InvalidOperationException">A non-compliant FIPS algorithm was found.</exception>
		// Token: 0x060046DB RID: 18139 RVA: 0x000F9170 File Offset: 0x000F7370
		[SecuritySafeCritical]
		public RC2CryptoServiceProvider()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms."));
			}
			if (!Utils.HasAlgorithm(26114, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptographic service provider (CSP) could not be found for this algorithm."));
			}
			this.LegalKeySizesValue = RC2CryptoServiceProvider.s_legalKeySizes;
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Gets or sets the effective size, in bits, of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</summary>
		/// <returns>The effective key size, in bits, used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <see cref="P:System.Security.Cryptography.RC2CryptoServiceProvider.EffectiveKeySize" /> property was set to a value other than the <see cref="F:System.Security.Cryptography.SymmetricAlgorithm.KeySizeValue" /> property. </exception>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060046DC RID: 18140 RVA: 0x000F90F6 File Offset: 0x000F72F6
		// (set) Token: 0x060046DD RID: 18141 RVA: 0x000F91C9 File Offset: 0x000F73C9
		public override int EffectiveKeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value != this.KeySizeValue)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("EffectiveKeySize must be the same as KeySize in this implementation."));
				}
			}
		}

		/// <summary>Gets or sets a value that determines whether to create a key with an 11-byte-long, zero-value salt. </summary>
		/// <returns>true if the key should be created with an 11-byte-long, zero-value salt; otherwise, false. The default is false.</returns>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060046DE RID: 18142 RVA: 0x000F91E4 File Offset: 0x000F73E4
		// (set) Token: 0x060046DF RID: 18143 RVA: 0x000F91EC File Offset: 0x000F73EC
		[ComVisible(false)]
		public bool UseSalt
		{
			get
			{
				return this.m_use40bitSalt;
			}
			set
			{
				this.m_use40bitSalt = value;
			}
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.RC2" /> encryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.RC2" /> encryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> cipher mode was used.-or-A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> cipher mode with a feedback size other than 8 bits was used.-or-An invalid key size was used.-or-The algorithm key size was not available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060046E0 RID: 18144 RVA: 0x000F91F5 File Offset: 0x000F73F5
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (this.m_use40bitSalt)
			{
				throw new NotImplementedException("UseSalt=true is not implemented on Mono yet");
			}
			return new RC2Transform(this, true, rgbKey, rgbIV);
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.RC2" /> decryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />)and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.RC2" /> decryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> cipher mode was used.-or-A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> cipher mode with a feedback size other than 8 bits was used.-or-An invalid key size was used.-or-The algorithm key size was not available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060046E1 RID: 18145 RVA: 0x000F9213 File Offset: 0x000F7413
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (this.m_use40bitSalt)
			{
				throw new NotImplementedException("UseSalt=true is not implemented on Mono yet");
			}
			return new RC2Transform(this, false, rgbKey, rgbIV);
		}

		/// <summary>Generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to be used for the algorithm.</summary>
		// Token: 0x060046E2 RID: 18146 RVA: 0x000F9231 File Offset: 0x000F7431
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
		}

		/// <summary>Generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x060046E3 RID: 18147 RVA: 0x000F710D File Offset: 0x000F530D
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}

		// Token: 0x04002467 RID: 9319
		private bool m_use40bitSalt;

		// Token: 0x04002468 RID: 9320
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 128, 8)
		};
	}
}
