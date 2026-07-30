using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000038 RID: 56
	internal sealed class MidiFileSlicer : IDisposable
	{
		// Token: 0x06000163 RID: 355 RVA: 0x000084C0 File Offset: 0x000066C0
		private MidiFileSlicer(TimeDivision timeDivision, IEnumerator<TimedEvent>[] timedEventsEnumerators)
		{
			this._timedEventsHolders = timedEventsEnumerators.Select((IEnumerator<TimedEvent> e) => new MidiFileSlicer.TimedEventsHolder(e)).ToArray<MidiFileSlicer.TimedEventsHolder>();
			this._timeDivision = timeDivision;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000084FF File Offset: 0x000066FF
		public bool AllEventsProcessed
		{
			get
			{
				return this._timedEventsHolders.All((MidiFileSlicer.TimedEventsHolder c) => !c.EventsToStartNextPart.Any<TimedEvent>() && c.Enumerator.Current == null);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000852C File Offset: 0x0000672C
		public MidiFile GetNextSlice(long endTime, SliceMidiFileSettings settings)
		{
			return new MidiFile((from e in this.GetNextTimedEvents(endTime, settings.PreserveTimes)
				select e.ToTrackChunk() into c
				where settings.PreserveTrackChunks || c.Events.Any<MidiEvent>()
				select c).ToList<TrackChunk>())
			{
				TimeDivision = this._timeDivision.Clone()
			};
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000085A8 File Offset: 0x000067A8
		private IEnumerable<IEnumerable<TimedEvent>> GetNextTimedEvents(long endTime, bool preserveTimes)
		{
			int num2;
			for (int i = 0; i < this._timedEventsHolders.Length; i = num2 + 1)
			{
				MidiFileSlicer.TimedEventsHolder timedEventsHolder = this._timedEventsHolders[i];
				IEnumerator<TimedEvent> enumerator = timedEventsHolder.Enumerator;
				Dictionary<Type, TimedEvent> eventsToCopyToNextPart = timedEventsHolder.EventsToCopyToNextPart;
				List<TimedEvent> eventsToStartNextPart = timedEventsHolder.EventsToStartNextPart;
				List<NoteId> list = new List<NoteId>();
				int num;
				List<TimedEvent> list2 = MidiFileSlicer.PrepareTakenTimedEvents(eventsToCopyToNextPart, list, preserveTimes, eventsToStartNextPart, out num);
				do
				{
					TimedEvent timedEvent = enumerator.Current;
					if (timedEvent == null)
					{
						break;
					}
					long time = timedEvent.Time;
					if (time > endTime)
					{
						break;
					}
					if (time == endTime)
					{
						MidiFileSlicer.TryToMoveEdgeNoteOffsToPreviousPart(timedEvent, list, list2, eventsToStartNextPart);
					}
					else
					{
						MidiFileSlicer.TryToUpdateNotesInformation(timedEvent.Event, list);
						MidiFileSlicer.UpdateEventsToCopyToNextPart(eventsToCopyToNextPart, timedEvent);
						list2.Add(timedEvent);
					}
				}
				while (enumerator.MoveNext());
				if (!preserveTimes)
				{
					MidiFileSlicer.MoveEventsToStart(list2, num, this._lastTime);
				}
				yield return list2;
				num2 = i;
			}
			this._lastTime = endTime;
			yield break;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000085C8 File Offset: 0x000067C8
		public static MidiFileSlicer CreateFromFile(MidiFile midiFile)
		{
			IEnumerator<TimedEvent>[] array = (from c in midiFile.GetTrackChunks()
				select c.GetTimedEvents().GetEnumerator()).ToArray<IEnumerator<TimedEvent>>();
			return new MidiFileSlicer(midiFile.TimeDivision, array);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008614 File Offset: 0x00006814
		private static void TryToUpdateNotesInformation(MidiEvent midiEvent, List<NoteId> noteOnIds)
		{
			NoteOnEvent noteOnEvent = midiEvent as NoteOnEvent;
			if (noteOnEvent != null)
			{
				noteOnIds.Add(noteOnEvent.GetNoteId());
				return;
			}
			NoteOffEvent noteOffEvent = midiEvent as NoteOffEvent;
			if (noteOffEvent != null)
			{
				noteOnIds.Remove(noteOffEvent.GetNoteId());
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008650 File Offset: 0x00006850
		private static void TryToMoveEdgeNoteOffsToPreviousPart(TimedEvent timedEvent, List<NoteId> noteOnIds, List<TimedEvent> takenTimedEvents, List<TimedEvent> eventsToStartNextPart)
		{
			NoteOffEvent noteOffEvent = timedEvent.Event as NoteOffEvent;
			if (noteOffEvent != null && noteOnIds.Remove(noteOffEvent.GetNoteId()))
			{
				takenTimedEvents.Add(timedEvent);
				return;
			}
			eventsToStartNextPart.Add(timedEvent);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000868C File Offset: 0x0000688C
		private static void MoveEventsToStart(List<TimedEvent> takenTimedEvents, int startIndex, long partStartTime)
		{
			for (int i = startIndex; i < takenTimedEvents.Count; i++)
			{
				takenTimedEvents[i].Time -= partStartTime;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000086C0 File Offset: 0x000068C0
		private static List<TimedEvent> PrepareTakenTimedEvents(Dictionary<Type, TimedEvent> eventsToCopyToNextPart, List<NoteId> noteOnIds, bool preserveTimes, List<TimedEvent> eventsToStartNextPart, out int newEventsStartIndex)
		{
			List<TimedEvent> list = new List<TimedEvent>(eventsToCopyToNextPart.Values);
			if (!preserveTimes)
			{
				list.ForEach(delegate(TimedEvent e)
				{
					e.Time = 0L;
				});
			}
			newEventsStartIndex = list.Count;
			list.AddRange(eventsToStartNextPart);
			eventsToStartNextPart.Clear();
			foreach (TimedEvent timedEvent in list)
			{
				MidiFileSlicer.TryToUpdateNotesInformation(timedEvent.Event, noteOnIds);
				MidiFileSlicer.UpdateEventsToCopyToNextPart(eventsToCopyToNextPart, timedEvent);
			}
			return list;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008768 File Offset: 0x00006968
		private static void UpdateEventsToCopyToNextPart(Dictionary<Type, TimedEvent> eventsToCopyToNextPart, TimedEvent timedEvent)
		{
			Type type = timedEvent.Event.GetType();
			if (MidiFileSlicer.EventsTypesToCopyToNextPart.Contains(type))
			{
				eventsToCopyToNextPart[type] = timedEvent.Clone();
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000879C File Offset: 0x0000699C
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				MidiFileSlicer.TimedEventsHolder[] timedEventsHolders = this._timedEventsHolders;
				for (int i = 0; i < timedEventsHolders.Length; i++)
				{
					timedEventsHolders[i].Dispose();
				}
			}
			this._disposed = true;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000087D9 File Offset: 0x000069D9
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x040000C0 RID: 192
		private static readonly Type[] EventsTypesToCopyToNextPart = new Type[]
		{
			typeof(ChannelAftertouchEvent),
			typeof(ControlChangeEvent),
			typeof(NoteAftertouchEvent),
			typeof(PitchBendEvent),
			typeof(ProgramChangeEvent),
			typeof(ChannelPrefixEvent),
			typeof(CopyrightNoticeEvent),
			typeof(DeviceNameEvent),
			typeof(InstrumentNameEvent),
			typeof(KeySignatureEvent),
			typeof(PortPrefixEvent),
			typeof(ProgramNameEvent),
			typeof(SequenceNumberEvent),
			typeof(SequenceTrackNameEvent),
			typeof(SetTempoEvent),
			typeof(SmpteOffsetEvent),
			typeof(TimeSignatureEvent)
		};

		// Token: 0x040000C1 RID: 193
		private readonly MidiFileSlicer.TimedEventsHolder[] _timedEventsHolders;

		// Token: 0x040000C2 RID: 194
		private readonly TimeDivision _timeDivision;

		// Token: 0x040000C3 RID: 195
		private long _lastTime;

		// Token: 0x040000C4 RID: 196
		private bool _disposed;

		// Token: 0x02000205 RID: 517
		private sealed class TimedEventsHolder : IDisposable
		{
			// Token: 0x06000CA0 RID: 3232 RVA: 0x000275A5 File Offset: 0x000257A5
			public TimedEventsHolder(IEnumerator<TimedEvent> timedEventsEumerator)
			{
				this.Enumerator = timedEventsEumerator;
				this.Enumerator.MoveNext();
			}

			// Token: 0x170001CB RID: 459
			// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000275D6 File Offset: 0x000257D6
			public IEnumerator<TimedEvent> Enumerator { get; }

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000275DE File Offset: 0x000257DE
			public Dictionary<Type, TimedEvent> EventsToCopyToNextPart { get; } = new Dictionary<Type, TimedEvent>();

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000275E6 File Offset: 0x000257E6
			public List<TimedEvent> EventsToStartNextPart { get; } = new List<TimedEvent>();

			// Token: 0x06000CA4 RID: 3236 RVA: 0x000275EE File Offset: 0x000257EE
			private void Dispose(bool disposing)
			{
				if (this._disposed)
				{
					return;
				}
				if (disposing)
				{
					this.Enumerator.Dispose();
				}
				this._disposed = true;
			}

			// Token: 0x06000CA5 RID: 3237 RVA: 0x0002760E File Offset: 0x0002580E
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x04000BD2 RID: 3026
			private bool _disposed;
		}
	}
}
