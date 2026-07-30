using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines properties that are shared by data-bound controls.</summary>
	// Token: 0x020002D2 RID: 722
	public interface IDataBoundControl
	{
		/// <summary>Gets or sets the ID of the data source control from which the data-bound control retrieves a list of data items.</summary>
		/// <returns>The ID of the data source control that contains the list of data items that the data-bound control retrieves.</returns>
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001B62 RID: 7010
		// (set) Token: 0x06001B63 RID: 7011
		string DataSourceID { get; set; }

		/// <summary>Gets the data source object from which the data-bound control retrieves a list of data items.</summary>
		/// <returns>The data source object that contains the list of data items that the data-bound control retrieves. </returns>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001B64 RID: 7012
		IDataSource DataSourceObject { get; }

		/// <summary>Gets or sets the object from which the data-bound control retrieves a list of data items.</summary>
		/// <returns>The object that contains the list of data that the data-bound control retrieves.</returns>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001B65 RID: 7013
		// (set) Token: 0x06001B66 RID: 7014
		object DataSource { get; set; }

		/// <summary>Gets or sets an array that contains the names of the primary-key fields of the items that are displayed in a data-bound control.</summary>
		/// <returns>An array that contains the names of the primary-key fields of the items that are displayed in a data-bound control.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001B67 RID: 7015
		// (set) Token: 0x06001B68 RID: 7016
		string[] DataKeyNames { get; set; }

		/// <summary>Gets or sets the name of the list of data that the data-bound control binds to when the data source contains more than one list of data items.</summary>
		/// <returns>The name of the list of data that the data-bound control binds to.</returns>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001B69 RID: 7017
		// (set) Token: 0x06001B6A RID: 7018
		string DataMember { get; set; }
	}
}
