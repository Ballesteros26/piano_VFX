using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000166 RID: 358
	public sealed class TimingClockEvent : SystemRealTimeEvent
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x000201E1 File Offset: 0x0001E3E1
		public TimingClockEvent()
			: base(MidiEventType.TimingClock)
		{
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000201EB File Offset: 0x0001E3EB
		protected override MidiEvent CloneEvent()
		{
			return new TimingClockEvent();
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000201F2 File Offset: 0x0001E3F2
		public override string ToString()
		{
			return "Timing Clock";
		}
	}
}
