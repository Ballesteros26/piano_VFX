using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200039D RID: 925
	internal class SmiExtendedMetaData : SmiMetaData
	{
		// Token: 0x06002BBD RID: 11197 RVA: 0x000C062C File Offset: 0x000BE82C
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000C0654 File Offset: 0x000BE854
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3)
			: base(dbType, maxLength, precision, scale, localeId, compareOptions, isMultiValued, fieldMetaData, extendedProperties)
		{
			this._name = name;
			this._typeSpecificNamePart1 = typeSpecificNamePart1;
			this._typeSpecificNamePart2 = typeSpecificNamePart2;
			this._typeSpecificNamePart3 = typeSpecificNamePart3;
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000C0696 File Offset: 0x000BE896
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000C069E File Offset: 0x000BE89E
		internal string TypeSpecificNamePart1
		{
			get
			{
				return this._typeSpecificNamePart1;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x000C06A6 File Offset: 0x000BE8A6
		internal string TypeSpecificNamePart2
		{
			get
			{
				return this._typeSpecificNamePart2;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000C06AE File Offset: 0x000BE8AE
		internal string TypeSpecificNamePart3
		{
			get
			{
				return this._typeSpecificNamePart3;
			}
		}

		// Token: 0x04001A98 RID: 6808
		private string _name;

		// Token: 0x04001A99 RID: 6809
		private string _typeSpecificNamePart1;

		// Token: 0x04001A9A RID: 6810
		private string _typeSpecificNamePart2;

		// Token: 0x04001A9B RID: 6811
		private string _typeSpecificNamePart3;
	}
}
