using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using Unity;

namespace System.Data
{
	/// <summary>Represents the collection of tables for the <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000091 RID: 145
	[DefaultEvent("CollectionChanged")]
	[ListBindable(false)]
	public sealed class DataTableCollection : InternalDataCollectionBase
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x00027B4C File Offset: 0x00025D4C
		internal DataTableCollection(DataSet dataSet)
		{
			this._list = new ArrayList();
			this._defaultNameIndex = 1;
			this._objectID = Interlocked.Increment(ref DataTableCollection.s_objectTypeCount);
			base..ctor();
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataTableCollection.DataTableCollection|INFO> {0}, dataSet={1}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0);
			this._dataSet = dataSet;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00027BA9 File Offset: 0x00025DA9
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00027BB1 File Offset: 0x00025DB1
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object at the specified index.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" />with the specified index; otherwise null if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000186 RID: 390
		public DataTable this[int index]
		{
			get
			{
				DataTable dataTable;
				try
				{
					dataTable = (DataTable)this._list[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw ExceptionBuilder.TableOutOfRange(index);
				}
				return dataTable;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object with the specified name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> with the specified name; otherwise null if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		/// <param name="name">The name of the DataTable to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000187 RID: 391
		public DataTable this[string name]
		{
			get
			{
				int num = this.InternalIndexOf(name);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num == -3)
				{
					throw ExceptionBuilder.NamespaceNameConflict(name);
				}
				if (num >= 0)
				{
					return (DataTable)this._list[num];
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object with the specified name in the specified namespace.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> with the specified name; otherwise null if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		/// <param name="name">The name of the DataTable to find.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000188 RID: 392
		public DataTable this[string name, string tableNamespace]
		{
			get
			{
				if (tableNamespace == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableNamespace");
				}
				int num = this.InternalIndexOf(name, tableNamespace);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num >= 0)
				{
					return (DataTable)this._list[num];
				}
				return null;
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00027C84 File Offset: 0x00025E84
		internal DataTable GetTable(string name, string ns)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (dataTable.TableName == name && dataTable.Namespace == ns)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00027CD8 File Offset: 0x00025ED8
		internal DataTable GetTableSmart(string name, string ns)
		{
			int num = 0;
			DataTable dataTable = null;
			for (int i = 0; i < this._list.Count; i++)
			{
				DataTable dataTable2 = (DataTable)this._list[i];
				if (dataTable2.TableName == name)
				{
					if (dataTable2.Namespace == ns)
					{
						return dataTable2;
					}
					num++;
					dataTable = dataTable2;
				}
			}
			if (num != 1)
			{
				return null;
			}
			return dataTable;
		}

		/// <summary>Adds the specified DataTable to the collection.</summary>
		/// <param name="table">The DataTable object to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The value specified for the table is null. </exception>
		/// <exception cref="T:System.ArgumentException">The table already belongs to this collection, or belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. The comparison is not case sensitive. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600089A RID: 2202 RVA: 0x00027D3C File Offset: 0x00025F3C
		public void Add(DataTable table)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTableCollection.Add|API> {0}, table={1}", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
				this.BaseAdd(table);
				this.ArrayAdd(table);
				if (table.SetLocaleValue(this._dataSet.Locale, false, false) || table.SetCaseSensitiveValue(this._dataSet.CaseSensitive, false, false))
				{
					table.ResetIndexes();
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataTable" /> array to the end of the collection.</summary>
		/// <param name="tables">The array of <see cref="T:System.Data.DataTable" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600089B RID: 2203 RVA: 0x00027DE4 File Offset: 0x00025FE4
		public void AddRange(DataTable[] tables)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTableCollection.AddRange|API> {0}", this.ObjectID);
			try
			{
				if (this._dataSet._fInitInProgress)
				{
					this._delayedAddRangeTables = tables;
				}
				else if (tables != null)
				{
					foreach (DataTable dataTable in tables)
					{
						if (dataTable != null)
						{
							this.Add(dataTable);
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object by using the specified name and adds it to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="name">The name to give the created <see cref="T:System.Data.DataTable" />. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. (The comparison is not case sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600089C RID: 2204 RVA: 0x00027E5C File Offset: 0x0002605C
		public DataTable Add(string name)
		{
			DataTable dataTable = new DataTable(name);
			this.Add(dataTable);
			return dataTable;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object by using the specified name and adds it to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="name">The name to give the created <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="tableNamespace">The namespace to give the created <see cref="T:System.Data.DataTable" />.</param>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. (The comparison is not case sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600089D RID: 2205 RVA: 0x00027E78 File Offset: 0x00026078
		public DataTable Add(string name, string tableNamespace)
		{
			DataTable dataTable = new DataTable(name, tableNamespace);
			this.Add(dataTable);
			return dataTable;
		}

		/// <summary>Creates a new <see cref="T:System.Data.DataTable" /> object by using a default name and adds it to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600089E RID: 2206 RVA: 0x00027E98 File Offset: 0x00026098
		public DataTable Add()
		{
			DataTable dataTable = new DataTable();
			this.Add(dataTable);
			return dataTable;
		}

		/// <summary>Occurs after the <see cref="T:System.Data.DataTableCollection" /> is changed because of <see cref="T:System.Data.DataTable" /> objects being added or removed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600089F RID: 2207 RVA: 0x00027EB3 File Offset: 0x000260B3
		// (remove) Token: 0x060008A0 RID: 2208 RVA: 0x00027EE1 File Offset: 0x000260E1
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.add_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.remove_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangedDelegate, value);
			}
		}

		/// <summary>Occurs while the <see cref="T:System.Data.DataTableCollection" /> is changing because of <see cref="T:System.Data.DataTable" /> objects being added or removed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060008A1 RID: 2209 RVA: 0x00027F0F File Offset: 0x0002610F
		// (remove) Token: 0x060008A2 RID: 2210 RVA: 0x00027F3D File Offset: 0x0002613D
		public event CollectionChangeEventHandler CollectionChanging
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.add_CollectionChanging|API> {0}", this.ObjectID);
				this._onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangingDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.remove_CollectionChanging|API> {0}", this.ObjectID);
				this._onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangingDelegate, value);
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00027F6B File Offset: 0x0002616B
		private void ArrayAdd(DataTable table)
		{
			this._list.Add(table);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00027F7C File Offset: 0x0002617C
		internal string AssignName()
		{
			string text;
			while (this.Contains(text = this.MakeName(this._defaultNameIndex)))
			{
				this._defaultNameIndex++;
			}
			return text;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00027FB4 File Offset: 0x000261B4
		private void BaseAdd(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			if (table.DataSet == this._dataSet)
			{
				throw ExceptionBuilder.TableAlreadyInTheDataSet();
			}
			if (table.DataSet != null)
			{
				throw ExceptionBuilder.TableAlreadyInOtherDataSet();
			}
			if (table.TableName.Length == 0)
			{
				table.TableName = this.AssignName();
			}
			else
			{
				if (base.NamesEqual(table.TableName, this._dataSet.DataSetName, false, this._dataSet.Locale) != 0 && !table._fNestedInDataset)
				{
					throw ExceptionBuilder.DatasetConflictingName(this._dataSet.DataSetName);
				}
				this.RegisterName(table.TableName, table.Namespace);
			}
			table.SetDataSet(this._dataSet);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00028068 File Offset: 0x00026268
		private void BaseGroupSwitch(DataTable[] oldArray, int oldLength, DataTable[] newArray, int newLength)
		{
			int num = 0;
			for (int i = 0; i < oldLength; i++)
			{
				bool flag = false;
				for (int j = num; j < newLength; j++)
				{
					if (oldArray[i] == newArray[j])
					{
						if (num == j)
						{
							num++;
						}
						flag = true;
						break;
					}
				}
				if (!flag && oldArray[i].DataSet == this._dataSet)
				{
					this.BaseRemove(oldArray[i]);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (newArray[k].DataSet != this._dataSet)
				{
					this.BaseAdd(newArray[k]);
					this._list.Add(newArray[k]);
				}
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000280FE File Offset: 0x000262FE
		private void BaseRemove(DataTable table)
		{
			if (this.CanRemove(table, true))
			{
				this.UnregisterName(table.TableName);
				table.SetDataSet(null);
			}
			this._list.Remove(table);
			this._dataSet.OnRemovedTable(table);
		}

		/// <summary>Verifies whether the specified <see cref="T:System.Data.DataTable" /> object can be removed from the collection.</summary>
		/// <returns>true if the table can be removed; otherwise false.</returns>
		/// <param name="table">The DataTable in the collection to perform the check against. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008A8 RID: 2216 RVA: 0x00028135 File Offset: 0x00026335
		public bool CanRemove(DataTable table)
		{
			return this.CanRemove(table, false);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00028140 File Offset: 0x00026340
		internal bool CanRemove(DataTable table, bool fThrowException)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool>("<ds.DataTableCollection.CanRemove|INFO> {0}, table={1}, fThrowException={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, fThrowException);
			bool flag;
			try
			{
				if (table == null)
				{
					if (fThrowException)
					{
						throw ExceptionBuilder.ArgumentNull("table");
					}
					flag = false;
				}
				else if (table.DataSet != this._dataSet)
				{
					if (fThrowException)
					{
						throw ExceptionBuilder.TableNotInTheDataSet(table.TableName);
					}
					flag = false;
				}
				else
				{
					this._dataSet.OnRemoveTable(table);
					if (table.ChildRelations.Count != 0 || table.ParentRelations.Count != 0)
					{
						if (fThrowException)
						{
							throw ExceptionBuilder.TableInRelation();
						}
						flag = false;
					}
					else
					{
						ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this._dataSet, table);
						while (parentForeignKeyConstraintEnumerator.GetNext())
						{
							ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
							if (foreignKeyConstraint.Table != table || foreignKeyConstraint.RelatedTable != table)
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.TableInConstraint(table, foreignKeyConstraint);
							}
						}
						ChildForeignKeyConstraintEnumerator childForeignKeyConstraintEnumerator = new ChildForeignKeyConstraintEnumerator(this._dataSet, table);
						while (childForeignKeyConstraintEnumerator.GetNext())
						{
							ForeignKeyConstraint foreignKeyConstraint2 = childForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
							if (foreignKeyConstraint2.Table != table || foreignKeyConstraint2.RelatedTable != table)
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.TableInConstraint(table, foreignKeyConstraint2);
							}
						}
						flag = true;
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		/// <summary>Clears the collection of all <see cref="T:System.Data.DataTable" /> objects.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008AA RID: 2218 RVA: 0x00028288 File Offset: 0x00026488
		public void Clear()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTableCollection.Clear|API> {0}", this.ObjectID);
			try
			{
				int count = this._list.Count;
				DataTable[] array = new DataTable[this._list.Count];
				this._list.CopyTo(array, 0);
				this.OnCollectionChanging(InternalDataCollectionBase.s_refreshEventArgs);
				if (this._dataSet._fInitInProgress && this._delayedAddRangeTables != null)
				{
					this._delayedAddRangeTables = null;
				}
				this.BaseGroupSwitch(array, count, null, 0);
				this._list.Clear();
				this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Data.DataTable" /> object with the specified name exists in the collection.</summary>
		/// <returns>true if the specified table exists; otherwise false.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008AB RID: 2219 RVA: 0x0002833C File Offset: 0x0002653C
		public bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Data.DataTable" /> object with the specified name and table namespace exists in the collection.</summary>
		/// <returns>true if the specified table exists; otherwise false.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> to find.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008AC RID: 2220 RVA: 0x0002834B File Offset: 0x0002654B
		public bool Contains(string name, string tableNamespace)
		{
			if (name == null)
			{
				throw ExceptionBuilder.ArgumentNull("name");
			}
			if (tableNamespace == null)
			{
				throw ExceptionBuilder.ArgumentNull("tableNamespace");
			}
			return this.InternalIndexOf(name, tableNamespace) >= 0;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00028378 File Offset: 0x00026578
		internal bool Contains(string name, string tableNamespace, bool checkProperty, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.InternalIndexOf(name) >= 0;
			}
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				string text = (checkProperty ? dataTable.Namespace : dataTable._tableNamespace);
				if (base.NamesEqual(dataTable.TableName, name, true, this._dataSet.Locale) == 1 && text == tableNamespace)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000283FC File Offset: 0x000265FC
		internal bool Contains(string name, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.InternalIndexOf(name) >= 0;
			}
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (base.NamesEqual(dataTable.TableName, name, true, this._dataSet.Locale) == 1)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.DataTableCollection" /> to a one-dimensional <see cref="T:System.Array" />, starting at the specified destination array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to copy the current <see cref="T:System.Data.DataTableCollection" /> object's elements into.</param>
		/// <param name="index">The destination <see cref="T:System.Array" /> index to start copying into.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008AF RID: 2223 RVA: 0x00028464 File Offset: 0x00026664
		public void CopyTo(DataTable[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			if (array.Length - index < this._list.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				array[index + i] = (DataTable)this._list[i];
			}
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>The zero-based index of the table, or -1 if the table is not found in the collection.</returns>
		/// <param name="table">The DataTable to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B0 RID: 2224 RVA: 0x000284D4 File Offset: 0x000266D4
		public int IndexOf(DataTable table)
		{
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				if (table == (DataTable)this._list[i])
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the index in the collection of the <see cref="T:System.Data.DataTable" /> object with the specified name.</summary>
		/// <returns>The zero-based index of the DataTable with the specified name, or -1 if the table does not exist in the collection.NoteReturns -1 when two or more tables have the same name but different namespaces. The call does not succeed if there is any ambiguity when matching a table name to exactly one table.</returns>
		/// <param name="tableName">The name of the DataTable object to look for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B1 RID: 2225 RVA: 0x00028510 File Offset: 0x00026710
		public int IndexOf(string tableName)
		{
			int num = this.InternalIndexOf(tableName);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Data.DataTable" /> with the specified name, or -1 if the table does not exist in the collection.</returns>
		/// <param name="tableName">The name of the <see cref="T:System.Data.DataTable" /> object to look for.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008B2 RID: 2226 RVA: 0x0002852C File Offset: 0x0002672C
		public int IndexOf(string tableName, string tableNamespace)
		{
			return this.IndexOf(tableName, tableNamespace, true);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00028538 File Offset: 0x00026738
		internal int IndexOf(string tableName, string tableNamespace, bool chekforNull)
		{
			if (chekforNull)
			{
				if (tableName == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableName");
				}
				if (tableNamespace == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableNamespace");
				}
			}
			int num = this.InternalIndexOf(tableName, tableNamespace);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00028574 File Offset: 0x00026774
		internal void ReplaceFromInference(List<DataTable> tableList)
		{
			this._list.Clear();
			this._list.AddRange(tableList);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00028590 File Offset: 0x00026790
		internal int InternalIndexOf(string tableName)
		{
			int num = -1;
			if (tableName != null && 0 < tableName.Length)
			{
				int count = this._list.Count;
				for (int i = 0; i < count; i++)
				{
					DataTable dataTable = (DataTable)this._list[i];
					int num2 = base.NamesEqual(dataTable.TableName, tableName, false, this._dataSet.Locale);
					if (num2 == 1)
					{
						for (int j = i + 1; j < count; j++)
						{
							DataTable dataTable2 = (DataTable)this._list[j];
							if (base.NamesEqual(dataTable2.TableName, tableName, false, this._dataSet.Locale) == 1)
							{
								return -3;
							}
						}
						return i;
					}
					if (num2 == -1)
					{
						num = ((num == -1) ? i : (-2));
					}
				}
			}
			return num;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002865C File Offset: 0x0002685C
		internal int InternalIndexOf(string tableName, string tableNamespace)
		{
			int num = -1;
			if (tableName != null && 0 < tableName.Length)
			{
				int count = this._list.Count;
				for (int i = 0; i < count; i++)
				{
					DataTable dataTable = (DataTable)this._list[i];
					int num2 = base.NamesEqual(dataTable.TableName, tableName, false, this._dataSet.Locale);
					if (num2 == 1 && dataTable.Namespace == tableNamespace)
					{
						return i;
					}
					if (num2 == -1 && dataTable.Namespace == tableNamespace)
					{
						num = ((num == -1) ? i : (-2));
					}
				}
			}
			return num;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000286F8 File Offset: 0x000268F8
		internal void FinishInitCollection()
		{
			if (this._delayedAddRangeTables != null)
			{
				foreach (DataTable dataTable in this._delayedAddRangeTables)
				{
					if (dataTable != null)
					{
						this.Add(dataTable);
					}
				}
				this._delayedAddRangeTables = null;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00028737 File Offset: 0x00026937
		private string MakeName(int index)
		{
			if (1 != index)
			{
				return "Table" + index.ToString(CultureInfo.InvariantCulture);
			}
			return "Table1";
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00028759 File Offset: 0x00026959
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.OnCollectionChanged|INFO> {0}", this.ObjectID);
				this._onCollectionChangedDelegate(this, ccevent);
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00028785 File Offset: 0x00026985
		private void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.OnCollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate(this, ccevent);
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000287B4 File Offset: 0x000269B4
		internal void RegisterName(string name, string tbNamespace)
		{
			DataCommonEventSource.Log.Trace<int, string, string>("<ds.DataTableCollection.RegisterName|INFO> {0}, name='{1}', tbNamespace='{2}'", this.ObjectID, name, tbNamespace);
			CultureInfo locale = this._dataSet.Locale;
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (base.NamesEqual(name, dataTable.TableName, true, locale) != 0 && tbNamespace == dataTable.Namespace)
				{
					throw ExceptionBuilder.DuplicateTableName(((DataTable)this._list[i]).TableName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, locale) != 0)
			{
				this._defaultNameIndex++;
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataTable" /> object from the collection.</summary>
		/// <param name="table">The DataTable to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The value specified for the table is null. </exception>
		/// <exception cref="T:System.ArgumentException">The table does not belong to this collection.-or- The table is part of a relationship. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008BC RID: 2236 RVA: 0x00028870 File Offset: 0x00026A70
		public void Remove(DataTable table)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTableCollection.Remove|API> {0}, table={1}", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
				this.BaseRemove(table);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.DataTable" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the DataTable to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a table at the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008BD RID: 2237 RVA: 0x000288E0 File Offset: 0x00026AE0
		public void RemoveAt(int index)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTableCollection.RemoveAt|API> {0}, index={1}", this.ObjectID, index);
			try
			{
				DataTable dataTable = this[index];
				if (dataTable == null)
				{
					throw ExceptionBuilder.TableOutOfRange(index);
				}
				this.Remove(dataTable);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.DataTable" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> object to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a table with the specified name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008BE RID: 2238 RVA: 0x0002893C File Offset: 0x00026B3C
		public void Remove(string name)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataTableCollection.Remove|API> {0}, name='{1}'", this.ObjectID, name);
			try
			{
				DataTable dataTable = this[name];
				if (dataTable == null)
				{
					throw ExceptionBuilder.TableNotInTheDataSet(name);
				}
				this.Remove(dataTable);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.DataTable" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> object to remove.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a table with the specified name. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008BF RID: 2239 RVA: 0x00028998 File Offset: 0x00026B98
		public void Remove(string name, string tableNamespace)
		{
			if (name == null)
			{
				throw ExceptionBuilder.ArgumentNull("name");
			}
			if (tableNamespace == null)
			{
				throw ExceptionBuilder.ArgumentNull("tableNamespace");
			}
			DataTable dataTable = this[name, tableNamespace];
			if (dataTable == null)
			{
				throw ExceptionBuilder.TableNotInTheDataSet(name);
			}
			this.Remove(dataTable);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000289DC File Offset: 0x00026BDC
		internal void UnregisterName(string name)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataTableCollection.UnregisterName|INFO> {0}, name='{1}'", this.ObjectID, name);
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this._dataSet.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00010468 File Offset: 0x0000E668
		internal DataTableCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000629 RID: 1577
		private readonly DataSet _dataSet;

		// Token: 0x0400062A RID: 1578
		private readonly ArrayList _list;

		// Token: 0x0400062B RID: 1579
		private int _defaultNameIndex;

		// Token: 0x0400062C RID: 1580
		private DataTable[] _delayedAddRangeTables;

		// Token: 0x0400062D RID: 1581
		private CollectionChangeEventHandler _onCollectionChangedDelegate;

		// Token: 0x0400062E RID: 1582
		private CollectionChangeEventHandler _onCollectionChangingDelegate;

		// Token: 0x0400062F RID: 1583
		private static int s_objectTypeCount;

		// Token: 0x04000630 RID: 1584
		private readonly int _objectID;
	}
}
