using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) using the <see cref="T:System.Security.Cryptography.SHA512" /> hash function.</summary>
	// Token: 0x02000667 RID: 1639
	[ComVisible(true)]
	public class HMACSHA512 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA512" /> class with a randomly generated key.</summary>
		// Token: 0x0600467E RID: 18046 RVA: 0x000F82B7 File Offset: 0x000F64B7
		public HMACSHA512()
			: this(Utils.GenerateRandom(128))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA512" /> class with the specified key data.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACSHA512" /> encryption. The key can be any length. However, the recommended size is 128 bytes. If the key is more than 128 bytes long, it is hashed (using SHA-512) to derive a 128-byte key. If it is less than 128 bytes long, it is padded to 128 bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null. </exception>
		// Token: 0x0600467F RID: 18047 RVA: 0x000F82CC File Offset: 0x000F64CC
		[SecuritySafeCritical]
		public HMACSHA512(byte[] key)
		{
			this.m_hashName = "SHA512";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA512Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA512Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider"));
			this.HashSizeValue = 512;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004680 RID: 18048 RVA: 0x000F83A5 File Offset: 0x000F65A5
		private int BlockSize
		{
			get
			{
				if (!this.m_useLegacyBlockSize)
				{
					return 128;
				}
				return 64;
			}
		}

		/// <summary>Provides a workaround for the .NET Framework 2.0 implementation of the <see cref="T:System.Security.Cryptography.HMACSHA512" /> algorithm, which is inconsistent with the .NET Framework 2.0 Service Pack 1 implementation.</summary>
		/// <returns>true to enable .NET Framework 2.0 Service Pack 1 applications to interact with .NET Framework 2.0 applications; otherwise, false.</returns>
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x000F83B7 File Offset: 0x000F65B7
		// (set) Token: 0x06004682 RID: 18050 RVA: 0x000F83BF File Offset: 0x000F65BF
		public bool ProduceLegacyHmacValues
		{
			get
			{
				return this.m_useLegacyBlockSize;
			}
			set
			{
				this.m_useLegacyBlockSize = value;
				base.BlockSizeValue = this.BlockSize;
				base.InitializeKey(this.KeyValue);
			}
		}

		// Token: 0x04002449 RID: 9289
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();
	}
}
