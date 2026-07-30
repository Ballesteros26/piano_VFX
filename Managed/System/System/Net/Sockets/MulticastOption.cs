using System;

namespace System.Net.Sockets
{
	/// <summary>Contains <see cref="T:System.Net.IPAddress" /> values used to join and drop multicast groups.</summary>
	// Token: 0x020005BB RID: 1467
	public class MulticastOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class with the specified IP multicast group address and local IP address associated with a network interface.</summary>
		/// <param name="group">The group <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="mcint">The local <see cref="T:System.Net.IPAddress" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null.-or- <paramref name="mcint" /> is null. </exception>
		// Token: 0x06002D93 RID: 11667 RVA: 0x000B435C File Offset: 0x000B255C
		public MulticastOption(IPAddress group, IPAddress mcint)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (mcint == null)
			{
				throw new ArgumentNullException("mcint");
			}
			this.Group = group;
			this.LocalAddress = mcint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class with the specified IP multicast group address and interface index.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group.</param>
		/// <param name="interfaceIndex">The index of the interface that is used to send and receive multicast packets.</param>
		// Token: 0x06002D94 RID: 11668 RVA: 0x000B438E File Offset: 0x000B258E
		public MulticastOption(IPAddress group, int interfaceIndex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (interfaceIndex < 0 || interfaceIndex > 16777215)
			{
				throw new ArgumentOutOfRangeException("interfaceIndex");
			}
			this.Group = group;
			this.ifIndex = interfaceIndex;
		}

		/// <summary>Initializes a new version of the <see cref="T:System.Net.Sockets.MulticastOption" /> class for the specified IP multicast group.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x06002D95 RID: 11669 RVA: 0x000B43C9 File Offset: 0x000B25C9
		public MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.LocalAddress = IPAddress.Any;
		}

		/// <summary>Gets or sets the IP address of a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the Internet address of a multicast group.</returns>
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000B43F1 File Offset: 0x000B25F1
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x000B43F9 File Offset: 0x000B25F9
		public IPAddress Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>Gets or sets the local address associated with a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the local address associated with a multicast group.</returns>
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000B4402 File Offset: 0x000B2602
		// (set) Token: 0x06002D99 RID: 11673 RVA: 0x000B440A File Offset: 0x000B260A
		public IPAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
			set
			{
				this.ifIndex = 0;
				this.localAddress = value;
			}
		}

		/// <summary>Gets or sets the index of the interface that is used to send and receive multicast packets. </summary>
		/// <returns>An integer that represents the index of a <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> array element.</returns>
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x000B441A File Offset: 0x000B261A
		// (set) Token: 0x06002D9B RID: 11675 RVA: 0x000B4422 File Offset: 0x000B2622
		public int InterfaceIndex
		{
			get
			{
				return this.ifIndex;
			}
			set
			{
				if (value < 0 || value > 16777215)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.localAddress = null;
				this.ifIndex = value;
			}
		}

		// Token: 0x040025DA RID: 9690
		private IPAddress group;

		// Token: 0x040025DB RID: 9691
		private IPAddress localAddress;

		// Token: 0x040025DC RID: 9692
		private int ifIndex;
	}
}
