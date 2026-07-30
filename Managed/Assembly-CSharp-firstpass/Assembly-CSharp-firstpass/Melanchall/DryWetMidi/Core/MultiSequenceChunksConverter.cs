using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200011B RID: 283
	internal sealed class MultiSequenceChunksConverter : IChunksConverter
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001D274 File Offset: 0x0001B474
		public IEnumerable<MidiChunk> Convert(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			TrackChunk[] array = chunks.OfType<TrackChunk>().ToArray<TrackChunk>();
			if (array.Length == 0)
			{
				return chunks;
			}
			var enumerable = array.Select((TrackChunk c, int i) => new
			{
				Chunk = c,
				Number = (((int)MultiSequenceChunksConverter.GetSequenceNumber(c)) ?? i)
			}).ToArray();
			IChunksConverter singleTrackChunksConverter = ChunksConverterFactory.GetConverter(MidiFileFormat.SingleTrack);
			return (from n in enumerable
				group n by n.Number).SelectMany(g => singleTrackChunksConverter.Convert(g.Select(n => n.Chunk))).Concat(chunks.Where((MidiChunk c) => !(c is TrackChunk)));
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001D33C File Offset: 0x0001B53C
		private static ushort? GetSequenceNumber(TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			SequenceNumberEvent sequenceNumberEvent = trackChunk.Events.TakeWhile((MidiEvent m) => m.DeltaTime == 0L).OfType<SequenceNumberEvent>().FirstOrDefault<SequenceNumberEvent>();
			if (sequenceNumberEvent == null)
			{
				return null;
			}
			return new ushort?(sequenceNumberEvent.Number);
		}
	}
}
