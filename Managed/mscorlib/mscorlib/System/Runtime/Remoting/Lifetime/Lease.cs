using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000774 RID: 1908
	internal class Lease : MarshalByRefObject, ILease
	{
		// Token: 0x06004EB4 RID: 20148 RVA: 0x0011C194 File Offset: 0x0011A394
		public Lease()
		{
			this._currentState = LeaseState.Initial;
			this._initialLeaseTime = LifetimeServices.LeaseTime;
			this._renewOnCallTime = LifetimeServices.RenewOnCallTime;
			this._sponsorshipTimeout = LifetimeServices.SponsorshipTimeout;
			this._leaseExpireTime = DateTime.UtcNow + this._initialLeaseTime;
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004EB5 RID: 20149 RVA: 0x0011C1E5 File Offset: 0x0011A3E5
		public TimeSpan CurrentLeaseTime
		{
			get
			{
				return this._leaseExpireTime - DateTime.UtcNow;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004EB6 RID: 20150 RVA: 0x0011C1F7 File Offset: 0x0011A3F7
		public LeaseState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x0011C1FF File Offset: 0x0011A3FF
		public void Activate()
		{
			this._currentState = LeaseState.Active;
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06004EB8 RID: 20152 RVA: 0x0011C208 File Offset: 0x0011A408
		// (set) Token: 0x06004EB9 RID: 20153 RVA: 0x0011C210 File Offset: 0x0011A410
		public TimeSpan InitialLeaseTime
		{
			get
			{
				return this._initialLeaseTime;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("InitialLeaseTime property can only be set when the lease is in initial state; state is " + this._currentState + ".");
				}
				this._initialLeaseTime = value;
				this._leaseExpireTime = DateTime.UtcNow + this._initialLeaseTime;
				if (value == TimeSpan.Zero)
				{
					this._currentState = LeaseState.Null;
				}
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004EBA RID: 20154 RVA: 0x0011C277 File Offset: 0x0011A477
		// (set) Token: 0x06004EBB RID: 20155 RVA: 0x0011C27F File Offset: 0x0011A47F
		public TimeSpan RenewOnCallTime
		{
			get
			{
				return this._renewOnCallTime;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("RenewOnCallTime property can only be set when the lease is in initial state; state is " + this._currentState + ".");
				}
				this._renewOnCallTime = value;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004EBC RID: 20156 RVA: 0x0011C2B1 File Offset: 0x0011A4B1
		// (set) Token: 0x06004EBD RID: 20157 RVA: 0x0011C2B9 File Offset: 0x0011A4B9
		public TimeSpan SponsorshipTimeout
		{
			get
			{
				return this._sponsorshipTimeout;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("SponsorshipTimeout property can only be set when the lease is in initial state; state is " + this._currentState + ".");
				}
				this._sponsorshipTimeout = value;
			}
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x0011C2EB File Offset: 0x0011A4EB
		public void Register(ISponsor obj)
		{
			this.Register(obj, TimeSpan.Zero);
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x0011C2FC File Offset: 0x0011A4FC
		public void Register(ISponsor obj, TimeSpan renewalTime)
		{
			lock (this)
			{
				if (this._sponsors == null)
				{
					this._sponsors = new ArrayList();
				}
				this._sponsors.Add(obj);
			}
			if (renewalTime != TimeSpan.Zero)
			{
				this.Renew(renewalTime);
			}
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x0011C368 File Offset: 0x0011A568
		public TimeSpan Renew(TimeSpan renewalTime)
		{
			DateTime dateTime = DateTime.UtcNow + renewalTime;
			if (dateTime > this._leaseExpireTime)
			{
				this._leaseExpireTime = dateTime;
			}
			return this.CurrentLeaseTime;
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x0011C39C File Offset: 0x0011A59C
		public void Unregister(ISponsor obj)
		{
			lock (this)
			{
				if (this._sponsors != null)
				{
					for (int i = 0; i < this._sponsors.Count; i++)
					{
						if (this._sponsors[i] == obj)
						{
							this._sponsors.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x0011C410 File Offset: 0x0011A610
		internal void UpdateState()
		{
			if (this._currentState != LeaseState.Active)
			{
				return;
			}
			if (this.CurrentLeaseTime > TimeSpan.Zero)
			{
				return;
			}
			if (this._sponsors != null)
			{
				this._currentState = LeaseState.Renewing;
				lock (this)
				{
					this._renewingSponsors = new Queue(this._sponsors);
				}
				this.CheckNextSponsor();
				return;
			}
			this._currentState = LeaseState.Expired;
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x0011C490 File Offset: 0x0011A690
		private void CheckNextSponsor()
		{
			if (this._renewingSponsors.Count == 0)
			{
				this._currentState = LeaseState.Expired;
				this._renewingSponsors = null;
				return;
			}
			ISponsor sponsor = (ISponsor)this._renewingSponsors.Peek();
			this._renewalDelegate = new Lease.RenewalDelegate(sponsor.Renewal);
			IAsyncResult asyncResult = this._renewalDelegate.BeginInvoke(this, null, null);
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, new WaitOrTimerCallback(this.ProcessSponsorResponse), asyncResult, this._sponsorshipTimeout, true);
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x0011C50C File Offset: 0x0011A70C
		private void ProcessSponsorResponse(object state, bool timedOut)
		{
			if (!timedOut)
			{
				try
				{
					IAsyncResult asyncResult = (IAsyncResult)state;
					TimeSpan timeSpan = this._renewalDelegate.EndInvoke(asyncResult);
					if (timeSpan != TimeSpan.Zero)
					{
						this.Renew(timeSpan);
						this._currentState = LeaseState.Active;
						this._renewingSponsors = null;
						return;
					}
				}
				catch
				{
				}
			}
			this.Unregister((ISponsor)this._renewingSponsors.Dequeue());
			this.CheckNextSponsor();
		}

		// Token: 0x040029FF RID: 10751
		private DateTime _leaseExpireTime;

		// Token: 0x04002A00 RID: 10752
		private LeaseState _currentState;

		// Token: 0x04002A01 RID: 10753
		private TimeSpan _initialLeaseTime;

		// Token: 0x04002A02 RID: 10754
		private TimeSpan _renewOnCallTime;

		// Token: 0x04002A03 RID: 10755
		private TimeSpan _sponsorshipTimeout;

		// Token: 0x04002A04 RID: 10756
		private ArrayList _sponsors;

		// Token: 0x04002A05 RID: 10757
		private Queue _renewingSponsors;

		// Token: 0x04002A06 RID: 10758
		private Lease.RenewalDelegate _renewalDelegate;

		// Token: 0x02000775 RID: 1909
		// (Invoke) Token: 0x06004EC6 RID: 20166
		private delegate TimeSpan RenewalDelegate(ILease lease);
	}
}
