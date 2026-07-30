using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000063 RID: 99
	public static class TimedEventsRandomizerUtilities
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000A074 File Offset: 0x00008274
		public static void RandomizeTimedEvents(this TrackChunk trackChunk, IBounds bounds, TempoMap tempoMap, TimedEventsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using (TimedEventsManager timedEventsManager = trackChunk.ManageTimedEvents(null))
			{
				new TimedEventsRandomizer().Randomize(timedEventsManager.Events, bounds, tempoMap, settings);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A0DC File Offset: 0x000082DC
		public static void RandomizeTimedEvents(this IEnumerable<TrackChunk> trackChunks, IBounds bounds, TempoMap tempoMap, TimedEventsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.RandomizeTimedEvents(bounds, tempoMap, settings);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A148 File Offset: 0x00008348
		public static void RandomizeTimedEvents(this MidiFile midiFile, IBounds bounds, TimedEventsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("bounds", bounds);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().RandomizeTimedEvents(bounds, tempoMap, settings);
		}
	}
}
