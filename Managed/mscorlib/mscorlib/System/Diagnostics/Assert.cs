using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x02000A5B RID: 2651
	internal static class Assert
	{
		// Token: 0x0600615A RID: 24922 RVA: 0x0013FE45 File Offset: 0x0013E045
		internal static void Check(bool condition, string conditionString, string message)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, -2146232797);
			}
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x0013FE57 File Offset: 0x0013E057
		internal static void Check(bool condition, string conditionString, string message, int exitCode)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, exitCode);
			}
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x0013FE65 File Offset: 0x0013E065
		internal static void Fail(string conditionString, string message)
		{
			Assert.Fail(conditionString, message, null, -2146232797);
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x0013FE74 File Offset: 0x0013E074
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode)
		{
			Assert.Fail(conditionString, message, windowTitle, exitCode, StackTrace.TraceFormat.Normal, 0);
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x0013FE81 File Offset: 0x0013E081
		internal static void Fail(string conditionString, string message, int exitCode, StackTrace.TraceFormat stackTraceFormat)
		{
			Assert.Fail(conditionString, message, null, exitCode, stackTraceFormat, 0);
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x0013FE90 File Offset: 0x0013E090
		[SecuritySafeCritical]
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode, StackTrace.TraceFormat stackTraceFormat, int numStackFramesToSkip)
		{
			StackTrace stackTrace = new StackTrace(numStackFramesToSkip, true);
			AssertFilters assertFilters = Assert.Filter.AssertFailure(conditionString, message, stackTrace, stackTraceFormat, windowTitle);
			if (assertFilters == AssertFilters.FailDebug)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
					return;
				}
				if (!Debugger.Launch())
				{
					throw new InvalidOperationException(Environment.GetResourceString("Debugger unable to launch."));
				}
			}
			else if (assertFilters == AssertFilters.FailTerminate)
			{
				if (Debugger.IsAttached)
				{
					Environment._Exit(exitCode);
					return;
				}
				Environment.FailFast(message, (uint)exitCode);
			}
		}

		// Token: 0x06006160 RID: 24928
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ShowDefaultAssertDialog(string conditionString, string message, string stackTrace, string windowTitle);

		// Token: 0x0400309B RID: 12443
		internal const int COR_E_FAILFAST = -2146232797;

		// Token: 0x0400309C RID: 12444
		private static AssertFilter Filter = new DefaultFilter();
	}
}
