using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of the <see cref="T:System.Security.Cryptography.Rijndael" /> symmetric encryption algorithm must inherit.</summary>
	// Token: 0x02000675 RID: 1653
	[ComVisible(true)]
	public abstract class Rijndael : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.Rijndael" />.</summary>
		// Token: 0x060046F4 RID: 18164 RVA: 0x000F9686 File Offset: 0x000F7886
		protected Rijndael()
		{
			this.KeySizeValue = 256;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = Rijndael.s_legalBlockSizes;
			this.LegalKeySizesValue = Rijndael.s_legalKeySizes;
		}

		/// <summary>Creates a cryptographic object to perform the <see cref="T:System.Security.Cryptography.Rijndael" /> algorithm.</summary>
		/// <returns>A cryptographic object.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060046F5 RID: 18165 RVA: 0x000F96C6 File Offset: 0x000F78C6
		public new static Rijndael Create()
		{
			return Rijndael.Create("System.Security.Cryptography.Rijndael");
		}

		/// <summary>Creates a cryptographic object to perform the specified implementation of the <see cref="T:System.Security.Cryptography.Rijndael" /> algorithm.</summary>
		/// <returns>A cryptographic object.</returns>
		/// <param name="algName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.Rijndael" /> to create. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="algName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x060046F6 RID: 18166 RVA: 0x000F96D2 File Offset: 0x000F78D2
		public new static Rijndael Create(string algName)
		{
			return (Rijndael)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x04002472 RID: 9330
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};

		// Token: 0x04002473 RID: 9331
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
