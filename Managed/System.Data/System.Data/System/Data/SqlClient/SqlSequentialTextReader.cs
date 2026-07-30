using System;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x020001E2 RID: 482
	internal sealed class SqlSequentialTextReader : TextReader
	{
		// Token: 0x0600165D RID: 5725 RVA: 0x0006F0A4 File Offset: 0x0006D2A4
		internal SqlSequentialTextReader(SqlDataReader reader, int columnIndex, Encoding encoding)
		{
			this._reader = reader;
			this._columnIndex = columnIndex;
			this._encoding = encoding;
			this._decoder = encoding.GetDecoder();
			this._leftOverBytes = null;
			this._peekedChar = -1;
			this._currentTask = null;
			this._disposalTokenSource = new CancellationTokenSource();
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0006F0F8 File Offset: 0x0006D2F8
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0006F100 File Offset: 0x0006D300
		public override int Peek()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (!this.HasPeekedChar)
			{
				this._peekedChar = this.Read();
			}
			return this._peekedChar;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0006F13C File Offset: 0x0006D33C
		public override int Read()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			int num = -1;
			if (this.HasPeekedChar)
			{
				num = this._peekedChar;
				this._peekedChar = -1;
			}
			else
			{
				char[] array = new char[1];
				if (this.InternalRead(array, 0, 1) == 1)
				{
					num = (int)array[0];
				}
			}
			return num;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0006F198 File Offset: 0x0006D398
		public override int Read(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			int num = 0;
			int num2 = count;
			if (num2 > 0 && this.HasPeekedChar)
			{
				buffer[index + num] = (char)this._peekedChar;
				num++;
				num2--;
				this._peekedChar = -1;
			}
			return num + this.InternalRead(buffer, index + num, num2);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0006F204 File Offset: 0x0006D404
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			TaskCompletionSource<int> completion = new TaskCompletionSource<int>();
			if (this.IsClosed)
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
						bool flag = true;
						int charsRead = 0;
						int adjustedIndex = index;
						int charsNeeded = count;
						if (this.HasPeekedChar && charsNeeded > 0)
						{
							int peekedChar = this._peekedChar;
							if (peekedChar >= 0)
							{
								buffer[adjustedIndex] = (char)peekedChar;
								int num = adjustedIndex;
								adjustedIndex = num + 1;
								num = charsRead;
								charsRead = num + 1;
								num = charsNeeded;
								charsNeeded = num - 1;
								this._peekedChar = -1;
							}
						}
						int byteBufferUsed;
						byte[] byteBuffer = this.PrepareByteBuffer(charsNeeded, out byteBufferUsed);
						if (byteBufferUsed < byteBuffer.Length || byteBuffer.Length == 0)
						{
							SqlDataReader reader = this._reader;
							if (reader != null)
							{
								int num2;
								Task<int> bytesAsync = reader.GetBytesAsync(this._columnIndex, byteBuffer, byteBufferUsed, byteBuffer.Length - byteBufferUsed, -1, this._disposalTokenSource.Token, out num2);
								if (bytesAsync == null)
								{
									byteBufferUsed += num2;
								}
								else
								{
									flag = false;
									bytesAsync.ContinueWith(delegate(Task<int> t)
									{
										this._currentTask = null;
										if (t.Status == TaskStatus.RanToCompletion && !this.IsClosed)
										{
											try
											{
												int result = t.Result;
												byteBufferUsed += result;
												if (byteBufferUsed > 0)
												{
													charsRead += this.DecodeBytesToChars(byteBuffer, byteBufferUsed, buffer, adjustedIndex, charsNeeded);
												}
												completion.SetResult(charsRead);
												return;
											}
											catch (Exception ex2)
											{
												completion.SetException(ex2);
												return;
											}
										}
										if (this.IsClosed)
										{
											completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
											return;
										}
										if (t.Status == TaskStatus.Faulted)
										{
											if (t.Exception.InnerException is SqlException)
											{
												completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
												return;
											}
											completion.SetException(t.Exception.InnerException);
											return;
										}
										else
										{
											completion.SetCanceled();
										}
									}, TaskScheduler.Default);
								}
								if (flag && byteBufferUsed > 0)
								{
									charsRead += this.DecodeBytesToChars(byteBuffer, byteBufferUsed, buffer, adjustedIndex, charsNeeded);
								}
							}
							else
							{
								completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
							}
						}
						if (flag)
						{
							this._currentTask = null;
							if (this.IsClosed)
							{
								completion.SetCanceled();
							}
							else
							{
								completion.SetResult(charsRead);
							}
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

		// Token: 0x06001663 RID: 5731 RVA: 0x0006F4BC File Offset: 0x0006D6BC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0006F4D0 File Offset: 0x0006D6D0
		internal void SetClosed()
		{
			this._disposalTokenSource.Cancel();
			this._reader = null;
			this._peekedChar = -1;
			Task currentTask = this._currentTask;
			if (currentTask != null)
			{
				((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0006F50C File Offset: 0x0006D70C
		private int InternalRead(char[] buffer, int index, int count)
		{
			int num2;
			try
			{
				int num;
				byte[] array = this.PrepareByteBuffer(count, out num);
				num += this._reader.GetBytesInternalSequential(this._columnIndex, array, num, array.Length - num, null);
				if (num > 0)
				{
					num2 = this.DecodeBytesToChars(array, num, buffer, index, count);
				}
				else
				{
					num2 = 0;
				}
			}
			catch (SqlException ex)
			{
				throw ADP.ErrorReadingFromStream(ex);
			}
			return num2;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0006F574 File Offset: 0x0006D774
		private byte[] PrepareByteBuffer(int numberOfChars, out int byteBufferUsed)
		{
			byte[] array;
			if (numberOfChars == 0)
			{
				array = Array.Empty<byte>();
				byteBufferUsed = 0;
			}
			else
			{
				int maxByteCount = this._encoding.GetMaxByteCount(numberOfChars);
				if (this._leftOverBytes != null)
				{
					if (this._leftOverBytes.Length > maxByteCount)
					{
						array = this._leftOverBytes;
						byteBufferUsed = array.Length;
					}
					else
					{
						array = new byte[maxByteCount];
						Buffer.BlockCopy(this._leftOverBytes, 0, array, 0, this._leftOverBytes.Length);
						byteBufferUsed = this._leftOverBytes.Length;
					}
				}
				else
				{
					array = new byte[maxByteCount];
					byteBufferUsed = 0;
				}
			}
			return array;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0006F5F4 File Offset: 0x0006D7F4
		private int DecodeBytesToChars(byte[] inBuffer, int inBufferCount, char[] outBuffer, int outBufferOffset, int outBufferCount)
		{
			int num;
			int num2;
			bool flag;
			this._decoder.Convert(inBuffer, 0, inBufferCount, outBuffer, outBufferOffset, outBufferCount, false, out num, out num2, out flag);
			if (!flag && num < inBufferCount)
			{
				this._leftOverBytes = new byte[inBufferCount - num];
				Buffer.BlockCopy(inBuffer, num, this._leftOverBytes, 0, this._leftOverBytes.Length);
			}
			else
			{
				this._leftOverBytes = null;
			}
			return num2;
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0006F650 File Offset: 0x0006D850
		private bool IsClosed
		{
			get
			{
				return this._reader == null;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0006F65B File Offset: 0x0006D85B
		private bool HasPeekedChar
		{
			get
			{
				return this._peekedChar >= 0;
			}
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0006F66C File Offset: 0x0006D86C
		internal static void ValidateReadParameters(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (index < 0)
			{
				throw ADP.ArgumentOutOfRange("index");
			}
			if (count < 0)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			try
			{
				if (checked(index + count) > buffer.Length)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}
			catch (OverflowException)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
		}

		// Token: 0x04000ECD RID: 3789
		private SqlDataReader _reader;

		// Token: 0x04000ECE RID: 3790
		private int _columnIndex;

		// Token: 0x04000ECF RID: 3791
		private Encoding _encoding;

		// Token: 0x04000ED0 RID: 3792
		private Decoder _decoder;

		// Token: 0x04000ED1 RID: 3793
		private byte[] _leftOverBytes;

		// Token: 0x04000ED2 RID: 3794
		private int _peekedChar;

		// Token: 0x04000ED3 RID: 3795
		private Task _currentTask;

		// Token: 0x04000ED4 RID: 3796
		private CancellationTokenSource _disposalTokenSource;
	}
}
