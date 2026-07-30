using System;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Performs symmetric encryption and decryption using the Cryptographic Application Programming Interfaces (CAPI) implementation of the Advanced Encryption Standard (AES) algorithm. </summary>
	// Token: 0x02000078 RID: 120
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AesCryptoServiceProvider : Aes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesCryptoServiceProvider" /> class. </summary>
		/// <exception cref="T:System.PlatformNotSupportedException">There is no supported key size for the current platform.</exception>
		// Token: 0x060002C7 RID: 711 RVA: 0x0000625A File Offset: 0x0000445A
		public AesCryptoServiceProvider()
		{
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Generates a random initialization vector (IV) to use for the algorithm.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The initialization vector (IV) could not be generated. </exception>
		// Token: 0x060002C8 RID: 712 RVA: 0x00006269 File Offset: 0x00004469
		public override void GenerateIV()
		{
			this.IVValue = KeyBuilder.IV(this.BlockSizeValue >> 3);
		}

		/// <summary>Generates a random key to use for the algorithm. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key could not be generated.</exception>
		// Token: 0x060002C9 RID: 713 RVA: 0x0000627E File Offset: 0x0000447E
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		/// <summary>Creates a symmetric AES decryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES decryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="iv" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is invalid.</exception>
		// Token: 0x060002CA RID: 714 RVA: 0x00006293 File Offset: 0x00004493
		public override ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			if (this.Mode == CipherMode.CFB && this.FeedbackSize > 64)
			{
				throw new CryptographicException("CFB with Feedbaack > 64 bits");
			}
			return new AesTransform(this, false, key, iv);
		}

		/// <summary>Creates a symmetric encryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES encryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="iv" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is invalid.</exception>
		// Token: 0x060002CB RID: 715 RVA: 0x000062BC File Offset: 0x000044BC
		public override ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			if (this.Mode == CipherMode.CFB && this.FeedbackSize > 64)
			{
				throw new CryptographicException("CFB with Feedbaack > 64 bits");
			}
			return new AesTransform(this, true, key, iv);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000062E5 File Offset: 0x000044E5
		// (set) Token: 0x060002CD RID: 717 RVA: 0x000062ED File Offset: 0x000044ED
		public override byte[] IV
		{
			get
			{
				return base.IV;
			}
			set
			{
				base.IV = value;
			}
		}

		/// <summary>Gets or sets the symmetric key that is used for encryption and decryption.</summary>
		/// <returns>The symmetric key that is used for encryption and decryption.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for the key is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The size of the key is invalid.</exception>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000062F6 File Offset: 0x000044F6
		// (set) Token: 0x060002CF RID: 719 RVA: 0x000062FE File Offset: 0x000044FE
		public override byte[] Key
		{
			get
			{
				return base.Key;
			}
			set
			{
				base.Key = value;
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key. </summary>
		/// <returns>The size, in bits, of the key.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00006307 File Offset: 0x00004507
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000630F File Offset: 0x0000450F
		public override int KeySize
		{
			get
			{
				return base.KeySize;
			}
			set
			{
				base.KeySize = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00006318 File Offset: 0x00004518
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00006320 File Offset: 0x00004520
		public override int FeedbackSize
		{
			get
			{
				return base.FeedbackSize;
			}
			set
			{
				base.FeedbackSize = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00006329 File Offset: 0x00004529
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00006331 File Offset: 0x00004531
		public override CipherMode Mode
		{
			get
			{
				return base.Mode;
			}
			set
			{
				if (value == CipherMode.CTS)
				{
					throw new CryptographicException("CTS is not supported");
				}
				base.Mode = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00006349 File Offset: 0x00004549
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00006351 File Offset: 0x00004551
		public override PaddingMode Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Creates a symmetric AES decryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES decryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The current key is invalid or missing.</exception>
		// Token: 0x060002D8 RID: 728 RVA: 0x0000635A File Offset: 0x0000455A
		public override ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		/// <summary>Creates a symmetric AES encryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES encryptor object.</returns>
		// Token: 0x060002D9 RID: 729 RVA: 0x0000636E File Offset: 0x0000456E
		public override ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00006382 File Offset: 0x00004582
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
