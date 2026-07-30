using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Adds a buffering layer to read and write operations on another stream. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000388 RID: 904
	[ComVisible(true)]
	public sealed class BufferedStream : Stream
	{
		// Token: 0x06002A06 RID: 10758 RVA: 0x00095473 File Offset: 0x00093673
		private BufferedStream()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BufferedStream" /> class with a default buffer size of 4096 bytes.</summary>
		/// <param name="stream">The current stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06002A07 RID: 10759 RVA: 0x0009547B File Offset: 0x0009367B
		public BufferedStream(Stream stream)
			: this(stream, 4096)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BufferedStream" /> class with the specified buffer size.</summary>
		/// <param name="stream">The current stream. </param>
		/// <param name="bufferSize">The buffer size in bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		// Token: 0x06002A08 RID: 10760 RVA: 0x0009548C File Offset: 0x0009368C
		public BufferedStream(Stream stream, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("'{0}' must be greater than zero.", new object[] { "bufferSize" }));
			}
			this._stream = stream;
			this._bufferSize = bufferSize;
			if (!this._stream.CanRead && !this._stream.CanWrite)
			{
				__Error.StreamIsClosed();
			}
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x00095501 File Offset: 0x00093701
		private void EnsureNotClosed()
		{
			if (this._stream == null)
			{
				__Error.StreamIsClosed();
			}
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x00095510 File Offset: 0x00093710
		private void EnsureCanSeek()
		{
			if (!this._stream.CanSeek)
			{
				__Error.SeekNotSupported();
			}
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x00095524 File Offset: 0x00093724
		private void EnsureCanRead()
		{
			if (!this._stream.CanRead)
			{
				__Error.ReadNotSupported();
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00095538 File Offset: 0x00093738
		private void EnsureCanWrite()
		{
			if (!this._stream.CanWrite)
			{
				__Error.WriteNotSupported();
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x0009554C File Offset: 0x0009374C
		private void EnsureBeginEndAwaitableAllocated()
		{
			if (this._beginEndAwaitable == null)
			{
				this._beginEndAwaitable = new BeginEndAwaitableAdapter();
			}
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00095564 File Offset: 0x00093764
		private void EnsureShadowBufferAllocated()
		{
			if (this._buffer.Length != this._bufferSize || this._bufferSize >= 81920)
			{
				return;
			}
			byte[] array = new byte[Math.Min(this._bufferSize + this._bufferSize, 81920)];
			Buffer.InternalBlockCopy(this._buffer, 0, array, 0, this._writePos);
			this._buffer = array;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000955C8 File Offset: 0x000937C8
		private void EnsureBufferAllocated()
		{
			if (this._buffer == null)
			{
				this._buffer = new byte[this._bufferSize];
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000955E3 File Offset: 0x000937E3
		internal Stream UnderlyingStream
		{
			[FriendAccessAllowed]
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x000955EB File Offset: 0x000937EB
		internal int BufferSize
		{
			[FriendAccessAllowed]
			get
			{
				return this._bufferSize;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>true if the stream supports reading; false if the stream is closed or was opened with write-only access.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x000955F3 File Offset: 0x000937F3
		public override bool CanRead
		{
			get
			{
				return this._stream != null && this._stream.CanRead;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>true if the stream supports writing; false if the stream is closed or was opened with read-only access.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002A13 RID: 10771 RVA: 0x0009560A File Offset: 0x0009380A
		public override bool CanWrite
		{
			get
			{
				return this._stream != null && this._stream.CanWrite;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>true if the stream supports seeking; false if the stream is closed or if the stream was constructed from an operating system handle such as a pipe or output to the console.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00095621 File Offset: 0x00093821
		public override bool CanSeek
		{
			get
			{
				return this._stream != null && this._stream.CanSeek;
			}
		}

		/// <summary>Gets the stream length in bytes.</summary>
		/// <returns>The stream length in bytes.</returns>
		/// <exception cref="T:System.IO.IOException">The underlying stream is null or closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002A15 RID: 10773 RVA: 0x00095638 File Offset: 0x00093838
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				return this._stream.Length;
			}
		}

		/// <summary>Gets the position within the current stream.</summary>
		/// <returns>The position within the current stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value passed to <see cref="M:System.IO.BufferedStream.Seek(System.Int64,System.IO.SeekOrigin)" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the stream being closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x0009565A File Offset: 0x0009385A
		// (set) Token: 0x06002A17 RID: 10775 RVA: 0x0009568C File Offset: 0x0009388C
		public override long Position
		{
			get
			{
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				return this._stream.Position + (long)(this._readPos - this._readLen + this._writePos);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Non-negative number required."));
				}
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				this._readPos = 0;
				this._readLen = 0;
				this._stream.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000956EC File Offset: 0x000938EC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._stream != null)
				{
					try
					{
						this.Flush();
					}
					finally
					{
						this._stream.Close();
					}
				}
			}
			finally
			{
				this._stream = null;
				this._buffer = null;
				this._lastSyncCompletedReadTask = null;
				base.Dispose(disposing);
			}
		}

		/// <summary>Clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.IO.IOException">The data source or repository is not open. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A19 RID: 10777 RVA: 0x00095754 File Offset: 0x00093954
		public override void Flush()
		{
			this.EnsureNotClosed();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return;
			}
			if (this._readPos >= this._readLen)
			{
				if (this._stream.CanWrite || this._stream is BufferedStream)
				{
					this._stream.Flush();
				}
				this._writePos = (this._readPos = (this._readLen = 0));
				return;
			}
			if (!this._stream.CanSeek)
			{
				return;
			}
			this.FlushRead();
			if (this._stream.CanWrite || this._stream is BufferedStream)
			{
				this._stream.Flush();
			}
		}

		/// <summary>Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06002A1A RID: 10778 RVA: 0x000957FD File Offset: 0x000939FD
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			return BufferedStream.FlushAsyncInternal(cancellationToken, this, this._stream, this._writePos, this._readPos, this._readLen);
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x00095834 File Offset: 0x00093A34
		private static async Task FlushAsyncInternal(CancellationToken cancellationToken, BufferedStream _this, Stream stream, int writePos, int readPos, int readLen)
		{
			SemaphoreSlim sem = _this.EnsureAsyncActiveSemaphoreInitialized();
			await sem.WaitAsync().ConfigureAwait(false);
			try
			{
				if (writePos > 0)
				{
					await _this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
				}
				else if (readPos < readLen)
				{
					if (stream.CanSeek)
					{
						_this.FlushRead();
						if (stream.CanRead || stream is BufferedStream)
						{
							await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
						}
					}
				}
				else if (stream.CanWrite || stream is BufferedStream)
				{
					await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
				}
			}
			finally
			{
				sem.Release();
			}
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000958A3 File Offset: 0x00093AA3
		private void FlushRead()
		{
			if (this._readPos - this._readLen != 0)
			{
				this._stream.Seek((long)(this._readPos - this._readLen), SeekOrigin.Current);
			}
			this._readPos = 0;
			this._readLen = 0;
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000958E0 File Offset: 0x00093AE0
		private void ClearReadBufferBeforeWrite()
		{
			if (this._readPos == this._readLen)
			{
				this._readPos = (this._readLen = 0);
				return;
			}
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException(Environment.GetResourceString("Cannot write to a BufferedStream while the read buffer is not empty if the underlying stream is not seekable. Ensure that the stream underlying this BufferedStream can seek or avoid interleaving read and write operations on this BufferedStream."));
			}
			this.FlushRead();
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x0009592F File Offset: 0x00093B2F
		private void FlushWrite()
		{
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this._stream.Flush();
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x0009595C File Offset: 0x00093B5C
		private async Task FlushWriteAsync(CancellationToken cancellationToken)
		{
			await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
			this._writePos = 0;
			await this._stream.FlushAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000959AC File Offset: 0x00093BAC
		private int ReadFromBuffer(byte[] array, int offset, int count)
		{
			int num = this._readLen - this._readPos;
			if (num == 0)
			{
				return 0;
			}
			if (num > count)
			{
				num = count;
			}
			Buffer.InternalBlockCopy(this._buffer, this._readPos, array, offset, num);
			this._readPos += num;
			return num;
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000959F8 File Offset: 0x00093BF8
		private int ReadFromBuffer(byte[] array, int offset, int count, out Exception error)
		{
			int num;
			try
			{
				error = null;
				num = this.ReadFromBuffer(array, offset, count);
			}
			catch (Exception ex)
			{
				error = ex;
				num = 0;
			}
			return num;
		}

		/// <summary>Copies bytes from the current buffered stream to an array.</summary>
		/// <returns>The total number of bytes read into <paramref name="array" />. This can be less than the number of bytes requested if that many bytes are not currently available, or 0 if the end of the stream has been reached before any data can be read.</returns>
		/// <param name="array">The buffer to which bytes are to be copied. </param>
		/// <param name="offset">The byte offset in the buffer at which to begin reading bytes. </param>
		/// <param name="count">The number of bytes to be read. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="array" /> minus <paramref name="offset" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is not open or is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A22 RID: 10786 RVA: 0x00095A30 File Offset: 0x00093C30
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = this.ReadFromBuffer(array, offset, count);
			if (num == count)
			{
				return num;
			}
			int num2 = num;
			if (num > 0)
			{
				count -= num;
				offset += num;
			}
			this._readPos = (this._readLen = 0);
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			if (count >= this._bufferSize)
			{
				return this._stream.Read(array, offset, count) + num2;
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			num = this.ReadFromBuffer(array, offset, count);
			return num + num2;
		}

		/// <summary>Begins an asynchronous read operation. (Consider using <see cref="M:System.IO.BufferedStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <returns>An object that represents the asynchronous read, which could still be pending.</returns>
		/// <param name="buffer">The buffer to read the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data read from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="callback">An optional asynchronous callback, to be called when the read is complete.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">Attempted an asynchronous read past the end of the stream.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="offset" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support the read operation. </exception>
		// Token: 0x06002A23 RID: 10787 RVA: 0x00095B38 File Offset: 0x00093D38
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
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
			if (this._stream == null)
			{
				__Error.ReadNotSupported();
			}
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = base.EnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.Status == TaskStatus.RanToCompletion)
			{
				bool flag = true;
				try
				{
					Exception ex;
					num = this.ReadFromBuffer(buffer, offset, count, out ex);
					flag = num == count || ex != null;
					if (flag)
					{
						Stream.SynchronousAsyncResult synchronousAsyncResult = ((ex == null) ? new Stream.SynchronousAsyncResult(num, state) : new Stream.SynchronousAsyncResult(ex, state, false));
						if (callback != null)
						{
							callback(synchronousAsyncResult);
						}
						return synchronousAsyncResult;
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.BeginReadFromUnderlyingStream(buffer, offset + num, count - num, callback, state, num, task);
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00095C54 File Offset: 0x00093E54
		private IAsyncResult BeginReadFromUnderlyingStream(byte[] buffer, int offset, int count, AsyncCallback callback, object state, int bytesAlreadySatisfied, Task semaphoreLockTask)
		{
			return TaskToApm.Begin(this.ReadFromUnderlyingStreamAsync(buffer, offset, count, CancellationToken.None, bytesAlreadySatisfied, semaphoreLockTask, true), callback, state);
		}

		/// <summary>Waits for the pending asynchronous read operation to complete. (Consider using <see cref="M:System.IO.BufferedStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <returns>The number of bytes read from the stream, between 0 (zero) and the number of bytes you requested. Streams only return 0 only at the end of the stream, otherwise, they should block until at least 1 byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.BufferedStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class. </exception>
		// Token: 0x06002A25 RID: 10789 RVA: 0x00095C72 File Offset: 0x00093E72
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (asyncResult is Stream.SynchronousAsyncResult)
			{
				return Stream.SynchronousAsyncResult.EndRead(asyncResult);
			}
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x00095C98 File Offset: 0x00093E98
		private Task<int> LastSyncCompletedReadTask(int val)
		{
			Task<int> task = this._lastSyncCompletedReadTask;
			if (task != null && task.Result == val)
			{
				return task;
			}
			task = Task.FromResult<int>(val);
			this._lastSyncCompletedReadTask = task;
			return task;
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. </returns>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation. </exception>
		// Token: 0x06002A27 RID: 10791 RVA: 0x00095CCC File Offset: 0x00093ECC
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
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = base.EnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.Status == TaskStatus.RanToCompletion)
			{
				bool flag = true;
				try
				{
					Exception ex;
					num = this.ReadFromBuffer(buffer, offset, count, out ex);
					flag = num == count || ex != null;
					if (flag)
					{
						return (ex == null) ? this.LastSyncCompletedReadTask(num) : Task.FromException<int>(ex);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.ReadFromUnderlyingStreamAsync(buffer, offset + num, count - num, cancellationToken, num, task, false);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00095DDC File Offset: 0x00093FDC
		private async Task<int> ReadFromUnderlyingStreamAsync(byte[] array, int offset, int count, CancellationToken cancellationToken, int bytesAlreadySatisfied, Task semaphoreLockTask, bool useApmPattern)
		{
			await semaphoreLockTask.ConfigureAwait(false);
			int num2;
			try
			{
				int num = this.ReadFromBuffer(array, offset, count);
				if (num == count)
				{
					num2 = bytesAlreadySatisfied + num;
				}
				else
				{
					if (num > 0)
					{
						count -= num;
						offset += num;
						bytesAlreadySatisfied += num;
					}
					int num3 = 0;
					this._readLen = num3;
					this._readPos = num3;
					if (this._writePos > 0)
					{
						await this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
					}
					if (count >= this._bufferSize)
					{
						if (useApmPattern)
						{
							this.EnsureBeginEndAwaitableAllocated();
							this._stream.BeginRead(array, offset, count, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
							int num4 = bytesAlreadySatisfied;
							Stream stream = this._stream;
							num2 = num4 + stream.EndRead(await this._beginEndAwaitable);
						}
						else
						{
							int num4 = bytesAlreadySatisfied;
							num2 = num4 + await this._stream.ReadAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
						}
					}
					else
					{
						this.EnsureBufferAllocated();
						if (useApmPattern)
						{
							this.EnsureBeginEndAwaitableAllocated();
							this._stream.BeginRead(this._buffer, 0, this._bufferSize, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
							Stream stream = this._stream;
							this._readLen = stream.EndRead(await this._beginEndAwaitable);
							stream = null;
						}
						else
						{
							this._readLen = await this._stream.ReadAsync(this._buffer, 0, this._bufferSize, cancellationToken).ConfigureAwait(false);
						}
						num2 = bytesAlreadySatisfied + this.ReadFromBuffer(array, offset, count);
					}
				}
			}
			finally
			{
				base.EnsureAsyncActiveSemaphoreInitialized().Release();
			}
			return num2;
		}

		/// <summary>Reads a byte from the underlying stream and returns the byte cast to an int, or returns -1 if reading from the end of the stream.</summary>
		/// <returns>The byte cast to an int, or -1 if reading from the end of the stream.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the stream being closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A29 RID: 10793 RVA: 0x00095E60 File Offset: 0x00094060
		public override int ReadByte()
		{
			this.EnsureNotClosed();
			this.EnsureCanRead();
			if (this._readPos == this._readLen)
			{
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				this.EnsureBufferAllocated();
				this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
				this._readPos = 0;
			}
			if (this._readPos == this._readLen)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int readPos = this._readPos;
			this._readPos = readPos + 1;
			return buffer[readPos];
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x00095EEC File Offset: 0x000940EC
		private void WriteToBuffer(byte[] array, ref int offset, ref int count)
		{
			int num = Math.Min(this._bufferSize - this._writePos, count);
			if (num <= 0)
			{
				return;
			}
			this.EnsureBufferAllocated();
			Buffer.InternalBlockCopy(array, offset, this._buffer, this._writePos, num);
			this._writePos += num;
			count -= num;
			offset += num;
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x00095F4C File Offset: 0x0009414C
		private void WriteToBuffer(byte[] array, ref int offset, ref int count, out Exception error)
		{
			try
			{
				error = null;
				this.WriteToBuffer(array, ref offset, ref count);
			}
			catch (Exception ex)
			{
				error = ex;
			}
		}

		/// <summary>Copies bytes to the buffered stream and advances the current position within the buffered stream by the number of bytes written.</summary>
		/// <param name="array">The byte array from which to copy <paramref name="count" /> bytes to the current buffered stream. </param>
		/// <param name="offset">The offset in the buffer at which to begin copying bytes to the current buffered stream. </param>
		/// <param name="count">The number of bytes to be written to the current buffered stream. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="array" /> minus <paramref name="offset" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or null. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A2C RID: 10796 RVA: 0x00095F80 File Offset: 0x00094180
		public override void Write(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			if (this._writePos == 0)
			{
				this.ClearReadBufferBeforeWrite();
			}
			int num = checked(this._writePos + count);
			if (checked(num + count >= this._bufferSize + this._bufferSize))
			{
				if (this._writePos > 0)
				{
					if (num <= this._bufferSize + this._bufferSize && num <= 81920)
					{
						this.EnsureShadowBufferAllocated();
						Buffer.InternalBlockCopy(array, offset, this._buffer, this._writePos, count);
						this._stream.Write(this._buffer, 0, num);
						this._writePos = 0;
						return;
					}
					this._stream.Write(this._buffer, 0, this._writePos);
					this._writePos = 0;
				}
				this._stream.Write(array, offset, count);
				return;
			}
			this.WriteToBuffer(array, ref offset, ref count);
			if (this._writePos < this._bufferSize)
			{
				return;
			}
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this.WriteToBuffer(array, ref offset, ref count);
		}

		/// <summary>Begins an asynchronous write operation. (Consider using <see cref="M:System.IO.BufferedStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <returns>An object that references the asynchronous write which could still be pending.</returns>
		/// <param name="buffer">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="callback">The method to be called when the asynchronous write operation is completed.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffer" /> length minus <paramref name="offset" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
		// Token: 0x06002A2D RID: 10797 RVA: 0x000960F0 File Offset: 0x000942F0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
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
			if (this._stream == null)
			{
				__Error.ReadNotSupported();
			}
			this.EnsureCanWrite();
			SemaphoreSlim semaphoreSlim = base.EnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.Status == TaskStatus.RanToCompletion)
			{
				bool flag = true;
				try
				{
					if (this._writePos == 0)
					{
						this.ClearReadBufferBeforeWrite();
					}
					flag = count < this._bufferSize - this._writePos;
					if (flag)
					{
						Exception ex;
						this.WriteToBuffer(buffer, ref offset, ref count, out ex);
						Stream.SynchronousAsyncResult synchronousAsyncResult = ((ex == null) ? new Stream.SynchronousAsyncResult(state) : new Stream.SynchronousAsyncResult(ex, state, true));
						if (callback != null)
						{
							callback(synchronousAsyncResult);
						}
						return synchronousAsyncResult;
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.BeginWriteToUnderlyingStream(buffer, offset, count, callback, state, task);
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x00096214 File Offset: 0x00094414
		private IAsyncResult BeginWriteToUnderlyingStream(byte[] buffer, int offset, int count, AsyncCallback callback, object state, Task semaphoreLockTask)
		{
			return TaskToApm.Begin(this.WriteToUnderlyingStreamAsync(buffer, offset, count, CancellationToken.None, semaphoreLockTask, true), callback, state);
		}

		/// <summary>Ends an asynchronous write operation and blocks until the I/O operation is complete. (Consider using <see cref="M:System.IO.BufferedStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <param name="asyncResult">The pending asynchronous request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.BufferedStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class. </exception>
		// Token: 0x06002A2F RID: 10799 RVA: 0x00096230 File Offset: 0x00094430
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (asyncResult is Stream.SynchronousAsyncResult)
			{
				Stream.SynchronousAsyncResult.EndWrite(asyncResult);
				return;
			}
			TaskToApm.End(asyncResult);
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation. </exception>
		// Token: 0x06002A30 RID: 10800 RVA: 0x00096258 File Offset: 0x00094458
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
				return Task.FromCancellation<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			SemaphoreSlim semaphoreSlim = base.EnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.Status == TaskStatus.RanToCompletion)
			{
				bool flag = true;
				try
				{
					if (this._writePos == 0)
					{
						this.ClearReadBufferBeforeWrite();
					}
					flag = count < this._bufferSize - this._writePos;
					if (flag)
					{
						Exception ex;
						this.WriteToBuffer(buffer, ref offset, ref count, out ex);
						return (ex == null) ? Task.CompletedTask : Task.FromException(ex);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.WriteToUnderlyingStreamAsync(buffer, offset, count, cancellationToken, task, false);
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x00096370 File Offset: 0x00094570
		private async Task WriteToUnderlyingStreamAsync(byte[] array, int offset, int count, CancellationToken cancellationToken, Task semaphoreLockTask, bool useApmPattern)
		{
			await semaphoreLockTask.ConfigureAwait(false);
			try
			{
				if (this._writePos == 0)
				{
					this.ClearReadBufferBeforeWrite();
				}
				int totalUserBytes = checked(this._writePos + count);
				if (checked(totalUserBytes + count < this._bufferSize + this._bufferSize))
				{
					this.WriteToBuffer(array, ref offset, ref count);
					if (this._writePos >= this._bufferSize)
					{
						if (useApmPattern)
						{
							this.EnsureBeginEndAwaitableAllocated();
							this._stream.BeginWrite(this._buffer, 0, this._writePos, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
							Stream stream = this._stream;
							stream.EndWrite(await this._beginEndAwaitable);
							stream = null;
						}
						else
						{
							await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
						}
						this._writePos = 0;
						this.WriteToBuffer(array, ref offset, ref count);
					}
				}
				else
				{
					if (this._writePos > 0)
					{
						if (totalUserBytes <= this._bufferSize + this._bufferSize && totalUserBytes <= 81920)
						{
							this.EnsureShadowBufferAllocated();
							Buffer.InternalBlockCopy(array, offset, this._buffer, this._writePos, count);
							if (useApmPattern)
							{
								this.EnsureBeginEndAwaitableAllocated();
								this._stream.BeginWrite(this._buffer, 0, totalUserBytes, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
								Stream stream = this._stream;
								stream.EndWrite(await this._beginEndAwaitable);
								stream = null;
							}
							else
							{
								await this._stream.WriteAsync(this._buffer, 0, totalUserBytes, cancellationToken).ConfigureAwait(false);
							}
							this._writePos = 0;
							return;
						}
						if (useApmPattern)
						{
							this.EnsureBeginEndAwaitableAllocated();
							this._stream.BeginWrite(this._buffer, 0, this._writePos, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
							Stream stream = this._stream;
							stream.EndWrite(await this._beginEndAwaitable);
							stream = null;
						}
						else
						{
							await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
						}
						this._writePos = 0;
					}
					if (useApmPattern)
					{
						this.EnsureBeginEndAwaitableAllocated();
						this._stream.BeginWrite(array, offset, count, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
						Stream stream = this._stream;
						stream.EndWrite(await this._beginEndAwaitable);
						stream = null;
					}
					else
					{
						await this._stream.WriteAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
					}
				}
			}
			finally
			{
				base.EnsureAsyncActiveSemaphoreInitialized().Release();
			}
		}

		/// <summary>Writes a byte to the current position in the buffered stream.</summary>
		/// <param name="value">A byte to write to the stream. </param>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A32 RID: 10802 RVA: 0x000963E8 File Offset: 0x000945E8
		public override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			if (this._writePos == 0)
			{
				this.EnsureCanWrite();
				this.ClearReadBufferBeforeWrite();
				this.EnsureBufferAllocated();
			}
			if (this._writePos >= this._bufferSize - 1)
			{
				this.FlushWrite();
			}
			byte[] buffer = this._buffer;
			int writePos = this._writePos;
			this._writePos = writePos + 1;
			buffer[writePos] = value;
		}

		/// <summary>Sets the position within the current buffered stream.</summary>
		/// <returns>The new position within the current buffered stream.</returns>
		/// <param name="offset">A byte offset relative to <paramref name="origin" />. </param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point from which to obtain the new position. </param>
		/// <exception cref="T:System.IO.IOException">The stream is not open or is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A33 RID: 10803 RVA: 0x00096444 File Offset: 0x00094644
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return this._stream.Seek(offset, origin);
			}
			if (this._readLen - this._readPos > 0 && origin == SeekOrigin.Current)
			{
				offset -= (long)(this._readLen - this._readPos);
			}
			long position = this.Position;
			long num = this._stream.Seek(offset, origin);
			this._readPos = (int)(num - (position - (long)this._readPos));
			if (0 <= this._readPos && this._readPos < this._readLen)
			{
				this._stream.Seek((long)(this._readLen - this._readPos), SeekOrigin.Current);
			}
			else
			{
				this._readPos = (this._readLen = 0);
			}
			return num;
		}

		/// <summary>Sets the length of the buffered stream.</summary>
		/// <param name="value">An integer indicating the desired length of the current buffered stream in bytes. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is not open or is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002A34 RID: 10804 RVA: 0x0009650C File Offset: 0x0009470C
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Length must be non-negative."));
			}
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			this.EnsureCanWrite();
			this.Flush();
			this._stream.SetLength(value);
		}

		// Token: 0x04001649 RID: 5705
		private const int _DefaultBufferSize = 4096;

		// Token: 0x0400164A RID: 5706
		private Stream _stream;

		// Token: 0x0400164B RID: 5707
		private byte[] _buffer;

		// Token: 0x0400164C RID: 5708
		private readonly int _bufferSize;

		// Token: 0x0400164D RID: 5709
		private int _readPos;

		// Token: 0x0400164E RID: 5710
		private int _readLen;

		// Token: 0x0400164F RID: 5711
		private int _writePos;

		// Token: 0x04001650 RID: 5712
		private BeginEndAwaitableAdapter _beginEndAwaitable;

		// Token: 0x04001651 RID: 5713
		private Task<int> _lastSyncCompletedReadTask;

		// Token: 0x04001652 RID: 5714
		private const int MaxShadowBufferSize = 81920;
	}
}
