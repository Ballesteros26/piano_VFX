using System;
using System.Globalization;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as an IP address and a port number.</summary>
	// Token: 0x02000432 RID: 1074
	[Serializable]
	public class IPEndPoint : EndPoint
	{
		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x0007E963 File Offset: 0x0007CB63
		public override AddressFamily AddressFamily
		{
			get
			{
				return this.m_Address.AddressFamily;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">The IP address of the Internet host. </param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.-or- <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x06002070 RID: 8304 RVA: 0x0007E970 File Offset: 0x0007CB70
		public IPEndPoint(long address, int port)
		{
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_Port = port;
			this.m_Address = new IPAddress(address);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.-or- <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.-or- <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x06002071 RID: 8305 RVA: 0x0007E99E File Offset: 0x0007CB9E
		public IPEndPoint(IPAddress address, int port)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_Port = port;
			this.m_Address = address;
		}

		/// <summary>Gets or sets the IP address of the endpoint.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance containing the IP address of the endpoint.</returns>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x0007E9D5 File Offset: 0x0007CBD5
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x0007E9DD File Offset: 0x0007CBDD
		public IPAddress Address
		{
			get
			{
				return this.m_Address;
			}
			set
			{
				this.m_Address = value;
			}
		}

		/// <summary>Gets or sets the port number of the endpoint.</summary>
		/// <returns>An integer value in the range <see cref="F:System.Net.IPEndPoint.MinPort" /> to <see cref="F:System.Net.IPEndPoint.MaxPort" /> indicating the port number of the endpoint.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that was specified for a set operation is less than <see cref="F:System.Net.IPEndPoint.MinPort" /> or greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x0007E9E6 File Offset: 0x0007CBE6
		// (set) Token: 0x06002075 RID: 8309 RVA: 0x0007E9EE File Offset: 0x0007CBEE
		public int Port
		{
			get
			{
				return this.m_Port;
			}
			set
			{
				if (!ValidationHelper.ValidateTcpPort(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_Port = value;
			}
		}

		/// <summary>Returns the IP address and port number of the specified endpoint.</summary>
		/// <returns>A string containing the IP address and the port number of the specified endpoint (for example, 192.168.1.2:80).</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002076 RID: 8310 RVA: 0x0007EA0C File Offset: 0x0007CC0C
		public override string ToString()
		{
			string text;
			if (this.m_Address.AddressFamily == AddressFamily.InterNetworkV6)
			{
				text = "[{0}]:{1}";
			}
			else
			{
				text = "{0}:{1}";
			}
			return string.Format(text, this.m_Address.ToString(), this.Port.ToString(NumberFormatInfo.InvariantInfo));
		}

		/// <summary>Serializes endpoint information into a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>A <see cref="T:System.Net.SocketAddress" /> instance containing the socket address for the endpoint.</returns>
		// Token: 0x06002077 RID: 8311 RVA: 0x0007EA5A File Offset: 0x0007CC5A
		public override SocketAddress Serialize()
		{
			return new SocketAddress(this.Address, this.Port);
		}

		/// <summary>Creates an endpoint from a socket address.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> instance using the specified socket address.</returns>
		/// <param name="socketAddress">The <see cref="T:System.Net.SocketAddress" /> to use for the endpoint. </param>
		/// <exception cref="T:System.ArgumentException">The AddressFamily of <paramref name="socketAddress" /> is not equal to the AddressFamily of the current instance.-or- <paramref name="socketAddress" />.Size &lt; 8. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002078 RID: 8312 RVA: 0x0007EA70 File Offset: 0x0007CC70
		public override EndPoint Create(SocketAddress socketAddress)
		{
			if (socketAddress.Family != this.AddressFamily)
			{
				throw new ArgumentException(global::SR.GetString("The AddressFamily {0} is not valid for the {1} end point, use {2} instead.", new object[]
				{
					socketAddress.Family.ToString(),
					base.GetType().FullName,
					this.AddressFamily.ToString()
				}), "socketAddress");
			}
			if (socketAddress.Size < 8)
			{
				throw new ArgumentException(global::SR.GetString("The supplied {0} is an invalid size for the {1} end point.", new object[]
				{
					socketAddress.GetType().FullName,
					base.GetType().FullName
				}), "socketAddress");
			}
			return socketAddress.GetIPEndPoint();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="comparand">The specified <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Net.IPEndPoint" /> instance.</param>
		// Token: 0x06002079 RID: 8313 RVA: 0x0007EB28 File Offset: 0x0007CD28
		public override bool Equals(object comparand)
		{
			return comparand is IPEndPoint && ((IPEndPoint)comparand).m_Address.Equals(this.m_Address) && ((IPEndPoint)comparand).m_Port == this.m_Port;
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>An integer hash value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600207A RID: 8314 RVA: 0x0007EB61 File Offset: 0x0007CD61
		public override int GetHashCode()
		{
			return this.m_Address.GetHashCode() ^ this.m_Port;
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x0007EB75 File Offset: 0x0007CD75
		internal IPEndPoint Snapshot()
		{
			return new IPEndPoint(this.Address.Snapshot(), this.Port);
		}

		/// <summary>Specifies the minimum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. This field is read-only.</summary>
		// Token: 0x04001CA6 RID: 7334
		public const int MinPort = 0;

		/// <summary>Specifies the maximum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. The MaxPort value is set to 0x0000FFFF. This field is read-only.</summary>
		// Token: 0x04001CA7 RID: 7335
		public const int MaxPort = 65535;

		// Token: 0x04001CA8 RID: 7336
		private IPAddress m_Address;

		// Token: 0x04001CA9 RID: 7337
		private int m_Port;

		// Token: 0x04001CAA RID: 7338
		internal const int AnyPort = 0;

		// Token: 0x04001CAB RID: 7339
		internal static IPEndPoint Any = new IPEndPoint(IPAddress.Any, 0);

		// Token: 0x04001CAC RID: 7340
		internal static IPEndPoint IPv6Any = new IPEndPoint(IPAddress.IPv6Any, 0);
	}
}
