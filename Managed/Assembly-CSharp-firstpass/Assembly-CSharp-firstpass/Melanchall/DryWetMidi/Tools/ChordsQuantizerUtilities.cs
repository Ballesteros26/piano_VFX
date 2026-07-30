using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000052 RID: 82
	public static class ChordsQuantizerUtilities
	{
		// Token: 0x060001CD RID: 461 RVA: 0x000096EC File Offset: 0x000078EC
		public static void QuantizeChords(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap, long notesTolerance = 0L, ChordsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			using (ChordsManager chordsManager = trackChunk.ManageChords(notesTolerance, null))
			{
				new ChordsQuantizer().Quantize(chordsManager.Chords, grid, tempoMap, settings);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009760 File Offset: 0x00007960
		public static void QuantizeChords(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap, long notesTolerance = 0L, ChordsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.QuantizeChords(grid, tempoMap, notesTolerance, settings);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000097D8 File Offset: 0x000079D8
		public static void QuantizeChords(this MidiFile midiFile, IGrid grid, long notesTolerance = 0L, ChordsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().QuantizeChords(grid, tempoMap, notesTolerance, settings);
		}
	}
}
