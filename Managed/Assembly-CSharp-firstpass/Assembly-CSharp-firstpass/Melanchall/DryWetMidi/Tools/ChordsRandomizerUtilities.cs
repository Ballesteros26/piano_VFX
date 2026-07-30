using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000061 RID: 97
	public static class ChordsRandomizerUtilities
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00009E38 File Offset: 0x00008038
		public static void RandomizeChords(this TrackChunk trackChunk, IBounds bounds, TempoMap tempoMap, long notesTolerance = 0L, ChordsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			using (ChordsManager chordsManager = trackChunk.ManageChords(notesTolerance, null))
			{
				new ChordsRandomizer().Randomize(chordsManager.Chords, bounds, tempoMap, settings);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009EAC File Offset: 0x000080AC
		public static void RandomizeChords(this IEnumerable<TrackChunk> trackChunks, IBounds bounds, TempoMap tempoMap, long notesTolerance = 0L, ChordsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.RandomizeChords(bounds, tempoMap, notesTolerance, settings);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009F24 File Offset: 0x00008124
		public static void RandomizeChords(this MidiFile midiFile, IBounds bounds, long notesTolerance = 0L, ChordsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().RandomizeChords(bounds, tempoMap, notesTolerance, settings);
		}
	}
}
