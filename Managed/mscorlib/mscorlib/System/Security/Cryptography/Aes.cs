using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of the Advanced Encryption Standard (AES) must inherit. </summary>
	// Token: 0x02000642 RID: 1602
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	public abstract class Aes : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Aes" /> class. </summary>
		// Token: 0x06004591 RID: 17809 RVA: 0x000F4A94 File Offset: 0x000F2C94
		protected Aes()
		{
			this.LegalBlockSizesValue = Aes.s_legalBlockSizes;
			this.LegalKeySizesValue = Aes.s_legalKeySizes;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = 8;
			this.KeySizeValue = 256;
			this.ModeValue = CipherMode.CBC;
		}

		/// <summary>Creates a cryptographic object that is used to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object that is used to perform the symmetric algorithm.</returns>
		// Token: 0x06004592 RID: 17810 RVA: 0x000F4AE1 File Offset: 0x000F2CE1
		public new static Aes Create()
		{
			return Aes.Create("AES");
		}

		/// <summary>Creates a cryptographic object that specifies the implementation of AES to use to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object that is used to perform the symmetric algorithm.</returns>
		/// <param name="algorithmName">The name of the specific implementation of AES to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithmName" /> parameter is null.</exception>
		// Token: 0x06004593 RID: 17811 RVA: 0x000F4AED File Offset: 0x000F2CED
		public new static Aes Create(string algorithmName)
		{
			if (algorithmName == null)
			{
				throw new ArgumentNullException("algorithmName");
			}
			return CryptoConfig.CreateFromName(algorithmName) as Aes;
		}

		// Token: 0x040023CC RID: 9164
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 128, 0)
		};

		// Token: 0x040023CD RID: 9165
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
