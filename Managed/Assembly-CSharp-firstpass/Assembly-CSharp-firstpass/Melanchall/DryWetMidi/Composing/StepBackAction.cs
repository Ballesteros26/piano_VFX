using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001AF RID: 431
	internal sealed class StepBackAction : StepAction
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x00022A5E File Offset: 0x00020C5E
		public StepBackAction(ITimeSpan step)
			: base(step)
		{
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00022A68 File Offset: 0x00020C68
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			TempoMap tempoMap = context.TempoMap;
			context.SaveTime(time);
			return new PatternActionResult(new long?(Math.Max(TimeConverter.ConvertFrom(((MidiTimeSpan)time).Subtract(base.Step, TimeSpanMode.TimeLength), tempoMap), 0L)));
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00022ABA File Offset: 0x00020CBA
		public override PatternAction Clone()
		{
			return new StepBackAction(base.Step.Clone());
		}
	}
}
