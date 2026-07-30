using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A9 RID: 169
	internal abstract class BaseVisualTreeHierarchyTrackerUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x06000503 RID: 1283
		protected abstract void OnHierarchyChange(VisualElement ve, HierarchyChangeType type);

		// Token: 0x06000504 RID: 1284 RVA: 0x00013250 File Offset: 0x00011450
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.Hierarchy) == VersionChangeType.Hierarchy;
			if (flag)
			{
				switch (this.m_State)
				{
				case BaseVisualTreeHierarchyTrackerUpdater.State.Waiting:
					this.ProcessNewChange(ve);
					break;
				case BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove:
					this.ProcessAddOrMove(ve);
					break;
				case BaseVisualTreeHierarchyTrackerUpdater.State.TrackingRemove:
					this.ProcessRemove(ve);
					break;
				}
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000132A4 File Offset: 0x000114A4
		public override void Update()
		{
			Debug.Assert(this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove || this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.Waiting);
			bool flag = this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Move);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			this.m_CurrentChangeElement = null;
			this.m_CurrentChangeParent = null;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00013300 File Offset: 0x00011500
		private void ProcessNewChange(VisualElement ve)
		{
			this.m_CurrentChangeElement = ve;
			this.m_CurrentChangeParent = ve.parent;
			bool flag = this.m_CurrentChangeParent == null && ve.panel != null;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Move);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.m_State = ((this.m_CurrentChangeParent == null) ? BaseVisualTreeHierarchyTrackerUpdater.State.TrackingRemove : BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove);
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00013368 File Offset: 0x00011568
		private void ProcessAddOrMove(VisualElement ve)
		{
			Debug.Assert(this.m_CurrentChangeParent != null);
			bool flag = this.m_CurrentChangeParent == ve;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Add);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Move);
				this.ProcessNewChange(ve);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000133C4 File Offset: 0x000115C4
		private void ProcessRemove(VisualElement ve)
		{
			this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.Remove);
			bool flag = ve.panel != null;
			if (flag)
			{
				this.m_CurrentChangeParent = null;
				this.m_CurrentChangeElement = null;
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.m_CurrentChangeElement = ve;
			}
		}

		// Token: 0x04000212 RID: 530
		private BaseVisualTreeHierarchyTrackerUpdater.State m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;

		// Token: 0x04000213 RID: 531
		private VisualElement m_CurrentChangeElement;

		// Token: 0x04000214 RID: 532
		private VisualElement m_CurrentChangeParent;

		// Token: 0x020000AA RID: 170
		private enum State
		{
			// Token: 0x04000216 RID: 534
			Waiting,
			// Token: 0x04000217 RID: 535
			TrackingAddOrMove,
			// Token: 0x04000218 RID: 536
			TrackingRemove
		}
	}
}
