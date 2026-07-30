using System;

namespace System.Web.Compilation
{
	// Token: 0x0200066D RID: 1645
	internal enum TagType
	{
		// Token: 0x04002537 RID: 9527
		Text,
		// Token: 0x04002538 RID: 9528
		Tag,
		// Token: 0x04002539 RID: 9529
		Close,
		// Token: 0x0400253A RID: 9530
		SelfClosing,
		// Token: 0x0400253B RID: 9531
		Directive,
		// Token: 0x0400253C RID: 9532
		ServerComment,
		// Token: 0x0400253D RID: 9533
		DataBinding,
		// Token: 0x0400253E RID: 9534
		CodeRender,
		// Token: 0x0400253F RID: 9535
		CodeRenderExpression,
		// Token: 0x04002540 RID: 9536
		Include,
		// Token: 0x04002541 RID: 9537
		CodeRenderEncode
	}
}
