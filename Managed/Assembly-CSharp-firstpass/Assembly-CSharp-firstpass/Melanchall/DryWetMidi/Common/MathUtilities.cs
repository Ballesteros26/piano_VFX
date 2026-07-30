using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001C5 RID: 453
	internal static class MathUtilities
	{
		// Token: 0x06000B51 RID: 2897 RVA: 0x00024AE7 File Offset: 0x00022CE7
		public static bool IsPowerOfTwo(int value)
		{
			return value != 0 && (value & (value - 1)) == 0;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00024AF8 File Offset: 0x00022CF8
		public static long LeastCommonMultiple(long a, long b)
		{
			ThrowIfArgument.IsNonpositive("a", a, "First number is zero or negative.");
			ThrowIfArgument.IsNonpositive("b", b, "Second number is zero or negative.");
			long num;
			long num2;
			if (a > b)
			{
				num = a;
				num2 = b;
			}
			else
			{
				num = b;
				num2 = a;
			}
			int num3 = 1;
			while ((long)num3 < num2)
			{
				if (num * (long)num3 % num2 == 0L)
				{
					return (long)num3 * num;
				}
				num3++;
			}
			return num * num2;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00024B50 File Offset: 0x00022D50
		public static long GreatestCommonDivisor(long a, long b)
		{
			while (b != 0L)
			{
				long num = a % b;
				a = b;
				b = num;
			}
			return a;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00024B60 File Offset: 0x00022D60
		public static Tuple<long, long> SolveDiophantineEquation(long a, long b)
		{
			long num = MathUtilities.GreatestCommonDivisor(a, b);
			return Tuple.Create<long, long>(b / num, -a / num);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00024B81 File Offset: 0x00022D81
		public static double Round(double value)
		{
			return Math.Round(value, MidpointRounding.AwayFromZero);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00024B8A File Offset: 0x00022D8A
		public static double Round(double value, int digits)
		{
			return Math.Round(value, digits, MidpointRounding.AwayFromZero);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00024B94 File Offset: 0x00022D94
		public static long RoundToLong(double value)
		{
			return (long)MathUtilities.Round(value);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00024B9D File Offset: 0x00022D9D
		public static IEnumerable<T[]> GetPermutations<T>(T[] objects)
		{
			return MathUtilities.GetPermutations<T>(objects, objects.Length);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00024BA8 File Offset: 0x00022DA8
		private static IEnumerable<T[]> GetPermutations<T>(T[] objects, int k)
		{
			if (k == 1)
			{
				yield return objects;
			}
			else
			{
				foreach (T[] array in MathUtilities.GetPermutations<T>(objects, k - 1))
				{
					yield return array;
				}
				IEnumerator<T[]> enumerator = null;
				int num3;
				for (int i = 0; i < k - 1; i = num3 + 1)
				{
					int num = ((k % 2 == 0) ? i : 0);
					int num2 = k - 1;
					if (objects[num].Equals(objects[num2]))
					{
						break;
					}
					T t = objects[num];
					objects[num] = objects[num2];
					objects[num2] = t;
					foreach (T[] array2 in MathUtilities.GetPermutations<T>(objects, k - 1))
					{
						yield return array2;
					}
					enumerator = null;
					num3 = i;
				}
			}
			yield break;
			yield break;
		}
	}
}
