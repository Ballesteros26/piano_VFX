using System;

namespace System.Data.SqlClient
{
	/// <summary>Describes the different notification types that can be received by an <see cref="T:System.Data.SqlClient.OnChangeEventHandler" /> event handler through the <see cref="T:System.Data.SqlClient.SqlNotificationEventArgs" /> parameter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D0 RID: 464
	public enum SqlNotificationType
	{
		/// <summary>Data on the server being monitored changed. Use the <see cref="T:System.Data.SqlClient.SqlNotificationInfo" /> item to determine the details of the change.</summary>
		// Token: 0x04000E98 RID: 3736
		Change,
		/// <summary>There was a failure to create a notification subscription. Use the <see cref="T:System.Data.SqlClient.SqlNotificationEventArgs" /> object's <see cref="T:System.Data.SqlClient.SqlNotificationInfo" /> item to determine the cause of the failure.</summary>
		// Token: 0x04000E99 RID: 3737
		Subscribe,
		/// <summary>Used when the type option sent by the server was not recognized by the client.</summary>
		// Token: 0x04000E9A RID: 3738
		Unknown = -1
	}
}
