using System;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Data.SqlTypes
{
	// Token: 0x020002C0 RID: 704
	internal sealed class StreamOnSqlBytes : Stream
	{
		// Token: 0x06001E8C RID: 7820 RVA: 0x0009408F File Offset: 0x0009228F
		internal StreamOnSqlBytes(SqlBytes sb)
		{
			this._sb = sb;
			this._lPosition = 0L;
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x000940A6 File Offset: 0x000922A6
		public override bool CanRead
		{
			get
			{
				return this._sb != null && !this._sb.IsNull;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x000940C0 File Offset: 0x000922C0
		public override bool CanSeek
		{
			get
			{
				return this._sb != null;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x000940CB File Offset: 0x000922CB
		public override bool CanWrite
		{
			get
			{
				return this._sb != null && (!this._sb.IsNull || this._sb._rgbBuf != null);
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x000940F4 File Offset: 0x000922F4
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this._sb.Length;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0009410C File Offset: 0x0009230C
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0009411F File Offset: 0x0009231F
		public override long Position
		{
			get
			{
				this.CheckIfStreamClosed("get_Position");
				return this._lPosition;
			}
			set
			{
				this.CheckIfStreamClosed("set_Position");
				if (value < 0L || value > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lPosition = value;
			}
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x00094154 File Offset: 0x00092354
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckIfStreamClosed("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this._lPosition + offset;
				if (num < 0L || num > this._sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this._sb.Length + offset;
				if (num < 0L || num > this._sb.Length)
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

		// Token: 0x06001E94 RID: 7828 RVA: 0x00094224 File Offset: 0x00092424
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Read");
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
			int num = (int)this._sb.Read(this._lPosition, buffer, offset, count);
			this._lPosition += (long)num;
			return num;
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0009429C File Offset: 0x0009249C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Write");
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
			this._sb.Write(this._lPosition, buffer, offset, count);
			this._lPosition += (long)count;
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00094314 File Offset: 0x00092514
		public override int ReadByte()
		{
			this.CheckIfStreamClosed("ReadByte");
			if (this._lPosition >= this._sb.Length)
			{
				return -1;
			}
			int num = (int)this._sb[this._lPosition];
			this._lPosition += 1L;
			return num;
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00094361 File Offset: 0x00092561
		public override void WriteByte(byte value)
		{
			this.CheckIfStreamClosed("WriteByte");
			this._sb[this._lPosition] = value;
			this._lPosition += 1L;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0009438F File Offset: 0x0009258F
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this._sb.SetLength(value);
			if (this._lPosition > value)
			{
				this._lPosition = value;
			}
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000943B8 File Offset: 0x000925B8
		public override void Flush()
		{
			if (this._sb.FStream())
			{
				this._sb._stream.Flush();
			}
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000943D8 File Offset: 0x000925D8
		protected override void Dispose(bool disposing)
		{
			try
			{
				this._sb = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00094408 File Offset: 0x00092608
		private bool FClosed()
		{
			return this._sb == null;
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x00094413 File Offset: 0x00092613
		private void CheckIfStreamClosed([CallerMemberName] string methodname = "")
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x040015C5 RID: 5573
		private SqlBytes _sb;

		// Token: 0x040015C6 RID: 5574
		private long _lPosition;
	}
}
