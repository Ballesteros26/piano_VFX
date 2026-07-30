using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>Contains a generic column mapping for an object that inherits from <see cref="T:System.Data.Common.DataAdapter" />. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000331 RID: 817
	[TypeConverter(typeof(DataColumnMapping.DataColumnMappingConverter))]
	public sealed class DataColumnMapping : MarshalByRefObject, IColumnMapping, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class.</summary>
		// Token: 0x0600257F RID: 9599 RVA: 0x000044AD File Offset: 0x000026AD
		public DataColumnMapping()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class with the specified source column name and <see cref="T:System.Data.DataSet" /> column name to map to.</summary>
		/// <param name="sourceColumn">The case-sensitive column name from a data source. </param>
		/// <param name="dataSetColumn">The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		// Token: 0x06002580 RID: 9600 RVA: 0x000AAFFF File Offset: 0x000A91FF
		public DataColumnMapping(string sourceColumn, string dataSetColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DataSetColumn = dataSetColumn;
		}

		/// <summary>Gets or sets the name of the column within the <see cref="T:System.Data.DataSet" /> to map to.</summary>
		/// <returns>The name of the column within the <see cref="T:System.Data.DataSet" /> to map to. The name is not case sensitive.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x000AB015 File Offset: 0x000A9215
		// (set) Token: 0x06002582 RID: 9602 RVA: 0x000AB026 File Offset: 0x000A9226
		[DefaultValue("")]
		public string DataSetColumn
		{
			get
			{
				return this._dataSetColumnName ?? string.Empty;
			}
			set
			{
				this._dataSetColumnName = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x000AB02F File Offset: 0x000A922F
		// (set) Token: 0x06002584 RID: 9604 RVA: 0x000AB037 File Offset: 0x000A9237
		internal DataColumnMappingCollection Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		/// <summary>Gets or sets the name of the column within the data source to map from. The name is case-sensitive.</summary>
		/// <returns>The case-sensitive name of the column in the data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x000AB040 File Offset: 0x000A9240
		// (set) Token: 0x06002586 RID: 9606 RVA: 0x000AB051 File Offset: 0x000A9251
		[DefaultValue("")]
		public string SourceColumn
		{
			get
			{
				return this._sourceColumnName ?? string.Empty;
			}
			set
			{
				if (this.Parent != null && ADP.SrcCompare(this._sourceColumnName, value) != 0)
				{
					this.Parent.ValidateSourceColumn(-1, value);
				}
				this._sourceColumnName = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current object.</returns>
		// Token: 0x06002587 RID: 9607 RVA: 0x000AB07D File Offset: 0x000A927D
		object ICloneable.Clone()
		{
			return new DataColumnMapping
			{
				_sourceColumnName = this._sourceColumnName,
				_dataSetColumnName = this._dataSetColumnName
			};
		}

		/// <summary>Gets a <see cref="T:System.Data.DataColumn" /> from the given <see cref="T:System.Data.DataTable" /> using the <see cref="T:System.Data.MissingSchemaAction" /> and the <see cref="P:System.Data.Common.DataColumnMapping.DataSetColumn" /> property.</summary>
		/// <returns>A data column.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to get the column from.</param>
		/// <param name="dataType">The <see cref="T:System.Type" /> of the data column.</param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002588 RID: 9608 RVA: 0x000AB09C File Offset: 0x000A929C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumnBySchemaAction(DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			return DataColumnMapping.GetDataColumnBySchemaAction(this.SourceColumn, this.DataSetColumn, dataTable, dataType, schemaAction);
		}

		/// <summary>A static version of <see cref="M:System.Data.Common.DataColumnMapping.GetDataColumnBySchemaAction(System.Data.DataTable,System.Type,System.Data.MissingSchemaAction)" /> that can be called without instantiating a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		/// <param name="sourceColumn">The case-sensitive column name from a data source. </param>
		/// <param name="dataSetColumn">The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		/// <param name="dataTable">An instance of <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="dataType">The data type for the column being mapped.</param>
		/// <param name="schemaAction">Determines the action to take when existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002589 RID: 9609 RVA: 0x000AB0B4 File Offset: 0x000A92B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumn GetDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (dataTable == null)
			{
				throw ADP.ArgumentNull("dataTable");
			}
			if (string.IsNullOrEmpty(dataSetColumn))
			{
				return null;
			}
			DataColumnCollection columns = dataTable.Columns;
			int num = columns.IndexOf(dataSetColumn);
			if (0 > num || num >= columns.Count)
			{
				return DataColumnMapping.CreateDataColumnBySchemaAction(sourceColumn, dataSetColumn, dataTable, dataType, schemaAction);
			}
			DataColumn dataColumn = columns[num];
			if (!string.IsNullOrEmpty(dataColumn.Expression))
			{
				throw ADP.ColumnSchemaExpression(sourceColumn, dataSetColumn);
			}
			if (null == dataType || dataType.IsArray == dataColumn.DataType.IsArray)
			{
				return dataColumn;
			}
			throw ADP.ColumnSchemaMismatch(sourceColumn, dataType, dataColumn);
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000AB144 File Offset: 0x000A9344
		internal static DataColumn CreateDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (string.IsNullOrEmpty(dataSetColumn))
			{
				return null;
			}
			switch (schemaAction)
			{
			case MissingSchemaAction.Add:
			case MissingSchemaAction.AddWithKey:
				return new DataColumn(dataSetColumn, dataType);
			case MissingSchemaAction.Ignore:
				return null;
			case MissingSchemaAction.Error:
				throw ADP.ColumnSchemaMissing(dataSetColumn, dataTable.TableName, sourceColumn);
			default:
				throw ADP.InvalidMissingSchemaAction(schemaAction);
			}
		}

		/// <summary>Converts the current <see cref="P:System.Data.Common.DataColumnMapping.SourceColumn" /> name to a string.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DataColumnMapping.SourceColumn" /> name as a string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600258B RID: 9611 RVA: 0x000AB195 File Offset: 0x000A9395
		public override string ToString()
		{
			return this.SourceColumn;
		}

		// Token: 0x04001848 RID: 6216
		private DataColumnMappingCollection _parent;

		// Token: 0x04001849 RID: 6217
		private string _dataSetColumnName;

		// Token: 0x0400184A RID: 6218
		private string _sourceColumnName;

		// Token: 0x02000332 RID: 818
		internal sealed class DataColumnMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x0600258D RID: 9613 RVA: 0x000AB19D File Offset: 0x000A939D
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x0600258E RID: 9614 RVA: 0x000AB1BC File Offset: 0x000A93BC
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (null == destinationType)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is DataColumnMapping)
				{
					DataColumnMapping dataColumnMapping = (DataColumnMapping)value;
					object[] array = new object[] { dataColumnMapping.SourceColumn, dataColumnMapping.DataSetColumn };
					Type[] array2 = new Type[]
					{
						typeof(string),
						typeof(string)
					};
					return new InstanceDescriptor(typeof(DataColumnMapping).GetConstructor(array2), array);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
