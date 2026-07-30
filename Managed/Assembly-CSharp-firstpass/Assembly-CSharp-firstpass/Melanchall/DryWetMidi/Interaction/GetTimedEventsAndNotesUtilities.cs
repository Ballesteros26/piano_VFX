using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000A5 RID: 165
	public static class GetTimedEventsAndNotesUtilities
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0001221B File Offset: 0x0001041B
		public static IEnumerable<ITimedObject> GetTimedEventsAndNotes(this IEnumerable<TimedEvent> timedEvents)
		{
			ThrowIfArgument.IsNull("timedEvents", timedEvents);
			List<GetTimedEventsAndNotesUtilities.NoteEventsDescriptor> noteEventsDescriptors = new List<GetTimedEventsAndNotesUtilities.NoteEventsDescriptor>();
			ObjectWrapper<List<TimedEvent>> eventsTail = new ObjectWrapper<List<TimedEvent>>();
			IEnumerator<ITimedObject> enumerator2;
			foreach (TimedEvent timedEvent in timedEvents)
			{
				foreach (ITimedObject timedObject in GetTimedEventsAndNotesUtilities.GetTimedEventsAndNotes(timedEvent, noteEventsDescriptors, eventsTail))
				{
					yield return timedObject;
				}
				enumerator2 = null;
			}
			IEnumerator<TimedEvent> enumerator = null;
			foreach (ITimedObject timedObject2 in noteEventsDescriptors.SelectMany((GetTimedEventsAndNotesUtilities.NoteEventsDescriptor d) => d.GetTimedObjects()))
			{
				yield return timedObject2;
			}
			enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001222B File Offset: 0x0001042B
		public static IEnumerable<ITimedObject> GetTimedEventsAndNotes(this TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.GetTimedEvents().GetTimedEventsAndNotes();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00012243 File Offset: 0x00010443
		public static IEnumerable<ITimedObject> GetTimedEventsAndNotes(this IEnumerable<TrackChunk> trackChunks)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			return trackChunks.GetTimedEvents().GetTimedEventsAndNotes();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001225B File Offset: 0x0001045B
		public static IEnumerable<ITimedObject> GetTimedEventsAndNotes(this MidiFile midiFile)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return midiFile.GetTimedEvents().GetTimedEventsAndNotes();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00012273 File Offset: 0x00010473
		internal static IEnumerable<ITimedObject> GetTimedEventsAndNotes(TimedEvent timedEvent, List<GetTimedEventsAndNotesUtilities.NoteEventsDescriptor> noteEventsDescriptors, ObjectWrapper<List<TimedEvent>> eventsTail)
		{
			MidiEvent midiEvent = ((timedEvent != null) ? timedEvent.Event : null);
			if (midiEvent is NoteOnEvent)
			{
				noteEventsDescriptors.Add(new GetTimedEventsAndNotesUtilities.NoteEventsDescriptor(timedEvent, eventsTail.Object = new List<TimedEvent>()));
				yield break;
			}
			NoteOffEvent noteOffEvent = midiEvent as NoteOffEvent;
			if (noteOffEvent != null)
			{
				GetTimedEventsAndNotesUtilities.NoteEventsDescriptor noteEventsDescriptor = noteEventsDescriptors.FirstOrDefault((GetTimedEventsAndNotesUtilities.NoteEventsDescriptor d) => d.IsCorrespondingNoteOffEvent(noteOffEvent));
				if (noteEventsDescriptor != null)
				{
					noteEventsDescriptor.CompleteNote(timedEvent);
					if (noteEventsDescriptors.First<GetTimedEventsAndNotesUtilities.NoteEventsDescriptor>() != noteEventsDescriptor)
					{
						yield break;
					}
					int num;
					for (int i = 0; i < noteEventsDescriptors.Count; i = num + 1)
					{
						GetTimedEventsAndNotesUtilities.NoteEventsDescriptor noteEventsDescriptor2 = noteEventsDescriptors[i];
						if (!noteEventsDescriptor2.IsNoteCompleted)
						{
							break;
						}
						foreach (ITimedObject timedObject in noteEventsDescriptor2.GetTimedObjects())
						{
							yield return timedObject;
						}
						IEnumerator<ITimedObject> enumerator = null;
						noteEventsDescriptors.RemoveAt(i);
						num = i;
						i = num - 1;
						num = i;
					}
					if (!noteEventsDescriptors.Any<GetTimedEventsAndNotesUtilities.NoteEventsDescriptor>())
					{
						eventsTail.Object = null;
					}
					yield break;
				}
			}
			if (eventsTail.Object != null)
			{
				eventsTail.Object.Add(timedEvent);
			}
			else
			{
				yield return timedEvent;
			}
			yield break;
			yield break;
		}

		// Token: 0x02000244 RID: 580
		internal sealed class NoteEventsDescriptor
		{
			// Token: 0x06000DC8 RID: 3528 RVA: 0x00029B33 File Offset: 0x00027D33
			public NoteEventsDescriptor(TimedEvent noteOnTimedEvent, IEnumerable<TimedEvent> eventsTail)
			{
				this.NoteOnTimedEvent = noteOnTimedEvent;
				this.EventsTail = eventsTail;
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00029B49 File Offset: 0x00027D49
			public TimedEvent NoteOnTimedEvent { get; }

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00029B51 File Offset: 0x00027D51
			// (set) Token: 0x06000DCB RID: 3531 RVA: 0x00029B59 File Offset: 0x00027D59
			public TimedEvent NoteOffTimedEvent { get; private set; }

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00029B62 File Offset: 0x00027D62
			public IEnumerable<TimedEvent> EventsTail { get; }

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00029B6A File Offset: 0x00027D6A
			// (set) Token: 0x06000DCE RID: 3534 RVA: 0x00029B72 File Offset: 0x00027D72
			public bool IsNoteCompleted { get; private set; }

			// Token: 0x06000DCF RID: 3535 RVA: 0x00029B7B File Offset: 0x00027D7B
			public void CompleteNote(TimedEvent noteOffTimedEvent)
			{
				this.NoteOffTimedEvent = noteOffTimedEvent;
				this.IsNoteCompleted = true;
			}

			// Token: 0x06000DD0 RID: 3536 RVA: 0x00029B8B File Offset: 0x00027D8B
			public bool IsCorrespondingNoteOffEvent(NoteOffEvent noteOffEvent)
			{
				return NoteEventUtilities.IsNoteOnCorrespondToNoteOff((NoteOnEvent)this.NoteOnTimedEvent.Event, noteOffEvent) && !this.IsNoteCompleted;
			}

			// Token: 0x06000DD1 RID: 3537 RVA: 0x00029BB0 File Offset: 0x00027DB0
			public IEnumerable<ITimedObject> GetTimedObjects()
			{
				if (this.IsNoteCompleted)
				{
					yield return new Note(this.NoteOnTimedEvent, this.NoteOffTimedEvent);
				}
				else
				{
					yield return this.NoteOnTimedEvent;
				}
				foreach (TimedEvent timedEvent in this.EventsTail)
				{
					yield return timedEvent;
				}
				IEnumerator<TimedEvent> enumerator = null;
				yield break;
				yield break;
			}
		}
	}
}
