using System;

namespace System.Web.SessionState
{
	// Token: 0x020004AA RID: 1194
	[Serializable]
	internal class StateServerItem
	{
		// Token: 0x0600361C RID: 13852 RVA: 0x0008E527 File Offset: 0x0008C727
		public StateServerItem(int timeout)
			: this(null, null, timeout)
		{
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x0008E534 File Offset: 0x0008C734
		public StateServerItem(byte[] collection_data, byte[] sobjs_data, int timeout)
		{
			this.CollectionData = collection_data;
			this.StaticObjectsData = sobjs_data;
			this.Timeout = timeout;
			this.last_access = DateTime.UtcNow;
			this.Locked = false;
			this.LockId = int.MinValue;
			this.LockedTime = DateTime.MinValue;
			this.Action = SessionStateActions.None;
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x0008E58B File Offset: 0x0008C78B
		public void Touch()
		{
			this.last_access = DateTime.UtcNow;
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x0008E598 File Offset: 0x0008C798
		public bool IsAbandoned()
		{
			return this.last_access.AddMinutes((double)this.Timeout) < DateTime.UtcNow;
		}

		// Token: 0x04001D97 RID: 7575
		public byte[] CollectionData;

		// Token: 0x04001D98 RID: 7576
		public byte[] StaticObjectsData;

		// Token: 0x04001D99 RID: 7577
		private DateTime last_access;

		// Token: 0x04001D9A RID: 7578
		public int Timeout;

		// Token: 0x04001D9B RID: 7579
		public int LockId;

		// Token: 0x04001D9C RID: 7580
		public bool Locked;

		// Token: 0x04001D9D RID: 7581
		public DateTime LockedTime;

		// Token: 0x04001D9E RID: 7582
		public SessionStateActions Action;
	}
}
