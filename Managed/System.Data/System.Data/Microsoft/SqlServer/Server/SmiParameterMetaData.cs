using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200039E RID: 926
	internal sealed class SmiParameterMetaData : SmiExtendedMetaData
	{
		// Token: 0x06002BC3 RID: 11203 RVA: 0x000C06B8 File Offset: 0x000BE8B8
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._direction = direction;
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000C06EA File Offset: 0x000BE8EA
		internal ParameterDirection Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x04001A9C RID: 6812
		private ParameterDirection _direction;
	}
}
