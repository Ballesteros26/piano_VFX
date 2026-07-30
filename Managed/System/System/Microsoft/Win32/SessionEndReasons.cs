using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers that represent how the current logon session is ending.</summary>
	// Token: 0x020000CF RID: 207
	public enum SessionEndReasons
	{
		/// <summary>The user is logging off and ending the current user session. The operating system continues to run.</summary>
		// Token: 0x04000B8B RID: 2955
		Logoff = 1,
		/// <summary>The operating system is shutting down.</summary>
		// Token: 0x04000B8C RID: 2956
		SystemShutdown
	}
}
