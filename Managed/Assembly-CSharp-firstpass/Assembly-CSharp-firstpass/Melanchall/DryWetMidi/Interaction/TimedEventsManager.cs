using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000CF RID: 207
	public sealed class TimedEventsManager : IDisposable
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x000173E4 File Offset: 0x000155E4
		public TimedEventsManager(EventsCollection eventsCollection, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			this._eventsCollection = eventsCollection;
			this.Events = new TimedEventsCollection(TimedEventsManager.CreateTimedEvents(eventsCollection), sameTimeEventsComparison);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00017410 File Offset: 0x00015610
		public TimedEventsCollection Events { get; }

		// Token: 0x0600051B RID: 1307 RVA: 0x00017418 File Offset: 0x00015618
		public void SaveChanges()
		{
			this._eventsCollection.Clear();
			long num = 0L;
			foreach (TimedEvent timedEvent in this.Events)
			{
				MidiEvent @event = timedEvent.Event;
				@event.DeltaTime = timedEvent.Time - num;
				this._eventsCollection.Add(@event);
				num = timedEvent.Time;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00017494 File Offset: 0x00015694
		private static IEnumerable<TimedEvent> CreateTimedEvents(EventsCollection events)
		{
			ThrowIfArgument.IsNull("events", events);
			long time = 0L;
			foreach (MidiEvent midiEvent in events)
			{
				time += midiEvent.DeltaTime;
				yield return new TimedEvent(midiEvent.Clone(), time);
			}
			IEnumerator<MidiEvent> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000174A4 File Offset: 0x000156A4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000174AD File Offset: 0x000156AD
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this.SaveChanges();
			}
			this._disposed = true;
		}

		// Token: 0x04000726 RID: 1830
		private readonly EventsCollection _eventsCollection;

		// Token: 0x04000727 RID: 1831
		private bool _disposed;
	}
}
