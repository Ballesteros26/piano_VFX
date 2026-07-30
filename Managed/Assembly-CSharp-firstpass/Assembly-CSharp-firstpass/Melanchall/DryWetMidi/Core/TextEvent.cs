using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200014F RID: 335
	public sealed class TextEvent : BaseTextEvent
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x0001F471 File Offset: 0x0001D671
		public TextEvent()
			: base(MidiEventType.Text)
		{
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001F47A File Offset: 0x0001D67A
		public TextEvent(string text)
			: base(MidiEventType.Text, text)
		{
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001F484 File Offset: 0x0001D684
		protected override MidiEvent CloneEvent()
		{
			return new TextEvent(base.Text);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001F491 File Offset: 0x0001D691
		public override string ToString()
		{
			return "Text (" + base.Text + ")";
		}
	}
}
