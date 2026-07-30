using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000164 RID: 356
	public sealed class StartEvent : SystemRealTimeEvent
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x000201B1 File Offset: 0x0001E3B1
		public StartEvent()
			: base(MidiEventType.Start)
		{
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override MidiEvent CloneEvent()
		{
			return new StartEvent();
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000201C2 File Offset: 0x0001E3C2
		public override string ToString()
		{
			return "Start";
		}
	}
}
