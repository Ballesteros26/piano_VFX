using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000D1 RID: 209
	public sealed class TimedEventsReadingHandler : ReadingHandler
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x000179A2 File Offset: 0x00015BA2
		public TimedEventsReadingHandler(bool sortEvents)
			: base(ReadingHandler.TargetScope.Event)
		{
			this._sortEvents = sortEvents;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000179C0 File Offset: 0x00015BC0
		public IEnumerable<TimedEvent> TimedEvents
		{
			get
			{
				IEnumerable<TimedEvent> enumerable;
				if ((enumerable = this._timedEventsProcessed) == null)
				{
					IEnumerable<TimedEvent> enumerable3;
					if (!this._sortEvents)
					{
						IEnumerable<TimedEvent> enumerable2 = this._timedEvents;
						enumerable3 = enumerable2;
					}
					else
					{
						IEnumerable<TimedEvent> enumerable2 = this._timedEvents.OrderBy((TimedEvent e) => e.Time);
						enumerable3 = enumerable2;
					}
					enumerable = (this._timedEventsProcessed = enumerable3.ToList<TimedEvent>());
				}
				return enumerable;
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00017A23 File Offset: 0x00015C23
		public override void Initialize()
		{
			this._timedEvents.Clear();
			this._timedEventsProcessed = null;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00017A37 File Offset: 0x00015C37
		public override void OnFinishEventReading(MidiEvent midiEvent, long absoluteTime)
		{
			this._timedEvents.Add(new TimedEvent(midiEvent, absoluteTime));
		}

		// Token: 0x04000729 RID: 1833
		private readonly bool _sortEvents;

		// Token: 0x0400072A RID: 1834
		private readonly List<TimedEvent> _timedEvents = new List<TimedEvent>();

		// Token: 0x0400072B RID: 1835
		private IEnumerable<TimedEvent> _timedEventsProcessed;
	}
}
