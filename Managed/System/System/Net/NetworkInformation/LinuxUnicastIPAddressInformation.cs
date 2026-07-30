using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000676 RID: 1654
	internal class LinuxUnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x0600348C RID: 13452 RVA: 0x000C36C7 File Offset: 0x000C18C7
		public LinuxUnicastIPAddressInformation(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000C36D6 File Offset: 0x000C18D6
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x000C36E0 File Offset: 0x000C18E0
		public override bool IsDnsEligible
		{
			get
			{
				byte[] addressBytes = this.address.GetAddressBytes();
				return addressBytes[0] != 169 || addressBytes[1] != 254;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x00004240 File Offset: 0x00002440
		[MonoTODO("Always returns false")]
		public override bool IsTransient
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x00004239 File Offset: 0x00002439
		public override long AddressPreferredLifetime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x00004239 File Offset: 0x00002439
		public override long AddressValidLifetime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x00004239 File Offset: 0x00002439
		public override long DhcpLeaseLifetime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x00004239 File Offset: 0x00002439
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000C3712 File Offset: 0x000C1912
		public override IPAddress IPv4Mask
		{
			get
			{
				if (this.Address.AddressFamily != AddressFamily.InterNetwork)
				{
					return IPAddress.Any;
				}
				if (this.ipv4Mask == null)
				{
					this.ipv4Mask = SystemNetworkInterface.GetNetMask(this.address);
				}
				return this.ipv4Mask;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x00004239 File Offset: 0x00002439
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x00004239 File Offset: 0x00002439
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0400298A RID: 10634
		private IPAddress address;

		// Token: 0x0400298B RID: 10635
		private IPAddress ipv4Mask;
	}
}
