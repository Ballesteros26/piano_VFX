using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
	// Token: 0x02000541 RID: 1345
	internal class ResponseStream : Stream
	{
		// Token: 0x060029A3 RID: 10659 RVA: 0x000A0C87 File Offset: 0x0009EE87
		internal ResponseStream(Stream stream, HttpListenerResponse response, bool ignore_errors)
		{
			this.response = response;
			this.ignore_errors = ignore_errors;
			this.stream = stream;
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x000074E4 File Offset: 0x000056E4
		// (set) Token: 0x060029A9 RID: 10665 RVA: 0x000074E4 File Offset: 0x000056E4
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

		// Token: 0x060029AA RID: 10666 RVA: 0x000A0CA4 File Offset: 0x0009EEA4
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				MemoryStream headers = this.GetHeaders(true);
				bool sendChunked = this.response.SendChunked;
				if (this.stream.CanWrite)
				{
					try
					{
						if (headers != null)
						{
							long position = headers.Position;
							if (sendChunked && !this.trailer_sent)
							{
								byte[] array = ResponseStream.GetChunkSizeBytes(0, true);
								headers.Position = headers.Length;
								headers.Write(array, 0, array.Length);
							}
							this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
							this.trailer_sent = true;
						}
						else if (sendChunked && !this.trailer_sent)
						{
							byte[] array = ResponseStream.GetChunkSizeBytes(0, true);
							this.InternalWrite(array, 0, array.Length);
							this.trailer_sent = true;
						}
					}
					catch (IOException)
					{
					}
				}
				this.response.Close();
			}
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000A0D80 File Offset: 0x0009EF80
		private MemoryStream GetHeaders(bool closing)
		{
			object headers_lock = this.response.headers_lock;
			MemoryStream memoryStream;
			lock (headers_lock)
			{
				if (this.response.HeadersSent)
				{
					memoryStream = null;
				}
				else
				{
					MemoryStream memoryStream2 = new MemoryStream();
					this.response.SendHeaders(closing, memoryStream2);
					memoryStream = memoryStream2;
				}
			}
			return memoryStream;
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Flush()
		{
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000A0DE8 File Offset: 0x0009EFE8
		private static byte[] GetChunkSizeBytes(int size, bool final)
		{
			string text = string.Format("{0:x}\r\n{1}", size, final ? "\r\n" : "");
			return Encoding.ASCII.GetBytes(text);
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000A0E20 File Offset: 0x0009F020
		internal void InternalWrite(byte[] buffer, int offset, int count)
		{
			if (this.ignore_errors)
			{
				try
				{
					this.stream.Write(buffer, offset, count);
					return;
				}
				catch
				{
					return;
				}
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000A0E68 File Offset: 0x0009F068
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (count == 0)
			{
				return;
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(array, 0, array.Length);
				}
				int num = Math.Min(count, 16384 - (int)headers.Position + (int)position);
				headers.Write(buffer, offset, num);
				count -= num;
				offset += num;
				this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
				headers.SetLength(0L);
				headers.Capacity = 0;
			}
			else if (sendChunked)
			{
				byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(array, 0, array.Length);
			}
			if (count > 0)
			{
				this.InternalWrite(buffer, offset, count);
			}
			if (sendChunked)
			{
				this.InternalWrite(ResponseStream.crlf, 0, 2);
			}
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000A0F60 File Offset: 0x0009F160
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(array, 0, array.Length);
				}
				headers.Write(buffer, offset, count);
				buffer = headers.GetBuffer();
				offset = (int)position;
				count = (int)(headers.Position - position);
			}
			else if (sendChunked)
			{
				byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(array, 0, array.Length);
			}
			return this.stream.BeginWrite(buffer, offset, count, cback, state);
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000A1014 File Offset: 0x0009F214
		public override void EndWrite(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (this.ignore_errors)
			{
				try
				{
					this.stream.EndWrite(ares);
					if (this.response.SendChunked)
					{
						this.stream.Write(ResponseStream.crlf, 0, 2);
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.stream.EndWrite(ares);
			if (this.response.SendChunked)
			{
				this.stream.Write(ResponseStream.crlf, 0, 2);
			}
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000074E4 File Offset: 0x000056E4
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000074E4 File Offset: 0x000056E4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000074E4 File Offset: 0x000056E4
		public override int EndRead(IAsyncResult ares)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000074E4 File Offset: 0x000056E4
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040022A5 RID: 8869
		private HttpListenerResponse response;

		// Token: 0x040022A6 RID: 8870
		private bool ignore_errors;

		// Token: 0x040022A7 RID: 8871
		private bool disposed;

		// Token: 0x040022A8 RID: 8872
		private bool trailer_sent;

		// Token: 0x040022A9 RID: 8873
		private Stream stream;

		// Token: 0x040022AA RID: 8874
		private static byte[] crlf = new byte[] { 13, 10 };
	}
}
