using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of symmetric algorithms must inherit.</summary>
	// Token: 0x02000691 RID: 1681
	[ComVisible(true)]
	public abstract class SymmetricAlgorithm : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The implementation of the class derived from the symmetric algorithm is not valid.</exception>
		// Token: 0x060047EF RID: 18415 RVA: 0x00100F1E File Offset: 0x000FF11E
		protected SymmetricAlgorithm()
		{
			this.ModeValue = CipherMode.CBC;
			this.PaddingValue = PaddingMode.PKCS7;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		// Token: 0x060047F0 RID: 18416 RVA: 0x00100F34 File Offset: 0x000FF134
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		// Token: 0x060047F1 RID: 18417 RVA: 0x000F7B92 File Offset: 0x000F5D92
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060047F2 RID: 18418 RVA: 0x00100F44 File Offset: 0x000FF144
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
					this.KeyValue = null;
				}
				if (this.IVValue != null)
				{
					Array.Clear(this.IVValue, 0, this.IVValue.Length);
					this.IVValue = null;
				}
			}
		}

		/// <summary>Gets or sets the block size, in bits, of the cryptographic operation.</summary>
		/// <returns>The block size, in bits.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The block size is invalid. </exception>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060047F3 RID: 18419 RVA: 0x00100F9A File Offset: 0x000FF19A
		// (set) Token: 0x060047F4 RID: 18420 RVA: 0x00100FA4 File Offset: 0x000FF1A4
		public virtual int BlockSize
		{
			get
			{
				return this.BlockSizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalBlockSizesValue.Length; i++)
				{
					if (this.LegalBlockSizesValue[i].SkipSize == 0)
					{
						if (this.LegalBlockSizesValue[i].MinSize == value)
						{
							this.BlockSizeValue = value;
							this.IVValue = null;
							return;
						}
					}
					else
					{
						for (int j = this.LegalBlockSizesValue[i].MinSize; j <= this.LegalBlockSizesValue[i].MaxSize; j += this.LegalBlockSizesValue[i].SkipSize)
						{
							if (j == value)
							{
								if (this.BlockSizeValue != value)
								{
									this.BlockSizeValue = value;
									this.IVValue = null;
								}
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Specified block size is not valid for this algorithm."));
			}
		}

		/// <summary>Gets or sets the feedback size, in bits, of the cryptographic operation.</summary>
		/// <returns>The feedback size in bits.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The feedback size is larger than the block size. </exception>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060047F5 RID: 18421 RVA: 0x00101050 File Offset: 0x000FF250
		// (set) Token: 0x060047F6 RID: 18422 RVA: 0x00101058 File Offset: 0x000FF258
		public virtual int FeedbackSize
		{
			get
			{
				return this.FeedbackSizeValue;
			}
			set
			{
				if (value <= 0 || value > this.BlockSizeValue || value % 8 != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified feedback size is invalid."));
				}
				this.FeedbackSizeValue = value;
			}
		}

		/// <summary>Gets or sets the initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) for the symmetric algorithm.</summary>
		/// <returns>The initialization vector.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the initialization vector to null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to set the initialization vector to an invalid size. </exception>
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x00101083 File Offset: 0x000FF283
		// (set) Token: 0x060047F8 RID: 18424 RVA: 0x001010A3 File Offset: 0x000FF2A3
		public virtual byte[] IV
		{
			get
			{
				if (this.IVValue == null)
				{
					this.GenerateIV();
				}
				return (byte[])this.IVValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != this.BlockSizeValue / 8)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified initialization vector (IV) does not match the block size for this algorithm."));
				}
				this.IVValue = (byte[])value.Clone();
			}
		}

		/// <summary>Gets or sets the secret key for the symmetric algorithm.</summary>
		/// <returns>The secret key to use for the symmetric algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the key to null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key size is invalid.</exception>
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x00015E72 File Offset: 0x00014072
		// (set) Token: 0x060047FA RID: 18426 RVA: 0x001010E4 File Offset: 0x000FF2E4
		public virtual byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.ValidKeySize(value.Length * 8))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		/// <summary>Gets the block sizes, in bits, that are supported by the symmetric algorithm.</summary>
		/// <returns>An array that contains the block sizes supported by the algorithm.</returns>
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x00101138 File Offset: 0x000FF338
		public virtual KeySizes[] LegalBlockSizes
		{
			get
			{
				return (KeySizes[])this.LegalBlockSizesValue.Clone();
			}
		}

		/// <summary>Gets the key sizes, in bits, that are supported by the symmetric algorithm.</summary>
		/// <returns>An array that contains the key sizes supported by the algorithm.</returns>
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x0010114A File Offset: 0x000FF34A
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key used by the symmetric algorithm.</summary>
		/// <returns>The size, in bits, of the secret key used by the symmetric algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key size is not valid. </exception>
		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060047FD RID: 18429 RVA: 0x000F90F6 File Offset: 0x000F72F6
		// (set) Token: 0x060047FE RID: 18430 RVA: 0x0010115C File Offset: 0x000FF35C
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (!this.ValidKeySize(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
				}
				this.KeySizeValue = value;
				this.KeyValue = null;
			}
		}

		/// <summary>Gets or sets the mode for operation of the symmetric algorithm.</summary>
		/// <returns>The mode for operation of the symmetric algorithm. The default is <see cref="F:System.Security.Cryptography.CipherMode.CBC" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cipher mode is not one of the <see cref="T:System.Security.Cryptography.CipherMode" /> values. </exception>
		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060047FF RID: 18431 RVA: 0x00101185 File Offset: 0x000FF385
		// (set) Token: 0x06004800 RID: 18432 RVA: 0x0010118D File Offset: 0x000FF38D
		public virtual CipherMode Mode
		{
			get
			{
				return this.ModeValue;
			}
			set
			{
				if (value < CipherMode.CBC || CipherMode.CFB < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified cipher mode is not valid for this algorithm."));
				}
				this.ModeValue = value;
			}
		}

		/// <summary>Gets or sets the padding mode used in the symmetric algorithm.</summary>
		/// <returns>The padding mode used in the symmetric algorithm. The default is <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not one of the <see cref="T:System.Security.Cryptography.PaddingMode" /> values. </exception>
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x001011AE File Offset: 0x000FF3AE
		// (set) Token: 0x06004802 RID: 18434 RVA: 0x001011B6 File Offset: 0x000FF3B6
		public virtual PaddingMode Padding
		{
			get
			{
				return this.PaddingValue;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified padding mode is not valid for this algorithm."));
				}
				this.PaddingValue = value;
			}
		}

		/// <summary>Determines whether the specified key size is valid for the current algorithm.</summary>
		/// <returns>true if the specified key size is valid for the current algorithm; otherwise, false.</returns>
		/// <param name="bitLength">The length, in bits, to check for a valid key size. </param>
		// Token: 0x06004803 RID: 18435 RVA: 0x001011D8 File Offset: 0x000FF3D8
		public bool ValidKeySize(int bitLength)
		{
			KeySizes[] legalKeySizes = this.LegalKeySizes;
			if (legalKeySizes == null)
			{
				return false;
			}
			for (int i = 0; i < legalKeySizes.Length; i++)
			{
				if (legalKeySizes[i].SkipSize == 0)
				{
					if (legalKeySizes[i].MinSize == bitLength)
					{
						return true;
					}
				}
				else
				{
					for (int j = legalKeySizes[i].MinSize; j <= legalKeySizes[i].MaxSize; j += legalKeySizes[i].SkipSize)
					{
						if (j == bitLength)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>Creates a default cryptographic object used to perform the symmetric algorithm.</summary>
		/// <returns>A default cryptographic object used to perform the symmetric algorithm.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06004804 RID: 18436 RVA: 0x0010123E File Offset: 0x000FF43E
		public static SymmetricAlgorithm Create()
		{
			return SymmetricAlgorithm.Create("System.Security.Cryptography.SymmetricAlgorithm");
		}

		/// <summary>Creates the specified cryptographic object used to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object used to perform the symmetric algorithm.</returns>
		/// <param name="algName">The name of the specific implementation of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class to use. </param>
		// Token: 0x06004805 RID: 18437 RVA: 0x0010124A File Offset: 0x000FF44A
		public static SymmetricAlgorithm Create(string algName)
		{
			return (SymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>Creates a symmetric encryptor object with the current <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		// Token: 0x06004806 RID: 18438 RVA: 0x00101257 File Offset: 0x000FF457
		public virtual ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		/// <summary>When overridden in a derived class, creates a symmetric encryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		// Token: 0x06004807 RID: 18439
		public abstract ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV);

		/// <summary>Creates a symmetric decryptor object with the current <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		// Token: 0x06004808 RID: 18440 RVA: 0x0010126B File Offset: 0x000FF46B
		public virtual ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		/// <summary>When overridden in a derived class, creates a symmetric decryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		// Token: 0x06004809 RID: 18441
		public abstract ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV);

		/// <summary>When overridden in a derived class, generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to use for the algorithm. </summary>
		// Token: 0x0600480A RID: 18442
		public abstract void GenerateKey();

		/// <summary>When overridden in a derived class, generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x0600480B RID: 18443
		public abstract void GenerateIV();

		/// <summary>Represents the block size, in bits, of the cryptographic operation.</summary>
		// Token: 0x040024C9 RID: 9417
		protected int BlockSizeValue;

		/// <summary>Represents the feedback size, in bits, of the cryptographic operation.</summary>
		// Token: 0x040024CA RID: 9418
		protected int FeedbackSizeValue;

		/// <summary>Represents the initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) for the symmetric algorithm.</summary>
		// Token: 0x040024CB RID: 9419
		protected byte[] IVValue;

		/// <summary>Represents the secret key for the symmetric algorithm.</summary>
		// Token: 0x040024CC RID: 9420
		protected byte[] KeyValue;

		/// <summary>Specifies the block sizes, in bits, that are supported by the symmetric algorithm.</summary>
		// Token: 0x040024CD RID: 9421
		protected KeySizes[] LegalBlockSizesValue;

		/// <summary>Specifies the key sizes, in bits, that are supported by the symmetric algorithm.</summary>
		// Token: 0x040024CE RID: 9422
		protected KeySizes[] LegalKeySizesValue;

		/// <summary>Represents the size, in bits, of the secret key used by the symmetric algorithm.</summary>
		// Token: 0x040024CF RID: 9423
		protected int KeySizeValue;

		/// <summary>Represents the cipher mode used in the symmetric algorithm.</summary>
		// Token: 0x040024D0 RID: 9424
		protected CipherMode ModeValue;

		/// <summary>Represents the padding mode used in the symmetric algorithm.</summary>
		// Token: 0x040024D1 RID: 9425
		protected PaddingMode PaddingValue;
	}
}
