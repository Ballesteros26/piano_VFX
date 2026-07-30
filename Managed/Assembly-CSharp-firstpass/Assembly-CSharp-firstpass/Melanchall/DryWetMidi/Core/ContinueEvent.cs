using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000162 RID: 354
	public sealed class ContinueEvent : SystemRealTimeEvent
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x00020181 File Offset: 0x0001E381
		public ContinueEvent()
			: base(MidiEventType.Continue)
		{
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002018B File Offset: 0x0001E38B
		protected override MidiEvent CloneEvent()
		{
			return new ContinueEvent();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00020192 File Offset: 0x0001E392
		public override string ToString()
		{
			return "Continue";
		}
	}
}
