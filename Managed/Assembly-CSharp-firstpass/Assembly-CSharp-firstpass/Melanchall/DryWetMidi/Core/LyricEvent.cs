using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000146 RID: 326
	public sealed class LyricEvent : BaseTextEvent
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x0001EF7D File Offset: 0x0001D17D
		public LyricEvent()
			: base(MidiEventType.Lyric)
		{
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001EF86 File Offset: 0x0001D186
		public LyricEvent(string text)
			: base(MidiEventType.Lyric, text)
		{
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001EF90 File Offset: 0x0001D190
		protected override MidiEvent CloneEvent()
		{
			return new LyricEvent(base.Text);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001EF9D File Offset: 0x0001D19D
		public override string ToString()
		{
			return "Lyric (" + base.Text + ")";
		}
	}
}
