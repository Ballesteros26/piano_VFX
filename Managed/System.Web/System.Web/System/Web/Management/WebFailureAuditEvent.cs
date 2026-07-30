using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about security failures. </summary>
	// Token: 0x02000753 RID: 1875
	public class WebFailureAuditEvent : WebAuditEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebFailureAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004CDF RID: 19679 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebFailureAuditEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebFailureAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		// Token: 0x06004CE0 RID: 19680 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebFailureAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Increments the Audit Failure Events Raised performance counter.</summary>
		// Token: 0x06004CE1 RID: 19681 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
