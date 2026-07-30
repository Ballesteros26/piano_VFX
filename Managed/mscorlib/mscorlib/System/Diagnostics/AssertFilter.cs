using System;

namespace System.Diagnostics
{
	// Token: 0x02000A5C RID: 2652
	[Serializable]
	internal abstract class AssertFilter
	{
		// Token: 0x06006161 RID: 24929
		public abstract AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle);
	}
}
