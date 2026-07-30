using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Creates a stream whose backing store is memory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A1 RID: 929
	[ComVisible(true)]
	[Serializable]
	public class MemoryStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class with an expandable capacity initialized to zero.</summary>
		// Token: 0x06002B12 RID: 11026 RVA: 0x0009981F File Offset: 0x00097A1F
		public MemoryStream()
			: this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class with an expandable capacity initialized as specified.</summary>
		/// <param name="capacity">The initial size of the internal array in bytes. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is negative. </exception>
		// Token: 0x06002B13 RID: 11027 RVA: 0x00099828 File Offset: 0x00097A28
		public MemoryStream(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("Capacity must be positive."));
			}
			this._buffer = new byte[capacity];
			this._capacity = capacity;
			this._expandable = true;
			this._writable = true;
			this._exposable = true;
			this._origin = 0;
			this._isOpen = true;
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create the current stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		// Token: 0x06002B14 RID: 11028 RVA: 0x0009988A File Offset: 0x00097A8A
		public MemoryStream(byte[] buffer)
			: this(buffer, true)
		{
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified byte array with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		// Token: 0x06002B15 RID: 11029 RVA: 0x00099894 File Offset: 0x00097A94
		public MemoryStream(byte[] buffer, bool writable)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			this._buffer = buffer;
			this._length = (this._capacity = buffer.Length);
			this._writable = writable;
			this._exposable = false;
			this._origin = 0;
			this._isOpen = true;
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region (index) of a byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="index">The index into <paramref name="buffer" /> at which the stream begins. </param>
		/// <param name="count">The length of the stream in bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x06002B16 RID: 11030 RVA: 0x000998F4 File Offset: 0x00097AF4
		public MemoryStream(byte[] buffer, int index, int count)
			: this(buffer, index, count, true, false)
		{
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region of a byte array, with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="index">The index in <paramref name="buffer" /> at which the stream begins. </param>
		/// <param name="count">The length of the stream in bytes. </param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> are negative. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x06002B17 RID: 11031 RVA: 0x00099901 File Offset: 0x00097B01
		public MemoryStream(byte[] buffer, int index, int count, bool writable)
			: this(buffer, index, count, writable, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region of a byte array, with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified, and the ability to call <see cref="M:System.IO.MemoryStream.GetBuffer" /> set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="index">The index into <paramref name="buffer" /> at which the stream begins. </param>
		/// <param name="count">The length of the stream in bytes. </param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing. </param>
		/// <param name="publiclyVisible">true to enable <see cref="M:System.IO.MemoryStream.GetBuffer" />, which returns the unsigned byte array from which the stream was created; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		// Token: 0x06002B18 RID: 11032 RVA: 0x00099910 File Offset: 0x00097B10
		public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			this._buffer = buffer;
			this._position = index;
			this._origin = index;
			this._length = (this._capacity = index + count);
			this._writable = writable;
			this._exposable = publiclyVisible;
			this._expandable = false;
			this._isOpen = true;
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>true if the stream is open.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x000999CC File Offset: 0x00097BCC
		public override bool CanRead
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>true if the stream is open.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000999CC File Offset: 0x00097BCC
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>true if the stream supports writing; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x000999D4 File Offset: 0x00097BD4
		public override bool CanWrite
		{
			get
			{
				return this._writable;
			}
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000999DC File Offset: 0x00097BDC
		private void EnsureWriteable()
		{
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.MemoryStream" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06002B1D RID: 11037 RVA: 0x000999EC File Offset: 0x00097BEC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._isOpen = false;
					this._writable = false;
					this._expandable = false;
					this._lastReadTask = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x00099A34 File Offset: 0x00097C34
		private bool EnsureCapacity(int value)
		{
			if (value < 0)
			{
				throw new IOException(Environment.GetResourceString("Stream was too long."));
			}
			if (value > this._capacity)
			{
				int num = value;
				if (num < 256)
				{
					num = 256;
				}
				if (num < this._capacity * 2)
				{
					num = this._capacity * 2;
				}
				if (this._capacity * 2 > 2147483591)
				{
					num = ((value > 2147483591) ? value : 2147483591);
				}
				this.Capacity = num;
				return true;
			}
			return false;
		}

		/// <summary>Overrides the <see cref="M:System.IO.Stream.Flush" /> method so that no action is performed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B1F RID: 11039 RVA: 0x00002194 File Offset: 0x00000394
		public override void Flush()
		{
		}

		/// <summary>Asynchronously clears all buffers for this stream, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06002B20 RID: 11040 RVA: 0x00099AAC File Offset: 0x00097CAC
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

		/// <summary>Returns the array of unsigned bytes from which this stream was created.</summary>
		/// <returns>The byte array from which this stream was created, or the underlying array if a byte array was not provided to the <see cref="T:System.IO.MemoryStream" /> constructor during construction of the current instance.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The MemoryStream instance was not created with a publicly visible buffer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B21 RID: 11041 RVA: 0x00099AF4 File Offset: 0x00097CF4
		public virtual byte[] GetBuffer()
		{
			if (!this._exposable)
			{
				throw new UnauthorizedAccessException(Environment.GetResourceString("MemoryStream's internal buffer cannot be accessed."));
			}
			return this._buffer;
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x00099B14 File Offset: 0x00097D14
		public virtual bool TryGetBuffer(out ArraySegment<byte> buffer)
		{
			if (!this._exposable)
			{
				buffer = default(ArraySegment<byte>);
				return false;
			}
			buffer = new ArraySegment<byte>(this._buffer, this._origin, this._length - this._origin);
			return true;
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x00099B4C File Offset: 0x00097D4C
		internal byte[] InternalGetBuffer()
		{
			return this._buffer;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x00099B54 File Offset: 0x00097D54
		[FriendAccessAllowed]
		internal void InternalGetOriginAndLength(out int origin, out int length)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			origin = this._origin;
			length = this._length;
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x00099B73 File Offset: 0x00097D73
		internal int InternalGetPosition()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			return this._position;
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x00099B88 File Offset: 0x00097D88
		internal int InternalReadInt32()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			int num = (this._position += 4);
			if (num > this._length)
			{
				this._position = this._length;
				__Error.EndOfFile();
			}
			return (int)this._buffer[num - 4] | ((int)this._buffer[num - 3] << 8) | ((int)this._buffer[num - 2] << 16) | ((int)this._buffer[num - 1] << 24);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x00099C04 File Offset: 0x00097E04
		internal int InternalEmulateRead(int count)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num < 0)
			{
				num = 0;
			}
			this._position += num;
			return num;
		}

		/// <summary>Gets or sets the number of bytes allocated for this stream.</summary>
		/// <returns>The length of the usable portion of the buffer for the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A capacity is set that is negative or less than the current length of the stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">set is invoked on a stream whose capacity cannot be modified. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x00099C47 File Offset: 0x00097E47
		// (set) Token: 0x06002B29 RID: 11049 RVA: 0x00099C64 File Offset: 0x00097E64
		public virtual int Capacity
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return this._capacity - this._origin;
			}
			set
			{
				if ((long)value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("capacity was less than the current size."));
				}
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				if (!this._expandable && value != this.Capacity)
				{
					__Error.MemoryStreamNotExpandable();
				}
				if (this._expandable && value != this._capacity)
				{
					if (value > 0)
					{
						byte[] array = new byte[value];
						if (this._length > 0)
						{
							Buffer.InternalBlockCopy(this._buffer, 0, array, 0, this._length);
						}
						this._buffer = array;
					}
					else
					{
						this._buffer = null;
					}
					this._capacity = value;
				}
			}
		}

		/// <summary>Gets the length of the stream in bytes.</summary>
		/// <returns>The length of the stream in bytes.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x00099D04 File Offset: 0x00097F04
		public override long Length
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return (long)(this._length - this._origin);
			}
		}

		/// <summary>Gets or sets the current position within the stream.</summary>
		/// <returns>The current position within the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is set to a negative value or a value greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x00099D21 File Offset: 0x00097F21
		// (set) Token: 0x06002B2C RID: 11052 RVA: 0x00099D40 File Offset: 0x00097F40
		public override long Position
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return (long)(this._position - this._origin);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Non-negative number required."));
				}
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
				}
				this._position = this._origin + (int)value;
			}
		}

		/// <summary>Reads a block of bytes from the current stream and writes the data to a buffer.</summary>
		/// <returns>The total number of bytes written into the buffer. This can be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached before any bytes are read.</returns>
		/// <param name="buffer">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the characters read from the current stream. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing data from the current stream.</param>
		/// <param name="count">The maximum number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> subtracted from the buffer length is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B2D RID: 11053 RVA: 0x00099DA4 File Offset: 0x00097FA4
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
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
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num <= 0)
			{
				return 0;
			}
			if (num <= 8)
			{
				int num2 = num;
				while (--num2 >= 0)
				{
					buffer[offset + num2] = this._buffer[this._position + num2];
				}
			}
			else
			{
				Buffer.InternalBlockCopy(this._buffer, this._position, buffer, offset, num);
			}
			this._position += num;
			return num;
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. </returns>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation. </exception>
		// Token: 0x06002B2E RID: 11054 RVA: 0x00099E84 File Offset: 0x00098084
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
			catch (OperationCanceledException ex)
			{
				task = Task.FromCancellation<int>(ex);
			}
			catch (Exception ex2)
			{
				task = Task.FromException<int>(ex2);
			}
			return task;
		}

		/// <summary>Reads a byte from the current stream.</summary>
		/// <returns>The byte cast to a <see cref="T:System.Int32" />, or -1 if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B2F RID: 11055 RVA: 0x00099F64 File Offset: 0x00098164
		public override int ReadByte()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (this._position >= this._length)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			return buffer[position];
		}

		/// <summary>Asynchronously reads all the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token.</summary>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is negative or zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x06002B30 RID: 11056 RVA: 0x00099FA8 File Offset: 0x000981A8
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("Positive number required."));
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed Stream."));
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", Environment.GetResourceString("Cannot access a closed Stream."));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support writing."));
			}
			if (base.GetType() != typeof(MemoryStream))
			{
				return base.CopyToAsync(destination, bufferSize, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			int position = this._position;
			int num = this.InternalEmulateRead(this._length - this._position);
			MemoryStream memoryStream = destination as MemoryStream;
			if (memoryStream == null)
			{
				return destination.WriteAsync(this._buffer, position, num, cancellationToken);
			}
			Task task;
			try
			{
				memoryStream.Write(this._buffer, position, num);
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		/// <summary>Sets the position within the current stream to the specified value.</summary>
		/// <returns>The new position within the stream, calculated by combining the initial reference point and the offset.</returns>
		/// <param name="offset">The new position within the stream. This is relative to the <paramref name="loc" /> parameter, and can be positive or negative. </param>
		/// <param name="loc">A value of type <see cref="T:System.IO.SeekOrigin" />, which acts as the seek reference point. </param>
		/// <exception cref="T:System.IO.IOException">Seeking is attempted before the beginning of the stream. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentException">There is an invalid <see cref="T:System.IO.SeekOrigin" />. -or-<paramref name="offset" /> caused an arithmetic overflow.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B31 RID: 11057 RVA: 0x0009A0E8 File Offset: 0x000982E8
		public override long Seek(long offset, SeekOrigin loc)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
			}
			switch (loc)
			{
			case SeekOrigin.Begin:
			{
				int num = this._origin + (int)offset;
				if (offset < 0L || num < this._origin)
				{
					throw new IOException(Environment.GetResourceString("An attempt was made to move the position before the beginning of the stream."));
				}
				this._position = num;
				break;
			}
			case SeekOrigin.Current:
			{
				int num2 = this._position + (int)offset;
				if ((long)this._position + offset < (long)this._origin || num2 < this._origin)
				{
					throw new IOException(Environment.GetResourceString("An attempt was made to move the position before the beginning of the stream."));
				}
				this._position = num2;
				break;
			}
			case SeekOrigin.End:
			{
				int num3 = this._length + (int)offset;
				if ((long)this._length + offset < (long)this._origin || num3 < this._origin)
				{
					throw new IOException(Environment.GetResourceString("An attempt was made to move the position before the beginning of the stream."));
				}
				this._position = num3;
				break;
			}
			default:
				throw new ArgumentException(Environment.GetResourceString("Invalid seek origin."));
			}
			return (long)this._position;
		}

		/// <summary>Sets the length of the current stream to the specified value.</summary>
		/// <param name="value">The value at which to set the length. </param>
		/// <exception cref="T:System.NotSupportedException">The current stream is not resizable and <paramref name="value" /> is larger than the current capacity.-or- The current stream does not support writing. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is negative or is greater than the maximum length of the <see cref="T:System.IO.MemoryStream" />, where the maximum length is(<see cref="F:System.Int32.MaxValue" /> - origin), and origin is the index into the underlying buffer at which the stream starts. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B32 RID: 11058 RVA: 0x0009A200 File Offset: 0x00098400
		public override void SetLength(long value)
		{
			if (value < 0L || value > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
			}
			this.EnsureWriteable();
			if (value > (long)(2147483647 - this._origin))
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
			}
			int num = this._origin + (int)value;
			if (!this.EnsureCapacity(num) && num > this._length)
			{
				Array.Clear(this._buffer, this._length, num - this._length);
			}
			this._length = num;
			if (this._position > num)
			{
				this._position = num;
			}
		}

		/// <summary>Writes the stream contents to a byte array, regardless of the <see cref="P:System.IO.MemoryStream.Position" /> property.</summary>
		/// <returns>A new byte array.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B33 RID: 11059 RVA: 0x0009A2A8 File Offset: 0x000984A8
		public virtual byte[] ToArray()
		{
			if (this._length - this._origin == 0)
			{
				return EmptyArray<byte>.Value;
			}
			byte[] array = new byte[this._length - this._origin];
			Buffer.InternalBlockCopy(this._buffer, this._origin, array, 0, this._length - this._origin);
			return array;
		}

		/// <summary>Writes a block of bytes to the current stream using data read from a buffer.</summary>
		/// <param name="buffer">The buffer to write data from. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. For additional information see <see cref="P:System.IO.Stream.CanWrite" />.-or- The current position is closer than <paramref name="count" /> bytes to the end of the stream, and the capacity cannot be modified. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> subtracted from the buffer length is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> are negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B34 RID: 11060 RVA: 0x0009A300 File Offset: 0x00098500
		public override void Write(byte[] buffer, int offset, int count)
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
			this.EnsureWriteable();
			int num = this._position + count;
			if (num < 0)
			{
				throw new IOException(Environment.GetResourceString("Stream was too long."));
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			if (count <= 8 && buffer != this._buffer)
			{
				int num2 = count;
				while (--num2 >= 0)
				{
					this._buffer[this._position + num2] = buffer[offset + num2];
				}
			}
			else
			{
				Buffer.InternalBlockCopy(buffer, offset, this._buffer, this._position, count);
			}
			this._position = num;
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation. </exception>
		// Token: 0x06002B35 RID: 11061 RVA: 0x0009A43C File Offset: 0x0009863C
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
			catch (OperationCanceledException ex)
			{
				task = Task.FromCancellation<VoidTaskResult>(ex);
			}
			catch (Exception ex2)
			{
				task = Task.FromException(ex2);
			}
			return task;
		}

		/// <summary>Writes a byte to the current stream at the current position.</summary>
		/// <param name="value">The byte to write. </param>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. For additional information see <see cref="P:System.IO.Stream.CanWrite" />.-or- The current position is at the end of the stream, and the capacity cannot be modified. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B36 RID: 11062 RVA: 0x0009A4FC File Offset: 0x000986FC
		public override void WriteByte(byte value)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			this.EnsureWriteable();
			if (this._position >= this._length)
			{
				int num = this._position + 1;
				bool flag = this._position > this._length;
				if (num >= this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, this._position - this._length);
				}
				this._length = num;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		/// <summary>Writes the entire contents of this memory stream to another stream.</summary>
		/// <param name="stream">The stream to write this memory stream to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current or target stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B37 RID: 11063 RVA: 0x0009A598 File Offset: 0x00098798
		public virtual void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", Environment.GetResourceString("Stream cannot be null."));
			}
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			stream.Write(this._buffer, this._origin, this._length - this._origin);
		}

		// Token: 0x040016A6 RID: 5798
		private byte[] _buffer;

		// Token: 0x040016A7 RID: 5799
		private int _origin;

		// Token: 0x040016A8 RID: 5800
		private int _position;

		// Token: 0x040016A9 RID: 5801
		private int _length;

		// Token: 0x040016AA RID: 5802
		private int _capacity;

		// Token: 0x040016AB RID: 5803
		private bool _expandable;

		// Token: 0x040016AC RID: 5804
		private bool _writable;

		// Token: 0x040016AD RID: 5805
		private bool _exposable;

		// Token: 0x040016AE RID: 5806
		private bool _isOpen;

		// Token: 0x040016AF RID: 5807
		[NonSerialized]
		private Task<int> _lastReadTask;

		// Token: 0x040016B0 RID: 5808
		private const int MemStreamMaxLength = 2147483647;
	}
}
