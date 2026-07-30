using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x02000A5D RID: 2653
	internal class DefaultFilter : AssertFilter
	{
		// Token: 0x06006163 RID: 24931 RVA: 0x0013FEF7 File Offset: 0x0013E0F7
		internal DefaultFilter()
		{
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x0013FEFF File Offset: 0x0013E0FF
		[SecuritySafeCritical]
		public override AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle)
		{
			return (AssertFilters)Assert.ShowDefaultAssertDialog(condition, message, location.ToString(stackTraceFormat), windowTitle);
		}
	}
}
