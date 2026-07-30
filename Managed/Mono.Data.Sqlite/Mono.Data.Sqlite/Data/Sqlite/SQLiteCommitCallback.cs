using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200000F RID: 15
	// (Invoke) Token: 0x06000144 RID: 324
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int SQLiteCommitCallback(IntPtr puser);
}
