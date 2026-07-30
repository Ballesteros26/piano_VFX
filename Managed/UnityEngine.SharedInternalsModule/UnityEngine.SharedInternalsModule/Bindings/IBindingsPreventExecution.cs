using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000027 RID: 39
	[VisibleToOtherModules]
	internal interface IBindingsPreventExecution
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000077 RID: 119
		// (set) Token: 0x06000078 RID: 120
		object singleFlagValue { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000079 RID: 121
		// (set) Token: 0x0600007A RID: 122
		PreventExecutionSeverity severity { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007B RID: 123
		// (set) Token: 0x0600007C RID: 124
		string howToFix { get; set; }
	}
}
