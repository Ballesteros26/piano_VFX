using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000004 RID: 4
	internal static class MathF
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020AE File Offset: 0x000002AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float x)
		{
			return Math.Abs(x);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020B6 File Offset: 0x000002B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Acos(float x)
		{
			return (float)Math.Acos((double)x);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020C0 File Offset: 0x000002C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Cos(float x)
		{
			return (float)Math.Cos((double)x);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020CA File Offset: 0x000002CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float IEEERemainder(float x, float y)
		{
			return (float)Math.IEEERemainder((double)x, (double)y);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020D6 File Offset: 0x000002D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Pow(float x, float y)
		{
			return (float)Math.Pow((double)x, (double)y);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020E2 File Offset: 0x000002E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sin(float x)
		{
			return (float)Math.Sin((double)x);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020EC File Offset: 0x000002EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sqrt(float x)
		{
			return (float)Math.Sqrt((double)x);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020F6 File Offset: 0x000002F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Tan(float x)
		{
			return (float)Math.Tan((double)x);
		}

		// Token: 0x0400003C RID: 60
		public const float PI = 3.1415927f;
	}
}
