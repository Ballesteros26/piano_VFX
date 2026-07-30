using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Implements an event provider that uses templates to define and format e-mails it sends for event notifications.</summary>
	// Token: 0x0200074E RID: 1870
	public sealed class TemplatedMailWebEventProvider : MailWebEventProvider
	{
		// Token: 0x06004CD0 RID: 19664 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal TemplatedMailWebEventProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the event notification object that provides the event information used by the e-mail template.</summary>
		/// <returns>The <see cref="T:System.Web.Management.MailEventNotificationInfo" /> object currently being processed.</returns>
		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static MailEventNotificationInfo CurrentNotification
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
