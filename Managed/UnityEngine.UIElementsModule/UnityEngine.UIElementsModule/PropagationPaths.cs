using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000186 RID: 390
	internal class PropagationPaths
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x000284E1 File Offset: 0x000266E1
		public PropagationPaths()
		{
			this.trickleDownPath = new List<VisualElement>(16);
			this.targetElements = new List<VisualElement>(4);
			this.bubbleUpPath = new List<VisualElement>(16);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00028511 File Offset: 0x00026711
		public PropagationPaths(PropagationPaths paths)
		{
			this.trickleDownPath = new List<VisualElement>(paths.trickleDownPath);
			this.targetElements = new List<VisualElement>(paths.targetElements);
			this.bubbleUpPath = new List<VisualElement>(paths.bubbleUpPath);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00028550 File Offset: 0x00026750
		internal static PropagationPaths Copy(PropagationPaths paths)
		{
			PropagationPaths propagationPaths = PropagationPaths.s_Pool.Get();
			propagationPaths.trickleDownPath.AddRange(paths.trickleDownPath);
			propagationPaths.targetElements.AddRange(paths.targetElements);
			propagationPaths.bubbleUpPath.AddRange(paths.bubbleUpPath);
			return propagationPaths;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000285A4 File Offset: 0x000267A4
		public static PropagationPaths Build(VisualElement elem, PropagationPaths.Type pathTypesRequested)
		{
			bool flag = elem == null || pathTypesRequested == PropagationPaths.Type.None;
			PropagationPaths propagationPaths;
			if (flag)
			{
				propagationPaths = null;
			}
			else
			{
				PropagationPaths propagationPaths2 = PropagationPaths.s_Pool.Get();
				propagationPaths2.targetElements.Add(elem);
				while (elem.hierarchy.parent != null)
				{
					bool enabledInHierarchy = elem.hierarchy.parent.enabledInHierarchy;
					if (enabledInHierarchy)
					{
						bool isCompositeRoot = elem.hierarchy.parent.isCompositeRoot;
						if (isCompositeRoot)
						{
							propagationPaths2.targetElements.Add(elem.hierarchy.parent);
						}
						else
						{
							bool flag2 = (pathTypesRequested & PropagationPaths.Type.TrickleDown) == PropagationPaths.Type.TrickleDown && elem.hierarchy.parent.HasTrickleDownHandlers();
							if (flag2)
							{
								propagationPaths2.trickleDownPath.Add(elem.hierarchy.parent);
							}
							bool flag3 = (pathTypesRequested & PropagationPaths.Type.BubbleUp) == PropagationPaths.Type.BubbleUp && elem.hierarchy.parent.HasBubbleUpHandlers();
							if (flag3)
							{
								propagationPaths2.bubbleUpPath.Add(elem.hierarchy.parent);
							}
						}
					}
					elem = elem.hierarchy.parent;
				}
				propagationPaths = propagationPaths2;
			}
			return propagationPaths;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000286F2 File Offset: 0x000268F2
		public void Release()
		{
			this.bubbleUpPath.Clear();
			this.targetElements.Clear();
			this.trickleDownPath.Clear();
			PropagationPaths.s_Pool.Release(this);
		}

		// Token: 0x0400046A RID: 1130
		private static readonly ObjectPool<PropagationPaths> s_Pool = new ObjectPool<PropagationPaths>(100);

		// Token: 0x0400046B RID: 1131
		public readonly List<VisualElement> trickleDownPath;

		// Token: 0x0400046C RID: 1132
		public readonly List<VisualElement> targetElements;

		// Token: 0x0400046D RID: 1133
		public readonly List<VisualElement> bubbleUpPath;

		// Token: 0x0400046E RID: 1134
		private const int k_DefaultPropagationDepth = 16;

		// Token: 0x0400046F RID: 1135
		private const int k_DefaultTargetCount = 4;

		// Token: 0x02000187 RID: 391
		[Flags]
		public enum Type
		{
			// Token: 0x04000471 RID: 1137
			None = 0,
			// Token: 0x04000472 RID: 1138
			TrickleDown = 1,
			// Token: 0x04000473 RID: 1139
			BubbleUp = 2
		}
	}
}
