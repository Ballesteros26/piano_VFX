using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000141 RID: 321
	public sealed class CuePointEvent : BaseTextEvent
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x0001ED82 File Offset: 0x0001CF82
		public CuePointEvent()
			: base(MidiEventType.CuePoint)
		{
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001ED8C File Offset: 0x0001CF8C
		public CuePointEvent(string text)
			: base(MidiEventType.CuePoint, text)
		{
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001ED97 File Offset: 0x0001CF97
		protected override MidiEvent CloneEvent()
		{
			return new CuePointEvent(base.Text);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001EDA4 File Offset: 0x0001CFA4
		public override string ToString()
		{
			return "Cue Point (" + base.Text + ")";
		}
	}
}
