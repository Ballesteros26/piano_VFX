using System;

namespace System.Numerics.Hashing
{
	// Token: 0x0200001B RID: 27
	internal static class HashHelpers
	{
		// Token: 0x06000267 RID: 615 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		public static int Combine(int h1, int h2)
		{
			return (((h1 << 5) | (int)((uint)h1 >> 27)) + h1) ^ h2;
		}

		// Token: 0x04000097 RID: 151
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
