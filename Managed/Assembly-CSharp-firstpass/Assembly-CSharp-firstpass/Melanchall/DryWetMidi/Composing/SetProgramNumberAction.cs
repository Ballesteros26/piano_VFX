using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001AD RID: 429
	internal sealed class SetProgramNumberAction : PatternAction
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x000229D5 File Offset: 0x00020BD5
		public SetProgramNumberAction(SevenBitNumber programNumber)
		{
			this.ProgramNumber = programNumber;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x000229E4 File Offset: 0x00020BE4
		public SevenBitNumber ProgramNumber { get; }

		// Token: 0x06000A4A RID: 2634 RVA: 0x000229EC File Offset: 0x00020BEC
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			TimedEvent timedEvent = new TimedEvent(new ProgramChangeEvent(this.ProgramNumber)
			{
				Channel = context.Channel
			}, time);
			return new PatternActionResult(new long?(time), new TimedEvent[] { timedEvent });
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00022A3A File Offset: 0x00020C3A
		public override PatternAction Clone()
		{
			return new SetProgramNumberAction(this.ProgramNumber);
		}
	}
}
