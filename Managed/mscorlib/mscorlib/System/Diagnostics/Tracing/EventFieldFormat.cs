using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AA3 RID: 2723
	public enum EventFieldFormat
	{
		// Token: 0x0400314C RID: 12620
		Default,
		// Token: 0x0400314D RID: 12621
		String = 2,
		// Token: 0x0400314E RID: 12622
		Boolean,
		// Token: 0x0400314F RID: 12623
		Hexadecimal,
		// Token: 0x04003150 RID: 12624
		Xml = 11,
		// Token: 0x04003151 RID: 12625
		Json,
		// Token: 0x04003152 RID: 12626
		HResult = 15
	}
}
