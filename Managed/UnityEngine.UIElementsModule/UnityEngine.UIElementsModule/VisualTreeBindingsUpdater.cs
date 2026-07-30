using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010D RID: 269
	internal class VisualTreeBindingsUpdater : BaseVisualTreeHierarchyTrackerUpdater
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x000212F3 File Offset: 0x0001F4F3
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeBindingsUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000212FC File Offset: 0x0001F4FC
		private IBinding GetUpdaterFromElement(VisualElement ve)
		{
			IBindable bindable = ve as IBindable;
			return (bindable != null) ? bindable.binding : null;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00021320 File Offset: 0x0001F520
		private void StartTracking(VisualElement ve)
		{
			this.m_ElementsToAdd.Add(ve);
			this.m_ElementsToRemove.Remove(ve);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002133D File Offset: 0x0001F53D
		private void StopTracking(VisualElement ve)
		{
			this.m_ElementsToRemove.Add(ve);
			this.m_ElementsToAdd.Remove(ve);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0002135C File Offset: 0x0001F55C
		private void StartTrackingRecursive(VisualElement ve)
		{
			IBinding updaterFromElement = this.GetUpdaterFromElement(ve);
			bool flag = updaterFromElement != null;
			if (flag)
			{
				this.StartTracking(ve);
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				this.StartTrackingRecursive(visualElement);
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000213C8 File Offset: 0x0001F5C8
		private void StopTrackingRecursive(VisualElement ve)
		{
			this.StopTracking(ve);
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				this.StopTrackingRecursive(visualElement);
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002141C File Offset: 0x0001F61C
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			base.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & VersionChangeType.Bindings) == VersionChangeType.Bindings;
			if (flag)
			{
				bool flag2 = this.GetUpdaterFromElement(ve) != null;
				if (flag2)
				{
					this.StartTracking(ve);
				}
				else
				{
					this.StopTracking(ve);
				}
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00021464 File Offset: 0x0001F664
		protected override void OnHierarchyChange(VisualElement ve, HierarchyChangeType type)
		{
			if (type != HierarchyChangeType.Add)
			{
				if (type == HierarchyChangeType.Remove)
				{
					this.StopTrackingRecursive(ve);
				}
			}
			else
			{
				this.StartTrackingRecursive(ve);
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00021498 File Offset: 0x0001F698
		private static long CurrentTime()
		{
			return Panel.TimeSinceStartupMs();
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000214B0 File Offset: 0x0001F6B0
		public void PerformTrackingOperations()
		{
			foreach (VisualElement visualElement in this.m_ElementsToAdd)
			{
				IBinding updaterFromElement = this.GetUpdaterFromElement(visualElement);
				bool flag = updaterFromElement != null;
				if (flag)
				{
					this.m_ElementsWithBindings.Add(visualElement);
				}
			}
			this.m_ElementsToAdd.Clear();
			foreach (VisualElement visualElement2 in this.m_ElementsToRemove)
			{
				this.m_ElementsWithBindings.Remove(visualElement2);
			}
			this.m_ElementsToRemove.Clear();
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00021588 File Offset: 0x0001F788
		public override void Update()
		{
			base.Update();
			this.PerformTrackingOperations();
			bool flag = this.m_ElementsWithBindings.Count > 0;
			if (flag)
			{
				long num = VisualTreeBindingsUpdater.CurrentTime();
				bool flag2 = this.m_LastUpdateTime + 100L < num;
				if (flag2)
				{
					this.UpdateBindings();
					this.m_LastUpdateTime = num;
				}
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000215E0 File Offset: 0x0001F7E0
		private void UpdateBindings()
		{
			foreach (VisualElement visualElement in this.m_ElementsWithBindings)
			{
				IBinding updaterFromElement = this.GetUpdaterFromElement(visualElement);
				bool flag = updaterFromElement == null || visualElement.elementPanel != base.panel;
				if (flag)
				{
					if (updaterFromElement != null)
					{
						updaterFromElement.Release();
					}
					this.StopTracking(visualElement);
				}
				else
				{
					this.updatedBindings.Add(updaterFromElement);
				}
			}
			foreach (IBinding binding in this.updatedBindings)
			{
				binding.PreUpdate();
			}
			foreach (IBinding binding2 in this.updatedBindings)
			{
				binding2.Update();
			}
			this.updatedBindings.Clear();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002171C File Offset: 0x0001F91C
		internal void PollElementsWithBindings(Action<VisualElement, IBinding> callback)
		{
			this.PerformTrackingOperations();
			bool flag = this.m_ElementsWithBindings.Count > 0;
			if (flag)
			{
				foreach (VisualElement visualElement in this.m_ElementsWithBindings)
				{
					IBinding updaterFromElement = this.GetUpdaterFromElement(visualElement);
					bool flag2 = updaterFromElement == null || visualElement.elementPanel != base.panel;
					if (flag2)
					{
						if (updaterFromElement != null)
						{
							updaterFromElement.Release();
						}
						this.StopTracking(visualElement);
					}
					else
					{
						callback.Invoke(visualElement, updaterFromElement);
					}
				}
			}
		}

		// Token: 0x040003A8 RID: 936
		private static readonly string s_Description = "Update Bindings";

		// Token: 0x040003A9 RID: 937
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeBindingsUpdater.s_Description);

		// Token: 0x040003AA RID: 938
		private readonly HashSet<VisualElement> m_ElementsWithBindings = new HashSet<VisualElement>();

		// Token: 0x040003AB RID: 939
		private readonly HashSet<VisualElement> m_ElementsToAdd = new HashSet<VisualElement>();

		// Token: 0x040003AC RID: 940
		private readonly HashSet<VisualElement> m_ElementsToRemove = new HashSet<VisualElement>();

		// Token: 0x040003AD RID: 941
		private const int kMinUpdateDelay = 100;

		// Token: 0x040003AE RID: 942
		private long m_LastUpdateTime = 0L;

		// Token: 0x040003AF RID: 943
		private static ProfilerMarker s_MarkerUpdate = new ProfilerMarker("Bindings.Update");

		// Token: 0x040003B0 RID: 944
		private static ProfilerMarker s_MarkerPoll = new ProfilerMarker("Bindings.PollElementsWithBindings");

		// Token: 0x040003B1 RID: 945
		private List<IBinding> updatedBindings = new List<IBinding>();
	}
}
