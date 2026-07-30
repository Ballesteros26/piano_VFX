using System;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.PerformanceData
{
	/// <summary>Creates an instance of the logical counters defined in the <see cref="T:System.Diagnostics.PerformanceData.CounterSet" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000381 RID: 897
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CounterSetInstance : IDisposable
	{
		// Token: 0x06001AA7 RID: 6823 RVA: 0x0000220F File Offset: 0x0000040F
		internal CounterSetInstance()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the collection of counter data for the counter set instance.</summary>
		/// <returns>A collection of the counter data contained in the counter set instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x000560B4 File Offset: 0x000542B4
		public CounterSetInstanceCounterDataSet Counters
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Releases all unmanaged resources used by this object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AA9 RID: 6825 RVA: 0x0000220F File Offset: 0x0000040F
		[SecurityCritical]
		public void Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
