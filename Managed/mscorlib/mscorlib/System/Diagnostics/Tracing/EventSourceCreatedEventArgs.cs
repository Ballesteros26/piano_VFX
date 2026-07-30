using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000B03 RID: 2819
	public class EventSourceCreatedEventArgs : EventArgs
	{
		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06006563 RID: 25955 RVA: 0x0014D294 File Offset: 0x0014B494
		// (set) Token: 0x06006564 RID: 25956 RVA: 0x0014D29C File Offset: 0x0014B49C
		public EventSource EventSource { get; internal set; }
	}
}
