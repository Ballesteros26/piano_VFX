using System;
using System.Collections;

namespace System.Data
{
	/// <summary>Contains a collection of DataColumnMapping objects, and is implemented by the <see cref="T:System.Data.Common.DataColumnMappingCollection" />, which is used in common by .NET Framework data providers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C3 RID: 195
	public interface IColumnMappingCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.IColumnMapping" /> object with the specified SourceColumn name.</summary>
		/// <returns>The IColumnMapping object with the specified SourceColumn name.</returns>
		/// <param name="index">The SourceColumn name of the IColumnMapping object to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F6 RID: 502
		object this[string index] { get; set; }

		/// <summary>Adds a ColumnMapping object to the ColumnMapping collection using the source column and <see cref="T:System.Data.DataSet" /> column names.</summary>
		/// <returns>The ColumnMapping object that was added to the collection.</returns>
		/// <param name="sourceColumnName">The case-sensitive name of the source column. </param>
		/// <param name="dataSetColumnName">The name of the <see cref="T:System.Data.DataSet" /> column. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B4F RID: 2895
		IColumnMapping Add(string sourceColumnName, string dataSetColumnName);

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> contains a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</summary>
		/// <returns>true if a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name exists, otherwise false.</returns>
		/// <param name="sourceColumnName">The case-sensitive name of the source column. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B50 RID: 2896
		bool Contains(string sourceColumnName);

		/// <summary>Gets the ColumnMapping object with the specified <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <returns>The ColumnMapping object with the specified DataSet column name.</returns>
		/// <param name="dataSetColumnName">The name of the <see cref="T:System.Data.DataSet" /> column within the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B51 RID: 2897
		IColumnMapping GetByDataSetColumn(string dataSetColumnName);

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name. The name is case-sensitive.</summary>
		/// <returns>The zero-based location of the DataColumnMapping object with the specified source column name.</returns>
		/// <param name="sourceColumnName">The case-sensitive name of the source column. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B52 RID: 2898
		int IndexOf(string sourceColumnName);

		/// <summary>Removes the <see cref="T:System.Data.IColumnMapping" /> object with the specified <see cref="P:System.Data.IColumnMapping.SourceColumn" /> name from the collection.</summary>
		/// <param name="sourceColumnName">The case-sensitive SourceColumn name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">A <see cref="T:System.Data.Common.DataColumnMapping" /> object does not exist with the specified SourceColumn name. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B53 RID: 2899
		void RemoveAt(string sourceColumnName);
	}
}
