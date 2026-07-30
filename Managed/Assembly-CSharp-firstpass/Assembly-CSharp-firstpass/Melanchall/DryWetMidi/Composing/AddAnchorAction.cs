using System;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001A3 RID: 419
	internal sealed class AddAnchorAction : PatternAction
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x00022460 File Offset: 0x00020660
		public AddAnchorAction()
			: this(null)
		{
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00022469 File Offset: 0x00020669
		public AddAnchorAction(object anchor)
		{
			this.Anchor = anchor;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00022478 File Offset: 0x00020678
		public object Anchor { get; }

		// Token: 0x06000A20 RID: 2592 RVA: 0x00022480 File Offset: 0x00020680
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State == PatternActionState.Enabled)
			{
				context.AnchorTime(this.Anchor, time);
			}
			return PatternActionResult.DoNothing;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002249C File Offset: 0x0002069C
		public override PatternAction Clone()
		{
			return new AddAnchorAction(this.Anchor);
		}
	}
}
