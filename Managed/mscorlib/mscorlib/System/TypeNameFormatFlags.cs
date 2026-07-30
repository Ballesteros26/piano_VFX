using System;

namespace System
{
	// Token: 0x020001B7 RID: 439
	internal enum TypeNameFormatFlags
	{
		// Token: 0x04000A9F RID: 2719
		FormatBasic,
		// Token: 0x04000AA0 RID: 2720
		FormatNamespace,
		// Token: 0x04000AA1 RID: 2721
		FormatFullInst,
		// Token: 0x04000AA2 RID: 2722
		FormatAssembly = 4,
		// Token: 0x04000AA3 RID: 2723
		FormatSignature = 8,
		// Token: 0x04000AA4 RID: 2724
		FormatNoVersion = 16,
		// Token: 0x04000AA5 RID: 2725
		FormatAngleBrackets = 64,
		// Token: 0x04000AA6 RID: 2726
		FormatStubInfo = 128,
		// Token: 0x04000AA7 RID: 2727
		FormatGenericParam = 256,
		// Token: 0x04000AA8 RID: 2728
		FormatSerialization = 259
	}
}
