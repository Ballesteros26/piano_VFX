using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides basic functionality for describing the structure of a data source at design time.</summary>
	// Token: 0x0200008A RID: 138
	public interface IDataSourceSchema
	{
		/// <summary>Gets an array of schema descriptors for views contained in the data source.</summary>
		/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> objects.</returns>
		// Token: 0x06000458 RID: 1112
		IDataSourceViewSchema[] GetViews();
	}
}
