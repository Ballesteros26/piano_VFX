using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Implements an event provider that maps ASP.NET health-monitoring events to Windows Management Instrumentation (WMI) events.</summary>
	// Token: 0x0200075C RID: 1884
	public class WmiWebEventProvider : WebEventProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WmiWebEventProvider" /> class.</summary>
		// Token: 0x06004D03 RID: 19715 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WmiWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all events from the provider's buffer.</summary>
		// Token: 0x06004D04 RID: 19716 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Flush()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the event passed to the provider.</summary>
		/// <param name="eventRaised">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to process. </param>
		/// <exception cref="T:System.Web.HttpException">The event could not be raised.</exception>
		// Token: 0x06004D05 RID: 19717 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs tasks associated with shutting down the provider.</summary>
		// Token: 0x06004D06 RID: 19718 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void Shutdown()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
