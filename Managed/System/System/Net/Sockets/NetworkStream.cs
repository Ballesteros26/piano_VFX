using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Provides the underlying stream of data for network access.</summary>
	// Token: 0x020005BD RID: 1469
	public class NetworkStream : Stream
	{
		// Token: 0x06002DA2 RID: 11682 RVA: 0x000B44EC File Offset: 0x000B26EC
		internal NetworkStream()
		{
			this.m_OwnsSocket = true;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- The <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x06002DA3 RID: 11683 RVA: 0x000B4510 File Offset: 0x000B2710
		public NetworkStream(Socket socket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <param name="ownsSocket">Set to true to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- the value of the <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- the <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x06002DA4 RID: 11684 RVA: 0x000B4543 File Offset: 0x000B2743
		public NetworkStream(Socket socket, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
			this.m_OwnsSocket = ownsSocket;
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000B4580 File Offset: 0x000B2780
		internal NetworkStream(NetworkStream networkStream, bool ownsSocket)
		{
			Socket socket = networkStream.Socket;
			if (socket == null)
			{
				throw new ArgumentNullException("networkStream");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
			this.m_OwnsSocket = ownsSocket;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified access rights.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values that specify the type of access given to the <see cref="T:System.Net.Sockets.NetworkStream" /> over the provided <see cref="T:System.Net.Sockets.Socket" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- the <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- the <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x06002DA6 RID: 11686 RVA: 0x000B45CC File Offset: 0x000B27CC
		public NetworkStream(Socket socket, FileAccess access)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, access);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified access rights and the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values that specifies the type of access given to the <see cref="T:System.Net.Sockets.NetworkStream" /> over the provided <see cref="T:System.Net.Sockets.Socket" />. </param>
		/// <param name="ownsSocket">Set to true to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- The <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x06002DA7 RID: 11687 RVA: 0x000B45FF File Offset: 0x000B27FF
		public NetworkStream(Socket socket, FileAccess access, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, access);
			this.m_OwnsSocket = ownsSocket;
		}

		/// <summary>Gets the underlying <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> that represents the underlying network connection.</returns>
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000B4639 File Offset: 0x000B2839
		protected Socket Socket
		{
			get
			{
				return this.m_StreamSocket;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x000B4644 File Offset: 0x000B2844
		internal Socket InternalSocket
		{
			get
			{
				Socket streamSocket = this.m_StreamSocket;
				if (this.m_CleanedUp || streamSocket == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return streamSocket;
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000B4678 File Offset: 0x000B2878
		internal void InternalAbortSocket()
		{
			if (!this.m_OwnsSocket)
			{
				throw new InvalidOperationException();
			}
			Socket streamSocket = this.m_StreamSocket;
			if (this.m_CleanedUp || streamSocket == null)
			{
				return;
			}
			try
			{
				streamSocket.Close(0);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000B46C8 File Offset: 0x000B28C8
		internal void ConvertToNotSocketOwner()
		{
			this.m_OwnsSocket = false;
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> can be read.</summary>
		/// <returns>true to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> can be read; otherwise, false. The default value is true.</returns>
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x000B46D7 File Offset: 0x000B28D7
		// (set) Token: 0x06002DAD RID: 11693 RVA: 0x000B46DF File Offset: 0x000B28DF
		protected bool Readable
		{
			get
			{
				return this.m_Readable;
			}
			set
			{
				this.m_Readable = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> is writable.</summary>
		/// <returns>true if data can be written to the stream; otherwise, false. The default value is true.</returns>
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x000B46E8 File Offset: 0x000B28E8
		// (set) Token: 0x06002DAF RID: 11695 RVA: 0x000B46F0 File Offset: 0x000B28F0
		protected bool Writeable
		{
			get
			{
				return this.m_Writeable;
			}
			set
			{
				this.m_Writeable = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports reading.</summary>
		/// <returns>true if data can be read from the stream; otherwise, false. The default value is true.</returns>
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000B46D7 File Offset: 0x000B28D7
		public override bool CanRead
		{
			get
			{
				return this.m_Readable;
			}
		}

		/// <summary>Gets a value that indicates whether the stream supports seeking. This property is not currently supported.This property always returns false.</summary>
		/// <returns>false in all cases to indicate that <see cref="T:System.Net.Sockets.NetworkStream" /> cannot seek a specific location in the stream.</returns>
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports writing.</summary>
		/// <returns>true if data can be written to the <see cref="T:System.Net.Sockets.NetworkStream" />; otherwise, false. The default value is true.</returns>
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x000B46E8 File Offset: 0x000B28E8
		public override bool CanWrite
		{
			get
			{
				return this.m_Writeable;
			}
		}

		/// <summary>Indicates whether timeout properties are usable for <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the amount of time that a read operation blocks waiting for data. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a read operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the read operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x000B46FC File Offset: 0x000B28FC
		// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x000B472A File Offset: 0x000B292A
		public override int ReadTimeout
		{
			get
			{
				int num = (int)this.m_StreamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", global::SR.GetString("Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0."));
				}
				this.SetSocketTimeoutOption(SocketShutdown.Receive, value, false);
			}
		}

		/// <summary>Gets or sets the amount of time that a write operation blocks waiting for data. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a write operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the write operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000B4754 File Offset: 0x000B2954
		// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x000B4782 File Offset: 0x000B2982
		public override int WriteTimeout
		{
			get
			{
				int num = (int)this.m_StreamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", global::SR.GetString("Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0."));
				}
				this.SetSocketTimeoutOption(SocketShutdown.Send, value, false);
			}
		}

		/// <summary>Gets a value that indicates whether data is available on the <see cref="T:System.Net.Sockets.NetworkStream" /> to be read.</summary>
		/// <returns>true if data is available on the stream to be read; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code, and refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000B47AC File Offset: 0x000B29AC
		public virtual bool DataAvailable
		{
			get
			{
				if (this.m_CleanedUp)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				Socket streamSocket = this.m_StreamSocket;
				if (streamSocket == null)
				{
					throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
				}
				return streamSocket.Available != 0;
			}
		}

		/// <summary>Gets the length of the data available on the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The length of the data available on the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000B480A File Offset: 0x000B2A0A
		public override long Length
		{
			get
			{
				throw new NotSupportedException(global::SR.GetString("This stream does not support seek operations."));
			}
		}

		/// <summary>Gets or sets the current position in the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The current position in the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000B480A File Offset: 0x000B2A0A
		// (set) Token: 0x06002DBB RID: 11707 RVA: 0x000B480A File Offset: 0x000B2A0A
		public override long Position
		{
			get
			{
				throw new NotSupportedException(global::SR.GetString("This stream does not support seek operations."));
			}
			set
			{
				throw new NotSupportedException(global::SR.GetString("This stream does not support seek operations."));
			}
		}

		/// <summary>Sets the current position of the stream to the given value. This method is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The position in the stream.</returns>
		/// <param name="offset">This parameter is not used. </param>
		/// <param name="origin">This parameter is not used. </param>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		// Token: 0x06002DBC RID: 11708 RVA: 0x000B480A File Offset: 0x000B2A0A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(global::SR.GetString("This stream does not support seek operations."));
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000B481C File Offset: 0x000B2A1C
		internal void InitNetworkStream(Socket socket, FileAccess Access)
		{
			if (!socket.Blocking)
			{
				throw new IOException(global::SR.GetString("The operation is not allowed on a non-blocking Socket."));
			}
			if (!socket.Connected)
			{
				throw new IOException(global::SR.GetString("The operation is not allowed on non-connected sockets."));
			}
			if (socket.SocketType != SocketType.Stream)
			{
				throw new IOException(global::SR.GetString("The operation is not allowed on non-stream oriented sockets."));
			}
			this.m_StreamSocket = socket;
			switch (Access)
			{
			case FileAccess.Read:
				this.m_Readable = true;
				return;
			case FileAccess.Write:
				this.m_Writeable = true;
				return;
			}
			this.m_Readable = true;
			this.m_Writeable = true;
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000B48B0 File Offset: 0x000B2AB0
		internal bool PollRead()
		{
			if (this.m_CleanedUp)
			{
				return false;
			}
			Socket streamSocket = this.m_StreamSocket;
			return streamSocket != null && streamSocket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000B48E0 File Offset: 0x000B2AE0
		internal bool Poll(int microSeconds, SelectMode mode)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			return streamSocket.Poll(microSeconds, mode);
		}

		/// <summary>Reads data from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data to. </param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.-or- The <paramref name="size" /> parameter is less than 0.-or- The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.-or- There is a failure reading from the network. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DC0 RID: 11712 RVA: 0x000B4940 File Offset: 0x000B2B40
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			bool canRead = this.CanRead;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException(global::SR.GetString("The stream does not support reading."));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			int num;
			try
			{
				num = streamSocket.Receive(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
			return num;
		}

		/// <summary>Writes data to the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> from which to start writing data. </param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.-or- The <paramref name="size" /> parameter is less than 0.-or- The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.IO.IOException">There was a failure while writing to the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.-or- There was a failure reading from the network. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B4A44 File Offset: 0x000B2C44
		public override void Write(byte[] buffer, int offset, int size)
		{
			bool canWrite = this.CanWrite;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException(global::SR.GetString("The stream does not support writing."));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			try
			{
				streamSocket.Send(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
		}

		/// <summary>Closes the <see cref="T:System.Net.Sockets.NetworkStream" /> after waiting the specified time to allow data to be sent.</summary>
		/// <param name="timeout">A 32-bit signed integer that specifies the number of milliseconds to wait to send any remaining data before closing.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1.</exception>
		// Token: 0x06002DC2 RID: 11714 RVA: 0x000B4B44 File Offset: 0x000B2D44
		public void Close(int timeout)
		{
			if (timeout < -1)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			this.m_CloseTimeout = timeout;
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.NetworkStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002DC3 RID: 11715 RVA: 0x000B4B64 File Offset: 0x000B2D64
		protected override void Dispose(bool disposing)
		{
			int cleanedUp = (this.m_CleanedUp ? 1 : 0);
			this.m_CleanedUp = true;
			if (cleanedUp == 0 && disposing && this.m_StreamSocket != null)
			{
				this.m_Readable = false;
				this.m_Writeable = false;
				if (this.m_OwnsSocket)
				{
					Socket streamSocket = this.m_StreamSocket;
					if (streamSocket != null)
					{
						streamSocket.InternalShutdown(SocketShutdown.Both);
						streamSocket.Close(this.m_CloseTimeout);
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000B4BCC File Offset: 0x000B2DCC
		~NetworkStream()
		{
			this.Dispose(false);
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x000B4BFC File Offset: 0x000B2DFC
		internal bool Connected
		{
			get
			{
				Socket streamSocket = this.m_StreamSocket;
				return !this.m_CleanedUp && streamSocket != null && streamSocket.Connected;
			}
		}

		/// <summary>Begins an asynchronous read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data. </param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes. </param>
		/// <param name="state">An object that contains any additional user-defined data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of the <paramref name="buffer" /> paramater.-or- The <paramref name="size" /> is less than 0.-or- The <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- There was a failure while reading from the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002DC6 RID: 11718 RVA: 0x000B4C28 File Offset: 0x000B2E28
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canRead = this.CanRead;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException(global::SR.GetString("The stream does not support reading."));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = streamSocket.BeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
			return asyncResult;
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000B4D30 File Offset: 0x000B2F30
		internal virtual IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canRead = this.CanRead;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException(global::SR.GetString("The stream does not support reading."));
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = streamSocket.UnsafeBeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
			return asyncResult;
		}

		/// <summary>Handles the end of an asynchronous read.</summary>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that represents an asynchronous call. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DC8 RID: 11720 RVA: 0x000B4DEC File Offset: 0x000B2FEC
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			int num;
			try
			{
				num = streamSocket.EndReceive(asyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to read data from the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
			return num;
		}

		/// <summary>Begins an asynchronous write to a stream.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin sending the data. </param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes. </param>
		/// <param name="state">An object that contains any additional user-defined data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.-or- The <paramref name="size" /> parameter is less than 0.-or- The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- There was a failure while writing to the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002DC9 RID: 11721 RVA: 0x000B4EA8 File Offset: 0x000B30A8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canWrite = this.CanWrite;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException(global::SR.GetString("The stream does not support writing."));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = streamSocket.BeginSend(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
			return asyncResult;
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000B4FB0 File Offset: 0x000B31B0
		internal virtual IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canWrite = this.CanWrite;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException(global::SR.GetString("The stream does not support writing."));
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = streamSocket.UnsafeBeginSend(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
			return asyncResult;
		}

		/// <summary>Handles the end of an asynchronous write.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that represents the asynchronous call. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- An error occurred while writing to the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DCB RID: 11723 RVA: 0x000B507C File Offset: 0x000B327C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			try
			{
				streamSocket.EndSend(asyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000B5138 File Offset: 0x000B3338
		internal virtual void MultipleWrite(BufferOffsetSize[] buffers)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			try
			{
				streamSocket.MultipleSend(buffers, SocketFlags.None);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000B51D8 File Offset: 0x000B33D8
		internal virtual IAsyncResult BeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = streamSocket.BeginMultipleSend(buffers, SocketFlags.None, callback, state);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
			return asyncResult;
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000B527C File Offset: 0x000B347C
		internal virtual IAsyncResult UnsafeBeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = streamSocket.UnsafeBeginMultipleSend(buffers, SocketFlags.None, callback, state);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
			return asyncResult;
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000B5320 File Offset: 0x000B3520
		internal virtual void EndMultipleWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { global::SR.GetString("The connection was closed") }));
			}
			try
			{
				streamSocket.EndMultipleSend(asyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(global::SR.GetString("Unable to write data to the transport connection: {0}.", new object[] { ex.Message }), ex);
			}
		}

		/// <summary>Flushes data from the stream. This method is reserved for future use.</summary>
		// Token: 0x06002DD0 RID: 11728 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Flush()
		{
		}

		/// <summary>Flushes data from the stream as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x06002DD1 RID: 11729 RVA: 0x00009E40 File Offset: 0x00008040
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		/// <summary>Sets the length of the stream. This method always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">This parameter is not used. </param>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		// Token: 0x06002DD2 RID: 11730 RVA: 0x000B480A File Offset: 0x000B2A0A
		public override void SetLength(long value)
		{
			throw new NotSupportedException(global::SR.GetString("This stream does not support seek operations."));
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000B53C0 File Offset: 0x000B35C0
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			if (timeout < 0)
			{
				timeout = 0;
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				return;
			}
			if ((mode == SocketShutdown.Send || mode == SocketShutdown.Both) && timeout != this.m_CurrentWriteTimeout)
			{
				streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout, silent);
				this.m_CurrentWriteTimeout = timeout;
			}
			if ((mode == SocketShutdown.Receive || mode == SocketShutdown.Both) && timeout != this.m_CurrentReadTimeout)
			{
				streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout, silent);
				this.m_CurrentReadTimeout = timeout;
			}
		}

		// Token: 0x040025DF RID: 9695
		private Socket m_StreamSocket;

		// Token: 0x040025E0 RID: 9696
		private bool m_Readable;

		// Token: 0x040025E1 RID: 9697
		private bool m_Writeable;

		// Token: 0x040025E2 RID: 9698
		private bool m_OwnsSocket;

		// Token: 0x040025E3 RID: 9699
		private int m_CloseTimeout = -1;

		// Token: 0x040025E4 RID: 9700
		private volatile bool m_CleanedUp;

		// Token: 0x040025E5 RID: 9701
		private int m_CurrentReadTimeout = -1;

		// Token: 0x040025E6 RID: 9702
		private int m_CurrentWriteTimeout = -1;
	}
}
