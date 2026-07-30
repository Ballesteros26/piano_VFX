using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000026 RID: 38
	// (Invoke) Token: 0x06000202 RID: 514
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteCallback(IntPtr context, int nArgs, IntPtr argsptr);
}
