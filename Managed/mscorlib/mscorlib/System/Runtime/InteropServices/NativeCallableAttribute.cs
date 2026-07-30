using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008A1 RID: 2209
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class NativeCallableAttribute : Attribute
	{
		// Token: 0x04002BE9 RID: 11241
		public string EntryPoint;

		// Token: 0x04002BEA RID: 11242
		public CallingConvention CallingConvention;
	}
}
