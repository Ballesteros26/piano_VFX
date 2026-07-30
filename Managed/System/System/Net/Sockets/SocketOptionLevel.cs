using System;

namespace System.Net.Sockets
{
	/// <summary>Defines socket option levels for the <see cref="M:System.Net.Sockets.Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName,System.Int32)" /> and <see cref="M:System.Net.Sockets.Socket.GetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName)" /> methods.</summary>
	// Token: 0x020005D1 RID: 1489
	public enum SocketOptionLevel
	{
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply to all sockets.</summary>
		// Token: 0x040026B6 RID: 9910
		Socket = 65535,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to IP sockets.</summary>
		// Token: 0x040026B7 RID: 9911
		IP = 0,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to IPv6 sockets.</summary>
		// Token: 0x040026B8 RID: 9912
		IPv6 = 41,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to TCP sockets.</summary>
		// Token: 0x040026B9 RID: 9913
		Tcp = 6,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to UDP sockets.</summary>
		// Token: 0x040026BA RID: 9914
		Udp = 17
	}
}
