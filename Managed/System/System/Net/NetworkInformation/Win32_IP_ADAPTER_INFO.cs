using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200067B RID: 1659
	internal struct Win32_IP_ADAPTER_INFO
	{
		// Token: 0x040029C9 RID: 10697
		private const int MAX_ADAPTER_NAME_LENGTH = 256;

		// Token: 0x040029CA RID: 10698
		private const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;

		// Token: 0x040029CB RID: 10699
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x040029CC RID: 10700
		public IntPtr Next;

		// Token: 0x040029CD RID: 10701
		public int ComboIndex;

		// Token: 0x040029CE RID: 10702
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string AdapterName;

		// Token: 0x040029CF RID: 10703
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string Description;

		// Token: 0x040029D0 RID: 10704
		public uint AddressLength;

		// Token: 0x040029D1 RID: 10705
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] Address;

		// Token: 0x040029D2 RID: 10706
		public uint Index;

		// Token: 0x040029D3 RID: 10707
		public uint Type;

		// Token: 0x040029D4 RID: 10708
		public uint DhcpEnabled;

		// Token: 0x040029D5 RID: 10709
		public IntPtr CurrentIpAddress;

		// Token: 0x040029D6 RID: 10710
		public Win32_IP_ADDR_STRING IpAddressList;

		// Token: 0x040029D7 RID: 10711
		public Win32_IP_ADDR_STRING GatewayList;

		// Token: 0x040029D8 RID: 10712
		public Win32_IP_ADDR_STRING DhcpServer;

		// Token: 0x040029D9 RID: 10713
		public bool HaveWins;

		// Token: 0x040029DA RID: 10714
		public Win32_IP_ADDR_STRING PrimaryWinsServer;

		// Token: 0x040029DB RID: 10715
		public Win32_IP_ADDR_STRING SecondaryWinsServer;

		// Token: 0x040029DC RID: 10716
		public long LeaseObtained;

		// Token: 0x040029DD RID: 10717
		public long LeaseExpires;
	}
}
