using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B6 RID: 182
	internal class VisualTreeViewDataUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x000142EB File Offset: 0x000124EB
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeViewDataUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000142F4 File Offset: 0x000124F4
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.ViewData) != VersionChangeType.ViewData;
			if (!flag)
			{
				this.m_Version += 1U;
				this.m_UpdateList.Add(ve);
				this.PropagateToParents(ve);
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00014334 File Offset: 0x00012534
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				int num = 0;
				while (this.m_LastVersion != this.m_Version)
				{
					this.m_LastVersion = this.m_Version;
					this.ValidateViewDataOnSubTree(base.visualTree, true);
					num++;
					bool flag2 = num > 5;
					if (flag2)
					{
						Debug.LogError("UIElements: Too many children recursively added that rely on persistent view data: " + base.visualTree);
						break;
					}
				}
				this.m_UpdateList.Clear();
				this.m_ParentList.Clear();
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000143C8 File Offset: 0x000125C8
		private void ValidateViewDataOnSubTree(VisualElement ve, bool enablePersistence)
		{
			enablePersistence = ve.IsViewDataPersitenceSupportedOnChildren(enablePersistence);
			bool flag = this.m_UpdateList.Contains(ve);
			if (flag)
			{
				this.m_UpdateList.Remove(ve);
				ve.OnViewDataReady(enablePersistence);
			}
			bool flag2 = this.m_ParentList.Contains(ve);
			if (flag2)
			{
				this.m_ParentList.Remove(ve);
				int childCount = ve.hierarchy.childCount;
				for (int i = 0; i < childCount; i++)
				{
					this.ValidateViewDataOnSubTree(ve.hierarchy[i], enablePersistence);
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00014464 File Offset: 0x00012664
		private void PropagateToParents(VisualElement ve)
		{
			for (VisualElement visualElement = ve.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = !this.m_ParentList.Add(visualElement);
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x04000241 RID: 577
		private HashSet<VisualElement> m_UpdateList = new HashSet<VisualElement>();

		// Token: 0x04000242 RID: 578
		private HashSet<VisualElement> m_ParentList = new HashSet<VisualElement>();

		// Token: 0x04000243 RID: 579
		private const int kMaxValidatePersistentDataCount = 5;

		// Token: 0x04000244 RID: 580
		private uint m_Version = 0U;

		// Token: 0x04000245 RID: 581
		private uint m_LastVersion = 0U;

		// Token: 0x04000246 RID: 582
		private static readonly string s_Description = "Update ViewData";

		// Token: 0x04000247 RID: 583
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeViewDataUpdater.s_Description);
	}
}
