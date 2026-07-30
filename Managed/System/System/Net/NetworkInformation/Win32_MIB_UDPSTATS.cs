using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000674 RID: 1652
	internal struct Win32_MIB_UDPSTATS
	{
		// Token: 0x04002983 RID: 10627
		public uint InDatagrams;

		// Token: 0x04002984 RID: 10628
		public uint NoPorts;

		// Token: 0x04002985 RID: 10629
		public uint InErrors;

		// Token: 0x04002986 RID: 10630
		public uint OutDatagrams;

		// Token: 0x04002987 RID: 10631
		public int NumAddrs;
	}
}
