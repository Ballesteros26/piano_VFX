using System;

namespace System.Web.Management
{
	/// <summary>Describes the session-state type used when installing a session-state database provider.</summary>
	// Token: 0x0200052D RID: 1325
	public enum SessionStateType
	{
		/// <summary>Session state data is stored in tempdb, and stored procedures are placed in the "ASPState" database. Session state data will not survive a restart of SQL Server.</summary>
		// Token: 0x04001F67 RID: 8039
		Temporary,
		/// <summary>Session-state data and stored procedures are placed in the "ASPState" database. Session-state data will survive a restart of the database server.</summary>
		// Token: 0x04001F68 RID: 8040
		Persisted,
		/// <summary>Session-state data and stored procedures are placed in a custom data store.</summary>
		// Token: 0x04001F69 RID: 8041
		Custom
	}
}
