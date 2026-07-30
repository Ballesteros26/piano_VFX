using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000193 RID: 403
	internal class EventDebuggerPathTrace : EventDebuggerTrace
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00029DAA File Offset: 0x00027FAA
		public PropagationPaths paths { get; }

		// Token: 0x06000B36 RID: 2870 RVA: 0x00029DB2 File Offset: 0x00027FB2
		public EventDebuggerPathTrace(IPanel panel, EventBase evt, PropagationPaths paths)
			: base(panel, evt, -1L, null)
		{
			this.paths = paths;
		}
	}
}
