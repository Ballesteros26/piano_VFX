using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x020001A0 RID: 416
	public static class TrackChunkUtilities
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x00022301 File Offset: 0x00020501
		public static IEnumerable<TrackChunk> GetTrackChunks(this MidiFile midiFile)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return midiFile.Chunks.OfType<TrackChunk>();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00022319 File Offset: 0x00020519
		public static TrackChunk Merge(this IEnumerable<TrackChunk> trackChunks)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			return TrackChunkUtilities.ConvertTrackChunks(trackChunks, MidiFileFormat.SingleTrack).First<TrackChunk>();
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00022332 File Offset: 0x00020532
		public static IEnumerable<TrackChunk> Explode(this TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return TrackChunkUtilities.ConvertTrackChunks(new TrackChunk[] { trackChunk }, MidiFileFormat.MultiTrack);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00022350 File Offset: 0x00020550
		public static IEnumerable<FourBitNumber> GetChannels(this TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return (from e in trackChunk.Events.OfType<ChannelEvent>()
				select e.Channel).Distinct<FourBitNumber>().ToArray<FourBitNumber>();
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000223A4 File Offset: 0x000205A4
		public static IEnumerable<FourBitNumber> GetChannels(this IEnumerable<TrackChunk> trackChunks)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			return trackChunks.Where((TrackChunk c) => c != null).SelectMany(new Func<TrackChunk, IEnumerable<FourBitNumber>>(TrackChunkUtilities.GetChannels)).Distinct<FourBitNumber>()
				.ToArray<FourBitNumber>();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000223FC File Offset: 0x000205FC
		private static IEnumerable<TrackChunk> ConvertTrackChunks(IEnumerable<TrackChunk> trackChunks, MidiFileFormat format)
		{
			return ChunksConverterFactory.GetConverter(format).Convert(trackChunks).OfType<TrackChunk>();
		}
	}
}
