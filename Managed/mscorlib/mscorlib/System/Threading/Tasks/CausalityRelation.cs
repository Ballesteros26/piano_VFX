using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004B8 RID: 1208
	internal enum CausalityRelation
	{
		// Token: 0x04001D9A RID: 7578
		AssignDelegate,
		// Token: 0x04001D9B RID: 7579
		Join,
		// Token: 0x04001D9C RID: 7580
		Choice,
		// Token: 0x04001D9D RID: 7581
		Cancel,
		// Token: 0x04001D9E RID: 7582
		Error
	}
}
