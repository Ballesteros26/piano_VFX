using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000062 RID: 98
	public static class NotesRandomizerUtilities
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00009F68 File Offset: 0x00008168
		public static void RandomizeNotes(this TrackChunk trackChunk, IBounds bounds, TempoMap tempoMap, NotesRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using (NotesManager notesManager = trackChunk.ManageNotes(null))
			{
				new NotesRandomizer().Randomize(notesManager.Notes, bounds, tempoMap, settings);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009FD0 File Offset: 0x000081D0
		public static void RandomizeNotes(this IEnumerable<TrackChunk> trackChunks, IBounds bounds, TempoMap tempoMap, NotesRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.RandomizeNotes(bounds, tempoMap, settings);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000A03C File Offset: 0x0000823C
		public static void RandomizeNotes(this MidiFile midiFile, IBounds bounds, NotesRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("bounds", bounds);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().RandomizeNotes(bounds, tempoMap, settings);
		}
	}
}
