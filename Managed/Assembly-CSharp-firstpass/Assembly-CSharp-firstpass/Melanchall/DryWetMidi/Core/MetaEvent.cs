using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200012A RID: 298
	public abstract class MetaEvent : MidiEvent
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x0001E390 File Offset: 0x0001C590
		protected MetaEvent()
			: this(MidiEventType.CustomMeta)
		{
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001E39A File Offset: 0x0001C59A
		internal MetaEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001E3A3 File Offset: 0x0001C5A3
		internal sealed override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			this.ReadContent(reader, settings, size);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001E3AE File Offset: 0x0001C5AE
		internal sealed override void Write(MidiWriter writer, WritingSettings settings)
		{
			this.WriteContent(writer, settings);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
		internal sealed override int GetSize(WritingSettings settings)
		{
			return this.GetContentSize(settings);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001E3C1 File Offset: 0x0001C5C1
		public static byte[] GetStandardMetaEventStatusBytes()
		{
			return StandardMetaEventStatusBytes.GetStatusBytes();
		}

		// Token: 0x060007C2 RID: 1986
		protected abstract void ReadContent(MidiReader reader, ReadingSettings settings, int size);

		// Token: 0x060007C3 RID: 1987
		protected abstract void WriteContent(MidiWriter writer, WritingSettings settings);

		// Token: 0x060007C4 RID: 1988
		protected abstract int GetContentSize(WritingSettings settings);
	}
}
