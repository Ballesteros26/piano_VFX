using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000037 RID: 55
	public static class NotesSplitterUtilities
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public static void SplitNotesByStep(this TrackChunk trackChunk, ITimeSpan step, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			NotesSplitterUtilities.SplitTrackChunkNotes(trackChunk, (NotesSplitter splitter, IEnumerable<Note> notes) => splitter.SplitByStep(notes, step, tempoMap));
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007E48 File Offset: 0x00006048
		public static void SplitNotesByStep(this IEnumerable<TrackChunk> trackChunks, ITimeSpan step, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesByStep(step, tempoMap);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007EB0 File Offset: 0x000060B0
		public static void SplitNotesByStep(this MidiFile midiFile, ITimeSpan step)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("step", step);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesByStep(step, tempoMap);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007EE8 File Offset: 0x000060E8
		public static void SplitNotesByPartsNumber(this TrackChunk trackChunk, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			NotesSplitterUtilities.SplitTrackChunkNotes(trackChunk, (NotesSplitter splitter, IEnumerable<Note> notes) => splitter.SplitByPartsNumber(notes, partsNumber, lengthType, tempoMap));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007F64 File Offset: 0x00006164
		public static void SplitNotesByPartsNumber(this IEnumerable<TrackChunk> trackChunks, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesByPartsNumber(partsNumber, lengthType, tempoMap);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007FE0 File Offset: 0x000061E0
		public static void SplitNotesByPartsNumber(this MidiFile midiFile, int partsNumber, TimeSpanType lengthType)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesByPartsNumber(partsNumber, lengthType, tempoMap);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008028 File Offset: 0x00006228
		public static void SplitNotesByGrid(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			NotesSplitterUtilities.SplitTrackChunkNotes(trackChunk, (NotesSplitter splitter, IEnumerable<Note> notes) => splitter.SplitByGrid(notes, grid, tempoMap));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008088 File Offset: 0x00006288
		public static void SplitNotesByGrid(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesByGrid(grid, tempoMap);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000080F0 File Offset: 0x000062F0
		public static void SplitNotesByGrid(this MidiFile midiFile, IGrid grid)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesByGrid(grid, tempoMap);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008128 File Offset: 0x00006328
		public static void SplitNotesAtDistance(this TrackChunk trackChunk, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			NotesSplitterUtilities.SplitTrackChunkNotes(trackChunk, (NotesSplitter splitter, IEnumerable<Note> notes) => splitter.SplitAtDistance(notes, distance, from, tempoMap));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000081A0 File Offset: 0x000063A0
		public static void SplitNotesAtDistance(this IEnumerable<TrackChunk> trackChunks, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesAtDistance(distance, from, tempoMap);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008214 File Offset: 0x00006414
		public static void SplitNotesAtDistance(this MidiFile midiFile, ITimeSpan distance, LengthedObjectTarget from)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesAtDistance(distance, from, tempoMap);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008258 File Offset: 0x00006458
		public static void SplitNotesAtDistance(this TrackChunk trackChunk, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, string.Format("Ratio is out of [{0}; {1}] range.", 0.0, 1.0));
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			NotesSplitterUtilities.SplitTrackChunkNotes(trackChunk, (NotesSplitter splitter, IEnumerable<Note> notes) => splitter.SplitAtDistance(notes, ratio, lengthType, from, tempoMap));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008320 File Offset: 0x00006520
		public static void SplitNotesAtDistance(this IEnumerable<TrackChunk> trackChunks, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, string.Format("Ratio is out of [{0}; {1}] range.", 0.0, 1.0));
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesAtDistance(ratio, lengthType, from, tempoMap);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000083DC File Offset: 0x000065DC
		public static void SplitNotesAtDistance(this MidiFile midiFile, double ratio, TimeSpanType lengthType, LengthedObjectTarget from)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, string.Format("Ratio is out of [{0}; {1}] range.", 0.0, 1.0));
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("from", from);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesAtDistance(ratio, lengthType, from, tempoMap);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008464 File Offset: 0x00006664
		private static void SplitTrackChunkNotes(TrackChunk trackChunk, Func<NotesSplitter, IEnumerable<Note>, IEnumerable<Note>> splitOperation)
		{
			using (NotesManager notesManager = trackChunk.ManageNotes(null))
			{
				NotesCollection notes = notesManager.Notes;
				NotesSplitter notesSplitter = new NotesSplitter();
				List<Note> list = splitOperation(notesSplitter, notes).ToList<Note>();
				notes.Clear();
				notes.Add(list);
			}
		}
	}
}
