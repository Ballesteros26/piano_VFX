using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	/// <summary>Represents the schema of a column in a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005F RID: 95
	[ToolboxItem(false)]
	[DefaultProperty("ColumnName")]
	[DesignTimeVisible(false)]
	public class DataColumn : MarshalByValueComponent
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataColumn" /> class as type string.</summary>
		// Token: 0x0600030F RID: 783 RVA: 0x00010847 File Offset: 0x0000EA47
		public DataColumn()
			: this(null, typeof(string), null, MappingType.Element)
		{
		}

		/// <summary>Inititalizes a new instance of the <see cref="T:System.Data.DataColumn" /> class, as type string, using the specified column name.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		// Token: 0x06000310 RID: 784 RVA: 0x0001085C File Offset: 0x0000EA5C
		public DataColumn(string columnName)
			: this(columnName, typeof(string), null, MappingType.Element)
		{
		}

		/// <summary>Inititalizes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified column name and data type.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />. </param>
		/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified. </exception>
		// Token: 0x06000311 RID: 785 RVA: 0x00010871 File Offset: 0x0000EA71
		public DataColumn(string columnName, Type dataType)
			: this(columnName, dataType, null, MappingType.Element)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified name, data type, and expression.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />. </param>
		/// <param name="expr">The expression used to create this column. For more information, see the <see cref="P:System.Data.DataColumn.Expression" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified. </exception>
		// Token: 0x06000312 RID: 786 RVA: 0x0001087D File Offset: 0x0000EA7D
		public DataColumn(string columnName, Type dataType, string expr)
			: this(columnName, dataType, expr, MappingType.Element)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified name, data type, expression, and value that determines whether the column is an attribute.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />. </param>
		/// <param name="expr">The expression used to create this column. For more information, see the <see cref="P:System.Data.DataColumn.Expression" /> property. </param>
		/// <param name="type">One of the <see cref="T:System.Data.MappingType" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified. </exception>
		// Token: 0x06000313 RID: 787 RVA: 0x0001088C File Offset: 0x0000EA8C
		public DataColumn(string columnName, Type dataType, string expr, MappingType type)
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int, string, string, MappingType>("<ds.DataColumn.DataColumn|API> {0}, columnName='{1}', expr='{2}', type={3}", this.ObjectID, columnName, expr, type);
			if (dataType == null)
			{
				throw ExceptionBuilder.ArgumentNull("dataType");
			}
			StorageType storageType = DataStorage.GetStorageType(dataType);
			if (DataStorage.ImplementsINullableValue(storageType, dataType))
			{
				throw ExceptionBuilder.ColumnTypeNotSupported();
			}
			this._columnName = columnName ?? string.Empty;
			SimpleType simpleType = SimpleType.CreateSimpleType(storageType, dataType);
			if (simpleType != null)
			{
				this.SimpleType = simpleType;
			}
			this.UpdateColumnType(dataType, storageType);
			if (!string.IsNullOrEmpty(expr))
			{
				this.Expression = expr;
			}
			this._columnMapping = type;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00010984 File Offset: 0x0000EB84
		private void UpdateColumnType(Type type, StorageType typeCode)
		{
			this._dataType = type;
			this._storageType = typeCode;
			if (StorageType.DateTime != typeCode)
			{
				this._dateTimeMode = DataSetDateTime.UnspecifiedLocal;
			}
			DataStorage.ImplementsInterfaces(typeCode, type, out this._isSqlType, out this._implementsINullable, out this._implementsIXMLSerializable, out this._implementsIChangeTracking, out this._implementsIRevertibleChangeTracking);
			if (!this._isSqlType && this._implementsINullable)
			{
				SqlUdtStorage.GetStaticNullForUdtType(type);
			}
		}

		/// <summary>Gets or sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</summary>
		/// <returns>true if null values values are allowed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000109E7 File Offset: 0x0000EBE7
		// (set) Token: 0x06000316 RID: 790 RVA: 0x000109F0 File Offset: 0x0000EBF0
		[DefaultValue(true)]
		public bool AllowDBNull
		{
			get
			{
				return this._allowNull;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataColumn.set_AllowDBNull|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._allowNull != value)
					{
						if (this._table != null && !value && this._table.EnforceConstraints)
						{
							this.CheckNotAllowNull();
						}
						this._allowNull = value;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the column automatically increments the value of the column for new rows added to the table.</summary>
		/// <returns>true if the value of the column increments automatically; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The column is a computed column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00010A60 File Offset: 0x0000EC60
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00010A78 File Offset: 0x0000EC78
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool AutoIncrement
		{
			get
			{
				return this._autoInc != null && this._autoInc.Auto;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, bool>("<ds.DataColumn.set_AutoIncrement|API> {0}, {1}", this.ObjectID, value);
				if (this.AutoIncrement != value)
				{
					if (value)
					{
						if (this._expression != null)
						{
							throw ExceptionBuilder.AutoIncrementAndExpression();
						}
						if (!this.DefaultValueIsNull)
						{
							throw ExceptionBuilder.AutoIncrementAndDefaultValue();
						}
						if (!DataColumn.IsAutoIncrementType(this.DataType))
						{
							if (this.HasData)
							{
								throw ExceptionBuilder.AutoIncrementCannotSetIfHasData(this.DataType.Name);
							}
							this.DataType = typeof(int);
						}
					}
					this.AutoInc.Auto = value;
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00010B05 File Offset: 0x0000ED05
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00010B26 File Offset: 0x0000ED26
		internal object AutoIncrementCurrent
		{
			get
			{
				if (this._autoInc == null)
				{
					return this.AutoIncrementSeed;
				}
				return this._autoInc.Current;
			}
			set
			{
				if (this.AutoIncrementSeed != BigIntegerStorage.ConvertToBigInteger(value, this.FormatProvider))
				{
					this.AutoInc.SetCurrent(value, this.FormatProvider);
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00010B58 File Offset: 0x0000ED58
		internal AutoIncrementValue AutoInc
		{
			get
			{
				AutoIncrementValue autoIncrementValue;
				if ((autoIncrementValue = this._autoInc) == null)
				{
					autoIncrementValue = (this._autoInc = ((this.DataType == typeof(BigInteger)) ? new AutoIncrementBigInteger() : new AutoIncrementInt64()));
				}
				return autoIncrementValue;
			}
		}

		/// <summary>Gets or sets the starting value for a column that has its <see cref="P:System.Data.DataColumn.AutoIncrement" /> property set to true. The default is 0.</summary>
		/// <returns>The starting value for the <see cref="P:System.Data.DataColumn.AutoIncrement" /> feature.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00010B9B File Offset: 0x0000ED9B
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00010BB3 File Offset: 0x0000EDB3
		[DefaultValue(0L)]
		public long AutoIncrementSeed
		{
			get
			{
				if (this._autoInc == null)
				{
					return 0L;
				}
				return this._autoInc.Seed;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, long>("<ds.DataColumn.set_AutoIncrementSeed|API> {0}, {1}", this.ObjectID, value);
				if (this.AutoIncrementSeed != value)
				{
					this.AutoInc.Seed = value;
				}
			}
		}

		/// <summary>Gets or sets the increment used by a column with its <see cref="P:System.Data.DataColumn.AutoIncrement" /> property set to true.</summary>
		/// <returns>The number by which the value of the column is automatically incremented. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00010BF8 File Offset: 0x0000EDF8
		[DefaultValue(1L)]
		public long AutoIncrementStep
		{
			get
			{
				if (this._autoInc == null)
				{
					return 1L;
				}
				return this._autoInc.Step;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, long>("<ds.DataColumn.set_AutoIncrementStep|API> {0}, {1}", this.ObjectID, value);
				if (this.AutoIncrementStep != value)
				{
					this.AutoInc.Step = value;
				}
			}
		}

		/// <summary>Gets or sets the caption for the column.</summary>
		/// <returns>The caption of the column. If not set, returns the <see cref="P:System.Data.DataColumn.ColumnName" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00010C25 File Offset: 0x0000EE25
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00010C3C File Offset: 0x0000EE3C
		public string Caption
		{
			get
			{
				if (this._caption == null)
				{
					return this._columnName;
				}
				return this._caption;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this._caption == null || string.Compare(this._caption, value, true, this.Locale) != 0)
				{
					this._caption = value;
				}
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00010C6C File Offset: 0x0000EE6C
		private void ResetCaption()
		{
			if (this._caption != null)
			{
				this._caption = null;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00010C7D File Offset: 0x0000EE7D
		private bool ShouldSerializeCaption()
		{
			return this._caption != null;
		}

		/// <summary>Gets or sets the name of the column in the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The name of the column.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to null or an empty string and the column belongs to a collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">A column with the same name already exists in the collection. The name comparison is not case sensitive. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00010C88 File Offset: 0x0000EE88
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00010C90 File Offset: 0x0000EE90
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataColumn.set_ColumnName|API> {0}, '{1}'", this.ObjectID, value);
				try
				{
					if (value == null)
					{
						value = string.Empty;
					}
					if (string.Compare(this._columnName, value, true, this.Locale) != 0)
					{
						if (this._table != null)
						{
							if (value.Length == 0)
							{
								throw ExceptionBuilder.ColumnNameRequired();
							}
							this._table.Columns.RegisterColumnName(value, this);
							if (this._columnName.Length != 0)
							{
								this._table.Columns.UnregisterName(this._columnName);
							}
						}
						this.RaisePropertyChanging("ColumnName");
						this._columnName = value;
						this._encodedColumnName = null;
						if (this._table != null)
						{
							this._table.Columns.OnColumnPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						}
					}
					else if (this._columnName != value)
					{
						this.RaisePropertyChanging("ColumnName");
						this._columnName = value;
						this._encodedColumnName = null;
						if (this._table != null)
						{
							this._table.Columns.OnColumnPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						}
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		internal string EncodedColumnName
		{
			get
			{
				if (this._encodedColumnName == null)
				{
					this._encodedColumnName = XmlConvert.EncodeLocalName(this.ColumnName);
				}
				return this._encodedColumnName;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		internal IFormatProvider FormatProvider
		{
			get
			{
				if (this._table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._table.FormatProvider;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00010E0C File Offset: 0x0000F00C
		internal CultureInfo Locale
		{
			get
			{
				if (this._table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._table.Locale;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00010E27 File Offset: 0x0000F027
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets or sets an XML prefix that aliases the namespace of the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The XML prefix for the <see cref="T:System.Data.DataTable" /> namespace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00010E2F File Offset: 0x0000F02F
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00010E38 File Offset: 0x0000F038
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._columnPrefix;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataColumn.set_Prefix|API> {0}, '{1}'", this.ObjectID, value);
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				this._columnPrefix = value;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00010E90 File Offset: 0x0000F090
		internal string GetColumnValueAsString(DataRow row, DataRowVersion version)
		{
			object obj = this[row.GetRecordFromVersion(version)];
			if (DataStorage.IsObjectNull(obj))
			{
				return null;
			}
			return this.ConvertObjectToXml(obj);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00010EBC File Offset: 0x0000F0BC
		internal bool Computed
		{
			get
			{
				return this._expression != null;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00010EC7 File Offset: 0x0000F0C7
		internal DataExpression DataExpression
		{
			get
			{
				return this._expression;
			}
		}

		/// <summary>Gets or sets the type of data stored in the column.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the column data type.</returns>
		/// <exception cref="T:System.ArgumentException">The column already has data stored. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00010ECF File Offset: 0x0000F0CF
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(ColumnTypeConverter))]
		[DefaultValue(typeof(string))]
		public Type DataType
		{
			get
			{
				return this._dataType;
			}
			set
			{
				if (this._dataType != value)
				{
					if (this.HasData)
					{
						throw ExceptionBuilder.CantChangeDataType();
					}
					if (value == null)
					{
						throw ExceptionBuilder.NullDataType();
					}
					StorageType storageType = DataStorage.GetStorageType(value);
					if (DataStorage.ImplementsINullableValue(storageType, value))
					{
						throw ExceptionBuilder.ColumnTypeNotSupported();
					}
					if (this._table != null && this.IsInRelation())
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
					if (storageType == StorageType.BigInteger && this._expression != null)
					{
						throw ExprException.UnsupportedDataType(value);
					}
					if (!this.DefaultValueIsNull)
					{
						try
						{
							if (this._defaultValue is BigInteger)
							{
								this._defaultValue = BigIntegerStorage.ConvertFromBigInteger((BigInteger)this._defaultValue, value, this.FormatProvider);
							}
							else if (typeof(BigInteger) == value)
							{
								this._defaultValue = BigIntegerStorage.ConvertToBigInteger(this._defaultValue, this.FormatProvider);
							}
							else if (typeof(string) == value)
							{
								this._defaultValue = this.DefaultValue.ToString();
							}
							else if (typeof(SqlString) == value)
							{
								this._defaultValue = SqlConvert.ConvertToSqlString(this.DefaultValue);
							}
							else if (typeof(object) != value)
							{
								this.DefaultValue = SqlConvert.ChangeTypeForDefaultValue(this.DefaultValue, value, this.FormatProvider);
							}
						}
						catch (InvalidCastException ex)
						{
							throw ExceptionBuilder.DefaultValueDataType(this.ColumnName, this.DefaultValue.GetType(), value, ex);
						}
						catch (FormatException ex2)
						{
							throw ExceptionBuilder.DefaultValueDataType(this.ColumnName, this.DefaultValue.GetType(), value, ex2);
						}
					}
					if (this.ColumnMapping == MappingType.SimpleContent && value == typeof(char))
					{
						throw ExceptionBuilder.CannotSetSimpleContentType(this.ColumnName, value);
					}
					this.SimpleType = SimpleType.CreateSimpleType(storageType, value);
					if (StorageType.String == storageType)
					{
						this._maxLength = -1;
					}
					this.UpdateColumnType(value, storageType);
					this.XmlDataType = null;
					if (this.AutoIncrement)
					{
						if (!DataColumn.IsAutoIncrementType(value))
						{
							this.AutoIncrement = false;
						}
						if (this._autoInc != null)
						{
							AutoIncrementValue autoInc = this._autoInc;
							this._autoInc = null;
							this.AutoInc.Auto = autoInc.Auto;
							this.AutoInc.Seed = autoInc.Seed;
							this.AutoInc.Step = autoInc.Step;
							if (this._autoInc.DataType == autoInc.DataType)
							{
								this._autoInc.Current = autoInc.Current;
								return;
							}
							if (autoInc.DataType == typeof(long))
							{
								this.AutoInc.Current = (long)autoInc.Current;
								return;
							}
							this.AutoInc.Current = (long)((BigInteger)autoInc.Current);
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the DateTimeMode for the column.</summary>
		/// <returns>The <see cref="T:System.Data.DataSetDateTime" /> for the specified column.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000111C0 File Offset: 0x0000F3C0
		// (set) Token: 0x06000332 RID: 818 RVA: 0x000111C8 File Offset: 0x0000F3C8
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(DataSetDateTime.UnspecifiedLocal)]
		public DataSetDateTime DateTimeMode
		{
			get
			{
				return this._dateTimeMode;
			}
			set
			{
				if (this._dateTimeMode != value)
				{
					if (this.DataType != typeof(DateTime) && value != DataSetDateTime.UnspecifiedLocal)
					{
						throw ExceptionBuilder.CannotSetDateTimeModeForNonDateTimeColumns();
					}
					switch (value)
					{
					case DataSetDateTime.Local:
					case DataSetDateTime.Utc:
						if (this.HasData)
						{
							throw ExceptionBuilder.CantChangeDateTimeMode(this._dateTimeMode, value);
						}
						break;
					case DataSetDateTime.Unspecified:
					case DataSetDateTime.UnspecifiedLocal:
						if (this._dateTimeMode != DataSetDateTime.Unspecified && this._dateTimeMode != DataSetDateTime.UnspecifiedLocal && this.HasData)
						{
							throw ExceptionBuilder.CantChangeDateTimeMode(this._dateTimeMode, value);
						}
						break;
					default:
						throw ExceptionBuilder.InvalidDateTimeMode(value);
					}
					this._dateTimeMode = value;
				}
			}
		}

		/// <summary>Gets or sets the default value for the column when you are creating new rows.</summary>
		/// <returns>A value appropriate to the column's <see cref="P:System.Data.DataColumn.DataType" />.</returns>
		/// <exception cref="T:System.InvalidCastException">When you are adding a row, the default value is not an instance of the column's data type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00011268 File Offset: 0x0000F468
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00011304 File Offset: 0x0000F504
		[TypeConverter(typeof(DefaultValueTypeConverter))]
		public object DefaultValue
		{
			get
			{
				if (this._defaultValue == DBNull.Value && this._implementsINullable)
				{
					if (this._storage != null)
					{
						this._defaultValue = this._storage._nullValue;
					}
					else if (this._isSqlType)
					{
						this._defaultValue = SqlConvert.ChangeTypeForDefaultValue(this._defaultValue, this._dataType, this.FormatProvider);
					}
					else if (this._implementsINullable)
					{
						PropertyInfo property = this._dataType.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
						if (property != null)
						{
							this._defaultValue = property.GetValue(null, null);
						}
					}
				}
				return this._defaultValue;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataColumn.set_DefaultValue|API> {0}", this.ObjectID);
				if (this._defaultValue == null || !this.DefaultValue.Equals(value))
				{
					if (this.AutoIncrement)
					{
						throw ExceptionBuilder.DefaultValueAndAutoIncrement();
					}
					object obj = ((value == null) ? DBNull.Value : value);
					if (obj != DBNull.Value && this.DataType != typeof(object))
					{
						try
						{
							obj = SqlConvert.ChangeTypeForDefaultValue(obj, this.DataType, this.FormatProvider);
						}
						catch (InvalidCastException ex)
						{
							throw ExceptionBuilder.DefaultValueColumnDataType(this.ColumnName, obj.GetType(), this.DataType, ex);
						}
					}
					this._defaultValue = obj;
					this._defaultValueIsNull = obj == DBNull.Value || (this.ImplementsINullable && DataStorage.IsObjectSqlNull(obj));
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000335 RID: 821 RVA: 0x000113E0 File Offset: 0x0000F5E0
		internal bool DefaultValueIsNull
		{
			get
			{
				return this._defaultValueIsNull;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000113E8 File Offset: 0x0000F5E8
		internal void BindExpression()
		{
			this.DataExpression.Bind(this._table);
		}

		/// <summary>Gets or sets the expression used to filter rows, calculate the values in a column, or create an aggregate column.</summary>
		/// <returns>An expression to calculate the value of a column, or create an aggregate column. The return type of an expression is determined by the <see cref="P:System.Data.DataColumn.DataType" /> of the column.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Data.DataColumn.AutoIncrement" /> or <see cref="P:System.Data.DataColumn.Unique" /> property is set to true. </exception>
		/// <exception cref="T:System.FormatException">When you are using the CONVERT function, the expression evaluates to a string, but the string does not contain a representation that can be converted to the type parameter. </exception>
		/// <exception cref="T:System.InvalidCastException">When you are using the CONVERT function, the requested cast is not possible. See the Conversion function in the following section for detailed information about possible casts. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">When you use the SUBSTRING function, the start argument is out of range.-Or- When you use the SUBSTRING function, the length argument is out of range. </exception>
		/// <exception cref="T:System.Exception">When you use the LEN function or the TRIM function, the expression does not evaluate to a string. This includes expressions that evaluate to <see cref="T:System.Char" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000113FB File Offset: 0x0000F5FB
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00011418 File Offset: 0x0000F618
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		public string Expression
		{
			get
			{
				if (this._expression != null)
				{
					return this._expression.Expression;
				}
				return "";
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataColumn.set_Expression|API> {0}, '{1}'", this.ObjectID, value);
				if (value == null)
				{
					value = string.Empty;
				}
				try
				{
					DataExpression dataExpression = null;
					if (value.Length > 0)
					{
						DataExpression dataExpression2 = new DataExpression(this._table, value, this._dataType);
						if (dataExpression2.HasValue)
						{
							dataExpression = dataExpression2;
						}
					}
					if (this._expression == null && dataExpression != null)
					{
						if (this.AutoIncrement || this.Unique)
						{
							throw ExceptionBuilder.ExpressionAndUnique();
						}
						if (this._table != null)
						{
							for (int i = 0; i < this._table.Constraints.Count; i++)
							{
								if (this._table.Constraints[i].ContainsColumn(this))
								{
									throw ExceptionBuilder.ExpressionAndConstraint(this, this._table.Constraints[i]);
								}
							}
						}
						bool readOnly = this.ReadOnly;
						try
						{
							this.ReadOnly = true;
						}
						catch (ReadOnlyException ex)
						{
							ExceptionBuilder.TraceExceptionForCapture(ex);
							this.ReadOnly = readOnly;
							throw ExceptionBuilder.ExpressionAndReadOnly();
						}
					}
					if (this._table != null)
					{
						if (dataExpression != null && dataExpression.DependsOn(this))
						{
							throw ExceptionBuilder.ExpressionCircular();
						}
						this.HandleDependentColumnList(this._expression, dataExpression);
						DataExpression expression = this._expression;
						this._expression = dataExpression;
						try
						{
							if (dataExpression == null)
							{
								for (int j = 0; j < this._table.RecordCapacity; j++)
								{
									this.InitializeRecord(j);
								}
							}
							else
							{
								this._table.EvaluateExpressions(this);
							}
							this._table.ResetInternalIndexes(this);
							this._table.EvaluateDependentExpressions(this);
							return;
						}
						catch (Exception ex2) when (ADP.IsCatchableExceptionType(ex2))
						{
							ExceptionBuilder.TraceExceptionForCapture(ex2);
							try
							{
								this._expression = expression;
								this.HandleDependentColumnList(dataExpression, this._expression);
								if (expression == null)
								{
									for (int k = 0; k < this._table.RecordCapacity; k++)
									{
										this.InitializeRecord(k);
									}
								}
								else
								{
									this._table.EvaluateExpressions(this);
								}
								this._table.ResetInternalIndexes(this);
								this._table.EvaluateDependentExpressions(this);
							}
							catch (Exception ex3) when (ADP.IsCatchableExceptionType(ex3))
							{
								ExceptionBuilder.TraceExceptionWithoutRethrow(ex3);
							}
							throw;
						}
					}
					this._expression = dataExpression;
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Gets the collection of custom user information associated with a <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> of custom information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000116C4 File Offset: 0x0000F8C4
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection propertyCollection;
				if ((propertyCollection = this._extendedProperties) == null)
				{
					propertyCollection = (this._extendedProperties = new PropertyCollection());
				}
				return propertyCollection;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600033A RID: 826 RVA: 0x000116E9 File Offset: 0x0000F8E9
		internal bool HasData
		{
			get
			{
				return this._storage != null;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000116F4 File Offset: 0x0000F8F4
		internal bool ImplementsINullable
		{
			get
			{
				return this._implementsINullable;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000116FC File Offset: 0x0000F8FC
		internal bool ImplementsIChangeTracking
		{
			get
			{
				return this._implementsIChangeTracking;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00011704 File Offset: 0x0000F904
		internal bool ImplementsIRevertibleChangeTracking
		{
			get
			{
				return this._implementsIRevertibleChangeTracking;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0001170C File Offset: 0x0000F90C
		internal bool IsCloneable
		{
			get
			{
				return this._storage._isCloneable;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00011719 File Offset: 0x0000F919
		internal bool IsStringType
		{
			get
			{
				return this._storage._isStringType;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00011726 File Offset: 0x0000F926
		internal bool IsValueType
		{
			get
			{
				return this._storage._isValueType;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00011733 File Offset: 0x0000F933
		internal bool IsSqlType
		{
			get
			{
				return this._isSqlType;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001173C File Offset: 0x0000F93C
		private void SetMaxLengthSimpleType()
		{
			if (this._simpleType != null)
			{
				this._simpleType.MaxLength = this._maxLength;
				if (this._simpleType.IsPlainString())
				{
					this._simpleType = null;
					return;
				}
				if (this._simpleType.Name != null && this.XmlDataType != null)
				{
					this._simpleType.ConvertToAnnonymousSimpleType();
					this.XmlDataType = null;
					return;
				}
			}
			else if (-1 < this._maxLength)
			{
				this.SimpleType = SimpleType.CreateLimitedStringType(this._maxLength);
			}
		}

		/// <summary>Gets or sets the maximum length of a text column.</summary>
		/// <returns>The maximum length of the column in characters. If the column has no maximum length, the value is –1 (default).</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000117B9 File Offset: 0x0000F9B9
		// (set) Token: 0x06000344 RID: 836 RVA: 0x000117C4 File Offset: 0x0000F9C4
		[DefaultValue(-1)]
		public int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataColumn.set_MaxLength|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._maxLength != value)
					{
						if (this.ColumnMapping == MappingType.SimpleContent)
						{
							throw ExceptionBuilder.CannotSetMaxLength2(this);
						}
						if (this.DataType != typeof(string) && this.DataType != typeof(SqlString))
						{
							throw ExceptionBuilder.HasToBeStringType(this);
						}
						int maxLength = this._maxLength;
						this._maxLength = Math.Max(value, -1);
						if ((maxLength < 0 || value < maxLength) && this._table != null && this._table.EnforceConstraints && !this.CheckMaxLength())
						{
							this._maxLength = maxLength;
							throw ExceptionBuilder.CannotSetMaxLength(this, value);
						}
						this.SetMaxLengthSimpleType();
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Gets or sets the namespace of the <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataColumn" />.</returns>
		/// <exception cref="T:System.ArgumentException">The namespace already has data. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000118A8 File Offset: 0x0000FAA8
		// (set) Token: 0x06000346 RID: 838 RVA: 0x000118DC File Offset: 0x0000FADC
		public string Namespace
		{
			get
			{
				if (this._columnUri != null)
				{
					return this._columnUri;
				}
				if (this.Table != null && this._columnMapping != MappingType.Attribute)
				{
					return this.Table.Namespace;
				}
				return string.Empty;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataColumn.set_Namespace|API> {0}, '{1}'", this.ObjectID, value);
				if (this._columnUri != value)
				{
					if (this._columnMapping != MappingType.SimpleContent)
					{
						this.RaisePropertyChanging("Namespace");
						this._columnUri = value;
						return;
					}
					if (value != this.Namespace)
					{
						throw ExceptionBuilder.CannotChangeNamespace(this.ColumnName);
					}
				}
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00011943 File Offset: 0x0000FB43
		private bool ShouldSerializeNamespace()
		{
			return this._columnUri != null;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001194E File Offset: 0x0000FB4E
		private void ResetNamespace()
		{
			this.Namespace = null;
		}

		/// <summary>Gets the (zero-based) position of the column in the <see cref="T:System.Data.DataColumnCollection" /> collection.</summary>
		/// <returns>The position of the column. Gets -1 if the column is not a member of a collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00011957 File Offset: 0x0000FB57
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		/// <summary>Changes the ordinal or position of the <see cref="T:System.Data.DataColumn" /> to the specified ordinal or position.</summary>
		/// <param name="ordinal">The specified ordinal.</param>
		// Token: 0x0600034A RID: 842 RVA: 0x0001195F File Offset: 0x0000FB5F
		public void SetOrdinal(int ordinal)
		{
			if (this._ordinal == -1)
			{
				throw ExceptionBuilder.ColumnNotInAnyTable();
			}
			if (this._ordinal != ordinal)
			{
				this._table.Columns.MoveTo(this, ordinal);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001198C File Offset: 0x0000FB8C
		internal void SetOrdinalInternal(int ordinal)
		{
			if (this._ordinal != ordinal)
			{
				if (this.Unique && this._ordinal != -1 && ordinal == -1)
				{
					UniqueConstraint uniqueConstraint = this._table.Constraints.FindKeyConstraint(this);
					if (uniqueConstraint != null)
					{
						this._table.Constraints.Remove(uniqueConstraint);
					}
				}
				if (this._sortIndex != null && -1 == ordinal)
				{
					this._sortIndex.RemoveRef();
					this._sortIndex.RemoveRef();
					this._sortIndex = null;
				}
				int ordinal2 = this._ordinal;
				this._ordinal = ordinal;
				if (ordinal2 == -1 && this._ordinal != -1 && this.Unique)
				{
					UniqueConstraint uniqueConstraint2 = new UniqueConstraint(this);
					this._table.Constraints.Add(uniqueConstraint2);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</summary>
		/// <returns>true if the column is read only; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to false on a computed column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00011A44 File Offset: 0x0000FC44
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00011A4C File Offset: 0x0000FC4C
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, bool>("<ds.DataColumn.set_ReadOnly|API> {0}, {1}", this.ObjectID, value);
				if (this._readOnly != value)
				{
					if (!value && this._expression != null)
					{
						throw ExceptionBuilder.ReadOnlyAndExpression();
					}
					this._readOnly = value;
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00011A88 File Offset: 0x0000FC88
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Index SortIndex
		{
			get
			{
				if (this._sortIndex == null)
				{
					IndexField[] array = new IndexField[]
					{
						new IndexField(this, false)
					};
					this._sortIndex = this._table.GetIndex(array, DataViewRowState.CurrentRows, null);
					this._sortIndex.AddRef();
				}
				return this._sortIndex;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the column belongs to.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataColumn" /> belongs to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		internal void SetTable(DataTable table)
		{
			if (this._table != table)
			{
				if (this.Computed && (table == null || (!table.fInitInProgress && (table.DataSet == null || (!table.DataSet._fIsSchemaLoading && !table.DataSet._fInitInProgress)))))
				{
					this.DataExpression.Bind(table);
				}
				if (this.Unique && this._table != null)
				{
					UniqueConstraint uniqueConstraint = table.Constraints.FindKeyConstraint(this);
					if (uniqueConstraint != null)
					{
						table.Constraints.CanRemove(uniqueConstraint, true);
					}
				}
				this._table = table;
				this._storage = null;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00011B73 File Offset: 0x0000FD73
		private DataRow GetDataRow(int index)
		{
			return this._table._recordManager[index];
		}

		// Token: 0x170000D7 RID: 215
		internal object this[int record]
		{
			get
			{
				return this._storage.Get(record);
			}
			set
			{
				try
				{
					this._storage.Set(record, value);
				}
				catch (Exception ex)
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExceptionBuilder.SetFailed(value, this, this.DataType, ex);
				}
				if (this.AutoIncrement && !this._storage.IsNull(record))
				{
					this.AutoInc.SetCurrentAndIncrement(this._storage.Get(record));
				}
				if (this.Computed)
				{
					DataRow dataRow = this.GetDataRow(record);
					if (dataRow != null)
					{
						dataRow.LastChangedColumn = this;
					}
				}
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011C20 File Offset: 0x0000FE20
		internal void InitializeRecord(int record)
		{
			this._storage.Set(record, this.DefaultValue);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00011C34 File Offset: 0x0000FE34
		internal void SetValue(int record, object value)
		{
			try
			{
				this._storage.Set(record, value);
			}
			catch (Exception ex)
			{
				ExceptionBuilder.TraceExceptionForCapture(ex);
				throw ExceptionBuilder.SetFailed(value, this, this.DataType, ex);
			}
			DataRow dataRow = this.GetDataRow(record);
			if (dataRow != null)
			{
				dataRow.LastChangedColumn = this;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00011C8C File Offset: 0x0000FE8C
		internal void FreeRecord(int record)
		{
			this._storage.Set(record, this._storage._nullValue);
		}

		/// <summary>Gets or sets a value that indicates whether the values in each row of the column must be unique.</summary>
		/// <returns>true if the value must be unique; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The column is a calculated column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00011CA5 File Offset: 0x0000FEA5
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00011CB0 File Offset: 0x0000FEB0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		public bool Unique
		{
			get
			{
				return this._unique;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataColumn.set_Unique|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._unique != value)
					{
						if (value && this._expression != null)
						{
							throw ExceptionBuilder.UniqueAndExpression();
						}
						UniqueConstraint uniqueConstraint = null;
						if (this._table != null)
						{
							if (value)
							{
								this.CheckUnique();
							}
							else
							{
								foreach (object obj in this.Table.Constraints)
								{
									UniqueConstraint uniqueConstraint2 = obj as UniqueConstraint;
									if (uniqueConstraint2 != null && uniqueConstraint2.ColumnsReference.Length == 1 && uniqueConstraint2.ColumnsReference[0] == this)
									{
										uniqueConstraint = uniqueConstraint2;
									}
								}
								this._table.Constraints.CanRemove(uniqueConstraint, true);
							}
						}
						this._unique = value;
						if (this._table != null)
						{
							if (value)
							{
								UniqueConstraint uniqueConstraint3 = new UniqueConstraint(this);
								this._table.Constraints.Add(uniqueConstraint3);
							}
							else
							{
								this._table.Constraints.Remove(uniqueConstraint);
							}
						}
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		internal void InternalUnique(bool value)
		{
			this._unique = value;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00011DC1 File Offset: 0x0000FFC1
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00011DC9 File Offset: 0x0000FFC9
		internal string XmlDataType { get; set; } = string.Empty;

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00011DD2 File Offset: 0x0000FFD2
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00011DDA File Offset: 0x0000FFDA
		internal SimpleType SimpleType
		{
			get
			{
				return this._simpleType;
			}
			set
			{
				this._simpleType = value;
				if (value != null && value.CanHaveMaxLength())
				{
					this._maxLength = this._simpleType.MaxLength;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.MappingType" /> of the column.</summary>
		/// <returns>One of the <see cref="T:System.Data.MappingType" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00011DFF File Offset: 0x0000FFFF
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00011E08 File Offset: 0x00010008
		[DefaultValue(MappingType.Element)]
		public virtual MappingType ColumnMapping
		{
			get
			{
				return this._columnMapping;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, MappingType>("<ds.DataColumn.set_ColumnMapping|API> {0}, {1}", this.ObjectID, value);
				if (value != this._columnMapping)
				{
					if (value == MappingType.SimpleContent && this._table != null)
					{
						int num = 0;
						if (this._columnMapping == MappingType.Element)
						{
							num = 1;
						}
						if (this._dataType == typeof(char))
						{
							throw ExceptionBuilder.CannotSetSimpleContent(this.ColumnName, this._dataType);
						}
						if (this._table.XmlText != null && this._table.XmlText != this)
						{
							throw ExceptionBuilder.CannotAddColumn3();
						}
						if (this._table.ElementColumnCount > num)
						{
							throw ExceptionBuilder.CannotAddColumn4(this.ColumnName);
						}
					}
					this.RaisePropertyChanging("ColumnMapping");
					if (this._table != null)
					{
						if (this._columnMapping == MappingType.SimpleContent)
						{
							this._table._xmlText = null;
						}
						if (value == MappingType.Element)
						{
							DataTable table = this._table;
							int num2 = table.ElementColumnCount;
							table.ElementColumnCount = num2 + 1;
						}
						else if (this._columnMapping == MappingType.Element)
						{
							DataTable table2 = this._table;
							int num2 = table2.ElementColumnCount;
							table2.ElementColumnCount = num2 - 1;
						}
					}
					this._columnMapping = value;
					if (value == MappingType.SimpleContent)
					{
						this._columnUri = null;
						if (this._table != null)
						{
							this._table.XmlText = this;
						}
						this.SimpleType = null;
					}
				}
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000360 RID: 864 RVA: 0x00011F44 File Offset: 0x00010144
		// (remove) Token: 0x06000361 RID: 865 RVA: 0x00011F7C File Offset: 0x0001017C
		internal event PropertyChangedEventHandler PropertyChanging;

		// Token: 0x06000362 RID: 866 RVA: 0x00011FB1 File Offset: 0x000101B1
		internal void CheckColumnConstraint(DataRow row, DataRowAction action)
		{
			if (this._table.UpdatingCurrent(row, action))
			{
				this.CheckNullable(row);
				this.CheckMaxLength(row);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00011FD0 File Offset: 0x000101D0
		internal bool CheckMaxLength()
		{
			if (0 <= this._maxLength && this.Table != null && 0 < this.Table.Rows.Count)
			{
				foreach (object obj in this.Table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.HasVersion(DataRowVersion.Current) && this._maxLength < this.GetStringLength(dataRow.GetCurrentRecordNo()))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00012074 File Offset: 0x00010274
		internal void CheckMaxLength(DataRow dr)
		{
			if (0 <= this._maxLength && this._maxLength < this.GetStringLength(dr.GetDefaultRecord()))
			{
				throw ExceptionBuilder.LongerThanMaxLength(this);
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x06000365 RID: 869 RVA: 0x0001209C File Offset: 0x0001029C
		protected internal void CheckNotAllowNull()
		{
			if (this._storage == null)
			{
				return;
			}
			if (this._sortIndex != null)
			{
				if (this._sortIndex.IsKeyInIndex(this._storage._nullValue))
				{
					throw ExceptionBuilder.NullKeyValues(this.ColumnName);
				}
			}
			else
			{
				foreach (object obj in this._table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState != DataRowState.Deleted)
					{
						if (!this._implementsINullable)
						{
							if (dataRow[this] == DBNull.Value)
							{
								throw ExceptionBuilder.NullKeyValues(this.ColumnName);
							}
						}
						else if (DataStorage.IsObjectNull(dataRow[this]))
						{
							throw ExceptionBuilder.NullKeyValues(this.ColumnName);
						}
					}
				}
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00012170 File Offset: 0x00010370
		internal void CheckNullable(DataRow row)
		{
			if (!this.AllowDBNull && this._storage.IsNull(row.GetDefaultRecord()))
			{
				throw ExceptionBuilder.NullValues(this.ColumnName);
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x06000367 RID: 871 RVA: 0x00012199 File Offset: 0x00010399
		protected void CheckUnique()
		{
			if (!this.SortIndex.CheckUnique())
			{
				throw ExceptionBuilder.NonUniqueValues(this.ColumnName);
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000121B4 File Offset: 0x000103B4
		internal int Compare(int record1, int record2)
		{
			return this._storage.Compare(record1, record2);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000121C4 File Offset: 0x000103C4
		internal bool CompareValueTo(int record1, object value, bool checkType)
		{
			if (this.CompareValueTo(record1, value) == 0)
			{
				Type type = value.GetType();
				Type type2 = this._storage.Get(record1).GetType();
				if (type == typeof(string) && type2 == typeof(string))
				{
					return string.CompareOrdinal((string)this._storage.Get(record1), (string)value) == 0;
				}
				if (type == type2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00012246 File Offset: 0x00010446
		internal int CompareValueTo(int record1, object value)
		{
			return this._storage.CompareValueTo(record1, value);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012255 File Offset: 0x00010455
		internal object ConvertValue(object value)
		{
			return this._storage.ConvertValue(value);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00012263 File Offset: 0x00010463
		internal void Copy(int srcRecordNo, int dstRecordNo)
		{
			this._storage.Copy(srcRecordNo, dstRecordNo);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012274 File Offset: 0x00010474
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal DataColumn Clone()
		{
			DataColumn dataColumn = (DataColumn)Activator.CreateInstance(base.GetType());
			dataColumn.SimpleType = this.SimpleType;
			dataColumn._allowNull = this._allowNull;
			if (this._autoInc != null)
			{
				dataColumn._autoInc = this._autoInc.Clone();
			}
			dataColumn._caption = this._caption;
			dataColumn.ColumnName = this.ColumnName;
			dataColumn._columnUri = this._columnUri;
			dataColumn._columnPrefix = this._columnPrefix;
			dataColumn.DataType = this.DataType;
			dataColumn._defaultValue = this._defaultValue;
			dataColumn._defaultValueIsNull = this._defaultValue == DBNull.Value || (dataColumn.ImplementsINullable && DataStorage.IsObjectSqlNull(this._defaultValue));
			dataColumn._columnMapping = this._columnMapping;
			dataColumn._readOnly = this._readOnly;
			dataColumn.MaxLength = this.MaxLength;
			dataColumn.XmlDataType = this.XmlDataType;
			dataColumn._dateTimeMode = this._dateTimeMode;
			if (this._extendedProperties != null)
			{
				foreach (object obj in this._extendedProperties.Keys)
				{
					dataColumn.ExtendedProperties[obj] = this._extendedProperties[obj];
				}
			}
			return dataColumn;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000123DC File Offset: 0x000105DC
		internal DataRelation FindParentRelation()
		{
			DataRelation[] array = new DataRelation[this.Table.ParentRelations.Count];
			this.Table.ParentRelations.CopyTo(array, 0);
			foreach (DataRelation dataRelation in array)
			{
				DataKey childKey = dataRelation.ChildKey;
				if (childKey.ColumnsReference.Length == 1 && childKey.ColumnsReference[0] == this)
				{
					return dataRelation;
				}
			}
			return null;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00012445 File Offset: 0x00010645
		internal object GetAggregateValue(int[] records, AggregateType kind)
		{
			if (this._storage != null)
			{
				return this._storage.Aggregate(records, kind);
			}
			if (kind != AggregateType.Count)
			{
				return DBNull.Value;
			}
			return 0;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001246E File Offset: 0x0001066E
		private int GetStringLength(int record)
		{
			return this._storage.GetStringLength(record);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001247C File Offset: 0x0001067C
		internal void Init(int record)
		{
			if (this.AutoIncrement)
			{
				object obj = this._autoInc.Current;
				this._autoInc.MoveAfter();
				this._storage.Set(record, obj);
				return;
			}
			this[record] = this._defaultValue;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000124C4 File Offset: 0x000106C4
		internal static bool IsAutoIncrementType(Type dataType)
		{
			return dataType == typeof(int) || dataType == typeof(long) || dataType == typeof(short) || dataType == typeof(decimal) || dataType == typeof(BigInteger) || dataType == typeof(SqlInt32) || dataType == typeof(SqlInt64) || dataType == typeof(SqlInt16) || dataType == typeof(SqlDecimal);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00012576 File Offset: 0x00010776
		private bool IsColumnMappingValid(StorageType typeCode, MappingType mapping)
		{
			return mapping == MappingType.Element || !DataStorage.IsTypeCustomType(typeCode);
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00012587 File Offset: 0x00010787
		internal bool IsCustomType
		{
			get
			{
				if (this._storage == null)
				{
					return DataStorage.IsTypeCustomType(this.DataType);
				}
				return this._storage._isCustomDefinedType;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000125A8 File Offset: 0x000107A8
		internal bool IsValueCustomTypeInstance(object value)
		{
			return DataStorage.IsTypeCustomType(value.GetType()) && !(value is Type);
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000125C5 File Offset: 0x000107C5
		internal bool ImplementsIXMLSerializable
		{
			get
			{
				return this._implementsIXMLSerializable;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000125CD File Offset: 0x000107CD
		internal bool IsNull(int record)
		{
			return this._storage.IsNull(record);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000125DC File Offset: 0x000107DC
		internal bool IsInRelation()
		{
			DataRelationCollection dataRelationCollection = this._table.ParentRelations;
			for (int i = 0; i < dataRelationCollection.Count; i++)
			{
				if (dataRelationCollection[i].ChildKey.ContainsColumn(this))
				{
					return true;
				}
			}
			dataRelationCollection = this._table.ChildRelations;
			for (int j = 0; j < dataRelationCollection.Count; j++)
			{
				if (dataRelationCollection[j].ParentKey.ContainsColumn(this))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00012658 File Offset: 0x00010858
		internal bool IsMaxLengthViolated()
		{
			if (this.MaxLength < 0)
			{
				return true;
			}
			bool flag = false;
			string text = null;
			foreach (object obj in this.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.HasVersion(DataRowVersion.Current))
				{
					object obj2 = dataRow[this];
					if (!this._isSqlType)
					{
						if (obj2 != null && obj2 != DBNull.Value && ((string)obj2).Length > this.MaxLength)
						{
							if (text == null)
							{
								text = ExceptionBuilder.MaxLengthViolationText(this.ColumnName);
							}
							dataRow.RowError = text;
							dataRow.SetColumnError(this, text);
							flag = true;
						}
					}
					else if (!DataStorage.IsObjectNull(obj2) && ((SqlString)obj2).Value.Length > this.MaxLength)
					{
						if (text == null)
						{
							text = ExceptionBuilder.MaxLengthViolationText(this.ColumnName);
						}
						dataRow.RowError = text;
						dataRow.SetColumnError(this, text);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00012778 File Offset: 0x00010978
		internal bool IsNotAllowDBNullViolated()
		{
			Index sortIndex = this.SortIndex;
			DataRow[] rows = sortIndex.GetRows(sortIndex.FindRecords(DBNull.Value));
			for (int i = 0; i < rows.Length; i++)
			{
				string text = ExceptionBuilder.NotAllowDBNullViolationText(this.ColumnName);
				rows[i].RowError = text;
				rows[i].SetColumnError(this, text);
			}
			return rows.Length != 0;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000127CE File Offset: 0x000109CE
		internal void FinishInitInProgress()
		{
			if (this.Computed)
			{
				this.BindExpression();
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="pcevent">Parameter reference.</param>
		// Token: 0x0600037C RID: 892 RVA: 0x000127DE File Offset: 0x000109DE
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			PropertyChangedEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging(this, pcevent);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="name">Parameter reference.</param>
		// Token: 0x0600037D RID: 893 RVA: 0x000127F2 File Offset: 0x000109F2
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00012800 File Offset: 0x00010A00
		private void InsureStorage()
		{
			if (this._storage == null)
			{
				this._storage = DataStorage.CreateStorage(this, this._dataType, this._storageType);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00012822 File Offset: 0x00010A22
		internal void SetCapacity(int capacity)
		{
			this.InsureStorage();
			this._storage.SetCapacity(capacity);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00012836 File Offset: 0x00010A36
		private bool ShouldSerializeDefaultValue()
		{
			return !this.DefaultValueIsNull;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00005E03 File Offset: 0x00004003
		internal void OnSetDataSet()
		{
		}

		/// <summary>Gets the <see cref="P:System.Data.DataColumn.Expression" /> of the column, if one exists.</summary>
		/// <returns>The <see cref="P:System.Data.DataColumn.Expression" /> value, if the property is set; otherwise, the <see cref="P:System.Data.DataColumn.ColumnName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000382 RID: 898 RVA: 0x00012841 File Offset: 0x00010A41
		public override string ToString()
		{
			if (this._expression != null)
			{
				return this.ColumnName + " + " + this.Expression;
			}
			return this.ColumnName;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00012868 File Offset: 0x00010A68
		internal object ConvertXmlToObject(string s)
		{
			this.InsureStorage();
			return this._storage.ConvertXmlToObject(s);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001287C File Offset: 0x00010A7C
		internal object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			this.InsureStorage();
			return this._storage.ConvertXmlToObject(xmlReader, xmlAttrib);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00012891 File Offset: 0x00010A91
		internal string ConvertObjectToXml(object value)
		{
			this.InsureStorage();
			return this._storage.ConvertObjectToXml(value);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000128A5 File Offset: 0x00010AA5
		internal void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			this.InsureStorage();
			this._storage.ConvertObjectToXml(value, xmlWriter, xmlAttrib);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000128BB File Offset: 0x00010ABB
		internal object GetEmptyColumnStore(int recordCount)
		{
			this.InsureStorage();
			return this._storage.GetEmptyStorageInternal(recordCount);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000128CF File Offset: 0x00010ACF
		internal void CopyValueIntoStore(int record, object store, BitArray nullbits, int storeIndex)
		{
			this._storage.CopyValueInternal(record, store, nullbits, storeIndex);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000128E1 File Offset: 0x00010AE1
		internal void SetStorage(object store, BitArray nullbits)
		{
			this.InsureStorage();
			this._storage.SetStorageInternal(store, nullbits);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000128F6 File Offset: 0x00010AF6
		internal void AddDependentColumn(DataColumn expressionColumn)
		{
			if (this._dependentColumns == null)
			{
				this._dependentColumns = new List<DataColumn>();
			}
			this._dependentColumns.Add(expressionColumn);
			this._table.AddDependentColumn(expressionColumn);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00012923 File Offset: 0x00010B23
		internal void RemoveDependentColumn(DataColumn expressionColumn)
		{
			if (this._dependentColumns != null && this._dependentColumns.Contains(expressionColumn))
			{
				this._dependentColumns.Remove(expressionColumn);
			}
			this._table.RemoveDependentColumn(expressionColumn);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00012954 File Offset: 0x00010B54
		internal void HandleDependentColumnList(DataExpression oldExpression, DataExpression newExpression)
		{
			if (oldExpression != null)
			{
				foreach (DataColumn dataColumn in oldExpression.GetDependency())
				{
					dataColumn.RemoveDependentColumn(this);
					if (dataColumn._table != this._table)
					{
						this._table.RemoveDependentColumn(this);
					}
				}
				this._table.RemoveDependentColumn(this);
			}
			if (newExpression != null)
			{
				foreach (DataColumn dataColumn2 in newExpression.GetDependency())
				{
					dataColumn2.AddDependentColumn(this);
					if (dataColumn2._table != this._table)
					{
						this._table.AddDependentColumn(this);
					}
				}
				this._table.AddDependentColumn(this);
			}
		}

		// Token: 0x04000516 RID: 1302
		private bool _allowNull = true;

		// Token: 0x04000517 RID: 1303
		private string _caption;

		// Token: 0x04000518 RID: 1304
		private string _columnName;

		// Token: 0x04000519 RID: 1305
		private Type _dataType;

		// Token: 0x0400051A RID: 1306
		private StorageType _storageType;

		// Token: 0x0400051B RID: 1307
		internal object _defaultValue = DBNull.Value;

		// Token: 0x0400051C RID: 1308
		private DataSetDateTime _dateTimeMode = DataSetDateTime.UnspecifiedLocal;

		// Token: 0x0400051D RID: 1309
		private DataExpression _expression;

		// Token: 0x0400051E RID: 1310
		private int _maxLength = -1;

		// Token: 0x0400051F RID: 1311
		private int _ordinal = -1;

		// Token: 0x04000520 RID: 1312
		private bool _readOnly;

		// Token: 0x04000521 RID: 1313
		internal Index _sortIndex;

		// Token: 0x04000522 RID: 1314
		internal DataTable _table;

		// Token: 0x04000523 RID: 1315
		private bool _unique;

		// Token: 0x04000524 RID: 1316
		internal MappingType _columnMapping = MappingType.Element;

		// Token: 0x04000525 RID: 1317
		internal int _hashCode;

		// Token: 0x04000526 RID: 1318
		internal int _errors;

		// Token: 0x04000527 RID: 1319
		private bool _isSqlType;

		// Token: 0x04000528 RID: 1320
		private bool _implementsINullable;

		// Token: 0x04000529 RID: 1321
		private bool _implementsIChangeTracking;

		// Token: 0x0400052A RID: 1322
		private bool _implementsIRevertibleChangeTracking;

		// Token: 0x0400052B RID: 1323
		private bool _implementsIXMLSerializable;

		// Token: 0x0400052C RID: 1324
		private bool _defaultValueIsNull = true;

		// Token: 0x0400052D RID: 1325
		internal List<DataColumn> _dependentColumns;

		// Token: 0x0400052E RID: 1326
		internal PropertyCollection _extendedProperties;

		// Token: 0x0400052F RID: 1327
		private DataStorage _storage;

		// Token: 0x04000530 RID: 1328
		private AutoIncrementValue _autoInc;

		// Token: 0x04000531 RID: 1329
		internal string _columnUri;

		// Token: 0x04000532 RID: 1330
		private string _columnPrefix = string.Empty;

		// Token: 0x04000533 RID: 1331
		internal string _encodedColumnName;

		// Token: 0x04000534 RID: 1332
		internal SimpleType _simpleType;

		// Token: 0x04000535 RID: 1333
		private static int s_objectTypeCount;

		// Token: 0x04000536 RID: 1334
		private readonly int _objectID = Interlocked.Increment(ref DataColumn.s_objectTypeCount);
	}
}
