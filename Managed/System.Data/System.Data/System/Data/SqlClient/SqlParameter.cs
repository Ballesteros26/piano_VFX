using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.SqlServer.Server;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a parameter to a <see cref="T:System.Data.SqlClient.SqlCommand" /> and optionally its mapping to <see cref="T:System.Data.DataSet" /> columns. This class cannot be inherited. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001D5 RID: 469
	public sealed class SqlParameter : DbParameter, IDbDataParameter, IDataParameter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class.</summary>
		// Token: 0x06001588 RID: 5512 RVA: 0x0006C0BE File Offset: 0x0006A2BE
		public SqlParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name and the data type.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001589 RID: 5513 RVA: 0x0006C0D4 File Offset: 0x0006A2D4
		public SqlParameter(string parameterName, SqlDbType dbType)
			: this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name and a value of the new <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.SqlClient.SqlParameter" />. </param>
		// Token: 0x0600158A RID: 5514 RVA: 0x0006C0EA File Offset: 0x0006A2EA
		public SqlParameter(string parameterName, object value)
			: this()
		{
			this.ParameterName = parameterName;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the <see cref="T:System.Data.SqlDbType" />, and the size.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x0600158B RID: 5515 RVA: 0x0006C100 File Offset: 0x0006A300
		public SqlParameter(string parameterName, SqlDbType dbType, int size)
			: this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the <see cref="T:System.Data.SqlDbType" />, the size, and the source column name.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="sourceColumn">The name of the source column (<see cref="P:System.Data.SqlClient.SqlParameter.SourceColumn" />) if this <see cref="T:System.Data.SqlClient.SqlParameter" /> is used in a call to <see cref="Overload:System.Data.Common.DbDataAdapter.Update" />.</param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x0600158C RID: 5516 RVA: 0x0006C11D File Offset: 0x0006A31D
		public SqlParameter(string parameterName, SqlDbType dbType, int size, string sourceColumn)
			: this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
			this.SourceColumn = sourceColumn;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the type of the parameter, the size of the parameter, a <see cref="T:System.Data.ParameterDirection" />, the precision of the parameter, the scale of the parameter, the source column, a <see cref="T:System.Data.DataRowVersion" /> to use, and the value of the parameter.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="isNullable">true if the value of the field can be null; otherwise false. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved. </param>
		/// <param name="sourceColumn">The name of the source column (<see cref="P:System.Data.SqlClient.SqlParameter.SourceColumn" />) if this <see cref="T:System.Data.SqlClient.SqlParameter" /> is used in a call to <see cref="Overload:System.Data.Common.DbDataAdapter.Update" />.</param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.SqlClient.SqlParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x0600158D RID: 5517 RVA: 0x0006C142 File Offset: 0x0006A342
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
			: this(parameterName, dbType, size, sourceColumn)
		{
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.Precision = precision;
			this.Scale = scale;
			this.SourceVersion = sourceVersion;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the type of the parameter, the length of the parameter the direction, the precision, the scale, the name of the source column, one of the <see cref="T:System.Data.DataRowVersion" /> values, a Boolean for source column mapping, the value of the SqlParameter, the name of the database where the schema collection for this XML instance is located, the owning relational schema where the schema collection for this XML instance is located, and the name of the schema collection for this parameter.</summary>
		/// <param name="parameterName">The name of the parameter to map.</param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</param>
		/// <param name="sourceColumn">The name of the source column (<see cref="P:System.Data.SqlClient.SqlParameter.SourceColumn" />) if this <see cref="T:System.Data.SqlClient.SqlParameter" /> is used in a call to <see cref="Overload:System.Data.Common.DbDataAdapter.Update" />.</param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="sourceColumnNullMapping">true if the source column is nullable; false if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.SqlClient.SqlParameter" />.</param>
		/// <param name="xmlSchemaCollectionDatabase">The name of the database where the schema collection for this XML instance is located.</param>
		/// <param name="xmlSchemaCollectionOwningSchema">The owning relational schema where the schema collection for this XML instance is located.</param>
		/// <param name="xmlSchemaCollectionName">The name of the schema collection for this parameter.</param>
		// Token: 0x0600158E RID: 5518 RVA: 0x0006C180 File Offset: 0x0006A380
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName)
			: this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
			this.Direction = direction;
			this.Precision = precision;
			this.Scale = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
			this.XmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
			this.XmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
			this.XmlSchemaCollectionName = xmlSchemaCollectionName;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0006C1F8 File Offset: 0x0006A3F8
		private SqlParameter(SqlParameter source)
			: this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0006C238 File Offset: 0x0006A438
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x0006C240 File Offset: 0x0006A440
		internal SqlCollation Collation
		{
			get
			{
				return this._collation;
			}
			set
			{
				this._collation = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Globalization.CompareInfo" /> object that defines how string comparisons should be performed for this parameter.</summary>
		/// <returns>A <see cref="T:System.Globalization.CompareInfo" /> object that defines string comparison for this parameter.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0006C24C File Offset: 0x0006A44C
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x0006C26C File Offset: 0x0006A46C
		public SqlCompareOptions CompareInfo
		{
			get
			{
				SqlCollation collation = this._collation;
				if (collation != null)
				{
					return collation.SqlCompareOptions;
				}
				return SqlCompareOptions.None;
			}
			set
			{
				SqlCollation sqlCollation = this._collation;
				if (sqlCollation == null)
				{
					sqlCollation = (this._collation = new SqlCollation());
				}
				if ((value & (SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2)) != value)
				{
					throw ADP.ArgumentOutOfRange("CompareInfo");
				}
				sqlCollation.SqlCompareOptions = value;
			}
		}

		/// <summary>Gets the name of the database where the schema collection for this XML instance is located.</summary>
		/// <returns>The name of the database where the schema collection for this XML instance is located.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x0006C2AC File Offset: 0x0006A4AC
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x0006C2CA File Offset: 0x0006A4CA
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				string xmlSchemaCollectionDatabase = this._xmlSchemaCollectionDatabase;
				if (xmlSchemaCollectionDatabase == null)
				{
					return ADP.StrEmpty;
				}
				return xmlSchemaCollectionDatabase;
			}
			set
			{
				this._xmlSchemaCollectionDatabase = value;
			}
		}

		/// <summary>The owning relational schema where the schema collection for this XML instance is located.</summary>
		/// <returns>The owning relational schema for this XML instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0006C2D4 File Offset: 0x0006A4D4
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x0006C2F2 File Offset: 0x0006A4F2
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				string xmlSchemaCollectionOwningSchema = this._xmlSchemaCollectionOwningSchema;
				if (xmlSchemaCollectionOwningSchema == null)
				{
					return ADP.StrEmpty;
				}
				return xmlSchemaCollectionOwningSchema;
			}
			set
			{
				this._xmlSchemaCollectionOwningSchema = value;
			}
		}

		/// <summary>Gets the name of the schema collection for this XML instance.</summary>
		/// <returns>The name of the schema collection for this XML instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0006C2FC File Offset: 0x0006A4FC
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x0006C31A File Offset: 0x0006A51A
		public string XmlSchemaCollectionName
		{
			get
			{
				string xmlSchemaCollectionName = this._xmlSchemaCollectionName;
				if (xmlSchemaCollectionName == null)
				{
					return ADP.StrEmpty;
				}
				return xmlSchemaCollectionName;
			}
			set
			{
				this._xmlSchemaCollectionName = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlDbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.SqlDbType" /> values. The default is NVarChar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0006C323 File Offset: 0x0006A523
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x0006C330 File Offset: 0x0006A530
		public override DbType DbType
		{
			get
			{
				return this.GetMetaTypeOnly().DbType;
			}
			set
			{
				MetaType metaType = this._metaType;
				if (metaType == null || metaType.DbType != value || value == DbType.Date || value == DbType.Time)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeFromDbType(value);
				}
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600159C RID: 5532 RVA: 0x0006C36B File Offset: 0x0006A56B
		public override void ResetDbType()
		{
			this.ResetSqlDbType();
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0006C373 File Offset: 0x0006A573
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x0006C37B File Offset: 0x0006A57B
		internal MetaType InternalMetaType
		{
			get
			{
				return this._internalMetaType;
			}
			set
			{
				this._internalMetaType = value;
			}
		}

		/// <summary>Gets or sets the locale identifier that determines conventions and language for a particular region.</summary>
		/// <returns>Returns the locale identifier associated with the parameter.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0006C384 File Offset: 0x0006A584
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x0006C3A4 File Offset: 0x0006A5A4
		public int LocaleId
		{
			get
			{
				SqlCollation collation = this._collation;
				if (collation != null)
				{
					return collation.LCID;
				}
				return 0;
			}
			set
			{
				SqlCollation sqlCollation = this._collation;
				if (sqlCollation == null)
				{
					sqlCollation = (this._collation = new SqlCollation());
				}
				if ((long)value != (1048575L & (long)value))
				{
					throw ADP.ArgumentOutOfRange("LocaleId");
				}
				sqlCollation.LCID = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0006C3E7 File Offset: 0x0006A5E7
		internal bool SizeInferred
		{
			get
			{
				return this._size == 0;
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0006C3F4 File Offset: 0x0006A5F4
		internal SmiParameterMetaData MetaDataForSmi(out ParameterPeekAheadValue peekAhead)
		{
			peekAhead = null;
			MetaType metaType = this.ValidateTypeLengths();
			long num = (long)this.GetActualSize();
			long num2 = (long)this.Size;
			if (!metaType.IsLong)
			{
				if (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)
				{
					num /= 2L;
				}
				if (num > num2)
				{
					num2 = num;
				}
			}
			if (num2 == 0L)
			{
				if (SqlDbType.Binary == metaType.SqlDbType || SqlDbType.VarBinary == metaType.SqlDbType)
				{
					num2 = 8000L;
				}
				else if (SqlDbType.Char == metaType.SqlDbType || SqlDbType.VarChar == metaType.SqlDbType)
				{
					num2 = 8000L;
				}
				else if (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)
				{
					num2 = 4000L;
				}
			}
			else if ((num2 > 8000L && (SqlDbType.Binary == metaType.SqlDbType || SqlDbType.VarBinary == metaType.SqlDbType)) || (num2 > 8000L && (SqlDbType.Char == metaType.SqlDbType || SqlDbType.VarChar == metaType.SqlDbType)) || (num2 > 4000L && (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)))
			{
				num2 = -1L;
			}
			int num3 = this.LocaleId;
			if (num3 == 0 && metaType.IsCharType)
			{
				object coercedValue = this.GetCoercedValue();
				if (coercedValue is SqlString && !((SqlString)coercedValue).IsNull)
				{
					num3 = ((SqlString)coercedValue).LCID;
				}
				else
				{
					num3 = CultureInfo.CurrentCulture.LCID;
				}
			}
			SqlCompareOptions sqlCompareOptions = this.CompareInfo;
			if (sqlCompareOptions == SqlCompareOptions.None && metaType.IsCharType)
			{
				object coercedValue2 = this.GetCoercedValue();
				if (coercedValue2 is SqlString && !((SqlString)coercedValue2).IsNull)
				{
					sqlCompareOptions = ((SqlString)coercedValue2).SqlCompareOptions;
				}
				else
				{
					sqlCompareOptions = SmiMetaData.GetDefaultForType(metaType.SqlDbType).CompareOptions;
				}
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			if (SqlDbType.Xml == metaType.SqlDbType)
			{
				text = this.XmlSchemaCollectionDatabase;
				text2 = this.XmlSchemaCollectionOwningSchema;
				text3 = this.XmlSchemaCollectionName;
			}
			else if (SqlDbType.Udt == metaType.SqlDbType || (SqlDbType.Structured == metaType.SqlDbType && !string.IsNullOrEmpty(this.TypeName)))
			{
				if (SqlDbType.Udt == metaType.SqlDbType)
				{
					throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
				}
				string[] array = SqlParameter.ParseTypeName(this.TypeName);
				if (1 == array.Length)
				{
					text3 = array[0];
				}
				else if (2 == array.Length)
				{
					text2 = array[0];
					text3 = array[1];
				}
				else
				{
					if (3 != array.Length)
					{
						throw ADP.ArgumentOutOfRange("names");
					}
					text = array[0];
					text2 = array[1];
					text3 = array[2];
				}
				if ((!string.IsNullOrEmpty(text) && 255 < text.Length) || (!string.IsNullOrEmpty(text2) && 255 < text2.Length) || (!string.IsNullOrEmpty(text3) && 255 < text3.Length))
				{
					throw ADP.ArgumentOutOfRange("names");
				}
			}
			byte b = this.GetActualPrecision();
			byte actualScale = this.GetActualScale();
			if (SqlDbType.Decimal == metaType.SqlDbType && b == 0)
			{
				b = 29;
			}
			List<SmiExtendedMetaData> list = null;
			SmiMetaDataPropertyCollection smiMetaDataPropertyCollection = null;
			if (SqlDbType.Structured == metaType.SqlDbType)
			{
				this.GetActualFieldsAndProperties(out list, out smiMetaDataPropertyCollection, out peekAhead);
			}
			return new SmiParameterMetaData(metaType.SqlDbType, num2, b, actualScale, (long)num3, sqlCompareOptions, SqlDbType.Structured == metaType.SqlDbType, list, smiMetaDataPropertyCollection, this.ParameterNameFixed, text, text2, text3, this.Direction);
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0006C734 File Offset: 0x0006A934
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x0006C73C File Offset: 0x0006A93C
		internal bool ParameterIsSqlType
		{
			get
			{
				return this._isSqlParameterSqlType;
			}
			set
			{
				this._isSqlParameterSqlType = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.SqlClient.SqlParameter" />. The default is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0006C748 File Offset: 0x0006A948
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x0006C768 File Offset: 0x0006A968
		public override string ParameterName
		{
			get
			{
				string parameterName = this._parameterName;
				if (parameterName == null)
				{
					return ADP.StrEmpty;
				}
				return parameterName;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && value.Length >= 128 && ('@' != value[0] || value.Length > 128))
				{
					throw SQL.InvalidParameterNameLength(value);
				}
				if (this._parameterName != value)
				{
					this.PropertyChanging();
					this._parameterName = value;
					return;
				}
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0006C7C8 File Offset: 0x0006A9C8
		internal string ParameterNameFixed
		{
			get
			{
				string text = this.ParameterName;
				if (0 < text.Length && '@' != text[0])
				{
					text = "@" + text;
				}
				return text;
			}
		}

		/// <summary>Gets or sets the maximum number of digits used to represent the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> property. The default value is 0. This indicates that the data provider sets the precision for <see cref="P:System.Data.SqlClient.SqlParameter.Value" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0006C7FD File Offset: 0x0006A9FD
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x0006C805 File Offset: 0x0006AA05
		public override byte Precision
		{
			get
			{
				return this.PrecisionInternal;
			}
			set
			{
				this.PrecisionInternal = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x0006C810 File Offset: 0x0006AA10
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x0006C840 File Offset: 0x0006AA40
		internal byte PrecisionInternal
		{
			get
			{
				byte b = this._precision;
				SqlDbType metaSqlDbTypeOnly = this.GetMetaSqlDbTypeOnly();
				if (b == 0 && SqlDbType.Decimal == metaSqlDbTypeOnly)
				{
					b = this.ValuePrecision(this.SqlValue);
				}
				return b;
			}
			set
			{
				if (this.SqlDbType == SqlDbType.Decimal && value > 38)
				{
					throw SQL.PrecisionValueOutOfRange(value);
				}
				if (this._precision != value)
				{
					this.PropertyChanging();
					this._precision = value;
				}
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0006C86D File Offset: 0x0006AA6D
		private bool ShouldSerializePrecision()
		{
			return this._precision > 0;
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0006C878 File Offset: 0x0006AA78
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x0006C880 File Offset: 0x0006AA80
		public override byte Scale
		{
			get
			{
				return this.ScaleInternal;
			}
			set
			{
				this.ScaleInternal = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0006C88C File Offset: 0x0006AA8C
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x0006C8BC File Offset: 0x0006AABC
		internal byte ScaleInternal
		{
			get
			{
				byte b = this._scale;
				SqlDbType metaSqlDbTypeOnly = this.GetMetaSqlDbTypeOnly();
				if (b == 0 && SqlDbType.Decimal == metaSqlDbTypeOnly)
				{
					b = this.ValueScale(this.SqlValue);
				}
				return b;
			}
			set
			{
				if (this._scale != value || !this._hasScale)
				{
					this.PropertyChanging();
					this._scale = value;
					this._hasScale = true;
					this._actualSize = -1;
				}
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0006C8EA File Offset: 0x0006AAEA
		private bool ShouldSerializeScale()
		{
			return this._scale > 0;
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlDbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.SqlDbType" /> values. The default is NVarChar.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0006C8F5 File Offset: 0x0006AAF5
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0006C904 File Offset: 0x0006AB04
		public SqlDbType SqlDbType
		{
			get
			{
				return this.GetMetaTypeOnly().SqlDbType;
			}
			set
			{
				MetaType metaType = this._metaType;
				if ((SqlDbType)24 == value)
				{
					throw SQL.InvalidSqlDbType(value);
				}
				if (metaType == null || metaType.SqlDbType != value)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeFromSqlDbType(value, value == SqlDbType.Structured);
				}
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0006C947 File Offset: 0x0006AB47
		private bool ShouldSerializeSqlDbType()
		{
			return this._metaType != null;
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015B5 RID: 5557 RVA: 0x0006C952 File Offset: 0x0006AB52
		public void ResetSqlDbType()
		{
			if (this._metaType != null)
			{
				this.PropertyTypeChanging();
				this._metaType = null;
			}
		}

		/// <summary>Gets or sets the value of the parameter as an SQL type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter, using SQL types. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0006C96C File Offset: 0x0006AB6C
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x0006C9FE File Offset: 0x0006ABFE
		public object SqlValue
		{
			get
			{
				if (this._value != null)
				{
					if (this._value == DBNull.Value)
					{
						return MetaType.GetNullSqlValue(this.GetMetaTypeOnly().SqlType);
					}
					if (this._value is INullable)
					{
						return this._value;
					}
					if (this._value is DateTime)
					{
						SqlDbType sqlDbType = this.GetMetaTypeOnly().SqlDbType;
						if (sqlDbType == SqlDbType.Date || sqlDbType == SqlDbType.DateTime2)
						{
							return this._value;
						}
					}
					return MetaType.GetSqlValueFromComVariant(this._value);
				}
				else
				{
					if (this._sqlBufferReturnValue != null)
					{
						return this._sqlBufferReturnValue.SqlValue;
					}
					return null;
				}
			}
			set
			{
				this.Value = value;
			}
		}

		/// <summary>Gets or sets the type name for a table-valued parameter.</summary>
		/// <returns>The type name of the specified table-valued parameter.</returns>
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0006CA08 File Offset: 0x0006AC08
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x0006CA26 File Offset: 0x0006AC26
		public string TypeName
		{
			get
			{
				string typeName = this._typeName;
				if (typeName == null)
				{
					return ADP.StrEmpty;
				}
				return typeName;
			}
			set
			{
				this._typeName = value;
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x0006CA2F File Offset: 0x0006AC2F
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x0006CA6C File Offset: 0x0006AC6C
		public override object Value
		{
			get
			{
				if (this._value != null)
				{
					return this._value;
				}
				if (this._sqlBufferReturnValue == null)
				{
					return null;
				}
				if (this.ParameterIsSqlType)
				{
					return this._sqlBufferReturnValue.SqlValue;
				}
				return this._sqlBufferReturnValue.Value;
			}
			set
			{
				this._value = value;
				this._sqlBufferReturnValue = null;
				this._coercedValue = null;
				this._valueAsINullable = this._value as INullable;
				this._isSqlParameterSqlType = this._valueAsINullable != null;
				this._isNull = this._value == null || this._value == DBNull.Value || (this._isSqlParameterSqlType && this._valueAsINullable.IsNull);
				this._actualSize = -1;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0006CAE9 File Offset: 0x0006ACE9
		internal INullable ValueAsINullable
		{
			get
			{
				return this._valueAsINullable;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0006CAF4 File Offset: 0x0006ACF4
		internal bool IsNull
		{
			get
			{
				if (this._internalMetaType.SqlDbType == SqlDbType.Udt)
				{
					this._isNull = this._value == null || this._value == DBNull.Value || (this._isSqlParameterSqlType && this._valueAsINullable.IsNull);
				}
				return this._isNull;
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0006CB4C File Offset: 0x0006AD4C
		internal int GetActualSize()
		{
			MetaType metaType = this.InternalMetaType;
			SqlDbType sqlDbType = metaType.SqlDbType;
			if (this._actualSize == -1 || sqlDbType == SqlDbType.Udt)
			{
				this._actualSize = 0;
				object coercedValue = this.GetCoercedValue();
				bool flag = false;
				if (this.IsNull && !metaType.IsVarTime)
				{
					return 0;
				}
				if (sqlDbType == SqlDbType.Variant)
				{
					metaType = MetaType.GetMetaTypeFromValue(coercedValue, true, false);
					sqlDbType = MetaType.GetSqlDataType((int)metaType.TDSType, 0U, 0).SqlDbType;
					flag = true;
				}
				if (metaType.IsFixed)
				{
					this._actualSize = metaType.FixedLength;
				}
				else
				{
					int num = 0;
					if (sqlDbType <= SqlDbType.Char)
					{
						if (sqlDbType == SqlDbType.Binary)
						{
							goto IL_01E8;
						}
						if (sqlDbType != SqlDbType.Char)
						{
							goto IL_02BE;
						}
					}
					else
					{
						if (sqlDbType != SqlDbType.Image)
						{
							if (sqlDbType - SqlDbType.NChar > 2)
							{
								switch (sqlDbType)
								{
								case SqlDbType.Text:
								case SqlDbType.VarChar:
									goto IL_0175;
								case SqlDbType.Timestamp:
								case SqlDbType.VarBinary:
									goto IL_01E8;
								case SqlDbType.TinyInt:
								case SqlDbType.Variant:
								case (SqlDbType)24:
								case (SqlDbType)26:
								case (SqlDbType)27:
								case (SqlDbType)28:
								case SqlDbType.Date:
									goto IL_02BE;
								case SqlDbType.Xml:
									break;
								case SqlDbType.Udt:
									throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
								case SqlDbType.Structured:
									num = -1;
									goto IL_02BE;
								case SqlDbType.Time:
									this._actualSize = (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_02BE;
								case SqlDbType.DateTime2:
									this._actualSize = 3 + (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_02BE;
								case SqlDbType.DateTimeOffset:
									this._actualSize = 5 + (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_02BE;
								default:
									goto IL_02BE;
								}
							}
							num = ((!this._isNull && !this._coercedValueIsDataFeed) ? SqlParameter.StringSize(coercedValue, this._coercedValueIsSqlType) : 0);
							this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
							this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
							if (this._actualSize == -1)
							{
								this._actualSize = num;
							}
							this._actualSize <<= 1;
							goto IL_02BE;
						}
						goto IL_01E8;
					}
					IL_0175:
					num = ((!this._isNull && !this._coercedValueIsDataFeed) ? SqlParameter.StringSize(coercedValue, this._coercedValueIsSqlType) : 0);
					this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
					this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
					if (this._actualSize == -1)
					{
						this._actualSize = num;
						goto IL_02BE;
					}
					goto IL_02BE;
					IL_01E8:
					num = ((!this._isNull && !this._coercedValueIsDataFeed) ? SqlParameter.BinarySize(coercedValue, this._coercedValueIsSqlType) : 0);
					this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
					this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
					if (this._actualSize == -1)
					{
						this._actualSize = num;
					}
					IL_02BE:
					if (flag && num > 8000)
					{
						throw SQL.ParameterInvalidVariant(this.ParameterName);
					}
				}
			}
			return this._actualSize;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0006CE38 File Offset: 0x0006B038
		internal static object CoerceValue(object value, MetaType destinationType, out bool coercedToDataFeed, out bool typeChanged, bool allowStreaming = true)
		{
			coercedToDataFeed = false;
			typeChanged = false;
			Type type = value.GetType();
			if (typeof(object) != destinationType.ClassType && type != destinationType.ClassType && (type != destinationType.SqlType || SqlDbType.Xml == destinationType.SqlDbType))
			{
				try
				{
					typeChanged = true;
					if (typeof(string) == destinationType.ClassType)
					{
						if (typeof(SqlXml) == type)
						{
							value = MetaType.GetStringFromXml(((SqlXml)value).CreateReader());
						}
						else if (typeof(SqlString) == type)
						{
							typeChanged = false;
						}
						else if (typeof(XmlReader).IsAssignableFrom(type))
						{
							if (allowStreaming)
							{
								coercedToDataFeed = true;
								value = new XmlDataFeed((XmlReader)value);
							}
							else
							{
								value = MetaType.GetStringFromXml((XmlReader)value);
							}
						}
						else if (typeof(char[]) == type)
						{
							value = new string((char[])value);
						}
						else if (typeof(SqlChars) == type)
						{
							value = new string(((SqlChars)value).Value);
						}
						else if (value is TextReader && allowStreaming)
						{
							coercedToDataFeed = true;
							value = new TextDataFeed((TextReader)value);
						}
						else
						{
							value = Convert.ChangeType(value, destinationType.ClassType, null);
						}
					}
					else if (DbType.Currency == destinationType.DbType && typeof(string) == type)
					{
						value = decimal.Parse((string)value, NumberStyles.Currency, null);
					}
					else if (typeof(SqlBytes) == type && typeof(byte[]) == destinationType.ClassType)
					{
						typeChanged = false;
					}
					else if (typeof(string) == type && SqlDbType.Time == destinationType.SqlDbType)
					{
						value = TimeSpan.Parse((string)value);
					}
					else if (typeof(string) == type && SqlDbType.DateTimeOffset == destinationType.SqlDbType)
					{
						value = DateTimeOffset.Parse((string)value, null);
					}
					else if (typeof(DateTime) == type && SqlDbType.DateTimeOffset == destinationType.SqlDbType)
					{
						value = new DateTimeOffset((DateTime)value);
					}
					else if (243 == destinationType.TDSType && (value is DataTable || value is DbDataReader || value is IEnumerable<SqlDataRecord>))
					{
						typeChanged = false;
					}
					else if (destinationType.ClassType == typeof(byte[]) && value is Stream && allowStreaming)
					{
						coercedToDataFeed = true;
						value = new StreamDataFeed((Stream)value);
					}
					else
					{
						value = Convert.ChangeType(value, destinationType.ClassType, null);
					}
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					throw ADP.ParameterConversionFailed(value, destinationType.ClassType, ex);
				}
			}
			return value;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0006D168 File Offset: 0x0006B368
		internal void FixStreamDataForNonPLP()
		{
			object coercedValue = this.GetCoercedValue();
			if (!this._coercedValueIsDataFeed)
			{
				return;
			}
			this._coercedValueIsDataFeed = false;
			if (coercedValue is TextDataFeed)
			{
				if (this.Size > 0)
				{
					char[] array = new char[this.Size];
					int num = ((TextDataFeed)coercedValue)._source.ReadBlock(array, 0, this.Size);
					this.CoercedValue = new string(array, 0, num);
					return;
				}
				this.CoercedValue = ((TextDataFeed)coercedValue)._source.ReadToEnd();
				return;
			}
			else if (coercedValue is StreamDataFeed)
			{
				if (this.Size > 0)
				{
					byte[] array2 = new byte[this.Size];
					int i = 0;
					Stream source = ((StreamDataFeed)coercedValue)._source;
					while (i < this.Size)
					{
						int num2 = source.Read(array2, i, this.Size - i);
						if (num2 == 0)
						{
							break;
						}
						i += num2;
					}
					if (i < this.Size)
					{
						Array.Resize<byte>(ref array2, i);
					}
					this.CoercedValue = array2;
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				((StreamDataFeed)coercedValue)._source.CopyTo(memoryStream);
				this.CoercedValue = memoryStream.ToArray();
				return;
			}
			else
			{
				if (coercedValue is XmlDataFeed)
				{
					this.CoercedValue = MetaType.GetStringFromXml(((XmlDataFeed)coercedValue)._source);
					return;
				}
				return;
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0006D2A5 File Offset: 0x0006B4A5
		internal byte GetActualPrecision()
		{
			if (!this.ShouldSerializePrecision())
			{
				return this.ValuePrecision(this.CoercedValue);
			}
			return this.PrecisionInternal;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0006D2C2 File Offset: 0x0006B4C2
		internal byte GetActualScale()
		{
			if (this.ShouldSerializeScale())
			{
				return this.ScaleInternal;
			}
			if (this.GetMetaTypeOnly().IsVarTime)
			{
				return 7;
			}
			return this.ValueScale(this.CoercedValue);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0006D2EE File Offset: 0x0006B4EE
		internal int GetParameterSize()
		{
			if (!this.ShouldSerializeSize())
			{
				return this.ValueSize(this.CoercedValue);
			}
			return this.Size;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0006D30C File Offset: 0x0006B50C
		private void GetActualFieldsAndProperties(out List<SmiExtendedMetaData> fields, out SmiMetaDataPropertyCollection props, out ParameterPeekAheadValue peekAhead)
		{
			fields = null;
			props = null;
			peekAhead = null;
			object coercedValue = this.GetCoercedValue();
			DataTable dataTable;
			if ((dataTable = coercedValue as DataTable) != null)
			{
				if (dataTable.Columns.Count <= 0)
				{
					throw SQL.NotEnoughColumnsInStructuredType();
				}
				fields = new List<SmiExtendedMetaData>(dataTable.Columns.Count);
				bool[] array = new bool[dataTable.Columns.Count];
				bool flag = false;
				if (dataTable.PrimaryKey != null && dataTable.PrimaryKey.Length != 0)
				{
					foreach (DataColumn dataColumn in dataTable.PrimaryKey)
					{
						array[dataColumn.Ordinal] = true;
						flag = true;
					}
				}
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					fields.Add(MetaDataUtilsSmi.SmiMetaDataFromDataColumn(dataTable.Columns[j], dataTable));
					if (!flag && dataTable.Columns[j].Unique)
					{
						array[j] = true;
						flag = true;
					}
				}
				if (flag)
				{
					props = new SmiMetaDataPropertyCollection();
					props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array));
					return;
				}
			}
			else if (coercedValue is SqlDataReader)
			{
				fields = new List<SmiExtendedMetaData>(((SqlDataReader)coercedValue).GetInternalSmiMetaData());
				if (fields.Count <= 0)
				{
					throw SQL.NotEnoughColumnsInStructuredType();
				}
				bool[] array2 = new bool[fields.Count];
				bool flag2 = false;
				for (int k = 0; k < fields.Count; k++)
				{
					SmiQueryMetaData smiQueryMetaData = fields[k] as SmiQueryMetaData;
					if (smiQueryMetaData != null && !smiQueryMetaData.IsKey.IsNull && smiQueryMetaData.IsKey.Value)
					{
						array2[k] = true;
						flag2 = true;
					}
				}
				if (flag2)
				{
					props = new SmiMetaDataPropertyCollection();
					props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array2));
					return;
				}
			}
			else
			{
				if (coercedValue is IEnumerable<SqlDataRecord>)
				{
					IEnumerator<SqlDataRecord> enumerator = ((IEnumerable<SqlDataRecord>)coercedValue).GetEnumerator();
					try
					{
						if (!enumerator.MoveNext())
						{
							throw SQL.IEnumerableOfSqlDataRecordHasNoRows();
						}
						SqlDataRecord sqlDataRecord = enumerator.Current;
						int fieldCount = sqlDataRecord.FieldCount;
						if (0 < fieldCount)
						{
							bool[] array3 = new bool[fieldCount];
							bool[] array4 = new bool[fieldCount];
							bool[] array5 = new bool[fieldCount];
							int num = -1;
							bool flag3 = false;
							bool flag4 = false;
							int num2 = 0;
							SmiOrderProperty.SmiColumnOrder[] array6 = new SmiOrderProperty.SmiColumnOrder[fieldCount];
							fields = new List<SmiExtendedMetaData>(fieldCount);
							for (int l = 0; l < fieldCount; l++)
							{
								SqlMetaData sqlMetaData = sqlDataRecord.GetSqlMetaData(l);
								fields.Add(MetaDataUtilsSmi.SqlMetaDataToSmiExtendedMetaData(sqlMetaData));
								if (sqlMetaData.IsUniqueKey)
								{
									array3[l] = true;
									flag3 = true;
								}
								if (sqlMetaData.UseServerDefault)
								{
									array4[l] = true;
									flag4 = true;
								}
								array6[l].Order = sqlMetaData.SortOrder;
								if (SortOrder.Unspecified != sqlMetaData.SortOrder)
								{
									if (fieldCount <= sqlMetaData.SortOrdinal)
									{
										throw SQL.SortOrdinalGreaterThanFieldCount(l, sqlMetaData.SortOrdinal);
									}
									if (array5[sqlMetaData.SortOrdinal])
									{
										throw SQL.DuplicateSortOrdinal(sqlMetaData.SortOrdinal);
									}
									array6[l].SortOrdinal = sqlMetaData.SortOrdinal;
									array5[sqlMetaData.SortOrdinal] = true;
									if (sqlMetaData.SortOrdinal > num)
									{
										num = sqlMetaData.SortOrdinal;
									}
									num2++;
								}
							}
							if (flag3)
							{
								props = new SmiMetaDataPropertyCollection();
								props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array3));
							}
							if (flag4)
							{
								if (props == null)
								{
									props = new SmiMetaDataPropertyCollection();
								}
								props[SmiPropertySelector.DefaultFields] = new SmiDefaultFieldsProperty(new List<bool>(array4));
							}
							if (0 < num2)
							{
								if (num >= num2)
								{
									int num3 = 0;
									while (num3 < num2 && array5[num3])
									{
										num3++;
									}
									throw SQL.MissingSortOrdinal(num3);
								}
								if (props == null)
								{
									props = new SmiMetaDataPropertyCollection();
								}
								props[SmiPropertySelector.SortOrder] = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>(array6));
							}
							peekAhead = new ParameterPeekAheadValue();
							peekAhead.Enumerator = enumerator;
							peekAhead.FirstRecord = sqlDataRecord;
							enumerator = null;
							return;
						}
						throw SQL.NotEnoughColumnsInStructuredType();
					}
					finally
					{
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
				}
				if (coercedValue is DbDataReader)
				{
					DataTable schemaTable = ((DbDataReader)coercedValue).GetSchemaTable();
					if (schemaTable.Rows.Count <= 0)
					{
						throw SQL.NotEnoughColumnsInStructuredType();
					}
					int count = schemaTable.Rows.Count;
					fields = new List<SmiExtendedMetaData>(count);
					bool[] array7 = new bool[count];
					bool flag5 = false;
					int ordinal = schemaTable.Columns[SchemaTableColumn.IsKey].Ordinal;
					int ordinal2 = schemaTable.Columns[SchemaTableColumn.ColumnOrdinal].Ordinal;
					for (int m = 0; m < count; m++)
					{
						DataRow dataRow = schemaTable.Rows[m];
						SmiExtendedMetaData smiExtendedMetaData = MetaDataUtilsSmi.SmiMetaDataFromSchemaTableRow(dataRow);
						int n = m;
						if (!dataRow.IsNull(ordinal2))
						{
							n = (int)dataRow[ordinal2];
						}
						if (n >= count || n < 0)
						{
							throw SQL.InvalidSchemaTableOrdinals();
						}
						while (n > fields.Count)
						{
							fields.Add(null);
						}
						if (fields.Count == n)
						{
							fields.Add(smiExtendedMetaData);
						}
						else
						{
							if (fields[n] != null)
							{
								throw SQL.InvalidSchemaTableOrdinals();
							}
							fields[n] = smiExtendedMetaData;
						}
						if (!dataRow.IsNull(ordinal) && (bool)dataRow[ordinal])
						{
							array7[n] = true;
							flag5 = true;
						}
					}
					if (flag5)
					{
						props = new SmiMetaDataPropertyCollection();
						props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array7));
					}
				}
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0006D87C File Offset: 0x0006BA7C
		internal object GetCoercedValue()
		{
			if (this._coercedValue == null || this._internalMetaType.SqlDbType == SqlDbType.Udt)
			{
				bool flag = this.Value is DataFeed;
				if (this.IsNull || flag)
				{
					this._coercedValue = this.Value;
					this._coercedValueIsSqlType = this._coercedValue != null && this._isSqlParameterSqlType;
					this._coercedValueIsDataFeed = flag;
					this._actualSize = (this.IsNull ? 0 : (-1));
				}
				else
				{
					bool flag2;
					this._coercedValue = SqlParameter.CoerceValue(this.Value, this._internalMetaType, out this._coercedValueIsDataFeed, out flag2, true);
					this._coercedValueIsSqlType = this._isSqlParameterSqlType && !flag2;
					this._actualSize = -1;
				}
			}
			return this._coercedValue;
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0006D93C File Offset: 0x0006BB3C
		internal bool CoercedValueIsSqlType
		{
			get
			{
				if (this._coercedValue == null)
				{
					this.GetCoercedValue();
				}
				return this._coercedValueIsSqlType;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0006D953 File Offset: 0x0006BB53
		internal bool CoercedValueIsDataFeed
		{
			get
			{
				if (this._coercedValue == null)
				{
					this.GetCoercedValue();
				}
				return this._coercedValueIsDataFeed;
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		internal void AssertCachedPropertiesAreValid()
		{
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		internal void AssertPropertiesAreValid(object value, bool? isSqlType = null, bool? isDataFeed = null, bool? isNull = null)
		{
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0006D96C File Offset: 0x0006BB6C
		private SqlDbType GetMetaSqlDbTypeOnly()
		{
			MetaType metaType = this._metaType;
			if (metaType == null)
			{
				metaType = MetaType.GetDefaultMetaType();
			}
			return metaType.SqlDbType;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0006D990 File Offset: 0x0006BB90
		private MetaType GetMetaTypeOnly()
		{
			if (this._metaType != null)
			{
				return this._metaType;
			}
			if (this._value != null && DBNull.Value != this._value)
			{
				if (this._value is char)
				{
					this._value = this._value.ToString();
				}
				else if (this.Value is char[])
				{
					this._value = new string((char[])this._value);
				}
				return MetaType.GetMetaTypeFromValue(this._value, false, true);
			}
			if (this._sqlBufferReturnValue != null)
			{
				Type typeFromStorageType = this._sqlBufferReturnValue.GetTypeFromStorageType(this._isSqlParameterSqlType);
				if (null != typeFromStorageType)
				{
					return MetaType.GetMetaTypeFromType(typeFromStorageType, true);
				}
			}
			return MetaType.GetDefaultMetaType();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0006DA44 File Offset: 0x0006BC44
		internal void Prepare(SqlCommand cmd)
		{
			if (this._metaType == null)
			{
				throw ADP.PrepareParameterType(cmd);
			}
			if (!this.ShouldSerializeSize() && !this._metaType.IsFixed)
			{
				throw ADP.PrepareParameterSize(cmd);
			}
			if (!this.ShouldSerializePrecision() && !this.ShouldSerializeScale() && this._metaType.SqlDbType == SqlDbType.Decimal)
			{
				throw ADP.PrepareParameterScale(cmd, this.SqlDbType.ToString());
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0006DAB5 File Offset: 0x0006BCB5
		private void PropertyChanging()
		{
			this._internalMetaType = null;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0006DABE File Offset: 0x0006BCBE
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
			this.CoercedValue = null;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0006DACD File Offset: 0x0006BCCD
		internal void SetSqlBuffer(SqlBuffer buff)
		{
			this._sqlBufferReturnValue = buff;
			this._value = null;
			this._coercedValue = null;
			this._isNull = this._sqlBufferReturnValue.IsNull;
			this._coercedValueIsDataFeed = false;
			this._coercedValueIsSqlType = false;
			this._actualSize = -1;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0006DB0C File Offset: 0x0006BD0C
		internal void Validate(int index, bool isCommandProc)
		{
			MetaType metaTypeOnly = this.GetMetaTypeOnly();
			this._internalMetaType = metaTypeOnly;
			if (ADP.IsDirection(this, ParameterDirection.Output) && !ADP.IsDirection(this, ParameterDirection.ReturnValue) && !metaTypeOnly.IsFixed && !this.ShouldSerializeSize() && (this._value == null || this._value == DBNull.Value) && this.SqlDbType != SqlDbType.Timestamp && this.SqlDbType != SqlDbType.Udt && this.SqlDbType != SqlDbType.Xml && !metaTypeOnly.IsVarTime)
			{
				throw ADP.UninitializedParameterSize(index, metaTypeOnly.ClassType);
			}
			if (metaTypeOnly.SqlDbType != SqlDbType.Udt && this.Direction != ParameterDirection.Output)
			{
				this.GetCoercedValue();
			}
			if (metaTypeOnly.SqlDbType == SqlDbType.Structured)
			{
				if (!isCommandProc && string.IsNullOrEmpty(this.TypeName))
				{
					throw SQL.MustSetTypeNameForParam(metaTypeOnly.TypeName, this.ParameterName);
				}
				if (ParameterDirection.Input != this.Direction)
				{
					throw SQL.UnsupportedTVPOutputParameter(this.Direction, this.ParameterName);
				}
				if (DBNull.Value == this.GetCoercedValue())
				{
					throw SQL.DBNullNotSupportedForTVPValues(this.ParameterName);
				}
			}
			else if (!string.IsNullOrEmpty(this.TypeName))
			{
				throw SQL.UnexpectedTypeNameForNonStructParams(this.ParameterName);
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0006DC24 File Offset: 0x0006BE24
		internal MetaType ValidateTypeLengths()
		{
			MetaType metaType = this.InternalMetaType;
			if (SqlDbType.Udt != metaType.SqlDbType && !metaType.IsFixed && !metaType.IsLong)
			{
				long num = (long)this.GetActualSize();
				long num2 = (long)this.Size;
				long num3;
				if (metaType.IsNCharType)
				{
					num3 = ((num2 * 2L > num) ? (num2 * 2L) : num);
				}
				else
				{
					num3 = ((num2 > num) ? num2 : num);
				}
				if (num3 > 8000L || this._coercedValueIsDataFeed || num2 == -1L || num == -1L)
				{
					metaType = MetaType.GetMaxMetaTypeFromMetaType(metaType);
					this._metaType = metaType;
					this.InternalMetaType = metaType;
					if (!metaType.IsPlp)
					{
						if (metaType.SqlDbType == SqlDbType.Xml)
						{
							throw ADP.InvalidMetaDataValue();
						}
						if (metaType.SqlDbType == SqlDbType.NVarChar || metaType.SqlDbType == SqlDbType.VarChar || metaType.SqlDbType == SqlDbType.VarBinary)
						{
							this.Size = -1;
						}
					}
				}
			}
			return metaType;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0006DD00 File Offset: 0x0006BF00
		private byte ValuePrecision(object value)
		{
			if (!(value is SqlDecimal))
			{
				return this.ValuePrecisionCore(value);
			}
			if (((SqlDecimal)value).IsNull)
			{
				return 0;
			}
			return ((SqlDecimal)value).Precision;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0006DD40 File Offset: 0x0006BF40
		private byte ValueScale(object value)
		{
			if (!(value is SqlDecimal))
			{
				return this.ValueScaleCore(value);
			}
			if (((SqlDecimal)value).IsNull)
			{
				return 0;
			}
			return ((SqlDecimal)value).Scale;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0006DD80 File Offset: 0x0006BF80
		private static int StringSize(object value, bool isSqlType)
		{
			if (isSqlType)
			{
				if (value is SqlString)
				{
					return ((SqlString)value).Value.Length;
				}
				if (value is SqlChars)
				{
					return ((SqlChars)value).Value.Length;
				}
			}
			else
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				char[] array = value as char[];
				if (array != null)
				{
					return array.Length;
				}
				if (value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0006DDEC File Offset: 0x0006BFEC
		private static int BinarySize(object value, bool isSqlType)
		{
			if (isSqlType)
			{
				if (value is SqlBinary)
				{
					return ((SqlBinary)value).Length;
				}
				if (value is SqlBytes)
				{
					return ((SqlBytes)value).Value.Length;
				}
			}
			else
			{
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				if (value is byte)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0006DE44 File Offset: 0x0006C044
		private int ValueSize(object value)
		{
			if (value is SqlString)
			{
				if (((SqlString)value).IsNull)
				{
					return 0;
				}
				return ((SqlString)value).Value.Length;
			}
			else if (value is SqlChars)
			{
				if (((SqlChars)value).IsNull)
				{
					return 0;
				}
				return ((SqlChars)value).Value.Length;
			}
			else if (value is SqlBinary)
			{
				if (((SqlBinary)value).IsNull)
				{
					return 0;
				}
				return ((SqlBinary)value).Length;
			}
			else if (value is SqlBytes)
			{
				if (((SqlBytes)value).IsNull)
				{
					return 0;
				}
				return (int)((SqlBytes)value).Length;
			}
			else
			{
				if (value is DataFeed)
				{
					return 0;
				}
				return this.ValueSizeCore(value);
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0006DF04 File Offset: 0x0006C104
		internal static string[] ParseTypeName(string typeName)
		{
			string[] array;
			try
			{
				string text = "SqlParameter.TypeName is an invalid multipart name";
				array = MultipartIdentifier.ParseMultipartIdentifier(typeName, "[\"", "]\"", '.', 3, true, text, true);
			}
			catch (ArgumentException)
			{
				throw SQL.InvalidParameterTypeNameFormat();
			}
			return array;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x060015D8 RID: 5592 RVA: 0x0006DF48 File Offset: 0x0006C148
		object ICloneable.Clone()
		{
			return new SqlParameter(this);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0006DF50 File Offset: 0x0006C150
		private void CloneHelper(SqlParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._metaType = this._metaType;
			destination._collation = this._collation;
			destination._xmlSchemaCollectionDatabase = this._xmlSchemaCollectionDatabase;
			destination._xmlSchemaCollectionOwningSchema = this._xmlSchemaCollectionOwningSchema;
			destination._xmlSchemaCollectionName = this._xmlSchemaCollectionName;
			destination._typeName = this._typeName;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._sqlBufferReturnValue = this._sqlBufferReturnValue;
			destination._isSqlParameterSqlType = this._isSqlParameterSqlType;
			destination._internalMetaType = this._internalMetaType;
			destination.CoercedValue = this.CoercedValue;
			destination._valueAsINullable = this._valueAsINullable;
			destination._isNull = this._isNull;
			destination._coercedValueIsDataFeed = this._coercedValueIsDataFeed;
			destination._coercedValueIsSqlType = this._coercedValueIsSqlType;
			destination._actualSize = this._actualSize;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0006E03C File Offset: 0x0006C23C
		private void CloneHelperCore(SqlParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.SqlClient.SqlParameter.Value" /></summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x0006E0AC File Offset: 0x0006C2AC
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x0006E0CA File Offset: 0x0006C2CA
		public override DataRowVersion SourceVersion
		{
			get
			{
				DataRowVersion sourceVersion = this._sourceVersion;
				if (sourceVersion == (DataRowVersion)0)
				{
					return DataRowVersion.Current;
				}
				return sourceVersion;
			}
			set
			{
				if (value <= DataRowVersion.Current)
				{
					if (value != DataRowVersion.Original && value != DataRowVersion.Current)
					{
						goto IL_0032;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_0032;
				}
				this._sourceVersion = value;
				return;
				IL_0032:
				throw ADP.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0006E104 File Offset: 0x0006C304
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x0006E10C File Offset: 0x0006C30C
		private object CoercedValue
		{
			get
			{
				return this._coercedValue;
			}
			set
			{
				this._coercedValue = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0006E118 File Offset: 0x0006C318
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x0006E132 File Offset: 0x0006C332
		public override ParameterDirection Direction
		{
			get
			{
				ParameterDirection direction = this._direction;
				if (direction == (ParameterDirection)0)
				{
					return ParameterDirection.Input;
				}
				return direction;
			}
			set
			{
				if (this._direction == value)
				{
					return;
				}
				if (value - ParameterDirection.Input <= 2 || value == ParameterDirection.ReturnValue)
				{
					this.PropertyChanging();
					this._direction = value;
					return;
				}
				throw ADP.InvalidParameterDirection(value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values. <see cref="P:System.Data.SqlClient.SqlParameter.IsNullable" /> is not used to validate the parameter’s value and will not prevent sending or receiving a null value when executing a command.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0006E15C File Offset: 0x0006C35C
		// (set) Token: 0x060015E2 RID: 5602 RVA: 0x0006E164 File Offset: 0x0006C364
		public override bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		/// <summary>Gets or sets the offset to the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> property.</summary>
		/// <returns>The offset to the <see cref="P:System.Data.SqlClient.SqlParameter.Value" />. The default is 0.</returns>
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0006E16D File Offset: 0x0006C36D
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x0006E175 File Offset: 0x0006C375
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidOffsetValue(value);
				}
				this._offset = value;
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0006E18C File Offset: 0x0006C38C
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x0006E1B1 File Offset: 0x0006C3B1
		public override int Size
		{
			get
			{
				int num = this._size;
				if (num == 0)
				{
					num = this.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size != value)
				{
					if (value < -1)
					{
						throw ADP.InvalidSizeValue(value);
					}
					this.PropertyChanging();
					this._size = value;
				}
			}
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0006E1D4 File Offset: 0x0006C3D4
		private bool ShouldSerializeSize()
		{
			return this._size != 0;
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /></summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0006E1E0 File Offset: 0x0006C3E0
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0006E1FE File Offset: 0x0006C3FE
		public override string SourceColumn
		{
			get
			{
				string sourceColumn = this._sourceColumn;
				if (sourceColumn == null)
				{
					return ADP.StrEmpty;
				}
				return sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0006E207 File Offset: 0x0006C407
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x0006E20F File Offset: 0x0006C40F
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				this._sourceColumnNullMapping = value;
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0006E218 File Offset: 0x0006C418
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0006E238 File Offset: 0x0006C438
		internal void ResetParent()
		{
			this._parent = null;
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.SqlClient.SqlParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.SqlClient.SqlParameter.ParameterName" />.</returns>
		// Token: 0x060015EE RID: 5614 RVA: 0x0006E241 File Offset: 0x0006C441
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0006E24C File Offset: 0x0006C44C
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0006E276 File Offset: 0x0006C476
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0006E29C File Offset: 0x0006C49C
		private int ValueSizeCore(object value)
		{
			if (!ADP.IsNull(value))
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				char[] array2 = value as char[];
				if (array2 != null)
				{
					return array2.Length;
				}
				if (value is byte || value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0006E2F4 File Offset: 0x0006C4F4
		internal void CopyTo(SqlParameter destination)
		{
			ADP.CheckArgumentNull(destination, "destination");
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		/// <summary>Gets or sets a string that represents a user-defined type as a parameter.</summary>
		/// <returns>A string that represents the fully qualified name of a user-defined type in the database.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		// (set) Token: 0x060015F4 RID: 5620 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		[MonoTODO]
		public string UdtTypeName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0006E36C File Offset: 0x0006C56C
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x00010468 File Offset: 0x0000E668
		public bool ForceColumnEncryption
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04000E9E RID: 3742
		private MetaType _metaType;

		// Token: 0x04000E9F RID: 3743
		private SqlCollation _collation;

		// Token: 0x04000EA0 RID: 3744
		private string _xmlSchemaCollectionDatabase;

		// Token: 0x04000EA1 RID: 3745
		private string _xmlSchemaCollectionOwningSchema;

		// Token: 0x04000EA2 RID: 3746
		private string _xmlSchemaCollectionName;

		// Token: 0x04000EA3 RID: 3747
		private string _typeName;

		// Token: 0x04000EA4 RID: 3748
		private string _parameterName;

		// Token: 0x04000EA5 RID: 3749
		private byte _precision;

		// Token: 0x04000EA6 RID: 3750
		private byte _scale;

		// Token: 0x04000EA7 RID: 3751
		private bool _hasScale;

		// Token: 0x04000EA8 RID: 3752
		private MetaType _internalMetaType;

		// Token: 0x04000EA9 RID: 3753
		private SqlBuffer _sqlBufferReturnValue;

		// Token: 0x04000EAA RID: 3754
		private INullable _valueAsINullable;

		// Token: 0x04000EAB RID: 3755
		private bool _isSqlParameterSqlType;

		// Token: 0x04000EAC RID: 3756
		private bool _isNull = true;

		// Token: 0x04000EAD RID: 3757
		private bool _coercedValueIsSqlType;

		// Token: 0x04000EAE RID: 3758
		private bool _coercedValueIsDataFeed;

		// Token: 0x04000EAF RID: 3759
		private int _actualSize = -1;

		// Token: 0x04000EB0 RID: 3760
		private DataRowVersion _sourceVersion;

		// Token: 0x04000EB1 RID: 3761
		private object _value;

		// Token: 0x04000EB2 RID: 3762
		private object _parent;

		// Token: 0x04000EB3 RID: 3763
		private ParameterDirection _direction;

		// Token: 0x04000EB4 RID: 3764
		private int _size;

		// Token: 0x04000EB5 RID: 3765
		private int _offset;

		// Token: 0x04000EB6 RID: 3766
		private string _sourceColumn;

		// Token: 0x04000EB7 RID: 3767
		private bool _sourceColumnNullMapping;

		// Token: 0x04000EB8 RID: 3768
		private bool _isNullable;

		// Token: 0x04000EB9 RID: 3769
		private object _coercedValue;
	}
}
