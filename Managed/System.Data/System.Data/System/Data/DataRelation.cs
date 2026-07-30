using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents a parent/child relationship between two <see cref="T:System.Data.DataTable" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000076 RID: 118
	[TypeConverter(typeof(RelationshipConverter))]
	[DefaultProperty("RelationName")]
	public class DataRelation
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified <see cref="T:System.Data.DataRelation" /> name, and parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="relationName">The name of the <see cref="T:System.Data.DataRelation" />. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the relationship. </param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the relationship. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types -Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x0600056B RID: 1387 RVA: 0x00015D92 File Offset: 0x00013F92
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn)
			: this(relationName, parentColumn, childColumn, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified name, parent and child <see cref="T:System.Data.DataColumn" /> objects, and a value that indicates whether to create constraints.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the relation. </param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the relation. </param>
		/// <param name="createConstraints">A value that indicates whether constraints are created. true, if constraints are created. Otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types -Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x0600056C RID: 1388 RVA: 0x00015DA0 File Offset: 0x00013FA0
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
		{
			this._relationName = string.Empty;
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation.s_objectTypeCount);
			base..ctor();
			DataCommonEventSource.Log.Trace<int, string, int, int, bool>("<ds.DataRelation.DataRelation|API> {0}, relationName='{1}', parentColumn={2}, childColumn={3}, createConstraints={4}", this.ObjectID, relationName, (parentColumn != null) ? parentColumn.ObjectID : 0, (childColumn != null) ? childColumn.ObjectID : 0, createConstraints);
			this.Create(relationName, new DataColumn[] { parentColumn }, new DataColumn[] { childColumn }, createConstraints);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified <see cref="T:System.Data.DataRelation" /> name and matched arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The <see cref="T:System.Data.DataColumn" /> objects have different data types -Or- One or both of the arrays are not composed of distinct columns from the same table.-Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x0600056D RID: 1389 RVA: 0x00015E26 File Offset: 0x00014026
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns)
			: this(relationName, parentColumns, childColumns, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified name, matched arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects, and value that indicates whether to create constraints.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="createConstraints">A value that indicates whether to create constraints. true, if constraints are created. Otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types -Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x0600056E RID: 1390 RVA: 0x00015E32 File Offset: 0x00014032
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			this._relationName = string.Empty;
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation.s_objectTypeCount);
			base..ctor();
			this.Create(relationName, parentColumns, childColumns, createConstraints);
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio environment.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the parent table of the relation. </param>
		/// <param name="childTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the child table of the relation. </param>
		/// <param name="parentColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the parent <see cref="T:System.Data.DataTable" /> of the relation. </param>
		/// <param name="childColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the child <see cref="T:System.Data.DataTable" /> of the relation. </param>
		/// <param name="nested">A value that indicates whether relationships are nested. </param>
		// Token: 0x0600056F RID: 1391 RVA: 0x00015E68 File Offset: 0x00014068
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string childTableName, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this._relationName = string.Empty;
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation.s_objectTypeCount);
			base..ctor();
			this._relationName = relationName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._childTableName = childTableName;
			this._nested = nested;
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio environment.</summary>
		/// <param name="relationName">The name of the <see cref="T:System.Data.DataRelation" />. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the parent table of the relation.</param>
		/// <param name="parentTableNamespace">The name of the parent table namespace.</param>
		/// <param name="childTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the child table of the relation. </param>
		/// <param name="childTableNamespace">The name of the child table namespace.</param>
		/// <param name="parentColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the parent <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="childColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the child <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="nested">A value that indicates whether relationships are nested.</param>
		// Token: 0x06000570 RID: 1392 RVA: 0x00015ECC File Offset: 0x000140CC
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string parentTableNamespace, string childTableName, string childTableNamespace, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this._relationName = string.Empty;
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation.s_objectTypeCount);
			base..ctor();
			this._relationName = relationName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._childTableName = childTableName;
			this._parentTableNamespace = parentTableNamespace;
			this._childTableNamespace = childTableNamespace;
			this._nested = nested;
		}

		/// <summary>Gets the child <see cref="T:System.Data.DataColumn" /> objects of this relation.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00015F3E File Offset: 0x0001413E
		public virtual DataColumn[] ChildColumns
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey.ToArray();
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00015F51 File Offset: 0x00014151
		internal DataColumn[] ChildColumnsReference
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey.ColumnsReference;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00015F64 File Offset: 0x00014164
		internal DataKey ChildKey
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey;
			}
		}

		/// <summary>Gets the child table of this relation.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the child table of the relation.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00015F72 File Offset: 0x00014172
		public virtual DataTable ChildTable
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey.Table;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which the <see cref="T:System.Data.DataRelation" /> belongs.</summary>
		/// <returns>A <see cref="T:System.Data.DataSet" /> to which the <see cref="T:System.Data.DataRelation" /> belongs.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00015F85 File Offset: 0x00014185
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual DataSet DataSet
		{
			get
			{
				this.CheckStateForProperty();
				return this._dataSet;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00015F93 File Offset: 0x00014193
		internal string[] ParentColumnNames
		{
			get
			{
				return this._parentKey.GetColumnNames();
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00015FA0 File Offset: 0x000141A0
		internal string[] ChildColumnNames
		{
			get
			{
				return this._childKey.GetColumnNames();
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00015FB0 File Offset: 0x000141B0
		private static bool IsKeyNull(object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (!DataStorage.IsObjectNull(values[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00015FD8 File Offset: 0x000141D8
		internal static DataRow[] GetChildRows(DataKey parentKey, DataKey childKey, DataRow parentRow, DataRowVersion version)
		{
			object[] keyValues = parentRow.GetKeyValues(parentKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return childKey.Table.NewRowArray(0);
			}
			return childKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows).GetRows(keyValues);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00016020 File Offset: 0x00014220
		internal static DataRow[] GetParentRows(DataKey parentKey, DataKey childKey, DataRow childRow, DataRowVersion version)
		{
			object[] keyValues = childRow.GetKeyValues(childKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return parentKey.Table.NewRowArray(0);
			}
			return parentKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows).GetRows(keyValues);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00016068 File Offset: 0x00014268
		internal static DataRow GetParentRow(DataKey parentKey, DataKey childKey, DataRow childRow, DataRowVersion version)
		{
			if (!childRow.HasVersion((version == DataRowVersion.Original) ? DataRowVersion.Original : DataRowVersion.Current) && childRow._tempRecord == -1)
			{
				return null;
			}
			object[] keyValues = childRow.GetKeyValues(childKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return null;
			}
			Index sortIndex = parentKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			Range range = sortIndex.FindRecords(keyValues);
			if (range.IsNull)
			{
				return null;
			}
			if (range.Count > 1)
			{
				throw ExceptionBuilder.MultipleParents();
			}
			return parentKey.Table._recordManager[sortIndex.GetRecord(range.Min)];
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00016106 File Offset: 0x00014306
		internal void SetDataSet(DataSet dataSet)
		{
			if (this._dataSet != dataSet)
			{
				this._dataSet = dataSet;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00016118 File Offset: 0x00014318
		internal void SetParentRowRecords(DataRow childRow, DataRow parentRow)
		{
			object[] keyValues = parentRow.GetKeyValues(this.ParentKey);
			if (childRow._tempRecord != -1)
			{
				this.ChildTable._recordManager.SetKeyValues(childRow._tempRecord, this.ChildKey, keyValues);
			}
			if (childRow._newRecord != -1)
			{
				this.ChildTable._recordManager.SetKeyValues(childRow._newRecord, this.ChildKey, keyValues);
			}
			if (childRow._oldRecord != -1)
			{
				this.ChildTable._recordManager.SetKeyValues(childRow._oldRecord, this.ChildKey, keyValues);
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of this <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of this <see cref="T:System.Data.DataRelation" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x000161A4 File Offset: 0x000143A4
		public virtual DataColumn[] ParentColumns
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKey.ToArray();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000161B7 File Offset: 0x000143B7
		internal DataColumn[] ParentColumnsReference
		{
			get
			{
				return this._parentKey.ColumnsReference;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x000161C4 File Offset: 0x000143C4
		internal DataKey ParentKey
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKey;
			}
		}

		/// <summary>Gets the parent <see cref="T:System.Data.DataTable" /> of this <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the parent table of this relation.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000161D2 File Offset: 0x000143D2
		public virtual DataTable ParentTable
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKey.Table;
			}
		}

		/// <summary>Gets or sets the name used to retrieve a <see cref="T:System.Data.DataRelation" /> from the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <returns>The name of the a <see cref="T:System.Data.DataRelation" />.</returns>
		/// <exception cref="T:System.ArgumentException">null or empty string ("") was passed into a <see cref="T:System.Data.DataColumn" /> that is a <see cref="T:System.Data.DataRelation" />. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The <see cref="T:System.Data.DataRelation" /> belongs to a collection that already contains a <see cref="T:System.Data.DataRelation" /> with the same name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000161E5 File Offset: 0x000143E5
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x000161F4 File Offset: 0x000143F4
		[DefaultValue("")]
		public virtual string RelationName
		{
			get
			{
				this.CheckStateForProperty();
				return this._relationName;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataRelation.set_RelationName|API> {0}, '{1}'", this.ObjectID, value);
				try
				{
					if (value == null)
					{
						value = string.Empty;
					}
					CultureInfo cultureInfo = ((this._dataSet != null) ? this._dataSet.Locale : CultureInfo.CurrentCulture);
					if (string.Compare(this._relationName, value, true, cultureInfo) != 0)
					{
						if (this._dataSet != null)
						{
							if (value.Length == 0)
							{
								throw ExceptionBuilder.NoRelationName();
							}
							this._dataSet.Relations.RegisterName(value);
							if (this._relationName.Length != 0)
							{
								this._dataSet.Relations.UnregisterName(this._relationName);
							}
						}
						this._relationName = value;
						((DataRelationCollection.DataTableRelationCollection)this.ParentTable.ChildRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						((DataRelationCollection.DataTableRelationCollection)this.ChildTable.ParentRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
					}
					else if (string.Compare(this._relationName, value, false, cultureInfo) != 0)
					{
						this._relationName = value;
						((DataRelationCollection.DataTableRelationCollection)this.ParentTable.ChildRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						((DataRelationCollection.DataTableRelationCollection)this.ChildTable.ParentRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00016354 File Offset: 0x00014554
		internal void CheckNamespaceValidityForNestedRelations(string ns)
		{
			foreach (object obj in this.ChildTable.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if ((dataRelation == this || dataRelation.Nested) && dataRelation.ParentTable.Namespace != ns)
				{
					throw ExceptionBuilder.InValidNestedRelation(this.ChildTable.TableName);
				}
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000163DC File Offset: 0x000145DC
		internal void CheckNestedRelations()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.DataRelation.CheckNestedRelations|INFO> {0}", this.ObjectID);
			DataTable parentTable = this.ParentTable;
			if (this.ChildTable != this.ParentTable)
			{
				List<DataTable> list = new List<DataTable>();
				list.Add(this.ChildTable);
				for (int i = 0; i < list.Count; i++)
				{
					foreach (DataRelation dataRelation in list[i].NestedParentRelations)
					{
						if (dataRelation.ParentTable == this.ChildTable && dataRelation.ChildTable != this.ChildTable)
						{
							throw ExceptionBuilder.LoopInNestedRelations(this.ChildTable.TableName);
						}
						if (!list.Contains(dataRelation.ParentTable))
						{
							list.Add(dataRelation.ParentTable);
						}
					}
				}
				return;
			}
			if (string.Compare(this.ChildTable.TableName, this.ChildTable.DataSet.DataSetName, true, this.ChildTable.DataSet.Locale) == 0)
			{
				throw ExceptionBuilder.SelfnestedDatasetConflictingName(this.ChildTable.TableName);
			}
		}

		/// <summary>Gets or sets a value that indicates whether <see cref="T:System.Data.DataRelation" /> objects are nested.</summary>
		/// <returns>true, if <see cref="T:System.Data.DataRelation" /> objects are nested; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000164E6 File Offset: 0x000146E6
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x000164F4 File Offset: 0x000146F4
		[DefaultValue(false)]
		public virtual bool Nested
		{
			get
			{
				this.CheckStateForProperty();
				return this._nested;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataRelation.set_Nested|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._nested != value)
					{
						if (this._dataSet != null && value)
						{
							if (this.ChildTable.IsNamespaceInherited())
							{
								this.CheckNamespaceValidityForNestedRelations(this.ParentTable.Namespace);
							}
							ForeignKeyConstraint foreignKeyConstraint = this.ChildTable.Constraints.FindForeignKeyConstraint(this.ChildKey.ColumnsReference, this.ParentKey.ColumnsReference);
							if (foreignKeyConstraint != null)
							{
								foreignKeyConstraint.CheckConstraint();
							}
							this.ValidateMultipleNestedRelations();
						}
						if (!value && this._parentKey.ColumnsReference[0].ColumnMapping == MappingType.Hidden)
						{
							throw ExceptionBuilder.RelationNestedReadOnly();
						}
						if (value)
						{
							this.ParentTable.Columns.RegisterColumnName(this.ChildTable.TableName, null);
						}
						else
						{
							this.ParentTable.Columns.UnregisterName(this.ChildTable.TableName);
						}
						this.RaisePropertyChanging("Nested");
						if (value)
						{
							this.CheckNestedRelations();
							if (this.DataSet != null)
							{
								if (this.ParentTable == this.ChildTable)
								{
									foreach (object obj in this.ChildTable.Rows)
									{
										((DataRow)obj).CheckForLoops(this);
									}
									if (this.ChildTable.DataSet != null && string.Compare(this.ChildTable.TableName, this.ChildTable.DataSet.DataSetName, true, this.ChildTable.DataSet.Locale) == 0)
									{
										throw ExceptionBuilder.DatasetConflictingName(this._dataSet.DataSetName);
									}
									this.ChildTable._fNestedInDataset = false;
								}
								else
								{
									foreach (object obj2 in this.ChildTable.Rows)
									{
										((DataRow)obj2).GetParentRow(this);
									}
								}
							}
							DataTable parentTable = this.ParentTable;
							int num2 = parentTable.ElementColumnCount;
							parentTable.ElementColumnCount = num2 + 1;
						}
						else
						{
							DataTable parentTable2 = this.ParentTable;
							int num2 = parentTable2.ElementColumnCount;
							parentTable2.ElementColumnCount = num2 - 1;
						}
						this._nested = value;
						this.ChildTable.CacheNestedParent();
						if (value && string.IsNullOrEmpty(this.ChildTable.Namespace) && (this.ChildTable.NestedParentsCount > 1 || (this.ChildTable.NestedParentsCount > 0 && !this.ChildTable.DataSet.Relations.Contains(this.RelationName))))
						{
							string text = null;
							foreach (object obj3 in this.ChildTable.ParentRelations)
							{
								DataRelation dataRelation = (DataRelation)obj3;
								if (dataRelation.Nested)
								{
									if (text == null)
									{
										text = dataRelation.ParentTable.Namespace;
									}
									else if (string.Compare(text, dataRelation.ParentTable.Namespace, StringComparison.Ordinal) != 0)
									{
										this._nested = false;
										throw ExceptionBuilder.InvalidParentNamespaceinNestedRelation(this.ChildTable.TableName);
									}
								}
							}
							if (this.CheckMultipleNested && this.ChildTable._tableNamespace != null && this.ChildTable._tableNamespace.Length == 0)
							{
								throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
							}
							this.ChildTable._tableNamespace = null;
						}
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.UniqueConstraint" /> that guarantees that values in the parent column of a <see cref="T:System.Data.DataRelation" /> are unique.</summary>
		/// <returns>A <see cref="T:System.Data.UniqueConstraint" /> that makes sure that values in a parent column are unique.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000168D8 File Offset: 0x00014AD8
		public virtual UniqueConstraint ParentKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKeyConstraint;
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000168E6 File Offset: 0x00014AE6
		internal void SetParentKeyConstraint(UniqueConstraint value)
		{
			this._parentKeyConstraint = value;
		}

		/// <summary>Gets the <see cref="T:System.Data.ForeignKeyConstraint" /> for the relation.</summary>
		/// <returns>A ForeignKeyConstraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x000168EF File Offset: 0x00014AEF
		public virtual ForeignKeyConstraint ChildKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKeyConstraint;
			}
		}

		/// <summary>Gets the collection that stores customized properties.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> that contains customized properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00016900 File Offset: 0x00014B00
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00016925 File Offset: 0x00014B25
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0001692D File Offset: 0x00014B2D
		internal bool CheckMultipleNested
		{
			get
			{
				return this._checkMultipleNested;
			}
			set
			{
				this._checkMultipleNested = value;
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00016936 File Offset: 0x00014B36
		internal void SetChildKeyConstraint(ForeignKeyConstraint value)
		{
			this._childKeyConstraint = value;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600058F RID: 1423 RVA: 0x00016940 File Offset: 0x00014B40
		// (remove) Token: 0x06000590 RID: 1424 RVA: 0x00016978 File Offset: 0x00014B78
		internal event PropertyChangedEventHandler PropertyChanging;

		// Token: 0x06000591 RID: 1425 RVA: 0x000169B0 File Offset: 0x00014BB0
		internal void CheckState()
		{
			if (this._dataSet == null)
			{
				this._parentKey.CheckState();
				this._childKey.CheckState();
				if (this._parentKey.Table.DataSet != this._childKey.Table.DataSet)
				{
					throw ExceptionBuilder.RelationDataSetMismatch();
				}
				if (this._childKey.ColumnsEqual(this._parentKey))
				{
					throw ExceptionBuilder.KeyColumnsIdentical();
				}
				for (int i = 0; i < this._parentKey.ColumnsReference.Length; i++)
				{
					if (this._parentKey.ColumnsReference[i].DataType != this._childKey.ColumnsReference[i].DataType || (this._parentKey.ColumnsReference[i].DataType == typeof(DateTime) && this._parentKey.ColumnsReference[i].DateTimeMode != this._childKey.ColumnsReference[i].DateTimeMode && (this._parentKey.ColumnsReference[i].DateTimeMode & this._childKey.ColumnsReference[i].DateTimeMode) != DataSetDateTime.Unspecified))
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
				}
			}
		}

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <exception cref="T:System.Data.DataException">The parent and child tables belong to different <see cref="T:System.Data.DataSet" /> objects.-Or- One or more pairs of parent and child <see cref="T:System.Data.DataColumn" /> objects have mismatched data types.-Or- The parent and child <see cref="T:System.Data.DataColumn" /> objects are identical. </exception>
		// Token: 0x06000592 RID: 1426 RVA: 0x00016AE0 File Offset: 0x00014CE0
		protected void CheckStateForProperty()
		{
			try
			{
				this.CheckState();
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				throw ExceptionBuilder.BadObjectPropertyAccess(ex.Message);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00016B2C File Offset: 0x00014D2C
		private void Create(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string, bool>("<ds.DataRelation.Create|INFO> {0}, relationName='{1}', createConstraints={2}", this.ObjectID, relationName, createConstraints);
			try
			{
				this._parentKey = new DataKey(parentColumns, true);
				this._childKey = new DataKey(childColumns, true);
				if (parentColumns.Length != childColumns.Length)
				{
					throw ExceptionBuilder.KeyLengthMismatch();
				}
				for (int i = 0; i < parentColumns.Length; i++)
				{
					if (parentColumns[i].Table.DataSet == null || childColumns[i].Table.DataSet == null)
					{
						throw ExceptionBuilder.ParentOrChildColumnsDoNotHaveDataSet();
					}
				}
				this.CheckState();
				this._relationName = ((relationName == null) ? "" : relationName);
				this._createConstraints = createConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00016BEC File Offset: 0x00014DEC
		internal DataRelation Clone(DataSet destination)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelation.Clone|INFO> {0}, destination={1}", this.ObjectID, (destination != null) ? destination.ObjectID : 0);
			DataTable dataTable = destination.Tables[this.ParentTable.TableName, this.ParentTable.Namespace];
			DataTable dataTable2 = destination.Tables[this.ChildTable.TableName, this.ChildTable.Namespace];
			int num = this._parentKey.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num];
			DataColumn[] array2 = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = dataTable.Columns[this.ParentKey.ColumnsReference[i].ColumnName];
				array2[i] = dataTable2.Columns[this.ChildKey.ColumnsReference[i].ColumnName];
			}
			DataRelation dataRelation = new DataRelation(this._relationName, array, array2, false);
			dataRelation.CheckMultipleNested = false;
			dataRelation.Nested = this.Nested;
			dataRelation.CheckMultipleNested = true;
			if (this._extendedProperties != null)
			{
				foreach (object obj in this._extendedProperties.Keys)
				{
					dataRelation.ExtendedProperties[obj] = this._extendedProperties[obj];
				}
			}
			return dataRelation;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="pcevent">Parameter reference.</param>
		// Token: 0x06000595 RID: 1429 RVA: 0x00016D78 File Offset: 0x00014F78
		protected internal void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.PropertyChanging != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelation.OnPropertyChanging|INFO> {0}", this.ObjectID);
				this.PropertyChanging(this, pcevent);
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="name">Parameter reference.</param>
		// Token: 0x06000596 RID: 1430 RVA: 0x00016DA4 File Offset: 0x00014FA4
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		/// <summary>Gets the <see cref="P:System.Data.DataRelation.RelationName" />, if one exists.</summary>
		/// <returns>The value of the <see cref="P:System.Data.DataRelation.RelationName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000597 RID: 1431 RVA: 0x00016DB2 File Offset: 0x00014FB2
		public override string ToString()
		{
			return this.RelationName;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00016DBC File Offset: 0x00014FBC
		internal void ValidateMultipleNestedRelations()
		{
			if (!this.Nested || !this.CheckMultipleNested)
			{
				return;
			}
			if (this.ChildTable.NestedParentRelations.Length != 0)
			{
				DataColumn[] childColumns = this.ChildColumns;
				if (childColumns.Length != 1 || !this.IsAutoGenerated(childColumns[0]))
				{
					throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
				}
				if (!XmlTreeGen.AutoGenerated(this))
				{
					throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
				}
				foreach (object obj in this.ChildTable.Constraints)
				{
					Constraint constraint = (Constraint)obj;
					if (constraint is ForeignKeyConstraint)
					{
						if (!XmlTreeGen.AutoGenerated((ForeignKeyConstraint)constraint, true))
						{
							throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
						}
					}
					else if (!XmlTreeGen.AutoGenerated((UniqueConstraint)constraint))
					{
						throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
					}
				}
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00016EBC File Offset: 0x000150BC
		private bool IsAutoGenerated(DataColumn col)
		{
			if (col.ColumnMapping != MappingType.Hidden)
			{
				return false;
			}
			if (col.DataType != typeof(int))
			{
				return false;
			}
			string text = col.Table.TableName + "_Id";
			if (col.ColumnName == text || col.ColumnName == text + "_0")
			{
				return true;
			}
			text = this.ParentColumnsReference[0].Table.TableName + "_Id";
			return col.ColumnName == text || col.ColumnName == text + "_0";
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00016F71 File Offset: 0x00015171
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x04000558 RID: 1368
		private DataSet _dataSet;

		// Token: 0x04000559 RID: 1369
		internal PropertyCollection _extendedProperties;

		// Token: 0x0400055A RID: 1370
		internal string _relationName;

		// Token: 0x0400055B RID: 1371
		private DataKey _childKey;

		// Token: 0x0400055C RID: 1372
		private DataKey _parentKey;

		// Token: 0x0400055D RID: 1373
		private UniqueConstraint _parentKeyConstraint;

		// Token: 0x0400055E RID: 1374
		private ForeignKeyConstraint _childKeyConstraint;

		// Token: 0x0400055F RID: 1375
		internal string[] _parentColumnNames;

		// Token: 0x04000560 RID: 1376
		internal string[] _childColumnNames;

		// Token: 0x04000561 RID: 1377
		internal string _parentTableName;

		// Token: 0x04000562 RID: 1378
		internal string _childTableName;

		// Token: 0x04000563 RID: 1379
		internal string _parentTableNamespace;

		// Token: 0x04000564 RID: 1380
		internal string _childTableNamespace;

		// Token: 0x04000565 RID: 1381
		internal bool _nested;

		// Token: 0x04000566 RID: 1382
		internal bool _createConstraints;

		// Token: 0x04000567 RID: 1383
		private bool _checkMultipleNested;

		// Token: 0x04000568 RID: 1384
		private static int s_objectTypeCount;

		// Token: 0x04000569 RID: 1385
		private readonly int _objectID;
	}
}
