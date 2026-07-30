using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Implements an event provider that logs ASP.NET health-monitoring events into the Windows Application Event Log. </summary>
	// Token: 0x02000745 RID: 1861
	public sealed class EventLogWebEventProvider : WebEventProvider
	{
		// Token: 0x06004C95 RID: 19605 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal EventLogWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Moves events from the provider's buffer into the event log.</summary>
		// Token: 0x06004C96 RID: 19606 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Flush()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004C97 RID: 19607 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004C98 RID: 19608 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
