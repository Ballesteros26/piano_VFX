using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000122 RID: 290
	public abstract class PointerCaptureEventBase<T> : EventBase<T>, IPointerCaptureEvent where T : PointerCaptureEventBase<T>, new()
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00022999 File Offset: 0x00020B99
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x000229A1 File Offset: 0x00020BA1
		public IEventHandler relatedTarget { get; private set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x000229AA File Offset: 0x00020BAA
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x000229B2 File Offset: 0x00020BB2
		public int pointerId { get; private set; }

		// Token: 0x0600088C RID: 2188 RVA: 0x000229BB File Offset: 0x00020BBB
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000229CC File Offset: 0x00020BCC
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
			this.relatedTarget = null;
			this.pointerId = PointerId.invalidPointerId;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000229EC File Offset: 0x00020BEC
		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget, int pointerId)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.target = target;
			pooled.relatedTarget = relatedTarget;
			pooled.pointerId = pointerId;
			return pooled;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00022A2C File Offset: 0x00020C2C
		protected PointerCaptureEventBase()
		{
			this.LocalInit();
		}
	}
}
