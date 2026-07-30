using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000053 RID: 83
	public static class NotesQuantizerUtilities
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000981C File Offset: 0x00007A1C
		public static void QuantizeNotes(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap, NotesQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using (NotesManager notesManager = trackChunk.ManageNotes(null))
			{
				new NotesQuantizer().Quantize(notesManager.Notes, grid, tempoMap, settings);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009884 File Offset: 0x00007A84
		public static void QuantizeNotes(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap, NotesQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.QuantizeNotes(grid, tempoMap, settings);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000098F0 File Offset: 0x00007AF0
		public static void QuantizeNotes(this MidiFile midiFile, IGrid grid, NotesQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().QuantizeNotes(grid, tempoMap, settings);
		}
	}
}
