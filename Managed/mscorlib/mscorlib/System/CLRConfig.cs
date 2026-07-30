using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x020001F5 RID: 501
	[FriendAccessAllowed]
	internal class CLRConfig
	{
		// Token: 0x06001767 RID: 5991 RVA: 0x00015ED5 File Offset: 0x000140D5
		[SuppressUnmanagedCodeSecurity]
		[FriendAccessAllowed]
		[SecurityCritical]
		internal static bool CheckLegacyManagedDeflateStream()
		{
			return false;
		}

		// Token: 0x06001768 RID: 5992
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CheckThrowUnobservedTaskExceptions();
	}
}
