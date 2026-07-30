using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all asymmetric key exchange deformatters derive.</summary>
	// Token: 0x02000644 RID: 1604
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeDeformatter
	{
		/// <summary>When overridden in a derived class, gets or sets the parameters for the asymmetric key exchange.</summary>
		/// <returns>A string in XML format containing the parameters of the asymmetric key exchange operation.</returns>
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060045A3 RID: 17827
		// (set) Token: 0x060045A4 RID: 17828
		public abstract string Parameters { get; set; }

		/// <summary>When overridden in a derived class, sets the private key to use for decrypting the secret information.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the private key. </param>
		// Token: 0x060045A5 RID: 17829
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, extracts secret information from the encrypted key exchange data.</summary>
		/// <returns>The secret information derived from the key exchange data.</returns>
		/// <param name="rgb">The key exchange data within which the secret information is hidden. </param>
		// Token: 0x060045A6 RID: 17830
		public abstract byte[] DecryptKeyExchange(byte[] rgb);
	}
}
