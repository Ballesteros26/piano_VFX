using System;

namespace System.Net.Sockets
{
	/// <summary>Presents the packet information from a call to <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" />.</summary>
	// Token: 0x020005B8 RID: 1464
	public struct IPPacketInformation
	{
		// Token: 0x06002D87 RID: 11655 RVA: 0x000B4276 File Offset: 0x000B2476
		internal IPPacketInformation(IPAddress address, int networkInterface)
		{
			this.address = address;
			this.networkInterface = networkInterface;
		}

		/// <summary>Gets the origin information of the packet that was received as a result of calling the <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that indicates the origin information of the packet that was received as a result of calling the <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method. For packets that were sent from a unicast address, the <see cref="P:System.Net.Sockets.IPPacketInformation.Address" /> property will return the <see cref="T:System.Net.IPAddress" /> of the sender; for multicast or broadcast packets, the <see cref="P:System.Net.Sockets.IPPacketInformation.Address" /> property will return the multicast or broadcast <see cref="T:System.Net.IPAddress" />.</returns>
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000B4286 File Offset: 0x000B2486
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		/// <summary>Gets the network interface information that is associated with a call to <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value, which represents the index of the network interface. You can use this index with <see cref="M:System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces" /> to get more information about the relevant interface.</returns>
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x000B428E File Offset: 0x000B248E
		public int Interface
		{
			get
			{
				return this.networkInterface;
			}
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.IPPacketInformation" /> instances are equivalent.</summary>
		/// <returns>true if <paramref name="packetInformation1" /> and <paramref name="packetInformation2" /> are equal; otherwise, false.</returns>
		/// <param name="packetInformation1">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the left of the equality operator.</param>
		/// <param name="packetInformation2">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the right of the equality operator.</param>
		// Token: 0x06002D8A RID: 11658 RVA: 0x000B4296 File Offset: 0x000B2496
		public static bool operator ==(IPPacketInformation packetInformation1, IPPacketInformation packetInformation2)
		{
			return packetInformation1.Equals(packetInformation2);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.IPPacketInformation" /> instances are not equal.</summary>
		/// <returns>true if <paramref name="packetInformation1" /> and <paramref name="packetInformation2" /> are unequal; otherwise, false.</returns>
		/// <param name="packetInformation1">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the left of the inequality operator.</param>
		/// <param name="packetInformation2">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the right of the inequality operator.</param>
		// Token: 0x06002D8B RID: 11659 RVA: 0x000B42AB File Offset: 0x000B24AB
		public static bool operator !=(IPPacketInformation packetInformation1, IPPacketInformation packetInformation2)
		{
			return !packetInformation1.Equals(packetInformation2);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="comparand" /> is an instance of <see cref="T:System.Net.Sockets.IPPacketInformation" /> and equals the value of the instance; otherwise, false.</returns>
		/// <param name="comparand">The object to compare with this instance.</param>
		// Token: 0x06002D8C RID: 11660 RVA: 0x000B42C4 File Offset: 0x000B24C4
		public override bool Equals(object comparand)
		{
			if (comparand == null)
			{
				return false;
			}
			if (!(comparand is IPPacketInformation))
			{
				return false;
			}
			IPPacketInformation ippacketInformation = (IPPacketInformation)comparand;
			return this.address.Equals(ippacketInformation.address) && this.networkInterface == ippacketInformation.networkInterface;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>An Int32 hash code.</returns>
		// Token: 0x06002D8D RID: 11661 RVA: 0x000B430B File Offset: 0x000B250B
		public override int GetHashCode()
		{
			return this.address.GetHashCode() + this.networkInterface.GetHashCode();
		}

		// Token: 0x040025D1 RID: 9681
		private IPAddress address;

		// Token: 0x040025D2 RID: 9682
		private int networkInterface;
	}
}
