using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A96 RID: 2710
	internal class EventListenerHelper : EventListener
	{
		// Token: 0x060062BC RID: 25276 RVA: 0x00141ED8 File Offset: 0x001400D8
		public new static int EventSourceIndex(EventSource eventSource)
		{
			return EventListener.EventSourceIndex(eventSource);
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x00002194 File Offset: 0x00000394
		protected internal override void OnEventWritten(EventWrittenEventArgs eventData)
		{
		}
	}
}
