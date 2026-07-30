using System;

namespace System.Net.Sockets
{
	/// <summary>Defines error codes for the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
	// Token: 0x020005CD RID: 1485
	public enum SocketError
	{
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> operation succeeded.</summary>
		// Token: 0x04002673 RID: 9843
		Success,
		/// <summary>An unspecified <see cref="T:System.Net.Sockets.Socket" /> error has occurred.</summary>
		// Token: 0x04002674 RID: 9844
		SocketError = -1,
		/// <summary>A blocking <see cref="T:System.Net.Sockets.Socket" /> call was canceled.</summary>
		// Token: 0x04002675 RID: 9845
		Interrupted = 10004,
		/// <summary>An attempt was made to access a <see cref="T:System.Net.Sockets.Socket" /> in a way that is forbidden by its access permissions.</summary>
		// Token: 0x04002676 RID: 9846
		AccessDenied = 10013,
		/// <summary>An invalid pointer address was detected by the underlying socket provider.</summary>
		// Token: 0x04002677 RID: 9847
		Fault,
		/// <summary>An invalid argument was supplied to a <see cref="T:System.Net.Sockets.Socket" /> member.</summary>
		// Token: 0x04002678 RID: 9848
		InvalidArgument = 10022,
		/// <summary>There are too many open sockets in the underlying socket provider.</summary>
		// Token: 0x04002679 RID: 9849
		TooManyOpenSockets = 10024,
		/// <summary>An operation on a nonblocking socket cannot be completed immediately.</summary>
		// Token: 0x0400267A RID: 9850
		WouldBlock = 10035,
		/// <summary>A blocking operation is in progress.</summary>
		// Token: 0x0400267B RID: 9851
		InProgress,
		/// <summary>The nonblocking <see cref="T:System.Net.Sockets.Socket" /> already has an operation in progress.</summary>
		// Token: 0x0400267C RID: 9852
		AlreadyInProgress,
		/// <summary>A <see cref="T:System.Net.Sockets.Socket" /> operation was attempted on a non-socket.</summary>
		// Token: 0x0400267D RID: 9853
		NotSocket,
		/// <summary>A required address was omitted from an operation on a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x0400267E RID: 9854
		DestinationAddressRequired,
		/// <summary>The datagram is too long.</summary>
		// Token: 0x0400267F RID: 9855
		MessageSize,
		/// <summary>The protocol type is incorrect for this <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04002680 RID: 9856
		ProtocolType,
		/// <summary>An unknown, invalid, or unsupported option or level was used with a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04002681 RID: 9857
		ProtocolOption,
		/// <summary>The protocol is not implemented or has not been configured.</summary>
		// Token: 0x04002682 RID: 9858
		ProtocolNotSupported,
		/// <summary>The support for the specified socket type does not exist in this address family.</summary>
		// Token: 0x04002683 RID: 9859
		SocketNotSupported,
		/// <summary>The address family is not supported by the protocol family.</summary>
		// Token: 0x04002684 RID: 9860
		OperationNotSupported,
		/// <summary>The protocol family is not implemented or has not been configured.</summary>
		// Token: 0x04002685 RID: 9861
		ProtocolFamilyNotSupported,
		/// <summary>The address family specified is not supported. This error is returned if the IPv6 address family was specified and the IPv6 stack is not installed on the local machine. This error is returned if the IPv4 address family was specified and the IPv4 stack is not installed on the local machine.</summary>
		// Token: 0x04002686 RID: 9862
		AddressFamilyNotSupported,
		/// <summary>Only one use of an address is normally permitted.</summary>
		// Token: 0x04002687 RID: 9863
		AddressAlreadyInUse,
		/// <summary>The selected IP address is not valid in this context.</summary>
		// Token: 0x04002688 RID: 9864
		AddressNotAvailable,
		/// <summary>The network is not available.</summary>
		// Token: 0x04002689 RID: 9865
		NetworkDown,
		/// <summary>No route to the remote host exists.</summary>
		// Token: 0x0400268A RID: 9866
		NetworkUnreachable,
		/// <summary>The application tried to set <see cref="F:System.Net.Sockets.SocketOptionName.KeepAlive" /> on a connection that has already timed out.</summary>
		// Token: 0x0400268B RID: 9867
		NetworkReset,
		/// <summary>The connection was aborted by the .NET Framework or the underlying socket provider.</summary>
		// Token: 0x0400268C RID: 9868
		ConnectionAborted,
		/// <summary>The connection was reset by the remote peer.</summary>
		// Token: 0x0400268D RID: 9869
		ConnectionReset,
		/// <summary>No free buffer space is available for a <see cref="T:System.Net.Sockets.Socket" /> operation.</summary>
		// Token: 0x0400268E RID: 9870
		NoBufferSpaceAvailable,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is already connected.</summary>
		// Token: 0x0400268F RID: 9871
		IsConnected,
		/// <summary>The application tried to send or receive data, and the <see cref="T:System.Net.Sockets.Socket" /> is not connected.</summary>
		// Token: 0x04002690 RID: 9872
		NotConnected,
		/// <summary>A request to send or receive data was disallowed because the <see cref="T:System.Net.Sockets.Socket" /> has already been closed.</summary>
		// Token: 0x04002691 RID: 9873
		Shutdown,
		/// <summary>The connection attempt timed out, or the connected host has failed to respond.</summary>
		// Token: 0x04002692 RID: 9874
		TimedOut = 10060,
		/// <summary>The remote host is actively refusing a connection.</summary>
		// Token: 0x04002693 RID: 9875
		ConnectionRefused,
		/// <summary>The operation failed because the remote host is down.</summary>
		// Token: 0x04002694 RID: 9876
		HostDown = 10064,
		/// <summary>There is no network route to the specified host.</summary>
		// Token: 0x04002695 RID: 9877
		HostUnreachable,
		/// <summary>Too many processes are using the underlying socket provider.</summary>
		// Token: 0x04002696 RID: 9878
		ProcessLimit = 10067,
		/// <summary>The network subsystem is unavailable.</summary>
		// Token: 0x04002697 RID: 9879
		SystemNotReady = 10091,
		/// <summary>The version of the underlying socket provider is out of range.</summary>
		// Token: 0x04002698 RID: 9880
		VersionNotSupported,
		/// <summary>The underlying socket provider has not been initialized.</summary>
		// Token: 0x04002699 RID: 9881
		NotInitialized,
		/// <summary>A graceful shutdown is in progress.</summary>
		// Token: 0x0400269A RID: 9882
		Disconnecting = 10101,
		/// <summary>The specified class was not found.</summary>
		// Token: 0x0400269B RID: 9883
		TypeNotFound = 10109,
		/// <summary>No such host is known. The name is not an official host name or alias.</summary>
		// Token: 0x0400269C RID: 9884
		HostNotFound = 11001,
		/// <summary>The name of the host could not be resolved. Try again later.</summary>
		// Token: 0x0400269D RID: 9885
		TryAgain,
		/// <summary>The error is unrecoverable or the requested database cannot be located.</summary>
		// Token: 0x0400269E RID: 9886
		NoRecovery,
		/// <summary>The requested name or IP address was not found on the name server.</summary>
		// Token: 0x0400269F RID: 9887
		NoData,
		/// <summary>The application has initiated an overlapped operation that cannot be completed immediately.</summary>
		// Token: 0x040026A0 RID: 9888
		IOPending = 997,
		/// <summary>The overlapped operation was aborted due to the closure of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x040026A1 RID: 9889
		OperationAborted = 995
	}
}
