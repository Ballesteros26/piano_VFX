using System;

namespace System.Xml.Xsl
{
	// Token: 0x020004C9 RID: 1225
	[Flags]
	internal enum XmlNodeKindFlags
	{
		// Token: 0x0400205E RID: 8286
		None = 0,
		// Token: 0x0400205F RID: 8287
		Document = 1,
		// Token: 0x04002060 RID: 8288
		Element = 2,
		// Token: 0x04002061 RID: 8289
		Attribute = 4,
		// Token: 0x04002062 RID: 8290
		Text = 8,
		// Token: 0x04002063 RID: 8291
		Comment = 16,
		// Token: 0x04002064 RID: 8292
		PI = 32,
		// Token: 0x04002065 RID: 8293
		Namespace = 64,
		// Token: 0x04002066 RID: 8294
		Content = 58,
		// Token: 0x04002067 RID: 8295
		Any = 127
	}
}
