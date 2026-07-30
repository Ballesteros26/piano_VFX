using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200014C RID: 332
	public sealed class SequencerSpecificEvent : MetaEvent
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x0001F137 File Offset: 0x0001D337
		public SequencerSpecificEvent()
			: base(MidiEventType.SequencerSpecific)
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001F141 File Offset: 0x0001D341
		public SequencerSpecificEvent(byte[] data)
			: this()
		{
			this.Data = data;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0001F150 File Offset: 0x0001D350
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x0001F158 File Offset: 0x0001D358
		public byte[] Data { get; set; }

		// Token: 0x0600087E RID: 2174 RVA: 0x0001F161 File Offset: 0x0001D361
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			ThrowIfArgument.IsNegative("size", size, "Sequencer specific event cannot be read since the size is negative number.");
			this.Data = reader.ReadBytes(size);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001F180 File Offset: 0x0001D380
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			byte[] data = this.Data;
			if (data != null)
			{
				writer.WriteBytes(data);
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001F19E File Offset: 0x0001D39E
		protected override int GetContentSize(WritingSettings settings)
		{
			byte[] data = this.Data;
			if (data == null)
			{
				return 0;
			}
			return data.Length;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001F1AE File Offset: 0x0001D3AE
		protected override MidiEvent CloneEvent()
		{
			byte[] data = this.Data;
			return new SequencerSpecificEvent(((data != null) ? data.Clone() : null) as byte[]);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001F1CC File Offset: 0x0001D3CC
		public override string ToString()
		{
			return "Sequencer Specific";
		}
	}
}
