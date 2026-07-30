using System;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Provides User Datagram Protocol (UDP) network services.</summary>
	// Token: 0x020005D8 RID: 1496
	public class UdpClient : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F42 RID: 12098 RVA: 0x000BAA9F File Offset: 0x000B8C9F
		public UdpClient()
			: this(AddressFamily.InterNetwork)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class.</summary>
		/// <param name="family">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values that specifies the addressing scheme of the socket. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="family" /> is not <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F43 RID: 12099 RVA: 0x000BAAA8 File Offset: 0x000B8CA8
		public UdpClient(AddressFamily family)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(global::SR.GetString("'{0}' Client can only accept InterNetwork or InterNetworkV6 addresses.", new object[] { "UDP" }), "family");
			}
			this.m_Family = family;
			this.createClientSocket();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the local port number provided.</summary>
		/// <param name="port">The local port number from which you intend to communicate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> parameter is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" /> or less than <see cref="F:System.Net.IPEndPoint.MinPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F44 RID: 12100 RVA: 0x000BAB0B File Offset: 0x000B8D0B
		public UdpClient(int port)
			: this(port, AddressFamily.InterNetwork)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the local port number provided.</summary>
		/// <param name="port">The port on which to listen for incoming connection attempts. </param>
		/// <param name="family">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values that specifies the addressing scheme of the socket. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="family" /> is not <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" /> or less than <see cref="F:System.Net.IPEndPoint.MinPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F45 RID: 12101 RVA: 0x000BAB18 File Offset: 0x000B8D18
		public UdpClient(int port, AddressFamily family)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(global::SR.GetString("'{0}' Client can only accept InterNetwork or InterNetworkV6 addresses."), "family");
			}
			this.m_Family = family;
			IPEndPoint ipendPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				ipendPoint = new IPEndPoint(IPAddress.Any, port);
			}
			else
			{
				ipendPoint = new IPEndPoint(IPAddress.IPv6Any, port);
			}
			this.createClientSocket();
			this.Client.Bind(ipendPoint);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the specified local endpoint.</summary>
		/// <param name="localEP">An <see cref="T:System.Net.IPEndPoint" /> that respresents the local endpoint to which you bind the UDP connection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F46 RID: 12102 RVA: 0x000BABB0 File Offset: 0x000B8DB0
		public UdpClient(IPEndPoint localEP)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.m_Family = localEP.AddressFamily;
			this.createClientSocket();
			this.Client.Bind(localEP);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and establishes a default remote host.</summary>
		/// <param name="hostname">The name of the remote DNS host to which you intend to connect. </param>
		/// <param name="port">The remote port number to which you intend to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostname" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F47 RID: 12103 RVA: 0x000BAC08 File Offset: 0x000B8E08
		public UdpClient(string hostname, int port)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.Connect(hostname, port);
		}

		/// <summary>Gets or sets the underlying network <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The underlying Network <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000BAC5B File Offset: 0x000B8E5B
		// (set) Token: 0x06002F49 RID: 12105 RVA: 0x000BAC63 File Offset: 0x000B8E63
		public Socket Client
		{
			get
			{
				return this.m_ClientSocket;
			}
			set
			{
				this.m_ClientSocket = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a default remote host has been established.</summary>
		/// <returns>true if a connection is active; otherwise, false.</returns>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x000BAC6C File Offset: 0x000B8E6C
		// (set) Token: 0x06002F4B RID: 12107 RVA: 0x000BAC74 File Offset: 0x000B8E74
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
			}
		}

		/// <summary>Gets the amount of data received from the network that is available to read.</summary>
		/// <returns>The number of bytes of data received from the network.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000BAC7D File Offset: 0x000B8E7D
		public int Available
		{
			get
			{
				return this.m_ClientSocket.Available;
			}
		}

		/// <summary>Gets or sets a value that specifies the Time to Live (TTL) value of Internet Protocol (IP) packets sent by the <see cref="T:System.Net.Sockets.UdpClient" />.</summary>
		/// <returns>The TTL value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x000BAC8A File Offset: 0x000B8E8A
		// (set) Token: 0x06002F4E RID: 12110 RVA: 0x000BAC97 File Offset: 0x000B8E97
		public short Ttl
		{
			get
			{
				return this.m_ClientSocket.Ttl;
			}
			set
			{
				this.m_ClientSocket.Ttl = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.UdpClient" /> allows Internet Protocol (IP) datagrams to be fragmented.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.UdpClient" /> allows datagram fragmentation; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.NotSupportedException">This property can be set only for sockets that use the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> flag or the <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> flag. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x000BACA5 File Offset: 0x000B8EA5
		// (set) Token: 0x06002F50 RID: 12112 RVA: 0x000BACB2 File Offset: 0x000B8EB2
		public bool DontFragment
		{
			get
			{
				return this.m_ClientSocket.DontFragment;
			}
			set
			{
				this.m_ClientSocket.DontFragment = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether outgoing multicast packets are delivered to the sending application.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.UdpClient" /> receives outgoing multicast packets; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x000BACC0 File Offset: 0x000B8EC0
		// (set) Token: 0x06002F52 RID: 12114 RVA: 0x000BACCD File Offset: 0x000B8ECD
		public bool MulticastLoopback
		{
			get
			{
				return this.m_ClientSocket.MulticastLoopback;
			}
			set
			{
				this.m_ClientSocket.MulticastLoopback = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.UdpClient" /> may send or receive broadcast packets.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.UdpClient" /> allows broadcast packets; otherwise, false. The default is false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000BACDB File Offset: 0x000B8EDB
		// (set) Token: 0x06002F54 RID: 12116 RVA: 0x000BACE8 File Offset: 0x000B8EE8
		public bool EnableBroadcast
		{
			get
			{
				return this.m_ClientSocket.EnableBroadcast;
			}
			set
			{
				this.m_ClientSocket.EnableBroadcast = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.UdpClient" /> allows only one client to use a port.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.UdpClient" /> allows only one client to use a specific port; otherwise, false. The default is true for Windows Server 2003 and Windows XP Service Pack 2 and later, and false for all other versions.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002F55 RID: 12117 RVA: 0x000BACF6 File Offset: 0x000B8EF6
		// (set) Token: 0x06002F56 RID: 12118 RVA: 0x000BAD03 File Offset: 0x000B8F03
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.m_ClientSocket.ExclusiveAddressUse;
			}
			set
			{
				this.m_ClientSocket.ExclusiveAddressUse = value;
			}
		}

		/// <summary>Enables or disables Network Address Translation (NAT) traversal on a <see cref="T:System.Net.Sockets.UdpClient" /> instance.</summary>
		/// <param name="allowed">A Boolean value that specifies whether to enable or disable NAT traversal.</param>
		// Token: 0x06002F57 RID: 12119 RVA: 0x000BAD11 File Offset: 0x000B8F11
		public void AllowNatTraversal(bool allowed)
		{
			if (allowed)
			{
				this.m_ClientSocket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
				return;
			}
			this.m_ClientSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
		}

		/// <summary>Closes the UDP connection.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002F58 RID: 12120 RVA: 0x000BAD31 File Offset: 0x000B8F31
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000BAD3C File Offset: 0x000B8F3C
		private void FreeResources()
		{
			if (this.m_CleanedUp)
			{
				return;
			}
			Socket client = this.Client;
			if (client != null)
			{
				client.InternalShutdown(SocketShutdown.Both);
				client.Close();
				this.Client = null;
			}
			this.m_CleanedUp = true;
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000BAD31 File Offset: 0x000B8F31
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.UdpClient" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002F5B RID: 12123 RVA: 0x000BAD77 File Offset: 0x000B8F77
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.FreeResources();
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Establishes a default remote host using the specified host name and port number.</summary>
		/// <param name="hostname">The DNS name of the remote host to which you intend send data. </param>
		/// <param name="port">The port number on the remote host to which you intend to send data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F5C RID: 12124 RVA: 0x000BAD88 File Offset: 0x000B8F88
		public void Connect(string hostname, int port)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			Exception ex = null;
			Socket socket = null;
			Socket socket2 = null;
			try
			{
				if (this.m_ClientSocket == null)
				{
					if (Socket.OSSupportsIPv4)
					{
						socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
					}
					if (Socket.OSSupportsIPv6)
					{
						socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
					}
				}
				foreach (IPAddress ipaddress in hostAddresses)
				{
					try
					{
						if (this.m_ClientSocket == null)
						{
							if (ipaddress.AddressFamily == AddressFamily.InterNetwork && socket2 != null)
							{
								socket2.Connect(ipaddress, port);
								this.m_ClientSocket = socket2;
								if (socket != null)
								{
									socket.Close();
								}
							}
							else if (socket != null)
							{
								socket.Connect(ipaddress, port);
								this.m_ClientSocket = socket;
								if (socket2 != null)
								{
									socket2.Close();
								}
							}
							this.m_Family = ipaddress.AddressFamily;
							this.m_Active = true;
							break;
						}
						if (ipaddress.AddressFamily == this.m_Family)
						{
							this.Connect(new IPEndPoint(ipaddress, port));
							this.m_Active = true;
							break;
						}
					}
					catch (Exception ex2)
					{
						if (NclUtilities.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
				}
			}
			catch (Exception ex3)
			{
				if (NclUtilities.IsFatal(ex3))
				{
					throw;
				}
				ex = ex3;
			}
			finally
			{
				if (!this.m_Active)
				{
					if (socket != null)
					{
						socket.Close();
					}
					if (socket2 != null)
					{
						socket2.Close();
					}
					if (ex != null)
					{
						throw ex;
					}
					throw new SocketException(SocketError.NotConnected);
				}
			}
		}

		/// <summary>Establishes a default remote host using the specified IP address and port number.</summary>
		/// <param name="addr">The <see cref="T:System.Net.IPAddress" /> of the remote host to which you intend to send data. </param>
		/// <param name="port">The port number to which you intend send data. </param>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addr" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F5D RID: 12125 RVA: 0x000BAF24 File Offset: 0x000B9124
		public void Connect(IPAddress addr, int port)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (addr == null)
			{
				throw new ArgumentNullException("addr");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IPEndPoint ipendPoint = new IPEndPoint(addr, port);
			this.Connect(ipendPoint);
		}

		/// <summary>Establishes a default remote host using the specified network endpoint.</summary>
		/// <param name="endPoint">An <see cref="T:System.Net.IPEndPoint" /> that specifies the network endpoint to which you intend to send data. </param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="endPoint" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F5E RID: 12126 RVA: 0x000BAF7C File Offset: 0x000B917C
		public void Connect(IPEndPoint endPoint)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			this.CheckForBroadcast(endPoint.Address);
			this.Client.Connect(endPoint);
			this.m_Active = true;
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000BAFCF File Offset: 0x000B91CF
		private void CheckForBroadcast(IPAddress ipAddress)
		{
			if (this.Client != null && !this.m_IsBroadcast && ipAddress.IsBroadcast)
			{
				this.m_IsBroadcast = true;
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
			}
		}

		/// <summary>Sends a UDP datagram to the host at the specified remote endpoint.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="dgram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send, represented as an array of bytes. </param>
		/// <param name="bytes">The number of bytes in the datagram. </param>
		/// <param name="endPoint">An <see cref="T:System.Net.IPEndPoint" /> that represents the host and port to which to send the datagram. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F60 RID: 12128 RVA: 0x000BB004 File Offset: 0x000B9204
		public int Send(byte[] dgram, int bytes, IPEndPoint endPoint)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (this.m_Active && endPoint != null)
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot send packets to an arbitrary host while connected."));
			}
			if (endPoint == null)
			{
				return this.Client.Send(dgram, 0, bytes, SocketFlags.None);
			}
			this.CheckForBroadcast(endPoint.Address);
			return this.Client.SendTo(dgram, 0, bytes, SocketFlags.None, endPoint);
		}

		/// <summary>Sends a UDP datagram to a specified port on a specified remote host.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="dgram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes. </param>
		/// <param name="bytes">The number of bytes in the datagram. </param>
		/// <param name="hostname">The name of the remote host to which you intend to send the datagram. </param>
		/// <param name="port">The remote port number with which you intend to communicate. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F61 RID: 12129 RVA: 0x000BB084 File Offset: 0x000B9284
		public int Send(byte[] dgram, int bytes, string hostname, int port)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (this.m_Active && (hostname != null || port != 0))
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot send packets to an arbitrary host while connected."));
			}
			if (hostname == null || port == 0)
			{
				return this.Client.Send(dgram, 0, bytes, SocketFlags.None);
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			int num = 0;
			while (num < hostAddresses.Length && hostAddresses[num].AddressFamily != this.m_Family)
			{
				num++;
			}
			if (hostAddresses.Length == 0 || num == hostAddresses.Length)
			{
				throw new ArgumentException(global::SR.GetString("None of the discovered or specified addresses match the socket address family."), "hostname");
			}
			this.CheckForBroadcast(hostAddresses[num]);
			IPEndPoint ipendPoint = new IPEndPoint(hostAddresses[num], port);
			return this.Client.SendTo(dgram, 0, bytes, SocketFlags.None, ipendPoint);
		}

		/// <summary>Sends a UDP datagram to a remote host.</summary>
		/// <returns>The number of bytes sent.</returns>
		/// <param name="dgram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes. </param>
		/// <param name="bytes">The number of bytes in the datagram. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F62 RID: 12130 RVA: 0x000BB158 File Offset: 0x000B9358
		public int Send(byte[] dgram, int bytes)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(global::SR.GetString("The operation is not allowed on non-connected sockets."));
			}
			return this.Client.Send(dgram, 0, bytes, SocketFlags.None);
		}

		/// <summary>Sends a datagram to a destination asynchronously. The destination is specified by a <see cref="T:System.Net.EndPoint" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous send.</returns>
		/// <param name="datagram">A <see cref="T:System.Byte" /> array that contains the data to be sent.</param>
		/// <param name="bytes">The number of bytes to send.</param>
		/// <param name="endPoint">The <see cref="T:System.Net.EndPoint" /> that represents the destination for the data.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the send operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		// Token: 0x06002F63 RID: 12131 RVA: 0x000BB1B4 File Offset: 0x000B93B4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, IPEndPoint endPoint, AsyncCallback requestCallback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (datagram == null)
			{
				throw new ArgumentNullException("datagram");
			}
			if (bytes > datagram.Length || bytes < 0)
			{
				throw new ArgumentOutOfRangeException("bytes");
			}
			if (this.m_Active && endPoint != null)
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot send packets to an arbitrary host while connected."));
			}
			if (endPoint == null)
			{
				return this.Client.BeginSend(datagram, 0, bytes, SocketFlags.None, requestCallback, state);
			}
			this.CheckForBroadcast(endPoint.Address);
			return this.Client.BeginSendTo(datagram, 0, bytes, SocketFlags.None, endPoint, requestCallback, state);
		}

		/// <summary>Sends a datagram to a destination asynchronously. The destination is specified by the host name and port number.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous send.</returns>
		/// <param name="datagram">A <see cref="T:System.Byte" /> array that contains the data to be sent.</param>
		/// <param name="bytes">The number of bytes to send.</param>
		/// <param name="hostname">The destination host.</param>
		/// <param name="port">The destination port number.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the send operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		// Token: 0x06002F64 RID: 12132 RVA: 0x000BB250 File Offset: 0x000B9450
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, string hostname, int port, AsyncCallback requestCallback, object state)
		{
			if (this.m_Active && (hostname != null || port != 0))
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot send packets to an arbitrary host while connected."));
			}
			IPEndPoint ipendPoint = null;
			if (hostname != null && port != 0)
			{
				IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
				int num = 0;
				while (num < hostAddresses.Length && hostAddresses[num].AddressFamily != this.m_Family)
				{
					num++;
				}
				if (hostAddresses.Length == 0 || num == hostAddresses.Length)
				{
					throw new ArgumentException(global::SR.GetString("None of the discovered or specified addresses match the socket address family."), "hostname");
				}
				this.CheckForBroadcast(hostAddresses[num]);
				ipendPoint = new IPEndPoint(hostAddresses[num], port);
			}
			return this.BeginSend(datagram, bytes, ipendPoint, requestCallback, state);
		}

		/// <summary>Sends a datagram to a remote host asynchronously. The destination was specified previously by a call to <see cref="Overload:System.Net.Sockets.UdpClient.Connect" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous send.</returns>
		/// <param name="datagram">A <see cref="T:System.Byte" /> array that contains the data to be sent.</param>
		/// <param name="bytes">The number of bytes to send.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the send operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		// Token: 0x06002F65 RID: 12133 RVA: 0x000BB2EA File Offset: 0x000B94EA
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, AsyncCallback requestCallback, object state)
		{
			return this.BeginSend(datagram, bytes, null, requestCallback, state);
		}

		/// <summary>Ends a pending asynchronous send.</summary>
		/// <returns>If successful, the number of bytes sent to the <see cref="T:System.Net.Sockets.UdpClient" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object returned by a call to <see cref="Overload:System.Net.Sockets.UdpClient.BeginSend" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSend(System.IAsyncResult)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002F66 RID: 12134 RVA: 0x000BB2F8 File Offset: 0x000B94F8
		public int EndSend(IAsyncResult asyncResult)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.m_Active)
			{
				return this.Client.EndSend(asyncResult);
			}
			return this.Client.EndSendTo(asyncResult);
		}

		/// <summary>Returns a UDP datagram that was sent by a remote host.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains datagram data.</returns>
		/// <param name="remoteEP">An <see cref="T:System.Net.IPEndPoint" /> that represents the remote host from which the data was sent. </param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" />  has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F67 RID: 12135 RVA: 0x000BB334 File Offset: 0x000B9534
		public byte[] Receive(ref IPEndPoint remoteEP)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				endPoint = IPEndPoint.Any;
			}
			else
			{
				endPoint = IPEndPoint.IPv6Any;
			}
			int num = this.Client.ReceiveFrom(this.m_Buffer, 65536, SocketFlags.None, ref endPoint);
			remoteEP = (IPEndPoint)endPoint;
			if (num < 65536)
			{
				byte[] array = new byte[num];
				Buffer.BlockCopy(this.m_Buffer, 0, array, 0, num);
				return array;
			}
			return this.m_Buffer;
		}

		/// <summary>Receives a datagram from a remote host asynchronously.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous receive.</returns>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		// Token: 0x06002F68 RID: 12136 RVA: 0x000BB3BC File Offset: 0x000B95BC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceive(AsyncCallback requestCallback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				endPoint = IPEndPoint.Any;
			}
			else
			{
				endPoint = IPEndPoint.IPv6Any;
			}
			return this.Client.BeginReceiveFrom(this.m_Buffer, 0, 65536, SocketFlags.None, ref endPoint, requestCallback, state);
		}

		/// <summary>Ends a pending asynchronous receive.</summary>
		/// <returns>If successful, the number of bytes received. If unsuccessful, this method returns 0.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object returned by a call to <see cref="M:System.Net.Sockets.UdpClient.BeginReceive(System.AsyncCallback,System.Object)" />.</param>
		/// <param name="remoteEP">The specified remote endpoint.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.UdpClient.BeginReceive(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.UdpClient.EndReceive(System.IAsyncResult,System.Net.IPEndPoint@)" /> was previously called for the asynchronous read. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		// Token: 0x06002F69 RID: 12137 RVA: 0x000BB418 File Offset: 0x000B9618
		public byte[] EndReceive(IAsyncResult asyncResult, ref IPEndPoint remoteEP)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				endPoint = IPEndPoint.Any;
			}
			else
			{
				endPoint = IPEndPoint.IPv6Any;
			}
			int num = this.Client.EndReceiveFrom(asyncResult, ref endPoint);
			remoteEP = (IPEndPoint)endPoint;
			if (num < 65536)
			{
				byte[] array = new byte[num];
				Buffer.BlockCopy(this.m_Buffer, 0, array, 0, num);
				return array;
			}
			return this.m_Buffer;
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group.</summary>
		/// <param name="multicastAddr">The multicast <see cref="T:System.Net.IPAddress" /> of the group you want to join. </param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F6A RID: 12138 RVA: 0x000BB494 File Offset: 0x000B9694
		public void JoinMulticastGroup(IPAddress multicastAddr)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (multicastAddr.AddressFamily != this.m_Family)
			{
				throw new ArgumentException(global::SR.GetString("Multicast family is not the same as the family of the '{0}' Client.", new object[] { "UDP" }), "multicastAddr");
			}
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				MulticastOption multicastOption = new MulticastOption(multicastAddr);
				this.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);
				return;
			}
			IPv6MulticastOption pv6MulticastOption = new IPv6MulticastOption(multicastAddr);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, pv6MulticastOption);
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group.</summary>
		/// <param name="multicastAddr">The multicast <see cref="T:System.Net.IPAddress" /> of the group you want to join.</param>
		/// <param name="localAddress">The local <see cref="T:System.Net.IPAddress" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F6B RID: 12139 RVA: 0x000BB530 File Offset: 0x000B9730
		public void JoinMulticastGroup(IPAddress multicastAddr, IPAddress localAddress)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.m_Family != AddressFamily.InterNetwork)
			{
				throw new SocketException(SocketError.OperationNotSupported);
			}
			MulticastOption multicastOption = new MulticastOption(multicastAddr, localAddress);
			this.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group.</summary>
		/// <param name="ifindex">The interface index associated with the local IP address on which to join the multicast group.</param>
		/// <param name="multicastAddr">The multicast <see cref="T:System.Net.IPAddress" /> of the group you want to join. </param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F6C RID: 12140 RVA: 0x000BB584 File Offset: 0x000B9784
		public void JoinMulticastGroup(int ifindex, IPAddress multicastAddr)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (ifindex < 0)
			{
				throw new ArgumentException(global::SR.GetString("The specified value cannot be negative."), "ifindex");
			}
			if (this.m_Family != AddressFamily.InterNetworkV6)
			{
				throw new SocketException(SocketError.OperationNotSupported);
			}
			IPv6MulticastOption pv6MulticastOption = new IPv6MulticastOption(multicastAddr, (long)ifindex);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, pv6MulticastOption);
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group with the specified Time to Live (TTL).</summary>
		/// <param name="multicastAddr">The <see cref="T:System.Net.IPAddress" /> of the multicast group to join. </param>
		/// <param name="timeToLive">The Time to Live (TTL), measured in router hops. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The TTL provided is not between 0 and 255 </exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="multicastAddr" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F6D RID: 12141 RVA: 0x000BB600 File Offset: 0x000B9800
		public void JoinMulticastGroup(IPAddress multicastAddr, int timeToLive)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (!ValidationHelper.ValidateRange(timeToLive, 0, 255))
			{
				throw new ArgumentOutOfRangeException("timeToLive");
			}
			this.JoinMulticastGroup(multicastAddr);
			this.Client.SetSocketOption((this.m_Family == AddressFamily.InterNetwork) ? SocketOptionLevel.IP : SocketOptionLevel.IPv6, SocketOptionName.MulticastTimeToLive, timeToLive);
		}

		/// <summary>Leaves a multicast group.</summary>
		/// <param name="multicastAddr">The <see cref="T:System.Net.IPAddress" /> of the multicast group to leave. </param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="multicastAddr" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F6E RID: 12142 RVA: 0x000BB670 File Offset: 0x000B9870
		public void DropMulticastGroup(IPAddress multicastAddr)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (multicastAddr.AddressFamily != this.m_Family)
			{
				throw new ArgumentException(global::SR.GetString("Multicast family is not the same as the family of the '{0}' Client.", new object[] { "UDP" }), "multicastAddr");
			}
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				MulticastOption multicastOption = new MulticastOption(multicastAddr);
				this.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, multicastOption);
				return;
			}
			IPv6MulticastOption pv6MulticastOption = new IPv6MulticastOption(multicastAddr);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, pv6MulticastOption);
		}

		/// <summary>Leaves a multicast group.</summary>
		/// <param name="multicastAddr">The <see cref="T:System.Net.IPAddress" /> of the multicast group to leave. </param>
		/// <param name="ifindex">The local address of the multicast group to leave.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="multicastAddr" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F6F RID: 12143 RVA: 0x000BB70C File Offset: 0x000B990C
		public void DropMulticastGroup(IPAddress multicastAddr, int ifindex)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (ifindex < 0)
			{
				throw new ArgumentException(global::SR.GetString("The specified value cannot be negative."), "ifindex");
			}
			if (this.m_Family != AddressFamily.InterNetworkV6)
			{
				throw new SocketException(SocketError.OperationNotSupported);
			}
			IPv6MulticastOption pv6MulticastOption = new IPv6MulticastOption(multicastAddr, (long)ifindex);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, pv6MulticastOption);
		}

		/// <summary>Sends a UDP datagram asynchronously to a remote host.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="datagram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes.</param>
		/// <param name="bytes">The number of bytes in the datagram.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F70 RID: 12144 RVA: 0x000BB787 File Offset: 0x000B9987
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> SendAsync(byte[] datagram, int bytes)
		{
			return Task<int>.Factory.FromAsync<byte[], int>(new Func<byte[], int, AsyncCallback, object, IAsyncResult>(this.BeginSend), new Func<IAsyncResult, int>(this.EndSend), datagram, bytes, null);
		}

		/// <summary>Sends a UDP datagram asynchronously to a remote host.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="datagram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes.</param>
		/// <param name="bytes">The number of bytes in the datagram.</param>
		/// <param name="endPoint">An <see cref="T:System.Net.IPEndPoint" /> that represents the host and port to which to send the datagram.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F71 RID: 12145 RVA: 0x000BB7AE File Offset: 0x000B99AE
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> SendAsync(byte[] datagram, int bytes, IPEndPoint endPoint)
		{
			return Task<int>.Factory.FromAsync<byte[], int, IPEndPoint>(new Func<byte[], int, IPEndPoint, AsyncCallback, object, IAsyncResult>(this.BeginSend), new Func<IAsyncResult, int>(this.EndSend), datagram, bytes, endPoint, null);
		}

		/// <summary>Sends a UDP datagram asynchronously to a remote host.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="datagram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes.</param>
		/// <param name="bytes">The number of bytes in the datagram.</param>
		/// <param name="hostname">The name of the remote host to which you intend to send the datagram.</param>
		/// <param name="port">The remote port number with which you intend to communicate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F72 RID: 12146 RVA: 0x000BB7D8 File Offset: 0x000B99D8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> SendAsync(byte[] datagram, int bytes, string hostname, int port)
		{
			return Task<int>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginSend(datagram, bytes, hostname, port, callback, state), new Func<IAsyncResult, int>(this.EndSend), null);
		}

		/// <summary>Returns a UDP datagram asynchronously that was sent by a remote host.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" />  has been closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06002F73 RID: 12147 RVA: 0x000BB832 File Offset: 0x000B9A32
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<UdpReceiveResult> ReceiveAsync()
		{
			return Task<UdpReceiveResult>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginReceive(callback, state), delegate(IAsyncResult ar)
			{
				IPEndPoint ipendPoint = null;
				return new UdpReceiveResult(this.EndReceive(ar, ref ipendPoint), ipendPoint);
			}, null);
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000BB857 File Offset: 0x000B9A57
		private void createClientSocket()
		{
			this.Client = new Socket(this.m_Family, SocketType.Dgram, ProtocolType.Udp);
		}

		// Token: 0x04002705 RID: 9989
		private const int MaxUDPSize = 65536;

		// Token: 0x04002706 RID: 9990
		private Socket m_ClientSocket;

		// Token: 0x04002707 RID: 9991
		private bool m_Active;

		// Token: 0x04002708 RID: 9992
		private byte[] m_Buffer;

		// Token: 0x04002709 RID: 9993
		private AddressFamily m_Family;

		// Token: 0x0400270A RID: 9994
		private bool m_CleanedUp;

		// Token: 0x0400270B RID: 9995
		private bool m_IsBroadcast;
	}
}
