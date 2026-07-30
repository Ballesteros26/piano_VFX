using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines those health-monitoring events raised at a periodic interval.</summary>
	// Token: 0x02000758 RID: 1880
	public class WebHeartbeatEvent : WebManagementEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebHeartbeatEvent" /> class with the given message and event code.</summary>
		/// <param name="message">The description of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004CEF RID: 19695 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebHeartbeatEvent(string message, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides key information about the running process.</summary>
		/// <returns>The <see cref="T:System.Web.Management.WebProcessStatistics" /> for the running process.</returns>
		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebProcessStatistics ProcessStatistics
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
