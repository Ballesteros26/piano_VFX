using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Serves as the base class for all the health-monitoring error events.</summary>
	// Token: 0x020006E2 RID: 1762
	public class WebBaseErrorEvent : WebManagementEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseErrorEvent" /> class.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="e">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004AB5 RID: 19125 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebBaseErrorEvent(string message, object eventSource, int eventCode, Exception e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseErrorEvent" /> class.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The detailed identifier for the event.</param>
		/// <param name="e">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004AB6 RID: 19126 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebBaseErrorEvent(string message, object eventSource, int eventCode, int eventDetailCode, Exception e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> associated with the error. </summary>
		/// <returns>The <see cref="T:System.Exception" /> associated with the error.</returns>
		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public Exception ErrorException
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Increments the event-error performance-related counters.</summary>
		// Token: 0x06004AB8 RID: 19128 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
