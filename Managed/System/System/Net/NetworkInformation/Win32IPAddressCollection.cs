using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000626 RID: 1574
	internal class Win32IPAddressCollection : IPAddressCollection
	{
		// Token: 0x0600321B RID: 12827 RVA: 0x000BE4ED File Offset: 0x000BC6ED
		private Win32IPAddressCollection()
		{
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000BE4F8 File Offset: 0x000BC6F8
		public Win32IPAddressCollection(params IntPtr[] heads)
		{
			foreach (IntPtr intPtr in heads)
			{
				this.AddSubsequentlyString(intPtr);
			}
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000BE528 File Offset: 0x000BC728
		public Win32IPAddressCollection(params Win32_IP_ADDR_STRING[] al)
		{
			foreach (Win32_IP_ADDR_STRING win32_IP_ADDR_STRING in al)
			{
				if (!string.IsNullOrEmpty(win32_IP_ADDR_STRING.IpAddress))
				{
					base.InternalAdd(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress));
					this.AddSubsequentlyString(win32_IP_ADDR_STRING.Next);
				}
			}
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000BE580 File Offset: 0x000BC780
		public static Win32IPAddressCollection FromAnycast(IntPtr ptr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ANYCAST_ADDRESS win32_IP_ADAPTER_ANYCAST_ADDRESS = (Win32_IP_ADAPTER_ANYCAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_ANYCAST_ADDRESS));
				win32IPAddressCollection.InternalAdd(win32_IP_ADAPTER_ANYCAST_ADDRESS.Address.GetIPAddress());
				intPtr = win32_IP_ADAPTER_ANYCAST_ADDRESS.Next;
			}
			return win32IPAddressCollection;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000BE5D4 File Offset: 0x000BC7D4
		public static Win32IPAddressCollection FromDnsServer(IntPtr ptr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_DNS_SERVER_ADDRESS win32_IP_ADAPTER_DNS_SERVER_ADDRESS = (Win32_IP_ADAPTER_DNS_SERVER_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_DNS_SERVER_ADDRESS));
				win32IPAddressCollection.InternalAdd(win32_IP_ADAPTER_DNS_SERVER_ADDRESS.Address.GetIPAddress());
				intPtr = win32_IP_ADAPTER_DNS_SERVER_ADDRESS.Next;
			}
			return win32IPAddressCollection;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000BE628 File Offset: 0x000BC828
		public static Win32IPAddressCollection FromSocketAddress(Win32_SOCKET_ADDRESS addr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			if (addr.Sockaddr != IntPtr.Zero)
			{
				win32IPAddressCollection.InternalAdd(addr.GetIPAddress());
			}
			return win32IPAddressCollection;
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000BE65C File Offset: 0x000BC85C
		public static Win32IPAddressCollection FromWinsServer(IntPtr ptr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_WINS_SERVER_ADDRESS win32_IP_ADAPTER_WINS_SERVER_ADDRESS = (Win32_IP_ADAPTER_WINS_SERVER_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_WINS_SERVER_ADDRESS));
				win32IPAddressCollection.InternalAdd(win32_IP_ADAPTER_WINS_SERVER_ADDRESS.Address.GetIPAddress());
				intPtr = win32_IP_ADAPTER_WINS_SERVER_ADDRESS.Next;
			}
			return win32IPAddressCollection;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000BE6B0 File Offset: 0x000BC8B0
		private void AddSubsequentlyString(IntPtr head)
		{
			IntPtr intPtr = head;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADDR_STRING win32_IP_ADDR_STRING = (Win32_IP_ADDR_STRING)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADDR_STRING));
				base.InternalAdd(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress));
				intPtr = win32_IP_ADDR_STRING.Next;
			}
		}

		// Token: 0x04002855 RID: 10325
		public static readonly Win32IPAddressCollection Empty = new Win32IPAddressCollection(new IntPtr[] { IntPtr.Zero });
	}
}
