using System;

namespace System.Data.Common
{
	/// <summary>Describes optional column metadata of the schema for a database table.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000371 RID: 881
	public static class SchemaTableOptionalColumn
	{
		/// <summary>Specifies the provider-specific data type of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001951 RID: 6481
		public static readonly string ProviderSpecificDataType = "ProviderSpecificDataType";

		/// <summary>Specifies whether the column values in the column are automatically incremented.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001952 RID: 6482
		public static readonly string IsAutoIncrement = "IsAutoIncrement";

		/// <summary>Specifies whether this column is hidden.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001953 RID: 6483
		public static readonly string IsHidden = "IsHidden";

		/// <summary>Specifies whether this column is read-only.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001954 RID: 6484
		public static readonly string IsReadOnly = "IsReadOnly";

		/// <summary>Specifies whether this column contains row version information.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001955 RID: 6485
		public static readonly string IsRowVersion = "IsRowVersion";

		/// <summary>The server name of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001956 RID: 6486
		public static readonly string BaseServerName = "BaseServerName";

		/// <summary>The name of the catalog associated with the results of the latest query.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001957 RID: 6487
		public static readonly string BaseCatalogName = "BaseCatalogName";

		/// <summary>Specifies the value at which the series for new identity columns is assigned.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001958 RID: 6488
		public static readonly string AutoIncrementSeed = "AutoIncrementSeed";

		/// <summary>Specifies the increment between values in the identity column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001959 RID: 6489
		public static readonly string AutoIncrementStep = "AutoIncrementStep";

		/// <summary>The default value for the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195A RID: 6490
		public static readonly string DefaultValue = "DefaultValue";

		/// <summary>The expression used to compute the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195B RID: 6491
		public static readonly string Expression = "Expression";

		/// <summary>The namespace for the table that contains the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195C RID: 6492
		public static readonly string BaseTableNamespace = "BaseTableNamespace";

		/// <summary>The namespace of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195D RID: 6493
		public static readonly string BaseColumnNamespace = "BaseColumnNamespace";

		/// <summary>Specifies the mapping for the column.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400195E RID: 6494
		public static readonly string ColumnMapping = "ColumnMapping";
	}
}
