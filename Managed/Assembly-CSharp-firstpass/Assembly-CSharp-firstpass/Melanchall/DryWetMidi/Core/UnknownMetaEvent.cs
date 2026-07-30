using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000151 RID: 337
	public sealed class UnknownMetaEvent : MetaEvent
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0001F67D File Offset: 0x0001D87D
		internal UnknownMetaEvent(byte statusByte)
			: this(statusByte, null)
		{
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001F687 File Offset: 0x0001D887
		internal UnknownMetaEvent(byte statusByte, byte[] data)
			: base(MidiEventType.UnknownMeta)
		{
			this.StatusByte = statusByte;
			this.Data = data;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0001F69F File Offset: 0x0001D89F
		public byte StatusByte { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001F6A7 File Offset: 0x0001D8A7
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0001F6AF File Offset: 0x0001D8AF
		public byte[] Data { get; private set; }

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			ThrowIfArgument.IsNegative("size", size, "Unknown meta event cannot be read since the size is negative number.");
			this.Data = reader.ReadBytes(size);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001F6D8 File Offset: 0x0001D8D8
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			byte[] data = this.Data;
			if (data != null)
			{
				writer.WriteBytes(data);
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001F6F6 File Offset: 0x0001D8F6
		protected override int GetContentSize(WritingSettings settings)
		{
			byte[] data = this.Data;
			if (data == null)
			{
				return 0;
			}
			return data.Length;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001F706 File Offset: 0x0001D906
		protected override MidiEvent CloneEvent()
		{
			byte statusByte = this.StatusByte;
			byte[] data = this.Data;
			return new UnknownMetaEvent(statusByte, ((data != null) ? data.Clone() : null) as byte[]);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001F72A File Offset: 0x0001D92A
		public override string ToString()
		{
			return string.Format("Unknown meta event ({0})", this.StatusByte);
		}
	}
}
