using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001AE RID: 430
	internal abstract class StepAction : PatternAction
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x00022A47 File Offset: 0x00020C47
		public StepAction(ITimeSpan step)
		{
			this.Step = step;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00022A56 File Offset: 0x00020C56
		public ITimeSpan Step { get; }
	}
}
