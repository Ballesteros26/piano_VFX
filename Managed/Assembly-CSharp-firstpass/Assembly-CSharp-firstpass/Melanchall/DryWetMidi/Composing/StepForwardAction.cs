using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001B0 RID: 432
	internal sealed class StepForwardAction : StepAction
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x00022A5E File Offset: 0x00020C5E
		public StepForwardAction(ITimeSpan step)
			: base(step)
		{
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00022ACC File Offset: 0x00020CCC
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			context.SaveTime(time);
			return new PatternActionResult(new long?(time + LengthConverter.ConvertFrom(base.Step, time, context.TempoMap)));
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00022B01 File Offset: 0x00020D01
		public override PatternAction Clone()
		{
			return new StepForwardAction(base.Step.Clone());
		}
	}
}
