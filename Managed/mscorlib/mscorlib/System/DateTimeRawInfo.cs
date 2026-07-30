using System;
using System.Security;

namespace System
{
	// Token: 0x02000175 RID: 373
	internal struct DateTimeRawInfo
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x00045573 File Offset: 0x00043773
		[SecurityCritical]
		internal unsafe void Init(int* numberBuffer)
		{
			this.month = -1;
			this.year = -1;
			this.dayOfWeek = -1;
			this.era = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
			this.fraction = -1.0;
			this.num = numberBuffer;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000455B0 File Offset: 0x000437B0
		[SecuritySafeCritical]
		internal unsafe void AddNumber(int value)
		{
			ref int ptr = ref *this.num;
			int num = this.numCount;
			this.numCount = num + 1;
			*((ref ptr) + (IntPtr)num * 4) = value;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000455DA File Offset: 0x000437DA
		[SecuritySafeCritical]
		internal unsafe int GetNumber(int index)
		{
			return this.num[index];
		}

		// Token: 0x0400099D RID: 2461
		[SecurityCritical]
		private unsafe int* num;

		// Token: 0x0400099E RID: 2462
		internal int numCount;

		// Token: 0x0400099F RID: 2463
		internal int month;

		// Token: 0x040009A0 RID: 2464
		internal int year;

		// Token: 0x040009A1 RID: 2465
		internal int dayOfWeek;

		// Token: 0x040009A2 RID: 2466
		internal int era;

		// Token: 0x040009A3 RID: 2467
		internal DateTimeParse.TM timeMark;

		// Token: 0x040009A4 RID: 2468
		internal double fraction;

		// Token: 0x040009A5 RID: 2469
		internal bool hasSameDateAndTimeSeparators;

		// Token: 0x040009A6 RID: 2470
		internal bool timeZone;
	}
}
