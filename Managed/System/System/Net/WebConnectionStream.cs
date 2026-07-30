using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000556 RID: 1366
	internal abstract class WebConnectionStream : Stream
	{
		// Token: 0x06002A8D RID: 10893 RVA: 0x000A4740 File Offset: 0x000A2940
		protected WebConnectionStream(WebConnection cnc, WebOperation operation, Stream stream)
		{
			this.Connection = cnc;
			this.Operation = operation;
			this.Request = operation.Request;
			this.InnerStream = stream;
			this.read_timeout = this.Request.ReadWriteTimeout;
			this.write_timeout = this.read_timeout;
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x000A479C File Offset: 0x000A299C
		internal HttpWebRequest Request { get; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x000A47A4 File Offset: 0x000A29A4
		internal WebConnection Connection { get; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x000A47AC File Offset: 0x000A29AC
		internal WebOperation Operation { get; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x000A47B4 File Offset: 0x000A29B4
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.Connection.ServicePoint;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x000A47C1 File Offset: 0x000A29C1
		internal Stream InnerStream { get; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x000A47C9 File Offset: 0x000A29C9
		// (set) Token: 0x06002A95 RID: 10901 RVA: 0x000A47D1 File Offset: 0x000A29D1
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x000A47E9 File Offset: 0x000A29E9
		// (set) Token: 0x06002A97 RID: 10903 RVA: 0x000A47F1 File Offset: 0x000A29F1
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000A4809 File Offset: 0x000A2A09
		protected Exception GetException(Exception e)
		{
			e = HttpWebRequest.FlattenException(e);
			if (e is WebException)
			{
				return e;
			}
			if (this.Operation.Aborted || e is OperationCanceledException || e is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return e;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000A4844 File Offset: 0x000A2A44
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int result;
			try
			{
				result = this.ReadAsync(buffer, offset, size, CancellationToken.None).Result;
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return result;
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000A48DC File Offset: 0x000A2ADC
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, size, CancellationToken.None), cb, state);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000A4958 File Offset: 0x000A2B58
		public override int EndRead(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			int num;
			try
			{
				num = TaskToApm.End<int>(r);
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return num;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000A4998 File Offset: 0x000A2B98
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, size, CancellationToken.None), cb, state);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000A4A14 File Offset: 0x000A2C14
		public override void EndWrite(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			try
			{
				TaskToApm.End(r);
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000A4A54 File Offset: 0x000A2C54
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			try
			{
				base.WriteAsync(buffer, offset, size).Wait();
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Flush()
		{
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000A4AE4 File Offset: 0x000A2CE4
		internal void InternalClose()
		{
			this.disposed = true;
		}

		// Token: 0x06002AA1 RID: 10913
		protected abstract void Close_internal(ref bool disposed);

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000A4AED File Offset: 0x000A2CED
		public override void Close()
		{
			this.Close_internal(ref this.disposed);
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Seek(long a, SeekOrigin b)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000074E4 File Offset: 0x000056E4
		public override void SetLength(long a)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x000074E4 File Offset: 0x000056E4
		// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x000074E4 File Offset: 0x000056E4
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

		// Token: 0x04002335 RID: 9013
		protected bool closed;

		// Token: 0x04002336 RID: 9014
		private bool disposed;

		// Token: 0x04002337 RID: 9015
		private object locker = new object();

		// Token: 0x04002338 RID: 9016
		private int read_timeout;

		// Token: 0x04002339 RID: 9017
		private int write_timeout;

		// Token: 0x0400233A RID: 9018
		internal bool IgnoreIOErrors;
	}
}
