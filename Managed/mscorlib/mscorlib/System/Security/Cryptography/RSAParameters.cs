using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the standard parameters for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
	// Token: 0x0200067B RID: 1659
	[ComVisible(true)]
	[Serializable]
	public struct RSAParameters
	{
		/// <summary>Represents the Exponent parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04002493 RID: 9363
		public byte[] Exponent;

		/// <summary>Represents the Modulus parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04002494 RID: 9364
		public byte[] Modulus;

		/// <summary>Represents the P parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04002495 RID: 9365
		[NonSerialized]
		public byte[] P;

		/// <summary>Represents the Q parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04002496 RID: 9366
		[NonSerialized]
		public byte[] Q;

		/// <summary>Represents the DP parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04002497 RID: 9367
		[NonSerialized]
		public byte[] DP;

		/// <summary>Represents the DQ parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04002498 RID: 9368
		[NonSerialized]
		public byte[] DQ;

		/// <summary>Represents the InverseQ parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04002499 RID: 9369
		[NonSerialized]
		public byte[] InverseQ;

		/// <summary>Represents the D parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x0400249A RID: 9370
		[NonSerialized]
		public byte[] D;
	}
}
