using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000675 RID: 1653
	internal class Win32UnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x06003480 RID: 13440 RVA: 0x000C358C File Offset: 0x000C178C
		public Win32UnicastIPAddressInformation(Win32_IP_ADAPTER_UNICAST_ADDRESS info)
		{
			this.info = info;
			IPAddress ipaddress = info.Address.GetIPAddress();
			if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
			{
				this.ipv4Mask = Win32UnicastIPAddressInformation.PrefixLengthToSubnetMask(info.OnLinkPrefixLength, ipaddress.AddressFamily);
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x000C35D3 File Offset: 0x000C17D3
		public override IPAddress Address
		{
			get
			{
				return this.info.Address.GetIPAddress();
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000C35E5 File Offset: 0x000C17E5
		public override bool IsDnsEligible
		{
			get
			{
				return this.info.LengthFlags.IsDnsEligible;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x000C35F7 File Offset: 0x000C17F7
		public override bool IsTransient
		{
			get
			{
				return this.info.LengthFlags.IsTransient;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000C3609 File Offset: 0x000C1809
		public override long AddressPreferredLifetime
		{
			get
			{
				return (long)((ulong)this.info.PreferredLifetime);
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000C3617 File Offset: 0x000C1817
		public override long AddressValidLifetime
		{
			get
			{
				return (long)((ulong)this.info.ValidLifetime);
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000C3625 File Offset: 0x000C1825
		public override long DhcpLeaseLifetime
		{
			get
			{
				return (long)((ulong)this.info.LeaseLifetime);
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000C3633 File Offset: 0x000C1833
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return this.info.DadState;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x000C3640 File Offset: 0x000C1840
		public override IPAddress IPv4Mask
		{
			get
			{
				if (this.Address.AddressFamily != AddressFamily.InterNetwork)
				{
					return IPAddress.Any;
				}
				return this.ipv4Mask;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x000C365C File Offset: 0x000C185C
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return this.info.PrefixOrigin;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600348A RID: 13450 RVA: 0x000C3669 File Offset: 0x000C1869
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return this.info.SuffixOrigin;
			}
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000C3678 File Offset: 0x000C1878
		private static IPAddress PrefixLengthToSubnetMask(byte prefixLength, AddressFamily family)
		{
			byte[] array;
			if (family == AddressFamily.InterNetwork)
			{
				array = new byte[4];
			}
			else
			{
				array = new byte[16];
			}
			for (int i = 0; i < (int)prefixLength; i++)
			{
				byte[] array2 = array;
				int num = i / 8;
				array2[num] |= (byte)(128 >> i % 8);
			}
			return new IPAddress(array);
		}

		// Token: 0x04002988 RID: 10632
		private Win32_IP_ADAPTER_UNICAST_ADDRESS info;

		// Token: 0x04002989 RID: 10633
		private IPAddress ipv4Mask;
	}
}
