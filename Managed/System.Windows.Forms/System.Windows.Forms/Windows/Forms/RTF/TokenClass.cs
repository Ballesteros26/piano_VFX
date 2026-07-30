using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000033 RID: 51
	internal enum TokenClass
	{
		// Token: 0x040004EE RID: 1262
		None = -1,
		// Token: 0x040004EF RID: 1263
		Unknown,
		// Token: 0x040004F0 RID: 1264
		Group,
		// Token: 0x040004F1 RID: 1265
		Text,
		// Token: 0x040004F2 RID: 1266
		Control,
		// Token: 0x040004F3 RID: 1267
		EOF,
		// Token: 0x040004F4 RID: 1268
		MaxClass
	}
}
