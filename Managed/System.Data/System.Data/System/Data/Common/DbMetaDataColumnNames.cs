using System;

namespace System.Data.Common
{
	/// <summary>Provides static values that are used for the column names in the MetaDataCollection objects contained in the <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> is created by the GetSchema method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200034D RID: 845
	public static class DbMetaDataColumnNames
	{
		/// <summary>Used by the GetSchema method to create the CollectionName column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018D8 RID: 6360
		public static readonly string CollectionName = "CollectionName";

		/// <summary>Used by the GetSchema method to create the ColumnSize column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018D9 RID: 6361
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Used by the GetSchema method to create the CompositeIdentifierSeparatorPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018DA RID: 6362
		public static readonly string CompositeIdentifierSeparatorPattern = "CompositeIdentifierSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the CreateFormat column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018DB RID: 6363
		public static readonly string CreateFormat = "CreateFormat";

		/// <summary>Used by the GetSchema method to create the CreateParameters column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018DC RID: 6364
		public static readonly string CreateParameters = "CreateParameters";

		/// <summary>Used by the GetSchema method to create the DataSourceProductName column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018DD RID: 6365
		public static readonly string DataSourceProductName = "DataSourceProductName";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersion column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018DE RID: 6366
		public static readonly string DataSourceProductVersion = "DataSourceProductVersion";

		/// <summary>Used by the GetSchema method to create the DataType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018DF RID: 6367
		public static readonly string DataType = "DataType";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersionNormalized column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E0 RID: 6368
		public static readonly string DataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		/// <summary>Used by the GetSchema method to create the GroupByBehavior column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E1 RID: 6369
		public static readonly string GroupByBehavior = "GroupByBehavior";

		/// <summary>Used by the GetSchema method to create the IdentifierCase column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E2 RID: 6370
		public static readonly string IdentifierCase = "IdentifierCase";

		/// <summary>Used by the GetSchema method to create the IdentifierPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E3 RID: 6371
		public static readonly string IdentifierPattern = "IdentifierPattern";

		/// <summary>Used by the GetSchema method to create the IsAutoIncrementable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E4 RID: 6372
		public static readonly string IsAutoIncrementable = "IsAutoIncrementable";

		/// <summary>Used by the GetSchema method to create the IsBestMatch column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E5 RID: 6373
		public static readonly string IsBestMatch = "IsBestMatch";

		/// <summary>Used by the GetSchema method to create the IsCaseSensitive column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E6 RID: 6374
		public static readonly string IsCaseSensitive = "IsCaseSensitive";

		/// <summary>Used by the GetSchema method to create the IsConcurrencyType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E7 RID: 6375
		public static readonly string IsConcurrencyType = "IsConcurrencyType";

		/// <summary>Used by the GetSchema method to create the IsFixedLength column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E8 RID: 6376
		public static readonly string IsFixedLength = "IsFixedLength";

		/// <summary>Used by the GetSchema method to create the IsFixedPrecisionScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018E9 RID: 6377
		public static readonly string IsFixedPrecisionScale = "IsFixedPrecisionScale";

		/// <summary>Used by the GetSchema method to create the IsLiteralSupported column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018EA RID: 6378
		public static readonly string IsLiteralSupported = "IsLiteralSupported";

		/// <summary>Used by the GetSchema method to create the IsLong column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018EB RID: 6379
		public static readonly string IsLong = "IsLong";

		/// <summary>Used by the GetSchema method to create the IsNullable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018EC RID: 6380
		public static readonly string IsNullable = "IsNullable";

		/// <summary>Used by the GetSchema method to create the IsSearchable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018ED RID: 6381
		public static readonly string IsSearchable = "IsSearchable";

		/// <summary>Used by the GetSchema method to create the IsSearchableWithLike column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018EE RID: 6382
		public static readonly string IsSearchableWithLike = "IsSearchableWithLike";

		/// <summary>Used by the GetSchema method to create the IsUnsigned column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018EF RID: 6383
		public static readonly string IsUnsigned = "IsUnsigned";

		/// <summary>Used by the GetSchema method to create the LiteralPrefix column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F0 RID: 6384
		public static readonly string LiteralPrefix = "LiteralPrefix";

		/// <summary>Used by the GetSchema method to create the LiteralSuffix column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F1 RID: 6385
		public static readonly string LiteralSuffix = "LiteralSuffix";

		/// <summary>Used by the GetSchema method to create the MaximumScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F2 RID: 6386
		public static readonly string MaximumScale = "MaximumScale";

		/// <summary>Used by the GetSchema method to create the MinimumScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F3 RID: 6387
		public static readonly string MinimumScale = "MinimumScale";

		/// <summary>Used by the GetSchema method to create the NumberOfIdentifierParts column in the MetaDataCollections collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F4 RID: 6388
		public static readonly string NumberOfIdentifierParts = "NumberOfIdentifierParts";

		/// <summary>Used by the GetSchema method to create the NumberOfRestrictions column in the MetaDataCollections collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F5 RID: 6389
		public static readonly string NumberOfRestrictions = "NumberOfRestrictions";

		/// <summary>Used by the GetSchema method to create the OrderByColumnsInSelect column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F6 RID: 6390
		public static readonly string OrderByColumnsInSelect = "OrderByColumnsInSelect";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerFormat column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F7 RID: 6391
		public static readonly string ParameterMarkerFormat = "ParameterMarkerFormat";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F8 RID: 6392
		public static readonly string ParameterMarkerPattern = "ParameterMarkerPattern";

		/// <summary>Used by the GetSchema method to create the ParameterNameMaxLength column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018F9 RID: 6393
		public static readonly string ParameterNameMaxLength = "ParameterNameMaxLength";

		/// <summary>Used by the GetSchema method to create the ParameterNamePattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018FA RID: 6394
		public static readonly string ParameterNamePattern = "ParameterNamePattern";

		/// <summary>Used by the GetSchema method to create the ProviderDbType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018FB RID: 6395
		public static readonly string ProviderDbType = "ProviderDbType";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierCase column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018FC RID: 6396
		public static readonly string QuotedIdentifierCase = "QuotedIdentifierCase";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018FD RID: 6397
		public static readonly string QuotedIdentifierPattern = "QuotedIdentifierPattern";

		/// <summary>Used by the GetSchema method to create the ReservedWord column in the ReservedWords collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018FE RID: 6398
		public static readonly string ReservedWord = "ReservedWord";

		/// <summary>Used by the GetSchema method to create the StatementSeparatorPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018FF RID: 6399
		public static readonly string StatementSeparatorPattern = "StatementSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the StringLiteralPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001900 RID: 6400
		public static readonly string StringLiteralPattern = "StringLiteralPattern";

		/// <summary>Used by the GetSchema method to create the SupportedJoinOperators column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001901 RID: 6401
		public static readonly string SupportedJoinOperators = "SupportedJoinOperators";

		/// <summary>Used by the GetSchema method to create the TypeName column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001902 RID: 6402
		public static readonly string TypeName = "TypeName";
	}
}
