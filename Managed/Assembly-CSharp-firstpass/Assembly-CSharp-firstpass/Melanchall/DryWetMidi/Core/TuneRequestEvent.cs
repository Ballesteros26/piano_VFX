using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000160 RID: 352
	public sealed class TuneRequestEvent : SystemCommonEvent
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x00020151 File Offset: 0x0001E351
		public TuneRequestEvent()
			: base(MidiEventType.TuneRequest)
		{
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00002994 File Offset: 0x00000B94
		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00002994 File Offset: 0x00000B94
		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001E512 File Offset: 0x0001C712
		internal override int GetSize(WritingSettings settings)
		{
			return 0;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0002015B File Offset: 0x0001E35B
		protected override MidiEvent CloneEvent()
		{
			return new TuneRequestEvent();
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00020162 File Offset: 0x0001E362
		public override string ToString()
		{
			return "Tune Request";
		}
	}
}
