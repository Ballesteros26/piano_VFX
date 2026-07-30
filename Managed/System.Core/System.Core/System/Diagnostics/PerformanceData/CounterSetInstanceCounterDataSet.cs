using System;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.PerformanceData
{
	/// <summary>Contains the collection of counter values.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000382 RID: 898
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CounterSetInstanceCounterDataSet : IDisposable
	{
		// Token: 0x06001AAA RID: 6826 RVA: 0x0000220F File Offset: 0x0000040F
		internal CounterSetInstanceCounterDataSet()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000560B4 File Offset: 0x000542B4
		public CounterData get_Item(int counterId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Accesses a counter value in the collection by using the specified counter name.</summary>
		/// <returns>The counter data.</returns>
		/// <param name="counterName">Name of the counter. This is the name that you used when you added the counter to the counter set.</param>
		// Token: 0x170004D3 RID: 1235
		public CounterData this[string counterName]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Releases all unmanaged resources used by this object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AAD RID: 6829 RVA: 0x0000220F File Offset: 0x0000040F
		[SecurityCritical]
		public void Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
