using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections
{
	// Token: 0x020009CA RID: 2506
	[FriendAccessAllowed]
	internal static class HashHelpers
	{
		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06005CDF RID: 23775 RVA: 0x0013335C File Offset: 0x0013155C
		internal static ConditionalWeakTable<object, SerializationInfo> SerializationInfoTable
		{
			get
			{
				if (HashHelpers.s_SerializationInfoTable == null)
				{
					ConditionalWeakTable<object, SerializationInfo> conditionalWeakTable = new ConditionalWeakTable<object, SerializationInfo>();
					Interlocked.CompareExchange<ConditionalWeakTable<object, SerializationInfo>>(ref HashHelpers.s_SerializationInfoTable, conditionalWeakTable, null);
				}
				return HashHelpers.s_SerializationInfoTable;
			}
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x00133388 File Offset: 0x00131588
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool IsPrime(int candidate)
		{
			if ((candidate & 1) != 0)
			{
				int num = (int)Math.Sqrt((double)candidate);
				for (int i = 3; i <= num; i += 2)
				{
					if (candidate % i == 0)
					{
						return false;
					}
				}
				return true;
			}
			return candidate == 2;
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x001333BC File Offset: 0x001315BC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int GetPrime(int min)
		{
			if (min < 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table."));
			}
			for (int i = 0; i < HashHelpers.primes.Length; i++)
			{
				int num = HashHelpers.primes[i];
				if (num >= min)
				{
					return num;
				}
			}
			for (int j = min | 1; j < 2147483647; j += 2)
			{
				if (HashHelpers.IsPrime(j) && (j - 1) % 101 != 0)
				{
					return j;
				}
			}
			return min;
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x00133422 File Offset: 0x00131622
		public static int GetMinPrime()
		{
			return HashHelpers.primes[0];
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x0013342C File Offset: 0x0013162C
		public static int ExpandPrime(int oldSize)
		{
			int num = 2 * oldSize;
			if (num > 2146435069 && 2146435069 > oldSize)
			{
				return 2146435069;
			}
			return HashHelpers.GetPrime(num);
		}

		// Token: 0x04002F62 RID: 12130
		public const int HashCollisionThreshold = 100;

		// Token: 0x04002F63 RID: 12131
		public static readonly int[] primes = new int[]
		{
			3, 7, 11, 17, 23, 29, 37, 47, 59, 71,
			89, 107, 131, 163, 197, 239, 293, 353, 431, 521,
			631, 761, 919, 1103, 1327, 1597, 1931, 2333, 2801, 3371,
			4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591, 17519, 21023,
			25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363,
			156437, 187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403,
			968897, 1162687, 1395263, 1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559,
			5999471, 7199369
		};

		// Token: 0x04002F64 RID: 12132
		private static ConditionalWeakTable<object, SerializationInfo> s_SerializationInfoTable;

		// Token: 0x04002F65 RID: 12133
		public const int MaxPrimeArrayLength = 2146435069;
	}
}
