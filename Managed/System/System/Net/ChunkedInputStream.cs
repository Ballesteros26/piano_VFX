using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000500 RID: 1280
	internal class ChunkedInputStream : RequestStream
	{
		// Token: 0x06002652 RID: 9810 RVA: 0x00093EB8 File Offset: 0x000920B8
		public ChunkedInputStream(HttpListenerContext context, Stream stream, byte[] buffer, int offset, int length)
			: base(stream, buffer, offset, length)
		{
			this.context = context;
			WebHeaderCollection webHeaderCollection = (WebHeaderCollection)context.Request.Headers;
			this.decoder = new MonoChunkStream(webHeaderCollection);
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x00093EF5 File Offset: 0x000920F5
		// (set) Token: 0x06002654 RID: 9812 RVA: 0x00093EFD File Offset: 0x000920FD
		public MonoChunkStream Decoder
		{
			get
			{
				return this.decoder;
			}
			set
			{
				this.decoder = value;
			}
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x00093F08 File Offset: 0x00092108
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			IAsyncResult asyncResult = this.BeginRead(buffer, offset, count, null, null);
			return this.EndRead(asyncResult);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x00093F28 File Offset: 0x00092128
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (count < 0 || offset > num - count)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult();
			httpStreamAsyncResult.Callback = cback;
			httpStreamAsyncResult.State = state;
			if (this.no_more_data)
			{
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			int num2 = this.decoder.Read(buffer, offset, count);
			offset += num2;
			count -= num2;
			if (count == 0)
			{
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (!this.decoder.WantMore)
			{
				this.no_more_data = num2 == 0;
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			httpStreamAsyncResult.Buffer = new byte[8192];
			httpStreamAsyncResult.Offset = 0;
			httpStreamAsyncResult.Count = 8192;
			ChunkedInputStream.ReadBufferState readBufferState = new ChunkedInputStream.ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
			readBufferState.InitialCount += num2;
			base.BeginRead(httpStreamAsyncResult.Buffer, httpStreamAsyncResult.Offset, httpStreamAsyncResult.Count, new AsyncCallback(this.OnRead), readBufferState);
			return httpStreamAsyncResult;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x00094060 File Offset: 0x00092260
		private void OnRead(IAsyncResult base_ares)
		{
			ChunkedInputStream.ReadBufferState readBufferState = (ChunkedInputStream.ReadBufferState)base_ares.AsyncState;
			HttpStreamAsyncResult ares = readBufferState.Ares;
			try
			{
				int num = base.EndRead(base_ares);
				this.decoder.Write(ares.Buffer, ares.Offset, num);
				num = this.decoder.Read(readBufferState.Buffer, readBufferState.Offset, readBufferState.Count);
				readBufferState.Offset += num;
				readBufferState.Count -= num;
				if (readBufferState.Count == 0 || !this.decoder.WantMore || num == 0)
				{
					this.no_more_data = !this.decoder.WantMore && num == 0;
					ares.Count = readBufferState.InitialCount - readBufferState.Count;
					ares.Complete();
				}
				else
				{
					ares.Offset = 0;
					ares.Count = Math.Min(8192, this.decoder.ChunkLeft + 6);
					base.BeginRead(ares.Buffer, ares.Offset, ares.Count, new AsyncCallback(this.OnRead), readBufferState);
				}
			}
			catch (Exception ex)
			{
				this.context.Connection.SendError(ex.Message, 400);
				ares.Complete(ex);
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000941A8 File Offset: 0x000923A8
		public override int EndRead(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			HttpStreamAsyncResult httpStreamAsyncResult = ares as HttpStreamAsyncResult;
			if (ares == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "ares");
			}
			if (!ares.IsCompleted)
			{
				ares.AsyncWaitHandle.WaitOne();
			}
			if (httpStreamAsyncResult.Error != null)
			{
				throw new HttpListenerException(400, "I/O operation aborted: " + httpStreamAsyncResult.Error.Message);
			}
			return httpStreamAsyncResult.Count;
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0009422A File Offset: 0x0009242A
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				base.Close();
			}
		}

		// Token: 0x040020F9 RID: 8441
		private bool disposed;

		// Token: 0x040020FA RID: 8442
		private MonoChunkStream decoder;

		// Token: 0x040020FB RID: 8443
		private HttpListenerContext context;

		// Token: 0x040020FC RID: 8444
		private bool no_more_data;

		// Token: 0x02000501 RID: 1281
		private class ReadBufferState
		{
			// Token: 0x0600265A RID: 9818 RVA: 0x00094241 File Offset: 0x00092441
			public ReadBufferState(byte[] buffer, int offset, int count, HttpStreamAsyncResult ares)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
				this.InitialCount = count;
				this.Ares = ares;
			}

			// Token: 0x040020FD RID: 8445
			public byte[] Buffer;

			// Token: 0x040020FE RID: 8446
			public int Offset;

			// Token: 0x040020FF RID: 8447
			public int Count;

			// Token: 0x04002100 RID: 8448
			public int InitialCount;

			// Token: 0x04002101 RID: 8449
			public HttpStreamAsyncResult Ares;
		}
	}
}
