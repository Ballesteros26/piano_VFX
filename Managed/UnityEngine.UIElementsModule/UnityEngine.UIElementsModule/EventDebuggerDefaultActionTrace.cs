using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000192 RID: 402
	internal class EventDebuggerDefaultActionTrace : EventDebuggerTrace
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00029D62 File Offset: 0x00027F62
		public PropagationPhase phase { get; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00029D6C File Offset: 0x00027F6C
		public string targetName
		{
			get
			{
				return base.eventBase.target.GetType().FullName;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00029D93 File Offset: 0x00027F93
		public EventDebuggerDefaultActionTrace(IPanel panel, EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
			: base(panel, evt, duration, mouseCapture)
		{
			this.phase = phase;
		}
	}
}
