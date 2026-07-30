using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001AA RID: 426
	internal sealed class MoveToTimeAction : PatternAction
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x00022855 File Offset: 0x00020A55
		public MoveToTimeAction()
			: this(null)
		{
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002285E File Offset: 0x00020A5E
		public MoveToTimeAction(ITimeSpan time)
		{
			this.Time = time;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002286D File Offset: 0x00020A6D
		public ITimeSpan Time { get; }

		// Token: 0x06000A3E RID: 2622 RVA: 0x00022878 File Offset: 0x00020A78
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			if (this.Time != null)
			{
				context.SaveTime(time);
			}
			return new PatternActionResult((this.Time != null) ? new long?(TimeConverter.ConvertFrom(this.Time, context.TempoMap)) : context.RestoreTime());
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000228CD File Offset: 0x00020ACD
		public override PatternAction Clone()
		{
			ITimeSpan time = this.Time;
			return new MoveToTimeAction((time != null) ? time.Clone() : null);
		}
	}
}
