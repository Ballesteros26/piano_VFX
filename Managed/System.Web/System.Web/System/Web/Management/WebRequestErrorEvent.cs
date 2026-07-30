using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines the event that carries information about Web-request errors.</summary>
	// Token: 0x020006E1 RID: 1761
	public class WebRequestErrorEvent : WebBaseErrorEvent
	{
		/// <summary>Initializes the <see cref="T:System.Web.Management.WebRequestErrorEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The identifier associated with the event. It must be greater than the <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" /> field constant.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> associated with the error.</param>
		// Token: 0x06004AB0 RID: 19120 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebRequestErrorEvent(string message, object eventSource, int eventCode, Exception exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes the <see cref="T:System.Web.Management.WebRequestErrorEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The identifier associated with the event. It must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" /> field constant.</param>
		/// <param name="eventDetailCode">The event detail code identifier.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004AB1 RID: 19121 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebRequestErrorEvent(string message, object eventSource, int eventCode, int eventDetailCode, Exception exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the application request information.</summary>
		/// <returns>The application request information.</returns>
		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06004AB2 RID: 19122 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebRequestInformation RequestInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the application thread information.</summary>
		/// <returns>The application thread information.</returns>
		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06004AB3 RID: 19123 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebThreadInformation ThreadInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally to increment the relevant performance counters.</summary>
		// Token: 0x06004AB4 RID: 19124 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
