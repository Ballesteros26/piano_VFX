using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000027 RID: 39
	// (Invoke) Token: 0x06000206 RID: 518
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteFinalCallback(IntPtr context);
}
