using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000AC RID: 172
	internal class VisualTreeStyleUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x000134AD File Offset: 0x000116AD
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeStyleUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000134B4 File Offset: 0x000116B4
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.StyleSheet) != VersionChangeType.StyleSheet;
			if (!flag)
			{
				this.m_Version += 1U;
				bool isApplyingStyles = this.m_IsApplyingStyles;
				if (isApplyingStyles)
				{
					this.m_ApplyStyleUpdateList.Add(ve);
				}
				else
				{
					this.m_StyleContextHierarchyTraversal.AddChangedElement(ve);
				}
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001350C File Offset: 0x0001170C
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				this.ApplyStyles();
				this.m_StyleContextHierarchyTraversal.Clear();
				foreach (VisualElement visualElement in this.m_ApplyStyleUpdateList)
				{
					this.m_StyleContextHierarchyTraversal.AddChangedElement(visualElement);
				}
				this.m_ApplyStyleUpdateList.Clear();
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000135AC File Offset: 0x000117AC
		private void ApplyStyles()
		{
			Debug.Assert(base.visualTree.panel != null);
			this.m_IsApplyingStyles = true;
			this.m_StyleContextHierarchyTraversal.PrepareTraversal(base.panel.scaledPixelsPerPoint);
			this.m_StyleContextHierarchyTraversal.Traverse(base.visualTree);
			this.m_IsApplyingStyles = false;
		}

		// Token: 0x0400021B RID: 539
		private HashSet<VisualElement> m_ApplyStyleUpdateList = new HashSet<VisualElement>();

		// Token: 0x0400021C RID: 540
		private bool m_IsApplyingStyles = false;

		// Token: 0x0400021D RID: 541
		private uint m_Version = 0U;

		// Token: 0x0400021E RID: 542
		private uint m_LastVersion = 0U;

		// Token: 0x0400021F RID: 543
		private VisualTreeStyleUpdaterTraversal m_StyleContextHierarchyTraversal = new VisualTreeStyleUpdaterTraversal();

		// Token: 0x04000220 RID: 544
		private static readonly string s_Description = "Update Style";

		// Token: 0x04000221 RID: 545
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeStyleUpdater.s_Description);
	}
}
