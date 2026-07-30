using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000A0 RID: 160
	public static class NotesManagingUtilities
	{
		// Token: 0x0600036E RID: 878 RVA: 0x000117F8 File Offset: 0x0000F9F8
		public static Note SetTimeAndLength(this Note note, ITimeSpan time, ITimeSpan length, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("note", note);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			note.Time = TimeConverter.ConvertFrom(time, tempoMap);
			note.Length = LengthConverter.ConvertFrom(length, note.Time, tempoMap);
			return note;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00011852 File Offset: 0x0000FA52
		public static NotesManager ManageNotes(this EventsCollection eventsCollection, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return new NotesManager(eventsCollection, sameTimeEventsComparison);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011866 File Offset: 0x0000FA66
		public static NotesManager ManageNotes(this TrackChunk trackChunk, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.ManageNotes(sameTimeEventsComparison);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001187F File Offset: 0x0000FA7F
		public static IEnumerable<Note> GetNotes(this IEnumerable<MidiEvent> events)
		{
			ThrowIfArgument.IsNull("events", events);
			EventsCollection eventsCollection = new EventsCollection();
			eventsCollection.AddRange(events);
			return eventsCollection.ManageNotes(null).Notes.ToList<Note>();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000118A8 File Offset: 0x0000FAA8
		public static IEnumerable<Note> GetNotes(this EventsCollection eventsCollection)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return eventsCollection.ManageNotes(null).Notes.ToList<Note>();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000118C6 File Offset: 0x0000FAC6
		public static IEnumerable<Note> GetNotes(this TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.GetNotes();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000118E0 File Offset: 0x0000FAE0
		public static IEnumerable<Note> GetNotes(this IEnumerable<TrackChunk> trackChunks)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			return (from n in trackChunks.Where((TrackChunk c) => c != null).SelectMany(new Func<TrackChunk, IEnumerable<Note>>(NotesManagingUtilities.GetNotes))
				orderby n.Time
				select n).ToList<Note>();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00011957 File Offset: 0x0000FB57
		public static IEnumerable<Note> GetNotes(this MidiFile file)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.GetTrackChunks().GetNotes();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011970 File Offset: 0x0000FB70
		public static void ProcessNotes(this EventsCollection eventsCollection, Action<Note> action, Predicate<Note> match = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("action", action);
			using (NotesManager notesManager = eventsCollection.ManageNotes(null))
			{
				IEnumerable<Note> notes = notesManager.Notes;
				Func<Note, bool> <>9__0;
				Func<Note, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = delegate(Note n)
					{
						Predicate<Note> match2 = match;
						return match2 == null || match2(n);
					});
				}
				foreach (Note note in notes.Where(func))
				{
					action(note);
				}
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00011A24 File Offset: 0x0000FC24
		public static void ProcessNotes(this TrackChunk trackChunk, Action<Note> action, Predicate<Note> match = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("action", action);
			trackChunk.Events.ProcessNotes(action, match);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00011A4C File Offset: 0x0000FC4C
		public static void ProcessNotes(this IEnumerable<TrackChunk> trackChunks, Action<Note> action, Predicate<Note> match = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("action", action);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				if (trackChunk != null)
				{
					trackChunk.ProcessNotes(action, match);
				}
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		public static void ProcessNotes(this MidiFile file, Action<Note> action, Predicate<Note> match = null)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("action", action);
			file.GetTrackChunks().ProcessNotes(action, match);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		public static void RemoveNotes(this EventsCollection eventsCollection, Predicate<Note> match = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			using (NotesManager notesManager = eventsCollection.ManageNotes(null))
			{
				TimedObjectsCollection<Note> notes = notesManager.Notes;
				Predicate<Note> predicate = match;
				if (match == null && (predicate = NotesManagingUtilities.<>c.<>9__12_0) == null)
				{
					predicate = (NotesManagingUtilities.<>c.<>9__12_0 = (Note n) => true);
				}
				notes.RemoveAll(predicate);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00011B44 File Offset: 0x0000FD44
		public static void RemoveNotes(this TrackChunk trackChunk, Predicate<Note> match = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			trackChunk.Events.RemoveNotes(match);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00011B60 File Offset: 0x0000FD60
		public static void RemoveNotes(this IEnumerable<TrackChunk> trackChunks, Predicate<Note> match = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				if (trackChunk != null)
				{
					trackChunk.RemoveNotes(match);
				}
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011BB8 File Offset: 0x0000FDB8
		public static void RemoveNotes(this MidiFile file, Predicate<Note> match = null)
		{
			ThrowIfArgument.IsNull("file", file);
			file.GetTrackChunks().RemoveNotes(match);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		public static void AddNotes(this EventsCollection eventsCollection, IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("notes", notes);
			using (NotesManager notesManager = eventsCollection.ManageNotes(null))
			{
				notesManager.Notes.Add(notes);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00011C28 File Offset: 0x0000FE28
		public static void AddNotes(this TrackChunk trackChunk, IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("notes", notes);
			trackChunk.Events.AddNotes(notes);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011C4C File Offset: 0x0000FE4C
		public static TrackChunk ToTrackChunk(this IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			TrackChunk trackChunk = new TrackChunk();
			trackChunk.AddNotes(notes);
			return trackChunk;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011C65 File Offset: 0x0000FE65
		public static MidiFile ToFile(this IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			return new MidiFile(new MidiChunk[] { notes.ToTrackChunk() });
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011C86 File Offset: 0x0000FE86
		public static Note GetMusicTheoryNote(this Note note)
		{
			ThrowIfArgument.IsNull("note", note);
			return note.UnderlyingNote;
		}
	}
}
