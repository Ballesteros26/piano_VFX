using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes a <see cref="T:System.IO.Stream" /> around a named pipe, which supports both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x0200002C RID: 44
	[global::System.MonoTODO("working only on win32 right now")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class NamedPipeClientStream : PipeStream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe name.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".</exception>
		// Token: 0x060000C1 RID: 193 RVA: 0x0000354E File Offset: 0x0000174E
		public NamedPipeClientStream(string pipeName)
			: this(".", pipeName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".</exception>
		// Token: 0x060000C2 RID: 194 RVA: 0x0000355C File Offset: 0x0000175C
		public NamedPipeClientStream(string serverName, string pipeName)
			: this(serverName, pipeName, PipeDirection.InOut)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.</exception>
		// Token: 0x060000C3 RID: 195 RVA: 0x00003567 File Offset: 0x00001767
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction)
			: this(serverName, pipeName, direction, PipeOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction and pipe options.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.</exception>
		// Token: 0x060000C4 RID: 196 RVA: 0x00003573 File Offset: 0x00001773
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options)
			: this(serverName, pipeName, direction, options, TokenImpersonationLevel.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction, pipe options, and security impersonation level.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="impersonationLevel">One of the enumeration values that determines the security impersonation level.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-<paramref name="impersonationLevel" /> is not a valid <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.</exception>
		// Token: 0x060000C5 RID: 197 RVA: 0x00003581 File Offset: 0x00001781
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel)
			: this(serverName, pipeName, direction, options, impersonationLevel, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction, pipe options, security impersonation level, and inheritability mode.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="impersonationLevel">One of the enumeration values that determines the security impersonation level.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle will be inheritable by child processes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-<paramref name="impersonationLevel" /> is not a valid <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.-or-<paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> value.</exception>
		// Token: 0x060000C6 RID: 198 RVA: 0x00003591 File Offset: 0x00001791
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability)
			: this(serverName, pipeName, PipeStream.ToAccessRights(direction), options, impersonationLevel, inheritability)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class for the specified pipe handle with the specified pipe direction.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="isAsync">true to indicate that the handle was opened asynchronously; otherwise, false.</param>
		/// <param name="isConnected">true to indicate that the pipe is connected; otherwise, false.</param>
		/// <param name="safePipeHandle">A safe handle for the pipe that this <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> object will encapsulate.</param>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="safePipeHandle" /> is not a valid handle.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="safePipeHandle" /> is not a valid handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="safePipeHandle" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.</exception>
		/// <exception cref="T:System.IO.IOException">The stream has been closed. </exception>
		// Token: 0x060000C7 RID: 199 RVA: 0x000035A8 File Offset: 0x000017A8
		public NamedPipeClientStream(PipeDirection direction, bool isAsync, bool isConnected, SafePipeHandle safePipeHandle)
			: base(direction, 1024)
		{
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32NamedPipeClient(this, safePipeHandle);
			}
			else
			{
				this.impl = new UnixNamedPipeClient(this, safePipeHandle);
			}
			base.IsConnected = isConnected;
			base.InitializeHandle(safePipeHandle, true, isAsync);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe options, security impersonation level, and inheritability mode.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="desiredAccessRights">One of the enumeration values that specifies the desired access rights of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="impersonationLevel">One of the enumeration values that determines the security impersonation level.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle will be inheritable by child processes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-<paramref name="impersonationLevel" /> is not a valid <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.-or-<paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> value.</exception>
		// Token: 0x060000C8 RID: 200 RVA: 0x000035F8 File Offset: 0x000017F8
		public NamedPipeClientStream(string serverName, string pipeName, PipeAccessRights desiredAccessRights, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability)
			: base(PipeStream.ToDirection(desiredAccessRights), 1024)
		{
			if (impersonationLevel != TokenImpersonationLevel.None || inheritability != HandleInheritability.None)
			{
				throw base.ThrowACLException();
			}
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32NamedPipeClient(this, serverName, pipeName, desiredAccessRights, options, inheritability);
				return;
			}
			this.impl = new UnixNamedPipeClient(this, serverName, pipeName, desiredAccessRights, options, inheritability);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003654 File Offset: 0x00001854
		~NamedPipeClientStream()
		{
			this.Dispose(false);
		}

		/// <summary>Connects to a waiting server with an infinite time-out value.</summary>
		/// <exception cref="T:System.InvalidOperationException">The client is already connected.</exception>
		// Token: 0x060000CA RID: 202 RVA: 0x00003684 File Offset: 0x00001884
		public void Connect()
		{
			this.impl.Connect();
			base.InitializeHandle(this.impl.Handle, false, this.impl.IsAsync);
			base.IsConnected = true;
		}

		/// <summary>Connects to a waiting server within the specified time-out period.</summary>
		/// <param name="timeout">The number of milliseconds to wait for the server to respond before the connection times out.</param>
		/// <exception cref="T:System.TimeoutException">Could not connect to the server within the specified <paramref name="timeout" /> period.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than 0 and not set to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The client is already connected.</exception>
		/// <exception cref="T:System.IO.IOException">The server is connected to another client and the time-out period has expired.</exception>
		// Token: 0x060000CB RID: 203 RVA: 0x000036B5 File Offset: 0x000018B5
		public void Connect(int timeout)
		{
			this.impl.Connect(timeout);
			base.InitializeHandle(this.impl.Handle, false, this.impl.IsAsync);
			base.IsConnected = true;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000036E7 File Offset: 0x000018E7
		public Task ConnectAsync()
		{
			return this.ConnectAsync(-1, CancellationToken.None);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000036F5 File Offset: 0x000018F5
		public Task ConnectAsync(int timeout)
		{
			return this.ConnectAsync(timeout, CancellationToken.None);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003703 File Offset: 0x00001903
		public Task ConnectAsync(CancellationToken cancellationToken)
		{
			return this.ConnectAsync(-1, cancellationToken);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000227E File Offset: 0x0000047E
		public Task ConnectAsync(int timeout, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000370D File Offset: 0x0000190D
		protected internal override void CheckPipePropertyOperations()
		{
			base.CheckPipePropertyOperations();
		}

		/// <summary>Gets the number of server instances that share the same pipe name.</summary>
		/// <returns>The number of server instances that share the same pipe name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe handle has not been set.-or-The current <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> object has not yet connected to a <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> object.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or an I/O error occurred.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying pipe handle is closed.</exception>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003715 File Offset: 0x00001915
		public int NumberOfServerInstances
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.impl.NumberOfServerInstances;
			}
		}

		// Token: 0x040001F8 RID: 504
		private INamedPipeClient impl;
	}
}
