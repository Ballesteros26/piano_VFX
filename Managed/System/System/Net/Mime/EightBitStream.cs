using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x0200059A RID: 1434
	internal class EightBitStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000B05C4 File Offset: 0x000AE7C4
		private WriteStateInfoBase WriteState
		{
			get
			{
				if (this.writeState == null)
				{
					this.writeState = new WriteStateInfoBase();
				}
				return this.writeState;
			}
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000B05DF File Offset: 0x000AE7DF
		internal EightBitStream(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000B05E8 File Offset: 0x000AE7E8
		internal EightBitStream(Stream stream, bool shouldEncodeLeadingDots)
			: this(stream)
		{
			this.shouldEncodeLeadingDots = shouldEncodeLeadingDots;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000B05F8 File Offset: 0x000AE7F8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			IAsyncResult asyncResult;
			if (this.shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				asyncResult = base.BeginWrite(this.WriteState.Buffer, 0, this.WriteState.Length, callback, state);
			}
			else
			{
				asyncResult = base.BeginWrite(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000B067F File Offset: 0x000AE87F
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.EndWrite(asyncResult);
			this.WriteState.BufferFlushed();
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000B0694 File Offset: 0x000AE894
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
				return;
			}
			base.Write(buffer, offset, count);
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000B071C File Offset: 0x000AE91C
		private void EncodeLines(byte[] buffer, int offset, int count)
		{
			int num = offset;
			while (num < offset + count && num < buffer.Length)
			{
				if (buffer[num] == 13 && num + 1 < offset + count && buffer[num + 1] == 10)
				{
					this.WriteState.AppendCRLF(false);
					num++;
				}
				else if (this.WriteState.CurrentLineLength == 0 && buffer[num] == 46)
				{
					this.WriteState.Append(46);
					this.WriteState.Append(buffer[num]);
				}
				else
				{
					this.WriteState.Append(buffer[num]);
				}
				num++;
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x00004239 File Offset: 0x00002439
		public int DecodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x00004239 File Offset: 0x00002439
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x00002068 File Offset: 0x00000268
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x00004239 File Offset: 0x00002439
		public string GetEncodedString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002502 RID: 9474
		private WriteStateInfoBase writeState;

		// Token: 0x04002503 RID: 9475
		private bool shouldEncodeLeadingDots;
	}
}
