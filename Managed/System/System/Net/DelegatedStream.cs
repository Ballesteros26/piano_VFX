using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020004C8 RID: 1224
	internal class DelegatedStream : Stream
	{
		// Token: 0x06002449 RID: 9289 RVA: 0x0008DAD4 File Offset: 0x0008BCD4
		protected DelegatedStream()
		{
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x0008DADC File Offset: 0x0008BCDC
		protected DelegatedStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.netStream = stream as NetworkStream;
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x0008DB05 File Offset: 0x0008BD05
		protected Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x0008DB0D File Offset: 0x0008BD0D
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x0008DB1A File Offset: 0x0008BD1A
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x0008DB27 File Offset: 0x0008BD27
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x0008DB34 File Offset: 0x0008BD34
		public override long Length
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException(global::SR.GetString("Seeking is not supported on this stream."));
				}
				return this.stream.Length;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x0008DB59 File Offset: 0x0008BD59
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x0008DB7E File Offset: 0x0008BD7E
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException(global::SR.GetString("Seeking is not supported on this stream."));
				}
				return this.stream.Position;
			}
			set
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException(global::SR.GetString("Seeking is not supported on this stream."));
				}
				this.stream.Position = value;
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x0008DBA4 File Offset: 0x0008BDA4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(global::SR.GetString("Reading is not supported on this stream."));
			}
			IAsyncResult asyncResult;
			if (this.netStream != null)
			{
				asyncResult = this.netStream.UnsafeBeginRead(buffer, offset, count, callback, state);
			}
			else
			{
				asyncResult = this.stream.BeginRead(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x0008DBFC File Offset: 0x0008BDFC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(global::SR.GetString("Writing is not supported on this stream."));
			}
			IAsyncResult asyncResult;
			if (this.netStream != null)
			{
				asyncResult = this.netStream.UnsafeBeginWrite(buffer, offset, count, callback, state);
			}
			else
			{
				asyncResult = this.stream.BeginWrite(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x0008DC54 File Offset: 0x0008BE54
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0008DC61 File Offset: 0x0008BE61
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(global::SR.GetString("Reading is not supported on this stream."));
			}
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0008DC87 File Offset: 0x0008BE87
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(global::SR.GetString("Writing is not supported on this stream."));
			}
			this.stream.EndWrite(asyncResult);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0008DCAD File Offset: 0x0008BEAD
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x0008DCBA File Offset: 0x0008BEBA
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.stream.FlushAsync(cancellationToken);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0008DCC8 File Offset: 0x0008BEC8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(global::SR.GetString("Reading is not supported on this stream."));
			}
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x0008DCF0 File Offset: 0x0008BEF0
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(global::SR.GetString("Reading is not supported on this stream."));
			}
			return this.stream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0008DD1A File Offset: 0x0008BF1A
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(global::SR.GetString("Seeking is not supported on this stream."));
			}
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x0008DD41 File Offset: 0x0008BF41
		public override void SetLength(long value)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(global::SR.GetString("Seeking is not supported on this stream."));
			}
			this.stream.SetLength(value);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x0008DD67 File Offset: 0x0008BF67
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(global::SR.GetString("Writing is not supported on this stream."));
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0008DD8F File Offset: 0x0008BF8F
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(global::SR.GetString("Writing is not supported on this stream."));
			}
			return this.stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x04002023 RID: 8227
		private Stream stream;

		// Token: 0x04002024 RID: 8228
		private NetworkStream netStream;
	}
}
