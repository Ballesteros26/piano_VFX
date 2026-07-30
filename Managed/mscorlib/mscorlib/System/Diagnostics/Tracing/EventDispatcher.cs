using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000B11 RID: 2833
	internal class EventDispatcher
	{
		// Token: 0x060065CF RID: 26063 RVA: 0x0014E1A3 File Offset: 0x0014C3A3
		internal EventDispatcher(EventDispatcher next, bool[] eventEnabled, EventListener listener)
		{
			this.m_Next = next;
			this.m_EventEnabled = eventEnabled;
			this.m_Listener = listener;
		}

		// Token: 0x040032B1 RID: 12977
		internal readonly EventListener m_Listener;

		// Token: 0x040032B2 RID: 12978
		internal bool[] m_EventEnabled;

		// Token: 0x040032B3 RID: 12979
		internal bool m_activityFilteringEnabled;

		// Token: 0x040032B4 RID: 12980
		internal EventDispatcher m_Next;
	}
}
