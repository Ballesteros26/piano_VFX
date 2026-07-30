using System;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Listens for connections from TCP network clients.</summary>
	// Token: 0x020005D6 RID: 1494
	public class TcpListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class with the specified local endpoint.</summary>
		/// <param name="localEP">An <see cref="T:System.Net.IPEndPoint" /> that represents the local endpoint to which to bind the listener <see cref="T:System.Net.Sockets.Socket" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is null. </exception>
		// Token: 0x06002F2C RID: 12076 RVA: 0x000BA5F8 File Offset: 0x000B87F8
		public TcpListener(IPEndPoint localEP)
		{
			bool on = Logging.On;
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.m_ServerSocketEP = localEP;
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			bool on2 = Logging.On;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class that listens for incoming connection attempts on the specified local IP address and port number.</summary>
		/// <param name="localaddr">An <see cref="T:System.Net.IPAddress" /> that represents the local IP address. </param>
		/// <param name="port">The port on which to listen for incoming connection attempts. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localaddr" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x06002F2D RID: 12077 RVA: 0x000BA644 File Offset: 0x000B8844
		public TcpListener(IPAddress localaddr, int port)
		{
			bool on = Logging.On;
			if (localaddr == null)
			{
				throw new ArgumentNullException("localaddr");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_ServerSocketEP = new IPEndPoint(localaddr, port);
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			bool on2 = Logging.On;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class that listens on the specified port.</summary>
		/// <param name="port">The port on which to listen for incoming connection attempts. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		// Token: 0x06002F2E RID: 12078 RVA: 0x000BA6AC File Offset: 0x000B88AC
		[Obsolete("This method has been deprecated. Please use TcpListener(IPAddress localaddr, int port) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public TcpListener(int port)
		{
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_ServerSocketEP = new IPEndPoint(IPAddress.Any, port);
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		}

		/// <summary>Creates a new <see cref="T:System.Net.Sockets.TcpListener" /> instance to listen on the specified port.</summary>
		/// <returns>Returns <see cref="T:System.Net.Sockets.TcpListener" />.A new <see cref="T:System.Net.Sockets.TcpListener" /> instance to listen on the specified port.</returns>
		/// <param name="port">The port on which to listen for incoming connection attempts.</param>
		// Token: 0x06002F2F RID: 12079 RVA: 0x000BA6FB File Offset: 0x000B88FB
		public static TcpListener Create(int port)
		{
			bool on = Logging.On;
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			TcpListener tcpListener = new TcpListener(IPAddress.IPv6Any, port);
			tcpListener.Server.DualMode = true;
			bool on2 = Logging.On;
			return tcpListener;
		}

		/// <summary>Gets the underlying network <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The underlying <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000BA733 File Offset: 0x000B8933
		public Socket Server
		{
			get
			{
				return this.m_ServerSocket;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening for client connections.</summary>
		/// <returns>true if <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening; otherwise, false.</returns>
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000BA73B File Offset: 0x000B893B
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
		}

		/// <summary>Gets the underlying <see cref="T:System.Net.EndPoint" /> of the current <see cref="T:System.Net.Sockets.TcpListener" />.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> to which the <see cref="T:System.Net.Sockets.Socket" /> is bound.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x000BA743 File Offset: 0x000B8943
		public EndPoint LocalEndpoint
		{
			get
			{
				if (!this.m_Active)
				{
					return this.m_ServerSocketEP;
				}
				return this.m_ServerSocket.LocalEndPoint;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.TcpListener" /> allows only one underlying socket to listen to a specific port.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.TcpListener" /> allows only one <see cref="T:System.Net.Sockets.TcpListener" /> to listen to a specific port; otherwise, false. . The default is true for Windows Server 2003 and Windows XP Service Pack 2 and later, and false for all other versions.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.TcpListener" /> has been started. Call the <see cref="M:System.Net.Sockets.TcpListener.Stop" /> method and then set the <see cref="P:System.Net.Sockets.Socket.ExclusiveAddressUse" /> property.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x000BA75F File Offset: 0x000B895F
		// (set) Token: 0x06002F34 RID: 12084 RVA: 0x000BA76C File Offset: 0x000B896C
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.m_ServerSocket.ExclusiveAddressUse;
			}
			set
			{
				if (this.m_Active)
				{
					throw new InvalidOperationException(global::SR.GetString("The TcpListener must not be listening before performing this operation."));
				}
				this.m_ServerSocket.ExclusiveAddressUse = value;
				this.m_ExclusiveAddressUse = value;
			}
		}

		/// <summary>Enables or disables Network Address Translation (NAT) traversal on a <see cref="T:System.Net.Sockets.TcpListener" /> instance.</summary>
		/// <param name="allowed">A Boolean value that specifies whether to enable or disable NAT traversal.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.Sockets.TcpListener.AllowNatTraversal(System.Boolean)" /> method was called after calling the <see cref="M:System.Net.Sockets.TcpListener.Start" /> method</exception>
		// Token: 0x06002F35 RID: 12085 RVA: 0x000BA799 File Offset: 0x000B8999
		public void AllowNatTraversal(bool allowed)
		{
			if (this.m_Active)
			{
				throw new InvalidOperationException(global::SR.GetString("The TcpListener must not be listening before performing this operation."));
			}
			if (allowed)
			{
				this.m_ServerSocket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
				return;
			}
			this.m_ServerSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
		}

		/// <summary>Starts listening for incoming connection requests.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F36 RID: 12086 RVA: 0x000BA7D1 File Offset: 0x000B89D1
		public void Start()
		{
			this.Start(int.MaxValue);
		}

		/// <summary>Starts listening for incoming connection requests with a maximum number of pending connection.</summary>
		/// <param name="backlog">The maximum length of the pending connections queue.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The<paramref name=" backlog" /> parameter is less than zero or exceeds the maximum number of permitted connections.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F37 RID: 12087 RVA: 0x000BA7E0 File Offset: 0x000B89E0
		public void Start(int backlog)
		{
			if (backlog > 2147483647 || backlog < 0)
			{
				throw new ArgumentOutOfRangeException("backlog");
			}
			bool on = Logging.On;
			if (this.m_ServerSocket == null)
			{
				throw new InvalidOperationException(global::SR.GetString("The socket handle is not valid."));
			}
			if (this.m_Active)
			{
				bool on2 = Logging.On;
				return;
			}
			this.m_ServerSocket.Bind(this.m_ServerSocketEP);
			try
			{
				this.m_ServerSocket.Listen(backlog);
			}
			catch (SocketException)
			{
				this.Stop();
				throw;
			}
			this.m_Active = true;
			bool on3 = Logging.On;
		}

		/// <summary>Closes the listener.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F38 RID: 12088 RVA: 0x000BA878 File Offset: 0x000B8A78
		public void Stop()
		{
			bool on = Logging.On;
			if (this.m_ServerSocket != null)
			{
				this.m_ServerSocket.Close();
				this.m_ServerSocket = null;
			}
			this.m_Active = false;
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (this.m_ExclusiveAddressUse)
			{
				this.m_ServerSocket.ExclusiveAddressUse = true;
			}
			bool on2 = Logging.On;
		}

		/// <summary>Determines if there are pending connection requests.</summary>
		/// <returns>true if connections are pending; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002F39 RID: 12089 RVA: 0x000BA8DE File Offset: 0x000B8ADE
		public bool Pending()
		{
			if (!this.m_Active)
			{
				throw new InvalidOperationException(global::SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			return this.m_ServerSocket.Poll(0, SelectMode.SelectRead);
		}

		/// <summary>Accepts a pending connection request.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F3A RID: 12090 RVA: 0x000BA905 File Offset: 0x000B8B05
		public Socket AcceptSocket()
		{
			bool on = Logging.On;
			if (!this.m_Active)
			{
				throw new InvalidOperationException(global::SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			Socket socket = this.m_ServerSocket.Accept();
			bool on2 = Logging.On;
			return socket;
		}

		/// <summary>Accepts a pending connection request.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F3B RID: 12091 RVA: 0x000BA936 File Offset: 0x000B8B36
		public TcpClient AcceptTcpClient()
		{
			bool on = Logging.On;
			if (!this.m_Active)
			{
				throw new InvalidOperationException(global::SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			TcpClient tcpClient = new TcpClient(this.m_ServerSocket.Accept());
			bool on2 = Logging.On;
			return tcpClient;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous creation of the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object containing information about the accept operation. This object is passed to the <paramref name="callback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F3C RID: 12092 RVA: 0x000BA96C File Offset: 0x000B8B6C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAcceptSocket(AsyncCallback callback, object state)
		{
			bool on = Logging.On;
			if (!this.m_Active)
			{
				throw new InvalidOperationException(global::SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			IAsyncResult asyncResult = this.m_ServerSocket.BeginAccept(callback, state);
			bool on2 = Logging.On;
			return asyncResult;
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> to handle remote host communication.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" />.The <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)" />  method.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter was not created by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.Sockets.TcpListener.EndAcceptSocket(System.IAsyncResult)" /> method was previously called. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F3D RID: 12093 RVA: 0x000BA9A0 File Offset: 0x000B8BA0
		public Socket EndAcceptSocket(IAsyncResult asyncResult)
		{
			bool on = Logging.On;
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			SocketAsyncResult socketAsyncResult = asyncResult as SocketAsyncResult;
			object obj = ((socketAsyncResult == null) ? null : socketAsyncResult.socket);
			if (obj == null)
			{
				throw new ArgumentException(global::SR.GetString("The IAsyncResult object was not returned from the corresponding asynchronous method on this class."), "asyncResult");
			}
			Socket socket = obj.EndAccept(asyncResult);
			bool on2 = Logging.On;
			return socket;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous creation of the <see cref="T:System.Net.Sockets.TcpClient" />.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object containing information about the accept operation. This object is passed to the <paramref name="callback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F3E RID: 12094 RVA: 0x000BA96C File Offset: 0x000B8B6C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
		{
			bool on = Logging.On;
			if (!this.m_Active)
			{
				throw new InvalidOperationException(global::SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			IAsyncResult asyncResult = this.m_ServerSocket.BeginAccept(callback, state);
			bool on2 = Logging.On;
			return asyncResult;
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.TcpClient" /> to handle remote host communication.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TcpClient" />.The <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptTcpClient(System.AsyncCallback,System.Object)" /> method.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F3F RID: 12095 RVA: 0x000BA9F8 File Offset: 0x000B8BF8
		public TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
		{
			bool on = Logging.On;
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			SocketAsyncResult socketAsyncResult = asyncResult as SocketAsyncResult;
			object obj = ((socketAsyncResult == null) ? null : socketAsyncResult.socket);
			if (obj == null)
			{
				throw new ArgumentException(global::SR.GetString("The IAsyncResult object was not returned from the corresponding asynchronous method on this class."), "asyncResult");
			}
			Socket socket = obj.EndAccept(asyncResult);
			bool on2 = Logging.On;
			return new TcpClient(socket);
		}

		/// <summary>Accepts a pending connection request as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />. </exception>
		// Token: 0x06002F40 RID: 12096 RVA: 0x000BAA55 File Offset: 0x000B8C55
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Socket> AcceptSocketAsync()
		{
			return Task<Socket>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAcceptSocket), new Func<IAsyncResult, Socket>(this.EndAcceptSocket), null);
		}

		/// <summary>Accepts a pending connection request as an asynchronous operation. </summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error. </exception>
		// Token: 0x06002F41 RID: 12097 RVA: 0x000BAA7A File Offset: 0x000B8C7A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<TcpClient> AcceptTcpClientAsync()
		{
			return Task<TcpClient>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAcceptTcpClient), new Func<IAsyncResult, TcpClient>(this.EndAcceptTcpClient), null);
		}

		// Token: 0x040026FA RID: 9978
		private IPEndPoint m_ServerSocketEP;

		// Token: 0x040026FB RID: 9979
		private Socket m_ServerSocket;

		// Token: 0x040026FC RID: 9980
		private bool m_Active;

		// Token: 0x040026FD RID: 9981
		private bool m_ExclusiveAddressUse;
	}
}
