using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000198 RID: 408
	internal static class TimeDivisionFactory
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x00021D5B File Offset: 0x0001FF5B
		internal static TimeDivision GetTimeDivision(short division)
		{
			if (division < 0)
			{
				division = -division;
				return new SmpteTimeDivision((SmpteFormat)division.GetHead(), division.GetTail());
			}
			return new TicksPerQuarterNoteTimeDivision(division);
		}
	}
}
