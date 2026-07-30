using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200002C RID: 44
	internal class XPrintfFunctions
	{
		// Token: 0x0400013E RID: 318
		internal static XPrintfFunctions.XPrintf printf = new XPrintfFunctions.XPrintf(new CdeclFunction("msvcrt", "printf", typeof(int)).Invoke);

		// Token: 0x0400013F RID: 319
		internal static XPrintfFunctions.XPrintf fprintf = new XPrintfFunctions.XPrintf(new CdeclFunction("msvcrt", "fprintf", typeof(int)).Invoke);

		// Token: 0x04000140 RID: 320
		internal static XPrintfFunctions.XPrintf snprintf = new XPrintfFunctions.XPrintf(new CdeclFunction("MonoPosixHelper", "Mono_Posix_Stdlib_snprintf", typeof(int)).Invoke);

		// Token: 0x04000141 RID: 321
		internal static XPrintfFunctions.XPrintf syslog = new XPrintfFunctions.XPrintf(new CdeclFunction("MonoPosixHelper", "Mono_Posix_Stdlib_syslog2", typeof(int)).Invoke);

		// Token: 0x020000A5 RID: 165
		// (Invoke) Token: 0x0600076A RID: 1898
		internal delegate object XPrintf(object[] parameters);
	}
}
