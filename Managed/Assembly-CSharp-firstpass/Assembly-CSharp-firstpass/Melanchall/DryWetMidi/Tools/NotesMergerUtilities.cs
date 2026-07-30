using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200003C RID: 60
	public static class NotesMergerUtilities
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00008D50 File Offset: 0x00006F50
		public static void MergeNotes(this TrackChunk trackChunk, TempoMap tempoMap, NotesMergingSettings settings = null, Predicate<Note> filter = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using (NotesManager notesManager = trackChunk.ManageNotes(null))
			{
				NotesCollection notes = notesManager.Notes;
				List<Note> list = new NotesMerger().Merge(notes.Where((Note n) => filter == null || filter(n)), tempoMap, settings).ToList<Note>();
				notes.Clear();
				notes.Add(list);
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008DDC File Offset: 0x00006FDC
		public static void MergeNotes(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap, NotesMergingSettings settings = null, Predicate<Note> filter = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks.Where((TrackChunk c) => c != null))
			{
				trackChunk.MergeNotes(tempoMap, settings, filter);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008E60 File Offset: 0x00007060
		public static void MergeNotes(this MidiFile midiFile, NotesMergingSettings settings = null, Predicate<Note> filter = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().MergeNotes(tempoMap, settings, filter);
		}
	}
}
