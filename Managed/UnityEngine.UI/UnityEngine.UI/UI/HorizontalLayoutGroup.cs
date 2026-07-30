using System;

namespace UnityEngine.UI
{
	// Token: 0x0200001D RID: 29
	[AddComponentMenu("Layout/Horizontal Layout Group", 150)]
	public class HorizontalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0000D528 File Offset: 0x0000B728
		protected HorizontalLayoutGroup()
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D530 File Offset: 0x0000B730
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, false);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D540 File Offset: 0x0000B740
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, false);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D54A File Offset: 0x0000B74A
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, false);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D554 File Offset: 0x0000B754
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, false);
		}
	}
}
