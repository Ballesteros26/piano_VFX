using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000A2 RID: 162
	public static class GetNotesAndRestsUtilities
	{
		// Token: 0x06000389 RID: 905 RVA: 0x00011E68 File Offset: 0x00010068
		public static IEnumerable<ILengthedObject> GetNotesAndRests(this IEnumerable<Note> notes, RestSeparationPolicy restSeparationPolicy)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsInvalidEnumValue<RestSeparationPolicy>("restSeparationPolicy", restSeparationPolicy);
			switch (restSeparationPolicy)
			{
			case RestSeparationPolicy.NoSeparation:
				return GetNotesAndRestsUtilities.GetNotesAndRests<object>(notes, (Note n) => GetNotesAndRestsUtilities.NoSeparationNoteDescriptor, false, false);
			case RestSeparationPolicy.SeparateByChannel:
				return GetNotesAndRestsUtilities.GetNotesAndRests<FourBitNumber>(notes, (Note n) => n.Channel, true, false);
			case RestSeparationPolicy.SeparateByNoteNumber:
				return GetNotesAndRestsUtilities.GetNotesAndRests<SevenBitNumber>(notes, (Note n) => n.NoteNumber, false, true);
			case RestSeparationPolicy.SeparateByChannelAndNoteNumber:
				return GetNotesAndRestsUtilities.GetNotesAndRests<NoteId>(notes, (Note n) => n.GetNoteId(), true, true);
			default:
				throw new NotSupportedException(string.Format("Rest separation policy {0} is not supported.", restSeparationPolicy));
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00011F5B File Offset: 0x0001015B
		public static IEnumerable<ILengthedObject> GetNotesAndRests(this TrackChunk trackChunk, RestSeparationPolicy restSeparationPolicy)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsInvalidEnumValue<RestSeparationPolicy>("restSeparationPolicy", restSeparationPolicy);
			return trackChunk.GetNotes().GetNotesAndRests(restSeparationPolicy);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00011F7F File Offset: 0x0001017F
		public static IEnumerable<ILengthedObject> GetNotesAndRests(this IEnumerable<TrackChunk> trackChunks, RestSeparationPolicy restSeparationPolicy)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsInvalidEnumValue<RestSeparationPolicy>("restSeparationPolicy", restSeparationPolicy);
			return trackChunks.GetNotes().GetNotesAndRests(restSeparationPolicy);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011FA3 File Offset: 0x000101A3
		public static IEnumerable<ILengthedObject> GetNotesAndRests(this MidiFile midiFile, RestSeparationPolicy restSeparationPolicy)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsInvalidEnumValue<RestSeparationPolicy>("restSeparationPolicy", restSeparationPolicy);
			return midiFile.GetNotes().GetNotesAndRests(restSeparationPolicy);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011FC7 File Offset: 0x000101C7
		private static IEnumerable<ILengthedObject> GetNotesAndRests<TDescriptor>(IEnumerable<Note> notes, Func<Note, TDescriptor> noteDescriptorGetter, bool setRestChannel, bool setRestNoteNumber)
		{
			Dictionary<TDescriptor, long> lastEndTimes = new Dictionary<TDescriptor, long>();
			foreach (Note note in from n in notes
				where n != null
				orderby n.Time
				select n)
			{
				TDescriptor noteDescriptor = noteDescriptorGetter(note);
				long lastEndTime;
				lastEndTimes.TryGetValue(noteDescriptor, out lastEndTime);
				if (note.Time > lastEndTime)
				{
					yield return new Rest(lastEndTime, note.Time - lastEndTime, setRestChannel ? new FourBitNumber?(note.Channel) : null, setRestNoteNumber ? new SevenBitNumber?(note.NoteNumber) : null);
				}
				yield return note.Clone();
				lastEndTimes[noteDescriptor] = Math.Max(lastEndTime, note.Time + note.Length);
				noteDescriptor = default(TDescriptor);
				note = null;
			}
			IEnumerator<Note> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000685 RID: 1669
		private static readonly object NoSeparationNoteDescriptor = new object();
	}
}
