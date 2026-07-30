using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000449 RID: 1097
	internal static class HashHelpers
	{
		// Token: 0x0600349E RID: 13470 RVA: 0x000C30BB File Offset: 0x000C12BB
		public static int Combine(int h1, int h2)
		{
			return (((h1 << 5) | (int)((uint)h1 >> 27)) + h1) ^ h2;
		}

		// Token: 0x04001C20 RID: 7200
		public static readonly int RandomSeed = new Random().Next(int.MinValue, int.MaxValue);
	}
}
