using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200061B RID: 1563
	internal class SystemMulticastIPAddressInformation : MulticastIPAddressInformation
	{
		// Token: 0x060031D9 RID: 12761 RVA: 0x000BE3A4 File Offset: 0x000BC5A4
		private SystemMulticastIPAddressInformation()
		{
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000BE3AC File Offset: 0x000BC5AC
		public SystemMulticastIPAddressInformation(SystemIPAddressInformation addressInfo)
		{
			this.innerInfo = addressInfo;
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000BE3BB File Offset: 0x000BC5BB
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000BE3C8 File Offset: 0x000BC5C8
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x000BE3D5 File Offset: 0x000BC5D5
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x00004240 File Offset: 0x00002440
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return PrefixOrigin.Other;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x00004240 File Offset: 0x00002440
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return SuffixOrigin.Other;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x00004240 File Offset: 0x00002440
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return DuplicateAddressDetectionState.Invalid;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x00045828 File Offset: 0x00043A28
		public override long AddressValidLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x00045828 File Offset: 0x00043A28
		public override long AddressPreferredLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x00045828 File Offset: 0x00043A28
		public override long DhcpLeaseLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000BE3E4 File Offset: 0x000BC5E4
		internal static MulticastIPAddressInformationCollection ToMulticastIpAddressInformationCollection(IPAddressInformationCollection addresses)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			foreach (IPAddressInformation ipaddressInformation in addresses)
			{
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation((SystemIPAddressInformation)ipaddressInformation));
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x0400281F RID: 10271
		private SystemIPAddressInformation innerInfo;
	}
}
