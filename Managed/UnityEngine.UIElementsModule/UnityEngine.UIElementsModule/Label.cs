using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D2 RID: 210
	public class Label : TextElement
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x0001750A File Offset: 0x0001570A
		public Label()
			: this(string.Empty)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00017519 File Offset: 0x00015719
		public Label(string text)
		{
			base.AddToClassList(Label.ussClassName);
			this.text = text;
		}

		// Token: 0x0400029D RID: 669
		public new static readonly string ussClassName = "unity-label";

		// Token: 0x020000D3 RID: 211
		public new class UxmlFactory : UxmlFactory<Label, Label.UxmlTraits>
		{
		}

		// Token: 0x020000D4 RID: 212
		public new class UxmlTraits : TextElement.UxmlTraits
		{
		}
	}
}
