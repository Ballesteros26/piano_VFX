using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000080 RID: 128
	internal static class IntervalUtilities
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0000DE4F File Offset: 0x0000C04F
		internal static bool IsIntervalValid(int halfSteps)
		{
			return halfSteps >= (int)(-(int)SevenBitNumber.MaxValue) && halfSteps <= (int)SevenBitNumber.MaxValue;
		}
	}
}
