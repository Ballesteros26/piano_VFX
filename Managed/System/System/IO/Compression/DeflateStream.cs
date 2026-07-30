using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	/// <summary>Provides methods and properties for compressing and decompressing streams by using the Deflate algorithm.</summary>
	// Token: 0x02000406 RID: 1030
	public class DeflateStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class by using the specified stream and compression mode.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x06001F6D RID: 8045 RVA: 0x0007AFB7 File Offset: 0x000791B7
		public DeflateStream(Stream stream, CompressionMode mode)
			: this(stream, mode, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class by using the specified stream and compression mode, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <param name="leaveOpen">true to leave the stream open after disposing the <see cref="T:System.IO.Compression.DeflateStream" /> object; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x06001F6E RID: 8046 RVA: 0x0007AFC3 File Offset: 0x000791C3
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, leaveOpen, false)
		{
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0007AFCF File Offset: 0x000791CF
		internal DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen, int windowsBits)
			: this(stream, mode, leaveOpen, true)
		{
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0007AFDC File Offset: 0x000791DC
		internal DeflateStream(Stream compressedStream, CompressionMode mode, bool leaveOpen, bool gzip)
		{
			if (compressedStream == null)
			{
				throw new ArgumentNullException("compressedStream");
			}
			if (mode != CompressionMode.Compress && mode != CompressionMode.Decompress)
			{
				throw new ArgumentException("mode");
			}
			this.base_stream = compressedStream;
			this.native = DeflateStreamNative.Create(compressedStream, mode, gzip);
			if (this.native == null)
			{
				throw new NotImplementedException("Failed to initialize zlib. You probably have an old zlib installed. Version 1.2.0.4 or later is required.");
			}
			this.mode = mode;
			this.leaveOpen = leaveOpen;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class by using the specified stream and compression level.</summary>
		/// <param name="stream">The stream to compress.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support write operations such as compression. (The <see cref="P:System.IO.Stream.CanWrite" /> property on the stream object is false.)</exception>
		// Token: 0x06001F71 RID: 8049 RVA: 0x0007B046 File Offset: 0x00079246
		public DeflateStream(Stream stream, CompressionLevel compressionLevel)
			: this(stream, compressionLevel, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class by using the specified stream and compression level, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to compress.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing the stream.</param>
		/// <param name="leaveOpen">true to leave the stream object open after disposing the <see cref="T:System.IO.Compression.DeflateStream" /> object; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support write operations such as compression. (The <see cref="P:System.IO.Stream.CanWrite" /> property on the stream object is false.)</exception>
		// Token: 0x06001F72 RID: 8050 RVA: 0x0007B052 File Offset: 0x00079252
		public DeflateStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
			: this(stream, compressionLevel, leaveOpen, false)
		{
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0007B05E File Offset: 0x0007925E
		internal DeflateStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen, int windowsBits)
			: this(stream, compressionLevel, leaveOpen, true)
		{
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0007B06A File Offset: 0x0007926A
		internal DeflateStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen, bool gzip)
			: this(stream, CompressionMode.Compress, leaveOpen, gzip)
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Compression.DeflateStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001F75 RID: 8053 RVA: 0x0007B078 File Offset: 0x00079278
		protected override void Dispose(bool disposing)
		{
			this.native.Dispose(disposing);
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (!this.leaveOpen)
				{
					Stream stream = this.base_stream;
					if (stream != null)
					{
						stream.Close();
					}
					this.base_stream = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0007B0CC File Offset: 0x000792CC
		private unsafe int ReadInternal(byte[] array, int offset, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			byte* ptr;
			if (array == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			IntPtr intPtr = new IntPtr((void*)(ptr + offset));
			return this.native.ReadZStream(intPtr, count);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x00004239 File Offset: 0x00002439
		internal ValueTask<int> ReadAsyncMemory(Memory<byte> destination, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x00004239 File Offset: 0x00002439
		internal int ReadCore(Span<byte> destination)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a number of decompressed bytes into the specified byte array.</summary>
		/// <returns>The number of bytes that were read into the byte array.</returns>
		/// <param name="array">The array to store decompressed bytes.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed.</param>
		/// <param name="count">The maximum number of decompressed bytes to read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.Compression.CompressionMode" /> value was Compress when the object was created.- or - The underlying stream does not support reading.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than zero.-or-<paramref name="array" /> length minus the index starting point is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The data is in an invalid format.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06001F79 RID: 8057 RVA: 0x0007B110 File Offset: 0x00079310
		public override int Read(byte[] array, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (array == null)
			{
				throw new ArgumentNullException("Destination array is null.");
			}
			if (!this.CanRead)
			{
				throw new InvalidOperationException("Stream does not support reading.");
			}
			int num = array.Length;
			if (offset < 0 || count < 0)
			{
				throw new ArgumentException("Dest or count is negative.");
			}
			if (offset > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (offset + count > num)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			return this.ReadInternal(array, offset, count);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0007B198 File Offset: 0x00079398
		private unsafe void WriteInternal(byte[] array, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				IntPtr intPtr = new IntPtr((void*)(ptr + offset));
				this.native.WriteZStream(intPtr, count);
			}
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00004239 File Offset: 0x00002439
		internal Task WriteAsyncMemory(ReadOnlyMemory<byte> source, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00004239 File Offset: 0x00002439
		internal void WriteCore(ReadOnlySpan<byte> source)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes compressed bytes to the underlying stream from the specified byte array.</summary>
		/// <param name="array">The buffer that contains the data to compress.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> from which the bytes will be read.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		// Token: 0x06001F7D RID: 8061 RVA: 0x0007B1DC File Offset: 0x000793DC
		public override void Write(byte[] array, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (offset > array.Length - count)
			{
				throw new ArgumentException("Buffer too small. count/offset wrong.");
			}
			this.WriteInternal(array, offset, count);
		}

		/// <summary>The current implementation of this method has no functionality.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001F7E RID: 8062 RVA: 0x0007B25D File Offset: 0x0007945D
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.CanWrite)
			{
				this.native.Flush();
			}
		}

		/// <summary>Begins an asynchronous read operation. (Consider using the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <returns>An  object that represents the asynchronous read operation, which could still be pending.</returns>
		/// <param name="array">The byte array to read the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin reading data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="asyncCallback">An optional asynchronous callback, to be called when the read operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <exception cref="T:System.IO.IOException">The method tried to read asynchronously past the end of the stream, or a disk error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.DeflateStream" /> implementation does not support the read operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">This call cannot be completed. </exception>
		// Token: 0x06001F7F RID: 8063 RVA: 0x0007B28C File Offset: 0x0007948C
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (count + offset > array.Length)
			{
				throw new ArgumentException("Buffer too small. count/offset wrong.");
			}
			return new DeflateStream.ReadMethod(this.ReadInternal).BeginInvoke(array, offset, count, asyncCallback, asyncState);
		}

		/// <summary>Begins an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <returns>An  object that represents the asynchronous write operation, which could still be pending.</returns>
		/// <param name="array">The buffer to write data from.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> to begin writing from.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="asyncCallback">An optional asynchronous callback, to be called when the write operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <exception cref="T:System.IO.IOException">The method tried to write asynchronously past the end of the stream, or a disk error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.DeflateStream" /> implementation does not support the write operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">The write operation cannot be performed because the stream is closed.</exception>
		// Token: 0x06001F80 RID: 8064 RVA: 0x0007B328 File Offset: 0x00079528
		public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanWrite)
			{
				throw new InvalidOperationException("This stream does not support writing");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (count + offset > array.Length)
			{
				throw new ArgumentException("Buffer too small. count/offset wrong.");
			}
			return new DeflateStream.WriteMethod(this.WriteInternal).BeginInvoke(array, offset, count, asyncCallback, asyncState);
		}

		/// <summary>Waits for the pending asynchronous read to complete. (Consider using the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <returns>The number of bytes read from the stream, between 0 (zero) and the number of bytes you requested. <see cref="T:System.IO.Compression.DeflateStream" /> returns 0 only at the end of the stream; otherwise, it blocks until at least one byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.DeflateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.SystemException">An exception was thrown during a call to <see cref="M:System.Threading.WaitHandle.WaitOne" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The end call is invalid because asynchronous read operations for this stream are not yet complete.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is null.</exception>
		// Token: 0x06001F81 RID: 8065 RVA: 0x0007B3C4 File Offset: 0x000795C4
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			DeflateStream.ReadMethod readMethod = asyncResult2.AsyncDelegate as DeflateStream.ReadMethod;
			if (readMethod == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			return readMethod.EndInvoke(asyncResult);
		}

		/// <summary>Ends an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <param name="asyncResult">A reference to the outstanding asynchronous I/O request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.DeflateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.Exception">An exception was thrown during a call to <see cref="M:System.Threading.WaitHandle.WaitOne" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The end write call is invalid.</exception>
		// Token: 0x06001F82 RID: 8066 RVA: 0x0007B41C File Offset: 0x0007961C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			DeflateStream.WriteMethod writeMethod = asyncResult2.AsyncDelegate as DeflateStream.WriteMethod;
			if (writeMethod == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			writeMethod.EndInvoke(asyncResult);
		}

		/// <summary>This operation is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <param name="offset">The location in the stream.</param>
		/// <param name="origin">One of the <see cref="T:System.IO.SeekOrigin" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x06001F83 RID: 8067 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		/// <summary>This operation is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x06001F84 RID: 8068 RVA: 0x000074E4 File Offset: 0x000056E4
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a reference to the underlying stream.</summary>
		/// <returns>A stream object that represents the underlying stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream is closed.</exception>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x0007B473 File Offset: 0x00079673
		public Stream BaseStream
		{
			get
			{
				return this.base_stream;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports reading while decompressing a file.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Decompress, and the underlying stream is opened and supports reading; otherwise, false.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0007B47B File Offset: 0x0007967B
		public override bool CanRead
		{
			get
			{
				return !this.disposed && this.mode == CompressionMode.Decompress && this.base_stream.CanRead;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports seeking.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports writing.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Compress, and the underlying stream supports writing and is not closed; otherwise, false.</returns>
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0007B49A File Offset: 0x0007969A
		public override bool CanWrite
		{
			get
			{
				return !this.disposed && this.mode == CompressionMode.Compress && this.base_stream.CanWrite;
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x000074E4 File Offset: 0x000056E4
		// (set) Token: 0x06001F8B RID: 8075 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04001B79 RID: 7033
		private Stream base_stream;

		// Token: 0x04001B7A RID: 7034
		private CompressionMode mode;

		// Token: 0x04001B7B RID: 7035
		private bool leaveOpen;

		// Token: 0x04001B7C RID: 7036
		private bool disposed;

		// Token: 0x04001B7D RID: 7037
		private DeflateStreamNative native;

		// Token: 0x02000407 RID: 1031
		// (Invoke) Token: 0x06001F8D RID: 8077
		private delegate int ReadMethod(byte[] array, int offset, int count);

		// Token: 0x02000408 RID: 1032
		// (Invoke) Token: 0x06001F91 RID: 8081
		private delegate void WriteMethod(byte[] array, int offset, int count);
	}
}
