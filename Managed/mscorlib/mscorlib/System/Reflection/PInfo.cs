using System;

namespace System.Reflection
{
	// Token: 0x02000333 RID: 819
	[Flags]
	internal enum PInfo
	{
		// Token: 0x04001359 RID: 4953
		Attributes = 1,
		// Token: 0x0400135A RID: 4954
		GetMethod = 2,
		// Token: 0x0400135B RID: 4955
		SetMethod = 4,
		// Token: 0x0400135C RID: 4956
		ReflectedType = 8,
		// Token: 0x0400135D RID: 4957
		DeclaringType = 16,
		// Token: 0x0400135E RID: 4958
		Name = 32
	}
}
