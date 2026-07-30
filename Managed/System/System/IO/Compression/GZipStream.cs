using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	/// <summary>Provides methods and properties used to compress and decompress streams.</summary>
	// Token: 0x02000405 RID: 1029
	public class GZipStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression mode.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> enumeration value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x06001F4C RID: 8012 RVA: 0x0007ACF6 File Offset: 0x00078EF6
		public GZipStream(Stream stream, CompressionMode mode)
			: this(stream, mode, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression mode, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <param name="leaveOpen">true to leave the stream open after disposing the <see cref="T:System.IO.Compression.GZipStream" /> object; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x06001F4D RID: 8013 RVA: 0x0007AD01 File Offset: 0x00078F01
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
		{
			this._deflateStream = new DeflateStream(stream, mode, leaveOpen, 31);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression level.</summary>
		/// <param name="stream">The stream to compress.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support write operations such as compression. (The <see cref="P:System.IO.Stream.CanWrite" /> property on the stream object is false.)</exception>
		// Token: 0x06001F4E RID: 8014 RVA: 0x0007AD19 File Offset: 0x00078F19
		public GZipStream(Stream stream, CompressionLevel compressionLevel)
			: this(stream, compressionLevel, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression level, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to compress.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing the stream.</param>
		/// <param name="leaveOpen">true to leave the stream object open after disposing the <see cref="T:System.IO.Compression.GZipStream" /> object; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support write operations such as compression. (The <see cref="P:System.IO.Stream.CanWrite" /> property on the stream object is false.)</exception>
		// Token: 0x06001F4F RID: 8015 RVA: 0x0007AD24 File Offset: 0x00078F24
		public GZipStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
		{
			this._deflateStream = new DeflateStream(stream, compressionLevel, leaveOpen, 31);
		}

		/// <summary>Gets a value indicating whether the stream supports reading while decompressing a file.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Decompress, and the underlying stream supports reading and is not closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x0007AD3C File Offset: 0x00078F3C
		public override bool CanRead
		{
			get
			{
				DeflateStream deflateStream = this._deflateStream;
				return deflateStream != null && deflateStream.CanRead;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports writing.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Compress, and the underlying stream supports writing and is not closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0007AD4F File Offset: 0x00078F4F
		public override bool CanWrite
		{
			get
			{
				DeflateStream deflateStream = this._deflateStream;
				return deflateStream != null && deflateStream.CanWrite;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports seeking.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x0007AD62 File Offset: 0x00078F62
		public override bool CanSeek
		{
			get
			{
				DeflateStream deflateStream = this._deflateStream;
				return deflateStream != null && deflateStream.CanSeek;
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0007AD75 File Offset: 0x00078F75
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x0007AD75 File Offset: 0x00078F75
		// (set) Token: 0x06001F55 RID: 8021 RVA: 0x0007AD75 File Offset: 0x00078F75
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
			set
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		/// <summary>The current implementation of this method has no functionality.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001F56 RID: 8022 RVA: 0x0007AD81 File Offset: 0x00078F81
		public override void Flush()
		{
			this.CheckDeflateStream();
			this._deflateStream.Flush();
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <param name="offset">The location in the stream.</param>
		/// <param name="origin">One of the <see cref="T:System.IO.SeekOrigin" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x06001F57 RID: 8023 RVA: 0x0007AD75 File Offset: 0x00078F75
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x06001F58 RID: 8024 RVA: 0x0007AD75 File Offset: 0x00078F75
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0007AD94 File Offset: 0x00078F94
		public override int ReadByte()
		{
			this.CheckDeflateStream();
			return this._deflateStream.ReadByte();
		}

		/// <summary>Begins an asynchronous read operation. (Consider using the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <returns>An object that represents the asynchronous read operation, which could still be pending.</returns>
		/// <param name="array">The byte array to read the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin reading data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="asyncCallback">An optional asynchronous callback, to be called when the read operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <exception cref="T:System.IO.IOException">The method tried to  read asynchronously past the end of the stream, or a disk error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.GZipStream" /> implementation does not support the read operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">A read operation cannot be performed because the stream is closed.</exception>
		// Token: 0x06001F5A RID: 8026 RVA: 0x0007ADA7 File Offset: 0x00078FA7
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ReadAsync(array, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		/// <summary>Waits for the pending asynchronous read to complete. (Consider using the the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <returns>The number of bytes read from the stream, between 0 (zero) and the number of bytes you requested. <see cref="T:System.IO.Compression.GZipStream" /> returns 0 only at the end of the stream; otherwise, it blocks until at least one byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.DeflateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The end operation cannot be performed because the stream is closed.</exception>
		// Token: 0x06001F5B RID: 8027 RVA: 0x00006C26 File Offset: 0x00004E26
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		/// <summary>Reads a number of decompressed bytes into the specified byte array.</summary>
		/// <returns>The number of bytes that were decompressed into the byte array. If the end of the stream has been reached, zero or the number of bytes read is returned.</returns>
		/// <param name="array">The array used to store decompressed bytes.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed.</param>
		/// <param name="count">The maximum number of decompressed bytes to read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.Compression.CompressionMode" /> value was Compress when the object was created.- or -The underlying stream does not support reading.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than zero.-or-<paramref name="array" /> length minus the index starting point is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The data is in an invalid format.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x06001F5C RID: 8028 RVA: 0x0007ADC0 File Offset: 0x00078FC0
		public override int Read(byte[] array, int offset, int count)
		{
			this.CheckDeflateStream();
			return this._deflateStream.Read(array, offset, count);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0007ADD6 File Offset: 0x00078FD6
		public override int Read(Span<byte> destination)
		{
			if (base.GetType() != typeof(GZipStream))
			{
				return base.Read(destination);
			}
			this.CheckDeflateStream();
			return this._deflateStream.ReadCore(destination);
		}

		/// <summary>Begins an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <returns>An  object that represents the asynchronous write operation, which could still be pending.</returns>
		/// <param name="array">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="asyncCallback">An optional asynchronous callback to be called when the write operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying stream is null. -or-The underlying stream is closed.</exception>
		// Token: 0x06001F5E RID: 8030 RVA: 0x0007AE09 File Offset: 0x00079009
		public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.WriteAsync(array, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		/// <summary>Handles the end of an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <param name="asyncResult">The object that represents the asynchronous call.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying stream is null. -or-The underlying stream is closed.</exception>
		// Token: 0x06001F5F RID: 8031 RVA: 0x00006AA6 File Offset: 0x00004CA6
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		/// <summary>Writes compressed bytes to the underlying stream from the specified byte array.</summary>
		/// <param name="array">The buffer that contains the data to compress.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> from which the bytes will be read.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The write operation cannot be performed because the stream is closed.</exception>
		// Token: 0x06001F60 RID: 8032 RVA: 0x0007AE22 File Offset: 0x00079022
		public override void Write(byte[] array, int offset, int count)
		{
			this.CheckDeflateStream();
			this._deflateStream.Write(array, offset, count);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0007AE38 File Offset: 0x00079038
		public override void Write(ReadOnlySpan<byte> source)
		{
			if (base.GetType() != typeof(GZipStream))
			{
				base.Write(source);
				return;
			}
			this.CheckDeflateStream();
			this._deflateStream.WriteCore(source);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0007AE6B File Offset: 0x0007906B
		public override void CopyTo(Stream destination, int bufferSize)
		{
			this.CheckDeflateStream();
			this._deflateStream.CopyTo(destination, bufferSize);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Compression.GZipStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001F63 RID: 8035 RVA: 0x0007AE80 File Offset: 0x00079080
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._deflateStream != null)
				{
					this._deflateStream.Dispose();
				}
				this._deflateStream = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Gets a reference to the underlying stream.</summary>
		/// <returns>A stream object that represents the underlying stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x0007AEC4 File Offset: 0x000790C4
		public Stream BaseStream
		{
			get
			{
				DeflateStream deflateStream = this._deflateStream;
				if (deflateStream == null)
				{
					return null;
				}
				return deflateStream.BaseStream;
			}
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0007AED7 File Offset: 0x000790D7
		public override Task<int> ReadAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckDeflateStream();
			return this._deflateStream.ReadAsync(array, offset, count, cancellationToken);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0007AEEF File Offset: 0x000790EF
		public override ValueTask<int> ReadAsync(Memory<byte> destination, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(GZipStream))
			{
				return base.ReadAsync(destination, cancellationToken);
			}
			this.CheckDeflateStream();
			return this._deflateStream.ReadAsyncMemory(destination, cancellationToken);
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0007AF24 File Offset: 0x00079124
		public override Task WriteAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckDeflateStream();
			return this._deflateStream.WriteAsync(array, offset, count, cancellationToken);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0007AF3C File Offset: 0x0007913C
		public override Task WriteAsync(ReadOnlyMemory<byte> source, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(GZipStream))
			{
				return base.WriteAsync(source, cancellationToken);
			}
			this.CheckDeflateStream();
			return this._deflateStream.WriteAsyncMemory(source, cancellationToken);
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0007AF71 File Offset: 0x00079171
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.CheckDeflateStream();
			return this._deflateStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0007AF85 File Offset: 0x00079185
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			this.CheckDeflateStream();
			return this._deflateStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0007AF9B File Offset: 0x0007919B
		private void CheckDeflateStream()
		{
			if (this._deflateStream == null)
			{
				GZipStream.ThrowStreamClosedException();
			}
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0007AFAA File Offset: 0x000791AA
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowStreamClosedException()
		{
			throw new ObjectDisposedException(null, "Can not access a closed Stream.");
		}

		// Token: 0x04001B78 RID: 7032
		private DeflateStream _deflateStream;
	}
}
