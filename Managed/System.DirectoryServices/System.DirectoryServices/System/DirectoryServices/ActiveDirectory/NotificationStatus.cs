using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Represents the notification status of a replication connection. </summary>
	// Token: 0x02000067 RID: 103
	public enum NotificationStatus
	{
		/// <summary>Do not send notifications.</summary>
		// Token: 0x0400012E RID: 302
		NoNotification,
		/// <summary>Send notifications only for intra-site connections.</summary>
		// Token: 0x0400012F RID: 303
		IntraSiteOnly,
		/// <summary>Always send notifications.</summary>
		// Token: 0x04000130 RID: 304
		NotificationAlways
	}
}
