using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000839 RID: 2105
	public static class RuntimeFeature
	{
		// Token: 0x060053A7 RID: 21415 RVA: 0x00125C76 File Offset: 0x00123E76
		public static bool IsSupported(string feature)
		{
			return feature == "PortablePdb";
		}

		// Token: 0x04002B7F RID: 11135
		public const string PortablePdb = "PortablePdb";
	}
}
