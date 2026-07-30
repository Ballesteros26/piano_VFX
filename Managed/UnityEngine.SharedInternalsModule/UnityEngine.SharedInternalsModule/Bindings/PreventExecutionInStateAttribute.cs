using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000028 RID: 40
	[AttributeUsage(192, AllowMultiple = true)]
	[VisibleToOtherModules]
	internal class PreventExecutionInStateAttribute : Attribute, IBindingsPreventExecution
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000025EB File Offset: 0x000007EB
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000025F3 File Offset: 0x000007F3
		public object singleFlagValue { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000025FC File Offset: 0x000007FC
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002604 File Offset: 0x00000804
		public PreventExecutionSeverity severity { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000260D File Offset: 0x0000080D
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002615 File Offset: 0x00000815
		public string howToFix { get; set; }

		// Token: 0x06000083 RID: 131 RVA: 0x0000261E File Offset: 0x0000081E
		public PreventExecutionInStateAttribute(object systemAndFlags, PreventExecutionSeverity reportSeverity, string howToString = "")
		{
			this.singleFlagValue = systemAndFlags;
			this.severity = reportSeverity;
			this.howToFix = howToString;
		}
	}
}
