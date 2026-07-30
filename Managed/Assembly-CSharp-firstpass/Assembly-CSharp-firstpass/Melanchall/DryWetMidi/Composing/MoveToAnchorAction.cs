using System;
using System.Collections.Generic;
using System.Linq;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001A9 RID: 425
	internal sealed class MoveToAnchorAction : PatternAction
	{
		// Token: 0x06000A32 RID: 2610 RVA: 0x00022777 File Offset: 0x00020977
		public MoveToAnchorAction(AnchorPosition position)
			: this(null, position)
		{
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00022781 File Offset: 0x00020981
		public MoveToAnchorAction(object anchor, AnchorPosition position)
			: this(anchor, position, -1)
		{
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002278C File Offset: 0x0002098C
		public MoveToAnchorAction(AnchorPosition position, int index)
			: this(null, position, index)
		{
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00022797 File Offset: 0x00020997
		public MoveToAnchorAction(object anchor, AnchorPosition position, int index)
		{
			this.Anchor = anchor;
			this.AnchorPosition = position;
			this.Index = index;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x000227B4 File Offset: 0x000209B4
		public object Anchor { get; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000227BC File Offset: 0x000209BC
		public AnchorPosition AnchorPosition { get; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x000227C4 File Offset: 0x000209C4
		public int Index { get; }

		// Token: 0x06000A39 RID: 2617 RVA: 0x000227CC File Offset: 0x000209CC
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			IReadOnlyList<long> anchorTimes = context.GetAnchorTimes(this.Anchor);
			long num = 0L;
			switch (this.AnchorPosition)
			{
			case AnchorPosition.First:
				num = anchorTimes.First<long>();
				break;
			case AnchorPosition.Last:
				num = anchorTimes.Last<long>();
				break;
			case AnchorPosition.Nth:
				num = anchorTimes[this.Index];
				break;
			}
			return new PatternActionResult(new long?(num));
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002283C File Offset: 0x00020A3C
		public override PatternAction Clone()
		{
			return new MoveToAnchorAction(this.Anchor, this.AnchorPosition, this.Index);
		}
	}
}
