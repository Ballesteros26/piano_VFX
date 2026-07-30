using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies whether a <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> or <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control retrieves data as a <see cref="T:System.Data.IDataReader" /> or <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x02000311 RID: 785
	public enum SqlDataSourceMode
	{
		/// <summary>Retrieves data from the underlying data storage as an <see cref="T:System.Data.IDataReader" /></summary>
		// Token: 0x0400176A RID: 5994
		DataReader,
		/// <summary>Retrieves data from the underlying data storage into a <see cref="T:System.Data.DataSet" /> structure.</summary>
		// Token: 0x0400176B RID: 5995
		DataSet
	}
}
