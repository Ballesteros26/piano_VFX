using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AA2 RID: 2722
	[AttributeUsage(AttributeTargets.Property)]
	public class EventFieldAttribute : Attribute
	{
		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x060062F0 RID: 25328 RVA: 0x0014271B File Offset: 0x0014091B
		// (set) Token: 0x060062F1 RID: 25329 RVA: 0x00142723 File Offset: 0x00140923
		public EventFieldTags Tags { get; set; }

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x060062F2 RID: 25330 RVA: 0x0014272C File Offset: 0x0014092C
		// (set) Token: 0x060062F3 RID: 25331 RVA: 0x00142734 File Offset: 0x00140934
		internal string Name { get; set; }

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x060062F4 RID: 25332 RVA: 0x0014273D File Offset: 0x0014093D
		// (set) Token: 0x060062F5 RID: 25333 RVA: 0x00142745 File Offset: 0x00140945
		public EventFieldFormat Format { get; set; }
	}
}
