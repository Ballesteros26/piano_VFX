using System;

namespace System.Data.Common
{
	/// <summary>Provides a list of constants for the well-known MetaDataCollections: DataSourceInformation, DataTypes, MetaDataCollections, ReservedWords, and Restrictions.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200034C RID: 844
	public static class DbMetaDataCollectionNames
	{
		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the MetaDataCollections collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018D3 RID: 6355
		public static readonly string MetaDataCollections = "MetaDataCollections";

		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018D4 RID: 6356
		public static readonly string DataSourceInformation = "DataSourceInformation";

		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018D5 RID: 6357
		public static readonly string DataTypes = "DataTypes";

		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the Restrictions collection.  </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018D6 RID: 6358
		public static readonly string Restrictions = "Restrictions";

		/// <summary>A constant for use with the <see cref="M:System.Data.Common.DbConnection.GetSchema" /> method that represents the ReservedWords collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018D7 RID: 6359
		public static readonly string ReservedWords = "ReservedWords";
	}
}
