using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A28 RID: 2600
	internal static class IntrospectiveSortUtilities
	{
		// Token: 0x06005FE5 RID: 24549 RVA: 0x0013B804 File Offset: 0x00139A04
		internal static int FloorLog2(int n)
		{
			int num = 0;
			while (n >= 1)
			{
				num++;
				n /= 2;
			}
			return num;
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x0013B823 File Offset: 0x00139A23
		internal static void ThrowOrIgnoreBadComparer(object comparer)
		{
			throw new ArgumentException(SR.Format("Unable to sort because the IComparer.Compare() method returns inconsistent results. Either a value does not compare equal to itself, or one value repeatedly compared to another value yields different results. IComparer: '{0}'.", comparer));
		}

		// Token: 0x04003058 RID: 12376
		internal const int IntrosortSizeThreshold = 16;
	}
}
