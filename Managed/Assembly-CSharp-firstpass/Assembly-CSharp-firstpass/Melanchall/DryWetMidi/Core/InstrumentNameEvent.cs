using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000144 RID: 324
	public sealed class InstrumentNameEvent : BaseTextEvent
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x0001EE0C File Offset: 0x0001D00C
		public InstrumentNameEvent()
			: base(MidiEventType.InstrumentName)
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001EE15 File Offset: 0x0001D015
		public InstrumentNameEvent(string instrumentName)
			: base(MidiEventType.InstrumentName, instrumentName)
		{
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001EE1F File Offset: 0x0001D01F
		protected override MidiEvent CloneEvent()
		{
			return new InstrumentNameEvent(base.Text);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001EE2C File Offset: 0x0001D02C
		public override string ToString()
		{
			return "Instrument Name (" + base.Text + ")";
		}
	}
}
