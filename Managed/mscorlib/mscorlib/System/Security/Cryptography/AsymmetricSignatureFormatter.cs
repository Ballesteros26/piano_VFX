using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of asymmetric signature formatters derive.</summary>
	// Token: 0x02000647 RID: 1607
	[ComVisible(true)]
	public abstract class AsymmetricSignatureFormatter
	{
		/// <summary>When overridden in a derived class, sets the asymmetric algorithm to use to create the signature.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> to use to create the signature. </param>
		// Token: 0x060045B2 RID: 17842
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, sets the hash algorithm to use for creating the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for creating the signature. </param>
		// Token: 0x060045B3 RID: 17843
		public abstract void SetHashAlgorithm(string strName);

		/// <summary>Creates the signature from the specified hash value.</summary>
		/// <returns>The signature for the specified hash value.</returns>
		/// <param name="hash">The hash algorithm to use to create the signature. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hash" /> parameter is null. </exception>
		// Token: 0x060045B4 RID: 17844 RVA: 0x000F4C4E File Offset: 0x000F2E4E
		public virtual byte[] CreateSignature(HashAlgorithm hash)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.CreateSignature(hash.Hash);
		}

		/// <summary>When overridden in a derived class, creates the signature for the specified data.</summary>
		/// <returns>The digital signature for the <paramref name="rgbHash" /> parameter.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		// Token: 0x060045B5 RID: 17845
		public abstract byte[] CreateSignature(byte[] rgbHash);
	}
}
