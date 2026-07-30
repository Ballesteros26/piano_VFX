using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200011D RID: 285
	internal sealed class SingleTrackChunksConverter : IChunksConverter
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x0001D5B0 File Offset: 0x0001B7B0
		public IEnumerable<MidiChunk> Convert(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			TrackChunk[] array = chunks.OfType<TrackChunk>().ToArray<TrackChunk>();
			if (array.Length == 1)
			{
				return chunks;
			}
			MidiEvent midiEvent;
			IEnumerable<SingleTrackChunksConverter.EventDescriptor> enumerable = array.SelectMany(delegate(TrackChunk trackChunk)
			{
				long absoluteTime = 0L;
				int channel = -1;
				return trackChunk.Events.Select(delegate(MidiEvent midiEvent)
				{
					ChannelPrefixEvent channelPrefixEvent = midiEvent as ChannelPrefixEvent;
					if (channelPrefixEvent != null)
					{
						channel = (int)channelPrefixEvent.Channel;
					}
					if (!(midiEvent is MetaEvent))
					{
						channel = -1;
					}
					return new SingleTrackChunksConverter.EventDescriptor(midiEvent, absoluteTime += midiEvent.DeltaTime, channel);
				});
			}).OrderBy((SingleTrackChunksConverter.EventDescriptor d) => d, new SingleTrackChunksConverter.EventDescriptorComparer());
			TrackChunk trackChunk2 = new TrackChunk();
			long num = 0L;
			foreach (SingleTrackChunksConverter.EventDescriptor eventDescriptor in enumerable)
			{
				midiEvent = eventDescriptor.Event.Clone();
				midiEvent.DeltaTime = eventDescriptor.AbsoluteTime - num;
				trackChunk2.Events.Add(midiEvent);
				num = eventDescriptor.AbsoluteTime;
			}
			return new TrackChunk[] { trackChunk2 }.Concat(chunks.Where((MidiChunk c) => !(c is TrackChunk)));
		}

		// Token: 0x0200028C RID: 652
		private sealed class EventDescriptor
		{
			// Token: 0x06000ECD RID: 3789 RVA: 0x0002B030 File Offset: 0x00029230
			public EventDescriptor(MidiEvent midiEvent, long absoluteTime, int channel)
			{
				this.Event = midiEvent;
				this.AbsoluteTime = absoluteTime;
				this.Channel = channel;
			}

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0002B04D File Offset: 0x0002924D
			public MidiEvent Event { get; }

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0002B055 File Offset: 0x00029255
			public long AbsoluteTime { get; }

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0002B05D File Offset: 0x0002925D
			public int Channel { get; }
		}

		// Token: 0x0200028D RID: 653
		private sealed class EventDescriptorComparer : IComparer<SingleTrackChunksConverter.EventDescriptor>
		{
			// Token: 0x06000ED1 RID: 3793 RVA: 0x0002B068 File Offset: 0x00029268
			public int Compare(SingleTrackChunksConverter.EventDescriptor x, SingleTrackChunksConverter.EventDescriptor y)
			{
				long num = x.AbsoluteTime - y.AbsoluteTime;
				if (num != 0L)
				{
					return Math.Sign(num);
				}
				MetaEvent metaEvent = x.Event as MetaEvent;
				MetaEvent metaEvent2 = y.Event as MetaEvent;
				if (metaEvent != null && metaEvent2 == null)
				{
					return -1;
				}
				if (metaEvent == null && metaEvent2 != null)
				{
					return 1;
				}
				if (metaEvent == null)
				{
					return 0;
				}
				int num2 = x.Channel - y.Channel;
				if (num2 != 0)
				{
					return num2;
				}
				ChannelPrefixEvent channelPrefixEvent = x.Event as ChannelPrefixEvent;
				ChannelPrefixEvent channelPrefixEvent2 = y.Event as ChannelPrefixEvent;
				if (channelPrefixEvent != null && channelPrefixEvent2 == null)
				{
					return -1;
				}
				if (channelPrefixEvent == null && channelPrefixEvent2 != null)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
