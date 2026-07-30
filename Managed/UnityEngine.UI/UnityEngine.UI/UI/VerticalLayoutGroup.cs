using System;

namespace UnityEngine.UI
{
	// Token: 0x02000028 RID: 40
	[AddComponentMenu("Layout/Vertical Layout Group", 151)]
	public class VerticalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000D528 File Offset: 0x0000B728
		protected VerticalLayoutGroup()
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000E9BD File Offset: 0x0000CBBD
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, true);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000E9CD File Offset: 0x0000CBCD
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, true);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000E9D7 File Offset: 0x0000CBD7
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, true);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000E9E1 File Offset: 0x0000CBE1
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, true);
		}
	}
}
