using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200013F RID: 319
	public sealed class ChannelPrefixEvent : MetaEvent
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x0001ECE1 File Offset: 0x0001CEE1
		public ChannelPrefixEvent()
			: base(MidiEventType.ChannelPrefix)
		{
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001ECEB File Offset: 0x0001CEEB
		public ChannelPrefixEvent(byte channel)
			: this()
		{
			this.Channel = channel;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001ECFA File Offset: 0x0001CEFA
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x0001ED02 File Offset: 0x0001CF02
		public byte Channel { get; set; }

		// Token: 0x06000831 RID: 2097 RVA: 0x0001ED0B File Offset: 0x0001CF0B
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			this.Channel = reader.ReadByte();
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001ED19 File Offset: 0x0001CF19
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(this.Channel);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00003941 File Offset: 0x00001B41
		protected override int GetContentSize(WritingSettings settings)
		{
			return 1;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001ED27 File Offset: 0x0001CF27
		protected override MidiEvent CloneEvent()
		{
			return new ChannelPrefixEvent(this.Channel);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001ED34 File Offset: 0x0001CF34
		public override string ToString()
		{
			return string.Format("Channel Prefix ({0})", this.Channel);
		}
	}
}
