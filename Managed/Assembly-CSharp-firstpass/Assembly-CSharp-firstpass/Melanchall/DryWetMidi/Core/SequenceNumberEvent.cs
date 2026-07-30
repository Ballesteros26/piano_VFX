using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200014A RID: 330
	public sealed class SequenceNumberEvent : MetaEvent
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x0001F092 File Offset: 0x0001D292
		public SequenceNumberEvent()
			: base(MidiEventType.SequenceNumber)
		{
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001F09B File Offset: 0x0001D29B
		public SequenceNumberEvent(ushort number)
			: this()
		{
			this.Number = number;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0001F0AA File Offset: 0x0001D2AA
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x0001F0B2 File Offset: 0x0001D2B2
		public ushort Number { get; set; }

		// Token: 0x06000871 RID: 2161 RVA: 0x0001F0BB File Offset: 0x0001D2BB
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			if (size < 2)
			{
				return;
			}
			this.Number = reader.ReadWord();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001F0CE File Offset: 0x0001D2CE
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteWord(this.Number);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001EF45 File Offset: 0x0001D145
		protected override int GetContentSize(WritingSettings settings)
		{
			return 2;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001F0DC File Offset: 0x0001D2DC
		protected override MidiEvent CloneEvent()
		{
			return new SequenceNumberEvent(this.Number);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001F0E9 File Offset: 0x0001D2E9
		public override string ToString()
		{
			return string.Format("Sequence Number ({0})", this.Number);
		}
	}
}
