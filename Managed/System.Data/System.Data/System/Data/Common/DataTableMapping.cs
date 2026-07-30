using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>Contains a description of a mapped relationship between a source table and a <see cref="T:System.Data.DataTable" />. This class is used by a <see cref="T:System.Data.Common.DataAdapter" /> when populating a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000338 RID: 824
	[TypeConverter(typeof(DataTableMapping.DataTableMappingConverter))]
	public sealed class DataTableMapping : MarshalByRefObject, ITableMapping, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class.</summary>
		// Token: 0x06002617 RID: 9751 RVA: 0x000044AD File Offset: 0x000026AD
		public DataTableMapping()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class with a source when given a source table name and a <see cref="T:System.Data.DataTable" /> name.</summary>
		/// <param name="sourceTable">The case-sensitive source table name from a data source. </param>
		/// <param name="dataSetTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		// Token: 0x06002618 RID: 9752 RVA: 0x000AC65C File Offset: 0x000AA85C
		public DataTableMapping(string sourceTable, string dataSetTable)
		{
			this.SourceTable = sourceTable;
			this.DataSetTable = dataSetTable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class when given a source table name, a <see cref="T:System.Data.DataTable" /> name, and an array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects.</summary>
		/// <param name="sourceTable">The case-sensitive source table name from a data source. </param>
		/// <param name="dataSetTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		/// <param name="columnMappings">An array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects. </param>
		// Token: 0x06002619 RID: 9753 RVA: 0x000AC672 File Offset: 0x000AA872
		public DataTableMapping(string sourceTable, string dataSetTable, DataColumnMapping[] columnMappings)
		{
			this.SourceTable = sourceTable;
			this.DataSetTable = dataSetTable;
			if (columnMappings != null && columnMappings.Length != 0)
			{
				this.ColumnMappings.AddRange(columnMappings);
			}
		}

		/// <summary>Gets the derived <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A data column mapping collection.</returns>
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x000AC69B File Offset: 0x000AA89B
		IColumnMappingCollection ITableMapping.ColumnMappings
		{
			get
			{
				return this.ColumnMappings;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A data column mapping collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x000AC6A4 File Offset: 0x000AA8A4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataColumnMappingCollection ColumnMappings
		{
			get
			{
				DataColumnMappingCollection dataColumnMappingCollection = this._columnMappings;
				if (dataColumnMappingCollection == null)
				{
					dataColumnMappingCollection = new DataColumnMappingCollection();
					this._columnMappings = dataColumnMappingCollection;
				}
				return dataColumnMappingCollection;
			}
		}

		/// <summary>Gets or sets the table name from a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The table name from a <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000AC6C9 File Offset: 0x000AA8C9
		// (set) Token: 0x0600261D RID: 9757 RVA: 0x000AC6DA File Offset: 0x000AA8DA
		[DefaultValue("")]
		public string DataSetTable
		{
			get
			{
				return this._dataSetTableName ?? string.Empty;
			}
			set
			{
				this._dataSetTableName = value;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x000AC6E3 File Offset: 0x000AA8E3
		// (set) Token: 0x0600261F RID: 9759 RVA: 0x000AC6EB File Offset: 0x000AA8EB
		internal DataTableMappingCollection Parent
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

		/// <summary>Gets or sets the case-sensitive source table name from a data source.</summary>
		/// <returns>The case-sensitive source table name from a data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x000AC6F4 File Offset: 0x000AA8F4
		// (set) Token: 0x06002621 RID: 9761 RVA: 0x000AC705 File Offset: 0x000AA905
		[DefaultValue("")]
		public string SourceTable
		{
			get
			{
				return this._sourceTableName ?? string.Empty;
			}
			set
			{
				if (this.Parent != null && ADP.SrcCompare(this._sourceTableName, value) != 0)
				{
					this.Parent.ValidateSourceTable(-1, value);
				}
				this._sourceTableName = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of the current instance.</returns>
		// Token: 0x06002622 RID: 9762 RVA: 0x000AC734 File Offset: 0x000AA934
		object ICloneable.Clone()
		{
			DataTableMapping dataTableMapping = new DataTableMapping();
			dataTableMapping._dataSetTableName = this._dataSetTableName;
			dataTableMapping._sourceTableName = this._sourceTableName;
			if (this._columnMappings != null && 0 < this.ColumnMappings.Count)
			{
				DataColumnMappingCollection columnMappings = dataTableMapping.ColumnMappings;
				foreach (object obj in this.ColumnMappings)
				{
					ICloneable cloneable = (ICloneable)obj;
					columnMappings.Add(cloneable.Clone());
				}
			}
			return dataTableMapping;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataColumn" /> object for a given column name.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		/// <param name="sourceColumn">The name of the <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="dataType">The data type for <paramref name="sourceColumn" />.</param>
		/// <param name="dataTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values. </param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002623 RID: 9763 RVA: 0x000AC7D4 File Offset: 0x000AA9D4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumn(string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			return DataColumnMappingCollection.GetDataColumn(this._columnMappings, sourceColumn, dataType, dataTable, mappingAction, schemaAction);
		}

		/// <summary>Gets a <see cref="T:System.Data.DataColumn" /> from the specified <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Data.MissingMappingAction" /> value and the name of the <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>A data column.</returns>
		/// <param name="sourceColumn">The name of the <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to Error, and no mapping was specified. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002624 RID: 9764 RVA: 0x000AC7E8 File Offset: 0x000AA9E8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumnMapping GetColumnMappingBySchemaAction(string sourceColumn, MissingMappingAction mappingAction)
		{
			return DataColumnMappingCollection.GetColumnMappingBySchemaAction(this._columnMappings, sourceColumn, mappingAction);
		}

		/// <summary>Gets the current <see cref="T:System.Data.DataTable" /> for the specified <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Data.MissingSchemaAction" /> value.</summary>
		/// <returns>A data table.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> from which to get the <see cref="T:System.Data.DataTable" />. </param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002625 RID: 9765 RVA: 0x000AC7F8 File Offset: 0x000AA9F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataTable GetDataTableBySchemaAction(DataSet dataSet, MissingSchemaAction schemaAction)
		{
			if (dataSet == null)
			{
				throw ADP.ArgumentNull("dataSet");
			}
			string dataSetTable = this.DataSetTable;
			if (string.IsNullOrEmpty(dataSetTable))
			{
				return null;
			}
			DataTableCollection tables = dataSet.Tables;
			int num = tables.IndexOf(dataSetTable);
			if (0 <= num && num < tables.Count)
			{
				return tables[num];
			}
			switch (schemaAction)
			{
			case MissingSchemaAction.Add:
			case MissingSchemaAction.AddWithKey:
				return new DataTable(dataSetTable);
			case MissingSchemaAction.Ignore:
				return null;
			case MissingSchemaAction.Error:
				throw ADP.MissingTableSchema(dataSetTable, this.SourceTable);
			default:
				throw ADP.InvalidMissingSchemaAction(schemaAction);
			}
		}

		/// <summary>Converts the current <see cref="P:System.Data.Common.DataTableMapping.SourceTable" /> name to a string.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DataTableMapping.SourceTable" /> name, as a string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002626 RID: 9766 RVA: 0x000AC87E File Offset: 0x000AAA7E
		public override string ToString()
		{
			return this.SourceTable;
		}

		// Token: 0x0400188B RID: 6283
		private DataTableMappingCollection _parent;

		// Token: 0x0400188C RID: 6284
		private DataColumnMappingCollection _columnMappings;

		// Token: 0x0400188D RID: 6285
		private string _dataSetTableName;

		// Token: 0x0400188E RID: 6286
		private string _sourceTableName;

		// Token: 0x02000339 RID: 825
		internal sealed class DataTableMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x06002628 RID: 9768 RVA: 0x000AB19D File Offset: 0x000A939D
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06002629 RID: 9769 RVA: 0x000AC888 File Offset: 0x000AAA88
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (null == destinationType)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is DataTableMapping)
				{
					DataTableMapping dataTableMapping = (DataTableMapping)value;
					DataColumnMapping[] array = new DataColumnMapping[dataTableMapping.ColumnMappings.Count];
					dataTableMapping.ColumnMappings.CopyTo(array, 0);
					object[] array2 = new object[] { dataTableMapping.SourceTable, dataTableMapping.DataSetTable, array };
					Type[] array3 = new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(DataColumnMapping[])
					};
					return new InstanceDescriptor(typeof(DataTableMapping).GetConstructor(array3), array2);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
