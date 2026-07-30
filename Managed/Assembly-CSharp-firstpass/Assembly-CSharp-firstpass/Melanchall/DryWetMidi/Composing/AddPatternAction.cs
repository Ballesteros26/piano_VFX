using System;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001A6 RID: 422
	internal sealed class AddPatternAction : PatternAction
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x0002269C File Offset: 0x0002089C
		public AddPatternAction(Pattern pattern)
		{
			this.Pattern = pattern;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000226AB File Offset: 0x000208AB
		public Pattern Pattern { get; }

		// Token: 0x06000A2C RID: 2604 RVA: 0x000226B4 File Offset: 0x000208B4
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			context.SaveTime(time);
			PatternContext patternContext = new PatternContext(context.TempoMap, context.Channel);
			return this.Pattern.InvokeActions(time, patternContext);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000226E7 File Offset: 0x000208E7
		public override PatternAction Clone()
		{
			return new AddPatternAction(this.Pattern.Clone());
		}
	}
}
