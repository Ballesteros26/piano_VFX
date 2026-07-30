using System;

namespace System.Data.SqlClient
{
	/// <summary>Specifies a value for <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.ApplicationIntent" />. Possible values are ReadWrite and ReadOnly.</summary>
	// Token: 0x02000140 RID: 320
	public enum ApplicationIntent
	{
		/// <summary>The application workload type when connecting to a server is read write.</summary>
		// Token: 0x04000A93 RID: 2707
		ReadWrite,
		/// <summary>The application workload type when connecting to a server is read only.</summary>
		// Token: 0x04000A94 RID: 2708
		ReadOnly
	}
}
