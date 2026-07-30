using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000DC RID: 220
	public static class TrackChunkUtilities
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00018050 File Offset: 0x00016250
		public static void ShiftEvents(this TrackChunk trackChunk, ITimeSpan distance, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			long num = TimeConverter.ConvertFrom(distance, TempoMap.Create(tempoMap.TimeDivision));
			MidiEvent midiEvent = trackChunk.Events.FirstOrDefault<MidiEvent>();
			if (midiEvent == null)
			{
				return;
			}
			midiEvent.DeltaTime += num;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000180B0 File Offset: 0x000162B0
		public static void ShiftEvents(this IEnumerable<TrackChunk> trackChunks, ITimeSpan distance, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.ShiftEvents(distance, tempoMap);
			}
		}
	}
}
