using System;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000146 RID: 326
	internal enum NodeType : ushort
	{
		// Token: 0x0400014D RID: 333
		Element = 1,
		// Token: 0x0400014E RID: 334
		Attribute,
		// Token: 0x0400014F RID: 335
		Text,
		// Token: 0x04000150 RID: 336
		CDataSection,
		// Token: 0x04000151 RID: 337
		EntityReference,
		// Token: 0x04000152 RID: 338
		Entity,
		// Token: 0x04000153 RID: 339
		ProcessingInstruction,
		// Token: 0x04000154 RID: 340
		Comment,
		// Token: 0x04000155 RID: 341
		Document,
		// Token: 0x04000156 RID: 342
		DocumentType,
		// Token: 0x04000157 RID: 343
		DocumentFragment,
		// Token: 0x04000158 RID: 344
		Notation
	}
}
