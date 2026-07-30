using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) by using the <see cref="T:System.Security.Cryptography.SHA256" /> hash function.</summary>
	// Token: 0x02000663 RID: 1635
	[ComVisible(true)]
	public class HMACSHA256 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA256" /> class with a randomly generated key.</summary>
		// Token: 0x0600466B RID: 18027 RVA: 0x000F8081 File Offset: 0x000F6281
		public HMACSHA256()
			: this(Utils.GenerateRandom(64))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA256" /> class with the specified key data.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACSHA256" /> encryption. The key can be any length. However, the recommended size is 64 bytes. If the key is more than 64 bytes long, it is hashed (using SHA-256) to derive a 64-byte key. If it is less than 64 bytes long, it is padded to 64 bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null. </exception>
		// Token: 0x0600466C RID: 18028 RVA: 0x000F8090 File Offset: 0x000F6290
		public HMACSHA256(byte[] key)
		{
			this.m_hashName = "SHA256";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA256Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA256Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider"));
			this.HashSizeValue = 256;
			base.InitializeKey(key);
		}
	}
}
