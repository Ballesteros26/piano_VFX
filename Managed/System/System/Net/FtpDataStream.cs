using System;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace System.Net
{
	// Token: 0x02000514 RID: 1300
	internal class FtpDataStream : Stream, IDisposable
	{
		// Token: 0x06002708 RID: 9992 RVA: 0x00096CCC File Offset: 0x00094ECC
		internal FtpDataStream(FtpWebRequest request, Stream stream, bool isRead)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.request = request;
			this.networkStream = stream;
			this.isRead = isRead;
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x00096CF7 File Offset: 0x00094EF7
		public override bool CanRead
		{
			get
			{
				return this.isRead;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x00096CFF File Offset: 0x00094EFF
		public override bool CanWrite
		{
			get
			{
				return !this.isRead;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000074E4 File Offset: 0x000056E4
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x000074E4 File Offset: 0x000056E4
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

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x00096D0A File Offset: 0x00094F0A
		internal Stream NetworkStream
		{
			get
			{
				this.CheckDisposed();
				return this.networkStream;
			}
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x00096D18 File Offset: 0x00094F18
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Flush()
		{
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000074E4 File Offset: 0x000056E4
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x00096D24 File Offset: 0x00094F24
		private int ReadInternal(byte[] buffer, int offset, int size)
		{
			int num = 0;
			this.request.CheckIfAborted();
			try
			{
				num = this.networkStream.Read(buffer, offset, size);
			}
			catch (IOException)
			{
				throw new ProtocolViolationException("Server commited a protocol violation");
			}
			this.totalRead += num;
			if (num == 0)
			{
				this.networkStream = null;
				this.request.CloseDataConnection();
				this.request.SetTransferCompleted();
			}
			return num;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x00096D9C File Offset: 0x00094F9C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			this.CheckDisposed();
			if (!this.isRead)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("offset+size");
			}
			return new FtpDataStream.ReadDelegate(this.ReadInternal).BeginInvoke(buffer, offset, size, cb, state);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00096E10 File Offset: 0x00095010
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid asyncResult", "asyncResult");
			}
			FtpDataStream.ReadDelegate readDelegate = asyncResult2.AsyncDelegate as FtpDataStream.ReadDelegate;
			if (readDelegate == null)
			{
				throw new ArgumentException("Invalid asyncResult", "asyncResult");
			}
			return readDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x00096E68 File Offset: 0x00095068
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.request.CheckIfAborted();
			IAsyncResult asyncResult = this.BeginRead(buffer, offset, size, null, null);
			if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(this.request.ReadWriteTimeout, false))
			{
				throw new WebException("Read timed out.", WebExceptionStatus.Timeout);
			}
			return this.EndRead(asyncResult);
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x00096EC4 File Offset: 0x000950C4
		private void WriteInternal(byte[] buffer, int offset, int size)
		{
			this.request.CheckIfAborted();
			try
			{
				this.networkStream.Write(buffer, offset, size);
			}
			catch (IOException)
			{
				throw new ProtocolViolationException();
			}
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x00096F04 File Offset: 0x00095104
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			this.CheckDisposed();
			if (this.isRead)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("offset+size");
			}
			return new FtpDataStream.WriteDelegate(this.WriteInternal).BeginInvoke(buffer, offset, size, cb, state);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x00096F78 File Offset: 0x00095178
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid asyncResult.", "asyncResult");
			}
			FtpDataStream.WriteDelegate writeDelegate = asyncResult2.AsyncDelegate as FtpDataStream.WriteDelegate;
			if (writeDelegate == null)
			{
				throw new ArgumentException("Invalid asyncResult.", "asyncResult");
			}
			writeDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x00096FD0 File Offset: 0x000951D0
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.request.CheckIfAborted();
			IAsyncResult asyncResult = this.BeginWrite(buffer, offset, size, null, null);
			if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(this.request.ReadWriteTimeout, false))
			{
				throw new WebException("Read timed out.", WebExceptionStatus.Timeout);
			}
			this.EndWrite(asyncResult);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0009702C File Offset: 0x0009522C
		~FtpDataStream()
		{
			this.Dispose(false);
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x0007A451 File Offset: 0x00078651
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0009705C File Offset: 0x0009525C
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.networkStream != null)
			{
				this.request.CloseDataConnection();
				this.request.SetTransferCompleted();
				this.request = null;
				this.networkStream = null;
			}
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0009709A File Offset: 0x0009529A
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04002138 RID: 8504
		private FtpWebRequest request;

		// Token: 0x04002139 RID: 8505
		private Stream networkStream;

		// Token: 0x0400213A RID: 8506
		private bool disposed;

		// Token: 0x0400213B RID: 8507
		private bool isRead;

		// Token: 0x0400213C RID: 8508
		private int totalRead;

		// Token: 0x02000515 RID: 1301
		// (Invoke) Token: 0x06002721 RID: 10017
		private delegate void WriteDelegate(byte[] buffer, int offset, int size);

		// Token: 0x02000516 RID: 1302
		// (Invoke) Token: 0x06002725 RID: 10021
		private delegate int ReadDelegate(byte[] buffer, int offset, int size);
	}
}
