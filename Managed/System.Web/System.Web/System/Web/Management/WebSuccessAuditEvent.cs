using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about successful security events.</summary>
	// Token: 0x02000755 RID: 1877
	public class WebSuccessAuditEvent : WebAuditEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebSuccessAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004CE5 RID: 19685 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebSuccessAuditEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebSuccessAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		// Token: 0x06004CE6 RID: 19686 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebSuccessAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Increments the Audit Success Events Raised performance counter.</summary>
		// Token: 0x06004CE7 RID: 19687 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
