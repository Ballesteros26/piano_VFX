using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x02000091 RID: 145
	public sealed class ChordsManager : IDisposable
	{
		// Token: 0x060002FB RID: 763 RVA: 0x000105E4 File Offset: 0x0000E7E4
		public ChordsManager(EventsCollection eventsCollection, long notesTolerance = 0L, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			this._notesManager = eventsCollection.ManageNotes(sameTimeEventsComparison);
			this.Chords = new ChordsCollection(ChordsManager.CreateChords(this._notesManager.Notes, notesTolerance));
			this.Chords.CollectionChanged += this.OnChordsCollectionChanged;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0001064D File Offset: 0x0000E84D
		public ChordsCollection Chords { get; }

		// Token: 0x060002FD RID: 765 RVA: 0x00010655 File Offset: 0x0000E855
		public void SaveChanges()
		{
			this._notesManager.SaveChanges();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00010664 File Offset: 0x0000E864
		private void OnChordsCollectionChanged(ChordsCollection collection, ChordsCollectionChangedEventArgs args)
		{
			IEnumerable<Chord> addedChords = args.AddedChords;
			if (addedChords != null)
			{
				foreach (Chord chord in addedChords)
				{
					this.AddNotes(chord.Notes);
					this.SubscribeToChordEvents(chord);
				}
			}
			IEnumerable<Chord> removedChords = args.RemovedChords;
			if (removedChords != null)
			{
				foreach (Chord chord2 in removedChords)
				{
					this.RemoveNotes(chord2.Notes);
					this.UnsubscribeFromChordEvents(chord2);
				}
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00010714 File Offset: 0x0000E914
		private void OnChordNotesCollectionChanged(NotesCollection collection, NotesCollectionChangedEventArgs args)
		{
			IEnumerable<Note> addedNotes = args.AddedNotes;
			if (addedNotes != null)
			{
				this.AddNotes(addedNotes);
			}
			IEnumerable<Note> removedNotes = args.RemovedNotes;
			if (removedNotes != null)
			{
				this.RemoveNotes(removedNotes);
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00010743 File Offset: 0x0000E943
		private void SubscribeToChordEvents(Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			chord.NotesCollectionChanged += this.OnChordNotesCollectionChanged;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00010762 File Offset: 0x0000E962
		private void UnsubscribeFromChordEvents(Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			chord.NotesCollectionChanged -= this.OnChordNotesCollectionChanged;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00010781 File Offset: 0x0000E981
		private void AddNotes(IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			this._notesManager.Notes.Add(notes);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001079F File Offset: 0x0000E99F
		private void RemoveNotes(IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			this._notesManager.Notes.Remove(notes);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000107BD File Offset: 0x0000E9BD
		internal static IEnumerable<Chord> CreateChords(IEnumerable<Note> notes, long notesTolerance)
		{
			ThrowIfArgument.IsNull("notes", notes);
			int num = FourBitNumber.Values.Length;
			long[] lastNoteEndTimes = (from i in Enumerable.Range(0, num)
				select long.MinValue).ToArray<long>();
			Chord[] chords = new Chord[num];
			foreach (Note note in notes)
			{
				FourBitNumber channel = note.Channel;
				long num2 = lastNoteEndTimes[(int)channel];
				Chord chord = chords[(int)channel];
				long noteTime = note.Time;
				if (noteTime >= num2 || noteTime - chord.Time > notesTolerance)
				{
					if (chord != null)
					{
						yield return chord;
					}
					chord = (chords[(int)channel] = new Chord());
				}
				chord.Notes.Add(new Note[] { note });
				lastNoteEndTimes[(int)channel] = noteTime + note.Length;
				note = null;
			}
			IEnumerator<Note> enumerator = null;
			foreach (Chord chord2 in chords.Where((Chord c) => c != null && c.Notes.Any<Note>()))
			{
				yield return chord2;
			}
			IEnumerator<Chord> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000107D4 File Offset: 0x0000E9D4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000107E0 File Offset: 0x0000E9E0
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				foreach (Chord chord in this.Chords)
				{
					this.UnsubscribeFromChordEvents(chord);
				}
				this.Chords.CollectionChanged -= this.OnChordsCollectionChanged;
				this.SaveChanges();
			}
			this._disposed = true;
		}

		// Token: 0x04000667 RID: 1639
		private readonly NotesManager _notesManager;

		// Token: 0x04000668 RID: 1640
		private bool _disposed;
	}
}
