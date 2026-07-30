using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000165 RID: 357
	public sealed class StopEvent : SystemRealTimeEvent
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x000201C9 File Offset: 0x0001E3C9
		public StopEvent()
			: base(MidiEventType.Stop)
		{
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000201D3 File Offset: 0x0001E3D3
		protected override MidiEvent CloneEvent()
		{
			return new StopEvent();
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000201DA File Offset: 0x0001E3DA
		public override string ToString()
		{
			return "Stop";
		}
	}
}
