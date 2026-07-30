using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Defines an interface that a control designer can implement to provide access to a data source.</summary>
	// Token: 0x02000089 RID: 137
	public interface IDataSourceProvider
	{
		/// <summary>Gets the selected data member from the selected data source.</summary>
		/// <returns>The selected data member from the selected data source, if the control allows the user to select an <see cref="T:System.ComponentModel.IListSource" /> (such as a <see cref="T:System.Data.DataSet" />) for the data source, and provides a DataMember property to select a particular list (or <see cref="T:System.Data.DataTable" />) within the data source.</returns>
		// Token: 0x06000456 RID: 1110
		IEnumerable GetResolvedSelectedDataSource();

		/// <summary>Gets a reference to the selected data source from the data source provider.</summary>
		/// <returns>The currently selected data source object of this data source provider.</returns>
		// Token: 0x06000457 RID: 1111
		object GetSelectedDataSource();
	}
}
