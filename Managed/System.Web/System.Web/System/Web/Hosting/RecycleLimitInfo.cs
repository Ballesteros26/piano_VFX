using System;
using Unity;

namespace System.Web.Hosting
{
	// Token: 0x02000763 RID: 1891
	public sealed class RecycleLimitInfo
	{
		// Token: 0x06004D2A RID: 19754 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public RecycleLimitInfo(long currentPrivateBytes, long recycleLimit, RecycleLimitNotificationFrequency recycleLimitNearFrequency)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x06004D2B RID: 19755 RVA: 0x000CB298 File Offset: 0x000C9498
		public long CurrentPrivateBytes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x06004D2C RID: 19756 RVA: 0x000CB2B4 File Offset: 0x000C94B4
		public long RecycleLimit
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x06004D2D RID: 19757 RVA: 0x000CB2D0 File Offset: 0x000C94D0
		// (set) Token: 0x06004D2E RID: 19758 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool RequestGC
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x06004D2F RID: 19759 RVA: 0x000CB2EC File Offset: 0x000C94EC
		public RecycleLimitNotificationFrequency TrimFrequency
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return RecycleLimitNotificationFrequency.High;
			}
		}
	}
}
