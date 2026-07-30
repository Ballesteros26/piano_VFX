using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mono.Net.Security
{
	// Token: 0x02000066 RID: 102
	internal abstract class AsyncProtocolRequest
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000059E6 File Offset: 0x00003BE6
		public MobileAuthenticatedStream Parent { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000059EE File Offset: 0x00003BEE
		public bool RunSynchronously { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000059F6 File Offset: 0x00003BF6
		public int ID
		{
			get
			{
				return ++AsyncProtocolRequest.next_id;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005A05 File Offset: 0x00003C05
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005A12 File Offset: 0x00003C12
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00005A1A File Offset: 0x00003C1A
		public int UserResult { get; protected set; }

		// Token: 0x060001DF RID: 479 RVA: 0x00005A23 File Offset: 0x00003C23
		public AsyncProtocolRequest(MobileAuthenticatedStream parent, bool sync)
		{
			this.Parent = parent;
			this.RunSynchronously = sync;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("MONO_TLS_DEBUG")]
		protected void Debug(string message, params object[] args)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005A44 File Offset: 0x00003C44
		internal void RequestRead(int size)
		{
			object obj = this.locker;
			lock (obj)
			{
				this.RequestedSize += size;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005A8C File Offset: 0x00003C8C
		internal void RequestWrite()
		{
			this.WriteRequested = 1;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005A98 File Offset: 0x00003C98
		internal async Task<AsyncProtocolResult> StartOperation(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.Started, 1, 0) != 0)
			{
				throw new InvalidOperationException();
			}
			AsyncProtocolResult asyncProtocolResult;
			try
			{
				await this.ProcessOperation(cancellationToken).ConfigureAwait(false);
				asyncProtocolResult = new AsyncProtocolResult(this.UserResult);
			}
			catch (Exception ex)
			{
				asyncProtocolResult = new AsyncProtocolResult(this.Parent.SetException(MobileAuthenticatedStream.GetSSPIException(ex)));
			}
			return asyncProtocolResult;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005AE8 File Offset: 0x00003CE8
		private async Task ProcessOperation(CancellationToken cancellationToken)
		{
			AsyncOperationStatus newStatus;
			for (AsyncOperationStatus status = AsyncOperationStatus.Initialize; status != AsyncOperationStatus.Complete; status = newStatus)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int? num = await this.InnerRead(cancellationToken).ConfigureAwait(false);
				if (num != null)
				{
					if (num == 0)
					{
						status = AsyncOperationStatus.ReadDone;
					}
					else if (num < 0)
					{
						throw new IOException("Remote prematurely closed connection.");
					}
				}
				if (status > AsyncOperationStatus.ReadDone)
				{
					throw new InvalidOperationException();
				}
				newStatus = this.Run(status);
				if (Interlocked.Exchange(ref this.WriteRequested, 0) != 0)
				{
					await this.Parent.InnerWrite(this.RunSynchronously, cancellationToken).ConfigureAwait(false);
				}
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005B38 File Offset: 0x00003D38
		private async Task<int?> InnerRead(CancellationToken cancellationToken)
		{
			int? totalRead = null;
			int num2;
			for (int requestedSize = Interlocked.Exchange(ref this.RequestedSize, 0); requestedSize > 0; requestedSize += num2)
			{
				int num = await this.Parent.InnerRead(this.RunSynchronously, requestedSize, cancellationToken).ConfigureAwait(false);
				if (num <= 0)
				{
					return new int?(num);
				}
				if (num > requestedSize)
				{
					throw new InvalidOperationException();
				}
				totalRead += num;
				requestedSize -= num;
				num2 = Interlocked.Exchange(ref this.RequestedSize, 0);
			}
			return totalRead;
		}

		// Token: 0x060001E6 RID: 486
		protected abstract AsyncOperationStatus Run(AsyncOperationStatus status);

		// Token: 0x060001E7 RID: 487 RVA: 0x00005B85 File Offset: 0x00003D85
		public override string ToString()
		{
			return string.Format("[{0}]", this.Name);
		}

		// Token: 0x0400078E RID: 1934
		private int Started;

		// Token: 0x0400078F RID: 1935
		private int RequestedSize;

		// Token: 0x04000790 RID: 1936
		private int WriteRequested;

		// Token: 0x04000791 RID: 1937
		private readonly object locker = new object();

		// Token: 0x04000792 RID: 1938
		private static int next_id;
	}
}
