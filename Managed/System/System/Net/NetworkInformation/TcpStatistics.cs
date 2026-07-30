using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Transmission Control Protocol (TCP) statistical data.</summary>
	// Token: 0x0200061F RID: 1567
	public abstract class TcpStatistics
	{
		/// <summary>Gets the number of accepted Transmission Control Protocol (TCP) connection requests.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connection requests accepted.</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060031ED RID: 12781
		public abstract long ConnectionsAccepted { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) connection requests made by clients.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections initiated by clients.</returns>
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060031EE RID: 12782
		public abstract long ConnectionsInitiated { get; }

		/// <summary>Specifies the total number of Transmission Control Protocol (TCP) connections established.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of connections established.</returns>
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060031EF RID: 12783
		public abstract long CumulativeConnections { get; }

		/// <summary>Gets the number of current Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of current TCP connections.</returns>
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060031F0 RID: 12784
		public abstract long CurrentConnections { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) errors received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP errors received.</returns>
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060031F1 RID: 12785
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of failed Transmission Control Protocol (TCP) connection attempts.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of failed TCP connection attempts.</returns>
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060031F2 RID: 12786
		public abstract long FailedConnectionAttempts { get; }

		/// <summary>Gets the maximum number of supported Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections that can be supported.</returns>
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060031F3 RID: 12787
		public abstract long MaximumConnections { get; }

		/// <summary>Gets the maximum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the maximum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060031F4 RID: 12788
		public abstract long MaximumTransmissionTimeout { get; }

		/// <summary>Gets the minimum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the minimum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060031F5 RID: 12789
		public abstract long MinimumTransmissionTimeout { get; }

		/// <summary>Gets the number of RST packets received by Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of reset TCP connections.</returns>
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060031F6 RID: 12790
		public abstract long ResetConnections { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments received.</returns>
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060031F7 RID: 12791
		public abstract long SegmentsReceived { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments re-sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments retransmitted.</returns>
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060031F8 RID: 12792
		public abstract long SegmentsResent { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent.</returns>
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060031F9 RID: 12793
		public abstract long SegmentsSent { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent with the reset flag set.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent with the reset flag set.</returns>
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060031FA RID: 12794
		public abstract long ResetsSent { get; }
	}
}
