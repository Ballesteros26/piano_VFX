using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000777 RID: 1911
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x06004ED0 RID: 20176 RVA: 0x0011C797 File Offset: 0x0011A997
		public LeaseSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x0011C7A6 File Offset: 0x0011A9A6
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.RenewLease(msg);
			return this._nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x0011C7BB File Offset: 0x0011A9BB
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.RenewLease(msg);
			return this._nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x0011C7D4 File Offset: 0x0011A9D4
		private void RenewLease(IMessage msg)
		{
			ILease lease = ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).Lease;
			if (lease != null && lease.CurrentLeaseTime < lease.RenewOnCallTime)
			{
				lease.Renew(lease.RenewOnCallTime);
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004ED4 RID: 20180 RVA: 0x0011C815 File Offset: 0x0011AA15
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04002A09 RID: 10761
		private IMessageSink _nextSink;
	}
}
