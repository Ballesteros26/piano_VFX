using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000163 RID: 355
	public sealed class ResetEvent : SystemRealTimeEvent
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x00020199 File Offset: 0x0001E399
		public ResetEvent()
			: base(MidiEventType.Reset)
		{
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000201A3 File Offset: 0x0001E3A3
		protected override MidiEvent CloneEvent()
		{
			return new ResetEvent();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000201AA File Offset: 0x0001E3AA
		public override string ToString()
		{
			return "Reset";
		}
	}
}
