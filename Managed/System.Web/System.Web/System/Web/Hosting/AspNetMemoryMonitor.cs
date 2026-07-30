using System;
using Unity;

namespace System.Web.Hosting
{
	// Token: 0x02000761 RID: 1889
	public sealed class AspNetMemoryMonitor : IDisposable, IObservable<LowPhysicalMemoryInfo>, IObservable<RecycleLimitInfo>, IApplicationMonitor
	{
		// Token: 0x06004D1B RID: 19739 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal AspNetMemoryMonitor()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x06004D1C RID: 19740 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		// (set) Token: 0x06004D1D RID: 19741 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public IObserver<LowPhysicalMemoryInfo> DefaultLowPhysicalMemoryObserver
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		// (set) Token: 0x06004D1F RID: 19743 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public IObserver<RecycleLimitInfo> DefaultRecycleLimitObserver
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Start()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Stop()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IDisposable Subscribe(IObserver<LowPhysicalMemoryInfo> observer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IDisposable Subscribe(IObserver<RecycleLimitInfo> observer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
