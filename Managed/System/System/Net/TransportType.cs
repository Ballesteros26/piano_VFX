using System;

namespace System.Net
{
	/// <summary>Defines transport types for the <see cref="T:System.Net.SocketPermission" /> and <see cref="T:System.Net.Sockets.Socket" /> classes.</summary>
	// Token: 0x02000462 RID: 1122
	public enum TransportType
	{
		/// <summary>UDP transport.</summary>
		// Token: 0x04001DFB RID: 7675
		Udp = 1,
		/// <summary>The transport type is connectionless, such as UDP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Udp" />.</summary>
		// Token: 0x04001DFC RID: 7676
		Connectionless = 1,
		/// <summary>TCP transport.</summary>
		// Token: 0x04001DFD RID: 7677
		Tcp,
		/// <summary>The transport is connection oriented, such as TCP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Tcp" />.</summary>
		// Token: 0x04001DFE RID: 7678
		ConnectionOriented = 2,
		/// <summary>All transport types.</summary>
		// Token: 0x04001DFF RID: 7679
		All
	}
}
