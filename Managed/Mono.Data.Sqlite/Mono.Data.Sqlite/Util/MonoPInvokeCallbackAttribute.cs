using System;
using System.Diagnostics;

namespace Mono.Util
{
	// Token: 0x02000038 RID: 56
	[Conditional("MONOTOUCH")]
	[Conditional("FULL_AOT_RUNTIME")]
	[Conditional("UNITY")]
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000E5B6 File Offset: 0x0000C7B6
		public MonoPInvokeCallbackAttribute(Type t)
		{
		}
	}
}
