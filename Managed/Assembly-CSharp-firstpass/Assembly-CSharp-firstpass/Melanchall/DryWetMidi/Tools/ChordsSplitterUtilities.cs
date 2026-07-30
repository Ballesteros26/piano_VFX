using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000036 RID: 54
	public static class ChordsSplitterUtilities
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00007654 File Offset: 0x00005854
		public static void SplitChordsByStep(this TrackChunk trackChunk, ITimeSpan step, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			ChordsSplitterUtilities.SplitTrackChunkChords(trackChunk, (ChordsSplitter splitter, IEnumerable<Chord> chords) => splitter.SplitByStep(chords, step, tempoMap), notesTolerance);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000076C0 File Offset: 0x000058C0
		public static void SplitChordsByStep(this IEnumerable<TrackChunk> trackChunks, ITimeSpan step, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsByStep(step, tempoMap, notesTolerance);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007734 File Offset: 0x00005934
		public static void SplitChordsByStep(this MidiFile midiFile, ITimeSpan step, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsByStep(step, tempoMap, notesTolerance);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007778 File Offset: 0x00005978
		public static void SplitChordsByPartsNumber(this TrackChunk trackChunk, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			ChordsSplitterUtilities.SplitTrackChunkChords(trackChunk, (ChordsSplitter splitter, IEnumerable<Chord> chords) => splitter.SplitByPartsNumber(chords, partsNumber, lengthType, tempoMap), notesTolerance);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007800 File Offset: 0x00005A00
		public static void SplitChordsByPartsNumber(this IEnumerable<TrackChunk> trackChunks, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsByPartsNumber(partsNumber, lengthType, tempoMap, notesTolerance);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007888 File Offset: 0x00005A88
		public static void SplitChordsByPartsNumber(this MidiFile midiFile, int partsNumber, TimeSpanType lengthType, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsByPartsNumber(partsNumber, lengthType, tempoMap, notesTolerance);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000078DC File Offset: 0x00005ADC
		public static void SplitChordsByGrid(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			ChordsSplitterUtilities.SplitTrackChunkChords(trackChunk, (ChordsSplitter splitter, IEnumerable<Chord> chords) => splitter.SplitByGrid(chords, grid, tempoMap), notesTolerance);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007948 File Offset: 0x00005B48
		public static void SplitChordsByGrid(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsByGrid(grid, tempoMap, notesTolerance);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000079BC File Offset: 0x00005BBC
		public static void SplitChordsByGrid(this MidiFile midiFile, IGrid grid, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsByGrid(grid, tempoMap, notesTolerance);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007A00 File Offset: 0x00005C00
		public static void SplitChordsAtDistance(this TrackChunk trackChunk, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			ChordsSplitterUtilities.SplitTrackChunkChords(trackChunk, (ChordsSplitter splitter, IEnumerable<Chord> chords) => splitter.SplitAtDistance(chords, distance, from, tempoMap), notesTolerance);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007A84 File Offset: 0x00005C84
		public static void SplitChordsAtDistance(this IEnumerable<TrackChunk> trackChunks, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsAtDistance(distance, from, tempoMap, notesTolerance);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007B08 File Offset: 0x00005D08
		public static void SplitChordsAtDistance(this MidiFile midiFile, ITimeSpan distance, LengthedObjectTarget from, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsAtDistance(distance, from, tempoMap, notesTolerance);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007B58 File Offset: 0x00005D58
		public static void SplitChordsAtDistance(this TrackChunk trackChunk, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, string.Format("Ratio is out of [{0}; {1}] range.", 0.0, 1.0));
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			ChordsSplitterUtilities.SplitTrackChunkChords(trackChunk, (ChordsSplitter splitter, IEnumerable<Chord> chords) => splitter.SplitAtDistance(chords, ratio, lengthType, from, tempoMap), notesTolerance);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007C2C File Offset: 0x00005E2C
		public static void SplitChordsAtDistance(this IEnumerable<TrackChunk> trackChunks, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, string.Format("Ratio is out of [{0}; {1}] range.", 0.0, 1.0));
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsAtDistance(ratio, lengthType, from, tempoMap, notesTolerance);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public static void SplitChordsAtDistance(this MidiFile midiFile, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, long notesTolerance = 0L)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, string.Format("Ratio is out of [{0}; {1}] range.", 0.0, 1.0));
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfNotesTolerance.IsNegative("notesTolerance", notesTolerance);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsAtDistance(ratio, lengthType, from, tempoMap, notesTolerance);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007D8C File Offset: 0x00005F8C
		private static void SplitTrackChunkChords(TrackChunk trackChunk, Func<ChordsSplitter, IEnumerable<Chord>, IEnumerable<Chord>> splitOperation, long notesTolerance)
		{
			using (ChordsManager chordsManager = trackChunk.ManageChords(notesTolerance, null))
			{
				ChordsCollection chords = chordsManager.Chords;
				ChordsSplitter chordsSplitter = new ChordsSplitter();
				List<Chord> list = splitOperation(chordsSplitter, chords).ToList<Chord>();
				chords.Clear();
				chords.Add(list);
			}
		}
	}
}
