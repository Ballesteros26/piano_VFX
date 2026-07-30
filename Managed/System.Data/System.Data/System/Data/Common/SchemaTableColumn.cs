using System;

namespace System.Data.Common
{
	/// <summary>Describes the column metadata of the schema for a database table.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000370 RID: 880
	public static class SchemaTableColumn
	{
		/// <summary>Specifies the name of the column in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001940 RID: 6464
		public static readonly string ColumnName = "ColumnName";

		/// <summary>Specifies the ordinal of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001941 RID: 6465
		public static readonly string ColumnOrdinal = "ColumnOrdinal";

		/// <summary>Specifies the size of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001942 RID: 6466
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Specifies the precision of the column data, if the data is numeric.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001943 RID: 6467
		public static readonly string NumericPrecision = "NumericPrecision";

		/// <summary>Specifies the scale of the column data, if the data is numeric.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001944 RID: 6468
		public static readonly string NumericScale = "NumericScale";

		/// <summary>Specifies the type of data in the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001945 RID: 6469
		public static readonly string DataType = "DataType";

		/// <summary>Specifies the provider-specific data type of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001946 RID: 6470
		public static readonly string ProviderType = "ProviderType";

		/// <summary>Specifies the non-versioned provider-specific data type of the column.</summary>
		// Token: 0x04001947 RID: 6471
		public static readonly string NonVersionedProviderType = "NonVersionedProviderType";

		/// <summary>Specifies whether this column contains long data.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001948 RID: 6472
		public static readonly string IsLong = "IsLong";

		/// <summary>Specifies whether value DBNull is allowed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001949 RID: 6473
		public static readonly string AllowDBNull = "AllowDBNull";

		/// <summary>Specifies whether this column is aliased.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194A RID: 6474
		public static readonly string IsAliased = "IsAliased";

		/// <summary>Specifies whether this column is an expression.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194B RID: 6475
		public static readonly string IsExpression = "IsExpression";

		/// <summary>Specifies whether this column is a key for the table. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194C RID: 6476
		public static readonly string IsKey = "IsKey";

		/// <summary>Specifies whether a unique constraint applies to this column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194D RID: 6477
		public static readonly string IsUnique = "IsUnique";

		/// <summary>Specifies the name of the schema in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194E RID: 6478
		public static readonly string BaseSchemaName = "BaseSchemaName";

		/// <summary>Specifies the name of the table in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194F RID: 6479
		public static readonly string BaseTableName = "BaseTableName";

		/// <summary>Specifies the name of the column in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001950 RID: 6480
		public static readonly string BaseColumnName = "BaseColumnName";
	}
}
