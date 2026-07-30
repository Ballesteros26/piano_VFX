using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x02000092 RID: 146
	public static class ChordsManagingUtilities
	{
		// Token: 0x06000307 RID: 775 RVA: 0x00010860 File Offset: 0x0000EA60
		public static Chord SetTimeAndLength(this Chord chord, ITimeSpan time, ITimeSpan length, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			chord.Time = TimeConverter.ConvertFrom(time, tempoMap);
			chord.Length = LengthConverter.ConvertFrom(length, chord.Time, tempoMap);
			return chord;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000108BA File Offset: 0x0000EABA
		public static ChordsManager ManageChords(this EventsCollection eventsCollection, long notesTolerance = 0L, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			return new ChordsManager(eventsCollection, notesTolerance, sameTimeEventsComparison);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000108DA File Offset: 0x0000EADA
		public static ChordsManager ManageChords(this TrackChunk trackChunk, long notesTolerance = 0L, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			return trackChunk.Events.ManageChords(notesTolerance, sameTimeEventsComparison);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000108FF File Offset: 0x0000EAFF
		public static IEnumerable<Chord> GetChords(this IEnumerable<MidiEvent> events, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("events", events);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			EventsCollection eventsCollection = new EventsCollection();
			eventsCollection.AddRange(events);
			return eventsCollection.ManageChords(notesTolerance, null).Chords.ToList<Chord>();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00010934 File Offset: 0x0000EB34
		public static IEnumerable<Chord> GetChords(this EventsCollection eventsCollection, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			return eventsCollection.ManageChords(notesTolerance, null).Chords.ToList<Chord>();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001095E File Offset: 0x0000EB5E
		public static IEnumerable<Chord> GetChords(this TrackChunk trackChunk, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			return trackChunk.Events.GetChords(notesTolerance);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00010984 File Offset: 0x0000EB84
		public static IEnumerable<Chord> GetChords(this IEnumerable<TrackChunk> trackChunks, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			return (from c in trackChunks.Where((TrackChunk c) => c != null).SelectMany((TrackChunk c) => c.GetChords(notesTolerance))
				orderby c.Time
				select c).ToList<Chord>();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00010A18 File Offset: 0x0000EC18
		public static IEnumerable<Chord> GetChords(this MidiFile file, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			return file.GetTrackChunks().GetChords(notesTolerance);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public static IEnumerable<Chord> GetChords(this IEnumerable<Note> notes, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			return (from c in ChordsManager.CreateChords(notes, notesTolerance)
				orderby c.Time
				select c).ToList<Chord>();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00010A90 File Offset: 0x0000EC90
		public static void ProcessChords(this EventsCollection eventsCollection, Action<Chord> action, Predicate<Chord> match = null, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			using (ChordsManager chordsManager = eventsCollection.ManageChords(notesTolerance, null))
			{
				IEnumerable<Chord> chords = chordsManager.Chords;
				Func<Chord, bool> <>9__0;
				Func<Chord, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = delegate(Chord c)
					{
						Predicate<Chord> match2 = match;
						return match2 == null || match2(c);
					});
				}
				foreach (Chord chord in chords.Where(func))
				{
					action(chord);
				}
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00010B50 File Offset: 0x0000ED50
		public static void ProcessChords(this TrackChunk trackChunk, Action<Chord> action, Predicate<Chord> match = null, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			trackChunk.Events.ProcessChords(action, match, notesTolerance);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00010B84 File Offset: 0x0000ED84
		public static void ProcessChords(this IEnumerable<TrackChunk> trackChunks, Action<Chord> action, Predicate<Chord> match = null, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				if (trackChunk != null)
				{
					trackChunk.ProcessChords(action, match, notesTolerance);
				}
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		public static void ProcessChords(this MidiFile file, Action<Chord> action, Predicate<Chord> match = null, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			file.GetTrackChunks().ProcessChords(action, match, notesTolerance);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00010C28 File Offset: 0x0000EE28
		public static void RemoveChords(this EventsCollection eventsCollection, Predicate<Chord> match = null, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			using (ChordsManager chordsManager = eventsCollection.ManageChords(notesTolerance, null))
			{
				TimedObjectsCollection<Chord> chords = chordsManager.Chords;
				Predicate<Chord> predicate = match;
				if (match == null && (predicate = ChordsManagingUtilities.<>c.<>9__13_0) == null)
				{
					predicate = (ChordsManagingUtilities.<>c.<>9__13_0 = (Chord c) => true);
				}
				chords.RemoveAll(predicate);
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		public static void RemoveChords(this TrackChunk trackChunk, Predicate<Chord> match = null, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			trackChunk.Events.RemoveChords(match, notesTolerance);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		public static void RemoveChords(this IEnumerable<TrackChunk> trackChunks, Predicate<Chord> match = null, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				if (trackChunk != null)
				{
					trackChunk.RemoveChords(match, notesTolerance);
				}
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00010D2C File Offset: 0x0000EF2C
		public static void RemoveChords(this MidiFile file, Predicate<Chord> match = null, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			file.GetTrackChunks().RemoveChords(match, notesTolerance);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00010D54 File Offset: 0x0000EF54
		public static void AddChords(this EventsCollection eventsCollection, IEnumerable<Chord> chords)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("chords", chords);
			using (ChordsManager chordsManager = eventsCollection.ManageChords(0L, null))
			{
				chordsManager.Chords.Add(chords);
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00010DAC File Offset: 0x0000EFAC
		public static void AddChords(this TrackChunk trackChunk, IEnumerable<Chord> chords)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("chords", chords);
			trackChunk.Events.AddChords(chords);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00010DD0 File Offset: 0x0000EFD0
		public static TrackChunk ToTrackChunk(this IEnumerable<Chord> chords)
		{
			ThrowIfArgument.IsNull("chords", chords);
			TrackChunk trackChunk = new TrackChunk();
			trackChunk.AddChords(chords);
			return trackChunk;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00010DE9 File Offset: 0x0000EFE9
		public static MidiFile ToFile(this IEnumerable<Chord> chords)
		{
			ThrowIfArgument.IsNull("chords", chords);
			return new MidiFile(new MidiChunk[] { chords.ToTrackChunk() });
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00010E0C File Offset: 0x0000F00C
		public static Chord GetMusicTheoryChord(this Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return new Chord((from n in chord.Notes
				orderby n.NoteNumber
				select n.NoteName).ToArray<NoteName>());
		}
	}
}
