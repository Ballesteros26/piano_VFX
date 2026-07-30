using System;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x020001DF RID: 479
	internal sealed class SqlSequentialStream : Stream
	{
		// Token: 0x06001644 RID: 5700 RVA: 0x0006EB94 File Offset: 0x0006CD94
		internal SqlSequentialStream(SqlDataReader reader, int columnIndex)
		{
			this._reader = reader;
			this._columnIndex = columnIndex;
			this._currentTask = null;
			this._disposalTokenSource = new CancellationTokenSource();
			if (reader.Command != null && reader.Command.CommandTimeout != 0)
			{
				this._readTimeout = (int)Math.Min((long)reader.Command.CommandTimeout * 1000L, 2147483647L);
				return;
			}
			this._readTimeout = -1;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0006EC09 File Offset: 0x0006CE09
		public override bool CanRead
		{
			get
			{
				return this._reader != null && !this._reader.IsClosed;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0000EF2B File Offset: 0x0000D12B
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00005E03 File Offset: 0x00004003
		public override void Flush()
		{
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x000621D6 File Offset: 0x000603D6
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x000621D6 File Offset: 0x000603D6
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x000621D6 File Offset: 0x000603D6
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0006EC23 File Offset: 0x0006CE23
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x0006EC2B File Offset: 0x0006CE2B
		public override int ReadTimeout
		{
			get
			{
				return this._readTimeout;
			}
			set
			{
				if (value > 0 || value == -1)
				{
					this._readTimeout = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("value");
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0006EC47 File Offset: 0x0006CE47
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0006EC50 File Offset: 0x0006CE50
		public override int Read(byte[] buffer, int offset, int count)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			if (!this.CanRead)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			int bytesInternalSequential;
			try
			{
				bytesInternalSequential = this._reader.GetBytesInternalSequential(this._columnIndex, buffer, offset, count, new long?((long)this._readTimeout));
			}
			catch (SqlException ex)
			{
				throw ADP.ErrorReadingFromStream(ex);
			}
			return bytesInternalSequential;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0006ECC0 File Offset: 0x0006CEC0
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			TaskCompletionSource<int> completion = new TaskCompletionSource<int>();
			if (!this.CanRead)
			{
				completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
			}
			else
			{
				try
				{
					if (Interlocked.CompareExchange<Task>(ref this._currentTask, completion.Task, null) != null)
					{
						completion.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					}
					else
					{
						CancellationTokenSource combinedTokenSource;
						if (!cancellationToken.CanBeCanceled)
						{
							combinedTokenSource = this._disposalTokenSource;
						}
						else
						{
							combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this._disposalTokenSource.Token);
						}
						int num = 0;
						Task<int> task = null;
						SqlDataReader reader = this._reader;
						if (reader != null && !cancellationToken.IsCancellationRequested && !this._disposalTokenSource.Token.IsCancellationRequested)
						{
							task = reader.GetBytesAsync(this._columnIndex, buffer, offset, count, this._readTimeout, combinedTokenSource.Token, out num);
						}
						if (task == null)
						{
							this._currentTask = null;
							if (cancellationToken.IsCancellationRequested)
							{
								completion.SetCanceled();
							}
							else if (!this.CanRead)
							{
								completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
							}
							else
							{
								completion.SetResult(num);
							}
							if (combinedTokenSource != this._disposalTokenSource)
							{
								combinedTokenSource.Dispose();
							}
						}
						else
						{
							task.ContinueWith(delegate(Task<int> t)
							{
								this._currentTask = null;
								if (t.Status == TaskStatus.RanToCompletion && this.CanRead)
								{
									completion.SetResult(t.Result);
								}
								else if (t.Status == TaskStatus.Faulted)
								{
									if (t.Exception.InnerException is SqlException)
									{
										completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
									}
									else
									{
										completion.SetException(t.Exception.InnerException);
									}
								}
								else if (!this.CanRead)
								{
									completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
								}
								else
								{
									completion.SetCanceled();
								}
								if (combinedTokenSource != this._disposalTokenSource)
								{
									combinedTokenSource.Dispose();
								}
							}, TaskScheduler.Default);
						}
					}
				}
				catch (Exception ex)
				{
					completion.TrySetException(ex);
					Interlocked.CompareExchange<Task>(ref this._currentTask, null, completion.Task);
					throw;
				}
			}
			return completion.Task;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0006EEB4 File Offset: 0x0006D0B4
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ReadAsync(array, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0006EECD File Offset: 0x0006D0CD
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000621D6 File Offset: 0x000603D6
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000621D6 File Offset: 0x000603D6
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000621D6 File Offset: 0x000603D6
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0006EED8 File Offset: 0x0006D0D8
		internal void SetClosed()
		{
			this._disposalTokenSource.Cancel();
			this._reader = null;
			Task currentTask = this._currentTask;
			if (currentTask != null)
			{
				((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0006EF0D File Offset: 0x0006D10D
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0006EF20 File Offset: 0x0006D120
		internal static void ValidateReadParameters(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0)
			{
				throw ADP.ArgumentOutOfRange("offset");
			}
			if (count < 0)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			try
			{
				if (checked(offset + count) > buffer.Length)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}
			catch (OverflowException)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
		}

		// Token: 0x04000EC4 RID: 3780
		private SqlDataReader _reader;

		// Token: 0x04000EC5 RID: 3781
		private int _columnIndex;

		// Token: 0x04000EC6 RID: 3782
		private Task _currentTask;

		// Token: 0x04000EC7 RID: 3783
		private int _readTimeout;

		// Token: 0x04000EC8 RID: 3784
		private CancellationTokenSource _disposalTokenSource;
	}
}
