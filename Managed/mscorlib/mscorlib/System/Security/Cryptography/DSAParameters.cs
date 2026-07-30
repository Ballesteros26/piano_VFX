using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains the typical parameters for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
	// Token: 0x0200065A RID: 1626
	[ComVisible(true)]
	[Serializable]
	public struct DSAParameters
	{
		/// <summary>Specifies the P parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04002427 RID: 9255
		public byte[] P;

		/// <summary>Specifies the Q parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04002428 RID: 9256
		public byte[] Q;

		/// <summary>Specifies the G parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04002429 RID: 9257
		public byte[] G;

		/// <summary>Specifies the Y parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400242A RID: 9258
		public byte[] Y;

		/// <summary>Specifies the J parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400242B RID: 9259
		public byte[] J;

		/// <summary>Specifies the X parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400242C RID: 9260
		[NonSerialized]
		public byte[] X;

		/// <summary>Specifies the seed for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400242D RID: 9261
		public byte[] Seed;

		/// <summary>Specifies the counter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x0400242E RID: 9262
		public int Counter;
	}
}
