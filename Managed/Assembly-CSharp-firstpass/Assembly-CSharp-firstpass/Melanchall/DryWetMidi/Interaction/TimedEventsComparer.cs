using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000CE RID: 206
	internal sealed class TimedEventsComparer : IComparer<TimedEvent>
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x0001737F File Offset: 0x0001557F
		internal TimedEventsComparer(Comparison<MidiEvent> sameTimeEventsComparison)
		{
			this._sameTimeEventsComparison = sameTimeEventsComparison;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00017390 File Offset: 0x00015590
		public int Compare(TimedEvent x, TimedEvent y)
		{
			if (x == null && y == null)
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
			int num = Math.Sign(x.Time - y.Time);
			if (num != 0)
			{
				return num;
			}
			Comparison<MidiEvent> sameTimeEventsComparison = this._sameTimeEventsComparison;
			if (sameTimeEventsComparison == null)
			{
				return 0;
			}
			return sameTimeEventsComparison(x.Event, y.Event);
		}

		// Token: 0x04000725 RID: 1829
		private readonly Comparison<MidiEvent> _sameTimeEventsComparison;
	}
}
