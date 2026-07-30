using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4).</summary>
	// Token: 0x02000602 RID: 1538
	public abstract class IPv4InterfaceProperties
	{
		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether an interface uses Windows Internet Name Service (WINS).</summary>
		/// <returns>true if the interface uses WINS; otherwise, false.</returns>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600312E RID: 12590
		public abstract bool UsesWins { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface is configured to use a Dynamic Host Configuration Protocol (DHCP) server to obtain an IP address.</summary>
		/// <returns>true if the interface is configured to obtain an IP address from a DHCP server; otherwise, false.</returns>
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600312F RID: 12591
		public abstract bool IsDhcpEnabled { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has an automatic private IP addressing (APIPA) address.</summary>
		/// <returns>true if the interface uses an APIPA address; otherwise, false.</returns>
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003130 RID: 12592
		public abstract bool IsAutomaticPrivateAddressingActive { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has automatic private IP addressing (APIPA) enabled.</summary>
		/// <returns>true if the interface uses APIPA; otherwise, false.</returns>
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06003131 RID: 12593
		public abstract bool IsAutomaticPrivateAddressingEnabled { get; }

		/// <summary>Gets the index of the network interface associated with the Internet Protocol version 4 (IPv4) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the index of the IPv4 interface.</returns>
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06003132 RID: 12594
		public abstract int Index { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface can forward (route) packets.</summary>
		/// <returns>true if this interface routes packets; otherwise false.</returns>
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06003133 RID: 12595
		public abstract bool IsForwardingEnabled { get; }

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06003134 RID: 12596
		public abstract int Mtu { get; }
	}
}
