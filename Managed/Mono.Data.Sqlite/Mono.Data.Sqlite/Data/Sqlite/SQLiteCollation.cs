using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000028 RID: 40
	// (Invoke) Token: 0x0600020A RID: 522
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int SQLiteCollation(IntPtr puser, int len1, IntPtr pv1, int len2, IntPtr pv2);
}
