using System;

namespace System.Net.Sockets
{
	/// <summary>Contains option values for joining an IPv6 multicast group.</summary>
	// Token: 0x020005BC RID: 1468
	public class IPv6MulticastOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class with the specified IP multicast group and the local interface address.</summary>
		/// <param name="group">The group <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="ifindex">The local interface address. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ifindex" /> is less than 0.-or- <paramref name="ifindex" /> is greater than 0x00000000FFFFFFFF. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x06002D9C RID: 11676 RVA: 0x000B4449 File Offset: 0x000B2649
		public IPv6MulticastOption(IPAddress group, long ifindex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (ifindex < 0L || ifindex > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("ifindex");
			}
			this.Group = group;
			this.InterfaceIndex = ifindex;
		}

		/// <summary>Initializes a new version of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class for the specified IP multicast group.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x06002D9D RID: 11677 RVA: 0x000B4482 File Offset: 0x000B2682
		public IPv6MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.InterfaceIndex = 0L;
		}

		/// <summary>Gets or sets the IP address of a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the Internet address of a multicast group.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x000B44A7 File Offset: 0x000B26A7
		// (set) Token: 0x06002D9F RID: 11679 RVA: 0x000B44AF File Offset: 0x000B26AF
		public IPAddress Group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Group = value;
			}
		}

		/// <summary>Gets or sets the interface index that is associated with a multicast group.</summary>
		/// <returns>A <see cref="T:System.UInt64" /> value that specifies the address of the interface.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that is specified for a set operation is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x000B44C6 File Offset: 0x000B26C6
		// (set) Token: 0x06002DA1 RID: 11681 RVA: 0x000B44CE File Offset: 0x000B26CE
		public long InterfaceIndex
		{
			get
			{
				return this.m_Interface;
			}
			set
			{
				if (value < 0L || value > (long)((ulong)(-1)))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_Interface = value;
			}
		}

		// Token: 0x040025DD RID: 9693
		private IPAddress m_Group;

		// Token: 0x040025DE RID: 9694
		private long m_Interface;
	}
}
