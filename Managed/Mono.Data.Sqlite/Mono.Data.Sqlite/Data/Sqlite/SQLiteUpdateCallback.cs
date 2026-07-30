using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200000E RID: 14
	// (Invoke) Token: 0x06000140 RID: 320
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteUpdateCallback(IntPtr puser, int type, IntPtr database, IntPtr table, long rowid);
}
