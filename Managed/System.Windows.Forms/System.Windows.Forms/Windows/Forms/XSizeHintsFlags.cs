using System;

namespace System.Windows.Forms
{
	// Token: 0x0200042A RID: 1066
	[Flags]
	internal enum XSizeHintsFlags
	{
		// Token: 0x04002190 RID: 8592
		USPosition = 1,
		// Token: 0x04002191 RID: 8593
		USSize = 2,
		// Token: 0x04002192 RID: 8594
		PPosition = 4,
		// Token: 0x04002193 RID: 8595
		PSize = 8,
		// Token: 0x04002194 RID: 8596
		PMinSize = 16,
		// Token: 0x04002195 RID: 8597
		PMaxSize = 32,
		// Token: 0x04002196 RID: 8598
		PResizeInc = 64,
		// Token: 0x04002197 RID: 8599
		PAspect = 128,
		// Token: 0x04002198 RID: 8600
		PAllHints = 252,
		// Token: 0x04002199 RID: 8601
		PBaseSize = 256,
		// Token: 0x0400219A RID: 8602
		PWinGravity = 512
	}
}
