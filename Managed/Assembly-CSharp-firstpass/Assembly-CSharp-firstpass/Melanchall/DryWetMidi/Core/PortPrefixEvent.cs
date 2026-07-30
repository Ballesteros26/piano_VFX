using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000148 RID: 328
	public sealed class PortPrefixEvent : MetaEvent
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x0001EFEB File Offset: 0x0001D1EB
		public PortPrefixEvent()
			: base(MidiEventType.PortPrefix)
		{
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001EFF5 File Offset: 0x0001D1F5
		public PortPrefixEvent(byte port)
			: this()
		{
			this.Port = port;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001F004 File Offset: 0x0001D204
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0001F00C File Offset: 0x0001D20C
		public byte Port { get; set; }

		// Token: 0x06000864 RID: 2148 RVA: 0x0001F015 File Offset: 0x0001D215
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			if (size >= 1)
			{
				this.Port = reader.ReadByte();
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001F027 File Offset: 0x0001D227
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(this.Port);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00003941 File Offset: 0x00001B41
		protected override int GetContentSize(WritingSettings settings)
		{
			return 1;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001F035 File Offset: 0x0001D235
		protected override MidiEvent CloneEvent()
		{
			return new PortPrefixEvent(this.Port);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001F042 File Offset: 0x0001D242
		public override string ToString()
		{
			return string.Format("Port Prefix ({0})", this.Port);
		}
	}
}
