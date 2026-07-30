using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000D0 RID: 208
	public static class TimedEventsManagingUtilities
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x000174C8 File Offset: 0x000156C8
		public static TimedEvent SetTime(this TimedEvent timedEvent, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("timedEvent", timedEvent);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			timedEvent.Time = TimeConverter.ConvertFrom(time, tempoMap);
			return timedEvent;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000174F9 File Offset: 0x000156F9
		public static TimedEventsManager ManageTimedEvents(this EventsCollection eventsCollection, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return new TimedEventsManager(eventsCollection, sameTimeEventsComparison);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001750D File Offset: 0x0001570D
		public static TimedEventsManager ManageTimedEvents(this TrackChunk trackChunk, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.ManageTimedEvents(sameTimeEventsComparison);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00017526 File Offset: 0x00015726
		public static IEnumerable<TimedEvent> GetTimedEvents(this IEnumerable<MidiEvent> events)
		{
			ThrowIfArgument.IsNull("events", events);
			EventsCollection eventsCollection = new EventsCollection();
			eventsCollection.AddRange(events);
			return eventsCollection.ManageTimedEvents(null).Events.ToList<TimedEvent>();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001754F File Offset: 0x0001574F
		public static IEnumerable<TimedEvent> GetTimedEvents(this EventsCollection eventsCollection)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return eventsCollection.ManageTimedEvents(null).Events.ToList<TimedEvent>();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001756D File Offset: 0x0001576D
		public static IEnumerable<TimedEvent> GetTimedEvents(this TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.GetTimedEvents();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00017588 File Offset: 0x00015788
		public static IEnumerable<TimedEvent> GetTimedEvents(this IEnumerable<TrackChunk> trackChunks)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			return (from e in trackChunks.Where((TrackChunk c) => c != null).SelectMany(new Func<TrackChunk, IEnumerable<TimedEvent>>(TimedEventsManagingUtilities.GetTimedEvents))
				orderby e.Time
				select e).ToList<TimedEvent>();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000175FF File Offset: 0x000157FF
		public static IEnumerable<TimedEvent> GetTimedEvents(this MidiFile file)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.GetTrackChunks().GetTimedEvents();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00017617 File Offset: 0x00015817
		public static void AddEvent(this TimedEventsCollection eventsCollection, MidiEvent midiEvent, long time)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			ThrowIfTimeArgument.IsNegative("time", time);
			eventsCollection.Add(new TimedEvent[]
			{
				new TimedEvent(midiEvent, time)
			});
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00017650 File Offset: 0x00015850
		public static void AddEvent(this TimedEventsCollection eventsCollection, MidiEvent midiEvent, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			eventsCollection.AddEvent(midiEvent, TimeConverter.ConvertFrom(time, tempoMap));
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001768C File Offset: 0x0001588C
		public static void ProcessTimedEvents(this EventsCollection eventsCollection, Action<TimedEvent> action, Predicate<TimedEvent> match = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("action", action);
			using (TimedEventsManager timedEventsManager = eventsCollection.ManageTimedEvents(null))
			{
				IEnumerable<TimedEvent> events = timedEventsManager.Events;
				Func<TimedEvent, bool> <>9__0;
				Func<TimedEvent, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = delegate(TimedEvent e)
					{
						Predicate<TimedEvent> match2 = match;
						return match2 == null || match2(e);
					});
				}
				foreach (TimedEvent timedEvent in events.Where(func))
				{
					action(timedEvent);
				}
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00017740 File Offset: 0x00015940
		public static void ProcessTimedEvents(this TrackChunk trackChunk, Action<TimedEvent> action, Predicate<TimedEvent> match = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("action", action);
			trackChunk.Events.ProcessTimedEvents(action, match);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00017768 File Offset: 0x00015968
		public static void ProcessTimedEvents(this IEnumerable<TrackChunk> trackChunks, Action<TimedEvent> action, Predicate<TimedEvent> match = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("action", action);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				if (trackChunk != null)
				{
					trackChunk.ProcessTimedEvents(action, match);
				}
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000177CC File Offset: 0x000159CC
		public static void ProcessTimedEvents(this MidiFile file, Action<TimedEvent> action, Predicate<TimedEvent> match = null)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("action", action);
			file.GetTrackChunks().ProcessTimedEvents(action, match);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000177F4 File Offset: 0x000159F4
		public static void RemoveTimedEvents(this EventsCollection eventsCollection, Predicate<TimedEvent> match = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			using (TimedEventsManager timedEventsManager = eventsCollection.ManageTimedEvents(null))
			{
				TimedObjectsCollection<TimedEvent> events = timedEventsManager.Events;
				Predicate<TimedEvent> predicate = match;
				if (match == null && (predicate = TimedEventsManagingUtilities.<>c.<>9__14_0) == null)
				{
					predicate = (TimedEventsManagingUtilities.<>c.<>9__14_0 = (TimedEvent e) => true);
				}
				events.RemoveAll(predicate);
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00017860 File Offset: 0x00015A60
		public static void RemoveTimedEvents(this TrackChunk trackChunk, Predicate<TimedEvent> match = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			trackChunk.Events.RemoveTimedEvents(match);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001787C File Offset: 0x00015A7C
		public static void RemoveTimedEvents(this IEnumerable<TrackChunk> trackChunks, Predicate<TimedEvent> match = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				if (trackChunk != null)
				{
					trackChunk.RemoveTimedEvents(match);
				}
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000178D4 File Offset: 0x00015AD4
		public static void RemoveTimedEvents(this MidiFile file, Predicate<TimedEvent> match = null)
		{
			ThrowIfArgument.IsNull("file", file);
			file.GetTrackChunks().RemoveTimedEvents(match);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000178F0 File Offset: 0x00015AF0
		public static void AddTimedEvents(this EventsCollection eventsCollection, IEnumerable<TimedEvent> events)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("events", events);
			using (TimedEventsManager timedEventsManager = eventsCollection.ManageTimedEvents(null))
			{
				timedEventsManager.Events.Add(events);
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00017944 File Offset: 0x00015B44
		public static void AddTimedEvents(this TrackChunk trackChunk, IEnumerable<TimedEvent> events)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("events", events);
			trackChunk.Events.AddTimedEvents(events);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00017968 File Offset: 0x00015B68
		public static TrackChunk ToTrackChunk(this IEnumerable<TimedEvent> events)
		{
			ThrowIfArgument.IsNull("events", events);
			TrackChunk trackChunk = new TrackChunk();
			trackChunk.AddTimedEvents(events);
			return trackChunk;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00017981 File Offset: 0x00015B81
		public static MidiFile ToFile(this IEnumerable<TimedEvent> events)
		{
			ThrowIfArgument.IsNull("events", events);
			return new MidiFile(new MidiChunk[] { events.ToTrackChunk() });
		}
	}
}
