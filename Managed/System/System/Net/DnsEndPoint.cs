using System;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as a host name or a string representation of an IP address and a port number.</summary>
	// Token: 0x02000421 RID: 1057
	public class DnsEndPoint : EndPoint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsEndPoint" /> class with the host name or string representation of an IP address and a port number.</summary>
		/// <param name="host">The host name or a string representation of the IP address.</param>
		/// <param name="port">The port number associated with the address, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="host" /> parameter contains an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is a null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x0600201D RID: 8221 RVA: 0x0007D5B7 File Offset: 0x0007B7B7
		public DnsEndPoint(string host, int port)
			: this(host, port, AddressFamily.Unspecified)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsEndPoint" /> class with the host name or string representation of an IP address, a port number, and an address family.</summary>
		/// <param name="host">The host name or a string representation of the IP address.</param>
		/// <param name="port">The port number associated with the address, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <param name="addressFamily">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="host" /> parameter contains an empty string.-or- <paramref name="addressFamily" /> is <see cref="F:System.Net.Sockets.AddressFamily.Unknown" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is a null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x0600201E RID: 8222 RVA: 0x0007D5C4 File Offset: 0x0007B7C4
		public DnsEndPoint(string host, int port, AddressFamily addressFamily)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (string.IsNullOrEmpty(host))
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "host" }));
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (addressFamily != AddressFamily.InterNetwork && addressFamily != AddressFamily.InterNetworkV6 && addressFamily != AddressFamily.Unspecified)
			{
				throw new ArgumentException(global::SR.GetString("The specified value is not valid."), "addressFamily");
			}
			this.m_Host = host;
			this.m_Port = port;
			this.m_Family = addressFamily;
		}

		/// <summary>Compares two <see cref="T:System.Net.DnsEndPoint" /> objects.</summary>
		/// <returns>true if the two <see cref="T:System.Net.DnsEndPoint" /> instances are equal; otherwise, false.</returns>
		/// <param name="comparand">A <see cref="T:System.Net.DnsEndPoint" /> instance to compare to the current instance.</param>
		// Token: 0x0600201F RID: 8223 RVA: 0x0007D658 File Offset: 0x0007B858
		public override bool Equals(object comparand)
		{
			DnsEndPoint dnsEndPoint = comparand as DnsEndPoint;
			return dnsEndPoint != null && (this.m_Family == dnsEndPoint.m_Family && this.m_Port == dnsEndPoint.m_Port) && this.m_Host == dnsEndPoint.m_Host;
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>An integer hash value for the <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x06002020 RID: 8224 RVA: 0x0007D6A0 File Offset: 0x0007B8A0
		public override int GetHashCode()
		{
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.ToString());
		}

		/// <summary>Returns the host name or string representation of the IP address and port number of the <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>A string containing the address family, host name or IP address string, and the port number of the specified <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x06002021 RID: 8225 RVA: 0x0007D6B4 File Offset: 0x0007B8B4
		public override string ToString()
		{
			return string.Concat(new object[] { this.m_Family, "/", this.m_Host, ":", this.m_Port });
		}

		/// <summary>Gets the host name or string representation of the Internet Protocol (IP) address of the host.</summary>
		/// <returns>A host name or string representation of an IP address.</returns>
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0007D701 File Offset: 0x0007B901
		public string Host
		{
			get
			{
				return this.m_Host;
			}
		}

		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</returns>
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0007D709 File Offset: 0x0007B909
		public override AddressFamily AddressFamily
		{
			get
			{
				return this.m_Family;
			}
		}

		/// <summary>Gets the port number of the <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>An integer value in the range 0 to 0xffff indicating the port number of the <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0007D711 File Offset: 0x0007B911
		public int Port
		{
			get
			{
				return this.m_Port;
			}
		}

		// Token: 0x04001BDE RID: 7134
		private string m_Host;

		// Token: 0x04001BDF RID: 7135
		private int m_Port;

		// Token: 0x04001BE0 RID: 7136
		private AddressFamily m_Family;
	}
}
