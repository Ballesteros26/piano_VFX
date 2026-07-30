using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Serves as the base class for all ASP.NET health-monitoring audit events. </summary>
	// Token: 0x02000751 RID: 1873
	public class WebAuditEvent : WebManagementEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004CD9 RID: 19673 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebAuditEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebAuditEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		// Token: 0x06004CDA RID: 19674 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Get the information associated with the Web request.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebRequestInformation" /> that contains the information associated with the Web request.</returns>
		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebRequestInformation RequestInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
