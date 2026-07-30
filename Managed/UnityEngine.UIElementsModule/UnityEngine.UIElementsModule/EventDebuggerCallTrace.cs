using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000191 RID: 401
	internal class EventDebuggerCallTrace : EventDebuggerTrace
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00029D03 File Offset: 0x00027F03
		public int callbackHashCode { get; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00029D0B File Offset: 0x00027F0B
		public string callbackName { get; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00029D13 File Offset: 0x00027F13
		public bool propagationHasStopped { get; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00029D1B File Offset: 0x00027F1B
		public bool immediatePropagationHasStopped { get; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00029D23 File Offset: 0x00027F23
		public bool defaultHasBeenPrevented { get; }

		// Token: 0x06000B31 RID: 2865 RVA: 0x00029D2B File Offset: 0x00027F2B
		public EventDebuggerCallTrace(IPanel panel, EventBase evt, int cbHashCode, string cbName, bool propagationHasStopped, bool immediatePropagationHasStopped, bool defaultHasBeenPrevented, long duration, IEventHandler mouseCapture)
			: base(panel, evt, duration, mouseCapture)
		{
			this.callbackHashCode = cbHashCode;
			this.callbackName = cbName;
			this.propagationHasStopped = propagationHasStopped;
			this.immediatePropagationHasStopped = immediatePropagationHasStopped;
			this.defaultHasBeenPrevented = defaultHasBeenPrevented;
		}
	}
}
