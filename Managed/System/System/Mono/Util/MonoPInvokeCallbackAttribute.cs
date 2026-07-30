using System;
using System.Diagnostics;

namespace Mono.Util
{
	// Token: 0x0200000D RID: 13
	[Conditional("MONOTOUCH")]
	[AttributeUsage(AttributeTargets.Method)]
	[Conditional("UNITY")]
	[Conditional("FULL_AOT_RUNTIME")]
	internal sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002C6F File Offset: 0x00000E6F
		public MonoPInvokeCallbackAttribute(Type t)
		{
		}
	}
}
