using System;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200039B RID: 923
	internal class SmiGettersStream : Stream
	{
		// Token: 0x06002BA0 RID: 11168 RVA: 0x000BFE0C File Offset: 0x000BE00C
		internal SmiGettersStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._getters = getters;
			this._ordinal = ordinal;
			this._readPosition = 0L;
			this._metaData = metaData;
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x0000EF2B File Offset: 0x0000D12B
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000BFE3C File Offset: 0x000BE03C
		public override long Length
		{
			get
			{
				return ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, 0L, null, 0, 0, false);
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x000BFE6C File Offset: 0x000BE06C
		// (set) Token: 0x06002BA6 RID: 11174 RVA: 0x000BFE74 File Offset: 0x000BE074
		public override long Position
		{
			get
			{
				return this._readPosition;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000BFE7B File Offset: 0x000BE07B
		public override void Flush()
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000BFE74 File Offset: 0x000BE074
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000BFE7B File Offset: 0x000BE07B
		public override void SetLength(long value)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000BFE84 File Offset: 0x000BE084
		public override int Read(byte[] buffer, int offset, int count)
		{
			long bytesInternal = ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, this._readPosition, buffer, offset, count, false);
			this._readPosition += bytesInternal;
			return checked((int)bytesInternal);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000BFE7B File Offset: 0x000BE07B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x04001A5B RID: 6747
		private SmiEventSink_Default _sink;

		// Token: 0x04001A5C RID: 6748
		private ITypedGettersV3 _getters;

		// Token: 0x04001A5D RID: 6749
		private int _ordinal;

		// Token: 0x04001A5E RID: 6750
		private long _readPosition;

		// Token: 0x04001A5F RID: 6751
		private SmiMetaData _metaData;
	}
}
