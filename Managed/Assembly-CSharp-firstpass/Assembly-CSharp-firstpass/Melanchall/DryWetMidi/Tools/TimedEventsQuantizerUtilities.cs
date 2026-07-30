using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000054 RID: 84
	public static class TimedEventsQuantizerUtilities
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00009928 File Offset: 0x00007B28
		public static void QuantizeTimedEvents(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap, TimedEventsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using (TimedEventsManager timedEventsManager = trackChunk.ManageTimedEvents(null))
			{
				new TimedEventsQuantizer().Quantize(timedEventsManager.Events, grid, tempoMap, settings);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009990 File Offset: 0x00007B90
		public static void QuantizeTimedEvents(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap, TimedEventsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.QuantizeTimedEvents(grid, tempoMap, settings);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000099FC File Offset: 0x00007BFC
		public static void QuantizeTimedEvents(this MidiFile midiFile, IGrid grid, TimedEventsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().QuantizeTimedEvents(grid, tempoMap, settings);
		}
	}
}
