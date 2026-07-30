using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) using the <see cref="T:System.Security.Cryptography.SHA384" /> hash function.</summary>
	// Token: 0x02000665 RID: 1637
	[ComVisible(true)]
	public class HMACSHA384 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA384" /> class by using a randomly generated key.</summary>
		// Token: 0x06004673 RID: 18035 RVA: 0x000F8171 File Offset: 0x000F6371
		public HMACSHA384()
			: this(Utils.GenerateRandom(128))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA384" /> class by using the specified key data.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACSHA384" /> encryption. The key can be any length. However, the recommended size is 128 bytes. If the key is more than 128 bytes long, it is hashed (using SHA-384) to derive a 128-byte key. If it is less than 128 bytes long, it is padded to 128 bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null. </exception>
		// Token: 0x06004674 RID: 18036 RVA: 0x000F8184 File Offset: 0x000F6384
		[SecuritySafeCritical]
		public HMACSHA384(byte[] key)
		{
			this.m_hashName = "SHA384";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA384Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA384Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider"));
			this.HashSizeValue = 384;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x000F825D File Offset: 0x000F645D
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

		/// <summary>Provides a workaround for the .NET Framework 2.0 implementation of the <see cref="T:System.Security.Cryptography.HMACSHA384" /> algorithm, which is inconsistent with the .NET Framework 2.0 Service Pack 1 implementation of the algorithm.</summary>
		/// <returns>true to enable .NET Framework 2.0 Service Pack 1 applications to interact with .NET Framework 2.0 applications; otherwise, false.</returns>
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x000F826F File Offset: 0x000F646F
		// (set) Token: 0x06004677 RID: 18039 RVA: 0x000F8277 File Offset: 0x000F6477
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

		// Token: 0x04002443 RID: 9283
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();
	}
}
