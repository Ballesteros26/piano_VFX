using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000957 RID: 2391
	internal static class AddrofIntrinsics
	{
		// Token: 0x06005926 RID: 22822 RVA: 0x0012AB47 File Offset: 0x00128D47
		internal static IntPtr AddrOf<T>(T ftn)
		{
			return Marshal.GetFunctionPointerForDelegate<T>(ftn);
		}
	}
}
