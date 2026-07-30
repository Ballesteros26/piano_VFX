using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes a <see cref="T:System.IO.Stream" /> object around a pipe, which supports both anonymous and named pipes.</summary>
	// Token: 0x02000039 RID: 57
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class PipeStream : Stream
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003A98 File Offset: 0x00001C98
		internal static bool IsWindows
		{
			get
			{
				return Win32Marshal.IsWindows;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003A9F File Offset: 0x00001C9F
		internal Exception ThrowACLException()
		{
			return new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003AAB File Offset: 0x00001CAB
		internal static PipeAccessRights ToAccessRights(PipeDirection direction)
		{
			switch (direction)
			{
			case PipeDirection.In:
				return PipeAccessRights.ReadData;
			case PipeDirection.Out:
				return PipeAccessRights.WriteData;
			case PipeDirection.InOut:
				return PipeAccessRights.ReadData | PipeAccessRights.WriteData;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003AD0 File Offset: 0x00001CD0
		internal static PipeDirection ToDirection(PipeAccessRights rights)
		{
			bool flag = (rights & PipeAccessRights.ReadData) > (PipeAccessRights)0;
			bool flag2 = (rights & PipeAccessRights.WriteData) > (PipeAccessRights)0;
			if (flag)
			{
				if (flag2)
				{
					return PipeDirection.InOut;
				}
				return PipeDirection.In;
			}
			else
			{
				if (flag2)
				{
					return PipeDirection.Out;
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeStream" /> class using the specified <see cref="T:System.IO.Pipes.PipeDirection" /> value and buffer size.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that indicates the direction of the pipe object.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than or equal to 0 that indicates the buffer size.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="bufferSize" /> is less than 0.</exception>
		// Token: 0x0600010C RID: 268 RVA: 0x00003AFD File Offset: 0x00001CFD
		protected PipeStream(PipeDirection direction, int bufferSize)
			: this(direction, PipeTransmissionMode.Byte, bufferSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeStream" /> class using the specified <see cref="T:System.IO.Pipes.PipeDirection" />, <see cref="T:System.IO.Pipes.PipeTransmissionMode" />, and buffer size.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that indicates the direction of the pipe object.</param>
		/// <param name="transmissionMode">One of the <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> values that indicates the transmission mode of the pipe object.</param>
		/// <param name="outBufferSize">A positive <see cref="T:System.Int32" /> value greater than or equal to 0 that indicates the buffer size.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="transmissionMode" /> is not a valid <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> value.-or-<paramref name="bufferSize" /> is less than 0.</exception>
		// Token: 0x0600010D RID: 269 RVA: 0x00003B08 File Offset: 0x00001D08
		protected PipeStream(PipeDirection direction, PipeTransmissionMode transmissionMode, int outBufferSize)
		{
			this.direction = direction;
			this.transmission_mode = transmissionMode;
			this.read_trans_mode = transmissionMode;
			if (outBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize must be greater than 0");
			}
			this.buffer_size = outBufferSize;
		}

		/// <summary>Gets a value indicating whether the current stream supports read operations.</summary>
		/// <returns>true if the stream supports read operations; otherwise, false.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003B3B File Offset: 0x00001D3B
		public override bool CanRead
		{
			get
			{
				return (this.direction & PipeDirection.In) > (PipeDirection)0;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seek operations.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002285 File Offset: 0x00000485
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports write operations.</summary>
		/// <returns>true if the stream supports write operations; otherwise, false.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003B48 File Offset: 0x00001D48
		public override bool CanWrite
		{
			get
			{
				return (this.direction & PipeDirection.Out) > (PipeDirection)0;
			}
		}

		/// <summary>Gets the size, in bytes, of the inbound buffer for a pipe.</summary>
		/// <returns>An integer value that represents the inbound buffer size, in bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">The stream is unreadable.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is waiting to connect.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003B55 File Offset: 0x00001D55
		public virtual int InBufferSize
		{
			get
			{
				return this.buffer_size;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.IO.Pipes.PipeStream" /> object was opened asynchronously or synchronously.</summary>
		/// <returns>true if the <see cref="T:System.IO.Pipes.PipeStream" /> object was opened asynchronously; otherwise, false.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003B5D File Offset: 0x00001D5D
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00003B65 File Offset: 0x00001D65
		public bool IsAsync { get; private set; }

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.IO.Pipes.PipeStream" /> object is connected.</summary>
		/// <returns>true if the <see cref="T:System.IO.Pipes.PipeStream" /> object is connected; otherwise, false.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003B6E File Offset: 0x00001D6E
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00003B76 File Offset: 0x00001D76
		public bool IsConnected { get; protected set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003B80 File Offset: 0x00001D80
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00003BE8 File Offset: 0x00001DE8
		internal Stream Stream
		{
			get
			{
				if (!this.IsConnected)
				{
					throw new InvalidOperationException("Pipe is not connected");
				}
				if (this.stream == null)
				{
					this.stream = new FileStream(this.handle.DangerousGetHandle(), this.CanRead ? (this.CanWrite ? FileAccess.ReadWrite : FileAccess.Read) : FileAccess.Write, false, this.buffer_size, this.IsAsync);
				}
				return this.stream;
			}
			set
			{
				this.stream = value;
			}
		}

		/// <summary>Gets a value indicating whether a handle to a <see cref="T:System.IO.Pipes.PipeStream" /> object is exposed.</summary>
		/// <returns>true if a handle to the <see cref="T:System.IO.Pipes.PipeStream" /> object is exposed; otherwise, false.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003BF1 File Offset: 0x00001DF1
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00003BF9 File Offset: 0x00001DF9
		private protected bool IsHandleExposed { protected get; private set; }

		/// <summary>Gets a value indicating whether there is more data in the message returned from the most recent read operation.</summary>
		/// <returns>true if there are no more characters to read in the message; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe is not connected.-or-The pipe handle has not been set.-or-The pipe's <see cref="P:System.IO.Pipes.PipeStream.ReadMode" /> property value is not <see cref="F:System.IO.Pipes.PipeTransmissionMode.Message" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003C02 File Offset: 0x00001E02
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00003C0A File Offset: 0x00001E0A
		[global::System.MonoTODO]
		public bool IsMessageComplete { get; private set; }

		/// <summary>Gets the size, in bytes, of the outbound buffer for a pipe.</summary>
		/// <returns>The outbound buffer size, in bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">The stream is unwriteable.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is waiting to connect.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003B55 File Offset: 0x00001D55
		[global::System.MonoTODO]
		public virtual int OutBufferSize
		{
			get
			{
				return this.buffer_size;
			}
		}

		/// <summary>Gets or sets the reading mode for a <see cref="T:System.IO.Pipes.PipeStream" /> object.</summary>
		/// <returns>One of the <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> values that indicates how the <see cref="T:System.IO.Pipes.PipeStream" /> object reads from the pipe.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The supplied value is not a valid <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> value.</exception>
		/// <exception cref="T:System.NotSupportedException">The supplied value is not a supported <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> value for this pipe stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The handle has not been set.-or-The pipe is waiting to connect with a named client.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or an I/O error occurred with a named client.</exception>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00003C13 File Offset: 0x00001E13
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00003C21 File Offset: 0x00001E21
		public virtual PipeTransmissionMode ReadMode
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.read_trans_mode;
			}
			set
			{
				this.CheckPipePropertyOperations();
				this.read_trans_mode = value;
			}
		}

		/// <summary>Gets the safe handle for the local end of the pipe that the current <see cref="T:System.IO.Pipes.PipeStream" /> object encapsulates.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> object for the pipe that is encapsulated by the current <see cref="T:System.IO.Pipes.PipeStream" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe handle has not been set.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003C30 File Offset: 0x00001E30
		public SafePipeHandle SafePipeHandle
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.handle;
			}
		}

		/// <summary>Gets the pipe transmission mode supported by the current pipe.</summary>
		/// <returns>One of the <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> values that indicates the transmission mode supported by the current pipe.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The handle has not been set.-or-The pipe is waiting to connect in an anonymous client/server operation or with a named client. </exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003C3E File Offset: 0x00001E3E
		public virtual PipeTransmissionMode TransmissionMode
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.transmission_mode;
			}
		}

		/// <summary>Verifies that the pipe is in a proper state for getting or setting properties.</summary>
		// Token: 0x06000121 RID: 289 RVA: 0x00003C4C File Offset: 0x00001E4C
		[global::System.MonoTODO]
		protected internal virtual void CheckPipePropertyOperations()
		{
		}

		/// <summary>Verifies that the pipe is in a connected state for read operations.</summary>
		// Token: 0x06000122 RID: 290 RVA: 0x00003C4E File Offset: 0x00001E4E
		[global::System.MonoTODO]
		protected internal void CheckReadOperations()
		{
			if (!this.IsConnected)
			{
				throw new InvalidOperationException("Pipe is not connected");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("The pipe stream does not support read operations");
			}
		}

		/// <summary>Verifies that the pipe is in a connected state for write operations.</summary>
		// Token: 0x06000123 RID: 291 RVA: 0x00003C76 File Offset: 0x00001E76
		[global::System.MonoTODO]
		protected internal void CheckWriteOperations()
		{
			if (!this.IsConnected)
			{
				throw new InvalidOperationException("Pipe is not connected");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The pipe stream does not support write operations");
			}
		}

		/// <summary>Initializes a <see cref="T:System.IO.Pipes.PipeStream" /> object from the specified <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> object.</summary>
		/// <param name="handle">The <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> object of the pipe to initialize.</param>
		/// <param name="isExposed">true to expose the handle; otherwise, false.</param>
		/// <param name="isAsync">true to indicate that the handle was opened asynchronously; otherwise, false.</param>
		/// <exception cref="T:System.IO.IOException">A handle cannot be bound to the pipe.</exception>
		// Token: 0x06000124 RID: 292 RVA: 0x00003C9E File Offset: 0x00001E9E
		protected void InitializeHandle(SafePipeHandle handle, bool isExposed, bool isAsync)
		{
			this.handle = handle;
			this.IsHandleExposed = isExposed;
			this.IsAsync = isAsync;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Pipes.PipeStream" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000125 RID: 293 RVA: 0x00003CB5 File Offset: 0x00001EB5
		protected override void Dispose(bool disposing)
		{
			if (this.handle != null && disposing)
			{
				this.handle.Dispose();
			}
		}

		/// <summary>Gets the length of a stream, in bytes.</summary>
		/// <returns>0 in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00003CCF File Offset: 0x00001ECF
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the current position of the current stream.</summary>
		/// <returns>0 in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00003CD6 File Offset: 0x00001ED6
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00003CCF File Offset: 0x00001ECF
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Sets the length of the current stream to the specified value.</summary>
		/// <param name="value">The new length of the stream.</param>
		// Token: 0x06000129 RID: 297 RVA: 0x00003CCF File Offset: 0x00001ECF
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets the current position of the current stream to the specified value.</summary>
		/// <returns>The new position in the stream.</returns>
		/// <param name="offset">The point, relative to <paramref name="origin" />, to begin seeking from.</param>
		/// <param name="origin">Specifies the beginning, the end, or the current position as a reference point for <paramref name="offset" />, using a value of type <see cref="T:System.IO.SeekOrigin" />.</param>
		// Token: 0x0600012A RID: 298 RVA: 0x00003CCF File Offset: 0x00001ECF
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.IO.Pipes.PipeSecurity" /> object that encapsulates the access control list (ACL) entries for the pipe described by the current <see cref="T:System.IO.Pipes.PipeStream" /> object.</summary>
		/// <returns>A <see cref="T:System.IO.Pipes.PipeSecurity" /> object that encapsulates the access control list (ACL) entries for the pipe described by the current <see cref="T:System.IO.Pipes.PipeStream" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The underlying call to set security information failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying call to set security information failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying call to set security information failed.</exception>
		// Token: 0x0600012B RID: 299 RVA: 0x00003CDA File Offset: 0x00001EDA
		public PipeSecurity GetAccessControl()
		{
			return new PipeSecurity(this.SafePipeHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Applies the access control list (ACL) entries specified by a <see cref="T:System.IO.Pipes.PipeSecurity" /> object to the pipe specified by the current <see cref="T:System.IO.Pipes.PipeStream" /> object.</summary>
		/// <param name="pipeSecurity">A <see cref="T:System.IO.Pipes.PipeSecurity" /> object that specifies an access control list (ACL) entry to apply to the current pipe.</param>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeSecurity" /> is null.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The underlying call to set security information failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying call to set security information failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying call to set security information failed.</exception>
		// Token: 0x0600012C RID: 300 RVA: 0x00003CE9 File Offset: 0x00001EE9
		public void SetAccessControl(PipeSecurity pipeSecurity)
		{
			if (pipeSecurity == null)
			{
				throw new ArgumentNullException("pipeSecurity");
			}
			pipeSecurity.Persist(this.SafePipeHandle);
		}

		/// <summary>Waits for the other end of the pipe to read all sent bytes.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x0600012D RID: 301 RVA: 0x00003C4C File Offset: 0x00001E4C
		public void WaitForPipeDrain()
		{
		}

		/// <summary>Reads a block of bytes from a stream and writes the data to a specified buffer.</summary>
		/// <returns>The total number of bytes that are read into <paramref name="buffer" />. This might be less than the number of bytes requested if that number of bytes is not currently available, or 0 if the end of the stream is reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The byte offset in the <paramref name="buffer" /> array at which the bytes that are read will be placed.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the number of bytes available in <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support read operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">Any I/O error occurred.</exception>
		// Token: 0x0600012E RID: 302 RVA: 0x00003D05 File Offset: 0x00001F05
		[global::System.MonoTODO]
		public override int Read([In] byte[] buffer, int offset, int count)
		{
			this.CheckReadOperations();
			return this.Stream.Read(buffer, offset, count);
		}

		/// <summary>Reads a byte from a pipe.</summary>
		/// <returns>The byte, cast to <see cref="T:System.Int32" />, or -1 indicates the end of the stream (the pipe has been closed).</returns>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support read operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">Any I/O error occurred.</exception>
		// Token: 0x0600012F RID: 303 RVA: 0x00003D1B File Offset: 0x00001F1B
		[global::System.MonoTODO]
		public override int ReadByte()
		{
			this.CheckReadOperations();
			return this.Stream.ReadByte();
		}

		/// <summary>Writes a block of bytes to the current stream using data from a buffer.</summary>
		/// <param name="buffer">The buffer that contains data to write to the pipe.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The maximum number of bytes to write to the current stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the number of bytes available in <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000130 RID: 304 RVA: 0x00003D2E File Offset: 0x00001F2E
		[global::System.MonoTODO]
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckWriteOperations();
			this.Stream.Write(buffer, offset, count);
		}

		/// <summary>Writes a byte to the current stream.</summary>
		/// <param name="value">The byte to write to the stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000131 RID: 305 RVA: 0x00003D44 File Offset: 0x00001F44
		[global::System.MonoTODO]
		public override void WriteByte(byte value)
		{
			this.CheckWriteOperations();
			this.Stream.WriteByte(value);
		}

		/// <summary>Clears the buffer for the current stream and causes any buffered data to be written to the underlying device.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000132 RID: 306 RVA: 0x00003D58 File Offset: 0x00001F58
		[global::System.MonoTODO]
		public override void Flush()
		{
			this.CheckWriteOperations();
			this.Stream.Flush();
		}

		/// <summary>Begins an asynchronous read operation.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous read.</returns>
		/// <param name="buffer">The buffer to read data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin reading.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="callback">The method to call when the asynchronous read operation is completed.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the number of bytes available in <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support read operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000133 RID: 307 RVA: 0x00003D6B File Offset: 0x00001F6B
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.read_delegate == null)
			{
				this.read_delegate = new Func<byte[], int, int, int>(this.Read);
			}
			return this.read_delegate.BeginInvoke(buffer, offset, count, callback, state);
		}

		/// <summary>Begins an asynchronous write operation.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer that contains the data to write to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="callback">The method to call when the asynchronous write operation is completed.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the number of bytes available in <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000134 RID: 308 RVA: 0x00003D9A File Offset: 0x00001F9A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.write_delegate == null)
			{
				this.write_delegate = new Action<byte[], int, int>(this.Write);
			}
			return this.write_delegate.BeginInvoke(buffer, offset, count, callback, state);
		}

		/// <summary>Ends a pending asynchronous read request.</summary>
		/// <returns>The number of bytes that were read. A return value of 0 indicates the end of the stream (the pipe has been closed).</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Pipes.PipeStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		// Token: 0x06000135 RID: 309 RVA: 0x00003DC9 File Offset: 0x00001FC9
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.read_delegate.EndInvoke(asyncResult);
		}

		/// <summary>Ends a pending asynchronous write request.</summary>
		/// <param name="asyncResult">The reference to the pending asynchronous request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Pipes.PipeStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		// Token: 0x06000136 RID: 310 RVA: 0x00003DD7 File Offset: 0x00001FD7
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.write_delegate.EndInvoke(asyncResult);
		}

		// Token: 0x04000216 RID: 534
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04000217 RID: 535
		private PipeDirection direction;

		// Token: 0x04000218 RID: 536
		private PipeTransmissionMode transmission_mode;

		// Token: 0x04000219 RID: 537
		private PipeTransmissionMode read_trans_mode;

		// Token: 0x0400021A RID: 538
		private int buffer_size;

		// Token: 0x0400021B RID: 539
		private SafePipeHandle handle;

		// Token: 0x0400021C RID: 540
		private Stream stream;

		// Token: 0x04000221 RID: 545
		private Func<byte[], int, int, int> read_delegate;

		// Token: 0x04000222 RID: 546
		private Action<byte[], int, int> write_delegate;
	}
}
