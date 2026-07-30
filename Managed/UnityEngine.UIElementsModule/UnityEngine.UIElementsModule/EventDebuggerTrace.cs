using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000190 RID: 400
	internal class EventDebuggerTrace
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00029C8A File Offset: 0x00027E8A
		public EventDebuggerEventRecord eventBase { get; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00029C92 File Offset: 0x00027E92
		public IEventHandler focusedElement { get; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00029C9A File Offset: 0x00027E9A
		public IEventHandler mouseCapture { get; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00029CA2 File Offset: 0x00027EA2
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x00029CAA File Offset: 0x00027EAA
		public long duration { get; set; }

		// Token: 0x06000B2B RID: 2859 RVA: 0x00029CB4 File Offset: 0x00027EB4
		public EventDebuggerTrace(IPanel panel, EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.eventBase = new EventDebuggerEventRecord(evt);
			IEventHandler eventHandler;
			if (panel == null)
			{
				eventHandler = null;
			}
			else
			{
				FocusController focusController = panel.focusController;
				eventHandler = ((focusController != null) ? focusController.focusedElement : null);
			}
			this.focusedElement = eventHandler;
			this.mouseCapture = mouseCapture;
			this.duration = duration;
		}
	}
}
