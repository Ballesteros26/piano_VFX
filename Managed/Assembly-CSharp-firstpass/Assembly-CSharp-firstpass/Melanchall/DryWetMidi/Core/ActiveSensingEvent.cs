using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000161 RID: 353
	public sealed class ActiveSensingEvent : SystemRealTimeEvent
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x00020169 File Offset: 0x0001E369
		public ActiveSensingEvent()
			: base(MidiEventType.ActiveSensing)
		{
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00020173 File Offset: 0x0001E373
		protected override MidiEvent CloneEvent()
		{
			return new ActiveSensingEvent();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002017A File Offset: 0x0001E37A
		public override string ToString()
		{
			return "Active Sensing";
		}
	}
}
