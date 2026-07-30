using System;

namespace System.Security.Policy
{
	// Token: 0x0200056C RID: 1388
	internal interface IBuiltInEvidence
	{
		// Token: 0x06003E55 RID: 15957
		int GetRequiredSize(bool verbose);

		// Token: 0x06003E56 RID: 15958
		int InitFromBuffer(char[] buffer, int position);

		// Token: 0x06003E57 RID: 15959
		int OutputToBuffer(char[] buffer, int position, bool verbose);
	}
}
