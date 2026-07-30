using System;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Standards;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001AC RID: 428
	internal sealed class SetGeneralMidiProgramAction : PatternAction
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x00022969 File Offset: 0x00020B69
		public SetGeneralMidiProgramAction(GeneralMidiProgram program)
		{
			this.Program = program;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00022978 File Offset: 0x00020B78
		public GeneralMidiProgram Program { get; }

		// Token: 0x06000A46 RID: 2630 RVA: 0x00022980 File Offset: 0x00020B80
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			TimedEvent timedEvent = new TimedEvent(this.Program.GetProgramEvent(context.Channel), time);
			return new PatternActionResult(new long?(time), new TimedEvent[] { timedEvent });
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000229C8 File Offset: 0x00020BC8
		public override PatternAction Clone()
		{
			return new SetGeneralMidiProgramAction(this.Program);
		}
	}
}
