using System;

namespace System.Web.UI.Design
{
	/// <summary>Defines a set of methods and properties used to examine a data source.</summary>
	// Token: 0x0200008B RID: 139
	public interface IDataSourceViewSchema
	{
		/// <summary>Gets the name of the view.</summary>
		/// <returns>The name of the view.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000459 RID: 1113
		string Name { get; }

		/// <summary>Gets an array representing the child views contained in the current view.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> objects that represent the child views contained in the current view.</returns>
		// Token: 0x0600045A RID: 1114
		IDataSourceViewSchema[] GetChildren();

		/// <summary>Gets an array containing information about each field in the data source.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceFieldSchema" /> objects representing each of the fields in the data source.</returns>
		// Token: 0x0600045B RID: 1115
		IDataSourceFieldSchema[] GetFields();
	}
}
