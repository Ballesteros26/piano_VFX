using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000140 RID: 320
	public sealed class CopyrightNoticeEvent : BaseTextEvent
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x0001ED4B File Offset: 0x0001CF4B
		public CopyrightNoticeEvent()
			: base(MidiEventType.CopyrightNotice)
		{
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001ED54 File Offset: 0x0001CF54
		public CopyrightNoticeEvent(string text)
			: base(MidiEventType.CopyrightNotice, text)
		{
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001ED5E File Offset: 0x0001CF5E
		protected override MidiEvent CloneEvent()
		{
			return new CopyrightNoticeEvent(base.Text);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001ED6B File Offset: 0x0001CF6B
		public override string ToString()
		{
			return "Copyright Notice (" + base.Text + ")";
		}
	}
}
