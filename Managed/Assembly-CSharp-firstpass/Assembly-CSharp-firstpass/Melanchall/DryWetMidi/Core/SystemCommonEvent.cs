using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200012E RID: 302
	public abstract class SystemCommonEvent : MidiEvent
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x0001E39A File Offset: 0x0001C59A
		protected SystemCommonEvent(MidiEventType eventType)
			: base(eventType)
		{
		}
	}
}
