using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000AC RID: 172
	public static class TempoMapManagingUtilities
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x00012FEA File Offset: 0x000111EA
		public static TempoMapManager ManageTempoMap(this IEnumerable<EventsCollection> eventsCollections, TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("eventsCollections", eventsCollections);
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			return new TempoMapManager(timeDivision, eventsCollections);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001300C File Offset: 0x0001120C
		public static TempoMapManager ManageTempoMap(this IEnumerable<TrackChunk> trackChunks, TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			return trackChunks.Select((TrackChunk c) => c.Events).ManageTempoMap(timeDivision);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001305A File Offset: 0x0001125A
		public static TempoMapManager ManageTempoMap(this MidiFile file)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.GetTrackChunks().ManageTempoMap(file.TimeDivision);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013078 File Offset: 0x00011278
		public static TempoMap GetTempoMap(this IEnumerable<EventsCollection> eventsCollections, TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("eventsCollections", eventsCollections);
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			if (!eventsCollections.Any<EventsCollection>())
			{
				return new TempoMap(timeDivision);
			}
			return eventsCollections.ManageTempoMap(timeDivision).TempoMap;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000130AB File Offset: 0x000112AB
		public static TempoMap GetTempoMap(this IEnumerable<TrackChunk> trackChunks, TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			if (!trackChunks.Any<TrackChunk>())
			{
				return new TempoMap(timeDivision);
			}
			return trackChunks.ManageTempoMap(timeDivision).TempoMap;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000130DE File Offset: 0x000112DE
		public static TempoMap GetTempoMap(this MidiFile file)
		{
			ThrowIfArgument.IsNull("file", file);
			if (!file.GetTrackChunks().Any<TrackChunk>())
			{
				return new TempoMap(file.TimeDivision);
			}
			return file.ManageTempoMap().TempoMap;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00013110 File Offset: 0x00011310
		public static void ReplaceTempoMap(this IEnumerable<EventsCollection> eventsCollections, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("eventsCollections", eventsCollections);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsEmptyCollection<EventsCollection>("eventsCollections", eventsCollections, "Collection of EventsCollection is empty.");
			using (TempoMapManager tempoMapManager = eventsCollections.ManageTempoMap(tempoMap.TimeDivision))
			{
				tempoMapManager.ReplaceTempoMap(tempoMap);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00013174 File Offset: 0x00011374
		public static void ReplaceTempoMap(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsEmptyCollection<TrackChunk>("trackChunks", trackChunks, "Collection of TrackChunk is empty.");
			trackChunks.Select((TrackChunk c) => c.Events).ReplaceTempoMap(tempoMap);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000131D4 File Offset: 0x000113D4
		public static void ReplaceTempoMap(this MidiFile file, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			IEnumerable<TrackChunk> trackChunks = file.GetTrackChunks();
			ThrowIfArgument.IsEmptyCollection<TrackChunk>("trackChunks", trackChunks, "Collection of TrackChunk of the file is empty.");
			trackChunks.ReplaceTempoMap(tempoMap);
			file.TimeDivision = tempoMap.TimeDivision.Clone();
		}
	}
}
