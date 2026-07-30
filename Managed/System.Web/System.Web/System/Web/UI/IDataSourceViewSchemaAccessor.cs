using System;

namespace System.Web.UI
{
	/// <summary>Allows a type converter to access schema information stored on an object.</summary>
	// Token: 0x02000172 RID: 370
	public interface IDataSourceViewSchemaAccessor
	{
		/// <summary>When implemented, gets or sets the schema associated with the object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the schema.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000F6C RID: 3948
		// (set) Token: 0x06000F6D RID: 3949
		object DataSourceViewSchema { get; set; }
	}
}
