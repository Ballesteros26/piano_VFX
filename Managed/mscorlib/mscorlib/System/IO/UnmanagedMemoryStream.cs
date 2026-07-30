using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Provides access to unmanaged blocks of memory from managed code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003C5 RID: 965
	public class UnmanagedMemoryStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class.</summary>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		// Token: 0x06002D3F RID: 11583 RVA: 0x000A1A21 File Offset: 0x0009FC21
		[SecuritySafeCritical]
		protected UnmanagedMemoryStream()
		{
			this._mem = null;
			this._isOpen = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class in a safe buffer with a specified offset and length. </summary>
		/// <param name="buffer">The buffer to contain the unmanaged memory stream.</param>
		/// <param name="offset">The byte position in the buffer at which to start the unmanaged memory stream.</param>
		/// <param name="length">The length of the unmanaged memory stream.</param>
		// Token: 0x06002D40 RID: 11584 RVA: 0x000A1A38 File Offset: 0x0009FC38
		[SecuritySafeCritical]
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length)
		{
			this.Initialize(buffer, offset, length, FileAccess.Read, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class in a safe buffer with a specified offset, length, and file access. </summary>
		/// <param name="buffer">The buffer to contain the unmanaged memory stream.</param>
		/// <param name="offset">The byte position in the buffer at which to start the unmanaged memory stream.</param>
		/// <param name="length">The length of the unmanaged memory stream.</param>
		/// <param name="access">The mode of file access to the unmanaged memory stream. </param>
		// Token: 0x06002D41 RID: 11585 RVA: 0x000A1A4B File Offset: 0x0009FC4B
		[SecuritySafeCritical]
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			this.Initialize(buffer, offset, length, access, false);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000A1A5F File Offset: 0x0009FC5F
		[SecurityCritical]
		internal UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access, bool skipSecurityCheck)
		{
			this.Initialize(buffer, offset, length, access, skipSecurityCheck);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class in a safe buffer with a specified offset, length, and file access. </summary>
		/// <param name="buffer">The buffer to contain the unmanaged memory stream.</param>
		/// <param name="offset">The byte position in the buffer at which to start the unmanaged memory stream.</param>
		/// <param name="length">The length of the unmanaged memory stream.</param>
		/// <param name="access">The mode of file access to the unmanaged memory stream.</param>
		// Token: 0x06002D43 RID: 11587 RVA: 0x000A1A74 File Offset: 0x0009FC74
		[SecuritySafeCritical]
		protected void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			this.Initialize(buffer, offset, length, access, false);
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000A1A84 File Offset: 0x0009FC84
		[SecurityCritical]
		internal unsafe void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access, bool skipSecurityCheck)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.ByteLength < (ulong)(offset + length))
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were greater than the size of the SafeBuffer."));
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The method cannot be called twice on the same instance."));
			}
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.AcquirePointer(ref ptr);
				if (ptr + offset + length < ptr)
				{
					throw new ArgumentException(Environment.GetResourceString("The UnmanagedMemoryStream capacity would wrap around the high end of the address space."));
				}
			}
			finally
			{
				if (ptr != null)
				{
					buffer.ReleasePointer();
				}
			}
			this._offset = offset;
			this._buffer = buffer;
			this._length = length;
			this._capacity = length;
			this._access = access;
			this._isOpen = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class using the specified location and memory length.</summary>
		/// <param name="pointer">A pointer to an unmanaged memory location.</param>
		/// <param name="length">The length of the memory to use.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pointer" /> value is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="length" /> value is less than zero.- or -The <paramref name="length" /> is large enough to cause an overflow.</exception>
		// Token: 0x06002D45 RID: 11589 RVA: 0x000A1B90 File Offset: 0x0009FD90
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length)
		{
			this.Initialize(pointer, length, length, FileAccess.Read, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class using the specified location, memory length, total amount of memory, and file access values.</summary>
		/// <param name="pointer">A pointer to an unmanaged memory location.</param>
		/// <param name="length">The length of the memory to use.</param>
		/// <param name="capacity">The total amount of memory assigned to the stream.</param>
		/// <param name="access">One of the <see cref="T:System.IO.FileAccess" /> values.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pointer" /> value is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="length" /> value is less than zero.- or - The <paramref name="capacity" /> value is less than zero.- or -The <paramref name="length" /> value is greater than the <paramref name="capacity" /> value.</exception>
		// Token: 0x06002D46 RID: 11590 RVA: 0x000A1BA3 File Offset: 0x0009FDA3
		[CLSCompliant(false)]
		[SecurityCritical]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access)
		{
			this.Initialize(pointer, length, capacity, access, false);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000A1BB7 File Offset: 0x0009FDB7
		[SecurityCritical]
		internal unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access, bool skipSecurityCheck)
		{
			this.Initialize(pointer, length, capacity, access, skipSecurityCheck);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryStream" /> class by using a pointer to an unmanaged memory location. </summary>
		/// <param name="pointer">A pointer to an unmanaged memory location.</param>
		/// <param name="length">The length of the memory to use.</param>
		/// <param name="capacity">The total amount of memory assigned to the stream.</param>
		/// <param name="access">One of the <see cref="T:System.IO.FileAccess" /> values. </param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pointer" /> value is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="length" /> value is less than zero.- or - The <paramref name="capacity" /> value is less than zero.- or -The <paramref name="length" /> value is large enough to cause an overflow.</exception>
		// Token: 0x06002D48 RID: 11592 RVA: 0x000A1BCC File Offset: 0x0009FDCC
		[SecurityCritical]
		[CLSCompliant(false)]
		protected unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access)
		{
			this.Initialize(pointer, length, capacity, access, false);
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000A1BDC File Offset: 0x0009FDDC
		[SecurityCritical]
		internal unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access, bool skipSecurityCheck)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			if (length < 0L || capacity < 0L)
			{
				throw new ArgumentOutOfRangeException((length < 0L) ? "length" : "capacity", Environment.GetResourceString("Non-negative number required."));
			}
			if (length > capacity)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("The length cannot be greater than the capacity."));
			}
			if (pointer + capacity < pointer)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("The UnmanagedMemoryStream capacity would wrap around the high end of the address space."));
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", Environment.GetResourceString("Enum value was out of legal range."));
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The method cannot be called twice on the same instance."));
			}
			this._mem = pointer;
			this._offset = 0L;
			this._length = length;
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
		}

		/// <summary>Gets a value indicating whether a stream supports reading.</summary>
		/// <returns>false if the object was created by a constructor with an <paramref name="access" /> parameter that did not include reading the stream and if the stream is closed; otherwise, true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x000A1CBD File Offset: 0x0009FEBD
		public override bool CanRead
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Read) > (FileAccess)0;
			}
		}

		/// <summary>Gets a value indicating whether a stream supports seeking.</summary>
		/// <returns>false if the stream is closed; otherwise, true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x000A1CD4 File Offset: 0x0009FED4
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Gets a value indicating whether a stream supports writing.</summary>
		/// <returns>false if the object was created by a constructor with an <paramref name="access" /> parameter value that supports writing or was created by a constructor that had no parameters, or if the stream is closed; otherwise, true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000A1CDC File Offset: 0x0009FEDC
		public override bool CanWrite
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Write) > (FileAccess)0;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.UnmanagedMemoryStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06002D4D RID: 11597 RVA: 0x000A1CF3 File Offset: 0x0009FEF3
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			this._mem = null;
			base.Dispose(disposing);
		}

		/// <summary>Overrides the <see cref="M:System.IO.Stream.Flush" /> method so that no action is performed.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002D4E RID: 11598 RVA: 0x000A1D0B File Offset: 0x0009FF0B
		public override void Flush()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000A1D1C File Offset: 0x0009FF1C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			Task task;
			try
			{
				this.Flush();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		/// <summary>Gets the length of the data in a stream.</summary>
		/// <returns>The length of the data in the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x000A1D64 File Offset: 0x0009FF64
		public override long Length
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return Interlocked.Read(ref this._length);
			}
		}

		/// <summary>Gets the stream length (size) or the total amount of memory assigned to a stream (capacity).</summary>
		/// <returns>The size or capacity of the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x000A1D7E File Offset: 0x0009FF7E
		public long Capacity
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return this._capacity;
			}
		}

		/// <summary>Gets or sets the current position in a stream.</summary>
		/// <returns>The current position in the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is set to a value that is less than zero, or the position is larger than <see cref="F:System.Int32.MaxValue" /> or results in overflow when added to the current pointer.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000A1D93 File Offset: 0x0009FF93
		// (set) Token: 0x06002D53 RID: 11603 RVA: 0x000A1DAD File Offset: 0x0009FFAD
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					__Error.StreamIsClosed();
				}
				return Interlocked.Read(ref this._position);
			}
			[SecuritySafeCritical]
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Non-negative number required."));
				}
				if (!this.CanSeek)
				{
					__Error.StreamIsClosed();
				}
				Interlocked.Exchange(ref this._position, value);
			}
		}

		/// <summary>Gets or sets a byte pointer to a stream based on the current position in the stream.</summary>
		/// <returns>A byte pointer.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The current position is larger than the capacity of the stream.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is being set is not a valid position in the current stream.</exception>
		/// <exception cref="T:System.IO.IOException">The pointer is being set to a lower value than the starting position of the stream.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream was initialized for use with a <see cref="T:System.Runtime.InteropServices.SafeBuffer" />. The <see cref="P:System.IO.UnmanagedMemoryStream.PositionPointer" /> property is valid only for streams that are initialized with a <see cref="T:System.Byte" /> pointer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x000A1DE4 File Offset: 0x0009FFE4
		// (set) Token: 0x06002D55 RID: 11605 RVA: 0x000A1E44 File Offset: 0x000A0044
		[CLSCompliant(false)]
		public unsafe byte* PositionPointer
		{
			[SecurityCritical]
			get
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException(Environment.GetResourceString("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer."));
				}
				long num = Interlocked.Read(ref this._position);
				if (num > this._capacity)
				{
					throw new IndexOutOfRangeException(Environment.GetResourceString("Unmanaged memory stream position was beyond the capacity of the stream."));
				}
				byte* ptr = this._mem + num;
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return ptr;
			}
			[SecurityCritical]
			set
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException(Environment.GetResourceString("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer."));
				}
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				if (new IntPtr((long)(value - this._mem)).ToInt64() > 9223372036854775807L)
				{
					throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("UnmanagedMemoryStream length must be non-negative and less than 2^63 - 1 - baseAddress."));
				}
				if (value < this._mem)
				{
					throw new IOException(Environment.GetResourceString("An attempt was made to move the position before the beginning of the stream."));
				}
				Interlocked.Exchange(ref this._position, (long)(value - this._mem));
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000A1EDE File Offset: 0x000A00DE
		internal unsafe byte* Pointer
		{
			[SecurityCritical]
			get
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException(Environment.GetResourceString("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer."));
				}
				return this._mem;
			}
		}

		/// <summary>Reads the specified number of bytes into the specified array.</summary>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source. This parameter is passed uninitialized.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to read from the current stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support reading.- or - The <see cref="P:System.IO.UnmanagedMemoryStream.CanRead" /> property is set to false. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is set to null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero. - or - The <paramref name="count" /> parameter is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the buffer array minus the <paramref name="offset" /> parameter is less than the <paramref name="count" /> parameter.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002D57 RID: 11607 RVA: 0x000A1F00 File Offset: 0x000A0100
		[SecuritySafeCritical]
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length) - num;
			if (num2 > (long)count)
			{
				num2 = (long)count;
			}
			if (num2 <= 0L)
			{
				return 0;
			}
			int num3 = (int)num2;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					Buffer.Memcpy(buffer, offset, ptr + num + this._offset, 0, num3);
					goto IL_0105;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			Buffer.Memcpy(buffer, offset, this._mem + num, 0, num3);
			IL_0105:
			Interlocked.Exchange(ref this._position, num + num2);
			return num3;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000A2034 File Offset: 0x000A0234
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<int>(cancellationToken);
			}
			Task<int> task;
			try
			{
				int num = this.Read(buffer, offset, count);
				Task<int> lastReadTask = this._lastReadTask;
				Task<int> task2;
				if (lastReadTask == null || lastReadTask.Result != num)
				{
					task = (this._lastReadTask = Task.FromResult<int>(num));
					task2 = task;
				}
				else
				{
					task2 = lastReadTask;
				}
				task = task2;
			}
			catch (Exception ex)
			{
				task = Task.FromException<int>(ex);
			}
			return task;
		}

		/// <summary>Reads a byte from a stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.</summary>
		/// <returns>The unsigned byte cast to an <see cref="T:System.Int32" /> object, or -1 if at the end of the stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support reading.- or -The current position is at the end of the stream.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002D59 RID: 11609 RVA: 0x000A2100 File Offset: 0x000A0300
		[SecuritySafeCritical]
		public unsafe override int ReadByte()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (num >= num2)
			{
				return -1;
			}
			Interlocked.Exchange(ref this._position, num + 1L);
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					return (int)(ptr + num)[this._offset];
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			return (int)this._mem[num];
		}

		/// <summary>Sets the current position of the current stream to the given value.</summary>
		/// <returns>The new position in the stream.</returns>
		/// <param name="offset">The point relative to <paramref name="origin" /> to begin seeking from. </param>
		/// <param name="loc">Specifies the beginning, the end, or the current position as a reference point for <paramref name="origin" />, using a value of type <see cref="T:System.IO.SeekOrigin" />. </param>
		/// <exception cref="T:System.IO.IOException">An attempt was made to seek before the beginning of the stream.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> value is larger than the maximum size of the stream.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="loc" /> is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002D5A RID: 11610 RVA: 0x000A21B0 File Offset: 0x000A03B0
		public override long Seek(long offset, SeekOrigin loc)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (offset > 9223372036854775807L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("UnmanagedMemoryStream length must be non-negative and less than 2^63 - 1 - baseAddress."));
			}
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException(Environment.GetResourceString("An attempt was made to move the position before the beginning of the stream."));
				}
				Interlocked.Exchange(ref this._position, offset);
				break;
			case SeekOrigin.Current:
			{
				long num = Interlocked.Read(ref this._position);
				if (offset + num < 0L)
				{
					throw new IOException(Environment.GetResourceString("An attempt was made to move the position before the beginning of the stream."));
				}
				Interlocked.Exchange(ref this._position, offset + num);
				break;
			}
			case SeekOrigin.End:
			{
				long num2 = Interlocked.Read(ref this._length);
				if (num2 + offset < 0L)
				{
					throw new IOException(Environment.GetResourceString("An attempt was made to move the position before the beginning of the stream."));
				}
				Interlocked.Exchange(ref this._position, num2 + offset);
				break;
			}
			default:
				throw new ArgumentException(Environment.GetResourceString("Invalid seek origin."));
			}
			return Interlocked.Read(ref this._position);
		}

		/// <summary>Sets the length of a stream to a specified value.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support writing.- or -An attempt is made to write to the stream and the <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> property is false.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="value" /> exceeds the capacity of the stream.- or -The specified <paramref name="value" /> is negative.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002D5B RID: 11611 RVA: 0x000A22AC File Offset: 0x000A04AC
		[SecuritySafeCritical]
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Non-negative number required."));
			}
			if (this._buffer != null)
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer."));
			}
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			if (value > this._capacity)
			{
				throw new IOException(Environment.GetResourceString("Unable to expand length of this stream beyond its capacity."));
			}
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (value > num2)
			{
				Buffer.ZeroMemory(this._mem + num2, value - num2);
			}
			Interlocked.Exchange(ref this._length, value);
			if (num > value)
			{
				Interlocked.Exchange(ref this._position, value);
			}
		}

		/// <summary>Writes a block of bytes to the current stream using data from a buffer.</summary>
		/// <param name="buffer">The byte array from which to copy bytes to the current stream.</param>
		/// <param name="offset">The offset in the buffer at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to write to the current stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support writing. - or -An attempt is made to write to the stream and the <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> property is false.- or -The <paramref name="count" /> value is greater than the capacity of the stream.- or -The position is at the end of the stream capacity.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One of the specified parameters is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="offset" /> parameter minus the length of the <paramref name="buffer" /> parameter is less than the <paramref name="count" /> parameter.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002D5C RID: 11612 RVA: 0x000A2368 File Offset: 0x000A0568
		[SecuritySafeCritical]
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + (long)count;
			if (num3 < 0L)
			{
				throw new IOException(Environment.GetResourceString("Stream was too long."));
			}
			if (num3 > this._capacity)
			{
				throw new NotSupportedException(Environment.GetResourceString("Unable to expand length of this stream beyond its capacity."));
			}
			if (this._buffer == null)
			{
				if (num > num2)
				{
					Buffer.ZeroMemory(this._mem + num2, num - num2);
				}
				if (num3 > num2)
				{
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			if (this._buffer != null)
			{
				if (this._capacity - num < (long)count)
				{
					throw new ArgumentException(Environment.GetResourceString("Not enough space available in the buffer."));
				}
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					Buffer.Memcpy(ptr + num + this._offset, 0, buffer, offset, count);
					goto IL_0168;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			Buffer.Memcpy(this._mem + num, 0, buffer, offset, count);
			IL_0168:
			Interlocked.Exchange(ref this._position, num3);
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000A24FC File Offset: 0x000A06FC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			Task task;
			try
			{
				this.Write(buffer, offset, count);
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException<int>(ex);
			}
			return task;
		}

		/// <summary>Writes a byte to the current position in the file stream.</summary>
		/// <param name="value">A byte value written to the stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying memory does not support writing.- or -An attempt is made to write to the stream and the <see cref="P:System.IO.UnmanagedMemoryStream.CanWrite" /> property is false.- or - The current position is at the end of the capacity of the stream.</exception>
		/// <exception cref="T:System.IO.IOException">The supplied <paramref name="value" /> causes the stream exceed its maximum capacity.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002D5E RID: 11614 RVA: 0x000A25A8 File Offset: 0x000A07A8
		[SecuritySafeCritical]
		public unsafe override void WriteByte(byte value)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + 1L;
			if (num >= num2)
			{
				if (num3 < 0L)
				{
					throw new IOException(Environment.GetResourceString("Stream was too long."));
				}
				if (num3 > this._capacity)
				{
					throw new NotSupportedException(Environment.GetResourceString("Unable to expand length of this stream beyond its capacity."));
				}
				if (this._buffer == null)
				{
					if (num > num2)
					{
						Buffer.ZeroMemory(this._mem + num2, num - num2);
					}
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					(ptr + num)[this._offset] = value;
					goto IL_00DC;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			this._mem[num] = value;
			IL_00DC:
			Interlocked.Exchange(ref this._position, num3);
		}

		// Token: 0x04001789 RID: 6025
		private const long UnmanagedMemStreamMaxLength = 9223372036854775807L;

		// Token: 0x0400178A RID: 6026
		[SecurityCritical]
		private SafeBuffer _buffer;

		// Token: 0x0400178B RID: 6027
		[SecurityCritical]
		private unsafe byte* _mem;

		// Token: 0x0400178C RID: 6028
		private long _length;

		// Token: 0x0400178D RID: 6029
		private long _capacity;

		// Token: 0x0400178E RID: 6030
		private long _position;

		// Token: 0x0400178F RID: 6031
		private long _offset;

		// Token: 0x04001790 RID: 6032
		private FileAccess _access;

		// Token: 0x04001791 RID: 6033
		internal bool _isOpen;

		// Token: 0x04001792 RID: 6034
		[NonSerialized]
		private Task<int> _lastReadTask;
	}
}
