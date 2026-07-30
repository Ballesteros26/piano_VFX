using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines the base class for events that carry application and process information.</summary>
	// Token: 0x020006E3 RID: 1763
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class WebManagementEvent : WebBaseEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebManagementEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		// Token: 0x06004AB9 RID: 19129 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebManagementEvent(string message, object eventSource, int eventCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebManagementEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		// Token: 0x06004ABA RID: 19130 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebManagementEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets information about the ASP.NET application-hosting process.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebProcessInformation" /> object that contains information about the process.</returns>
		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06004ABB RID: 19131 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebProcessInformation ProcessInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
