using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000139 RID: 313
	internal class ElementUnderPointer
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x00022F90 File Offset: 0x00021190
		internal VisualElement GetTopElementUnderPointer(int pointerId, out Vector2 pickPosition)
		{
			pickPosition = this.m_PickingPointerPositions[pointerId];
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00022FBC File Offset: 0x000211BC
		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00022FD8 File Offset: 0x000211D8
		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, Vector2 pointerPos)
		{
			Debug.Assert(pointerId >= 0);
			VisualElement visualElement = this.m_TopElementUnderPointer[pointerId];
			this.m_PickingPointerPositions[pointerId] = pointerPos;
			bool flag = newElementUnderPointer == visualElement;
			if (!flag)
			{
				this.m_PendingTopElementUnderPointer[pointerId] = newElementUnderPointer;
				this.m_TriggerPointerEvent[pointerId] = null;
				this.m_TriggerMouseEvent[pointerId] = null;
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00023030 File Offset: 0x00021230
		private Vector2 GetEventPointerPosition(EventBase triggerEvent)
		{
			IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
			bool flag = pointerEvent != null;
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(pointerEvent.position.x, pointerEvent.position.y);
			}
			else
			{
				IMouseEvent mouseEvent = triggerEvent as IMouseEvent;
				bool flag2 = mouseEvent != null;
				if (flag2)
				{
					vector = mouseEvent.mousePosition;
				}
				else
				{
					vector = new Vector2(float.MinValue, float.MinValue);
				}
			}
			return vector;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002309C File Offset: 0x0002129C
		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, EventBase triggerEvent)
		{
			int num = -1;
			bool flag = triggerEvent is IPointerEvent;
			if (flag)
			{
				num = ((IPointerEvent)triggerEvent).pointerId;
			}
			else
			{
				bool flag2 = triggerEvent is IMouseEvent;
				if (flag2)
				{
					num = PointerId.mousePointerId;
				}
			}
			Debug.Assert(num >= 0);
			this.m_PickingPointerPositions[num] = this.GetEventPointerPosition(triggerEvent);
			VisualElement visualElement = this.m_TopElementUnderPointer[num];
			bool flag3 = newElementUnderPointer == visualElement;
			if (!flag3)
			{
				this.m_PendingTopElementUnderPointer[num] = newElementUnderPointer;
				bool flag4 = this.m_TriggerPointerEvent[num] == null && triggerEvent is IPointerEvent;
				if (flag4)
				{
					this.m_TriggerPointerEvent[num] = triggerEvent as IPointerEvent;
				}
				bool flag5 = this.m_TriggerMouseEvent[num] == null && triggerEvent is IMouseEvent;
				if (flag5)
				{
					this.m_TriggerMouseEvent[num] = triggerEvent as IMouseEvent;
				}
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00023178 File Offset: 0x00021378
		internal void CommitElementUnderPointers(EventDispatcher dispatcher)
		{
			for (int i = 0; i < this.m_TopElementUnderPointer.Length; i++)
			{
				IPointerEvent pointerEvent = this.m_TriggerPointerEvent[i];
				VisualElement visualElement = this.m_TopElementUnderPointer[i];
				VisualElement visualElement2 = this.m_PendingTopElementUnderPointer[i];
				bool flag = visualElement2 == visualElement;
				if (flag)
				{
					bool flag2 = pointerEvent != null;
					if (flag2)
					{
						Vector3 position = pointerEvent.position;
						this.m_PickingPointerPositions[i] = new Vector2(position.x, position.y);
					}
					else
					{
						bool flag3 = this.m_TriggerMouseEvent[i] != null;
						if (flag3)
						{
							this.m_PickingPointerPositions[i] = this.m_TriggerMouseEvent[i].mousePosition;
						}
					}
				}
				else
				{
					this.m_TopElementUnderPointer[i] = visualElement2;
					bool flag4 = pointerEvent == null && this.m_TriggerMouseEvent[i] == null;
					if (flag4)
					{
						using (new EventDispatcherGate(dispatcher))
						{
							Vector2 pointerPosition = PointerDeviceState.GetPointerPosition(i);
							PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, pointerPosition, i);
							PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, pointerPosition, i);
							this.m_PickingPointerPositions[i] = pointerPosition;
							bool flag5 = i == PointerId.mousePointerId;
							if (flag5)
							{
								MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, null, pointerPosition);
								MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, null, pointerPosition);
							}
						}
					}
					bool flag6 = pointerEvent != null;
					if (flag6)
					{
						Vector3 position2 = pointerEvent.position;
						this.m_PickingPointerPositions[i] = new Vector2(position2.x, position2.y);
						EventBase eventBase = pointerEvent as EventBase;
						bool flag7 = eventBase != null && (eventBase.eventTypeId == EventBase<PointerMoveEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerCancelEvent>.TypeId());
						if (flag7)
						{
							using (new EventDispatcherGate(dispatcher))
							{
								PointerEventsHelper.SendOverOut(visualElement, visualElement2, pointerEvent, position2, i);
								PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, pointerEvent, position2, i);
							}
						}
					}
					this.m_TriggerPointerEvent[i] = null;
					IMouseEvent mouseEvent = this.m_TriggerMouseEvent[i];
					bool flag8 = mouseEvent != null;
					if (flag8)
					{
						Vector2 mousePosition = mouseEvent.mousePosition;
						this.m_PickingPointerPositions[i] = mousePosition;
						EventBase eventBase2 = mouseEvent as EventBase;
						bool flag9 = eventBase2 != null;
						if (flag9)
						{
							bool flag10 = eventBase2.eventTypeId == EventBase<MouseMoveEvent>.TypeId() || eventBase2.eventTypeId == EventBase<MouseDownEvent>.TypeId() || eventBase2.eventTypeId == EventBase<MouseUpEvent>.TypeId() || eventBase2.eventTypeId == EventBase<WheelEvent>.TypeId();
							if (flag10)
							{
								using (new EventDispatcherGate(dispatcher))
								{
									MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, mouseEvent, mousePosition);
									MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
								}
							}
							else
							{
								bool flag11 = eventBase2.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || eventBase2.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
								if (flag11)
								{
									using (new EventDispatcherGate(dispatcher))
									{
										PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, mousePosition, i);
										PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, mousePosition, i);
										bool flag12 = i == PointerId.mousePointerId;
										if (flag12)
										{
											MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, mouseEvent, mousePosition);
											MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
										}
									}
								}
								else
								{
									bool flag13 = eventBase2.eventTypeId == EventBase<DragUpdatedEvent>.TypeId() || eventBase2.eventTypeId == EventBase<DragExitedEvent>.TypeId();
									if (flag13)
									{
										using (new EventDispatcherGate(dispatcher))
										{
											PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, mousePosition, i);
											PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, mousePosition, i);
											MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, mouseEvent, mousePosition);
											MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
											MouseEventsHelper.SendEnterLeave<DragLeaveEvent, DragEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
										}
									}
								}
							}
						}
						this.m_TriggerMouseEvent[i] = null;
					}
				}
			}
		}

		// Token: 0x040003E2 RID: 994
		private VisualElement[] m_PendingTopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		// Token: 0x040003E3 RID: 995
		private VisualElement[] m_TopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		// Token: 0x040003E4 RID: 996
		private IPointerEvent[] m_TriggerPointerEvent = new IPointerEvent[PointerId.maxPointers];

		// Token: 0x040003E5 RID: 997
		private IMouseEvent[] m_TriggerMouseEvent = new IMouseEvent[PointerId.maxPointers];

		// Token: 0x040003E6 RID: 998
		private Vector2[] m_PickingPointerPositions = new Vector2[PointerId.maxPointers];
	}
}
