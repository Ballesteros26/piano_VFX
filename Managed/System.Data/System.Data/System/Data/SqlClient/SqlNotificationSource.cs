using System;

namespace System.Data.SqlClient
{
	/// <summary>Indicates the source of the notification received by the dependency event handler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CF RID: 463
	public enum SqlNotificationSource
	{
		/// <summary>Data has changed; for example, an insert, update, delete, or truncate operation occurred.</summary>
		// Token: 0x04000E8C RID: 3724
		Data,
		/// <summary>The subscription time-out expired.</summary>
		// Token: 0x04000E8D RID: 3725
		Timeout,
		/// <summary>A database object changed; for example, an underlying object related to the query was dropped or modified.</summary>
		// Token: 0x04000E8E RID: 3726
		Object,
		/// <summary>The database state changed; for example, the database related to the query was dropped or detached.</summary>
		// Token: 0x04000E8F RID: 3727
		Database,
		/// <summary>A system-related event occurred. For example, there was an internal error, the server was restarted, or resource pressure caused the invalidation.</summary>
		// Token: 0x04000E90 RID: 3728
		System,
		/// <summary>The Transact-SQL statement is not valid for notifications; for example, a SELECT statement that could not be notified or a non-SELECT statement was executed.</summary>
		// Token: 0x04000E91 RID: 3729
		Statement,
		/// <summary>The run-time environment was not compatible with notifications; for example, the isolation level was set to snapshot, or one or more SET options are not compatible.</summary>
		// Token: 0x04000E92 RID: 3730
		Environment,
		/// <summary>A run-time error occurred during execution.</summary>
		// Token: 0x04000E93 RID: 3731
		Execution,
		/// <summary>Internal only; not intended to be used in your code.</summary>
		// Token: 0x04000E94 RID: 3732
		Owner,
		/// <summary>Used when the source option sent by the server was not recognized by the client. </summary>
		// Token: 0x04000E95 RID: 3733
		Unknown = -1,
		/// <summary>A client-initiated notification occurred, such as a client-side time-out or as a result of attempting to add a command to a dependency that has already fired.</summary>
		// Token: 0x04000E96 RID: 3734
		Client = -2
	}
}
