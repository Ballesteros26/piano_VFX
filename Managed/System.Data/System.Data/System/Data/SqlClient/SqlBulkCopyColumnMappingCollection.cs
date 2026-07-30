using System;
using System.Collections;

namespace System.Data.SqlClient
{
	/// <summary>Collection of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> objects that inherits from <see cref="T:System.Collections.CollectionBase" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000164 RID: 356
	public sealed class SqlBulkCopyColumnMappingCollection : CollectionBase
	{
		// Token: 0x06001100 RID: 4352 RVA: 0x000563DA File Offset: 0x000545DA
		internal SqlBulkCopyColumnMappingCollection()
		{
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x000563E2 File Offset: 0x000545E2
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x000563EA File Offset: 0x000545EA
		internal bool ReadOnly { get; set; }

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object at the specified index.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> to find.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000300 RID: 768
		public SqlBulkCopyColumnMapping this[int index]
		{
			get
			{
				return (SqlBulkCopyColumnMapping)base.List[index];
			}
		}

		/// <summary>Adds the specified mapping to the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</returns>
		/// <param name="bulkCopyColumnMapping">The <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object that describes the mapping to be added to the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001104 RID: 4356 RVA: 0x00056408 File Offset: 0x00054608
		public SqlBulkCopyColumnMapping Add(SqlBulkCopyColumnMapping bulkCopyColumnMapping)
		{
			this.AssertWriteAccess();
			if ((string.IsNullOrEmpty(bulkCopyColumnMapping.SourceColumn) && bulkCopyColumnMapping.SourceOrdinal == -1) || (string.IsNullOrEmpty(bulkCopyColumnMapping.DestinationColumn) && bulkCopyColumnMapping.DestinationOrdinal == -1))
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			base.InnerList.Add(bulkCopyColumnMapping);
			return bulkCopyColumnMapping;
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using column names to specify both source and destination columns.</summary>
		/// <returns>A column mapping.</returns>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001105 RID: 4357 RVA: 0x0005645B File Offset: 0x0005465B
		public SqlBulkCopyColumnMapping Add(string sourceColumn, string destinationColumn)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumn, destinationColumn));
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using an ordinal for the source column and a string for the destination column.</summary>
		/// <returns>A column mapping.</returns>
		/// <param name="sourceColumnIndex">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001106 RID: 4358 RVA: 0x00056470 File Offset: 0x00054670
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, string destinationColumn)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumn));
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using a column name to describe the source column and an ordinal to specify the destination column.</summary>
		/// <returns>A column mapping.</returns>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumnIndex">The ordinal position of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001107 RID: 4359 RVA: 0x00056485 File Offset: 0x00054685
		public SqlBulkCopyColumnMapping Add(string sourceColumn, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumn, destinationColumnIndex));
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using ordinals to specify both source and destination columns.</summary>
		/// <returns>A column mapping.</returns>
		/// <param name="sourceColumnIndex">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumnIndex">The ordinal position of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001108 RID: 4360 RVA: 0x0005649A File Offset: 0x0005469A
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, int destinationColumnIndex)
		{
			this.AssertWriteAccess();
			return this.Add(new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumnIndex));
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000564AF File Offset: 0x000546AF
		private void AssertWriteAccess()
		{
			if (this.ReadOnly)
			{
				throw SQL.BulkLoadMappingInaccessible();
			}
		}

		/// <summary>Clears the contents of the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110A RID: 4362 RVA: 0x000564BF File Offset: 0x000546BF
		public new void Clear()
		{
			this.AssertWriteAccess();
			base.Clear();
		}

		/// <summary>Gets a value indicating whether a specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object exists in the collection.</summary>
		/// <returns>true if the specified mapping exists in the collection; otherwise false.</returns>
		/// <param name="value">A valid <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110B RID: 4363 RVA: 0x000564CD File Offset: 0x000546CD
		public bool Contains(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" /> to an array of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> items, starting at a particular index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> array that is the destination of the elements copied from <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110C RID: 4364 RVA: 0x000564DB File Offset: 0x000546DB
		public void CopyTo(SqlBulkCopyColumnMapping[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000564EC File Offset: 0x000546EC
		internal void CreateDefaultMapping(int columnCount)
		{
			for (int i = 0; i < columnCount; i++)
			{
				base.InnerList.Add(new SqlBulkCopyColumnMapping(i, i));
			}
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</summary>
		/// <returns>The zero-based index of the column mapping, or -1 if the column mapping is not found in the collection.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object for which to search.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110E RID: 4366 RVA: 0x00056518 File Offset: 0x00054718
		public int IndexOf(SqlBulkCopyColumnMapping value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Insert a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> at the index specified.</summary>
		/// <param name="index">Integer value of the location within the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />  at which to insert the new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" />.</param>
		/// <param name="value">
		///   <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be inserted in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600110F RID: 4367 RVA: 0x00056526 File Offset: 0x00054726
		public void Insert(int index, SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> element from the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />.</summary>
		/// <param name="value">
		///   <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be removed from the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001110 RID: 4368 RVA: 0x0005653B File Offset: 0x0005473B
		public void Remove(SqlBulkCopyColumnMapping value)
		{
			this.AssertWriteAccess();
			base.InnerList.Remove(value);
		}

		/// <summary>Removes the mapping at the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be removed from the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001111 RID: 4369 RVA: 0x0005654F File Offset: 0x0005474F
		public new void RemoveAt(int index)
		{
			this.AssertWriteAccess();
			base.RemoveAt(index);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00056560 File Offset: 0x00054760
		internal void ValidateCollection()
		{
			foreach (object obj in base.InnerList)
			{
				SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
				SqlBulkCopyColumnMappingCollection.MappingSchema mappingSchema = ((sqlBulkCopyColumnMapping.SourceOrdinal != -1) ? ((sqlBulkCopyColumnMapping.DestinationOrdinal != -1) ? SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsOrdinals : SqlBulkCopyColumnMappingCollection.MappingSchema.OrdinalsNames) : ((sqlBulkCopyColumnMapping.DestinationOrdinal != -1) ? SqlBulkCopyColumnMappingCollection.MappingSchema.NemesOrdinals : SqlBulkCopyColumnMappingCollection.MappingSchema.NamesNames));
				if (this._mappingSchema == SqlBulkCopyColumnMappingCollection.MappingSchema.Undefined)
				{
					this._mappingSchema = mappingSchema;
				}
				else if (this._mappingSchema != mappingSchema)
				{
					throw SQL.BulkLoadMappingsNamesOrOrdinalsOnly();
				}
			}
		}

		// Token: 0x04000B4D RID: 2893
		private SqlBulkCopyColumnMappingCollection.MappingSchema _mappingSchema;

		// Token: 0x02000165 RID: 357
		private enum MappingSchema
		{
			// Token: 0x04000B50 RID: 2896
			Undefined,
			// Token: 0x04000B51 RID: 2897
			NamesNames,
			// Token: 0x04000B52 RID: 2898
			NemesOrdinals,
			// Token: 0x04000B53 RID: 2899
			OrdinalsNames,
			// Token: 0x04000B54 RID: 2900
			OrdinalsOrdinals
		}
	}
}
