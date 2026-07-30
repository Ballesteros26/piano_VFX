using System;

namespace System.Xml.Xsl
{
	// Token: 0x020004D6 RID: 1238
	[Flags]
	internal enum XslFlags
	{
		// Token: 0x040020CF RID: 8399
		None = 0,
		// Token: 0x040020D0 RID: 8400
		String = 1,
		// Token: 0x040020D1 RID: 8401
		Number = 2,
		// Token: 0x040020D2 RID: 8402
		Boolean = 4,
		// Token: 0x040020D3 RID: 8403
		Node = 8,
		// Token: 0x040020D4 RID: 8404
		Nodeset = 16,
		// Token: 0x040020D5 RID: 8405
		Rtf = 32,
		// Token: 0x040020D6 RID: 8406
		TypeFilter = 63,
		// Token: 0x040020D7 RID: 8407
		AnyType = 63,
		// Token: 0x040020D8 RID: 8408
		Current = 256,
		// Token: 0x040020D9 RID: 8409
		Position = 512,
		// Token: 0x040020DA RID: 8410
		Last = 1024,
		// Token: 0x040020DB RID: 8411
		FocusFilter = 1792,
		// Token: 0x040020DC RID: 8412
		FullFocus = 1792,
		// Token: 0x040020DD RID: 8413
		HasCalls = 4096,
		// Token: 0x040020DE RID: 8414
		MayBeDefault = 8192,
		// Token: 0x040020DF RID: 8415
		SideEffects = 16384,
		// Token: 0x040020E0 RID: 8416
		Stop = 32768
	}
}
