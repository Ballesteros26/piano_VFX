using System;

namespace System.Web
{
	// Token: 0x02000073 RID: 115
	[Flags]
	internal enum ExceptionPageTemplateType
	{
		// Token: 0x04000E89 RID: 3721
		Standard = 1,
		// Token: 0x04000E8A RID: 3722
		CustomErrorDefault = 2,
		// Token: 0x04000E8B RID: 3723
		Htmlized = 4,
		// Token: 0x04000E8C RID: 3724
		SourceError = 8,
		// Token: 0x04000E8D RID: 3725
		CompilerOutput = 16,
		// Token: 0x04000E8E RID: 3726
		Any = 65535
	}
}
