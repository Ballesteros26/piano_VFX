using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000DF RID: 223
	public class PopupWindow : TextElement
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x0001A704 File Offset: 0x00018904
		public PopupWindow()
		{
			base.AddToClassList(PopupWindow.ussClassName);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.AddToClassList(PopupWindow.contentUssClassName);
			base.hierarchy.Add(this.m_ContentContainer);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001A764 File Offset: 0x00018964
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x040002F5 RID: 757
		private VisualElement m_ContentContainer;

		// Token: 0x040002F6 RID: 758
		public new static readonly string ussClassName = "unity-popup-window";

		// Token: 0x040002F7 RID: 759
		public static readonly string contentUssClassName = PopupWindow.ussClassName + "__content-container";

		// Token: 0x020000E0 RID: 224
		public new class UxmlFactory : UxmlFactory<PopupWindow, PopupWindow.UxmlTraits>
		{
		}

		// Token: 0x020000E1 RID: 225
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x17000177 RID: 375
			// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001A7A8 File Offset: 0x000189A8
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}
		}
	}
}
