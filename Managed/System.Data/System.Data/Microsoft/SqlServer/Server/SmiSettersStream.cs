using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A9 RID: 937
	internal class SmiSettersStream : Stream
	{
		// Token: 0x06002C26 RID: 11302 RVA: 0x000C09BC File Offset: 0x000BEBBC
		internal SmiSettersStream(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._setters = setters;
			this._ordinal = ordinal;
			this._lengthWritten = 0L;
			this._metaData = metaData;
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x0000EF2B File Offset: 0x0000D12B
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000C09E9 File Offset: 0x000BEBE9
		public override long Length
		{
			get
			{
				return this._lengthWritten;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x000C09E9 File Offset: 0x000BEBE9
		// (set) Token: 0x06002C2C RID: 11308 RVA: 0x000BFE74 File Offset: 0x000BE074
		public override long Position
		{
			get
			{
				return this._lengthWritten;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000C09F1 File Offset: 0x000BEBF1
		public override void Flush()
		{
			this._lengthWritten = ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000BFE74 File Offset: 0x000BE074
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000C0A1C File Offset: 0x000BEC1C
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw ADP.ArgumentOutOfRange("value");
			}
			ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, value);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000C0A4D File Offset: 0x000BEC4D
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamReadNotSupported();
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000C0A54 File Offset: 0x000BEC54
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._lengthWritten += ValueUtilsSmi.SetBytes(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten, buffer, offset, count);
		}

		// Token: 0x04001ABA RID: 6842
		private SmiEventSink_Default _sink;

		// Token: 0x04001ABB RID: 6843
		private ITypedSettersV3 _setters;

		// Token: 0x04001ABC RID: 6844
		private int _ordinal;

		// Token: 0x04001ABD RID: 6845
		private long _lengthWritten;

		// Token: 0x04001ABE RID: 6846
		private SmiMetaData _metaData;
	}
}
