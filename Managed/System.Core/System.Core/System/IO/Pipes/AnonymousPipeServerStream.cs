using System;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes a stream around an anonymous pipe, which supports both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x0200002B RID: 43
	[global::System.MonoTODO("Anonymous pipes are not working even on win32, due to some access authorization issue")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AnonymousPipeServerStream : PipeStream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class.</summary>
		// Token: 0x060000B4 RID: 180 RVA: 0x000033BC File Offset: 0x000015BC
		public AnonymousPipeServerStream()
			: this(PipeDirection.Out)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x060000B5 RID: 181 RVA: 0x000033C5 File Offset: 0x000015C5
		public AnonymousPipeServerStream(PipeDirection direction)
			: this(direction, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction and inheritability mode.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle can be inherited by child processes. Must be set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inheritability" /> is not set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x060000B6 RID: 182 RVA: 0x000033CF File Offset: 0x000015CF
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability)
			: this(direction, inheritability, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction, inheritability mode, and buffer size.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle can be inherited by child processes. Must be set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.</param>
		/// <param name="bufferSize">The size of the buffer. This value must be greater than or equal to 0. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inheritability" /> is not set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.-or-<paramref name="bufferSize" /> is less than 0.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x060000B7 RID: 183 RVA: 0x000033DE File Offset: 0x000015DE
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize)
			: this(direction, inheritability, bufferSize, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction, inheritability mode, buffer size, and pipe security.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle can be inherited by child processes.</param>
		/// <param name="bufferSize">The size of the buffer. This value must be greater than or equal to 0. </param>
		/// <param name="pipeSecurity">An object that determines the access control and audit security for the pipe.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inheritability" /> is not set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.-or-<paramref name="bufferSize" /> is less than 0.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x060000B8 RID: 184 RVA: 0x000033EC File Offset: 0x000015EC
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize, PipeSecurity pipeSecurity)
			: base(direction, bufferSize)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipe direction can only be either in or out.");
			}
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32AnonymousPipeServer(this, direction, inheritability, bufferSize, pipeSecurity);
			}
			else
			{
				this.impl = new UnixAnonymousPipeServer(this, direction, inheritability, bufferSize);
			}
			base.InitializeHandle(this.impl.Handle, false, false);
			base.IsConnected = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class from the specified pipe handles.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="serverSafePipeHandle">A safe handle for the pipe that this <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object will encapsulate.</param>
		/// <param name="clientSafePipeHandle">A safe handle for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="serverSafePipeHandle" /> or <paramref name="clientSafePipeHandle" /> is an invalid handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverSafePipeHandle" /> or <paramref name="clientSafePipeHandle" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, has occurred.-or-The stream has been closed.</exception>
		// Token: 0x060000B9 RID: 185 RVA: 0x00003454 File Offset: 0x00001654
		[global::System.MonoTODO]
		public AnonymousPipeServerStream(PipeDirection direction, SafePipeHandle serverSafePipeHandle, SafePipeHandle clientSafePipeHandle)
			: base(direction, 1024)
		{
			if (serverSafePipeHandle == null)
			{
				throw new ArgumentNullException("serverSafePipeHandle");
			}
			if (clientSafePipeHandle == null)
			{
				throw new ArgumentNullException("clientSafePipeHandle");
			}
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipe direction can only be either in or out.");
			}
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32AnonymousPipeServer(this, serverSafePipeHandle, clientSafePipeHandle);
			}
			else
			{
				this.impl = new UnixAnonymousPipeServer(this, serverSafePipeHandle, clientSafePipeHandle);
			}
			base.InitializeHandle(serverSafePipeHandle, true, false);
			base.IsConnected = true;
			this.ClientSafePipeHandle = clientSafePipeHandle;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000034D4 File Offset: 0x000016D4
		~AnonymousPipeServerStream()
		{
		}

		/// <summary>Gets the safe handle for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object that is currently connected to the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object.</summary>
		/// <returns>A handle for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object that is currently connected to the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000034FC File Offset: 0x000016FC
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003504 File Offset: 0x00001704
		[global::System.MonoTODO]
		public SafePipeHandle ClientSafePipeHandle { get; private set; }

		/// <summary>Sets the reading mode for the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object. For anonymous pipes, transmission mode must be <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" />.</summary>
		/// <returns>The reading mode for the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The transmission mode is not valid. For anonymous pipes, only <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" /> is supported. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set to <see cref="F:System.IO.Pipes.PipeTransmissionMode.Message" />, which is not supported for anonymous pipes.</exception>
		/// <exception cref="T:System.IO.IOException">The connection is broken or another I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x17000016 RID: 22
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000033B0 File Offset: 0x000015B0
		public override PipeTransmissionMode ReadMode
		{
			set
			{
				if (value == PipeTransmissionMode.Message)
				{
					throw new NotSupportedException();
				}
			}
		}

		/// <summary>Gets the pipe transmission mode that is supported by the current pipe.</summary>
		/// <returns>The <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> that is supported by the current pipe.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002285 File Offset: 0x00000485
		public override PipeTransmissionMode TransmissionMode
		{
			get
			{
				return PipeTransmissionMode.Byte;
			}
		}

		/// <summary>Closes the local copy of the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object's handle.</summary>
		// Token: 0x060000BF RID: 191 RVA: 0x0000350D File Offset: 0x0000170D
		[global::System.MonoTODO]
		public void DisposeLocalCopyOfClientHandle()
		{
			this.impl.DisposeLocalCopyOfClientHandle();
		}

		/// <summary>Gets the connected <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object's handle as a string.</summary>
		/// <returns>A string that represents the connected <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object's handle.</returns>
		// Token: 0x060000C0 RID: 192 RVA: 0x0000351C File Offset: 0x0000171C
		public string GetClientHandleAsString()
		{
			return this.impl.Handle.DangerousGetHandle().ToInt64().ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x040001F6 RID: 502
		private IAnonymousPipeServer impl;
	}
}
