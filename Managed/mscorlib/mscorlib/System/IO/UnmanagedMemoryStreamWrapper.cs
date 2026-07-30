using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020003C6 RID: 966
	internal sealed class UnmanagedMemoryStreamWrapper : MemoryStream
	{
		// Token: 0x06002D5F RID: 11615 RVA: 0x000A26B0 File Offset: 0x000A08B0
		internal UnmanagedMemoryStreamWrapper(UnmanagedMemoryStream stream)
		{
			this._unmanagedStream = stream;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000A26BF File Offset: 0x000A08BF
		public override bool CanRead
		{
			get
			{
				return this._unmanagedStream.CanRead;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000A26CC File Offset: 0x000A08CC
		public override bool CanSeek
		{
			get
			{
				return this._unmanagedStream.CanSeek;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000A26D9 File Offset: 0x000A08D9
		public override bool CanWrite
		{
			get
			{
				return this._unmanagedStream.CanWrite;
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000A26E8 File Offset: 0x000A08E8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._unmanagedStream.Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000A2720 File Offset: 0x000A0920
		public override void Flush()
		{
			this._unmanagedStream.Flush();
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000A272D File Offset: 0x000A092D
		public override byte[] GetBuffer()
		{
			throw new UnauthorizedAccessException(Environment.GetResourceString("MemoryStream's internal buffer cannot be accessed."));
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000A273E File Offset: 0x000A093E
		public override bool TryGetBuffer(out ArraySegment<byte> buffer)
		{
			buffer = default(ArraySegment<byte>);
			return false;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x000A2748 File Offset: 0x000A0948
		// (set) Token: 0x06002D68 RID: 11624 RVA: 0x000A2756 File Offset: 0x000A0956
		public override int Capacity
		{
			get
			{
				return (int)this._unmanagedStream.Capacity;
			}
			set
			{
				throw new IOException(Environment.GetResourceString("Unable to expand length of this stream beyond its capacity."));
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000A2767 File Offset: 0x000A0967
		public override long Length
		{
			get
			{
				return this._unmanagedStream.Length;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x000A2774 File Offset: 0x000A0974
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x000A2781 File Offset: 0x000A0981
		public override long Position
		{
			get
			{
				return this._unmanagedStream.Position;
			}
			set
			{
				this._unmanagedStream.Position = value;
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000A278F File Offset: 0x000A098F
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			return this._unmanagedStream.Read(buffer, offset, count);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000A279F File Offset: 0x000A099F
		public override int ReadByte()
		{
			return this._unmanagedStream.ReadByte();
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000A27AC File Offset: 0x000A09AC
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this._unmanagedStream.Seek(offset, loc);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000A27BC File Offset: 0x000A09BC
		[SecuritySafeCritical]
		public override byte[] ToArray()
		{
			if (!this._unmanagedStream._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this._unmanagedStream.CanRead)
			{
				__Error.ReadNotSupported();
			}
			byte[] array = new byte[this._unmanagedStream.Length];
			Buffer.Memcpy(array, 0, this._unmanagedStream.Pointer, 0, (int)this._unmanagedStream.Length);
			return array;
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000A281D File Offset: 0x000A0A1D
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._unmanagedStream.Write(buffer, offset, count);
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000A282D File Offset: 0x000A0A2D
		public override void WriteByte(byte value)
		{
			this._unmanagedStream.WriteByte(value);
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000A283C File Offset: 0x000A0A3C
		public override void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", Environment.GetResourceString("Stream cannot be null."));
			}
			if (!this._unmanagedStream._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			byte[] array = this.ToArray();
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000A2892 File Offset: 0x000A0A92
		public override void SetLength(long value)
		{
			base.SetLength(value);
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000A289C File Offset: 0x000A0A9C
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
			return this._unmanagedStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000A2954 File Offset: 0x000A0B54
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._unmanagedStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000A2962 File Offset: 0x000A0B62
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000A2974 File Offset: 0x000A0B74
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x04001793 RID: 6035
		private UnmanagedMemoryStream _unmanagedStream;
	}
}
