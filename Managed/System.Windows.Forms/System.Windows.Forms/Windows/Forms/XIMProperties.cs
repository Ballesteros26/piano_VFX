using System;

namespace System.Windows.Forms
{
	// Token: 0x02000435 RID: 1077
	[Flags]
	internal enum XIMProperties
	{
		// Token: 0x04002260 RID: 8800
		XIMPreeditArea = 1,
		// Token: 0x04002261 RID: 8801
		XIMPreeditCallbacks = 2,
		// Token: 0x04002262 RID: 8802
		XIMPreeditPosition = 4,
		// Token: 0x04002263 RID: 8803
		XIMPreeditNothing = 8,
		// Token: 0x04002264 RID: 8804
		XIMPreeditNone = 16,
		// Token: 0x04002265 RID: 8805
		XIMStatusArea = 256,
		// Token: 0x04002266 RID: 8806
		XIMStatusCallbacks = 512,
		// Token: 0x04002267 RID: 8807
		XIMStatusNothing = 1024,
		// Token: 0x04002268 RID: 8808
		XIMStatusNone = 2048
	}
}
