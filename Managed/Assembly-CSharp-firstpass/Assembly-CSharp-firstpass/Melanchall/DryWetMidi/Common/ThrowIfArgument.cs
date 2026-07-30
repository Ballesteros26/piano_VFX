using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001CE RID: 462
	internal static class ThrowIfArgument
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x0002505F File Offset: 0x0002325F
		internal static void IsNull(string parameterName, object argument)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002506B File Offset: 0x0002326B
		internal static void ContainsNull<T>(string parameterName, IEnumerable<T> argument)
		{
			if (argument.Any((T e) => e == null))
			{
				throw new ArgumentException("Collection contains null.", parameterName);
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000250A0 File Offset: 0x000232A0
		internal static void IsInvalidEnumValue<TEnum>(string parameterName, TEnum argument) where TEnum : struct
		{
			if (!Enum.IsDefined(typeof(TEnum), argument))
			{
				throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(argument), typeof(TEnum));
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000250D5 File Offset: 0x000232D5
		internal static void IsOutOfRange(string parameterName, TimeSpan value, TimeSpan min, TimeSpan max, string message)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000250F8 File Offset: 0x000232F8
		internal static void IsOutOfRange(string parameterName, int value, int min, int max, string message)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00025111 File Offset: 0x00023311
		internal static void IsOutOfRange(string parameterName, long value, long min, long max, string message)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002512A File Offset: 0x0002332A
		internal static void IsOutOfRange(string parameterName, double value, double min, double max, string message)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00025143 File Offset: 0x00023343
		internal static void IsOutOfRange(string parameterName, int value, string message, params int[] values)
		{
			if (Array.IndexOf<int>(values, value) < 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002515D File Offset: 0x0002335D
		internal static void DoesntSatisfyCondition(string parameterName, int value, Predicate<int> condition, string message)
		{
			if (!condition(value))
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00025176 File Offset: 0x00023376
		internal static void IsGreaterThan(string parameterName, int value, int reference, string message)
		{
			ThrowIfArgument.IsOutOfRange(parameterName, value, int.MinValue, reference, message);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00025186 File Offset: 0x00023386
		internal static void IsGreaterThan(string parameterName, long value, long reference, string message)
		{
			ThrowIfArgument.IsOutOfRange(parameterName, value, long.MinValue, reference, message);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002519A File Offset: 0x0002339A
		internal static void IsLessThan(string parameterName, int value, int reference, string message)
		{
			ThrowIfArgument.IsOutOfRange(parameterName, value, reference, int.MaxValue, message);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000251AA File Offset: 0x000233AA
		internal static void IsLessThan(string parameterName, long value, long reference, string message)
		{
			ThrowIfArgument.IsOutOfRange(parameterName, value, reference, long.MaxValue, message);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000251BE File Offset: 0x000233BE
		internal static void IsLessThan(string parameterName, double value, double reference, string message)
		{
			ThrowIfArgument.IsOutOfRange(parameterName, value, reference, double.MaxValue, message);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000251D2 File Offset: 0x000233D2
		internal static void IsNegative(string parameterName, int value, string message)
		{
			ThrowIfArgument.IsLessThan(parameterName, value, 0, message);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000251DD File Offset: 0x000233DD
		internal static void IsNegative(string parameterName, long value, string message)
		{
			ThrowIfArgument.IsLessThan(parameterName, value, 0L, message);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000251E9 File Offset: 0x000233E9
		internal static void IsNegative(string parameterName, double value, string message)
		{
			ThrowIfArgument.IsLessThan(parameterName, value, 0.0, message);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000251FC File Offset: 0x000233FC
		internal static void IsNonpositive(string parameterName, int value, string message)
		{
			ThrowIfArgument.IsLessThan(parameterName, value, 1, message);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00025207 File Offset: 0x00023407
		internal static void IsNonpositive(string parameterName, long value, string message)
		{
			ThrowIfArgument.IsLessThan(parameterName, value, 1L, message);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00025213 File Offset: 0x00023413
		internal static void IsNonpositive(string parameterName, double value, string message)
		{
			ThrowIfArgument.IsLessThan(parameterName, value, double.Epsilon, message);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00025226 File Offset: 0x00023426
		internal static void IsNullOrWhiteSpaceString(string parameterName, string value, string stringDescription)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(stringDescription + " is null or contains white-spaces only.", parameterName);
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00025242 File Offset: 0x00023442
		internal static void IsNullOrEmptyString(string parameterName, string value, string stringDescription)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(stringDescription + " is null or empty.", parameterName);
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002525E File Offset: 0x0002345E
		internal static void IsInvalidIndex(string parameterName, int index, int collectionSize)
		{
			ThrowIfArgument.IsOutOfRange(parameterName, index, 0, collectionSize, "Index is out of range.");
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002526E File Offset: 0x0002346E
		internal static void IsEmptyCollection<T>(string parameterName, IEnumerable<T> collection, string message)
		{
			if (!collection.Any<T>())
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00025280 File Offset: 0x00023480
		internal static void ContainsInvalidEnumValue<TEnum>(string parameterName, IEnumerable<TEnum> argument) where TEnum : struct
		{
			foreach (TEnum tenum in argument)
			{
				if (!Enum.IsDefined(typeof(TEnum), tenum))
				{
					throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(tenum), typeof(TEnum));
				}
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000252F4 File Offset: 0x000234F4
		internal static void StartsWithInvalidValue<T>(string parameterName, IEnumerable<T> collection, T invalidValue, string message)
		{
			if (collection != null)
			{
				T t = collection.First<T>();
				if (t.Equals(invalidValue))
				{
					throw new ArgumentException(message, parameterName);
				}
			}
		}

		// Token: 0x04000A2A RID: 2602
		private const int MinNonnegativeValue = 0;

		// Token: 0x04000A2B RID: 2603
		private const int MinPositiveValue = 1;
	}
}
