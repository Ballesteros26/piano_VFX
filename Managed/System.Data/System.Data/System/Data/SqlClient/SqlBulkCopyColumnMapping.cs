using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Defines the mapping between a column in a <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance's data source and a column in the instance's destination table. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000163 RID: 355
	public sealed class SqlBulkCopyColumnMapping
	{
		/// <summary>Name of the column being mapped in the destination database table.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.DestinationColumn" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0005628E File Offset: 0x0005448E
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x000562A4 File Offset: 0x000544A4
		public string DestinationColumn
		{
			get
			{
				if (this._destinationColumnName != null)
				{
					return this._destinationColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._destinationColumnOrdinal = (this._internalDestinationColumnOrdinal = -1);
				this._destinationColumnName = value;
			}
		}

		/// <summary>Ordinal value of the destination column within the destination table.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.DestinationOrdinal" /> property, or -1 if the property has not been set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x000562C8 File Offset: 0x000544C8
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x000562D0 File Offset: 0x000544D0
		public int DestinationOrdinal
		{
			get
			{
				return this._destinationColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._destinationColumnName = null;
					this._internalDestinationColumnOrdinal = value;
					this._destinationColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		/// <summary>Name of the column being mapped in the data source.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.SourceColumn" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x000562FF File Offset: 0x000544FF
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00056318 File Offset: 0x00054518
		public string SourceColumn
		{
			get
			{
				if (this._sourceColumnName != null)
				{
					return this._sourceColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._sourceColumnOrdinal = (this._internalSourceColumnOrdinal = -1);
				this._sourceColumnName = value;
			}
		}

		/// <summary>The ordinal position of the source column within the data source.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.SourceOrdinal" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0005633C File Offset: 0x0005453C
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x00056344 File Offset: 0x00054544
		public int SourceOrdinal
		{
			get
			{
				return this._sourceColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._sourceColumnName = null;
					this._internalSourceColumnOrdinal = value;
					this._sourceColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		/// <summary>Default constructor that initializes a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</summary>
		// Token: 0x060010FB RID: 4347 RVA: 0x00056373 File Offset: 0x00054573
		public SqlBulkCopyColumnMapping()
		{
			this._internalSourceColumnOrdinal = -1;
		}

		/// <summary>Creates a new column mapping, using column names to refer to source and destination columns.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		// Token: 0x060010FC RID: 4348 RVA: 0x00056382 File Offset: 0x00054582
		public SqlBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationColumn = destinationColumn;
		}

		/// <summary>Creates a new column mapping, using a column ordinal to refer to the source column and a column name for the target column.</summary>
		/// <param name="sourceColumnOrdinal">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		// Token: 0x060010FD RID: 4349 RVA: 0x00056398 File Offset: 0x00054598
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, string destinationColumn)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationColumn = destinationColumn;
		}

		/// <summary>Creates a new column mapping, using a column name to refer to the source column and a column ordinal for the target column.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationOrdinal">The ordinal position of the destination column within the destination table.</param>
		// Token: 0x060010FE RID: 4350 RVA: 0x000563AE File Offset: 0x000545AE
		public SqlBulkCopyColumnMapping(string sourceColumn, int destinationOrdinal)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationOrdinal = destinationOrdinal;
		}

		/// <summary>Creates a new column mapping, using column ordinals to refer to source and destination columns.</summary>
		/// <param name="sourceColumnOrdinal">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationOrdinal">The ordinal position of the destination column within the destination table.</param>
		// Token: 0x060010FF RID: 4351 RVA: 0x000563C4 File Offset: 0x000545C4
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, int destinationOrdinal)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x04000B47 RID: 2887
		internal string _destinationColumnName;

		// Token: 0x04000B48 RID: 2888
		internal int _destinationColumnOrdinal;

		// Token: 0x04000B49 RID: 2889
		internal string _sourceColumnName;

		// Token: 0x04000B4A RID: 2890
		internal int _sourceColumnOrdinal;

		// Token: 0x04000B4B RID: 2891
		internal int _internalDestinationColumnOrdinal;

		// Token: 0x04000B4C RID: 2892
		internal int _internalSourceColumnOrdinal;
	}
}
