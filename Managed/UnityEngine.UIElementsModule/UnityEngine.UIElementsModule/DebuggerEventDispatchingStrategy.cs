using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000130 RID: 304
	internal class DebuggerEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x060008AD RID: 2221 RVA: 0x00022D28 File Offset: 0x00020F28
		public bool CanDispatchEvent(EventBase evt)
		{
			return false;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000062F3 File Offset: 0x000044F3
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000062F3 File Offset: 0x000044F3
		public void PostDispatch(EventBase evt, IPanel panel)
		{
		}
	}
}
