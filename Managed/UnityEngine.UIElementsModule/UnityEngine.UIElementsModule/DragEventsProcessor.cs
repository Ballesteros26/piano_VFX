using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000110 RID: 272
	internal abstract class DragEventsProcessor
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x0002195C File Offset: 0x0001FB5C
		internal DragEventsProcessor(VisualElement target)
		{
			this.m_Target = target;
			this.m_Target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DragUpdatedEvent>(new EventCallback<DragUpdatedEvent>(this.OnDragUpdate), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DragPerformEvent>(new EventCallback<DragPerformEvent>(this.OnDragPerformEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DragExitedEvent>(new EventCallback<DragExitedEvent>(this.OnDragExitedEvent), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00021A40 File Offset: 0x0001FC40
		private void UnregisterCallbacksFromTarget(DetachFromPanelEvent evt)
		{
			this.m_Target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<DragUpdatedEvent>(new EventCallback<DragUpdatedEvent>(this.OnDragUpdate), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<DragPerformEvent>(new EventCallback<DragPerformEvent>(this.OnDragPerformEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<DragExitedEvent>(new EventCallback<DragExitedEvent>(this.OnDragExitedEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000835 RID: 2101
		protected abstract bool CanStartDrag(Vector3 pointerPosition);

		// Token: 0x06000836 RID: 2102
		protected abstract StartDragArgs StartDrag(Vector3 pointerPosition);

		// Token: 0x06000837 RID: 2103
		protected abstract DragVisualMode UpdateDrag(Vector3 pointerPosition);

		// Token: 0x06000838 RID: 2104
		protected abstract void OnDrop(Vector3 pointerPosition);

		// Token: 0x06000839 RID: 2105
		protected abstract void ClearDragAndDropUI();

		// Token: 0x0600083A RID: 2106 RVA: 0x00021B18 File Offset: 0x0001FD18
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			bool flag = evt.button != 0;
			if (flag)
			{
				this.m_CanStartDrag = false;
			}
			else
			{
				bool flag2 = this.CanStartDrag(evt.position);
				if (flag2)
				{
					this.m_CanStartDrag = true;
					this.m_Start = evt.position;
				}
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00021B62 File Offset: 0x0001FD62
		private void OnPointerUpEvent(PointerUpEvent evt)
		{
			this.m_CanStartDrag = false;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00021B6C File Offset: 0x0001FD6C
		private void OnPointerLeaveEvent(PointerLeaveEvent evt)
		{
			bool flag = evt.target == this.m_Target;
			if (flag)
			{
				this.ClearDragAndDropUI();
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00021B93 File Offset: 0x0001FD93
		private void OnDragExitedEvent(DragExitedEvent evt)
		{
			this.ClearDragAndDropUI();
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00021B9D File Offset: 0x0001FD9D
		private void OnDragPerformEvent(DragPerformEvent evt)
		{
			this.m_CanStartDrag = false;
			this.OnDrop(evt.mousePosition);
			this.ClearDragAndDropUI();
			DragAndDropUtility.dragAndDrop.AcceptDrag();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00021BCC File Offset: 0x0001FDCC
		private void OnDragUpdate(DragUpdatedEvent evt)
		{
			DragVisualMode dragVisualMode = this.UpdateDrag(evt.mousePosition);
			DragAndDropUtility.dragAndDrop.SetVisualMode(dragVisualMode);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00021BF8 File Offset: 0x0001FDF8
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool flag = !this.m_CanStartDrag;
			if (!flag)
			{
				bool flag2 = Mathf.Abs(this.m_Start.x - evt.position.x) > 5f || Mathf.Abs(this.m_Start.y - evt.position.y) > 5f;
				if (flag2)
				{
					StartDragArgs startDragArgs = this.StartDrag(evt.position);
					DragAndDropUtility.dragAndDrop.StartDrag(startDragArgs);
					this.m_CanStartDrag = false;
				}
			}
		}

		// Token: 0x040003B5 RID: 949
		private bool m_CanStartDrag;

		// Token: 0x040003B6 RID: 950
		private Vector3 m_Start;

		// Token: 0x040003B7 RID: 951
		internal readonly VisualElement m_Target;

		// Token: 0x040003B8 RID: 952
		private const int k_DistanceToActivation = 5;
	}
}
