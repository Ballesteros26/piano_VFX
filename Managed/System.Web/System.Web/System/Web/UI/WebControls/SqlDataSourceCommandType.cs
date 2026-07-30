using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Describes the type of SQL command used by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> and <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> controls when performing a database operation.</summary>
	// Token: 0x0200030E RID: 782
	public enum SqlDataSourceCommandType
	{
		/// <summary>The text contained in a corresponding text property is a SQL query or command.</summary>
		// Token: 0x04001766 RID: 5990
		Text,
		/// <summary>The text contained in a corresponding text property is the name of a stored procedure.</summary>
		// Token: 0x04001767 RID: 5991
		StoredProcedure
	}
}
