using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Implements an event provider that sends ASP.NET health-monitoring events as trace messages.</summary>
	// Token: 0x0200074F RID: 1871
	public sealed class TraceWebEventProvider : WebEventProvider
	{
		// Token: 0x06004CD2 RID: 19666 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal TraceWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all events from the provider's buffer.</summary>
		// Token: 0x06004CD3 RID: 19667 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Flush()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process.</param>
		// Token: 0x06004CD4 RID: 19668 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004CD5 RID: 19669 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
