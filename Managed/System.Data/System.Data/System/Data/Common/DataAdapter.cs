using System;
using System.ComponentModel;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace System.Data.Common
{
	/// <summary>Represents a set of SQL commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200032F RID: 815
	public class DataAdapter : Component, IDataAdapter
	{
		// Token: 0x06002546 RID: 9542 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		private void AssertReaderHandleFieldCount(DataReaderContainer readerHandler)
		{
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		private void AssertSchemaMapping(SchemaMapping mapping)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DataAdapter" /> class.</summary>
		// Token: 0x06002548 RID: 9544 RVA: 0x000AA4C8 File Offset: 0x000A86C8
		protected DataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DataAdapter" /> class from an existing object of the same type.</summary>
		/// <param name="from">A <see cref="T:System.Data.Common.DataAdapter" /> object used to create the new <see cref="T:System.Data.Common.DataAdapter" />. </param>
		// Token: 0x06002549 RID: 9545 RVA: 0x000AA514 File Offset: 0x000A8714
		protected DataAdapter(DataAdapter from)
		{
			this.CloneFrom(from);
		}

		/// <summary>Gets or sets a value indicating whether <see cref="M:System.Data.DataRow.AcceptChanges" /> is called on a <see cref="T:System.Data.DataRow" /> after it is added to the <see cref="T:System.Data.DataTable" /> during any of the Fill operations.</summary>
		/// <returns>true if <see cref="M:System.Data.DataRow.AcceptChanges" /> is called on the <see cref="T:System.Data.DataRow" />; otherwise false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x000AA561 File Offset: 0x000A8761
		// (set) Token: 0x0600254B RID: 9547 RVA: 0x000AA569 File Offset: 0x000A8769
		[DefaultValue(true)]
		public bool AcceptChangesDuringFill
		{
			get
			{
				return this._acceptChangesDuringFill;
			}
			set
			{
				this._acceptChangesDuringFill = value;
			}
		}

		/// <summary>Determines whether the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" /> property should be persisted.</summary>
		/// <returns>true if the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" /> property is persisted; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600254C RID: 9548 RVA: 0x000AA572 File Offset: 0x000A8772
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeAcceptChangesDuringFill()
		{
			return this._fillLoadOption == (LoadOption)0;
		}

		/// <summary>Gets or sets whether <see cref="M:System.Data.DataRow.AcceptChanges" /> is called during a <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>true if <see cref="M:System.Data.DataRow.AcceptChanges" /> is called during an <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)" />; otherwise false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x000AA57D File Offset: 0x000A877D
		// (set) Token: 0x0600254E RID: 9550 RVA: 0x000AA585 File Offset: 0x000A8785
		[DefaultValue(true)]
		public bool AcceptChangesDuringUpdate
		{
			get
			{
				return this._acceptChangesDuringUpdate;
			}
			set
			{
				this._acceptChangesDuringUpdate = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether to generate an exception when an error is encountered during a row update.</summary>
		/// <returns>true to continue the update without generating an exception; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x000AA58E File Offset: 0x000A878E
		// (set) Token: 0x06002550 RID: 9552 RVA: 0x000AA596 File Offset: 0x000A8796
		[DefaultValue(false)]
		public bool ContinueUpdateOnError
		{
			get
			{
				return this._continueUpdateOnError;
			}
			set
			{
				this._continueUpdateOnError = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.LoadOption" /> that determines how the adapter fills the <see cref="T:System.Data.DataTable" /> from the <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.LoadOption" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x000AA5A0 File Offset: 0x000A87A0
		// (set) Token: 0x06002552 RID: 9554 RVA: 0x000AA5BF File Offset: 0x000A87BF
		[RefreshProperties(RefreshProperties.All)]
		public LoadOption FillLoadOption
		{
			get
			{
				if (this._fillLoadOption == (LoadOption)0)
				{
					return LoadOption.OverwriteChanges;
				}
				return this._fillLoadOption;
			}
			set
			{
				if (value <= LoadOption.Upsert)
				{
					this._fillLoadOption = value;
					return;
				}
				throw ADP.InvalidLoadOption(value);
			}
		}

		/// <summary>Resets <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> to its default state and causes <see cref="M:System.Data.Common.DataAdapter.Fill(System.Data.DataSet)" /> to honor <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002553 RID: 9555 RVA: 0x000AA5D3 File Offset: 0x000A87D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetFillLoadOption()
		{
			this._fillLoadOption = (LoadOption)0;
		}

		/// <summary>Determines whether the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> property should be persisted.</summary>
		/// <returns>true if the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> property is persisted; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002554 RID: 9556 RVA: 0x000AA5DC File Offset: 0x000A87DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeFillLoadOption()
		{
			return this._fillLoadOption > (LoadOption)0;
		}

		/// <summary>Determines the action to take when incoming data does not have a matching table or column.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingMappingAction" /> values. The default is Passthrough.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingMappingAction" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000AA5E7 File Offset: 0x000A87E7
		// (set) Token: 0x06002556 RID: 9558 RVA: 0x000AA5EF File Offset: 0x000A87EF
		[DefaultValue(MissingMappingAction.Passthrough)]
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return this._missingMappingAction;
			}
			set
			{
				if (value - MissingMappingAction.Passthrough <= 2)
				{
					this._missingMappingAction = value;
					return;
				}
				throw ADP.InvalidMissingMappingAction(value);
			}
		}

		/// <summary>Determines the action to take when existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingSchemaAction" /> values. The default is Add.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingSchemaAction" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000AA605 File Offset: 0x000A8805
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x000AA60D File Offset: 0x000A880D
		[DefaultValue(MissingSchemaAction.Add)]
		public MissingSchemaAction MissingSchemaAction
		{
			get
			{
				return this._missingSchemaAction;
			}
			set
			{
				if (value - MissingSchemaAction.Add <= 3)
				{
					this._missingSchemaAction = value;
					return;
				}
				throw ADP.InvalidMissingSchemaAction(value);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x000AA623 File Offset: 0x000A8823
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets or sets whether the Fill method should return provider-specific values or common CLS-compliant values.</summary>
		/// <returns>true if the Fill method should return provider-specific values; otherwise false to return common CLS-compliant values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x000AA62B File Offset: 0x000A882B
		// (set) Token: 0x0600255B RID: 9563 RVA: 0x000AA633 File Offset: 0x000A8833
		[DefaultValue(false)]
		public virtual bool ReturnProviderSpecificTypes
		{
			get
			{
				return this._returnProviderSpecificTypes;
			}
			set
			{
				this._returnProviderSpecificTypes = value;
			}
		}

		/// <summary>Gets a collection that provides the master mapping between a source table and a <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet" />. The default value is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000AA63C File Offset: 0x000A883C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataTableMappingCollection TableMappings
		{
			get
			{
				DataTableMappingCollection dataTableMappingCollection = this._tableMappings;
				if (dataTableMappingCollection == null)
				{
					dataTableMappingCollection = this.CreateTableMappings();
					if (dataTableMappingCollection == null)
					{
						dataTableMappingCollection = new DataTableMappingCollection();
					}
					this._tableMappings = dataTableMappingCollection;
				}
				return dataTableMappingCollection;
			}
		}

		/// <summary>Indicates how a source table is mapped to a dataset table.</summary>
		/// <returns>A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet" />. The default value is an empty collection.</returns>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000AA66B File Offset: 0x000A886B
		ITableMappingCollection IDataAdapter.TableMappings
		{
			get
			{
				return this.TableMappings;
			}
		}

		/// <summary>Determines whether one or more <see cref="T:System.Data.Common.DataTableMapping" /> objects exist and they should be persisted.</summary>
		/// <returns>true if one or more <see cref="T:System.Data.Common.DataTableMapping" /> objects exist; otherwise false.</returns>
		// Token: 0x0600255E RID: 9566 RVA: 0x0000EF2B File Offset: 0x0000D12B
		protected virtual bool ShouldSerializeTableMappings()
		{
			return true;
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.Common.DataTableMappingCollection" /> has been created.</summary>
		/// <returns>true if a <see cref="T:System.Data.Common.DataTableMappingCollection" /> has been created; otherwise false.</returns>
		// Token: 0x0600255F RID: 9567 RVA: 0x000AA673 File Offset: 0x000A8873
		protected bool HasTableMappings()
		{
			return this._tableMappings != null && 0 < this.TableMappings.Count;
		}

		/// <summary>Returned when an error occurs during a fill operation.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06002560 RID: 9568 RVA: 0x000AA68D File Offset: 0x000A888D
		// (remove) Token: 0x06002561 RID: 9569 RVA: 0x000AA6A7 File Offset: 0x000A88A7
		public event FillErrorEventHandler FillError
		{
			add
			{
				this._hasFillErrorHandler = true;
				base.Events.AddHandler(DataAdapter.s_eventFillError, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataAdapter.s_eventFillError, value);
			}
		}

		/// <summary>Creates a copy of this instance of <see cref="T:System.Data.Common.DataAdapter" />.</summary>
		/// <returns>The cloned instance of <see cref="T:System.Data.Common.DataAdapter" />.</returns>
		// Token: 0x06002562 RID: 9570 RVA: 0x000AA6BA File Offset: 0x000A88BA
		[Obsolete("CloneInternals() has been deprecated.  Use the DataAdapter(DataAdapter from) constructor.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual DataAdapter CloneInternals()
		{
			DataAdapter dataAdapter = (DataAdapter)Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null);
			dataAdapter.CloneFrom(this);
			return dataAdapter;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000AA6E0 File Offset: 0x000A88E0
		private void CloneFrom(DataAdapter from)
		{
			this._acceptChangesDuringUpdate = from._acceptChangesDuringUpdate;
			this._acceptChangesDuringUpdateAfterInsert = from._acceptChangesDuringUpdateAfterInsert;
			this._continueUpdateOnError = from._continueUpdateOnError;
			this._returnProviderSpecificTypes = from._returnProviderSpecificTypes;
			this._acceptChangesDuringFill = from._acceptChangesDuringFill;
			this._fillLoadOption = from._fillLoadOption;
			this._missingMappingAction = from._missingMappingAction;
			this._missingSchemaAction = from._missingSchemaAction;
			if (from._tableMappings != null && 0 < from.TableMappings.Count)
			{
				DataTableMappingCollection tableMappings = this.TableMappings;
				foreach (object obj in from.TableMappings)
				{
					tableMappings.Add((obj is ICloneable) ? ((ICloneable)obj).Clone() : obj);
				}
			}
		}

		/// <summary>Creates a new <see cref="T:System.Data.Common.DataTableMappingCollection" />.</summary>
		/// <returns>A new table mapping collection.</returns>
		// Token: 0x06002564 RID: 9572 RVA: 0x000AA7C8 File Offset: 0x000A89C8
		protected virtual DataTableMappingCollection CreateTableMappings()
		{
			DataCommonEventSource.Log.Trace<int>("<comm.DataAdapter.CreateTableMappings|API> {0}", this.ObjectID);
			return new DataTableMappingCollection();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DataAdapter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002565 RID: 9573 RVA: 0x000AA7E4 File Offset: 0x000A89E4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._tableMappings = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002566 RID: 9574 RVA: 0x000621D6 File Offset: 0x000603D6
		public virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
		/// <param name="srcTable">The name of the source table to use for table mapping.</param>
		/// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
		// Token: 0x06002567 RID: 9575 RVA: 0x000AA7F8 File Offset: 0x000A89F8
		protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable, IDataReader dataReader)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, SchemaType>("<comm.DataAdapter.FillSchema|API> {0}, dataSet, schemaType={1}, srcTable, dataReader", this.ObjectID, schemaType);
			DataTable[] array;
			try
			{
				if (dataSet == null)
				{
					throw ADP.ArgumentNull("dataSet");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.FillSchemaRequiresSourceTableName("srcTable");
				}
				if (dataReader == null || dataReader.IsClosed)
				{
					throw ADP.FillRequires("dataReader");
				}
				array = (DataTable[])this.FillSchemaFromReader(dataSet, null, schemaType, srcTable, dataReader);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return array;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
		/// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
		// Token: 0x06002568 RID: 9576 RVA: 0x000AA898 File Offset: 0x000A8A98
		protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDataReader dataReader)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DataAdapter.FillSchema|API> {0}, dataTable, schemaType, dataReader", this.ObjectID);
			DataTable dataTable2;
			try
			{
				if (dataTable == null)
				{
					throw ADP.ArgumentNull("dataTable");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (dataReader == null || dataReader.IsClosed)
				{
					throw ADP.FillRequires("dataReader");
				}
				dataTable2 = (DataTable)this.FillSchemaFromReader(null, dataTable, schemaType, null, dataReader);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000AA920 File Offset: 0x000A8B20
		internal object FillSchemaFromReader(DataSet dataset, DataTable datatable, SchemaType schemaType, string srcTable, IDataReader dataReader)
		{
			DataTable[] array = null;
			int num = 0;
			SchemaMapping schemaMapping;
			for (;;)
			{
				DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
				if (0 < dataReaderContainer.FieldCount)
				{
					string text = null;
					if (dataset != null)
					{
						text = DataAdapter.GetSourceTableName(srcTable, num);
						num++;
					}
					schemaMapping = new SchemaMapping(this, dataset, datatable, dataReaderContainer, true, schemaType, text, false, null, null);
					if (datatable != null)
					{
						break;
					}
					if (schemaMapping.DataTable != null)
					{
						if (array == null)
						{
							array = new DataTable[] { schemaMapping.DataTable };
						}
						else
						{
							array = DataAdapter.AddDataTableToArray(array, schemaMapping.DataTable);
						}
					}
				}
				if (!dataReader.NextResult())
				{
					goto Block_6;
				}
			}
			return schemaMapping.DataTable;
			Block_6:
			object obj = array;
			if (obj == null && datatable == null)
			{
				obj = Array.Empty<DataTable>();
			}
			return obj;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in the data source.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600256A RID: 9578 RVA: 0x000621D6 File Offset: 0x000603D6
		public virtual int Fill(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records.</param>
		/// <param name="srcTable">A string indicating the name of the source table.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="startRecord">The zero-based index of the starting record.</param>
		/// <param name="maxRecords">An integer indicating the maximum number of records.</param>
		// Token: 0x0600256B RID: 9579 RVA: 0x000AA9C0 File Offset: 0x000A8BC0
		protected virtual int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DataAdapter.Fill|API> {0}, dataSet, srcTable, dataReader, startRecord, maxRecords", this.ObjectID);
			int num2;
			try
			{
				if (dataSet == null)
				{
					throw ADP.FillRequires("dataSet");
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.FillRequiresSourceTableName("srcTable");
				}
				if (dataReader == null)
				{
					throw ADP.FillRequires("dataReader");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (dataReader.IsClosed)
				{
					num2 = 0;
				}
				else
				{
					DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
					num2 = this.FillFromReader(dataSet, null, srcTable, dataReaderContainer, startRecord, maxRecords, null, null);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataTable" /> to match those in the data source using the <see cref="T:System.Data.DataTable" /> name and the specified <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		// Token: 0x0600256C RID: 9580 RVA: 0x000AAA80 File Offset: 0x000A8C80
		protected virtual int Fill(DataTable dataTable, IDataReader dataReader)
		{
			DataTable[] array = new DataTable[] { dataTable };
			return this.Fill(array, dataReader, 0, 0);
		}

		/// <summary>Adds or refreshes rows in a specified range in the collection of <see cref="T:System.Data.DataTable" /> objects to match those in the data source.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTables">A collection of <see cref="T:System.Data.DataTable" /> objects to fill with records.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="startRecord">The zero-based index of the starting record.</param>
		/// <param name="maxRecords">An integer indicating the maximum number of records.</param>
		// Token: 0x0600256D RID: 9581 RVA: 0x000AAAA4 File Offset: 0x000A8CA4
		protected virtual int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DataAdapter.Fill|API> {0}, dataTables[], dataReader, startRecord, maxRecords", this.ObjectID);
			int num5;
			try
			{
				ADP.CheckArgumentLength(dataTables, "dataTables");
				if (dataTables == null || dataTables.Length == 0 || dataTables[0] == null)
				{
					throw ADP.FillRequires("dataTable");
				}
				if (dataReader == null)
				{
					throw ADP.FillRequires("dataReader");
				}
				if (1 < dataTables.Length && (startRecord != 0 || maxRecords != 0))
				{
					throw ADP.NotSupported();
				}
				int num2 = 0;
				bool flag = false;
				DataSet dataSet = dataTables[0].DataSet;
				try
				{
					if (dataSet != null)
					{
						flag = dataSet.EnforceConstraints;
						dataSet.EnforceConstraints = false;
					}
					int num3 = 0;
					while (num3 < dataTables.Length && !dataReader.IsClosed)
					{
						DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
						if (dataReaderContainer.FieldCount > 0)
						{
							goto IL_00BC;
						}
						if (num3 == 0)
						{
							bool flag2;
							do
							{
								flag2 = this.FillNextResult(dataReaderContainer);
							}
							while (flag2 && dataReaderContainer.FieldCount <= 0);
							if (flag2)
							{
								goto IL_00BC;
							}
							break;
						}
						IL_00E7:
						num3++;
						continue;
						IL_00BC:
						if (0 < num3 && !this.FillNextResult(dataReaderContainer))
						{
							break;
						}
						int num4 = this.FillFromReader(null, dataTables[num3], null, dataReaderContainer, startRecord, maxRecords, null, null);
						if (num3 == 0)
						{
							num2 = num4;
							goto IL_00E7;
						}
						goto IL_00E7;
					}
				}
				catch (ConstraintException)
				{
					flag = false;
					throw;
				}
				finally
				{
					if (flag)
					{
						dataSet.EnforceConstraints = true;
					}
				}
				num5 = num2;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num5;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000AABF4 File Offset: 0x000A8DF4
		internal int FillFromReader(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int startRecord, int maxRecords, DataColumn parentChapterColumn, object parentChapterValue)
		{
			int num = 0;
			int num2 = 0;
			do
			{
				if (0 < dataReader.FieldCount)
				{
					SchemaMapping schemaMapping = this.FillMapping(dataset, datatable, srcTable, dataReader, num2, parentChapterColumn, parentChapterValue);
					num2++;
					if (schemaMapping != null && schemaMapping.DataValues != null && schemaMapping.DataTable != null)
					{
						schemaMapping.DataTable.BeginLoadData();
						try
						{
							if (1 == num2 && (0 < startRecord || 0 < maxRecords))
							{
								num = this.FillLoadDataRowChunk(schemaMapping, startRecord, maxRecords);
							}
							else
							{
								int num3 = this.FillLoadDataRow(schemaMapping);
								if (1 == num2)
								{
									num = num3;
								}
							}
						}
						finally
						{
							schemaMapping.DataTable.EndLoadData();
						}
						if (datatable != null)
						{
							break;
						}
					}
				}
			}
			while (this.FillNextResult(dataReader));
			return num;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000AAC9C File Offset: 0x000A8E9C
		private int FillLoadDataRowChunk(SchemaMapping mapping, int startRecord, int maxRecords)
		{
			DataReaderContainer dataReader = mapping.DataReader;
			while (0 < startRecord)
			{
				if (!dataReader.Read())
				{
					return 0;
				}
				startRecord--;
			}
			int i = 0;
			if (0 < maxRecords)
			{
				while (i < maxRecords)
				{
					if (!dataReader.Read())
					{
						break;
					}
					if (this._hasFillErrorHandler)
					{
						try
						{
							mapping.LoadDataRowWithClear();
							i++;
							continue;
						}
						catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
						{
							ADP.TraceExceptionForCapture(ex);
							this.OnFillErrorHandler(ex, mapping.DataTable, mapping.DataValues);
							continue;
						}
					}
					mapping.LoadDataRow();
					i++;
				}
			}
			else
			{
				i = this.FillLoadDataRow(mapping);
			}
			return i;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000AAD48 File Offset: 0x000A8F48
		private int FillLoadDataRow(SchemaMapping mapping)
		{
			int num = 0;
			DataReaderContainer dataReader = mapping.DataReader;
			if (this._hasFillErrorHandler)
			{
				while (dataReader.Read())
				{
					try
					{
						mapping.LoadDataRowWithClear();
						num++;
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ADP.TraceExceptionForCapture(ex);
						this.OnFillErrorHandler(ex, mapping.DataTable, mapping.DataValues);
					}
				}
			}
			else
			{
				while (dataReader.Read())
				{
					mapping.LoadDataRow();
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000AADD8 File Offset: 0x000A8FD8
		private SchemaMapping FillMappingInternal(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int schemaCount, DataColumn parentChapterColumn, object parentChapterValue)
		{
			bool flag = MissingSchemaAction.AddWithKey == this.MissingSchemaAction;
			string text = null;
			if (dataset != null)
			{
				text = DataAdapter.GetSourceTableName(srcTable, schemaCount);
			}
			return new SchemaMapping(this, dataset, datatable, dataReader, flag, SchemaType.Mapped, text, true, parentChapterColumn, parentChapterValue);
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000AAE10 File Offset: 0x000A9010
		private SchemaMapping FillMapping(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int schemaCount, DataColumn parentChapterColumn, object parentChapterValue)
		{
			SchemaMapping schemaMapping = null;
			if (this._hasFillErrorHandler)
			{
				try
				{
					return this.FillMappingInternal(dataset, datatable, srcTable, dataReader, schemaCount, parentChapterColumn, parentChapterValue);
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ADP.TraceExceptionForCapture(ex);
					this.OnFillErrorHandler(ex, null, null);
					return schemaMapping;
				}
			}
			schemaMapping = this.FillMappingInternal(dataset, datatable, srcTable, dataReader, schemaCount, parentChapterColumn, parentChapterValue);
			return schemaMapping;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000AAE88 File Offset: 0x000A9088
		private bool FillNextResult(DataReaderContainer dataReader)
		{
			bool flag = true;
			if (this._hasFillErrorHandler)
			{
				try
				{
					return dataReader.NextResult();
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ADP.TraceExceptionForCapture(ex);
					this.OnFillErrorHandler(ex, null, null);
					return flag;
				}
			}
			flag = dataReader.NextResult();
			return flag;
		}

		/// <summary>Gets the parameters set by the user when executing an SQL SELECT statement.</summary>
		/// <returns>An array of <see cref="T:System.Data.IDataParameter" /> objects that contains the parameters set by the user.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002574 RID: 9588 RVA: 0x000AAEEC File Offset: 0x000A90EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual IDataParameter[] GetFillParameters()
		{
			return Array.Empty<IDataParameter>();
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000AAEF3 File Offset: 0x000A90F3
		internal DataTableMapping GetTableMappingBySchemaAction(string sourceTableName, string dataSetTableName, MissingMappingAction mappingAction)
		{
			return DataTableMappingCollection.GetTableMappingBySchemaAction(this._tableMappings, sourceTableName, dataSetTableName, mappingAction);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000AAF03 File Offset: 0x000A9103
		internal int IndexOfDataSetTable(string dataSetTable)
		{
			if (this._tableMappings != null)
			{
				return this.TableMappings.IndexOfDataSetTable(dataSetTable);
			}
			return -1;
		}

		/// <summary>Invoked when an error occurs during a Fill.</summary>
		/// <param name="value">A <see cref="T:System.Data.FillErrorEventArgs" /> object.</param>
		// Token: 0x06002577 RID: 9591 RVA: 0x000AAF1B File Offset: 0x000A911B
		protected virtual void OnFillError(FillErrorEventArgs value)
		{
			FillErrorEventHandler fillErrorEventHandler = (FillErrorEventHandler)base.Events[DataAdapter.s_eventFillError];
			if (fillErrorEventHandler == null)
			{
				return;
			}
			fillErrorEventHandler(this, value);
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000AAF40 File Offset: 0x000A9140
		private void OnFillErrorHandler(Exception e, DataTable dataTable, object[] dataValues)
		{
			FillErrorEventArgs fillErrorEventArgs = new FillErrorEventArgs(dataTable, dataValues);
			fillErrorEventArgs.Errors = e;
			this.OnFillError(fillErrorEventArgs);
			if (fillErrorEventArgs.Continue)
			{
				return;
			}
			if (fillErrorEventArgs.Errors != null)
			{
				throw fillErrorEventArgs.Errors;
			}
			throw e;
		}

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataSet" /> from a <see cref="T:System.Data.DataTable" /> named "Table."</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> used to update the data source. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002579 RID: 9593 RVA: 0x000621D6 File Offset: 0x000603D6
		public virtual int Update(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000AAF7C File Offset: 0x000A917C
		private static DataTable[] AddDataTableToArray(DataTable[] tables, DataTable newTable)
		{
			for (int i = 0; i < tables.Length; i++)
			{
				if (tables[i] == newTable)
				{
					return tables;
				}
			}
			DataTable[] array = new DataTable[tables.Length + 1];
			for (int j = 0; j < tables.Length; j++)
			{
				array[j] = tables[j];
			}
			array[tables.Length] = newTable;
			return array;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000AAFC5 File Offset: 0x000A91C5
		private static string GetSourceTableName(string srcTable, int index)
		{
			if (index == 0)
			{
				return srcTable;
			}
			return srcTable + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0400183B RID: 6203
		private static readonly object s_eventFillError = new object();

		// Token: 0x0400183C RID: 6204
		private bool _acceptChangesDuringUpdate = true;

		// Token: 0x0400183D RID: 6205
		private bool _acceptChangesDuringUpdateAfterInsert = true;

		// Token: 0x0400183E RID: 6206
		private bool _continueUpdateOnError;

		// Token: 0x0400183F RID: 6207
		private bool _hasFillErrorHandler;

		// Token: 0x04001840 RID: 6208
		private bool _returnProviderSpecificTypes;

		// Token: 0x04001841 RID: 6209
		private bool _acceptChangesDuringFill = true;

		// Token: 0x04001842 RID: 6210
		private LoadOption _fillLoadOption;

		// Token: 0x04001843 RID: 6211
		private MissingMappingAction _missingMappingAction = MissingMappingAction.Passthrough;

		// Token: 0x04001844 RID: 6212
		private MissingSchemaAction _missingSchemaAction = MissingSchemaAction.Add;

		// Token: 0x04001845 RID: 6213
		private DataTableMappingCollection _tableMappings;

		// Token: 0x04001846 RID: 6214
		private static int s_objectTypeCount;

		// Token: 0x04001847 RID: 6215
		internal readonly int _objectID = Interlocked.Increment(ref DataAdapter.s_objectTypeCount);
	}
}
