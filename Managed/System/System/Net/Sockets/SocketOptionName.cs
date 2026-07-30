using System;

namespace System.Net.Sockets
{
	/// <summary>Defines configuration option names.</summary>
	// Token: 0x020005D2 RID: 1490
	public enum SocketOptionName
	{
		/// <summary>Record debugging information.</summary>
		// Token: 0x040026BC RID: 9916
		Debug = 1,
		/// <summary>The socket is listening.</summary>
		// Token: 0x040026BD RID: 9917
		AcceptConnection,
		/// <summary>Allows the socket to be bound to an address that is already in use.</summary>
		// Token: 0x040026BE RID: 9918
		ReuseAddress = 4,
		/// <summary>Use keep-alives.</summary>
		// Token: 0x040026BF RID: 9919
		KeepAlive = 8,
		/// <summary>Do not route; send the packet directly to the interface addresses.</summary>
		// Token: 0x040026C0 RID: 9920
		DontRoute = 16,
		/// <summary>Permit sending broadcast messages on the socket.</summary>
		// Token: 0x040026C1 RID: 9921
		Broadcast = 32,
		/// <summary>Bypass hardware when possible.</summary>
		// Token: 0x040026C2 RID: 9922
		UseLoopback = 64,
		/// <summary>Linger on close if unsent data is present.</summary>
		// Token: 0x040026C3 RID: 9923
		Linger = 128,
		/// <summary>Receives out-of-band data in the normal data stream.</summary>
		// Token: 0x040026C4 RID: 9924
		OutOfBandInline = 256,
		/// <summary>Close the socket gracefully without lingering.</summary>
		// Token: 0x040026C5 RID: 9925
		DontLinger = -129,
		/// <summary>Enables a socket to be bound for exclusive access.</summary>
		// Token: 0x040026C6 RID: 9926
		ExclusiveAddressUse = -5,
		/// <summary>Specifies the total per-socket buffer space reserved for sends. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x040026C7 RID: 9927
		SendBuffer = 4097,
		/// <summary>Specifies the total per-socket buffer space reserved for receives. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x040026C8 RID: 9928
		ReceiveBuffer,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Send" /> operations.</summary>
		// Token: 0x040026C9 RID: 9929
		SendLowWater,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Receive" /> operations.</summary>
		// Token: 0x040026CA RID: 9930
		ReceiveLowWater,
		/// <summary>Send a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x040026CB RID: 9931
		SendTimeout,
		/// <summary>Receive a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x040026CC RID: 9932
		ReceiveTimeout,
		/// <summary>Get the error status and clear.</summary>
		// Token: 0x040026CD RID: 9933
		Error,
		/// <summary>Get the socket type.</summary>
		// Token: 0x040026CE RID: 9934
		Type,
		// Token: 0x040026CF RID: 9935
		ReuseUnicastPort = 12295,
		/// <summary>Not supported; will throw a <see cref="T:System.Net.Sockets.SocketException" /> if used.</summary>
		// Token: 0x040026D0 RID: 9936
		MaxConnections = 2147483647,
		/// <summary>Specifies the IP options to be inserted into outgoing datagrams.</summary>
		// Token: 0x040026D1 RID: 9937
		IPOptions = 1,
		/// <summary>Indicates that the application provides the IP header for outgoing datagrams.</summary>
		// Token: 0x040026D2 RID: 9938
		HeaderIncluded,
		/// <summary>Change the IP header type of the service field.</summary>
		// Token: 0x040026D3 RID: 9939
		TypeOfService,
		/// <summary>Set the IP header Time-to-Live field.</summary>
		// Token: 0x040026D4 RID: 9940
		IpTimeToLive,
		/// <summary>Set the interface for outgoing multicast packets.</summary>
		// Token: 0x040026D5 RID: 9941
		MulticastInterface = 9,
		/// <summary>An IP multicast Time to Live.</summary>
		// Token: 0x040026D6 RID: 9942
		MulticastTimeToLive,
		/// <summary>An IP multicast loopback.</summary>
		// Token: 0x040026D7 RID: 9943
		MulticastLoopback,
		/// <summary>Add an IP group membership.</summary>
		// Token: 0x040026D8 RID: 9944
		AddMembership,
		/// <summary>Drop an IP group membership.</summary>
		// Token: 0x040026D9 RID: 9945
		DropMembership,
		/// <summary>Do not fragment IP datagrams.</summary>
		// Token: 0x040026DA RID: 9946
		DontFragment,
		/// <summary>Join a source group.</summary>
		// Token: 0x040026DB RID: 9947
		AddSourceMembership,
		/// <summary>Drop a source group.</summary>
		// Token: 0x040026DC RID: 9948
		DropSourceMembership,
		/// <summary>Block data from a source.</summary>
		// Token: 0x040026DD RID: 9949
		BlockSource,
		/// <summary>Unblock a previously blocked source.</summary>
		// Token: 0x040026DE RID: 9950
		UnblockSource,
		/// <summary>Return information about received packets.</summary>
		// Token: 0x040026DF RID: 9951
		PacketInformation,
		/// <summary>Specifies the maximum number of router hops for an Internet Protocol version 6 (IPv6) packet. This is similar to Time to Live (TTL) for Internet Protocol version 4.</summary>
		// Token: 0x040026E0 RID: 9952
		HopLimit = 21,
		/// <summary>Enables restriction of a IPv6 socket to a specified scope, such as addresses with the same link local or site local prefix.This socket option enables applications to place access restrictions on IPv6 sockets. Such restrictions enable an application running on a private LAN to simply and robustly harden itself against external attacks. This socket option widens or narrows the scope of a listening socket, enabling unrestricted access from public and private users when appropriate, or restricting access only to the same site, as required. This socket option has defined protection levels specified in the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> enumeration.</summary>
		// Token: 0x040026E1 RID: 9953
		IPProtectionLevel = 23,
		/// <summary>Indicates if a socket created for the AF_INET6 address family is restricted to IPv6 communications only. Sockets created for the AF_INET6 address family may be used for both IPv6 and IPv4 communications. Some applications may want to restrict their use of a socket created for the AF_INET6 address family to IPv6 communications only. When this value is non-zero (the default on Windows), a socket created for the AF_INET6 address family can be used to send and receive IPv6 packets only. When this value is zero, a socket created for the AF_INET6 address family can be used to send and receive packets to and from an IPv6 address or an IPv4 address. Note that the ability to interact with an IPv4 address requires the use of IPv4 mapped addresses. This socket option is supported on Windows Vista or later.</summary>
		// Token: 0x040026E2 RID: 9954
		IPv6Only = 27,
		/// <summary>Disables the Nagle algorithm for send coalescing.</summary>
		// Token: 0x040026E3 RID: 9955
		NoDelay = 1,
		/// <summary>Use urgent data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x040026E4 RID: 9956
		BsdUrgent,
		/// <summary>Use expedited data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x040026E5 RID: 9957
		Expedited = 2,
		/// <summary>Send UDP datagrams with checksum set to zero.</summary>
		// Token: 0x040026E6 RID: 9958
		NoChecksum = 1,
		/// <summary>Set or get the UDP checksum coverage.</summary>
		// Token: 0x040026E7 RID: 9959
		ChecksumCoverage = 20,
		/// <summary>Updates an accepted socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_ACCEPT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x040026E8 RID: 9960
		UpdateAcceptContext = 28683,
		/// <summary>Updates a connected socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_CONNECT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x040026E9 RID: 9961
		UpdateConnectContext = 28688
	}
}
