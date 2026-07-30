using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x020001EB RID: 491
	internal sealed class SqlCachedStream : Stream
	{
		// Token: 0x060016A6 RID: 5798 RVA: 0x0007025E File Offset: 0x0006E45E
		internal SqlCachedStream(SqlCachedBuffer sqlBuf)
		{
			this._cachedBytes = sqlBuf.CachedBytes;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0000EF2B File Offset: 0x0000D12B
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0000EF2B File Offset: 0x0000D12B
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00070272 File Offset: 0x0006E472
		public override long Length
		{
			get
			{
				return this.TotalLength;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0007027C File Offset: 0x0006E47C
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x000702C3 File Offset: 0x0006E4C3
		public override long Position
		{
			get
			{
				long num = 0L;
				if (this._currentArrayIndex > 0)
				{
					for (int i = 0; i < this._currentArrayIndex; i++)
					{
						num += (long)this._cachedBytes[i].Length;
					}
				}
				return num + (long)this._currentPosition;
			}
			set
			{
				if (this._cachedBytes == null)
				{
					throw ADP.StreamClosed("set_Position");
				}
				this.SetInternalPosition(value, "set_Position");
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000702E4 File Offset: 0x0006E4E4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._cachedBytes != null)
				{
					this._cachedBytes.Clear();
				}
				this._cachedBytes = null;
				this._currentPosition = 0;
				this._currentArrayIndex = 0;
				this._totalLength = 0L;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000621D6 File Offset: 0x000603D6
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00070340 File Offset: 0x0006E540
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this._cachedBytes == null)
			{
				throw ADP.StreamClosed("Read");
			}
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw ADP.ArgumentOutOfRange(string.Empty, (offset < 0) ? "offset" : "count");
			}
			if (buffer.Length - offset < count)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			if (this._cachedBytes.Count <= this._currentArrayIndex)
			{
				return 0;
			}
			while (count > 0)
			{
				if (this._cachedBytes[this._currentArrayIndex].Length <= this._currentPosition)
				{
					this._currentArrayIndex++;
					if (this._cachedBytes.Count <= this._currentArrayIndex)
					{
						break;
					}
					this._currentPosition = 0;
				}
				int num2 = this._cachedBytes[this._currentArrayIndex].Length - this._currentPosition;
				if (num2 > count)
				{
					num2 = count;
				}
				Buffer.BlockCopy(this._cachedBytes[this._currentArrayIndex], this._currentPosition, buffer, offset, num2);
				this._currentPosition += num2;
				count -= num2;
				offset += num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00070468 File Offset: 0x0006E668
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = 0L;
			if (this._cachedBytes == null)
			{
				throw ADP.StreamClosed("Seek");
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.SetInternalPosition(offset, "offset");
				break;
			case SeekOrigin.Current:
				num = offset + this.Position;
				this.SetInternalPosition(num, "offset");
				break;
			case SeekOrigin.End:
				num = this.TotalLength + offset;
				this.SetInternalPosition(num, "offset");
				break;
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return num;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000621D6 File Offset: 0x000603D6
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000621D6 File Offset: 0x000603D6
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000704E8 File Offset: 0x0006E6E8
		private void SetInternalPosition(long lPos, string argumentName)
		{
			long num = lPos;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
			for (int i = 0; i < this._cachedBytes.Count; i++)
			{
				if (num <= (long)this._cachedBytes[i].Length)
				{
					this._currentArrayIndex = i;
					this._currentPosition = (int)num;
					return;
				}
				num -= (long)this._cachedBytes[i].Length;
			}
			if (num > 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0007055C File Offset: 0x0006E75C
		private long TotalLength
		{
			get
			{
				if (this._totalLength == 0L && this._cachedBytes != null)
				{
					long num = 0L;
					for (int i = 0; i < this._cachedBytes.Count; i++)
					{
						num += (long)this._cachedBytes[i].Length;
					}
					this._totalLength = num;
				}
				return this._totalLength;
			}
		}

		// Token: 0x04000F05 RID: 3845
		private int _currentPosition;

		// Token: 0x04000F06 RID: 3846
		private int _currentArrayIndex;

		// Token: 0x04000F07 RID: 3847
		private List<byte[]> _cachedBytes;

		// Token: 0x04000F08 RID: 3848
		private long _totalLength;
	}
}
