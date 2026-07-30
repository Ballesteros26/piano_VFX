using System;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B0 RID: 176
	internal class VisualTreeTransformClipUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00013D7A File Offset: 0x00011F7A
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeTransformClipUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00013D84 File Offset: 0x00011F84
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size)) == (VersionChangeType)0;
			if (!flag)
			{
				bool flag2 = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool flag3 = (versionChangeType & (VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size)) > (VersionChangeType)0;
				flag2 = flag2 && !ve.isWorldTransformDirty;
				flag3 = flag3 && !ve.isWorldClipDirty;
				bool flag4 = flag2 || flag3;
				if (flag4)
				{
					VisualTreeTransformClipUpdater.DirtyHierarchy(ve, flag2, flag3);
				}
				VisualTreeTransformClipUpdater.DirtyBoundingBoxHierarchy(ve);
				this.m_Version += 1U;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00013E00 File Offset: 0x00012000
		private static void DirtyHierarchy(VisualElement ve, bool mustDirtyWorldTransform, bool mustDirtyWorldClip)
		{
			if (mustDirtyWorldTransform)
			{
				ve.isWorldTransformDirty = true;
				ve.isWorldBoundingBoxDirty = true;
			}
			if (mustDirtyWorldClip)
			{
				ve.isWorldClipDirty = true;
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = (mustDirtyWorldTransform && !visualElement.isWorldTransformDirty) || (mustDirtyWorldClip && !visualElement.isWorldClipDirty);
				if (flag)
				{
					VisualTreeTransformClipUpdater.DirtyHierarchy(visualElement, mustDirtyWorldTransform, mustDirtyWorldClip);
				}
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00013E9C File Offset: 0x0001209C
		private static void DirtyBoundingBoxHierarchy(VisualElement ve)
		{
			ve.isBoundingBoxDirty = true;
			ve.isWorldBoundingBoxDirty = true;
			VisualElement visualElement = ve.hierarchy.parent;
			while (visualElement != null && !visualElement.isBoundingBoxDirty)
			{
				visualElement.isBoundingBoxDirty = true;
				visualElement.isWorldBoundingBoxDirty = true;
				visualElement = visualElement.hierarchy.parent;
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00013EFC File Offset: 0x000120FC
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				base.panel.UpdateElementUnderPointers();
				base.panel.visualTree.UpdateBoundingBox();
			}
		}

		// Token: 0x0400022F RID: 559
		private uint m_Version = 0U;

		// Token: 0x04000230 RID: 560
		private uint m_LastVersion = 0U;

		// Token: 0x04000231 RID: 561
		private static readonly string s_Description = "Update Transform";

		// Token: 0x04000232 RID: 562
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeTransformClipUpdater.s_Description);
	}
}
