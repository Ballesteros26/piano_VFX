using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000728 RID: 1832
	[Serializable]
	internal enum InternalParseStateE
	{
		// Token: 0x04002840 RID: 10304
		Initial,
		// Token: 0x04002841 RID: 10305
		Object,
		// Token: 0x04002842 RID: 10306
		Member,
		// Token: 0x04002843 RID: 10307
		MemberChild
	}
}
