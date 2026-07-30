using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000027 RID: 39
	public static class LayoutUtility
	{
		// Token: 0x060002AF RID: 687 RVA: 0x0000E733 File Offset: 0x0000C933
		public static float GetMinSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetMinHeight(rect);
			}
			return LayoutUtility.GetMinWidth(rect);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000E745 File Offset: 0x0000C945
		public static float GetPreferredSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetPreferredHeight(rect);
			}
			return LayoutUtility.GetPreferredWidth(rect);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000E757 File Offset: 0x0000C957
		public static float GetFlexibleSize(RectTransform rect, int axis)
		{
			if (axis != 0)
			{
				return LayoutUtility.GetFlexibleHeight(rect);
			}
			return LayoutUtility.GetFlexibleWidth(rect);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000E769 File Offset: 0x0000C969
		public static float GetMinWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000E798 File Offset: 0x0000C998
		public static float GetPreferredWidth(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredWidth, 0f));
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000E7FE File Offset: 0x0000C9FE
		public static float GetFlexibleWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleWidth, 0f);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000E82A File Offset: 0x0000CA2A
		public static float GetMinHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000E858 File Offset: 0x0000CA58
		public static float GetPreferredHeight(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredHeight, 0f));
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000E8BE File Offset: 0x0000CABE
		public static float GetFlexibleHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleHeight, 0f);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue)
		{
			ILayoutElement layoutElement;
			return LayoutUtility.GetLayoutProperty(rect, property, defaultValue, out layoutElement);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000E904 File Offset: 0x0000CB04
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue, out ILayoutElement source)
		{
			source = null;
			if (rect == null)
			{
				return 0f;
			}
			float num = defaultValue;
			int num2 = int.MinValue;
			List<Component> list = ListPool<Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			for (int i = 0; i < list.Count; i++)
			{
				ILayoutElement layoutElement = list[i] as ILayoutElement;
				if (!(layoutElement is Behaviour) || ((Behaviour)layoutElement).isActiveAndEnabled)
				{
					int layoutPriority = layoutElement.layoutPriority;
					if (layoutPriority >= num2)
					{
						float num3 = property(layoutElement);
						if (num3 >= 0f)
						{
							if (layoutPriority > num2)
							{
								num = num3;
								num2 = layoutPriority;
								source = layoutElement;
							}
							else if (num3 > num)
							{
								num = num3;
								source = layoutElement;
							}
						}
					}
				}
			}
			ListPool<Component>.Release(list);
			return num;
		}
	}
}
