using System;

namespace System.Xml
{
	// Token: 0x0200009A RID: 154
	internal interface IDtdDefaultAttributeInfo : IDtdAttributeInfo
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000531 RID: 1329
		string DefaultValueExpanded { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000532 RID: 1330
		object DefaultValueTyped { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000533 RID: 1331
		int ValueLineNumber { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000534 RID: 1332
		int ValueLinePosition { get; }
	}
}
