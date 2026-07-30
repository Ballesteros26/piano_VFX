using System;
using System.Web.UI;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides Web-application view-state-related-failure information.  </summary>
	// Token: 0x0200075B RID: 1883
	public class WebViewStateFailureAuditEvent : WebFailureAuditEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebViewStateFailureAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		/// <param name="viewStateException">The <see cref="T:System.Web.UI.ViewStateException" /> caused by the failure.</param>
		// Token: 0x06004D00 RID: 19712 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebViewStateFailureAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode, ViewStateException viewStateException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes the <see cref="T:System.Web.Management.WebViewStateFailureAuditEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="viewStateException">The <see cref="T:System.Web.UI.ViewStateException" /> caused by the failure.</param>
		// Token: 0x06004D01 RID: 19713 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebViewStateFailureAuditEvent(string message, object eventSource, int eventCode, ViewStateException viewStateException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the exception caused by the failure.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ViewStateException" /> caused by the failure.</returns>
		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x06004D02 RID: 19714 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ViewStateException ViewStateException
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
