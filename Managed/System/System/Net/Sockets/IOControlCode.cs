using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the IO control codes supported by the <see cref="M:System.Net.Sockets.Socket.IOControl(System.Int32,System.Byte[],System.Byte[])" /> method.</summary>
	// Token: 0x020005B7 RID: 1463
	public enum IOControlCode : long
	{
		/// <summary>Enable notification for when data is waiting to be received. This value is equal to the Winsock 2 FIOASYNC constant.</summary>
		// Token: 0x040025AF RID: 9647
		AsyncIO = 2147772029L,
		/// <summary>Control the blocking behavior of the socket. If the argument specified with this control code is zero, the socket is placed in blocking mode. If the argument is nonzero, the socket is placed in nonblocking mode. This value is equal to the Winsock 2 FIONBIO constant.</summary>
		// Token: 0x040025B0 RID: 9648
		NonBlockingIO,
		/// <summary>Return the number of bytes available for reading. This value is equal to the Winsock 2 FIONREAD constant.</summary>
		// Token: 0x040025B1 RID: 9649
		DataToRead = 1074030207L,
		/// <summary>Return information about out-of-band data waiting to be received. When using this control code on stream sockets, the return value indicates the number of bytes available.</summary>
		// Token: 0x040025B2 RID: 9650
		OobDataRead = 1074033415L,
		/// <summary>Associate this socket with the specified handle of a companion interface. Refer to the appropriate  protocol-specific annex in the Winsock 2 reference or documentation for the particular companion interface for additional details. It is recommended that the Component Object Model (COM) be used instead of this IOCTL to discover and track other interfaces that might be supported by a socket. This control code is present for backward compatibility with systems where COM is not available or cannot be used for some other reason. This value is equal to the Winsock 2 SIO_ASSOCIATE_HANDLE constant. </summary>
		// Token: 0x040025B3 RID: 9651
		AssociateHandle = 2281701377L,
		/// <summary>Replace the oldest queued datagram with an incoming datagram when the incoming message queues are full. This value is equal to the Winsock 2 SIO_ENABLE_CIRCULAR_QUEUEING constant.</summary>
		// Token: 0x040025B4 RID: 9652
		EnableCircularQueuing = 671088642L,
		/// <summary>Discard the contents of the sending queue. This value is equal to the Winsock 2 SIO_FLUSH constant.</summary>
		// Token: 0x040025B5 RID: 9653
		Flush = 671088644L,
		/// <summary>Return a SOCKADDR structure that contains the broadcast address for the address family of the current socket. The returned address can be used with the <see cref="Overload:System.Net.Sockets.Socket.SendTo" /> method. This value is equal to the Winsock 2 SIO_GET_BROADCAST_ADDRESS constant. This value can be used on User Datagram Protocol (UDP) sockets only.</summary>
		// Token: 0x040025B6 RID: 9654
		GetBroadcastAddress = 1207959557L,
		/// <summary>Obtain provider-specific functions that are not part of the Winsock specification. Functions are specified using their provider-assigned GUID. This value is equal to the Winsock 2 SIO_GET_EXTENSION_FUNCTION_POINTER constant.</summary>
		// Token: 0x040025B7 RID: 9655
		GetExtensionFunctionPointer = 3355443206L,
		/// <summary>Retrieve the QOS structure associated with the socket. This control is only supported on platforms that provide a QOS capable transport (Windows Me, Windows 2000, and later.) This value is equal to the Winsock 2 SIO_GET_QOS constant.</summary>
		// Token: 0x040025B8 RID: 9656
		GetQos,
		/// <summary>Return the Quality of Service (QOS) attributes for the socket group. This value is reserved for future use, and is equal to the Winsock 2 SIO_GET_GROUP_QOS constant. </summary>
		// Token: 0x040025B9 RID: 9657
		GetGroupQos,
		/// <summary>Control whether multicast data sent by the socket appears as incoming data in the sockets receive queue. This value is equal to the Winsock 2 SIO_MULTIPOINT_LOOPBACK constant.</summary>
		// Token: 0x040025BA RID: 9658
		MultipointLoopback = 2281701385L,
		/// <summary>Control the number of times a multicast packet can be forwarded by a router, also known as the Time to Live (TTL), or hop count. This value is equal to the Winsock 2 SIO_MULTICAST_SCOPE constant.</summary>
		// Token: 0x040025BB RID: 9659
		MulticastScope,
		/// <summary>Set the Quality of Service (QOS) attributes for the socket. QOS defines the bandwidth requirements for the socket. This control code is supported on Windows Me, Windows 2000, and later operating systems. This value is equal to the Winsock 2 SIO_SET_QOS constant.</summary>
		// Token: 0x040025BC RID: 9660
		SetQos,
		/// <summary>Set the Quality of Service (QOS) attributes for the socket group. This value is reserved for future use and is equal to the Winsock 2 SIO_SET_GROUP_QOS constant.</summary>
		// Token: 0x040025BD RID: 9661
		SetGroupQos,
		/// <summary>Return a handle for the socket that is valid in the context of a companion interface. This value is equal to the Winsock 2 SIO_TRANSLATE_HANDLE constant.</summary>
		// Token: 0x040025BE RID: 9662
		TranslateHandle = 3355443213L,
		/// <summary>Return the interface addresses that can be used to connect to the specified remote address. This value is equal to the Winsock 2 SIO_ROUTING_INTERFACE_QUERY constant.</summary>
		// Token: 0x040025BF RID: 9663
		RoutingInterfaceQuery = 3355443220L,
		/// <summary>Enable receiving notification when the local interface used to access a remote endpoint changes. This value is equal to the Winsock 2 SIO_ROUTING_INTERFACE_CHANGE constant.</summary>
		// Token: 0x040025C0 RID: 9664
		RoutingInterfaceChange = 2281701397L,
		/// <summary>Return the list of local interfaces that the socket can bind to. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_ADDRESS_LIST_QUERY constant.</summary>
		// Token: 0x040025C1 RID: 9665
		AddressListQuery = 1207959574L,
		/// <summary>Enable receiving notification when the list of local interfaces for the socket's protocol family changes. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_ADDRESS_LIST_CHANGE constant.</summary>
		// Token: 0x040025C2 RID: 9666
		AddressListChange = 671088663L,
		/// <summary>Retrieve the underlying provider's SOCKET handle. This handle can be used to receive plug-and-play event notification. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_QUERY_TARGET_PNP_HANDLE constant.</summary>
		// Token: 0x040025C3 RID: 9667
		QueryTargetPnpHandle = 1207959576L,
		/// <summary>Control whether the socket receives notification when a namespace query becomes invalid. This control code is supported on Windows XP and later operating systems. This value is equal to the Winsock 2 SIO_NSP_NOTIFY_CHANGE constant.</summary>
		// Token: 0x040025C4 RID: 9668
		NamespaceChange = 2281701401L,
		/// <summary>Sort the structure returned by the <see cref="F:System.Net.Sockets.IOControlCode.AddressListQuery" /> field and add scope ID information for IPv6 addresses. This control code is supported on Windows XP and later operating systems. This value is equal to the Winsock 2 SIO_ADDRESS_LIST_SORT constant.</summary>
		// Token: 0x040025C5 RID: 9669
		AddressListSort = 3355443225L,
		/// <summary>Enable receiving all IPv4 packets on the network. The socket must have address family <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />, the socket type must be <see cref="F:System.Net.Sockets.SocketType.Raw" />, and the protocol type must be <see cref="F:System.Net.Sockets.ProtocolType.IP" />. The current user must belong to the Administrators group on the local computer, and the socket must be bound to a specific port. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_RCVALL constant.</summary>
		// Token: 0x040025C6 RID: 9670
		ReceiveAll = 2550136833L,
		/// <summary>Enable receiving all multicast IPv4 packets on the network. These are packets with destination addresses in the range 224.0.0.0 through 239.255.255.255. The socket must have address family <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />, the socket type must be <see cref="F:System.Net.Sockets.SocketType.Raw" />, and the protocol type must be <see cref="F:System.Net.Sockets.ProtocolType.Udp" />. The current user must belong to the Administrators group on the local computer, and the socket must be bound to a specific port. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_RCVALL_MCAST constant.</summary>
		// Token: 0x040025C7 RID: 9671
		ReceiveAllMulticast,
		/// <summary>Enable receiving all Internet Group Management Protocol (IGMP) packets on the network. The socket must have address family <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />, the socket type must be <see cref="F:System.Net.Sockets.SocketType.Raw" />, and the protocol type must be <see cref="F:System.Net.Sockets.ProtocolType.Igmp" />. The current user must belong to the Administrators group on the local computer, and the socket must be bound to a specific port. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_RCVALL_IGMPMCAST constant.</summary>
		// Token: 0x040025C8 RID: 9672
		ReceiveAllIgmpMulticast,
		/// <summary>Control sending TCP keep-alive packets and the interval at which they are sent. This control code is supported on Windows 2000 and later operating systems. For additional information, see RFC 1122 section 4.2.3.6. This value is equal to the Winsock 2 SIO_KEEPALIVE_VALS constant.</summary>
		// Token: 0x040025C9 RID: 9673
		KeepAliveValues,
		/// <summary>This value is equal to the Winsock 2 SIO_ABSORB_RTRALERT constant.</summary>
		// Token: 0x040025CA RID: 9674
		AbsorbRouterAlert,
		/// <summary>Set the interface used for outgoing unicast packets. This value is equal to the Winsock 2 SIO_UCAST_IF constant.</summary>
		// Token: 0x040025CB RID: 9675
		UnicastInterface,
		/// <summary>This value is equal to the Winsock 2 SIO_LIMIT_BROADCASTS constant.</summary>
		// Token: 0x040025CC RID: 9676
		LimitBroadcasts,
		/// <summary>Bind the socket to a specified interface index. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_INDEX_BIND constant.</summary>
		// Token: 0x040025CD RID: 9677
		BindToInterface,
		/// <summary>Set the interface used for outgoing multicast packets. The interface is identified by its index. This control code is supported on Windows 2000 and later operating systems.  This value is equal to the Winsock 2 SIO_INDEX_MCASTIF constant.</summary>
		// Token: 0x040025CE RID: 9678
		MulticastInterface,
		/// <summary>Join a multicast group using an interface identified by its index. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_INDEX_ADD_MCAST constant.</summary>
		// Token: 0x040025CF RID: 9679
		AddMulticastGroupOnInterface,
		/// <summary>Remove the socket from a multicast group. This control code is supported on Windows 2000 and later operating systems. This value is equal to the Winsock 2 SIO_INDEX_ADD_MCAST constant.</summary>
		// Token: 0x040025D0 RID: 9680
		DeleteMulticastGroupFromInterface
	}
}
