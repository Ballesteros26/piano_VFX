using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines the base class for events providing Web-request information.</summary>
	// Token: 0x0200075A RID: 1882
	public class WebRequestEvent : WebManagementEvent
	{
		/// <summary>Initializes the <see cref="T:System.Web.Management.WebRequestEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The message associated with the event.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The <see cref="T:System.Web.Management.WebEventCodes" /> code associated with the event. It must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004CFC RID: 19708 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebRequestEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes the <see cref="T:System.Web.Management.WebRequestEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The message associated with the event.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The <see cref="T:System.Web.Management.WebEventCodes" /> code associated with the event. It must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> detail code associated with the event.</param>
		// Token: 0x06004CFD RID: 19709 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebRequestEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the information associated with the Web-application request.</summary>
		/// <returns>The information associated with the Web-application request.</returns>
		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06004CFE RID: 19710 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebRequestInformation RequestInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally to increment the performance counters.</summary>
		// Token: 0x06004CFF RID: 19711 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
