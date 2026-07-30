using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200072A RID: 1834
	[Flags]
	[Serializable]
	internal enum MessageEnum
	{
		// Token: 0x04002859 RID: 10329
		NoArgs = 1,
		// Token: 0x0400285A RID: 10330
		ArgsInline = 2,
		// Token: 0x0400285B RID: 10331
		ArgsIsArray = 4,
		// Token: 0x0400285C RID: 10332
		ArgsInArray = 8,
		// Token: 0x0400285D RID: 10333
		NoContext = 16,
		// Token: 0x0400285E RID: 10334
		ContextInline = 32,
		// Token: 0x0400285F RID: 10335
		ContextInArray = 64,
		// Token: 0x04002860 RID: 10336
		MethodSignatureInArray = 128,
		// Token: 0x04002861 RID: 10337
		PropertyInArray = 256,
		// Token: 0x04002862 RID: 10338
		NoReturnValue = 512,
		// Token: 0x04002863 RID: 10339
		ReturnValueVoid = 1024,
		// Token: 0x04002864 RID: 10340
		ReturnValueInline = 2048,
		// Token: 0x04002865 RID: 10341
		ReturnValueInArray = 4096,
		// Token: 0x04002866 RID: 10342
		ExceptionInArray = 8192,
		// Token: 0x04002867 RID: 10343
		GenericMethod = 32768
	}
}
