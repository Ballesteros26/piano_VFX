using System;
using System.Runtime.InteropServices;

namespace System.Net.Security
{
	// Token: 0x0200004B RID: 75
	internal abstract class SafeFreeCredentials : SafeHandle
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000EB26 File Offset: 0x0000CD26
		protected SafeFreeCredentials(IntPtr handle, bool ownsHandle)
			: base(handle, ownsHandle)
		{
		}
	}
}
