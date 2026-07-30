using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000776 RID: 1910
	internal class LeaseManager
	{
		// Token: 0x06004EC9 RID: 20169 RVA: 0x0011C588 File Offset: 0x0011A788
		public void SetPollTime(TimeSpan timeSpan)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				if (this._timer != null)
				{
					this._timer.Change(timeSpan, timeSpan);
				}
			}
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x0011C5E0 File Offset: 0x0011A7E0
		public void TrackLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				identity.Lease.Activate();
				this._objects.Add(identity);
				if (this._timer == null)
				{
					this.StartManager();
				}
			}
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x0011C648 File Offset: 0x0011A848
		public void StopTrackingLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				this._objects.Remove(identity);
			}
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x0011C694 File Offset: 0x0011A894
		public void StartManager()
		{
			this._timer = new Timer(new TimerCallback(this.ManageLeases), null, LifetimeServices.LeaseManagerPollTime, LifetimeServices.LeaseManagerPollTime);
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x0011C6B8 File Offset: 0x0011A8B8
		public void StopManager()
		{
			Timer timer = this._timer;
			this._timer = null;
			if (timer != null)
			{
				timer.Dispose();
			}
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x0011C6DC File Offset: 0x0011A8DC
		public void ManageLeases(object state)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				int i = 0;
				while (i < this._objects.Count)
				{
					ServerIdentity serverIdentity = (ServerIdentity)this._objects[i];
					serverIdentity.Lease.UpdateState();
					if (serverIdentity.Lease.CurrentState == LeaseState.Expired)
					{
						this._objects.RemoveAt(i);
						serverIdentity.OnLifetimeExpired();
					}
					else
					{
						i++;
					}
				}
				if (this._objects.Count == 0)
				{
					this.StopManager();
				}
			}
		}

		// Token: 0x04002A07 RID: 10759
		private ArrayList _objects = new ArrayList();

		// Token: 0x04002A08 RID: 10760
		private Timer _timer;
	}
}
