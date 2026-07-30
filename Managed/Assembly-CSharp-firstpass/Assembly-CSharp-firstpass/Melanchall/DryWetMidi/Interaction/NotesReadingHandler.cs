using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000A1 RID: 161
	public sealed class NotesReadingHandler : ReadingHandler
	{
		// Token: 0x06000383 RID: 899 RVA: 0x00011C99 File Offset: 0x0000FE99
		public NotesReadingHandler(bool sortNotes)
			: base(ReadingHandler.TargetScope.File | ReadingHandler.TargetScope.Event)
		{
			this._sortNotes = sortNotes;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00011CCC File Offset: 0x0000FECC
		public IEnumerable<Note> Notes
		{
			get
			{
				IEnumerable<Note> enumerable;
				if ((enumerable = this._notesProcessed) == null)
				{
					IEnumerable<Note> enumerable3;
					if (!this._sortNotes)
					{
						IEnumerable<Note> enumerable2 = this._notes;
						enumerable3 = enumerable2;
					}
					else
					{
						IEnumerable<Note> enumerable2 = this._notes.OrderBy((Note e) => e.Time);
						enumerable3 = enumerable2;
					}
					enumerable = (this._notesProcessed = enumerable3.ToList<Note>());
				}
				return enumerable;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00011D2F File Offset: 0x0000FF2F
		public override void Initialize()
		{
			this._noteEventsDescriptors.Clear();
			this._eventsTail.Object = null;
			this._notes.Clear();
			this._notesProcessed = null;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public override void OnFinishFileReading(MidiFile midiFile)
		{
			foreach (ITimedObject timedObject in this._noteEventsDescriptors.SelectMany((GetTimedEventsAndNotesUtilities.NoteEventsDescriptor d) => d.GetTimedObjects()))
			{
				this.AddNote(timedObject);
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		public override void OnFinishEventReading(MidiEvent midiEvent, long absoluteTime)
		{
			if (midiEvent.EventType != MidiEventType.NoteOn && midiEvent.EventType != MidiEventType.NoteOff)
			{
				return;
			}
			foreach (ITimedObject timedObject in GetTimedEventsAndNotesUtilities.GetTimedEventsAndNotes(new TimedEvent(midiEvent, absoluteTime), this._noteEventsDescriptors, this._eventsTail))
			{
				this.AddNote(timedObject);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011E44 File Offset: 0x00010044
		private void AddNote(ITimedObject timedObject)
		{
			Note note = timedObject as Note;
			if (note == null)
			{
				return;
			}
			this._notes.Add(note);
		}

		// Token: 0x04000680 RID: 1664
		private readonly bool _sortNotes;

		// Token: 0x04000681 RID: 1665
		private readonly List<Note> _notes = new List<Note>();

		// Token: 0x04000682 RID: 1666
		private IEnumerable<Note> _notesProcessed;

		// Token: 0x04000683 RID: 1667
		private readonly List<GetTimedEventsAndNotesUtilities.NoteEventsDescriptor> _noteEventsDescriptors = new List<GetTimedEventsAndNotesUtilities.NoteEventsDescriptor>();

		// Token: 0x04000684 RID: 1668
		private readonly ObjectWrapper<List<TimedEvent>> _eventsTail = new ObjectWrapper<List<TimedEvent>>();
	}
}
