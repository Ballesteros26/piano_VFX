using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200067C RID: 1660
	internal struct Win32_MIB_IFROW
	{
		// Token: 0x040029DE RID: 10718
		private const int MAX_INTERFACE_NAME_LEN = 256;

		// Token: 0x040029DF RID: 10719
		private const int MAXLEN_PHYSADDR = 8;

		// Token: 0x040029E0 RID: 10720
		private const int MAXLEN_IFDESCR = 256;

		// Token: 0x040029E1 RID: 10721
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public char[] Name;

		// Token: 0x040029E2 RID: 10722
		public int Index;

		// Token: 0x040029E3 RID: 10723
		public NetworkInterfaceType Type;

		// Token: 0x040029E4 RID: 10724
		public int Mtu;

		// Token: 0x040029E5 RID: 10725
		public uint Speed;

		// Token: 0x040029E6 RID: 10726
		public int PhysAddrLen;

		// Token: 0x040029E7 RID: 10727
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysAddr;

		// Token: 0x040029E8 RID: 10728
		public uint AdminStatus;

		// Token: 0x040029E9 RID: 10729
		public uint OperStatus;

		// Token: 0x040029EA RID: 10730
		public uint LastChange;

		// Token: 0x040029EB RID: 10731
		public int InOctets;

		// Token: 0x040029EC RID: 10732
		public int InUcastPkts;

		// Token: 0x040029ED RID: 10733
		public int InNUcastPkts;

		// Token: 0x040029EE RID: 10734
		public int InDiscards;

		// Token: 0x040029EF RID: 10735
		public int InErrors;

		// Token: 0x040029F0 RID: 10736
		public int InUnknownProtos;

		// Token: 0x040029F1 RID: 10737
		public int OutOctets;

		// Token: 0x040029F2 RID: 10738
		public int OutUcastPkts;

		// Token: 0x040029F3 RID: 10739
		public int OutNUcastPkts;

		// Token: 0x040029F4 RID: 10740
		public int OutDiscards;

		// Token: 0x040029F5 RID: 10741
		public int OutErrors;

		// Token: 0x040029F6 RID: 10742
		public int OutQLen;

		// Token: 0x040029F7 RID: 10743
		public int DescrLen;

		// Token: 0x040029F8 RID: 10744
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public byte[] Descr;
	}
}
