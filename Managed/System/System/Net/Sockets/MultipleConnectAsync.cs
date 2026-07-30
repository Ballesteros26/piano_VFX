using System;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020005DB RID: 1499
	internal abstract class MultipleConnectAsync
	{
		// Token: 0x06002F81 RID: 12161 RVA: 0x000BB974 File Offset: 0x000B9B74
		public bool StartConnectAsync(SocketAsyncEventArgs args, DnsEndPoint endPoint)
		{
			object obj = this.lockObject;
			bool flag2;
			lock (obj)
			{
				this.userArgs = args;
				this.endPoint = endPoint;
				if (this.state == MultipleConnectAsync.State.Canceled)
				{
					this.SyncFail(new SocketException(SocketError.OperationAborted));
					flag2 = false;
				}
				else
				{
					this.state = MultipleConnectAsync.State.DnsQuery;
					IAsyncResult asyncResult = Dns.BeginGetHostAddresses(endPoint.Host, new AsyncCallback(this.DnsCallback), null);
					if (asyncResult.CompletedSynchronously)
					{
						flag2 = this.DoDnsCallback(asyncResult, true);
					}
					else
					{
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000BBA10 File Offset: 0x000B9C10
		private void DnsCallback(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				this.DoDnsCallback(result, false);
			}
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000BBA24 File Offset: 0x000B9C24
		private bool DoDnsCallback(IAsyncResult result, bool sync)
		{
			Exception ex = null;
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.state == MultipleConnectAsync.State.Canceled)
				{
					return true;
				}
				try
				{
					this.addressList = Dns.EndGetHostAddresses(result);
				}
				catch (Exception ex2)
				{
					this.state = MultipleConnectAsync.State.Completed;
					ex = ex2;
				}
				if (ex == null)
				{
					this.state = MultipleConnectAsync.State.ConnectAttempt;
					this.internalArgs = new SocketAsyncEventArgs();
					this.internalArgs.Completed += this.InternalConnectCallback;
					this.internalArgs.SetBuffer(this.userArgs.Buffer, this.userArgs.Offset, this.userArgs.Count);
					ex = this.AttemptConnection();
					if (ex != null)
					{
						this.state = MultipleConnectAsync.State.Completed;
					}
				}
			}
			return ex == null || this.Fail(sync, ex);
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000BBB10 File Offset: 0x000B9D10
		private void InternalConnectCallback(object sender, SocketAsyncEventArgs args)
		{
			Exception ex = null;
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.state == MultipleConnectAsync.State.Canceled)
				{
					ex = new SocketException(SocketError.OperationAborted);
				}
				else if (args.SocketError == SocketError.Success)
				{
					this.state = MultipleConnectAsync.State.Completed;
				}
				else if (args.SocketError == SocketError.OperationAborted)
				{
					ex = new SocketException(SocketError.OperationAborted);
					this.state = MultipleConnectAsync.State.Canceled;
				}
				else
				{
					SocketError socketError = args.SocketError;
					Exception ex2 = this.AttemptConnection();
					if (ex2 == null)
					{
						return;
					}
					SocketException ex3 = ex2 as SocketException;
					if (ex3 != null && ex3.SocketErrorCode == SocketError.NoData)
					{
						ex = new SocketException(socketError);
					}
					else
					{
						ex = ex2;
					}
					this.state = MultipleConnectAsync.State.Completed;
				}
			}
			if (ex == null)
			{
				this.Succeed();
				return;
			}
			this.AsyncFail(ex);
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000BBBEC File Offset: 0x000B9DEC
		private Exception AttemptConnection()
		{
			try
			{
				Socket socket = null;
				IPAddress ipaddress = this.GetNextAddress(out socket);
				if (ipaddress == null)
				{
					return new SocketException(SocketError.NoData);
				}
				this.internalArgs.RemoteEndPoint = new IPEndPoint(ipaddress, this.endPoint.Port);
				if (!socket.ConnectAsync(this.internalArgs))
				{
					return new SocketException(this.internalArgs.SocketError);
				}
			}
			catch (ObjectDisposedException)
			{
				return new SocketException(SocketError.OperationAborted);
			}
			catch (Exception ex)
			{
				return ex;
			}
			return null;
		}

		// Token: 0x06002F86 RID: 12166
		protected abstract void OnSucceed();

		// Token: 0x06002F87 RID: 12167 RVA: 0x000BBC84 File Offset: 0x000B9E84
		protected void Succeed()
		{
			this.OnSucceed();
			this.userArgs.FinishWrapperConnectSuccess(this.internalArgs.ConnectSocket, this.internalArgs.BytesTransferred, this.internalArgs.SocketFlags);
			this.internalArgs.Dispose();
		}

		// Token: 0x06002F88 RID: 12168
		protected abstract void OnFail(bool abortive);

		// Token: 0x06002F89 RID: 12169 RVA: 0x000BBCC3 File Offset: 0x000B9EC3
		private bool Fail(bool sync, Exception e)
		{
			if (sync)
			{
				this.SyncFail(e);
				return false;
			}
			this.AsyncFail(e);
			return true;
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000BBCDC File Offset: 0x000B9EDC
		private void SyncFail(Exception e)
		{
			this.OnFail(false);
			if (this.internalArgs != null)
			{
				this.internalArgs.Dispose();
			}
			SocketException ex = e as SocketException;
			if (ex != null)
			{
				this.userArgs.FinishConnectByNameSyncFailure(ex, 0, SocketFlags.None);
				return;
			}
			throw e;
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000BBD1D File Offset: 0x000B9F1D
		private void AsyncFail(Exception e)
		{
			this.OnFail(false);
			if (this.internalArgs != null)
			{
				this.internalArgs.Dispose();
			}
			this.userArgs.FinishOperationAsyncFailure(e, 0, SocketFlags.None);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000BBD48 File Offset: 0x000B9F48
		public void Cancel()
		{
			bool flag = false;
			object obj = this.lockObject;
			lock (obj)
			{
				switch (this.state)
				{
				case MultipleConnectAsync.State.NotStarted:
					flag = true;
					break;
				case MultipleConnectAsync.State.DnsQuery:
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.CallAsyncFail));
					flag = true;
					break;
				case MultipleConnectAsync.State.ConnectAttempt:
					flag = true;
					break;
				}
				this.state = MultipleConnectAsync.State.Canceled;
			}
			if (flag)
			{
				this.OnFail(true);
			}
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000BBDD0 File Offset: 0x000B9FD0
		private void CallAsyncFail(object ignored)
		{
			this.AsyncFail(new SocketException(SocketError.OperationAborted));
		}

		// Token: 0x06002F8E RID: 12174
		protected abstract IPAddress GetNextAddress(out Socket attemptSocket);

		// Token: 0x04002713 RID: 10003
		protected SocketAsyncEventArgs userArgs;

		// Token: 0x04002714 RID: 10004
		protected SocketAsyncEventArgs internalArgs;

		// Token: 0x04002715 RID: 10005
		protected DnsEndPoint endPoint;

		// Token: 0x04002716 RID: 10006
		protected IPAddress[] addressList;

		// Token: 0x04002717 RID: 10007
		protected int nextAddress;

		// Token: 0x04002718 RID: 10008
		private MultipleConnectAsync.State state;

		// Token: 0x04002719 RID: 10009
		private object lockObject = new object();

		// Token: 0x020005DC RID: 1500
		private enum State
		{
			// Token: 0x0400271B RID: 10011
			NotStarted,
			// Token: 0x0400271C RID: 10012
			DnsQuery,
			// Token: 0x0400271D RID: 10013
			ConnectAttempt,
			// Token: 0x0400271E RID: 10014
			Completed,
			// Token: 0x0400271F RID: 10015
			Canceled
		}
	}
}
