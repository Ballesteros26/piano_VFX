using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about ASP.NET authentication failures. </summary>
	// Token: 0x02000752 RID: 1874
	public class WebAuthenticationFailureAuditEvent : WebFailureAuditEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebAuthenticationFailureAuditEvent" /> class with the specified event parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		/// <param name="nameToAuthenticate">The name of the user to authenticate. </param>
		// Token: 0x06004CDC RID: 19676 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebAuthenticationFailureAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode, string nameToAuthenticate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebAuthenticationFailureAuditEvent" /> class with the specified event parameters.</summary>
		/// <param name="message">The event description. </param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value associated with the event.</param>
		/// <param name="nameToAuthenticate">The name of the user to authenticate.</param>
		// Token: 0x06004CDD RID: 19677 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal WebAuthenticationFailureAuditEvent(string message, object eventSource, int eventCode, string nameToAuthenticate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the name of the user to authenticate.</summary>
		/// <returns>The name of the user to authenticate.</returns>
		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06004CDE RID: 19678 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string NameToAuthenticate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
