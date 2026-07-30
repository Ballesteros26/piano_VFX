using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the Transmission Control Protocol (TCP) connections on the local computer.</summary>
	// Token: 0x0200061D RID: 1565
	public abstract class TcpConnectionInformation
	{
		/// <summary>Gets the local endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the local computer.</returns>
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060031E9 RID: 12777
		public abstract IPEndPoint LocalEndPoint { get; }

		/// <summary>Gets the remote endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the remote computer.</returns>
		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060031EA RID: 12778
		public abstract IPEndPoint RemoteEndPoint { get; }

		/// <summary>Gets the state of this Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.TcpState" /> enumeration values.</returns>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060031EB RID: 12779
		public abstract TcpState State { get; }
	}
}
