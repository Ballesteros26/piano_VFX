using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of the <see cref="T:System.Security.Cryptography.RC2" /> algorithm must derive.</summary>
	// Token: 0x02000672 RID: 1650
	[ComVisible(true)]
	public abstract class RC2 : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.RC2" />.</summary>
		// Token: 0x060046D3 RID: 18131 RVA: 0x000F9039 File Offset: 0x000F7239
		protected RC2()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC2.s_legalBlockSizes;
			this.LegalKeySizesValue = RC2.s_legalKeySizes;
		}

		/// <summary>Gets or sets the effective size of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm in bits.</summary>
		/// <returns>The effective key size used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The effective key size is invalid. </exception>
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060046D4 RID: 18132 RVA: 0x000F9076 File Offset: 0x000F7276
		// (set) Token: 0x060046D5 RID: 18133 RVA: 0x000F9090 File Offset: 0x000F7290
		public virtual int EffectiveKeySize
		{
			get
			{
				if (this.EffectiveKeySizeValue == 0)
				{
					return this.KeySizeValue;
				}
				return this.EffectiveKeySizeValue;
			}
			set
			{
				if (value > this.KeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("EffectiveKeySize value must be at least as large as the KeySize value."));
				}
				if (value == 0)
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				if (value < 40)
				{
					throw new CryptographicException(Environment.GetResourceString("EffectiveKeySize value must be at least 40 bits."));
				}
				if (base.ValidKeySize(value))
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
		}

		/// <summary>Gets or sets the size of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm in bits.</summary>
		/// <returns>The size of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value for the RC2 key size is less than the effective key size value.</exception>
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x000F90F6 File Offset: 0x000F72F6
		// (set) Token: 0x060046D7 RID: 18135 RVA: 0x000F90FE File Offset: 0x000F72FE
		public override int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value < this.EffectiveKeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("EffectiveKeySize value must be at least as large as the KeySize value."));
				}
				base.KeySize = value;
			}
		}

		/// <summary>Creates an instance of a cryptographic object to perform the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</summary>
		/// <returns>An instance of a cryptographic object.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060046D8 RID: 18136 RVA: 0x000F9120 File Offset: 0x000F7320
		public new static RC2 Create()
		{
			return RC2.Create("System.Security.Cryptography.RC2");
		}

		/// <summary>Creates an instance of a cryptographic object to perform the specified implementation of the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</summary>
		/// <returns>An instance of a cryptographic object.</returns>
		/// <param name="AlgName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.RC2" /> to use. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="algName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x060046D9 RID: 18137 RVA: 0x000F912C File Offset: 0x000F732C
		public new static RC2 Create(string AlgName)
		{
			return (RC2)CryptoConfig.CreateFromName(AlgName);
		}

		/// <summary>Represents the effective size of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm in bits.</summary>
		// Token: 0x04002464 RID: 9316
		protected int EffectiveKeySizeValue;

		// Token: 0x04002465 RID: 9317
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04002466 RID: 9318
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 1024, 8)
		};
	}
}
