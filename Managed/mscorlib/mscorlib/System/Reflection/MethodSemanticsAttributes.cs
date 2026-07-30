using System;

namespace System.Reflection
{
	// Token: 0x020002EA RID: 746
	[Flags]
	[Serializable]
	internal enum MethodSemanticsAttributes
	{
		// Token: 0x0400121C RID: 4636
		Setter = 1,
		// Token: 0x0400121D RID: 4637
		Getter = 2,
		// Token: 0x0400121E RID: 4638
		Other = 4,
		// Token: 0x0400121F RID: 4639
		AddOn = 8,
		// Token: 0x04001220 RID: 4640
		RemoveOn = 16,
		// Token: 0x04001221 RID: 4641
		Fire = 32
	}
}
