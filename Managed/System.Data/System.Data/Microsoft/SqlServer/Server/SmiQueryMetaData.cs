using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A0 RID: 928
	internal class SmiQueryMetaData : SmiStorageMetaData
	{
		// Token: 0x06002BC7 RID: 11207 RVA: 0x000C0770 File Offset: 0x000BE970
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000C07B4 File Offset: 0x000BE9B4
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isColumnSet)
		{
			this._isReadOnly = isReadOnly;
			this._isExpression = isExpression;
			this._isAliased = isAliased;
			this._isHidden = isHidden;
		}

		// Token: 0x04001AA6 RID: 6822
		private bool _isReadOnly;

		// Token: 0x04001AA7 RID: 6823
		private SqlBoolean _isExpression;

		// Token: 0x04001AA8 RID: 6824
		private SqlBoolean _isAliased;

		// Token: 0x04001AA9 RID: 6825
		private SqlBoolean _isHidden;
	}
}
