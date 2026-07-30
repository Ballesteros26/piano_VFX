using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200039F RID: 927
	internal class SmiStorageMetaData : SmiExtendedMetaData
	{
		// Token: 0x06002BC5 RID: 11205 RVA: 0x000C06F4 File Offset: 0x000BE8F4
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._allowsDBNull = allowsDBNull;
			this._serverName = serverName;
			this._catalogName = catalogName;
			this._schemaName = schemaName;
			this._tableName = tableName;
			this._columnName = columnName;
			this._isKey = isKey;
			this._isIdentity = isIdentity;
			this._isColumnSet = isColumnSet;
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x000C0766 File Offset: 0x000BE966
		internal SqlBoolean IsKey
		{
			get
			{
				return this._isKey;
			}
		}

		// Token: 0x04001A9D RID: 6813
		private bool _allowsDBNull;

		// Token: 0x04001A9E RID: 6814
		private string _serverName;

		// Token: 0x04001A9F RID: 6815
		private string _catalogName;

		// Token: 0x04001AA0 RID: 6816
		private string _schemaName;

		// Token: 0x04001AA1 RID: 6817
		private string _tableName;

		// Token: 0x04001AA2 RID: 6818
		private string _columnName;

		// Token: 0x04001AA3 RID: 6819
		private SqlBoolean _isKey;

		// Token: 0x04001AA4 RID: 6820
		private bool _isIdentity;

		// Token: 0x04001AA5 RID: 6821
		private bool _isColumnSet;
	}
}
