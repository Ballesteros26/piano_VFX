using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003D1 RID: 977
	[RequiredByNativeCode]
	internal class ScriptableRuntimeReflectionSystemWrapper
	{
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x00039767 File Offset: 0x00037967
		// (set) Token: 0x060021DD RID: 8669 RVA: 0x0003976F File Offset: 0x0003796F
		internal IScriptableRuntimeReflectionSystem implementation { get; set; }

		// Token: 0x060021DE RID: 8670 RVA: 0x00039778 File Offset: 0x00037978
		[RequiredByNativeCode]
		private void Internal_ScriptableRuntimeReflectionSystemWrapper_TickRealtimeProbes(out bool result)
		{
			result = this.implementation != null && this.implementation.TickRealtimeProbes();
		}
	}
}
