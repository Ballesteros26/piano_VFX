using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000C7 RID: 199
	public sealed class TimeSpanComparer : IComparer<ITimeSpan>
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x00016DE0 File Offset: 0x00014FE0
		public int Compare(ITimeSpan x, ITimeSpan y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return x.CompareTo(y);
		}
	}
}
