using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000147 RID: 327
	public sealed class MarkerEvent : BaseTextEvent
	{
		// Token: 0x0600085C RID: 2140 RVA: 0x0001EFB4 File Offset: 0x0001D1B4
		public MarkerEvent()
			: base(MidiEventType.Marker)
		{
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001EFBD File Offset: 0x0001D1BD
		public MarkerEvent(string text)
			: base(MidiEventType.Marker, text)
		{
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001EFC7 File Offset: 0x0001D1C7
		protected override MidiEvent CloneEvent()
		{
			return new MarkerEvent(base.Text);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001EFD4 File Offset: 0x0001D1D4
		public override string ToString()
		{
			return "Marker (" + base.Text + ")";
		}
	}
}
