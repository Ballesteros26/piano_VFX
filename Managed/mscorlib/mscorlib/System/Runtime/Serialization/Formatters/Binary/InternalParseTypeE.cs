using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000722 RID: 1826
	[Serializable]
	internal enum InternalParseTypeE
	{
		// Token: 0x04002818 RID: 10264
		Empty,
		// Token: 0x04002819 RID: 10265
		SerializedStreamHeader,
		// Token: 0x0400281A RID: 10266
		Object,
		// Token: 0x0400281B RID: 10267
		Member,
		// Token: 0x0400281C RID: 10268
		ObjectEnd,
		// Token: 0x0400281D RID: 10269
		MemberEnd,
		// Token: 0x0400281E RID: 10270
		Headers,
		// Token: 0x0400281F RID: 10271
		HeadersEnd,
		// Token: 0x04002820 RID: 10272
		SerializedStreamHeaderEnd,
		// Token: 0x04002821 RID: 10273
		Envelope,
		// Token: 0x04002822 RID: 10274
		EnvelopeEnd,
		// Token: 0x04002823 RID: 10275
		Body,
		// Token: 0x04002824 RID: 10276
		BodyEnd
	}
}
