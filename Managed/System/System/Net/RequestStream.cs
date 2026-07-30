using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000540 RID: 1344
	internal class RequestStream : Stream
	{
		// Token: 0x06002990 RID: 10640 RVA: 0x000A097B File Offset: 0x0009EB7B
		internal RequestStream(Stream stream, byte[] buffer, int offset, int length)
			: this(stream, buffer, offset, length, -1L)
		{
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000A098A File Offset: 0x0009EB8A
		internal RequestStream(Stream stream, byte[] buffer, int offset, int length, long contentlength)
		{
			this.stream = stream;
			this.buffer = buffer;
			this.offset = offset;
			this.length = length;
			this.remaining_body = contentlength;
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002993 RID: 10643 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002995 RID: 10645 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x000074E4 File Offset: 0x000056E4
		// (set) Token: 0x06002997 RID: 10647 RVA: 0x000074E4 File Offset: 0x000056E4
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

		// Token: 0x06002998 RID: 10648 RVA: 0x000A09B7 File Offset: 0x0009EBB7
		public override void Close()
		{
			this.disposed = true;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Flush()
		{
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000A09C0 File Offset: 0x0009EBC0
		private int FillFromBuffer(byte[] buffer, int off, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (off < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			int num = buffer.Length;
			if (off > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (off > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (this.remaining_body == 0L)
			{
				return -1;
			}
			if (this.length == 0)
			{
				return 0;
			}
			int num2 = Math.Min(this.length, count);
			if (this.remaining_body > 0L)
			{
				num2 = (int)Math.Min((long)num2, this.remaining_body);
			}
			if (this.offset > this.buffer.Length - num2)
			{
				num2 = Math.Min(num2, this.buffer.Length - this.offset);
			}
			if (num2 == 0)
			{
				return 0;
			}
			Buffer.BlockCopy(this.buffer, this.offset, buffer, off, num2);
			this.offset += num2;
			this.length -= num2;
			if (this.remaining_body > 0L)
			{
				this.remaining_body -= (long)num2;
			}
			return num2;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000A0AD8 File Offset: 0x0009ECD8
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			int num = this.FillFromBuffer(buffer, offset, count);
			if (num == -1)
			{
				return 0;
			}
			if (num > 0)
			{
				return num;
			}
			num = this.stream.Read(buffer, offset, count);
			if (num > 0 && this.remaining_body > 0L)
			{
				this.remaining_body -= (long)num;
			}
			return num;
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000A0B48 File Offset: 0x0009ED48
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			int num = this.FillFromBuffer(buffer, offset, count);
			if (num > 0 || num == -1)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult();
				httpStreamAsyncResult.Buffer = buffer;
				httpStreamAsyncResult.Offset = offset;
				httpStreamAsyncResult.Count = count;
				httpStreamAsyncResult.Callback = cback;
				httpStreamAsyncResult.State = state;
				httpStreamAsyncResult.SynchRead = Math.Max(0, num);
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (this.remaining_body >= 0L && (long)count > this.remaining_body)
			{
				count = (int)Math.Min(2147483647L, this.remaining_body);
			}
			return this.stream.BeginRead(buffer, offset, count, cback, state);
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000A0BFC File Offset: 0x0009EDFC
		public override int EndRead(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(typeof(RequestStream).ToString());
			}
			if (ares == null)
			{
				throw new ArgumentNullException("async_result");
			}
			if (ares is HttpStreamAsyncResult)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = (HttpStreamAsyncResult)ares;
				if (!ares.IsCompleted)
				{
					ares.AsyncWaitHandle.WaitOne();
				}
				return httpStreamAsyncResult.SynchRead;
			}
			int num = this.stream.EndRead(ares);
			if (this.remaining_body > 0L && num > 0)
			{
				this.remaining_body -= (long)num;
			}
			return num;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000074E4 File Offset: 0x000056E4
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000074E4 File Offset: 0x000056E4
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000074E4 File Offset: 0x000056E4
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000074E4 File Offset: 0x000056E4
		public override void EndWrite(IAsyncResult async_result)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400229F RID: 8863
		private byte[] buffer;

		// Token: 0x040022A0 RID: 8864
		private int offset;

		// Token: 0x040022A1 RID: 8865
		private int length;

		// Token: 0x040022A2 RID: 8866
		private long remaining_body;

		// Token: 0x040022A3 RID: 8867
		private bool disposed;

		// Token: 0x040022A4 RID: 8868
		private Stream stream;
	}
}
