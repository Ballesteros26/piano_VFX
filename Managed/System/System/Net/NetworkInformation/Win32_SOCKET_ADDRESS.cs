using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000686 RID: 1670
	internal struct Win32_SOCKET_ADDRESS
	{
		// Token: 0x060034A1 RID: 13473 RVA: 0x000C37E0 File Offset: 0x000C19E0
		public IPAddress GetIPAddress()
		{
			Win32_SOCKADDR win32_SOCKADDR = (Win32_SOCKADDR)Marshal.PtrToStructure(this.Sockaddr, typeof(Win32_SOCKADDR));
			byte[] array;
			if (win32_SOCKADDR.AddressFamily == 23)
			{
				array = new byte[16];
				Array.Copy(win32_SOCKADDR.AddressData, 6, array, 0, 16);
			}
			else
			{
				array = new byte[4];
				Array.Copy(win32_SOCKADDR.AddressData, 2, array, 0, 4);
			}
			return new IPAddress(array);
		}

		// Token: 0x04002A1C RID: 10780
		public IntPtr Sockaddr;

		// Token: 0x04002A1D RID: 10781
		public int SockaddrLength;

		// Token: 0x04002A1E RID: 10782
		private const int AF_INET6 = 23;
	}
}
