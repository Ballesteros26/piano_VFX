using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using Unity;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Specifies and retrieves metadata information from parameters and columns of <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020003B2 RID: 946
	public sealed class SqlMetaData
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name and type.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06002D92 RID: 11666 RVA: 0x000C5CF9 File Offset: 0x000C3EF9
		public SqlMetaData(string name, SqlDbType dbType)
		{
			this.Construct(name, dbType, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, and default server. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002D93 RID: 11667 RVA: 0x000C5D0D File Offset: 0x000C3F0D
		public SqlMetaData(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and maximum length.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06002D94 RID: 11668 RVA: 0x000C5D24 File Offset: 0x000C3F24
		public SqlMetaData(string name, SqlDbType dbType, long maxLength)
		{
			this.Construct(name, dbType, maxLength, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002D95 RID: 11669 RVA: 0x000C5D39 File Offset: 0x000C3F39
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, precision, and scale.</summary>
		/// <param name="name">The name of the parameter or column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="scale" /> was greater than <paramref name="precision" />. </exception>
		// Token: 0x06002D96 RID: 11670 RVA: 0x000C5D54 File Offset: 0x000C3F54
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale)
		{
			this.Construct(name, dbType, precision, scale, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, precision, scale, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002D97 RID: 11671 RVA: 0x000C5D78 File Offset: 0x000C3F78
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, locale, and compare options.</summary>
		/// <param name="name">The name of the parameter or column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type. </param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06002D98 RID: 11672 RVA: 0x000C5DA0 File Offset: 0x000C3FA0
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, false, false, SortOrder.Unspecified, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, locale, compare options, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002D99 RID: 11673 RVA: 0x000C5DC4 File Offset: 0x000C3FC4
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, database name, owning schema, object name, and default server. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="database">The database name of the XML schema collection of a typed XML instance.</param>
		/// <param name="owningSchema">The relational schema name of the XML schema collection of a typed XML instance.</param>
		/// <param name="objectName">The name of the XML schema collection of a typed XML instance.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002D9A RID: 11674 RVA: 0x000C5DEC File Offset: 0x000C3FEC
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, precision, scale, locale ID, compare options, and user-defined type (UDT).</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared.</exception>
		// Token: 0x06002D9B RID: 11675 RVA: 0x000C5E14 File Offset: 0x000C4014
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long locale, SqlCompareOptions compareOptions, Type userDefinedType)
			: this(name, dbType, maxLength, precision, scale, locale, compareOptions, userDefinedType, false, false, SortOrder.Unspecified, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, precision, scale, locale ID, compare options, and user-defined type (UDT). This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="localeId">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002D9C RID: 11676 RVA: 0x000C5E38 File Offset: 0x000C4038
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			switch (dbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Float:
			case SqlDbType.Image:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.Timestamp:
			case SqlDbType.TinyInt:
			case SqlDbType.Xml:
			case SqlDbType.Date:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this.Construct(name, dbType, maxLength, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Decimal:
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this.Construct(name, dbType, SqlMetaData.Max, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Variant:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Udt:
				throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
			}
			SQL.InvalidSqlDbTypeForConstructor(dbType);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, database name, owning schema, and object name.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="database">The database name of the XML schema collection of a typed XML instance.</param>
		/// <param name="owningSchema">The relational schema name of the XML schema collection of a typed XML instance.</param>
		/// <param name="objectName">The name of the XML schema collection of a typed XML instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null, or <paramref name="objectName" /> is null when <paramref name="database" /> and <paramref name="owningSchema" /> are non-null.</exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06002D9D RID: 11677 RVA: 0x000C5F78 File Offset: 0x000C4178
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000C5F9C File Offset: 0x000C419C
		internal SqlMetaData(string name, SqlDbType sqlDBType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName, bool partialLength)
		{
			this.AssertNameIsValid(name);
			this._strName = name;
			this._sqlDbType = sqlDBType;
			this._lMaxLength = maxLength;
			this._bPrecision = precision;
			this._bScale = scale;
			this._lLocale = localeId;
			this._eCompareOptions = compareOptions;
			this._xmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
			this._xmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
			this._xmlSchemaCollectionName = xmlSchemaCollectionName;
			this._bPartialLength = partialLength;
			this.ThrowIfUdt(sqlDBType);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000C6014 File Offset: 0x000C4214
		private SqlMetaData(string name, SqlDbType sqlDbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool partialLength)
		{
			this.AssertNameIsValid(name);
			this._strName = name;
			this._sqlDbType = sqlDbType;
			this._lMaxLength = maxLength;
			this._bPrecision = precision;
			this._bScale = scale;
			this._lLocale = localeId;
			this._eCompareOptions = compareOptions;
			this._bPartialLength = partialLength;
			this.ThrowIfUdt(sqlDbType);
		}

		/// <summary>Gets the comparison rules used for the column or parameter.</summary>
		/// <returns>The comparison rules used for the column or parameter as a <see cref="T:System.Data.SqlTypes.SqlCompareOptions" />.</returns>
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x000C6072 File Offset: 0x000C4272
		public SqlCompareOptions CompareOptions
		{
			get
			{
				return this._eCompareOptions;
			}
		}

		/// <summary>Indicates if the column in the table-valued parameter is unique.</summary>
		/// <returns>A Boolean value.</returns>
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000C607A File Offset: 0x000C427A
		public bool IsUniqueKey
		{
			get
			{
				return this._isUniqueKey;
			}
		}

		/// <summary>Gets the locale ID of the column or parameter.</summary>
		/// <returns>The locale ID of the column or parameter as a <see cref="T:System.Int64" />.</returns>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x000C6082 File Offset: 0x000C4282
		public long LocaleId
		{
			get
			{
				return this._lLocale;
			}
		}

		/// <summary>Gets the length of text, ntext, and image data types. </summary>
		/// <returns>The length of text, ntext, and image data types.</returns>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x000C608A File Offset: 0x000C428A
		public static long Max
		{
			get
			{
				return -1L;
			}
		}

		/// <summary>Gets the maximum length of the column or parameter.</summary>
		/// <returns>The maximum length of the column or parameter as a <see cref="T:System.Int64" />.</returns>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x000C608E File Offset: 0x000C428E
		public long MaxLength
		{
			get
			{
				return this._lMaxLength;
			}
		}

		/// <summary>Gets the name of the column or parameter.</summary>
		/// <returns>The name of the column or parameter as a <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="Name" /> specified in the constructor is longer than 128 characters. </exception>
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x000C6096 File Offset: 0x000C4296
		public string Name
		{
			get
			{
				return this._strName;
			}
		}

		/// <summary>Gets the precision of the column or parameter.</summary>
		/// <returns>The precision of the column or parameter as a <see cref="T:System.Byte" />.</returns>
		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x000C609E File Offset: 0x000C429E
		public byte Precision
		{
			get
			{
				return this._bPrecision;
			}
		}

		/// <summary>Gets the scale of the column or parameter.</summary>
		/// <returns>The scale of the column or parameter.</returns>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x000C60A6 File Offset: 0x000C42A6
		public byte Scale
		{
			get
			{
				return this._bScale;
			}
		}

		/// <summary>Returns the sort order for a column.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SortOrder" /> object.</returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000C60AE File Offset: 0x000C42AE
		public SortOrder SortOrder
		{
			get
			{
				return this._columnSortOrder;
			}
		}

		/// <summary>Returns the ordinal of the sort column.</summary>
		/// <returns>The ordinal of the sort column.</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x000C60B6 File Offset: 0x000C42B6
		public int SortOrdinal
		{
			get
			{
				return this._sortOrdinal;
			}
		}

		/// <summary>Gets the data type of the column or parameter.</summary>
		/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x000C60BE File Offset: 0x000C42BE
		public SqlDbType SqlDbType
		{
			get
			{
				return this._sqlDbType;
			}
		}

		/// <summary>Gets the three-part name of the user-defined type (UDT) or the SQL Server type represented by the instance.</summary>
		/// <returns>The name of the UDT or SQL Server type as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x000C60C6 File Offset: 0x000C42C6
		public string TypeName
		{
			get
			{
				return SqlMetaData.sxm_rgDefaults[(int)this.SqlDbType].Name;
			}
		}

		/// <summary>Reports whether this column should use the default server value.</summary>
		/// <returns>A Boolean value.</returns>
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x000C60D9 File Offset: 0x000C42D9
		public bool UseServerDefault
		{
			get
			{
				return this._useServerDefault;
			}
		}

		/// <summary>Gets the name of the database where the schema collection for this XML instance is located.</summary>
		/// <returns>The name of the database where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x000C60E1 File Offset: 0x000C42E1
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				return this._xmlSchemaCollectionDatabase;
			}
		}

		/// <summary>Gets the name of the schema collection for this XML instance.</summary>
		/// <returns>The name of the schema collection for this XML instance as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x000C60E9 File Offset: 0x000C42E9
		public string XmlSchemaCollectionName
		{
			get
			{
				return this._xmlSchemaCollectionName;
			}
		}

		/// <summary>Gets the owning relational schema where the schema collection for this XML instance is located.</summary>
		/// <returns>The owning relational schema where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000C60F1 File Offset: 0x000C42F1
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				return this._xmlSchemaCollectionOwningSchema;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000C60F9 File Offset: 0x000C42F9
		internal bool IsPartialLength
		{
			get
			{
				return this._bPartialLength;
			}
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000C6104 File Offset: 0x000C4304
		private void Construct(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (dbType != SqlDbType.BigInt && SqlDbType.Bit != dbType && SqlDbType.DateTime != dbType && SqlDbType.Date != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType && SqlDbType.Decimal != dbType && SqlDbType.Float != dbType && SqlDbType.Image != dbType && SqlDbType.Int != dbType && SqlDbType.Money != dbType && SqlDbType.NText != dbType && SqlDbType.Real != dbType && SqlDbType.SmallDateTime != dbType && SqlDbType.SmallInt != dbType && SqlDbType.SmallMoney != dbType && SqlDbType.Text != dbType && SqlDbType.Time != dbType && SqlDbType.Timestamp != dbType && SqlDbType.TinyInt != dbType && SqlDbType.UniqueIdentifier != dbType && SqlDbType.Variant != dbType && SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			this.ThrowIfUdt(dbType);
			this.SetDefaultsForType(dbType);
			if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				this._lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			this._strName = name;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000C61E4 File Offset: 0x000C43E4
		private void Construct(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			long num = 0L;
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
				num = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.Binary == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.VarBinary == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.Image != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			this.SetDefaultsForType(dbType);
			this._strName = name;
			this._lMaxLength = maxLength;
			this._lLocale = num;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000C6498 File Offset: 0x000C4698
		private void Construct(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.NText != dbType && SqlDbType.Text != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(SR.GetString("Specified length '{0}' is out of range.", new object[] { maxLength.ToString(CultureInfo.InvariantCulture) }), "maxLength");
				}
			}
			if (SqlCompareOptions.BinarySort != compareOptions && (~(SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth) & compareOptions) != SqlCompareOptions.None)
			{
				throw ADP.InvalidEnumerationValue(typeof(SqlCompareOptions), (int)compareOptions);
			}
			this.SetDefaultsForType(dbType);
			this._strName = name;
			this._lMaxLength = maxLength;
			this._lLocale = locale;
			this._eCompareOptions = compareOptions;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000C6670 File Offset: 0x000C4870
		private void Construct(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Decimal == dbType)
			{
				if (precision > SqlDecimal.MaxPrecision || scale > precision)
				{
					throw SQL.PrecisionValueOutOfRange(precision);
				}
				if (scale > SqlDecimal.MaxScale)
				{
					throw SQL.ScaleValueOutOfRange(scale);
				}
			}
			else
			{
				if (SqlDbType.Time != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (scale > 7)
				{
					throw SQL.TimeScaleValueOutOfRange(scale);
				}
			}
			this.SetDefaultsForType(dbType);
			this._strName = name;
			this._bPrecision = precision;
			this._bScale = scale;
			if (SqlDbType.Decimal == dbType)
			{
				this._lMaxLength = (long)((ulong)SqlMetaData.s_maxLenFromPrecision[(int)(precision - 1)]);
			}
			else
			{
				this._lMaxLength -= (long)((ulong)SqlMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
			}
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000C6744 File Offset: 0x000C4944
		private void Construct(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if ((database != null || owningSchema != null) && objectName == null)
			{
				throw ADP.ArgumentNull("objectName");
			}
			this.SetDefaultsForType(SqlDbType.Xml);
			this._strName = name;
			this._xmlSchemaCollectionDatabase = database;
			this._xmlSchemaCollectionOwningSchema = owningSchema;
			this._xmlSchemaCollectionName = objectName;
			this._useServerDefault = useServerDefault;
			this._isUniqueKey = isUniqueKey;
			this._columnSortOrder = columnSortOrder;
			this._sortOrdinal = sortOrdinal;
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000C67CA File Offset: 0x000C49CA
		private void AssertNameIsValid(string name)
		{
			if (name == null)
			{
				throw ADP.ArgumentNull("name");
			}
			if (128L < (long)name.Length)
			{
				throw SQL.NameTooLong("name");
			}
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000C67F4 File Offset: 0x000C49F4
		private void ValidateSortOrder(SortOrder columnSortOrder, int sortOrdinal)
		{
			if (SortOrder.Unspecified != columnSortOrder && columnSortOrder != SortOrder.Ascending && SortOrder.Descending != columnSortOrder)
			{
				throw SQL.InvalidSortOrder(columnSortOrder);
			}
			if (SortOrder.Unspecified == columnSortOrder != (-1 == sortOrdinal))
			{
				throw SQL.MustSpecifyBothSortOrderAndOrdinal(columnSortOrder, sortOrdinal);
			}
		}

		/// <summary>Validates the specified <see cref="T:System.Int16" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Int16" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DB8 RID: 11704 RVA: 0x000C681A File Offset: 0x000C4A1A
		public short Adjust(short value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Int32" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Int32" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DB9 RID: 11705 RVA: 0x000C682C File Offset: 0x000C4A2C
		public int Adjust(int value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Int64" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Int64" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DBA RID: 11706 RVA: 0x000C683D File Offset: 0x000C4A3D
		public long Adjust(long value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Single" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Single" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DBB RID: 11707 RVA: 0x000C684D File Offset: 0x000C4A4D
		public float Adjust(float value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Double" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Double" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DBC RID: 11708 RVA: 0x000C685F File Offset: 0x000C4A5F
		public double Adjust(double value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.String" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.String" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DBD RID: 11709 RVA: 0x000C6870 File Offset: 0x000C4A70
		public string Adjust(string value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					value = value.PadRight((int)this.MaxLength);
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = value.Remove((int)this.MaxLength, (int)((long)value.Length - this.MaxLength));
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Decimal" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DBE RID: 11710 RVA: 0x000C6920 File Offset: 0x000C4B20
		public decimal Adjust(decimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType && SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				this.VerifyMoneyRange(new SqlMoney(value));
				return value;
			}
			return this.InternalAdjustSqlDecimal(new SqlDecimal(value)).Value;
		}

		/// <summary>Validates the specified <see cref="T:System.DateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.DateTime" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DBF RID: 11711 RVA: 0x000C697C File Offset: 0x000C4B7C
		public DateTime Adjust(DateTime value)
		{
			if (SqlDbType.DateTime == this.SqlDbType || SqlDbType.SmallDateTime == this.SqlDbType)
			{
				this.VerifyDateTimeRange(value);
			}
			else
			{
				if (SqlDbType.DateTime2 == this.SqlDbType)
				{
					return new DateTime(this.InternalAdjustTimeTicks(value.Ticks));
				}
				if (SqlDbType.Date == this.SqlDbType)
				{
					return value.Date;
				}
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Guid" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Guid" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC0 RID: 11712 RVA: 0x000C69DA File Offset: 0x000C4BDA
		public Guid Adjust(Guid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000C69EC File Offset: 0x000C4BEC
		public SqlBoolean Adjust(SqlBoolean value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlByte" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC2 RID: 11714 RVA: 0x000C69FD File Offset: 0x000C4BFD
		public SqlByte Adjust(SqlByte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC3 RID: 11715 RVA: 0x000C681A File Offset: 0x000C4A1A
		public SqlInt16 Adjust(SqlInt16 value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC4 RID: 11716 RVA: 0x000C682C File Offset: 0x000C4A2C
		public SqlInt32 Adjust(SqlInt32 value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt64" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC5 RID: 11717 RVA: 0x000C683D File Offset: 0x000C4A3D
		public SqlInt64 Adjust(SqlInt64 value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC6 RID: 11718 RVA: 0x000C684D File Offset: 0x000C4A4D
		public SqlSingle Adjust(SqlSingle value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC7 RID: 11719 RVA: 0x000C685F File Offset: 0x000C4A5F
		public SqlDouble Adjust(SqlDouble value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC8 RID: 11720 RVA: 0x000C6A0F File Offset: 0x000C4C0F
		public SqlMoney Adjust(SqlMoney value)
		{
			if (SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyMoneyRange(value);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DC9 RID: 11721 RVA: 0x000C6A3B File Offset: 0x000C4C3B
		public SqlDateTime Adjust(SqlDateTime value)
		{
			if (SqlDbType.DateTime != this.SqlDbType && SqlDbType.SmallDateTime != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyDateTimeRange(value.Value);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DCA RID: 11722 RVA: 0x000C6A6C File Offset: 0x000C4C6C
		public SqlDecimal Adjust(SqlDecimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return this.InternalAdjustSqlDecimal(value);
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DCB RID: 11723 RVA: 0x000C6A84 File Offset: 0x000C4C84
		public SqlString Adjust(SqlString value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Value.Length < this.MaxLength)
				{
					return new SqlString(value.Value.PadRight((int)this.MaxLength));
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = new SqlString(value.Value.Remove((int)this.MaxLength, (int)((long)value.Value.Length - this.MaxLength)));
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DCC RID: 11724 RVA: 0x000C6B68 File Offset: 0x000C4D68
		public SqlBinary Adjust(SqlBinary value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Length < this.MaxLength)
				{
					byte[] value2 = value.Value;
					byte[] array = new byte[this.MaxLength];
					Buffer.BlockCopy(value2, 0, array, 0, value2.Length);
					Array.Clear(array, value2.Length, array.Length - value2.Length);
					return new SqlBinary(array);
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				Array value3 = value.Value;
				byte[] array2 = new byte[this.MaxLength];
				Buffer.BlockCopy(value3, 0, array2, 0, (int)this.MaxLength);
				value = new SqlBinary(array2);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlGuid" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DCD RID: 11725 RVA: 0x000C69DA File Offset: 0x000C4BDA
		public SqlGuid Adjust(SqlGuid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlChars" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DCE RID: 11726 RVA: 0x000C6C48 File Offset: 0x000C4E48
		public SqlChars Adjust(SqlChars value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					long length = value.Length;
					if (length < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							char[] array = new char[(int)this.MaxLength];
							Array.Copy(value.Buffer, 0, array, 0, (int)length);
							value = new SqlChars(array);
						}
						char[] buffer = value.Buffer;
						for (long num = length; num < this.MaxLength; num += 1L)
						{
							buffer[(int)(checked((IntPtr)num))] = ' ';
						}
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBytes" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DCF RID: 11727 RVA: 0x000C6D54 File Offset: 0x000C4F54
		public SqlBytes Adjust(SqlBytes value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					int num = (int)value.Length;
					if ((long)num < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							byte[] array = new byte[this.MaxLength];
							Buffer.BlockCopy(value.Buffer, 0, array, 0, num);
							value = new SqlBytes(array);
						}
						byte[] buffer = value.Buffer;
						Array.Clear(buffer, num, buffer.Length - num);
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlXml" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlXml" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DD0 RID: 11728 RVA: 0x000C6E34 File Offset: 0x000C5034
		public SqlXml Adjust(SqlXml value)
		{
			if (SqlDbType.Xml != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.TimeSpan" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array of <see cref="T:System.TimeSpan" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DD1 RID: 11729 RVA: 0x000C6E46 File Offset: 0x000C5046
		public TimeSpan Adjust(TimeSpan value)
		{
			if (SqlDbType.Time != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			this.VerifyTimeRange(value);
			return new TimeSpan(this.InternalAdjustTimeTicks(value.Ticks));
		}

		/// <summary>Validates the specified <see cref="T:System.DateTimeOffset" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array of <see cref="T:System.DateTimeOffset" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DD2 RID: 11730 RVA: 0x000C6E70 File Offset: 0x000C5070
		public DateTimeOffset Adjust(DateTimeOffset value)
		{
			if (SqlDbType.DateTimeOffset != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return new DateTimeOffset(this.InternalAdjustTimeTicks(value.Ticks), value.Offset);
		}

		/// <summary>Validates the specified <see cref="T:System.Object" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Object" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DD3 RID: 11731 RVA: 0x000C6E9C File Offset: 0x000C509C
		public object Adjust(object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is bool)
			{
				value = this.Adjust((bool)value);
			}
			else if (value is byte)
			{
				value = this.Adjust((byte)value);
			}
			else if (value is char)
			{
				value = this.Adjust((char)value);
			}
			else if (value is DateTime)
			{
				value = this.Adjust((DateTime)value);
			}
			else if (!(value is DBNull))
			{
				if (value is decimal)
				{
					value = this.Adjust((decimal)value);
				}
				else if (value is double)
				{
					value = this.Adjust((double)value);
				}
				else if (value is short)
				{
					value = this.Adjust((short)value);
				}
				else if (value is int)
				{
					value = this.Adjust((int)value);
				}
				else if (value is long)
				{
					value = this.Adjust((long)value);
				}
				else
				{
					if (value is sbyte)
					{
						throw ADP.InvalidDataType("SByte");
					}
					if (value is float)
					{
						value = this.Adjust((float)value);
					}
					else if (value is string)
					{
						value = this.Adjust((string)value);
					}
					else
					{
						if (value is ushort)
						{
							throw ADP.InvalidDataType("UInt16");
						}
						if (value is uint)
						{
							throw ADP.InvalidDataType("UInt32");
						}
						if (value is ulong)
						{
							throw ADP.InvalidDataType("UInt64");
						}
						if (value is byte[])
						{
							value = this.Adjust((byte[])value);
						}
						else if (value is char[])
						{
							value = this.Adjust((char[])value);
						}
						else if (value is Guid)
						{
							value = this.Adjust((Guid)value);
						}
						else if (value is SqlBinary)
						{
							value = this.Adjust((SqlBinary)value);
						}
						else if (value is SqlBoolean)
						{
							value = this.Adjust((SqlBoolean)value);
						}
						else if (value is SqlByte)
						{
							value = this.Adjust((SqlByte)value);
						}
						else if (value is SqlDateTime)
						{
							value = this.Adjust((SqlDateTime)value);
						}
						else if (value is SqlDouble)
						{
							value = this.Adjust((SqlDouble)value);
						}
						else if (value is SqlGuid)
						{
							value = this.Adjust((SqlGuid)value);
						}
						else if (value is SqlInt16)
						{
							value = this.Adjust((SqlInt16)value);
						}
						else if (value is SqlInt32)
						{
							value = this.Adjust((SqlInt32)value);
						}
						else if (value is SqlInt64)
						{
							value = this.Adjust((SqlInt64)value);
						}
						else if (value is SqlMoney)
						{
							value = this.Adjust((SqlMoney)value);
						}
						else if (value is SqlDecimal)
						{
							value = this.Adjust((SqlDecimal)value);
						}
						else if (value is SqlSingle)
						{
							value = this.Adjust((SqlSingle)value);
						}
						else if (value is SqlString)
						{
							value = this.Adjust((SqlString)value);
						}
						else if (value is SqlChars)
						{
							value = this.Adjust((SqlChars)value);
						}
						else if (value is SqlBytes)
						{
							value = this.Adjust((SqlBytes)value);
						}
						else if (value is SqlXml)
						{
							value = this.Adjust((SqlXml)value);
						}
						else if (value is TimeSpan)
						{
							value = this.Adjust((TimeSpan)value);
						}
						else
						{
							if (!(value is DateTimeOffset))
							{
								throw ADP.UnknownDataType(value.GetType());
							}
							value = this.Adjust((DateTimeOffset)value);
						}
					}
				}
			}
			return value;
		}

		/// <summary>Infers the metadata from the specified object and returns it as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</summary>
		/// <returns>The inferred metadata as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</returns>
		/// <param name="value">The object used from which the metadata is inferred.</param>
		/// <param name="name">The name assigned to the returned <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The v<paramref name="alue" /> is null. </exception>
		// Token: 0x06002DD4 RID: 11732 RVA: 0x000C72E8 File Offset: 0x000C54E8
		public static SqlMetaData InferFromValue(object value, string name)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull("value");
			}
			SqlMetaData sqlMetaData;
			if (value is bool)
			{
				sqlMetaData = new SqlMetaData(name, SqlDbType.Bit);
			}
			else if (value is byte)
			{
				sqlMetaData = new SqlMetaData(name, SqlDbType.TinyInt);
			}
			else if (value is char)
			{
				sqlMetaData = new SqlMetaData(name, SqlDbType.NVarChar, 1L);
			}
			else if (value is DateTime)
			{
				sqlMetaData = new SqlMetaData(name, SqlDbType.DateTime);
			}
			else
			{
				if (value is DBNull)
				{
					throw ADP.InvalidDataType("DBNull");
				}
				if (value is decimal)
				{
					SqlDecimal sqlDecimal = new SqlDecimal((decimal)value);
					sqlMetaData = new SqlMetaData(name, SqlDbType.Decimal, sqlDecimal.Precision, sqlDecimal.Scale);
				}
				else if (value is double)
				{
					sqlMetaData = new SqlMetaData(name, SqlDbType.Float);
				}
				else if (value is short)
				{
					sqlMetaData = new SqlMetaData(name, SqlDbType.SmallInt);
				}
				else if (value is int)
				{
					sqlMetaData = new SqlMetaData(name, SqlDbType.Int);
				}
				else if (value is long)
				{
					sqlMetaData = new SqlMetaData(name, SqlDbType.BigInt);
				}
				else
				{
					if (value is sbyte)
					{
						throw ADP.InvalidDataType("SByte");
					}
					if (value is float)
					{
						sqlMetaData = new SqlMetaData(name, SqlDbType.Real);
					}
					else if (value is string)
					{
						long num = (long)((string)value).Length;
						if (num < 1L)
						{
							num = 1L;
						}
						if (4000L < num)
						{
							num = SqlMetaData.Max;
						}
						sqlMetaData = new SqlMetaData(name, SqlDbType.NVarChar, num);
					}
					else
					{
						if (value is ushort)
						{
							throw ADP.InvalidDataType("UInt16");
						}
						if (value is uint)
						{
							throw ADP.InvalidDataType("UInt32");
						}
						if (value is ulong)
						{
							throw ADP.InvalidDataType("UInt64");
						}
						if (value is byte[])
						{
							long num2 = (long)((byte[])value).Length;
							if (num2 < 1L)
							{
								num2 = 1L;
							}
							if (8000L < num2)
							{
								num2 = SqlMetaData.Max;
							}
							sqlMetaData = new SqlMetaData(name, SqlDbType.VarBinary, num2);
						}
						else if (value is char[])
						{
							long num3 = (long)((char[])value).Length;
							if (num3 < 1L)
							{
								num3 = 1L;
							}
							if (4000L < num3)
							{
								num3 = SqlMetaData.Max;
							}
							sqlMetaData = new SqlMetaData(name, SqlDbType.NVarChar, num3);
						}
						else if (value is Guid)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.UniqueIdentifier);
						}
						else if (value != null)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.Variant);
						}
						else if (value is SqlBinary)
						{
							SqlBinary sqlBinary = (SqlBinary)value;
							long num4;
							if (!sqlBinary.IsNull)
							{
								num4 = (long)sqlBinary.Length;
								if (num4 < 1L)
								{
									num4 = 1L;
								}
								if (8000L < num4)
								{
									num4 = SqlMetaData.Max;
								}
							}
							else
							{
								num4 = SqlMetaData.sxm_rgDefaults[21].MaxLength;
							}
							sqlMetaData = new SqlMetaData(name, SqlDbType.VarBinary, num4);
						}
						else if (value is SqlBoolean)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.Bit);
						}
						else if (value is SqlByte)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.TinyInt);
						}
						else if (value is SqlDateTime)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.DateTime);
						}
						else if (value is SqlDouble)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.Float);
						}
						else if (value is SqlGuid)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.UniqueIdentifier);
						}
						else if (value is SqlInt16)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.SmallInt);
						}
						else if (value is SqlInt32)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.Int);
						}
						else if (value is SqlInt64)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.BigInt);
						}
						else if (value is SqlMoney)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.Money);
						}
						else if (value is SqlDecimal)
						{
							SqlDecimal sqlDecimal2 = (SqlDecimal)value;
							byte b;
							byte b2;
							if (!sqlDecimal2.IsNull)
							{
								b = sqlDecimal2.Precision;
								b2 = sqlDecimal2.Scale;
							}
							else
							{
								b = SqlMetaData.sxm_rgDefaults[5].Precision;
								b2 = SqlMetaData.sxm_rgDefaults[5].Scale;
							}
							sqlMetaData = new SqlMetaData(name, SqlDbType.Decimal, b, b2);
						}
						else if (value is SqlSingle)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.Real);
						}
						else if (value is SqlString)
						{
							SqlString sqlString = (SqlString)value;
							if (!sqlString.IsNull)
							{
								long num5 = (long)sqlString.Value.Length;
								if (num5 < 1L)
								{
									num5 = 1L;
								}
								if (num5 > 4000L)
								{
									num5 = SqlMetaData.Max;
								}
								sqlMetaData = new SqlMetaData(name, SqlDbType.NVarChar, num5, (long)sqlString.LCID, sqlString.SqlCompareOptions);
							}
							else
							{
								sqlMetaData = new SqlMetaData(name, SqlDbType.NVarChar, SqlMetaData.sxm_rgDefaults[12].MaxLength);
							}
						}
						else if (value is SqlChars)
						{
							SqlChars sqlChars = (SqlChars)value;
							long num6;
							if (!sqlChars.IsNull)
							{
								num6 = sqlChars.Length;
								if (num6 < 1L)
								{
									num6 = 1L;
								}
								if (num6 > 4000L)
								{
									num6 = SqlMetaData.Max;
								}
							}
							else
							{
								num6 = SqlMetaData.sxm_rgDefaults[12].MaxLength;
							}
							sqlMetaData = new SqlMetaData(name, SqlDbType.NVarChar, num6);
						}
						else if (value is SqlBytes)
						{
							SqlBytes sqlBytes = (SqlBytes)value;
							long num7;
							if (!sqlBytes.IsNull)
							{
								num7 = sqlBytes.Length;
								if (num7 < 1L)
								{
									num7 = 1L;
								}
								else if (8000L < num7)
								{
									num7 = SqlMetaData.Max;
								}
							}
							else
							{
								num7 = SqlMetaData.sxm_rgDefaults[21].MaxLength;
							}
							sqlMetaData = new SqlMetaData(name, SqlDbType.VarBinary, num7);
						}
						else if (value is SqlXml)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.Xml);
						}
						else if (value is TimeSpan)
						{
							sqlMetaData = new SqlMetaData(name, SqlDbType.Time, 0, SqlMetaData.InferScaleFromTimeTicks(((TimeSpan)value).Ticks));
						}
						else
						{
							if (!(value is DateTimeOffset))
							{
								throw ADP.UnknownDataType(value.GetType());
							}
							sqlMetaData = new SqlMetaData(name, SqlDbType.DateTimeOffset, 0, SqlMetaData.InferScaleFromTimeTicks(((DateTimeOffset)value).Ticks));
						}
					}
				}
			}
			return sqlMetaData;
		}

		/// <summary>Validates the specified <see cref="T:System.Boolean" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Boolean" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DD5 RID: 11733 RVA: 0x000C69EC File Offset: 0x000C4BEC
		public bool Adjust(bool value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Byte" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Byte" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DD6 RID: 11734 RVA: 0x000C69FD File Offset: 0x000C4BFD
		public byte Adjust(byte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified array of <see cref="T:System.Byte" /> values against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array of <see cref="T:System.Byte" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DD7 RID: 11735 RVA: 0x000C7868 File Offset: 0x000C5A68
		public byte[] Adjust(byte[] value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					byte[] array = new byte[this.MaxLength];
					Buffer.BlockCopy(value, 0, array, 0, value.Length);
					Array.Clear(array, value.Length, array.Length - value.Length);
					return array;
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				byte[] array2 = new byte[this.MaxLength];
				Buffer.BlockCopy(value, 0, array2, 0, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Char" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Char" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DD8 RID: 11736 RVA: 0x000C791C File Offset: 0x000C5B1C
		public char Adjust(char value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (1L != this.MaxLength)
				{
					SqlMetaData.ThrowInvalidType();
				}
			}
			else if (1L > this.MaxLength || (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType))
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		/// <summary>Validates the specified array of <see cref="T:System.Char" /> values against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array <see cref="T:System.Char" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06002DD9 RID: 11737 RVA: 0x000C7988 File Offset: 0x000C5B88
		public char[] Adjust(char[] value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null)
				{
					long num = (long)value.Length;
					if (num < this.MaxLength)
					{
						char[] array = new char[(int)this.MaxLength];
						Array.Copy(value, 0, array, 0, (int)num);
						for (long num2 = num; num2 < (long)array.Length; num2 += 1L)
						{
							array[(int)(checked((IntPtr)num2))] = ' ';
						}
						return array;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				char[] array2 = new char[this.MaxLength];
				Array.Copy(value, 0, array2, 0, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000C7A58 File Offset: 0x000C5C58
		internal static SqlMetaData GetPartialLengthMetaData(SqlMetaData md)
		{
			if (md.IsPartialLength)
			{
				return md;
			}
			if (md.SqlDbType == SqlDbType.Xml)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (md.SqlDbType == SqlDbType.NVarChar || md.SqlDbType == SqlDbType.VarChar || md.SqlDbType == SqlDbType.VarBinary)
			{
				return new SqlMetaData(md.Name, md.SqlDbType, SqlMetaData.Max, 0, 0, md.LocaleId, md.CompareOptions, null, null, null, true);
			}
			return md;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000C7AC6 File Offset: 0x000C5CC6
		private static void ThrowInvalidType()
		{
			throw ADP.InvalidMetaDataValue();
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000C7ACD File Offset: 0x000C5CCD
		private void VerifyDateTimeRange(DateTime value)
		{
			if (SqlDbType.SmallDateTime == this.SqlDbType && (SqlMetaData.s_dtSmallMax < value || SqlMetaData.s_dtSmallMin > value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000C7AF8 File Offset: 0x000C5CF8
		private void VerifyMoneyRange(SqlMoney value)
		{
			if (SqlDbType.SmallMoney == this.SqlDbType && ((SqlMetaData.s_smSmallMax < value).Value || (SqlMetaData.s_smSmallMin > value).Value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000C7B40 File Offset: 0x000C5D40
		private SqlDecimal InternalAdjustSqlDecimal(SqlDecimal value)
		{
			if (!value.IsNull && (value.Precision != this.Precision || value.Scale != this.Scale))
			{
				if (value.Scale != this.Scale)
				{
					value = SqlDecimal.AdjustScale(value, (int)(this.Scale - value.Scale), false);
				}
				return SqlDecimal.ConvertToPrecScale(value, (int)this.Precision, (int)this.Scale);
			}
			return value;
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000C7BAE File Offset: 0x000C5DAE
		private void VerifyTimeRange(TimeSpan value)
		{
			if (SqlDbType.Time == this.SqlDbType && (SqlMetaData.s_timeMin > value || value > SqlMetaData.s_timeMax))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000C7BD9 File Offset: 0x000C5DD9
		private long InternalAdjustTimeTicks(long ticks)
		{
			return ticks / SqlMetaData.s_unitTicksFromScale[(int)this.Scale] * SqlMetaData.s_unitTicksFromScale[(int)this.Scale];
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000C7BF8 File Offset: 0x000C5DF8
		private static byte InferScaleFromTimeTicks(long ticks)
		{
			for (byte b = 0; b < 7; b += 1)
			{
				if (ticks / SqlMetaData.s_unitTicksFromScale[(int)b] * SqlMetaData.s_unitTicksFromScale[(int)b] == ticks)
				{
					return b;
				}
			}
			return 7;
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000C7C2C File Offset: 0x000C5E2C
		private void SetDefaultsForType(SqlDbType dbType)
		{
			if (SqlDbType.BigInt <= dbType && SqlDbType.DateTimeOffset >= dbType)
			{
				SqlMetaData sqlMetaData = SqlMetaData.sxm_rgDefaults[(int)dbType];
				this._sqlDbType = dbType;
				this._lMaxLength = sqlMetaData.MaxLength;
				this._bPrecision = sqlMetaData.Precision;
				this._bScale = sqlMetaData.Scale;
				this._lLocale = sqlMetaData.LocaleId;
				this._eCompareOptions = sqlMetaData.CompareOptions;
			}
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000C7C90 File Offset: 0x000C5E90
		private void ThrowIfUdt(SqlDbType dbType)
		{
			if (dbType == SqlDbType.Udt)
			{
				throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and user-defined type (UDT).</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared. </exception>
		// Token: 0x06002DE4 RID: 11748 RVA: 0x000C7CB8 File Offset: 0x000C5EB8
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType)
			: this(name, dbType, -1L, 0, 0, 0L, SqlCompareOptions.None, userDefinedType)
		{
		}

		/// <summary>Gets the data type of the column or parameter.</summary>
		/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		[MonoTODO]
		public DbType DbType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, user-defined type (UDT), and SQLServer type.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <param name="serverTypeName">The SQL Server type name for <paramref name="userDefinedType" />.</param>
		// Token: 0x06002DE7 RID: 11751 RVA: 0x00010468 File Offset: 0x0000E668
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, user-defined type, SQL Server type, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <param name="serverTypeName">The SQL Server type name for <paramref name="userDefinedType" />.</param>
		/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
		/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
		/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
		/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
		// Token: 0x06002DE8 RID: 11752 RVA: 0x00010468 File Offset: 0x0000E668
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the common language runtime (CLR) type of a user-defined type (UDT).</summary>
		/// <returns>The CLR type name of a user-defined type as a <see cref="T:System.Type" />.</returns>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x00056B71 File Offset: 0x00054D71
		public Type Type
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04001B0B RID: 6923
		private string _strName;

		// Token: 0x04001B0C RID: 6924
		private long _lMaxLength;

		// Token: 0x04001B0D RID: 6925
		private SqlDbType _sqlDbType;

		// Token: 0x04001B0E RID: 6926
		private byte _bPrecision;

		// Token: 0x04001B0F RID: 6927
		private byte _bScale;

		// Token: 0x04001B10 RID: 6928
		private long _lLocale;

		// Token: 0x04001B11 RID: 6929
		private SqlCompareOptions _eCompareOptions;

		// Token: 0x04001B12 RID: 6930
		private string _xmlSchemaCollectionDatabase;

		// Token: 0x04001B13 RID: 6931
		private string _xmlSchemaCollectionOwningSchema;

		// Token: 0x04001B14 RID: 6932
		private string _xmlSchemaCollectionName;

		// Token: 0x04001B15 RID: 6933
		private bool _bPartialLength;

		// Token: 0x04001B16 RID: 6934
		private bool _useServerDefault;

		// Token: 0x04001B17 RID: 6935
		private bool _isUniqueKey;

		// Token: 0x04001B18 RID: 6936
		private SortOrder _columnSortOrder;

		// Token: 0x04001B19 RID: 6937
		private int _sortOrdinal;

		// Token: 0x04001B1A RID: 6938
		private const long x_lMax = -1L;

		// Token: 0x04001B1B RID: 6939
		private const long x_lServerMaxUnicode = 4000L;

		// Token: 0x04001B1C RID: 6940
		private const long x_lServerMaxANSI = 8000L;

		// Token: 0x04001B1D RID: 6941
		private const long x_lServerMaxBinary = 8000L;

		// Token: 0x04001B1E RID: 6942
		private const bool x_defaultUseServerDefault = false;

		// Token: 0x04001B1F RID: 6943
		private const bool x_defaultIsUniqueKey = false;

		// Token: 0x04001B20 RID: 6944
		private const SortOrder x_defaultColumnSortOrder = SortOrder.Unspecified;

		// Token: 0x04001B21 RID: 6945
		private const int x_defaultSortOrdinal = -1;

		// Token: 0x04001B22 RID: 6946
		private const SqlCompareOptions x_eDefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04001B23 RID: 6947
		private static byte[] s_maxLenFromPrecision = new byte[]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 17, 17,
			17, 17, 17, 17, 17, 17, 17, 17
		};

		// Token: 0x04001B24 RID: 6948
		private const byte MaxTimeScale = 7;

		// Token: 0x04001B25 RID: 6949
		private static byte[] s_maxVarTimeLenOffsetFromScale = new byte[] { 2, 2, 2, 1, 1, 0, 0, 0 };

		// Token: 0x04001B26 RID: 6950
		private static readonly DateTime s_dtSmallMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04001B27 RID: 6951
		private static readonly DateTime s_dtSmallMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x04001B28 RID: 6952
		private static readonly SqlMoney s_smSmallMax = new SqlMoney(214748.3647m);

		// Token: 0x04001B29 RID: 6953
		private static readonly SqlMoney s_smSmallMin = new SqlMoney(-214748.3648m);

		// Token: 0x04001B2A RID: 6954
		private static readonly TimeSpan s_timeMin = TimeSpan.Zero;

		// Token: 0x04001B2B RID: 6955
		private static readonly TimeSpan s_timeMax = new TimeSpan(863999999999L);

		// Token: 0x04001B2C RID: 6956
		private static readonly long[] s_unitTicksFromScale = new long[] { 10000000L, 1000000L, 100000L, 10000L, 1000L, 100L, 10L, 1L };

		// Token: 0x04001B2D RID: 6957
		internal static SqlMetaData[] sxm_rgDefaults = new SqlMetaData[]
		{
			new SqlMetaData("bigint", SqlDbType.BigInt, 8L, 19, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("binary", SqlDbType.Binary, 1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("bit", SqlDbType.Bit, 1L, 1, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("char", SqlDbType.Char, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("datetime", SqlDbType.DateTime, 8L, 23, 3, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("decimal", SqlDbType.Decimal, 9L, 18, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("float", SqlDbType.Float, 8L, 53, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("image", SqlDbType.Image, -1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("int", SqlDbType.Int, 4L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("money", SqlDbType.Money, 8L, 19, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nchar", SqlDbType.NChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("ntext", SqlDbType.NText, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("real", SqlDbType.Real, 4L, 24, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("uniqueidentifier", SqlDbType.UniqueIdentifier, 16L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smalldatetime", SqlDbType.SmallDateTime, 4L, 16, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallint", SqlDbType.SmallInt, 2L, 5, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallmoney", SqlDbType.SmallMoney, 4L, 10, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("text", SqlDbType.Text, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("timestamp", SqlDbType.Timestamp, 8L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("tinyint", SqlDbType.TinyInt, 1L, 3, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varbinary", SqlDbType.VarBinary, 8000L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varchar", SqlDbType.VarChar, 8000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("sql_variant", SqlDbType.Variant, 8016L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("xml", SqlDbType.Xml, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, true),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("udt", SqlDbType.Structured, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("table", SqlDbType.Structured, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("date", SqlDbType.Date, 3L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("time", SqlDbType.Time, 5L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetime2", SqlDbType.DateTime2, 8L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetimeoffset", SqlDbType.DateTimeOffset, 10L, 0, 7, 0L, SqlCompareOptions.None, false)
		};
	}
}
