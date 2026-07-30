using System;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.PerformanceData
{
	/// <summary>Contains the raw data for a counter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200037D RID: 893
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CounterData
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x0000220F File Offset: 0x0000040F
		internal CounterData()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets or gets the raw counter data.</summary>
		/// <returns>The raw counter data.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0005635C File Offset: 0x0005455C
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x0000220F File Offset: 0x0000040F
		public long RawValue
		{
			[SecurityCritical]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
			[SecurityCritical]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Sets or gets the counter data.</summary>
		/// <returns>The counter data.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00056378 File Offset: 0x00054578
		// (set) Token: 0x06001A9D RID: 6813 RVA: 0x0000220F File Offset: 0x0000040F
		public long Value
		{
			[SecurityCritical]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
			[SecurityCritical]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Decrements the counter value by 1.</summary>
		// Token: 0x06001A9E RID: 6814 RVA: 0x0000220F File Offset: 0x0000040F
		[SecurityCritical]
		public void Decrement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Increments the counter value by 1.</summary>
		// Token: 0x06001A9F RID: 6815 RVA: 0x0000220F File Offset: 0x0000040F
		[SecurityCritical]
		public void Increment()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Increments the counter value by the specified amount.</summary>
		/// <param name="value">The amount by which to increment the counter value. The increment value can be positive or negative.</param>
		// Token: 0x06001AA0 RID: 6816 RVA: 0x0000220F File Offset: 0x0000040F
		[SecurityCritical]
		public void IncrementBy(long value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
