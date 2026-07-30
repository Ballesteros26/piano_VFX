using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x0200009F RID: 159
	public sealed class NotesManager : IDisposable
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00011610 File Offset: 0x0000F810
		public NotesManager(EventsCollection eventsCollection, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			this._timedEventsManager = eventsCollection.ManageTimedEvents(sameTimeEventsComparison);
			this.Notes = new NotesCollection(NotesManager.CreateNotes(this._timedEventsManager.Events));
			this.Notes.CollectionChanged += this.OnNotesCollectionChanged;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0001166D File Offset: 0x0000F86D
		public NotesCollection Notes { get; }

		// Token: 0x06000368 RID: 872 RVA: 0x00011678 File Offset: 0x0000F878
		public void SaveChanges()
		{
			foreach (Note note in this.Notes)
			{
				NoteOnEvent noteOnEvent = (NoteOnEvent)note.TimedNoteOnEvent.Event;
				NoteOffEvent noteOffEvent = (NoteOffEvent)note.TimedNoteOffEvent.Event;
				noteOnEvent.Channel = (noteOffEvent.Channel = note.Channel);
				noteOnEvent.NoteNumber = (noteOffEvent.NoteNumber = note.NoteNumber);
				noteOnEvent.Velocity = note.Velocity;
				noteOffEvent.Velocity = note.OffVelocity;
			}
			this._timedEventsManager.SaveChanges();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00011730 File Offset: 0x0000F930
		private void OnNotesCollectionChanged(NotesCollection collection, NotesCollectionChangedEventArgs args)
		{
			IEnumerable<Note> addedNotes = args.AddedNotes;
			if (addedNotes != null)
			{
				this._timedEventsManager.Events.Add(NotesManager.GetNotesTimedEvents(addedNotes));
			}
			IEnumerable<Note> removedNotes = args.RemovedNotes;
			if (removedNotes != null)
			{
				this._timedEventsManager.Events.Remove(NotesManager.GetNotesTimedEvents(removedNotes));
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001177D File Offset: 0x0000F97D
		private static IEnumerable<Note> CreateNotes(IEnumerable<TimedEvent> events)
		{
			return events.GetTimedEventsAndNotes().OfType<Note>();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001178A File Offset: 0x0000F98A
		private static IEnumerable<TimedEvent> GetNotesTimedEvents(IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			return notes.SelectMany((Note n) => new TimedEvent[] { n.TimedNoteOnEvent, n.TimedNoteOffEvent });
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000117BC File Offset: 0x0000F9BC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000117C5 File Offset: 0x0000F9C5
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this.Notes.CollectionChanged -= this.OnNotesCollectionChanged;
				this.SaveChanges();
			}
			this._disposed = true;
		}

		// Token: 0x0400067D RID: 1661
		private readonly TimedEventsManager _timedEventsManager;

		// Token: 0x0400067E RID: 1662
		private bool _disposed;
	}
}
