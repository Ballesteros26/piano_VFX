using System;
using System.Collections;

namespace System.Data
{
	/// <summary>Contains a collection of TableMapping objects, and is implemented by the <see cref="T:System.Data.Common.DataTableMappingCollection" />, which is used in common by .NET Framework data providers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CF RID: 207
	public interface ITableMappingCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the instance of <see cref="T:System.Data.ITableMapping" /> with the specified <see cref="P:System.Data.ITableMapping.SourceTable" /> name.</summary>
		/// <returns>The instance of <see cref="T:System.Data.ITableMapping" /> with the specified SourceTable name.</returns>
		/// <param name="index">The SourceTable name of the <see cref="T:System.Data.ITableMapping" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021F RID: 543
		object this[string index] { get; set; }

		/// <summary>Adds a table mapping to the collection.</summary>
		/// <returns>A reference to the newly-mapped <see cref="T:System.Data.ITableMapping" /> object.</returns>
		/// <param name="sourceTableName">The case-sensitive name of the source table. </param>
		/// <param name="dataSetTableName">The name of the <see cref="T:System.Data.DataSet" /> table. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC7 RID: 3015
		ITableMapping Add(string sourceTableName, string dataSetTableName);

		/// <summary>Gets a value indicating whether the collection contains a table mapping with the specified source table name.</summary>
		/// <returns>true if a table mapping with the specified source table name exists, otherwise false.</returns>
		/// <param name="sourceTableName">The case-sensitive name of the source table. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC8 RID: 3016
		bool Contains(string sourceTableName);

		/// <summary>Gets the TableMapping object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <returns>The TableMapping object with the specified DataSet table name.</returns>
		/// <param name="dataSetTableName">The name of the DataSet table within the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC9 RID: 3017
		ITableMapping GetByDataSetTable(string dataSetTableName);

		/// <summary>Gets the location of the <see cref="T:System.Data.ITableMapping" /> object within the collection.</summary>
		/// <returns>The zero-based location of the <see cref="T:System.Data.ITableMapping" /> object within the collection.</returns>
		/// <param name="sourceTableName">The case-sensitive name of the source table. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BCA RID: 3018
		int IndexOf(string sourceTableName);

		/// <summary>Removes the <see cref="T:System.Data.ITableMapping" /> object with the specified <see cref="P:System.Data.ITableMapping.SourceTable" /> name from the collection.</summary>
		/// <param name="sourceTableName">The case-sensitive name of the SourceTable. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BCB RID: 3019
		void RemoveAt(string sourceTableName);
	}
}
