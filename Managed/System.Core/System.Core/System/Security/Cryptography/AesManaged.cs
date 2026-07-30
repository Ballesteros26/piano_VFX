using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a managed implementation of the Advanced Encryption Standard (AES) symmetric algorithm. </summary>
	// Token: 0x0200005A RID: 90
	public sealed class AesManaged : Aes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesManaged" /> class. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The Windows security policy setting for FIPS is enabled.</exception>
		/// <exception cref="T:System.InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
		// Token: 0x060001D5 RID: 469 RVA: 0x00005170 File Offset: 0x00003370
		public AesManaged()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				throw new InvalidOperationException(global::SR.GetString("This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms."));
			}
			this.m_rijndael = new RijndaelManaged();
			this.m_rijndael.BlockSize = this.BlockSize;
			this.m_rijndael.KeySize = this.KeySize;
		}

		/// <summary>Gets or sets the number of bits to use as feedback. </summary>
		/// <returns>The feedback size, in bits.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000051C7 File Offset: 0x000033C7
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x000051D4 File Offset: 0x000033D4
		public override int FeedbackSize
		{
			get
			{
				return this.m_rijndael.FeedbackSize;
			}
			set
			{
				this.m_rijndael.FeedbackSize = value;
			}
		}

		/// <summary>Gets or sets the initialization vector (IV) to use for the symmetric algorithm. </summary>
		/// <returns>The initialization vector to use for the symmetric algorithm</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000051E2 File Offset: 0x000033E2
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x000051EF File Offset: 0x000033EF
		public override byte[] IV
		{
			get
			{
				return this.m_rijndael.IV;
			}
			set
			{
				this.m_rijndael.IV = value;
			}
		}

		/// <summary>Gets or sets the secret key used for the symmetric algorithm.</summary>
		/// <returns>The key for the symmetric algorithm.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000051FD File Offset: 0x000033FD
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000520A File Offset: 0x0000340A
		public override byte[] Key
		{
			get
			{
				return this.m_rijndael.Key;
			}
			set
			{
				this.m_rijndael.Key = value;
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key used for the symmetric algorithm. </summary>
		/// <returns>The size, in bits, of the key used by the symmetric algorithm.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005218 File Offset: 0x00003418
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00005225 File Offset: 0x00003425
		public override int KeySize
		{
			get
			{
				return this.m_rijndael.KeySize;
			}
			set
			{
				this.m_rijndael.KeySize = value;
			}
		}

		/// <summary>Gets or sets the mode for operation of the symmetric algorithm.</summary>
		/// <returns>One of the enumeration values that specifies the block cipher mode to use for encryption. The default is <see cref="F:System.Security.Cryptography.CipherMode.CBC" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <see cref="P:System.Security.Cryptography.AesManaged.Mode" /> is set to <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> or <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.</exception>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005233 File Offset: 0x00003433
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00005240 File Offset: 0x00003440
		public override CipherMode Mode
		{
			get
			{
				return this.m_rijndael.Mode;
			}
			set
			{
				if (value == CipherMode.CFB || value == CipherMode.OFB)
				{
					throw new CryptographicException(global::SR.GetString("The specified cipher mode is not valid for this algorithm."));
				}
				this.m_rijndael.Mode = value;
			}
		}

		/// <summary>Gets or sets the padding mode used in the symmetric algorithm. </summary>
		/// <returns>One of the enumeration values that specifies the type of padding to apply. The default is <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" />.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00005266 File Offset: 0x00003466
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00005273 File Offset: 0x00003473
		public override PaddingMode Padding
		{
			get
			{
				return this.m_rijndael.Padding;
			}
			set
			{
				this.m_rijndael.Padding = value;
			}
		}

		/// <summary>Creates a symmetric decryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		// Token: 0x060001E2 RID: 482 RVA: 0x00005281 File Offset: 0x00003481
		public override ICryptoTransform CreateDecryptor()
		{
			return this.m_rijndael.CreateDecryptor();
		}

		/// <summary>Creates a symmetric decryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="iv" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is invalid.</exception>
		// Token: 0x060001E3 RID: 483 RVA: 0x00005290 File Offset: 0x00003490
		public override ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(global::SR.GetString("The specified key is not a valid size for this algorithm."), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(global::SR.GetString("The specified initialization vector (IV) does not match the block size for this algorithm."), "iv");
			}
			return this.m_rijndael.CreateDecryptor(key, iv);
		}

		/// <summary>Creates a symmetric encryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		// Token: 0x060001E4 RID: 484 RVA: 0x000052FF File Offset: 0x000034FF
		public override ICryptoTransform CreateEncryptor()
		{
			return this.m_rijndael.CreateEncryptor();
		}

		/// <summary>Creates a symmetric encryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="iv" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is invalid.</exception>
		// Token: 0x060001E5 RID: 485 RVA: 0x0000530C File Offset: 0x0000350C
		public override ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(global::SR.GetString("The specified key is not a valid size for this algorithm."), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(global::SR.GetString("The specified initialization vector (IV) does not match the block size for this algorithm."), "iv");
			}
			return this.m_rijndael.CreateEncryptor(key, iv);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000537C File Offset: 0x0000357C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((IDisposable)this.m_rijndael).Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Generates a random initialization vector (IV) to use for the symmetric algorithm.</summary>
		// Token: 0x060001E7 RID: 487 RVA: 0x000053B4 File Offset: 0x000035B4
		public override void GenerateIV()
		{
			this.m_rijndael.GenerateIV();
		}

		/// <summary>Generates a random key to use for the symmetric algorithm. </summary>
		// Token: 0x060001E8 RID: 488 RVA: 0x000053C1 File Offset: 0x000035C1
		public override void GenerateKey()
		{
			this.m_rijndael.GenerateKey();
		}

		// Token: 0x0400026C RID: 620
		private RijndaelManaged m_rijndael;
	}
}
