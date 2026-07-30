using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000684 RID: 1668
	internal struct Win32_IP_ADAPTER_UNICAST_ADDRESS
	{
		// Token: 0x04002A10 RID: 10768
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002A11 RID: 10769
		public IntPtr Next;

		// Token: 0x04002A12 RID: 10770
		public Win32_SOCKET_ADDRESS Address;

		// Token: 0x04002A13 RID: 10771
		public PrefixOrigin PrefixOrigin;

		// Token: 0x04002A14 RID: 10772
		public SuffixOrigin SuffixOrigin;

		// Token: 0x04002A15 RID: 10773
		public DuplicateAddressDetectionState DadState;

		// Token: 0x04002A16 RID: 10774
		public uint ValidLifetime;

		// Token: 0x04002A17 RID: 10775
		public uint PreferredLifetime;

		// Token: 0x04002A18 RID: 10776
		public uint LeaseLifetime;

		// Token: 0x04002A19 RID: 10777
		public byte OnLinkPrefixLength;
	}
}
