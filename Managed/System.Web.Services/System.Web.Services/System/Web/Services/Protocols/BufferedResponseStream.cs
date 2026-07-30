using System;
using System.IO;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200001A RID: 26
	internal class BufferedResponseStream : Stream
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002B2F File Offset: 0x00000D2F
		internal BufferedResponseStream(Stream outputStream, int buffersize)
		{
			this.buffer = new byte[buffersize];
			this.outputStream = outputStream;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002B51 File Offset: 0x00000D51
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002B51 File Offset: 0x00000D51
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002B54 File Offset: 0x00000D54
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002B57 File Offset: 0x00000D57
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002B57 File Offset: 0x00000D57
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002B57 File Offset: 0x00000D57
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
			}
			set
			{
				throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B68 File Offset: 0x00000D68
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.outputStream.Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000023 RID: 35
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002BA0 File Offset: 0x00000DA0
		internal bool FlushEnabled
		{
			set
			{
				this.flushEnabled = value;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BA9 File Offset: 0x00000DA9
		public override void Flush()
		{
			if (!this.flushEnabled)
			{
				return;
			}
			this.FlushWrite();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BBA File Offset: 0x00000DBA
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException(Res.GetString("StreamDoesNotRead"));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002BBA File Offset: 0x00000DBA
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotSupportedException(Res.GetString("StreamDoesNotRead"));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002B57 File Offset: 0x00000D57
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002B57 File Offset: 0x00000D57
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Res.GetString("StreamDoesNotSeek"));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002BBA File Offset: 0x00000DBA
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(Res.GetString("StreamDoesNotRead"));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002BBA File Offset: 0x00000DBA
		public override int ReadByte()
		{
			throw new NotSupportedException(Res.GetString("StreamDoesNotRead"));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002BCC File Offset: 0x00000DCC
		public override void Write(byte[] array, int offset, int count)
		{
			if (this.position > 0)
			{
				int num = this.buffer.Length - this.position;
				if (num > 0)
				{
					if (num > count)
					{
						num = count;
					}
					Array.Copy(array, offset, this.buffer, this.position, num);
					this.position += num;
					if (count == num)
					{
						return;
					}
					offset += num;
					count -= num;
				}
				this.FlushWrite();
			}
			if (count >= this.buffer.Length)
			{
				this.outputStream.Write(array, offset, count);
				return;
			}
			Array.Copy(array, offset, this.buffer, this.position, count);
			this.position = count;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C68 File Offset: 0x00000E68
		private void FlushWrite()
		{
			if (this.position > 0)
			{
				this.outputStream.Write(this.buffer, 0, this.position);
				this.position = 0;
			}
			this.outputStream.Flush();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public override void WriteByte(byte value)
		{
			if (this.position == this.buffer.Length)
			{
				this.FlushWrite();
			}
			byte[] array = this.buffer;
			int num = this.position;
			this.position = num + 1;
			array[num] = value;
		}

		// Token: 0x040001A2 RID: 418
		private Stream outputStream;

		// Token: 0x040001A3 RID: 419
		private byte[] buffer;

		// Token: 0x040001A4 RID: 420
		private int position;

		// Token: 0x040001A5 RID: 421
		private bool flushEnabled = true;
	}
}
