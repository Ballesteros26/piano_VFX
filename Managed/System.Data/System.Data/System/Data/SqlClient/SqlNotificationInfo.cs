using System;

namespace System.Data.SqlClient
{
	/// <summary>This enumeration provides additional information about the different notifications that can be received by the dependency event handler. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CE RID: 462
	public enum SqlNotificationInfo
	{
		/// <summary>One or more tables were truncated.</summary>
		// Token: 0x04000E78 RID: 3704
		Truncate,
		/// <summary>Data was changed by an INSERT statement.</summary>
		// Token: 0x04000E79 RID: 3705
		Insert,
		/// <summary>Data was changed by an UPDATE statement.</summary>
		// Token: 0x04000E7A RID: 3706
		Update,
		/// <summary>Data was changed by a DELETE statement.</summary>
		// Token: 0x04000E7B RID: 3707
		Delete,
		/// <summary>An underlying object related to the query was dropped.</summary>
		// Token: 0x04000E7C RID: 3708
		Drop,
		/// <summary>An underlying server object related to the query was modified.</summary>
		// Token: 0x04000E7D RID: 3709
		Alter,
		/// <summary>The server was restarted (notifications are sent during restart.).</summary>
		// Token: 0x04000E7E RID: 3710
		Restart,
		/// <summary>An internal server error occurred.</summary>
		// Token: 0x04000E7F RID: 3711
		Error,
		/// <summary>A SELECT statement that cannot be notified or was provided.</summary>
		// Token: 0x04000E80 RID: 3712
		Query,
		/// <summary>A statement was provided that cannot be notified (for example, an UPDATE statement).</summary>
		// Token: 0x04000E81 RID: 3713
		Invalid,
		/// <summary>The SET options were not set appropriately at subscription time.</summary>
		// Token: 0x04000E82 RID: 3714
		Options,
		/// <summary>The statement was executed under an isolation mode that was not valid (for example, Snapshot).</summary>
		// Token: 0x04000E83 RID: 3715
		Isolation,
		/// <summary>The SqlDependency object has expired.</summary>
		// Token: 0x04000E84 RID: 3716
		Expired,
		/// <summary>Fires as a result of server resource pressure.</summary>
		// Token: 0x04000E85 RID: 3717
		Resource,
		/// <summary>A previous statement has caused query notifications to fire under the current transaction.</summary>
		// Token: 0x04000E86 RID: 3718
		PreviousFire,
		/// <summary>The subscribing query causes the number of templates on one of the target tables to exceed the maximum allowable limit.</summary>
		// Token: 0x04000E87 RID: 3719
		TemplateLimit,
		/// <summary>Used to distinguish the server-side cause for a query notification firing.</summary>
		// Token: 0x04000E88 RID: 3720
		Merge,
		/// <summary>Used when the info option sent by the server was not recognized by the client.</summary>
		// Token: 0x04000E89 RID: 3721
		Unknown = -1,
		/// <summary>The SqlDependency object already fired, and new commands cannot be added to it.</summary>
		// Token: 0x04000E8A RID: 3722
		AlreadyChanged = -2
	}
}
