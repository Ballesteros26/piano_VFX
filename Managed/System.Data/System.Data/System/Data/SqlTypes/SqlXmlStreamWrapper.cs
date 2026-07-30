using System;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x020002D7 RID: 727
	internal sealed class SqlXmlStreamWrapper : Stream
	{
		// Token: 0x0600218D RID: 8589 RVA: 0x0009CFF5 File Offset: 0x0009B1F5
		internal SqlXmlStreamWrapper(Stream stream)
		{
			this._stream = stream;
			this._lPosition = 0L;
			this._isClosed = false;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0009D013 File Offset: 0x0009B213
		public override bool CanRead
		{
			get
			{
				return !this.IsStreamClosed() && this._stream.CanRead;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x0009D02A File Offset: 0x0009B22A
		public override bool CanSeek
		{
			get
			{
				return !this.IsStreamClosed() && this._stream.CanSeek;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x0009D041 File Offset: 0x0009B241
		public override bool CanWrite
		{
			get
			{
				return !this.IsStreamClosed() && this._stream.CanWrite;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x0009D058 File Offset: 0x0009B258
		public override long Length
		{
			get
			{
				this.ThrowIfStreamClosed("get_Length");
				this.ThrowIfStreamCannotSeek("get_Length");
				return this._stream.Length;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0009D07B File Offset: 0x0009B27B
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x0009D099 File Offset: 0x0009B299
		public override long Position
		{
			get
			{
				this.ThrowIfStreamClosed("get_Position");
				this.ThrowIfStreamCannotSeek("get_Position");
				return this._lPosition;
			}
			set
			{
				this.ThrowIfStreamClosed("set_Position");
				this.ThrowIfStreamCannotSeek("set_Position");
				if (value < 0L || value > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lPosition = value;
			}
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0009D0D8 File Offset: 0x0009B2D8
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfStreamClosed("Seek");
			this.ThrowIfStreamCannotSeek("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this._lPosition + offset;
				if (num < 0L || num > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this._stream.Length + offset;
				if (num < 0L || num > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return this._lPosition;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0009D1B4 File Offset: 0x0009B3B4
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfStreamClosed("Read");
			this.ThrowIfStreamCannotRead("Read");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			int num = this._stream.Read(buffer, offset, count);
			this._lPosition += (long)num;
			return num;
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0009D264 File Offset: 0x0009B464
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfStreamClosed("Write");
			this.ThrowIfStreamCannotWrite("Write");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			this._stream.Write(buffer, offset, count);
			this._lPosition += (long)count;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0009D314 File Offset: 0x0009B514
		public override int ReadByte()
		{
			this.ThrowIfStreamClosed("ReadByte");
			this.ThrowIfStreamCannotRead("ReadByte");
			if (this._stream.CanSeek && this._lPosition >= this._stream.Length)
			{
				return -1;
			}
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			int num = this._stream.ReadByte();
			this._lPosition += 1L;
			return num;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0009D3A8 File Offset: 0x0009B5A8
		public override void WriteByte(byte value)
		{
			this.ThrowIfStreamClosed("WriteByte");
			this.ThrowIfStreamCannotWrite("WriteByte");
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			this._stream.WriteByte(value);
			this._lPosition += 1L;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0009D419 File Offset: 0x0009B619
		public override void SetLength(long value)
		{
			this.ThrowIfStreamClosed("SetLength");
			this.ThrowIfStreamCannotSeek("SetLength");
			this._stream.SetLength(value);
			if (this._lPosition > value)
			{
				this._lPosition = value;
			}
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0009D44D File Offset: 0x0009B64D
		public override void Flush()
		{
			if (this._stream != null)
			{
				this._stream.Flush();
			}
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0009D464 File Offset: 0x0009B664
		protected override void Dispose(bool disposing)
		{
			try
			{
				this._isClosed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0009D494 File Offset: 0x0009B694
		private void ThrowIfStreamCannotSeek(string method)
		{
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonSeekable(method));
			}
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x0009D4AF File Offset: 0x0009B6AF
		private void ThrowIfStreamCannotRead(string method)
		{
			if (!this._stream.CanRead)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonReadable(method));
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0009D4CA File Offset: 0x0009B6CA
		private void ThrowIfStreamCannotWrite(string method)
		{
			if (!this._stream.CanWrite)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonWritable(method));
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0009D4E5 File Offset: 0x0009B6E5
		private void ThrowIfStreamClosed(string method)
		{
			if (this.IsStreamClosed())
			{
				throw new ObjectDisposedException(SQLResource.InvalidOpStreamClosed(method));
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0009D4FB File Offset: 0x0009B6FB
		private bool IsStreamClosed()
		{
			return this._isClosed || this._stream == null || (!this._stream.CanRead && !this._stream.CanWrite && !this._stream.CanSeek);
		}

		// Token: 0x0400168D RID: 5773
		private Stream _stream;

		// Token: 0x0400168E RID: 5774
		private long _lPosition;

		// Token: 0x0400168F RID: 5775
		private bool _isClosed;
	}
}
