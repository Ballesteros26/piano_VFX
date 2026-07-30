using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides User Datagram Protocol (UDP) statistical data.</summary>
	// Token: 0x02000620 RID: 1568
	public abstract class UdpStatistics
	{
		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were delivered to UDP users.</returns>
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060031FC RID: 12796
		public abstract long DatagramsReceived { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were sent.</returns>
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060031FD RID: 12797
		public abstract long DatagramsSent { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of port errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that were discarded because there was no listening application at the destination port.</returns>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060031FE RID: 12798
		public abstract long IncomingDatagramsDiscarded { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of errors other than bad port information.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that could not be delivered for reasons other than the lack of an application at the destination port.</returns>
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060031FF RID: 12799
		public abstract long IncomingDatagramsWithErrors { get; }

		/// <summary>Gets the number of local endpoints that are listening for User Datagram Protocol (UDP) datagrams.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of sockets that are listening for UDP datagrams.</returns>
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06003200 RID: 12800
		public abstract int UdpListeners { get; }
	}
}
