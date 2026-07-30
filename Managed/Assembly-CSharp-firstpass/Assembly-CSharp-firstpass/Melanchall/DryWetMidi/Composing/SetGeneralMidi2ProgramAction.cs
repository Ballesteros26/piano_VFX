using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Standards;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001AB RID: 427
	internal sealed class SetGeneralMidi2ProgramAction : PatternAction
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x000228E6 File Offset: 0x00020AE6
		public SetGeneralMidi2ProgramAction(GeneralMidi2Program program)
		{
			this.Program = program;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x000228F5 File Offset: 0x00020AF5
		public GeneralMidi2Program Program { get; }

		// Token: 0x06000A42 RID: 2626 RVA: 0x00022900 File Offset: 0x00020B00
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			IEnumerable<TimedEvent> enumerable = from e in this.Program.GetProgramEvents(context.Channel)
				select new TimedEvent(e, time);
			return new PatternActionResult(new long?(time), enumerable);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002295C File Offset: 0x00020B5C
		public override PatternAction Clone()
		{
			return new SetGeneralMidi2ProgramAction(this.Program);
		}
	}
}
