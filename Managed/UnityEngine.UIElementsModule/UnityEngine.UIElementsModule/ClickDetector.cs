using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000008 RID: 8
	internal class ClickDetector
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000027E5 File Offset: 0x000009E5
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000027EC File Offset: 0x000009EC
		internal static int s_DoubleClickTime { get; set; } = -1;

		// Token: 0x0600002D RID: 45 RVA: 0x000027F4 File Offset: 0x000009F4
		public ClickDetector()
		{
			this.m_ClickStatus = new List<ClickDetector.ButtonClickStatus>(PointerId.maxPointers);
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				this.m_ClickStatus.Add(new ClickDetector.ButtonClickStatus());
			}
			bool flag = ClickDetector.s_DoubleClickTime == -1;
			if (flag)
			{
				ClickDetector.s_DoubleClickTime = Event.GetDoubleClickTime();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000285C File Offset: 0x00000A5C
		private void StartClickTracking(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				VisualElement visualElement = evt.target as VisualElement;
				bool flag2 = visualElement != buttonClickStatus.m_Target;
				if (flag2)
				{
					buttonClickStatus.Reset();
				}
				buttonClickStatus.m_Target = visualElement;
				bool flag3 = evt.timestamp - buttonClickStatus.m_LastPointerDownTime > (long)ClickDetector.s_DoubleClickTime;
				if (flag3)
				{
					buttonClickStatus.m_ClickCount = 1;
				}
				else
				{
					buttonClickStatus.m_ClickCount++;
				}
				buttonClickStatus.m_LastPointerDownTime = evt.timestamp;
				buttonClickStatus.m_PointerDownPosition = pointerEvent.position;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002910 File Offset: 0x00000B10
		private void SendClickEvent(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				VisualElement visualElement = evt.target as VisualElement;
				bool flag2 = visualElement != null && visualElement.worldBound.Contains(pointerEvent.position);
				if (flag2)
				{
					bool flag3 = buttonClickStatus.m_Target != null && buttonClickStatus.m_ClickCount > 0;
					if (flag3)
					{
						VisualElement visualElement2 = buttonClickStatus.m_Target.FindCommonAncestor(evt.target as VisualElement);
						bool flag4 = visualElement2 != null;
						if (flag4)
						{
							using (ClickEvent pooled = ClickEvent.GetPooled(evt as PointerUpEvent, buttonClickStatus.m_ClickCount))
							{
								pooled.target = visualElement2;
								visualElement2.SendEvent(pooled);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A00 File Offset: 0x00000C00
		private void CancelClickTracking(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				buttonClickStatus.Reset();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A3C File Offset: 0x00000C3C
		public void ProcessEvent(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<PointerDownEvent>.TypeId() && pointerEvent.button == 0;
				if (flag2)
				{
					this.StartClickTracking(evt);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<PointerMoveEvent>.TypeId();
					if (flag3)
					{
						bool flag4 = pointerEvent.button == 0 && (pointerEvent.pressedButtons & 1) == 1;
						if (flag4)
						{
							this.StartClickTracking(evt);
						}
						else
						{
							bool flag5 = pointerEvent.button == 0 && (pointerEvent.pressedButtons & 1) == 0;
							if (flag5)
							{
								this.SendClickEvent(evt);
							}
							else
							{
								ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
								bool flag6 = buttonClickStatus.m_Target != null;
								if (flag6)
								{
									buttonClickStatus.m_LastPointerDownTime = 0L;
								}
							}
						}
					}
					else
					{
						bool flag7 = evt.eventTypeId == EventBase<PointerCancelEvent>.TypeId() || evt.eventTypeId == EventBase<PointerStationaryEvent>.TypeId() || evt.eventTypeId == EventBase<DragUpdatedEvent>.TypeId();
						if (flag7)
						{
							this.CancelClickTracking(evt);
						}
						else
						{
							bool flag8 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() && pointerEvent.button == 0;
							if (flag8)
							{
								this.SendClickEvent(evt);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000014 RID: 20
		private List<ClickDetector.ButtonClickStatus> m_ClickStatus;

		// Token: 0x02000009 RID: 9
		private class ButtonClickStatus
		{
			// Token: 0x06000033 RID: 51 RVA: 0x00002B90 File Offset: 0x00000D90
			public void Reset()
			{
				this.m_Target = null;
				this.m_ClickCount = 0;
				this.m_LastPointerDownTime = 0L;
				this.m_PointerDownPosition = Vector3.zero;
			}

			// Token: 0x04000016 RID: 22
			public VisualElement m_Target;

			// Token: 0x04000017 RID: 23
			public Vector3 m_PointerDownPosition;

			// Token: 0x04000018 RID: 24
			public long m_LastPointerDownTime;

			// Token: 0x04000019 RID: 25
			public int m_ClickCount;
		}
	}
}
