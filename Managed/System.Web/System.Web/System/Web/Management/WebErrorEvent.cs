using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about systemic errors.</summary>
	// Token: 0x02000756 RID: 1878
	public class WebErrorEvent : WebBaseErrorEvent
	{
		/// <summary>Initializes the <see cref="T:System.Web.Management.WebErrorEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004CE8 RID: 19688 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebErrorEvent(string message, object eventSource, int eventCode, Exception exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebErrorEvent" /> class using the supplied parameters. </summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004CE9 RID: 19689 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebErrorEvent(string message, object eventSource, int eventCode, int eventDetailCode, Exception exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the application request information.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebRequestInformation" /> object that contains information about the current request.</returns>
		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06004CEA RID: 19690 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebRequestInformation RequestInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the application-thread information.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebThreadInformation" /> object that contains information about the current thread.</returns>
		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebThreadInformation ThreadInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally to increment performance counters.</summary>
		// Token: 0x06004CEC RID: 19692 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
