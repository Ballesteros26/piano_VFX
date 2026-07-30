using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000896 RID: 2198
	[FriendAccessAllowed]
	internal static class JitHelpers
	{
		// Token: 0x06005476 RID: 21622 RVA: 0x001276F5 File Offset: 0x001258F5
		internal static T UnsafeCast<T>(object o) where T : class
		{
			return Array.UnsafeMov<object, T>(o);
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x001276FD File Offset: 0x001258FD
		internal static int UnsafeEnumCast<T>(T val) where T : struct
		{
			return Array.UnsafeMov<T, int>(val);
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x00127705 File Offset: 0x00125905
		internal static long UnsafeEnumCastLong<T>(T val) where T : struct
		{
			return Array.UnsafeMov<T, long>(val);
		}
	}
}
