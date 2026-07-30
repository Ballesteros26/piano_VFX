using System;

namespace System.Xml
{
	// Token: 0x020000B7 RID: 183
	internal enum ElementProperties : uint
	{
		// Token: 0x040003AF RID: 943
		DEFAULT,
		// Token: 0x040003B0 RID: 944
		URI_PARENT,
		// Token: 0x040003B1 RID: 945
		BOOL_PARENT,
		// Token: 0x040003B2 RID: 946
		NAME_PARENT = 4U,
		// Token: 0x040003B3 RID: 947
		EMPTY = 8U,
		// Token: 0x040003B4 RID: 948
		NO_ENTITIES = 16U,
		// Token: 0x040003B5 RID: 949
		HEAD = 32U,
		// Token: 0x040003B6 RID: 950
		BLOCK_WS = 64U,
		// Token: 0x040003B7 RID: 951
		HAS_NS = 128U
	}
}
