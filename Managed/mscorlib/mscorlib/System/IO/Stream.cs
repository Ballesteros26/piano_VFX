using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Provides a generic view of a sequence of bytes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A4 RID: 932
	[ComVisible(true)]
	[Serializable]
	public abstract class Stream : MarshalByRefObject, IDisposable
	{
		// Token: 0x06002B40 RID: 11072 RVA: 0x0009A6EF File Offset: 0x000988EF
		internal SemaphoreSlim EnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>true if the stream supports reading; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002B41 RID: 11073
		public abstract bool CanRead { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>true if the stream supports seeking; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002B42 RID: 11074
		public abstract bool CanSeek { get; }

		/// <summary>Gets a value that determines whether the current stream can time out.</summary>
		/// <returns>A value that determines whether the current stream can time out.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x00015ED5 File Offset: 0x000140D5
		[ComVisible(false)]
		public virtual bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>true if the stream supports writing; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002B44 RID: 11076
		public abstract bool CanWrite { get; }

		/// <summary>When overridden in a derived class, gets the length in bytes of the stream.</summary>
		/// <returns>A long value representing the length of the stream in bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002B45 RID: 11077
		public abstract long Length { get; }

		/// <summary>When overridden in a derived class, gets or sets the position within the current stream.</summary>
		/// <returns>The current position within the stream.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002B46 RID: 11078
		// (set) Token: 0x06002B47 RID: 11079
		public abstract long Position { get; set; }

		/// <summary>Gets or sets a value, in miliseconds, that determines how long the stream will attempt to read before timing out. </summary>
		/// <returns>A value, in miliseconds, that determines how long the stream will attempt to read before timing out.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Stream.ReadTimeout" /> method always throws an <see cref="T:System.InvalidOperationException" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x0009A71B File Offset: 0x0009891B
		// (set) Token: 0x06002B49 RID: 11081 RVA: 0x0009A71B File Offset: 0x0009891B
		[ComVisible(false)]
		public virtual int ReadTimeout
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("Timeouts are not supported on this stream."));
			}
			set
			{
				throw new InvalidOperationException(Environment.GetResourceString("Timeouts are not supported on this stream."));
			}
		}

		/// <summary>Gets or sets a value, in miliseconds, that determines how long the stream will attempt to write before timing out. </summary>
		/// <returns>A value, in miliseconds, that determines how long the stream will attempt to write before timing out.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Stream.WriteTimeout" /> method always throws an <see cref="T:System.InvalidOperationException" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x0009A71B File Offset: 0x0009891B
		// (set) Token: 0x06002B4B RID: 11083 RVA: 0x0009A71B File Offset: 0x0009891B
		[ComVisible(false)]
		public virtual int WriteTimeout
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("Timeouts are not supported on this stream."));
			}
			set
			{
				throw new InvalidOperationException(Environment.GetResourceString("Timeouts are not supported on this stream."));
			}
		}

		/// <summary>Asynchronously reads the bytes from the current stream and writes them to another stream.</summary>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x06002B4C RID: 11084 RVA: 0x0009A72C File Offset: 0x0009892C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task CopyToAsync(Stream destination)
		{
			return this.CopyToAsync(destination, 81920);
		}

		/// <summary>Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size.</summary>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero. The default size is 4096.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is negative or zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x06002B4D RID: 11085 RVA: 0x0009A73A File Offset: 0x0009893A
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task CopyToAsync(Stream destination, int bufferSize)
		{
			return this.CopyToAsync(destination, bufferSize, CancellationToken.None);
		}

		/// <summary>Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token.</summary>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero. The default size is 4096.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is negative or zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x06002B4E RID: 11086 RVA: 0x0009A74C File Offset: 0x0009894C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
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
			return this.CopyToAsyncInternal(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0009A800 File Offset: 0x00098A00
		private async Task CopyToAsyncInternal(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			byte[] buffer = new byte[bufferSize];
			int bytesRead;
			while ((bytesRead = await this.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
			{
				await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
			}
		}

		/// <summary>Reads the bytes from the current stream and writes them to another stream.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading.-or-<paramref name="destination" /> does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or <paramref name="destination" /> were closed before the <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> method was called.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06002B50 RID: 11088 RVA: 0x0009A860 File Offset: 0x00098A60
		public void CopyTo(Stream destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
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
			this.InternalCopyTo(destination, 81920);
		}

		/// <summary>Reads the bytes from the current stream and writes them to another stream, using a specified buffer size.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size of the buffer. This value must be greater than zero. The default size is 4096.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading.-or-<paramref name="destination" /> does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or <paramref name="destination" /> were closed before the <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> method was called.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06002B51 RID: 11089 RVA: 0x0009A900 File Offset: 0x00098B00
		public virtual void CopyTo(Stream destination, int bufferSize)
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
			this.InternalCopyTo(destination, bufferSize);
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0009A9B4 File Offset: 0x00098BB4
		private void InternalCopyTo(Stream destination, int bufferSize)
		{
			byte[] array = new byte[bufferSize];
			int num;
			while ((num = this.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, num);
			}
		}

		/// <summary>Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. Instead of calling this method, ensure that the stream is properly disposed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B53 RID: 11091 RVA: 0x0009A9E2 File Offset: 0x00098BE2
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.Stream" />.</summary>
		// Token: 0x06002B54 RID: 11092 RVA: 0x0009A9F1 File Offset: 0x00098BF1
		public void Dispose()
		{
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Stream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06002B55 RID: 11093 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B56 RID: 11094
		public abstract void Flush();

		/// <summary>Asynchronously clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06002B57 RID: 11095 RVA: 0x0009A9F9 File Offset: 0x00098BF9
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task FlushAsync()
		{
			return this.FlushAsync(CancellationToken.None);
		}

		/// <summary>Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06002B58 RID: 11096 RVA: 0x0009AA06 File Offset: 0x00098C06
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((Stream)state).Flush();
			}, this, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Allocates a <see cref="T:System.Threading.WaitHandle" /> object.</summary>
		/// <returns>A reference to the allocated WaitHandle.</returns>
		// Token: 0x06002B59 RID: 11097 RVA: 0x0009AA39 File Offset: 0x00098C39
		[Obsolete("CreateWaitHandle will be removed eventually.  Please use \"new ManualResetEvent(false)\" instead.")]
		protected virtual WaitHandle CreateWaitHandle()
		{
			return new ManualResetEvent(false);
		}

		/// <summary>Begins an asynchronous read operation. (Consider using <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> instead; see the Remarks section.)</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous read, which could still be pending.</returns>
		/// <param name="buffer">The buffer to read the data into. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data read from the stream. </param>
		/// <param name="count">The maximum number of bytes to read. </param>
		/// <param name="callback">An optional asynchronous callback, to be called when the read is complete. </param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests. </param>
		/// <exception cref="T:System.IO.IOException">Attempted an asynchronous read past the end of the stream, or a disk error occurs. </exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The current Stream implementation does not support the read operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B5A RID: 11098 RVA: 0x0009AA41 File Offset: 0x00098C41
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginReadInternal(buffer, offset, count, callback, state, false);
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x0009AA54 File Offset: 0x00098C54
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginReadInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
		{
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return this.BlockingBeginRead(buffer, offset, count, callback, state);
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(true, delegate
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int num = readWriteTask2._stream.Read(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				readWriteTask2.ClearBeginState();
				return num;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		/// <summary>Waits for the pending asynchronous read to complete. (Consider using <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> instead; see the Remarks section.)</summary>
		/// <returns>The number of bytes read from the stream, between zero (0) and the number of bytes you requested. Streams return zero (0) only at the end of the stream, otherwise, they should block until at least one byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A handle to the pending read operation is not available.-or-The pending operation does not support reading.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B5C RID: 11100 RVA: 0x0009AAE4 File Offset: 0x00098CE4
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return Stream.BlockingEndRead(asyncResult);
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult."));
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult."));
			}
			if (!activeReadWriteTask._isRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult."));
			}
			int result;
			try
			{
				result = activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this._activeReadWriteTask = null;
				this._asyncActiveSemaphore.Release();
			}
			return result;
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. </returns>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation. </exception>
		// Token: 0x06002B5D RID: 11101 RVA: 0x0009AB8C File Offset: 0x00098D8C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> ReadAsync(byte[] buffer, int offset, int count)
		{
			return this.ReadAsync(buffer, offset, count, CancellationToken.None);
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
		// Token: 0x06002B5E RID: 11102 RVA: 0x0009AB9C File Offset: 0x00098D9C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndReadAsync(buffer, offset, count);
			}
			return Task.FromCancellation<int>(cancellationToken);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x0009ABB8 File Offset: 0x00098DB8
		private Task<int> BeginEndReadAsync(byte[] buffer, int offset, int count)
		{
			return TaskFactory<int>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state), (Stream stream, IAsyncResult asyncResult) => stream.EndRead(asyncResult));
		}

		/// <summary>Begins an asynchronous write operation. (Consider using <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> instead; see the Remarks section.)</summary>
		/// <returns>An IAsyncResult that represents the asynchronous write, which could still be pending.</returns>
		/// <param name="buffer">The buffer to write data from. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> from which to begin writing. </param>
		/// <param name="count">The maximum number of bytes to write. </param>
		/// <param name="callback">An optional asynchronous callback, to be called when the write is complete. </param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests. </param>
		/// <exception cref="T:System.IO.IOException">Attempted an asynchronous write past the end of the stream, or a disk error occurs. </exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The current Stream implementation does not support the write operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B60 RID: 11104 RVA: 0x0009AC2A File Offset: 0x00098E2A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginWriteInternal(buffer, offset, count, callback, state, false);
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0009AC3C File Offset: 0x00098E3C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
		{
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return this.BlockingBeginWrite(buffer, offset, count, callback, state);
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(false, delegate
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				readWriteTask2._stream.Write(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				readWriteTask2.ClearBeginState();
				return 0;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x0009ACCC File Offset: 0x00098ECC
		private void RunReadWriteTaskWhenReady(Task asyncWaiter, Stream.ReadWriteTask readWriteTask)
		{
			if (asyncWaiter.IsCompleted)
			{
				this.RunReadWriteTask(readWriteTask);
				return;
			}
			asyncWaiter.ContinueWith(delegate(Task t, object state)
			{
				Tuple<Stream, Stream.ReadWriteTask> tuple = (Tuple<Stream, Stream.ReadWriteTask>)state;
				tuple.Item1.RunReadWriteTask(tuple.Item2);
			}, Tuple.Create<Stream, Stream.ReadWriteTask>(this, readWriteTask), default(CancellationToken), TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x0009AD29 File Offset: 0x00098F29
		private void RunReadWriteTask(Stream.ReadWriteTask readWriteTask)
		{
			this._activeReadWriteTask = readWriteTask;
			readWriteTask.m_taskScheduler = TaskScheduler.Default;
			readWriteTask.ScheduleAndStart(false);
		}

		/// <summary>Ends an asynchronous write operation. (Consider using <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> instead; see the Remarks section.)</summary>
		/// <param name="asyncResult">A reference to the outstanding asynchronous I/O request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A handle to the pending write operation is not available.-or-The pending operation does not support writing.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B64 RID: 11108 RVA: 0x0009AD44 File Offset: 0x00098F44
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				Stream.BlockingEndWrite(asyncResult);
				return;
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult."));
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult."));
			}
			if (activeReadWriteTask._isRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult."));
			}
			try
			{
				activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this._activeReadWriteTask = null;
				this._asyncActiveSemaphore.Release();
			}
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation. </exception>
		// Token: 0x06002B65 RID: 11109 RVA: 0x0009ADEC File Offset: 0x00098FEC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteAsync(byte[] buffer, int offset, int count)
		{
			return this.WriteAsync(buffer, offset, count, CancellationToken.None);
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
		// Token: 0x06002B66 RID: 11110 RVA: 0x0009ADFC File Offset: 0x00098FFC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndWriteAsync(buffer, offset, count);
			}
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x0009AE18 File Offset: 0x00099018
		private Task BeginEndWriteAsync(byte[] buffer, int offset, int count)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state), delegate(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			});
		}

		/// <summary>When overridden in a derived class, sets the position within the current stream.</summary>
		/// <returns>The new position within the current stream.</returns>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter. </param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B68 RID: 11112
		public abstract long Seek(long offset, SeekOrigin origin);

		/// <summary>When overridden in a derived class, sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B69 RID: 11113
		public abstract void SetLength(long value);

		/// <summary>When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream. </param>
		/// <param name="count">The maximum number of bytes to be read from the current stream. </param>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B6A RID: 11114
		public abstract int Read([In] [Out] byte[] buffer, int offset, int count);

		/// <summary>Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.</summary>
		/// <returns>The unsigned byte cast to an Int32, or -1 if at the end of the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B6B RID: 11115 RVA: 0x0009AE8C File Offset: 0x0009908C
		public virtual int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		/// <summary>When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream. </param>
		/// <param name="count">The number of bytes to be written to the current stream. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B6C RID: 11116
		public abstract void Write(byte[] buffer, int offset, int count);

		/// <summary>Writes a byte to the current position in the stream and advances the position within the stream by one byte.</summary>
		/// <param name="value">The byte to write to the stream. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing, or the stream is already closed. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002B6D RID: 11117 RVA: 0x0009AEB0 File Offset: 0x000990B0
		public virtual void WriteByte(byte value)
		{
			this.Write(new byte[] { value }, 0, 1);
		}

		/// <summary>Creates a thread-safe (synchronized) wrapper around the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <returns>A thread-safe <see cref="T:System.IO.Stream" /> object.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> object to synchronize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		// Token: 0x06002B6E RID: 11118 RVA: 0x0009AED1 File Offset: 0x000990D1
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Stream Synchronized(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (stream is Stream.SyncStream)
			{
				return stream;
			}
			return new Stream.SyncStream(stream);
		}

		/// <summary>Provides support for a <see cref="T:System.Diagnostics.Contracts.Contract" />.</summary>
		// Token: 0x06002B6F RID: 11119 RVA: 0x00002194 File Offset: 0x00000394
		[Obsolete("Do not call or override this method.")]
		protected virtual void ObjectInvariant()
		{
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0009AEF4 File Offset: 0x000990F4
		internal IAsyncResult BlockingBeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(this.Read(buffer, offset, count), state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, false);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0009AF3C File Offset: 0x0009913C
		internal static int BlockingEndRead(IAsyncResult asyncResult)
		{
			return Stream.SynchronousAsyncResult.EndRead(asyncResult);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0009AF44 File Offset: 0x00099144
		internal IAsyncResult BlockingBeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				this.Write(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, true);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0009AF8C File Offset: 0x0009918C
		internal static void BlockingEndWrite(IAsyncResult asyncResult)
		{
			Stream.SynchronousAsyncResult.EndWrite(asyncResult);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual int Read(Span<byte> destination)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual void Write(ReadOnlySpan<byte> source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual ValueTask<int> ReadAsync(Memory<byte> destination, CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual Task WriteAsync(ReadOnlyMemory<byte> source, CancellationToken cancellationToken = default(CancellationToken))
		{
			throw new NotImplementedException();
		}

		/// <summary>A Stream with no backing store.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040016B3 RID: 5811
		public static readonly Stream Null = new Stream.NullStream();

		// Token: 0x040016B4 RID: 5812
		private const int _DefaultCopyBufferSize = 81920;

		// Token: 0x040016B5 RID: 5813
		[NonSerialized]
		private Stream.ReadWriteTask _activeReadWriteTask;

		// Token: 0x040016B6 RID: 5814
		[NonSerialized]
		private SemaphoreSlim _asyncActiveSemaphore;

		// Token: 0x020003A5 RID: 933
		private struct ReadWriteParameters
		{
			// Token: 0x040016B7 RID: 5815
			internal byte[] Buffer;

			// Token: 0x040016B8 RID: 5816
			internal int Offset;

			// Token: 0x040016B9 RID: 5817
			internal int Count;
		}

		// Token: 0x020003A6 RID: 934
		private sealed class ReadWriteTask : Task<int>, ITaskCompletionAction
		{
			// Token: 0x06002B7A RID: 11130 RVA: 0x0009AFA0 File Offset: 0x000991A0
			internal void ClearBeginState()
			{
				this._stream = null;
				this._buffer = null;
			}

			// Token: 0x06002B7B RID: 11131 RVA: 0x0009AFB0 File Offset: 0x000991B0
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public ReadWriteTask(bool isRead, Func<object, int> function, object state, Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback)
				: base(function, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				this._isRead = isRead;
				this._stream = stream;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
				if (callback != null)
				{
					this._callback = callback;
					this._context = ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
					base.AddCompletionAction(this);
				}
			}

			// Token: 0x06002B7C RID: 11132 RVA: 0x0009B018 File Offset: 0x00099218
			[SecurityCritical]
			private static void InvokeAsyncCallback(object completedTask)
			{
				Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask)completedTask;
				AsyncCallback callback = readWriteTask._callback;
				readWriteTask._callback = null;
				callback(readWriteTask);
			}

			// Token: 0x06002B7D RID: 11133 RVA: 0x0009B040 File Offset: 0x00099240
			[SecuritySafeCritical]
			void ITaskCompletionAction.Invoke(Task completingTask)
			{
				ExecutionContext context = this._context;
				if (context == null)
				{
					AsyncCallback callback = this._callback;
					this._callback = null;
					callback(completingTask);
					return;
				}
				this._context = null;
				ContextCallback contextCallback = Stream.ReadWriteTask.s_invokeAsyncCallback;
				if (contextCallback == null)
				{
					contextCallback = (Stream.ReadWriteTask.s_invokeAsyncCallback = new ContextCallback(Stream.ReadWriteTask.InvokeAsyncCallback));
				}
				using (context)
				{
					ExecutionContext.Run(context, contextCallback, this, true);
				}
			}

			// Token: 0x040016BA RID: 5818
			internal readonly bool _isRead;

			// Token: 0x040016BB RID: 5819
			internal Stream _stream;

			// Token: 0x040016BC RID: 5820
			internal byte[] _buffer;

			// Token: 0x040016BD RID: 5821
			internal int _offset;

			// Token: 0x040016BE RID: 5822
			internal int _count;

			// Token: 0x040016BF RID: 5823
			private AsyncCallback _callback;

			// Token: 0x040016C0 RID: 5824
			private ExecutionContext _context;

			// Token: 0x040016C1 RID: 5825
			[SecurityCritical]
			private static ContextCallback s_invokeAsyncCallback;
		}

		// Token: 0x020003A7 RID: 935
		[Serializable]
		private sealed class NullStream : Stream
		{
			// Token: 0x06002B7E RID: 11134 RVA: 0x00095473 File Offset: 0x00093673
			internal NullStream()
			{
			}

			// Token: 0x170006E4 RID: 1764
			// (get) Token: 0x06002B7F RID: 11135 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x06002B80 RID: 11136 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170006E6 RID: 1766
			// (get) Token: 0x06002B81 RID: 11137 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170006E7 RID: 1767
			// (get) Token: 0x06002B82 RID: 11138 RVA: 0x0005BC4E File Offset: 0x00059E4E
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x170006E8 RID: 1768
			// (get) Token: 0x06002B83 RID: 11139 RVA: 0x0005BC4E File Offset: 0x00059E4E
			// (set) Token: 0x06002B84 RID: 11140 RVA: 0x00002194 File Offset: 0x00000394
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x06002B85 RID: 11141 RVA: 0x00002194 File Offset: 0x00000394
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06002B86 RID: 11142 RVA: 0x00002194 File Offset: 0x00000394
			public override void Flush()
			{
			}

			// Token: 0x06002B87 RID: 11143 RVA: 0x0009B0B8 File Offset: 0x000992B8
			[ComVisible(false)]
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCancellation(cancellationToken);
			}

			// Token: 0x06002B88 RID: 11144 RVA: 0x0009B0CF File Offset: 0x000992CF
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanRead)
				{
					__Error.ReadNotSupported();
				}
				return base.BlockingBeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x06002B89 RID: 11145 RVA: 0x0009B0EB File Offset: 0x000992EB
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				return Stream.BlockingEndRead(asyncResult);
			}

			// Token: 0x06002B8A RID: 11146 RVA: 0x0009B101 File Offset: 0x00099301
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanWrite)
				{
					__Error.WriteNotSupported();
				}
				return base.BlockingBeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06002B8B RID: 11147 RVA: 0x0009B11D File Offset: 0x0009931D
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream.BlockingEndWrite(asyncResult);
			}

			// Token: 0x06002B8C RID: 11148 RVA: 0x00015ED5 File Offset: 0x000140D5
			public override int Read([In] [Out] byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x06002B8D RID: 11149 RVA: 0x0009B134 File Offset: 0x00099334
			[ComVisible(false)]
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				Task<int> task = Stream.NullStream.s_nullReadTask;
				if (task == null)
				{
					task = (Stream.NullStream.s_nullReadTask = new Task<int>(false, 0, (TaskCreationOptions)16384, CancellationToken.None));
				}
				return task;
			}

			// Token: 0x06002B8E RID: 11150 RVA: 0x0009B163 File Offset: 0x00099363
			public override int ReadByte()
			{
				return -1;
			}

			// Token: 0x06002B8F RID: 11151 RVA: 0x00002194 File Offset: 0x00000394
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x06002B90 RID: 11152 RVA: 0x0009B166 File Offset: 0x00099366
			[ComVisible(false)]
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCancellation(cancellationToken);
			}

			// Token: 0x06002B91 RID: 11153 RVA: 0x00002194 File Offset: 0x00000394
			public override void WriteByte(byte value)
			{
			}

			// Token: 0x06002B92 RID: 11154 RVA: 0x0005BC4E File Offset: 0x00059E4E
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06002B93 RID: 11155 RVA: 0x00002194 File Offset: 0x00000394
			public override void SetLength(long length)
			{
			}

			// Token: 0x040016C2 RID: 5826
			private static Task<int> s_nullReadTask;
		}

		// Token: 0x020003A8 RID: 936
		internal sealed class SynchronousAsyncResult : IAsyncResult
		{
			// Token: 0x06002B94 RID: 11156 RVA: 0x0009B17E File Offset: 0x0009937E
			internal SynchronousAsyncResult(int bytesRead, object asyncStateObject)
			{
				this._bytesRead = bytesRead;
				this._stateObject = asyncStateObject;
			}

			// Token: 0x06002B95 RID: 11157 RVA: 0x0009B194 File Offset: 0x00099394
			internal SynchronousAsyncResult(object asyncStateObject)
			{
				this._stateObject = asyncStateObject;
				this._isWrite = true;
			}

			// Token: 0x06002B96 RID: 11158 RVA: 0x0009B1AA File Offset: 0x000993AA
			internal SynchronousAsyncResult(Exception ex, object asyncStateObject, bool isWrite)
			{
				this._exceptionInfo = ExceptionDispatchInfo.Capture(ex);
				this._stateObject = asyncStateObject;
				this._isWrite = isWrite;
			}

			// Token: 0x170006E9 RID: 1769
			// (get) Token: 0x06002B97 RID: 11159 RVA: 0x00003B29 File Offset: 0x00001D29
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170006EA RID: 1770
			// (get) Token: 0x06002B98 RID: 11160 RVA: 0x0009B1CC File Offset: 0x000993CC
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ManualResetEvent>(ref this._waitHandle, () => new ManualResetEvent(true));
				}
			}

			// Token: 0x170006EB RID: 1771
			// (get) Token: 0x06002B99 RID: 11161 RVA: 0x0009B1F8 File Offset: 0x000993F8
			public object AsyncState
			{
				get
				{
					return this._stateObject;
				}
			}

			// Token: 0x170006EC RID: 1772
			// (get) Token: 0x06002B9A RID: 11162 RVA: 0x00003B29 File Offset: 0x00001D29
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002B9B RID: 11163 RVA: 0x0009B200 File Offset: 0x00099400
			internal void ThrowIfError()
			{
				if (this._exceptionInfo != null)
				{
					this._exceptionInfo.Throw();
				}
			}

			// Token: 0x06002B9C RID: 11164 RVA: 0x0009B218 File Offset: 0x00099418
			internal static int EndRead(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || synchronousAsyncResult._isWrite)
				{
					__Error.WrongAsyncResult();
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					__Error.EndReadCalledTwice();
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
				return synchronousAsyncResult._bytesRead;
			}

			// Token: 0x06002B9D RID: 11165 RVA: 0x0009B25C File Offset: 0x0009945C
			internal static void EndWrite(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || !synchronousAsyncResult._isWrite)
				{
					__Error.WrongAsyncResult();
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					__Error.EndWriteCalledTwice();
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
			}

			// Token: 0x040016C3 RID: 5827
			private readonly object _stateObject;

			// Token: 0x040016C4 RID: 5828
			private readonly bool _isWrite;

			// Token: 0x040016C5 RID: 5829
			private ManualResetEvent _waitHandle;

			// Token: 0x040016C6 RID: 5830
			private ExceptionDispatchInfo _exceptionInfo;

			// Token: 0x040016C7 RID: 5831
			private bool _endXxxCalled;

			// Token: 0x040016C8 RID: 5832
			private int _bytesRead;
		}

		// Token: 0x020003AA RID: 938
		[Serializable]
		internal sealed class SyncStream : Stream, IDisposable
		{
			// Token: 0x06002BA1 RID: 11169 RVA: 0x0009B2AE File Offset: 0x000994AE
			internal SyncStream(Stream stream)
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				this._stream = stream;
			}

			// Token: 0x170006ED RID: 1773
			// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x0009B2CB File Offset: 0x000994CB
			public override bool CanRead
			{
				get
				{
					return this._stream.CanRead;
				}
			}

			// Token: 0x170006EE RID: 1774
			// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x0009B2D8 File Offset: 0x000994D8
			public override bool CanWrite
			{
				get
				{
					return this._stream.CanWrite;
				}
			}

			// Token: 0x170006EF RID: 1775
			// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x0009B2E5 File Offset: 0x000994E5
			public override bool CanSeek
			{
				get
				{
					return this._stream.CanSeek;
				}
			}

			// Token: 0x170006F0 RID: 1776
			// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x0009B2F2 File Offset: 0x000994F2
			[ComVisible(false)]
			public override bool CanTimeout
			{
				get
				{
					return this._stream.CanTimeout;
				}
			}

			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x0009B300 File Offset: 0x00099500
			public override long Length
			{
				get
				{
					Stream stream = this._stream;
					long length;
					lock (stream)
					{
						length = this._stream.Length;
					}
					return length;
				}
			}

			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x0009B348 File Offset: 0x00099548
			// (set) Token: 0x06002BA8 RID: 11176 RVA: 0x0009B390 File Offset: 0x00099590
			public override long Position
			{
				get
				{
					Stream stream = this._stream;
					long position;
					lock (stream)
					{
						position = this._stream.Position;
					}
					return position;
				}
				set
				{
					Stream stream = this._stream;
					lock (stream)
					{
						this._stream.Position = value;
					}
				}
			}

			// Token: 0x170006F3 RID: 1779
			// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x0009B3D8 File Offset: 0x000995D8
			// (set) Token: 0x06002BAA RID: 11178 RVA: 0x0009B3E5 File Offset: 0x000995E5
			[ComVisible(false)]
			public override int ReadTimeout
			{
				get
				{
					return this._stream.ReadTimeout;
				}
				set
				{
					this._stream.ReadTimeout = value;
				}
			}

			// Token: 0x170006F4 RID: 1780
			// (get) Token: 0x06002BAB RID: 11179 RVA: 0x0009B3F3 File Offset: 0x000995F3
			// (set) Token: 0x06002BAC RID: 11180 RVA: 0x0009B400 File Offset: 0x00099600
			[ComVisible(false)]
			public override int WriteTimeout
			{
				get
				{
					return this._stream.WriteTimeout;
				}
				set
				{
					this._stream.WriteTimeout = value;
				}
			}

			// Token: 0x06002BAD RID: 11181 RVA: 0x0009B410 File Offset: 0x00099610
			public override void Close()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						this._stream.Close();
					}
					finally
					{
						base.Dispose(true);
					}
				}
			}

			// Token: 0x06002BAE RID: 11182 RVA: 0x0009B46C File Offset: 0x0009966C
			protected override void Dispose(bool disposing)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						if (disposing)
						{
							((IDisposable)this._stream).Dispose();
						}
					}
					finally
					{
						base.Dispose(disposing);
					}
				}
			}

			// Token: 0x06002BAF RID: 11183 RVA: 0x0009B4C8 File Offset: 0x000996C8
			public override void Flush()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Flush();
				}
			}

			// Token: 0x06002BB0 RID: 11184 RVA: 0x0009B510 File Offset: 0x00099710
			public override int Read([In] [Out] byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.Read(bytes, offset, count);
				}
				return num;
			}

			// Token: 0x06002BB1 RID: 11185 RVA: 0x0009B55C File Offset: 0x0009975C
			public override int ReadByte()
			{
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.ReadByte();
				}
				return num;
			}

			// Token: 0x06002BB2 RID: 11186 RVA: 0x0009B5A4 File Offset: 0x000997A4
			private static bool OverridesBeginMethod(Stream stream, string methodName)
			{
				foreach (MethodInfo methodInfo in stream.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
				{
					if (methodInfo.DeclaringType == typeof(Stream) && methodInfo.Name == methodName)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06002BB3 RID: 11187 RVA: 0x0009B5FC File Offset: 0x000997FC
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (this._overridesBeginRead == null)
				{
					this._overridesBeginRead = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, "BeginRead"));
				}
				Stream stream = this._stream;
				IAsyncResult asyncResult;
				lock (stream)
				{
					asyncResult = (this._overridesBeginRead.Value ? this._stream.BeginRead(buffer, offset, count, callback, state) : this._stream.BeginReadInternal(buffer, offset, count, callback, state, true));
				}
				return asyncResult;
			}

			// Token: 0x06002BB4 RID: 11188 RVA: 0x0009B694 File Offset: 0x00099894
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.EndRead(asyncResult);
				}
				return num;
			}

			// Token: 0x06002BB5 RID: 11189 RVA: 0x0009B6EC File Offset: 0x000998EC
			public override long Seek(long offset, SeekOrigin origin)
			{
				Stream stream = this._stream;
				long num;
				lock (stream)
				{
					num = this._stream.Seek(offset, origin);
				}
				return num;
			}

			// Token: 0x06002BB6 RID: 11190 RVA: 0x0009B738 File Offset: 0x00099938
			public override void SetLength(long length)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.SetLength(length);
				}
			}

			// Token: 0x06002BB7 RID: 11191 RVA: 0x0009B780 File Offset: 0x00099980
			public override void Write(byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Write(bytes, offset, count);
				}
			}

			// Token: 0x06002BB8 RID: 11192 RVA: 0x0009B7C8 File Offset: 0x000999C8
			public override void WriteByte(byte b)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.WriteByte(b);
				}
			}

			// Token: 0x06002BB9 RID: 11193 RVA: 0x0009B810 File Offset: 0x00099A10
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (this._overridesBeginWrite == null)
				{
					this._overridesBeginWrite = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, "BeginWrite"));
				}
				Stream stream = this._stream;
				IAsyncResult asyncResult;
				lock (stream)
				{
					asyncResult = (this._overridesBeginWrite.Value ? this._stream.BeginWrite(buffer, offset, count, callback, state) : this._stream.BeginWriteInternal(buffer, offset, count, callback, state, true));
				}
				return asyncResult;
			}

			// Token: 0x06002BBA RID: 11194 RVA: 0x0009B8A8 File Offset: 0x00099AA8
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.EndWrite(asyncResult);
				}
			}

			// Token: 0x040016CB RID: 5835
			private Stream _stream;

			// Token: 0x040016CC RID: 5836
			[NonSerialized]
			private bool? _overridesBeginRead;

			// Token: 0x040016CD RID: 5837
			[NonSerialized]
			private bool? _overridesBeginWrite;
		}
	}
}
