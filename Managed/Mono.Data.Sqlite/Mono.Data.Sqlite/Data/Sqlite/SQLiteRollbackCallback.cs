using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000010 RID: 16
	// (Invoke) Token: 0x06000148 RID: 328
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteRollbackCallback(IntPtr puser);
}
