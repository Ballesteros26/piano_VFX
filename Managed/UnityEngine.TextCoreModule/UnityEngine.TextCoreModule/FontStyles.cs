using System;

namespace UnityEngine.TextCore
{
	// Token: 0x0200002E RID: 46
	[Flags]
	internal enum FontStyles
	{
		// Token: 0x040002AA RID: 682
		Normal = 0,
		// Token: 0x040002AB RID: 683
		Bold = 1,
		// Token: 0x040002AC RID: 684
		Italic = 2,
		// Token: 0x040002AD RID: 685
		Underline = 4,
		// Token: 0x040002AE RID: 686
		LowerCase = 8,
		// Token: 0x040002AF RID: 687
		UpperCase = 16,
		// Token: 0x040002B0 RID: 688
		SmallCaps = 32,
		// Token: 0x040002B1 RID: 689
		Strikethrough = 64,
		// Token: 0x040002B2 RID: 690
		Superscript = 128,
		// Token: 0x040002B3 RID: 691
		Subscript = 256,
		// Token: 0x040002B4 RID: 692
		Highlight = 512
	}
}
