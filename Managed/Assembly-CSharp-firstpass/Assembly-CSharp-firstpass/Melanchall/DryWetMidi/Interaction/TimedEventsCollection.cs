using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000CD RID: 205
	public sealed class TimedEventsCollection : TimedObjectsCollection<TimedEvent>
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x00017333 File Offset: 0x00015533
		internal TimedEventsCollection(IEnumerable<TimedEvent> events, Comparison<MidiEvent> sameTimeEventsComparison)
			: base(events)
		{
			this._eventsComparer = new TimedEventsComparer(sameTimeEventsComparison);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017348 File Offset: 0x00015548
		public override IEnumerator<TimedEvent> GetEnumerator()
		{
			return this._objects.OrderBy((TimedEvent e) => e, this._eventsComparer).GetEnumerator();
		}

		// Token: 0x04000724 RID: 1828
		private readonly TimedEventsComparer _eventsComparer;
	}
}
