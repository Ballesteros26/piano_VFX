using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000044 RID: 68
	[AddComponentMenu("UI/Effects/Outline", 15)]
	public class Outline : Shadow
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00015B0C File Offset: 0x00013D0C
		protected Outline()
		{
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00015B14 File Offset: 0x00013D14
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = ListPool<UIVertex>.Get();
			vh.GetUIVertexStream(list);
			int num = list.Count * 5;
			if (list.Capacity < num)
			{
				list.Capacity = num;
			}
			int num2 = 0;
			int count = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, num2, list.Count, base.effectDistance.x, base.effectDistance.y);
			num2 = count;
			int count2 = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, num2, list.Count, base.effectDistance.x, -base.effectDistance.y);
			num2 = count2;
			int count3 = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, num2, list.Count, -base.effectDistance.x, base.effectDistance.y);
			num2 = count3;
			int count4 = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, num2, list.Count, -base.effectDistance.x, -base.effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			ListPool<UIVertex>.Release(list);
		}
	}
}
