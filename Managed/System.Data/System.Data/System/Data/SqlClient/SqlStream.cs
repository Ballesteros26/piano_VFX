using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001EA RID: 490
	internal sealed class SqlStream : Stream
	{
		// Token: 0x06001695 RID: 5781 RVA: 0x0006FF32 File Offset: 0x0006E132
		internal SqlStream(SqlDataReader reader, bool addByteOrderMark, bool processAllRows)
			: this(0, reader, addByteOrderMark, processAllRows, true)
		{
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0006FF3F File Offset: 0x0006E13F
		internal SqlStream(int columnOrdinal, SqlDataReader reader, bool addByteOrderMark, bool processAllRows, bool advanceReader)
		{
			this._columnOrdinal = columnOrdinal;
			this._reader = reader;
			this._bom = (addByteOrderMark ? 65279 : 0);
			this._processAllRows = processAllRows;
			this._advanceReader = advanceReader;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0000EF2B File Offset: 0x0000D12B
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x000621D6 File Offset: 0x000603D6
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x000621D6 File Offset: 0x000603D6
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x000621D6 File Offset: 0x000603D6
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0006FF78 File Offset: 0x0006E178
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._advanceReader && this._reader != null && !this._reader.IsClosed)
				{
					this._reader.Close();
				}
				this._reader = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000621D6 File Offset: 0x000603D6
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0006FFD4 File Offset: 0x0006E1D4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			int num2 = 0;
			if (this._reader == null)
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
			if (this._bom > 0)
			{
				this._bufferedData = new byte[2];
				num2 = this.ReadBytes(this._bufferedData, 0, 2);
				if (num2 < 2 || (this._bufferedData[0] == 223 && this._bufferedData[1] == 255))
				{
					this._bom = 0;
				}
				while (count > 0 && this._bom > 0)
				{
					buffer[offset] = (byte)this._bom;
					this._bom >>= 8;
					offset++;
					count--;
					num++;
				}
			}
			if (num2 > 0)
			{
				while (count > 0)
				{
					buffer[offset++] = this._bufferedData[0];
					num++;
					count--;
					if (num2 > 1 && count > 0)
					{
						buffer[offset++] = this._bufferedData[1];
						num++;
						count--;
						break;
					}
				}
				this._bufferedData = null;
			}
			return num + this.ReadBytes(buffer, offset, count);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00070118 File Offset: 0x0006E318
		private static bool AdvanceToNextRow(SqlDataReader reader)
		{
			while (!reader.Read())
			{
				if (!reader.NextResult())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00070130 File Offset: 0x0006E330
		private int ReadBytes(byte[] buffer, int offset, int count)
		{
			bool flag = true;
			int num = 0;
			if (this._reader.IsClosed || this._endOfColumn)
			{
				return 0;
			}
			try
			{
				while (count > 0)
				{
					if (this._advanceReader && this._bytesCol == 0L)
					{
						flag = false;
						if ((!this._readFirstRow || this._processAllRows) && SqlStream.AdvanceToNextRow(this._reader))
						{
							this._readFirstRow = true;
							if (this._reader.IsDBNull(this._columnOrdinal))
							{
								continue;
							}
							flag = true;
						}
					}
					if (!flag)
					{
						break;
					}
					int num2 = (int)this._reader.GetBytesInternal(this._columnOrdinal, this._bytesCol, buffer, offset, count);
					if (num2 < count)
					{
						this._bytesCol = 0L;
						flag = false;
						if (!this._advanceReader)
						{
							this._endOfColumn = true;
						}
					}
					else
					{
						this._bytesCol += (long)num2;
					}
					count -= num2;
					offset += num2;
					num += num2;
				}
				if (!flag && this._advanceReader)
				{
					this._reader.Close();
				}
			}
			catch (Exception ex)
			{
				if (this._advanceReader && ADP.IsCatchableExceptionType(ex))
				{
					this._reader.Close();
				}
				throw;
			}
			return num;
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00070254 File Offset: 0x0006E454
		internal XmlReader ToXmlReader(bool async = false)
		{
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this, true, async);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x000621D6 File Offset: 0x000603D6
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000621D6 File Offset: 0x000603D6
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000621D6 File Offset: 0x000603D6
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x04000EFC RID: 3836
		private SqlDataReader _reader;

		// Token: 0x04000EFD RID: 3837
		private int _columnOrdinal;

		// Token: 0x04000EFE RID: 3838
		private long _bytesCol;

		// Token: 0x04000EFF RID: 3839
		private int _bom;

		// Token: 0x04000F00 RID: 3840
		private byte[] _bufferedData;

		// Token: 0x04000F01 RID: 3841
		private bool _processAllRows;

		// Token: 0x04000F02 RID: 3842
		private bool _advanceReader;

		// Token: 0x04000F03 RID: 3843
		private bool _readFirstRow;

		// Token: 0x04000F04 RID: 3844
		private bool _endOfColumn;
	}
}
