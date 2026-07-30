using System;

namespace System.Data
{
	/// <summary>Specifies how a command string is interpreted.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000053 RID: 83
	public enum CommandType
	{
		/// <summary>An SQL text command. (Default.) </summary>
		// Token: 0x040004F2 RID: 1266
		Text = 1,
		/// <summary>The name of a stored procedure.</summary>
		// Token: 0x040004F3 RID: 1267
		StoredProcedure = 4,
		/// <summary>The name of a table.</summary>
		// Token: 0x040004F4 RID: 1268
		TableDirect = 512
	}
}
