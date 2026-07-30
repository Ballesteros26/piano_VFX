using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000045 RID: 69
	internal class PointerDispatchState
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000711C File Offset: 0x0000531C
		public PointerDispatchState()
		{
			this.Reset();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007168 File Offset: 0x00005368
		internal void Reset()
		{
			for (int i = 0; i < this.m_PointerCapture.Length; i++)
			{
				this.m_PendingPointerCapture[i] = null;
				this.m_PointerCapture[i] = null;
				this.m_ShouldSendCompatibilityMouseEvents[i] = true;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000071AC File Offset: 0x000053AC
		public IEventHandler GetCapturingElement(int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId];
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000071C8 File Offset: 0x000053C8
		public bool HasPointerCapture(IEventHandler handler, int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId] == handler;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000071E8 File Offset: 0x000053E8
		public void CapturePointer(IEventHandler handler, int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId && this.m_PendingPointerCapture[pointerId] != handler && GUIUtility.hotControl != 0;
			if (flag)
			{
				Debug.LogWarning("Should not be capturing when there is a hotcontrol");
			}
			else
			{
				this.m_PendingPointerCapture[pointerId] = handler;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000722F File Offset: 0x0000542F
		public void ReleasePointer(int pointerId)
		{
			this.m_PendingPointerCapture[pointerId] = null;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000723C File Offset: 0x0000543C
		public void ReleasePointer(IEventHandler handler, int pointerId)
		{
			bool flag = handler == this.m_PendingPointerCapture[pointerId];
			if (flag)
			{
				this.m_PendingPointerCapture[pointerId] = null;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007264 File Offset: 0x00005464
		public void ProcessPointerCapture(int pointerId)
		{
			bool flag = this.m_PointerCapture[pointerId] == this.m_PendingPointerCapture[pointerId];
			if (!flag)
			{
				bool flag2 = this.m_PointerCapture[pointerId] != null;
				if (flag2)
				{
					using (PointerCaptureOutEvent pooled = PointerCaptureEventBase<PointerCaptureOutEvent>.GetPooled(this.m_PointerCapture[pointerId], this.m_PendingPointerCapture[pointerId], pointerId))
					{
						this.m_PointerCapture[pointerId].SendEvent(pooled);
					}
					bool flag3 = pointerId == PointerId.mousePointerId;
					if (flag3)
					{
						using (MouseCaptureOutEvent pooled2 = PointerCaptureEventBase<MouseCaptureOutEvent>.GetPooled(this.m_PointerCapture[pointerId], this.m_PendingPointerCapture[pointerId], pointerId))
						{
							this.m_PointerCapture[pointerId].SendEvent(pooled2);
						}
					}
				}
				bool flag4 = this.m_PendingPointerCapture[pointerId] != null;
				if (flag4)
				{
					using (PointerCaptureEvent pooled3 = PointerCaptureEventBase<PointerCaptureEvent>.GetPooled(this.m_PendingPointerCapture[pointerId], this.m_PointerCapture[pointerId], pointerId))
					{
						this.m_PendingPointerCapture[pointerId].SendEvent(pooled3);
					}
					bool flag5 = pointerId == PointerId.mousePointerId;
					if (flag5)
					{
						using (MouseCaptureEvent pooled4 = PointerCaptureEventBase<MouseCaptureEvent>.GetPooled(this.m_PendingPointerCapture[pointerId], this.m_PointerCapture[pointerId], pointerId))
						{
							this.m_PendingPointerCapture[pointerId].SendEvent(pooled4);
						}
					}
				}
				this.m_PointerCapture[pointerId] = this.m_PendingPointerCapture[pointerId];
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000073F8 File Offset: 0x000055F8
		public void ActivateCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = true;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007404 File Offset: 0x00005604
		public void PreventCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = false;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007410 File Offset: 0x00005610
		public bool ShouldSendCompatibilityMouseEvents(IPointerEvent evt)
		{
			return evt.isPrimary && this.m_ShouldSendCompatibilityMouseEvents[evt.pointerId];
		}

		// Token: 0x040000D1 RID: 209
		private IEventHandler[] m_PendingPointerCapture = new IEventHandler[PointerId.maxPointers];

		// Token: 0x040000D2 RID: 210
		private IEventHandler[] m_PointerCapture = new IEventHandler[PointerId.maxPointers];

		// Token: 0x040000D3 RID: 211
		private bool[] m_ShouldSendCompatibilityMouseEvents = new bool[PointerId.maxPointers];
	}
}
