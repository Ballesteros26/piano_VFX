using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Security.Cryptography
{
	/// <summary>Defines a stream that links data streams to cryptographic transformations.</summary>
	// Token: 0x02000653 RID: 1619
	[ComVisible(true)]
	public class CryptoStream : Stream, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptoStream" /> class with a target data stream, the transformation to use, and the mode of the stream.</summary>
		/// <param name="stream">The stream on which to perform the cryptographic transformation. </param>
		/// <param name="transform">The cryptographic transformation that is to be performed on the stream. </param>
		/// <param name="mode">One of the <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not readable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is invalid.</exception>
		// Token: 0x060045F1 RID: 17905 RVA: 0x000F5508 File Offset: 0x000F3708
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
		{
			this._stream = stream;
			this._transformMode = mode;
			this._Transform = transform;
			CryptoStreamMode transformMode = this._transformMode;
			if (transformMode != CryptoStreamMode.Read)
			{
				if (transformMode != CryptoStreamMode.Write)
				{
					throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
				}
				if (!this._stream.CanWrite)
				{
					throw new ArgumentException(Environment.GetResourceString("Stream was not writable."), "stream");
				}
				this._canWrite = true;
			}
			else
			{
				if (!this._stream.CanRead)
				{
					throw new ArgumentException(Environment.GetResourceString("Stream was not readable."), "stream");
				}
				this._canRead = true;
			}
			this.InitializeBuffer();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.CryptoStream" /> is readable.</summary>
		/// <returns>true if the current stream is readable; otherwise, false.</returns>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x000F55AC File Offset: 0x000F37AC
		public override bool CanRead
		{
			get
			{
				return this._canRead;
			}
		}

		/// <summary>Gets a value indicating whether you can seek within the current <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.CryptoStream" /> is writable.</summary>
		/// <returns>true if the current stream is writable; otherwise, false.</returns>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x000F55B4 File Offset: 0x000F37B4
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		/// <summary>Gets the length in bytes of the stream.</summary>
		/// <returns>This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x00094062 File Offset: 0x00092262
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		/// <summary>Gets or sets the position within the current stream.</summary>
		/// <returns>This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x00094062 File Offset: 0x00092262
		// (set) Token: 0x060045F7 RID: 17911 RVA: 0x00094062 File Offset: 0x00092262
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		/// <summary>Gets a value indicating whether the final buffer block has been written to the underlying stream. </summary>
		/// <returns>true if the final block has been flushed; otherwise, false. </returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x000F55BC File Offset: 0x000F37BC
		public bool HasFlushedFinalBlock
		{
			get
			{
				return this._finalBlockTransformed;
			}
		}

		/// <summary>Updates the underlying data source or repository with the current state of the buffer, then clears the buffer.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key is corrupt which can cause invalid padding to the stream. </exception>
		/// <exception cref="T:System.NotSupportedException">The current stream is not writable.-or- The final block has already been transformed. </exception>
		// Token: 0x060045F9 RID: 17913 RVA: 0x000F55C4 File Offset: 0x000F37C4
		public void FlushFinalBlock()
		{
			if (this._finalBlockTransformed)
			{
				throw new NotSupportedException(Environment.GetResourceString("FlushFinalBlock() method was called twice on a CryptoStream. It can only be called once."));
			}
			byte[] array = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
			this._finalBlockTransformed = true;
			if (this._canWrite && this._OutputBufferIndex > 0)
			{
				this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
				this._OutputBufferIndex = 0;
			}
			if (this._canWrite)
			{
				this._stream.Write(array, 0, array.Length);
			}
			CryptoStream cryptoStream = this._stream as CryptoStream;
			if (cryptoStream != null)
			{
				if (!cryptoStream.HasFlushedFinalBlock)
				{
					cryptoStream.FlushFinalBlock();
				}
			}
			else
			{
				this._stream.Flush();
			}
			if (this._InputBuffer != null)
			{
				Array.Clear(this._InputBuffer, 0, this._InputBuffer.Length);
			}
			if (this._OutputBuffer != null)
			{
				Array.Clear(this._OutputBuffer, 0, this._OutputBuffer.Length);
			}
		}

		/// <summary>Clears all buffers for the current stream and causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x060045FA RID: 17914 RVA: 0x00002194 File Offset: 0x00000394
		public override void Flush()
		{
		}

		/// <summary>Clears all buffers for the current stream asynchronously, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x060045FB RID: 17915 RVA: 0x000F56B3 File Offset: 0x000F38B3
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.FlushAsync(cancellationToken);
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCancellation(cancellationToken);
		}

		/// <summary>Sets the position within the current stream.</summary>
		/// <returns>This method is not supported.</returns>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter. </param>
		/// <param name="origin">A <see cref="T:System.IO.SeekOrigin" /> object indicating the reference point used to obtain the new position. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060045FC RID: 17916 RVA: 0x00094062 File Offset: 0x00092262
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		/// <summary>Sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes. </param>
		/// <exception cref="T:System.NotSupportedException">This property exists only to support inheritance from <see cref="T:System.IO.Stream" />, and cannot be used.</exception>
		// Token: 0x060045FD RID: 17917 RVA: 0x00094062 File Offset: 0x00092262
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		/// <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero if the end of the stream has been reached.</returns>
		/// <param name="buffer">An array of bytes. A maximum of <paramref name="count" /> bytes are read from the current stream and stored in <paramref name="buffer" />. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream. </param>
		/// <param name="count">The maximum number of bytes to be read from the current stream. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> associated with current <see cref="T:System.Security.Cryptography.CryptoStream" /> object does not match the underlying stream.  For example, this exception is thrown when using <see cref="F:System.Security.Cryptography.CryptoStreamMode.Read" /> with an underlying stream that is write only.  </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.-or- The <paramref name="count" /> parameter is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">Thesum of the <paramref name="count" /> and <paramref name="offset" /> parameters is longer than the length of the buffer. </exception>
		// Token: 0x060045FE RID: 17918 RVA: 0x000F56EC File Offset: 0x000F38EC
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
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
			int i = count;
			int num = offset;
			if (this._OutputBufferIndex != 0)
			{
				if (this._OutputBufferIndex > count)
				{
					Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, count);
					Buffer.InternalBlockCopy(this._OutputBuffer, count, this._OutputBuffer, 0, this._OutputBufferIndex - count);
					this._OutputBufferIndex -= count;
					return count;
				}
				Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, this._OutputBufferIndex);
				i -= this._OutputBufferIndex;
				num += this._OutputBufferIndex;
				this._OutputBufferIndex = 0;
			}
			if (this._finalBlockTransformed)
			{
				return count - i;
			}
			if (i > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
			{
				int num2 = i / this._OutputBlockSize * this._InputBlockSize;
				byte[] array = new byte[num2];
				Buffer.InternalBlockCopy(this._InputBuffer, 0, array, 0, this._InputBufferIndex);
				int num3 = this._InputBufferIndex;
				num3 += this._stream.Read(array, this._InputBufferIndex, num2 - this._InputBufferIndex);
				this._InputBufferIndex = 0;
				if (num3 <= this._InputBlockSize)
				{
					this._InputBuffer = array;
					this._InputBufferIndex = num3;
				}
				else
				{
					int num4 = num3 / this._InputBlockSize * this._InputBlockSize;
					int num5 = num3 - num4;
					if (num5 != 0)
					{
						this._InputBufferIndex = num5;
						Buffer.InternalBlockCopy(array, num4, this._InputBuffer, 0, num5);
					}
					byte[] array2 = new byte[num4 / this._InputBlockSize * this._OutputBlockSize];
					int num6 = this._Transform.TransformBlock(array, 0, num4, array2, 0);
					Buffer.InternalBlockCopy(array2, 0, buffer, num, num6);
					Array.Clear(array, 0, array.Length);
					Array.Clear(array2, 0, array2.Length);
					i -= num6;
					num += num6;
				}
			}
			while (i > 0)
			{
				while (this._InputBufferIndex < this._InputBlockSize)
				{
					int num3 = this._stream.Read(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
					if (num3 != 0)
					{
						this._InputBufferIndex += num3;
					}
					else
					{
						byte[] array3 = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
						this._OutputBuffer = array3;
						this._OutputBufferIndex = array3.Length;
						this._finalBlockTransformed = true;
						if (i < this._OutputBufferIndex)
						{
							Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, i);
							this._OutputBufferIndex -= i;
							Buffer.InternalBlockCopy(this._OutputBuffer, i, this._OutputBuffer, 0, this._OutputBufferIndex);
							return count;
						}
						Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, this._OutputBufferIndex);
						i -= this._OutputBufferIndex;
						this._OutputBufferIndex = 0;
						return count - i;
					}
				}
				int num6 = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
				this._InputBufferIndex = 0;
				if (i < num6)
				{
					Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, i);
					this._OutputBufferIndex = num6 - i;
					Buffer.InternalBlockCopy(this._OutputBuffer, i, this._OutputBuffer, 0, this._OutputBufferIndex);
					return count;
				}
				Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, num6);
				num += num6;
				i -= num6;
			}
			return count;
		}

		/// <summary>Reads a sequence of bytes from the current stream asynchronously, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the task object's <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. </returns>
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
		// Token: 0x060045FF RID: 17919 RVA: 0x000F5A7C File Offset: 0x000F3C7C
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
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
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.ReadAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<int>(cancellationToken);
			}
			return this.ReadAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000F5B2C File Offset: 0x000F3D2C
		private async Task<int> ReadAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			await default(CryptoStream.HopToThreadPoolAwaitable);
			SemaphoreSlim sem = base.EnsureAsyncActiveSemaphoreInitialized();
			await sem.WaitAsync().ConfigureAwait(false);
			int num;
			try
			{
				int bytesToDeliver = count;
				int currentOutputIndex = offset;
				if (this._OutputBufferIndex != 0)
				{
					if (this._OutputBufferIndex > count)
					{
						Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, count);
						Buffer.InternalBlockCopy(this._OutputBuffer, count, this._OutputBuffer, 0, this._OutputBufferIndex - count);
						this._OutputBufferIndex -= count;
						return count;
					}
					Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, this._OutputBufferIndex);
					bytesToDeliver -= this._OutputBufferIndex;
					currentOutputIndex += this._OutputBufferIndex;
					this._OutputBufferIndex = 0;
				}
				if (this._finalBlockTransformed)
				{
					num = count - bytesToDeliver;
				}
				else
				{
					if (bytesToDeliver > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
					{
						int num2 = bytesToDeliver / this._OutputBlockSize * this._InputBlockSize;
						byte[] tempInputBuffer = new byte[num2];
						Buffer.InternalBlockCopy(this._InputBuffer, 0, tempInputBuffer, 0, this._InputBufferIndex);
						int inputBufferIndex = this._InputBufferIndex;
						int num3 = inputBufferIndex + await this._stream.ReadAsync(tempInputBuffer, this._InputBufferIndex, num2 - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
						this._InputBufferIndex = 0;
						if (num3 <= this._InputBlockSize)
						{
							this._InputBuffer = tempInputBuffer;
							this._InputBufferIndex = num3;
						}
						else
						{
							int num4 = num3 / this._InputBlockSize * this._InputBlockSize;
							int num5 = num3 - num4;
							if (num5 != 0)
							{
								this._InputBufferIndex = num5;
								Buffer.InternalBlockCopy(tempInputBuffer, num4, this._InputBuffer, 0, num5);
							}
							byte[] array = new byte[num4 / this._InputBlockSize * this._OutputBlockSize];
							int num6 = this._Transform.TransformBlock(tempInputBuffer, 0, num4, array, 0);
							Buffer.InternalBlockCopy(array, 0, buffer, currentOutputIndex, num6);
							Array.Clear(tempInputBuffer, 0, tempInputBuffer.Length);
							Array.Clear(array, 0, array.Length);
							bytesToDeliver -= num6;
							currentOutputIndex += num6;
							tempInputBuffer = null;
						}
					}
					while (bytesToDeliver > 0)
					{
						while (this._InputBufferIndex < this._InputBlockSize)
						{
							int num3 = await this._stream.ReadAsync(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
							if (num3 != 0)
							{
								this._InputBufferIndex += num3;
							}
							else
							{
								byte[] array2 = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
								this._OutputBuffer = array2;
								this._OutputBufferIndex = array2.Length;
								this._finalBlockTransformed = true;
								if (bytesToDeliver < this._OutputBufferIndex)
								{
									Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
									this._OutputBufferIndex -= bytesToDeliver;
									Buffer.InternalBlockCopy(this._OutputBuffer, bytesToDeliver, this._OutputBuffer, 0, this._OutputBufferIndex);
									return count;
								}
								Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, this._OutputBufferIndex);
								bytesToDeliver -= this._OutputBufferIndex;
								this._OutputBufferIndex = 0;
								return count - bytesToDeliver;
							}
						}
						int num6 = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
						this._InputBufferIndex = 0;
						if (bytesToDeliver < num6)
						{
							Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
							this._OutputBufferIndex = num6 - bytesToDeliver;
							Buffer.InternalBlockCopy(this._OutputBuffer, bytesToDeliver, this._OutputBuffer, 0, this._OutputBufferIndex);
							return count;
						}
						Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, num6);
						currentOutputIndex += num6;
						bytesToDeliver -= num6;
					}
					num = count;
				}
			}
			finally
			{
				sem.Release();
			}
			return num;
		}

		/// <summary>Writes a sequence of bytes to the current <see cref="T:System.Security.Cryptography.CryptoStream" /> and advances the current position within the stream by the number of bytes written.</summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream. </param>
		/// <param name="count">The number of bytes to be written to the current stream. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> associated with current <see cref="T:System.Security.Cryptography.CryptoStream" /> object does not match the underlying stream.  For example, this exception is thrown when using <see cref="F:System.Security.Cryptography.CryptoStreamMode.Write" />  with an underlying stream that is read only.  </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.-or- The <paramref name="count" /> parameter is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">The sum of the <paramref name="count" /> and <paramref name="offset" /> parameters is longer than the length of the buffer. </exception>
		// Token: 0x06004601 RID: 17921 RVA: 0x000F5B94 File Offset: 0x000F3D94
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support writing."));
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
			int i = count;
			int num = offset;
			if (this._InputBufferIndex > 0)
			{
				if (count < this._InputBlockSize - this._InputBufferIndex)
				{
					Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, count);
					this._InputBufferIndex += count;
					return;
				}
				Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
				num += this._InputBlockSize - this._InputBufferIndex;
				i -= this._InputBlockSize - this._InputBufferIndex;
				this._InputBufferIndex = this._InputBlockSize;
			}
			if (this._OutputBufferIndex > 0)
			{
				this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
				this._OutputBufferIndex = 0;
			}
			if (this._InputBufferIndex == this._InputBlockSize)
			{
				int num2 = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
				this._stream.Write(this._OutputBuffer, 0, num2);
				this._InputBufferIndex = 0;
			}
			while (i > 0)
			{
				if (i < this._InputBlockSize)
				{
					Buffer.InternalBlockCopy(buffer, num, this._InputBuffer, 0, i);
					this._InputBufferIndex += i;
					return;
				}
				if (this._Transform.CanTransformMultipleBlocks)
				{
					int num3 = i / this._InputBlockSize;
					int num4 = num3 * this._InputBlockSize;
					byte[] array = new byte[num3 * this._OutputBlockSize];
					int num2 = this._Transform.TransformBlock(buffer, num, num4, array, 0);
					this._stream.Write(array, 0, num2);
					num += num4;
					i -= num4;
				}
				else
				{
					int num2 = this._Transform.TransformBlock(buffer, num, this._InputBlockSize, this._OutputBuffer, 0);
					this._stream.Write(this._OutputBuffer, 0, num2);
					num += this._InputBlockSize;
					i -= this._InputBlockSize;
				}
			}
		}

		/// <summary>Writes a sequence of bytes to the current stream asynchronously, advances the current position within the stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin writing bytes to the stream.</param>
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
		// Token: 0x06004602 RID: 17922 RVA: 0x000F5DDC File Offset: 0x000F3FDC
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support writing."));
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
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.WriteAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			return this.WriteAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x000F5E8C File Offset: 0x000F408C
		private async Task WriteAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			await default(CryptoStream.HopToThreadPoolAwaitable);
			SemaphoreSlim sem = base.EnsureAsyncActiveSemaphoreInitialized();
			await sem.WaitAsync().ConfigureAwait(false);
			try
			{
				int bytesToWrite = count;
				int currentInputIndex = offset;
				if (this._InputBufferIndex > 0)
				{
					if (count < this._InputBlockSize - this._InputBufferIndex)
					{
						Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, count);
						this._InputBufferIndex += count;
						return;
					}
					Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
					currentInputIndex += this._InputBlockSize - this._InputBufferIndex;
					bytesToWrite -= this._InputBlockSize - this._InputBufferIndex;
					this._InputBufferIndex = this._InputBlockSize;
				}
				if (this._OutputBufferIndex > 0)
				{
					await this._stream.WriteAsync(this._OutputBuffer, 0, this._OutputBufferIndex, cancellationToken).ConfigureAwait(false);
					this._OutputBufferIndex = 0;
				}
				if (this._InputBufferIndex == this._InputBlockSize)
				{
					int num = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
					await this._stream.WriteAsync(this._OutputBuffer, 0, num, cancellationToken).ConfigureAwait(false);
					this._InputBufferIndex = 0;
				}
				while (bytesToWrite > 0)
				{
					if (bytesToWrite < this._InputBlockSize)
					{
						Buffer.InternalBlockCopy(buffer, currentInputIndex, this._InputBuffer, 0, bytesToWrite);
						this._InputBufferIndex += bytesToWrite;
						break;
					}
					if (this._Transform.CanTransformMultipleBlocks)
					{
						int num2 = bytesToWrite / this._InputBlockSize;
						int numWholeBlocksInBytes = num2 * this._InputBlockSize;
						byte[] array = new byte[num2 * this._OutputBlockSize];
						int num = this._Transform.TransformBlock(buffer, currentInputIndex, numWholeBlocksInBytes, array, 0);
						await this._stream.WriteAsync(array, 0, num, cancellationToken).ConfigureAwait(false);
						currentInputIndex += numWholeBlocksInBytes;
						bytesToWrite -= numWholeBlocksInBytes;
					}
					else
					{
						int num = this._Transform.TransformBlock(buffer, currentInputIndex, this._InputBlockSize, this._OutputBuffer, 0);
						await this._stream.WriteAsync(this._OutputBuffer, 0, num, cancellationToken).ConfigureAwait(false);
						currentInputIndex += this._InputBlockSize;
						bytesToWrite -= this._InputBlockSize;
					}
				}
			}
			finally
			{
				sem.Release();
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
		// Token: 0x06004604 RID: 17924 RVA: 0x0009A9F1 File Offset: 0x00098BF1
		public void Clear()
		{
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.CryptoStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06004605 RID: 17925 RVA: 0x000F5EF4 File Offset: 0x000F40F4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (!this._finalBlockTransformed)
					{
						this.FlushFinalBlock();
					}
					this._stream.Close();
				}
			}
			finally
			{
				try
				{
					this._finalBlockTransformed = true;
					if (this._InputBuffer != null)
					{
						Array.Clear(this._InputBuffer, 0, this._InputBuffer.Length);
					}
					if (this._OutputBuffer != null)
					{
						Array.Clear(this._OutputBuffer, 0, this._OutputBuffer.Length);
					}
					this._InputBuffer = null;
					this._OutputBuffer = null;
					this._canRead = false;
					this._canWrite = false;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x000F5FA4 File Offset: 0x000F41A4
		private void InitializeBuffer()
		{
			if (this._Transform != null)
			{
				this._InputBlockSize = this._Transform.InputBlockSize;
				this._InputBuffer = new byte[this._InputBlockSize];
				this._OutputBlockSize = this._Transform.OutputBlockSize;
				this._OutputBuffer = new byte[this._OutputBlockSize];
			}
		}

		// Token: 0x040023FD RID: 9213
		private Stream _stream;

		// Token: 0x040023FE RID: 9214
		private ICryptoTransform _Transform;

		// Token: 0x040023FF RID: 9215
		private byte[] _InputBuffer;

		// Token: 0x04002400 RID: 9216
		private int _InputBufferIndex;

		// Token: 0x04002401 RID: 9217
		private int _InputBlockSize;

		// Token: 0x04002402 RID: 9218
		private byte[] _OutputBuffer;

		// Token: 0x04002403 RID: 9219
		private int _OutputBufferIndex;

		// Token: 0x04002404 RID: 9220
		private int _OutputBlockSize;

		// Token: 0x04002405 RID: 9221
		private CryptoStreamMode _transformMode;

		// Token: 0x04002406 RID: 9222
		private bool _canRead;

		// Token: 0x04002407 RID: 9223
		private bool _canWrite;

		// Token: 0x04002408 RID: 9224
		private bool _finalBlockTransformed;

		// Token: 0x02000654 RID: 1620
		private struct HopToThreadPoolAwaitable : INotifyCompletion
		{
			// Token: 0x06004607 RID: 17927 RVA: 0x000F5FFD File Offset: 0x000F41FD
			public CryptoStream.HopToThreadPoolAwaitable GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000BD9 RID: 3033
			// (get) Token: 0x06004608 RID: 17928 RVA: 0x00015ED5 File Offset: 0x000140D5
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004609 RID: 17929 RVA: 0x000F6005 File Offset: 0x000F4205
			public void OnCompleted(Action continuation)
			{
				Task.Run(continuation);
			}

			// Token: 0x0600460A RID: 17930 RVA: 0x00002194 File Offset: 0x00000394
			public void GetResult()
			{
			}
		}
	}
}
