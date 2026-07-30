using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200009D RID: 157
	public class VisualElementFocusChangeDirection : FocusChangeDirection
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x000120BB File Offset: 0x000102BB
		public static FocusChangeDirection left
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Left;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x000120C2 File Offset: 0x000102C2
		public static FocusChangeDirection right
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000120CC File Offset: 0x000102CC
		protected new static VisualElementFocusChangeDirection lastValue
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000120E3 File Offset: 0x000102E3
		protected VisualElementFocusChangeDirection(int value)
			: base(value)
		{
		}

		// Token: 0x040001F5 RID: 501
		private static readonly VisualElementFocusChangeDirection s_Left = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 1);

		// Token: 0x040001F6 RID: 502
		private static readonly VisualElementFocusChangeDirection s_Right = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 2);
	}
}
