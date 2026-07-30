using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000188 RID: 392
	public class CustomStyleResolvedEvent : EventBase<CustomStyleResolvedEvent>
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00028734 File Offset: 0x00026934
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement visualElement = base.target as VisualElement;
				return (visualElement != null) ? visualElement.customStyle : null;
			}
		}
	}
}
