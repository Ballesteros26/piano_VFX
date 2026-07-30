using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000677 RID: 1655
	internal class Win32NetworkInterface
	{
		// Token: 0x06003497 RID: 13463
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetNetworkParams(IntPtr ptr, ref int size);

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000C3748 File Offset: 0x000C1948
		public static Win32_FIXED_INFO FixedInfo
		{
			get
			{
				if (!Win32NetworkInterface.initialized)
				{
					int num = 0;
					Win32NetworkInterface.GetNetworkParams(IntPtr.Zero, ref num);
					IntPtr intPtr = Marshal.AllocHGlobal(num);
					Win32NetworkInterface.GetNetworkParams(intPtr, ref num);
					Win32NetworkInterface.fixedInfo = Marshal.PtrToStructure<Win32_FIXED_INFO>(intPtr);
					Win32NetworkInterface.initialized = true;
				}
				return Win32NetworkInterface.fixedInfo;
			}
		}

		// Token: 0x0400298C RID: 10636
		private static Win32_FIXED_INFO fixedInfo;

		// Token: 0x0400298D RID: 10637
		private static bool initialized;
	}
}
