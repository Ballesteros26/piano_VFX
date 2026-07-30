using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000257 RID: 599
	internal abstract class HierarchyTraversal
	{
		// Token: 0x060011BC RID: 4540 RVA: 0x0004DBF0 File Offset: 0x0004BDF0
		public virtual void Traverse(VisualElement element)
		{
			this.TraverseRecursive(element, 0);
		}

		// Token: 0x060011BD RID: 4541
		public abstract void TraverseRecursive(VisualElement element, int depth);

		// Token: 0x060011BE RID: 4542 RVA: 0x0004DBFC File Offset: 0x0004BDFC
		protected void Recurse(VisualElement element, int depth)
		{
			int i = 0;
			while (i < element.hierarchy.childCount)
			{
				VisualElement visualElement = element.hierarchy[i];
				this.TraverseRecursive(visualElement, depth + 1);
				bool flag = visualElement.hierarchy.parent != element;
				if (!flag)
				{
					i++;
				}
			}
		}
	}
}
