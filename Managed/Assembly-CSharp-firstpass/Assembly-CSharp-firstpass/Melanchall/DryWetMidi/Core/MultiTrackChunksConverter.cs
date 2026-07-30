using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200011C RID: 284
	internal sealed class MultiTrackChunksConverter : IChunksConverter
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		public IEnumerable<MidiChunk> Convert(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			TrackChunk[] array = chunks.OfType<TrackChunk>().ToArray<TrackChunk>();
			if (array.Length != 1)
			{
				return chunks;
			}
			MultiTrackChunksConverter.TrackChunkDescriptor[] array2 = (from i in Enumerable.Range(0, 17)
				select new MultiTrackChunksConverter.TrackChunkDescriptor()).ToArray<MultiTrackChunksConverter.TrackChunkDescriptor>();
			FourBitNumber? fourBitNumber = null;
			using (IEnumerator<MidiEvent> enumerator = array.First<TrackChunk>().Events.Select((MidiEvent m) => m.Clone()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MidiEvent midiEvent = enumerator.Current;
					Array.ForEach<MultiTrackChunksConverter.TrackChunkDescriptor>(array2, delegate(MultiTrackChunksConverter.TrackChunkDescriptor d)
					{
						d.DeltaTime += midiEvent.DeltaTime;
					});
					ChannelEvent channelEvent = midiEvent as ChannelEvent;
					if (channelEvent != null)
					{
						array2[(int)(channelEvent.Channel + 1)].AddEvent(midiEvent.Clone());
						fourBitNumber = null;
					}
					else
					{
						if (!(midiEvent is MetaEvent))
						{
							fourBitNumber = null;
						}
						ChannelPrefixEvent channelPrefixEvent = midiEvent as ChannelPrefixEvent;
						if (channelPrefixEvent != null)
						{
							fourBitNumber = new FourBitNumber?((FourBitNumber)channelPrefixEvent.Channel);
						}
						if (fourBitNumber != null)
						{
							array2[(int)(fourBitNumber.Value + 1)].AddEvent(midiEvent);
						}
						else
						{
							array2[0].AddEvent(midiEvent);
						}
					}
				}
			}
			return (from d in array2
				select d.Chunk into c
				where c.Events.Any<MidiEvent>()
				select c).Concat(chunks.Where((MidiChunk c) => !(c is TrackChunk)));
		}

		// Token: 0x04000843 RID: 2115
		private const int ChannelsCount = 16;

		// Token: 0x02000289 RID: 649
		private sealed class TrackChunkDescriptor
		{
			// Token: 0x17000203 RID: 515
			// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0002AF9B File Offset: 0x0002919B
			public TrackChunk Chunk { get; } = new TrackChunk();

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0002AFA3 File Offset: 0x000291A3
			// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x0002AFAB File Offset: 0x000291AB
			public long DeltaTime { get; set; }

			// Token: 0x06000EC2 RID: 3778 RVA: 0x0002AFB4 File Offset: 0x000291B4
			public void AddEvent(MidiEvent midiEvent)
			{
				midiEvent.DeltaTime = this.DeltaTime;
				this.Chunk.Events.Add(midiEvent);
				this.DeltaTime = 0L;
			}
		}
	}
}
