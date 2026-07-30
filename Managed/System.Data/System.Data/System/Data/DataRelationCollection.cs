using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents the collection of <see cref="T:System.Data.DataRelation" /> objects for this <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000077 RID: 119
	[DefaultProperty("Table")]
	[DefaultEvent("CollectionChanged")]
	public abstract class DataRelationCollection : InternalDataCollectionBase
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00016F79 File Offset: 0x00015179
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRelation" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.DataRelation" />, or a null value if the specified <see cref="T:System.Data.DataRelation" /> does not exist.</returns>
		/// <param name="index">The zero-based index to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000113 RID: 275
		public abstract DataRelation this[int index] { get; }

		/// <summary>Gets the <see cref="T:System.Data.DataRelation" /> object specified by name.</summary>
		/// <returns>The named <see cref="T:System.Data.DataRelation" />, or a null value if the specified <see cref="T:System.Data.DataRelation" /> does not exist.</returns>
		/// <param name="name">The name of the relation to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000114 RID: 276
		public abstract DataRelation this[string name] { get; }

		/// <summary>Adds a <see cref="T:System.Data.DataRelation" /> to the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <param name="relation">The DataRelation to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> parameter is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the specified name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600059E RID: 1438 RVA: 0x00016F84 File Offset: 0x00015184
		public void Add(DataRelation relation)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataRelationCollection.Add|API> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			try
			{
				if (this._inTransition != relation)
				{
					this._inTransition = relation;
					try
					{
						this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, relation));
						this.AddCore(relation);
						this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, relation));
					}
					finally
					{
						this._inTransition = null;
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataRelation" /> array to the end of the collection.</summary>
		/// <param name="relations">The array of <see cref="T:System.Data.DataRelation" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600059F RID: 1439 RVA: 0x00017018 File Offset: 0x00015218
		public virtual void AddRange(DataRelation[] relations)
		{
			if (relations != null)
			{
				foreach (DataRelation dataRelation in relations)
				{
					if (dataRelation != null)
					{
						this.Add(dataRelation);
					}
				}
			}
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name and arrays of parent and child columns, and adds it to the collection.</summary>
		/// <returns>The created DataRelation.</returns>
		/// <param name="name">The name of the DataRelation to create. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child DataColumn objects. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation name is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005A0 RID: 1440 RVA: 0x00017048 File Offset: 0x00015248
		public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumns, childColumns);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name, arrays of parent and child columns, and value specifying whether to create a constraint, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="name">The name of the DataRelation to create. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child DataColumn objects. </param>
		/// <param name="createConstraints">true to create a constraint; otherwise false. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation name is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005A1 RID: 1441 RVA: 0x00017068 File Offset: 0x00015268
		public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumns, childColumns, createConstraints);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified parent and child columns, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="parentColumns">The parent columns of the relation. </param>
		/// <param name="childColumns">The child columns of the relation. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> argument is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005A2 RID: 1442 RVA: 0x00017088 File Offset: 0x00015288
		public virtual DataRelation Add(DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			DataRelation dataRelation = new DataRelation(null, parentColumns, childColumns);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name, and parent and child columns, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="name">The name of the relation. </param>
		/// <param name="parentColumn">The parent column of the relation. </param>
		/// <param name="childColumn">The child column of the relation. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005A3 RID: 1443 RVA: 0x000170A8 File Offset: 0x000152A8
		public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumn, childColumn);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name, parent and child columns, with optional constraints according to the value of the <paramref name="createConstraints" /> parameter, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="name">The name of the relation. </param>
		/// <param name="parentColumn">The parent column of the relation. </param>
		/// <param name="childColumn">The child column of the relation. </param>
		/// <param name="createConstraints">true to create constraints; otherwise false. (The default is true). </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005A4 RID: 1444 RVA: 0x000170C8 File Offset: 0x000152C8
		public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumn, childColumn, createConstraints);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with a specified parent and child column, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="parentColumn">The parent column of the relation. </param>
		/// <param name="childColumn">The child column of the relation. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005A5 RID: 1445 RVA: 0x000170E8 File Offset: 0x000152E8
		public virtual DataRelation Add(DataColumn parentColumn, DataColumn childColumn)
		{
			DataRelation dataRelation = new DataRelation(null, parentColumn, childColumn);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Performs verification on the table.</summary>
		/// <param name="relation">The relation to check.</param>
		/// <exception cref="T:System.ArgumentNullException">The relation is null. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		// Token: 0x060005A6 RID: 1446 RVA: 0x00017108 File Offset: 0x00015308
		protected virtual void AddCore(DataRelation relation)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelationCollection.AddCore|INFO> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (relation == null)
			{
				throw ExceptionBuilder.ArgumentNull("relation");
			}
			relation.CheckState();
			DataSet dataSet = this.GetDataSet();
			if (relation.DataSet == dataSet)
			{
				throw ExceptionBuilder.RelationAlreadyInTheDataSet();
			}
			if (relation.DataSet != null)
			{
				throw ExceptionBuilder.RelationAlreadyInOtherDataSet();
			}
			if (relation.ChildTable.Locale.LCID != relation.ParentTable.Locale.LCID || relation.ChildTable.CaseSensitive != relation.ParentTable.CaseSensitive)
			{
				throw ExceptionBuilder.CaseLocaleMismatch();
			}
			if (relation.Nested)
			{
				relation.CheckNamespaceValidityForNestedRelations(relation.ParentTable.Namespace);
				relation.ValidateMultipleNestedRelations();
				DataTable parentTable = relation.ParentTable;
				int elementColumnCount = parentTable.ElementColumnCount;
				parentTable.ElementColumnCount = elementColumnCount + 1;
			}
		}

		/// <summary>Occurs when the collection has changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060005A7 RID: 1447 RVA: 0x000171E2 File Offset: 0x000153E2
		// (remove) Token: 0x060005A8 RID: 1448 RVA: 0x00017210 File Offset: 0x00015410
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.add_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.remove_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangedDelegate, value);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060005A9 RID: 1449 RVA: 0x0001723E File Offset: 0x0001543E
		// (remove) Token: 0x060005AA RID: 1450 RVA: 0x0001726C File Offset: 0x0001546C
		internal event CollectionChangeEventHandler CollectionChanging
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.add_CollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangingDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.remove_CollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangingDelegate, value);
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001729A File Offset: 0x0001549A
		internal string AssignName()
		{
			string text = this.MakeName(this._defaultNameIndex);
			this._defaultNameIndex++;
			return text;
		}

		/// <summary>Clears the collection of any relations.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005AC RID: 1452 RVA: 0x000172B8 File Offset: 0x000154B8
		public virtual void Clear()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataRelationCollection.Clear|API> {0}", this.ObjectID);
			try
			{
				int count = this.Count;
				this.OnCollectionChanging(InternalDataCollectionBase.s_refreshEventArgs);
				for (int i = count - 1; i >= 0; i--)
				{
					this._inTransition = this[i];
					this.RemoveCore(this._inTransition);
				}
				this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
				this._inTransition = null;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Verifies whether a <see cref="T:System.Data.DataRelation" /> with the specific name (case insensitive) exists in the collection.</summary>
		/// <returns>true, if a relation with the specified name exists; otherwise false.</returns>
		/// <param name="name">The name of the relation to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005AD RID: 1453 RVA: 0x00017344 File Offset: 0x00015544
		public virtual bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		/// <summary>Copies the collection of <see cref="T:System.Data.DataRelation" /> objects starting at the specified index.</summary>
		/// <param name="array">The array of <see cref="T:System.Data.DataRelation" /> objects to copy the collection to.</param>
		/// <param name="index">The index to start from.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005AE RID: 1454 RVA: 0x00017354 File Offset: 0x00015554
		public void CopyTo(DataRelation[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			ArrayList list = this.List;
			if (array.Length - index < list.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			for (int i = 0; i < list.Count; i++)
			{
				array[index + i] = (DataRelation)list[i];
			}
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataRelation" /> object.</summary>
		/// <returns>The 0-based index of the relation, or -1 if the relation is not found in the collection.</returns>
		/// <param name="relation">The relation to search for. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005AF RID: 1455 RVA: 0x000173BC File Offset: 0x000155BC
		public virtual int IndexOf(DataRelation relation)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				if (relation == (DataRelation)this.List[i])
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the index of the <see cref="T:System.Data.DataRelation" /> specified by name.</summary>
		/// <returns>The zero-based index of the relation with the specified name, or -1 if the relation does not exist in the collection.</returns>
		/// <param name="relationName">The name of the relation to find. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005B0 RID: 1456 RVA: 0x000173F8 File Offset: 0x000155F8
		public virtual int IndexOf(string relationName)
		{
			int num = this.InternalIndexOf(relationName);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00017414 File Offset: 0x00015614
		internal int InternalIndexOf(string name)
		{
			int num = -1;
			if (name != null && 0 < name.Length)
			{
				int count = this.List.Count;
				for (int i = 0; i < count; i++)
				{
					DataRelation dataRelation = (DataRelation)this.List[i];
					int num2 = base.NamesEqual(dataRelation.RelationName, name, false, this.GetDataSet().Locale);
					if (num2 == 1)
					{
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

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>The referenced DataSet.</returns>
		// Token: 0x060005B2 RID: 1458
		protected abstract DataSet GetDataSet();

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001748C File Offset: 0x0001568C
		private string MakeName(int index)
		{
			if (index != 1)
			{
				return "Relation" + index.ToString(CultureInfo.InvariantCulture);
			}
			return "Relation1";
		}

		/// <summary>Raises the <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060005B4 RID: 1460 RVA: 0x000174AE File Offset: 0x000156AE
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.OnCollectionChanged|INFO> {0}", this.ObjectID);
				this._onCollectionChangedDelegate(this, ccevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060005B5 RID: 1461 RVA: 0x000174DA File Offset: 0x000156DA
		protected virtual void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.OnCollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate(this, ccevent);
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00017508 File Offset: 0x00015708
		internal void RegisterName(string name)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataRelationCollection.RegisterName|INFO> {0}, name='{1}'", this.ObjectID, name);
			CultureInfo locale = this.GetDataSet().Locale;
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				if (base.NamesEqual(name, this[i].RelationName, true, locale) != 0)
				{
					throw ExceptionBuilder.DuplicateRelation(this[i].RelationName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, locale) != 0)
			{
				this._defaultNameIndex++;
			}
		}

		/// <summary>Verifies whether the specified <see cref="T:System.Data.DataRelation" /> can be removed from the collection.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataRelation" /> can be removed; otherwise, false.</returns>
		/// <param name="relation">The relation to perform the check against. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005B7 RID: 1463 RVA: 0x00017598 File Offset: 0x00015798
		public virtual bool CanRemove(DataRelation relation)
		{
			return relation != null && relation.DataSet == this.GetDataSet();
		}

		/// <summary>Removes the specified relation from the collection.</summary>
		/// <param name="relation">The relation to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation is a null value.</exception>
		/// <exception cref="T:System.ArgumentException">The relation does not belong to the collection.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005B8 RID: 1464 RVA: 0x000175B0 File Offset: 0x000157B0
		public void Remove(DataRelation relation)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelationCollection.Remove|API> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (this._inTransition == relation)
			{
				return;
			}
			this._inTransition = relation;
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, relation));
				this.RemoveCore(relation);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, relation));
			}
			finally
			{
				this._inTransition = null;
			}
		}

		/// <summary>Removes the relation at the specified index from the collection.</summary>
		/// <param name="index">The index of the relation to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a relation at the specified index. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005B9 RID: 1465 RVA: 0x0001762C File Offset: 0x0001582C
		public void RemoveAt(int index)
		{
			DataRelation dataRelation = this[index];
			if (dataRelation == null)
			{
				throw ExceptionBuilder.RelationOutOfRange(index);
			}
			this.Remove(dataRelation);
		}

		/// <summary>Removes the relation with the specified name from the collection.</summary>
		/// <param name="name">The name of the relation to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The collection does not have a relation with the specified name.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060005BA RID: 1466 RVA: 0x00017658 File Offset: 0x00015858
		public void Remove(string name)
		{
			DataRelation dataRelation = this[name];
			if (dataRelation == null)
			{
				throw ExceptionBuilder.RelationNotInTheDataSet(name);
			}
			this.Remove(dataRelation);
		}

		/// <summary>Performs a verification on the specified <see cref="T:System.Data.DataRelation" /> object.</summary>
		/// <param name="relation">The DataRelation object to verify. </param>
		/// <exception cref="T:System.ArgumentNullException">The collection does not have a relation at the specified index. </exception>
		/// <exception cref="T:System.ArgumentException">The specified relation does not belong to this collection, or it belongs to another collection. </exception>
		// Token: 0x060005BB RID: 1467 RVA: 0x00017680 File Offset: 0x00015880
		protected virtual void RemoveCore(DataRelation relation)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelationCollection.RemoveCore|INFO> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (relation == null)
			{
				throw ExceptionBuilder.ArgumentNull("relation");
			}
			DataSet dataSet = this.GetDataSet();
			if (relation.DataSet != dataSet)
			{
				throw ExceptionBuilder.RelationNotInTheDataSet(relation.RelationName);
			}
			if (relation.Nested)
			{
				DataTable parentTable = relation.ParentTable;
				int elementColumnCount = parentTable.ElementColumnCount;
				parentTable.ElementColumnCount = elementColumnCount - 1;
				relation.ParentTable.Columns.UnregisterName(relation.ChildTable.TableName);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00017710 File Offset: 0x00015910
		internal void UnregisterName(string name)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataRelationCollection.UnregisterName|INFO> {0}, name='{1}'", this.ObjectID, name);
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this.GetDataSet().Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x0400056B RID: 1387
		private DataRelation _inTransition;

		// Token: 0x0400056C RID: 1388
		private int _defaultNameIndex = 1;

		// Token: 0x0400056D RID: 1389
		private CollectionChangeEventHandler _onCollectionChangedDelegate;

		// Token: 0x0400056E RID: 1390
		private CollectionChangeEventHandler _onCollectionChangingDelegate;

		// Token: 0x0400056F RID: 1391
		private static int s_objectTypeCount;

		// Token: 0x04000570 RID: 1392
		private readonly int _objectID = Interlocked.Increment(ref DataRelationCollection.s_objectTypeCount);

		// Token: 0x02000078 RID: 120
		internal sealed class DataTableRelationCollection : DataRelationCollection
		{
			// Token: 0x060005BE RID: 1470 RVA: 0x000177A2 File Offset: 0x000159A2
			internal DataTableRelationCollection(DataTable table, bool fParentCollection)
			{
				if (table == null)
				{
					throw ExceptionBuilder.RelationTableNull();
				}
				this._table = table;
				this._fParentCollection = fParentCollection;
				this._relations = new ArrayList();
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x060005BF RID: 1471 RVA: 0x000177CC File Offset: 0x000159CC
			protected override ArrayList List
			{
				get
				{
					return this._relations;
				}
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x000177D4 File Offset: 0x000159D4
			private void EnsureDataSet()
			{
				if (this._table.DataSet == null)
				{
					throw ExceptionBuilder.RelationTableWasRemoved();
				}
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x000177E9 File Offset: 0x000159E9
			protected override DataSet GetDataSet()
			{
				this.EnsureDataSet();
				return this._table.DataSet;
			}

			// Token: 0x17000116 RID: 278
			public override DataRelation this[int index]
			{
				get
				{
					if (index >= 0 && index < this._relations.Count)
					{
						return (DataRelation)this._relations[index];
					}
					throw ExceptionBuilder.RelationOutOfRange(index);
				}
			}

			// Token: 0x17000117 RID: 279
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.InternalIndexOf(name);
					if (num == -2)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
					if (num >= 0)
					{
						return (DataRelation)this.List[num];
					}
					return null;
				}
			}

			// Token: 0x14000009 RID: 9
			// (add) Token: 0x060005C4 RID: 1476 RVA: 0x00017868 File Offset: 0x00015A68
			// (remove) Token: 0x060005C5 RID: 1477 RVA: 0x000178A0 File Offset: 0x00015AA0
			internal event CollectionChangeEventHandler RelationPropertyChanged;

			// Token: 0x060005C6 RID: 1478 RVA: 0x000178D5 File Offset: 0x00015AD5
			internal void OnRelationPropertyChanged(CollectionChangeEventArgs ccevent)
			{
				if (!this._fParentCollection)
				{
					this._table.UpdatePropertyDescriptorCollectionCache();
				}
				CollectionChangeEventHandler relationPropertyChanged = this.RelationPropertyChanged;
				if (relationPropertyChanged == null)
				{
					return;
				}
				relationPropertyChanged(this, ccevent);
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x000178FC File Offset: 0x00015AFC
			private void AddCache(DataRelation relation)
			{
				this._relations.Add(relation);
				if (!this._fParentCollection)
				{
					this._table.UpdatePropertyDescriptorCollectionCache();
				}
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x00017920 File Offset: 0x00015B20
			protected override void AddCore(DataRelation relation)
			{
				if (this._fParentCollection)
				{
					if (relation.ChildTable != this._table)
					{
						throw ExceptionBuilder.ChildTableMismatch();
					}
				}
				else if (relation.ParentTable != this._table)
				{
					throw ExceptionBuilder.ParentTableMismatch();
				}
				this.GetDataSet().Relations.Add(relation);
				this.AddCache(relation);
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x00017975 File Offset: 0x00015B75
			public override bool CanRemove(DataRelation relation)
			{
				if (!base.CanRemove(relation))
				{
					return false;
				}
				if (this._fParentCollection)
				{
					if (relation.ChildTable != this._table)
					{
						return false;
					}
				}
				else if (relation.ParentTable != this._table)
				{
					return false;
				}
				return true;
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x000179AC File Offset: 0x00015BAC
			private void RemoveCache(DataRelation relation)
			{
				for (int i = 0; i < this._relations.Count; i++)
				{
					if (relation == this._relations[i])
					{
						this._relations.RemoveAt(i);
						if (!this._fParentCollection)
						{
							this._table.UpdatePropertyDescriptorCollectionCache();
						}
						return;
					}
				}
				throw ExceptionBuilder.RelationDoesNotExist();
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x00017A04 File Offset: 0x00015C04
			protected override void RemoveCore(DataRelation relation)
			{
				if (this._fParentCollection)
				{
					if (relation.ChildTable != this._table)
					{
						throw ExceptionBuilder.ChildTableMismatch();
					}
				}
				else if (relation.ParentTable != this._table)
				{
					throw ExceptionBuilder.ParentTableMismatch();
				}
				this.GetDataSet().Relations.Remove(relation);
				this.RemoveCache(relation);
			}

			// Token: 0x04000571 RID: 1393
			private readonly DataTable _table;

			// Token: 0x04000572 RID: 1394
			private readonly ArrayList _relations;

			// Token: 0x04000573 RID: 1395
			private readonly bool _fParentCollection;
		}

		// Token: 0x02000079 RID: 121
		internal sealed class DataSetRelationCollection : DataRelationCollection
		{
			// Token: 0x060005CC RID: 1484 RVA: 0x00017A59 File Offset: 0x00015C59
			internal DataSetRelationCollection(DataSet dataSet)
			{
				if (dataSet == null)
				{
					throw ExceptionBuilder.RelationDataSetNull();
				}
				this._dataSet = dataSet;
				this._relations = new ArrayList();
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060005CD RID: 1485 RVA: 0x00017A7C File Offset: 0x00015C7C
			protected override ArrayList List
			{
				get
				{
					return this._relations;
				}
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x00017A84 File Offset: 0x00015C84
			public override void AddRange(DataRelation[] relations)
			{
				if (this._dataSet._fInitInProgress)
				{
					this._delayLoadingRelations = relations;
					return;
				}
				if (relations != null)
				{
					foreach (DataRelation dataRelation in relations)
					{
						if (dataRelation != null)
						{
							base.Add(dataRelation);
						}
					}
				}
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x00017AC7 File Offset: 0x00015CC7
			public override void Clear()
			{
				base.Clear();
				if (this._dataSet._fInitInProgress && this._delayLoadingRelations != null)
				{
					this._delayLoadingRelations = null;
				}
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x00017AEB File Offset: 0x00015CEB
			protected override DataSet GetDataSet()
			{
				return this._dataSet;
			}

			// Token: 0x17000119 RID: 281
			public override DataRelation this[int index]
			{
				get
				{
					if (index >= 0 && index < this._relations.Count)
					{
						return (DataRelation)this._relations[index];
					}
					throw ExceptionBuilder.RelationOutOfRange(index);
				}
			}

			// Token: 0x1700011A RID: 282
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.InternalIndexOf(name);
					if (num == -2)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
					if (num >= 0)
					{
						return (DataRelation)this.List[num];
					}
					return null;
				}
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x00017B5C File Offset: 0x00015D5C
			protected override void AddCore(DataRelation relation)
			{
				base.AddCore(relation);
				if (relation.ChildTable.DataSet != this._dataSet || relation.ParentTable.DataSet != this._dataSet)
				{
					throw ExceptionBuilder.ForeignRelation();
				}
				relation.CheckState();
				if (relation.Nested)
				{
					relation.CheckNestedRelations();
				}
				if (relation._relationName.Length == 0)
				{
					relation._relationName = base.AssignName();
				}
				else
				{
					base.RegisterName(relation._relationName);
				}
				DataKey childKey = relation.ChildKey;
				for (int i = 0; i < this._relations.Count; i++)
				{
					if (childKey.ColumnsEqual(((DataRelation)this._relations[i]).ChildKey) && relation.ParentKey.ColumnsEqual(((DataRelation)this._relations[i]).ParentKey))
					{
						throw ExceptionBuilder.RelationAlreadyExists();
					}
				}
				this._relations.Add(relation);
				((DataRelationCollection.DataTableRelationCollection)relation.ParentTable.ChildRelations).Add(relation);
				((DataRelationCollection.DataTableRelationCollection)relation.ChildTable.ParentRelations).Add(relation);
				relation.SetDataSet(this._dataSet);
				relation.ChildKey.GetSortIndex().AddRef();
				if (relation.Nested)
				{
					relation.ChildTable.CacheNestedParent();
				}
				ForeignKeyConstraint foreignKeyConstraint = relation.ChildTable.Constraints.FindForeignKeyConstraint(relation.ParentColumnsReference, relation.ChildColumnsReference);
				if (relation._createConstraints && foreignKeyConstraint == null)
				{
					relation.ChildTable.Constraints.Add(foreignKeyConstraint = new ForeignKeyConstraint(relation.ParentColumnsReference, relation.ChildColumnsReference));
					try
					{
						foreignKeyConstraint.ConstraintName = relation.RelationName;
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
				}
				UniqueConstraint uniqueConstraint = relation.ParentTable.Constraints.FindKeyConstraint(relation.ParentColumnsReference);
				relation.SetParentKeyConstraint(uniqueConstraint);
				relation.SetChildKeyConstraint(foreignKeyConstraint);
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x00017D60 File Offset: 0x00015F60
			protected override void RemoveCore(DataRelation relation)
			{
				base.RemoveCore(relation);
				this._dataSet.OnRemoveRelationHack(relation);
				relation.SetDataSet(null);
				relation.ChildKey.GetSortIndex().RemoveRef();
				if (relation.Nested)
				{
					relation.ChildTable.CacheNestedParent();
				}
				for (int i = 0; i < this._relations.Count; i++)
				{
					if (relation == this._relations[i])
					{
						this._relations.RemoveAt(i);
						((DataRelationCollection.DataTableRelationCollection)relation.ParentTable.ChildRelations).Remove(relation);
						((DataRelationCollection.DataTableRelationCollection)relation.ChildTable.ParentRelations).Remove(relation);
						if (relation.Nested)
						{
							relation.ChildTable.CacheNestedParent();
						}
						base.UnregisterName(relation.RelationName);
						relation.SetParentKeyConstraint(null);
						relation.SetChildKeyConstraint(null);
						return;
					}
				}
				throw ExceptionBuilder.RelationDoesNotExist();
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x00017E44 File Offset: 0x00016044
			internal void FinishInitRelations()
			{
				if (this._delayLoadingRelations == null)
				{
					return;
				}
				for (int i = 0; i < this._delayLoadingRelations.Length; i++)
				{
					DataRelation dataRelation = this._delayLoadingRelations[i];
					if (dataRelation._parentColumnNames == null || dataRelation._childColumnNames == null)
					{
						base.Add(dataRelation);
					}
					else
					{
						int num = dataRelation._parentColumnNames.Length;
						DataColumn[] array = new DataColumn[num];
						DataColumn[] array2 = new DataColumn[num];
						for (int j = 0; j < num; j++)
						{
							if (dataRelation._parentTableNamespace == null)
							{
								array[j] = this._dataSet.Tables[dataRelation._parentTableName].Columns[dataRelation._parentColumnNames[j]];
							}
							else
							{
								array[j] = this._dataSet.Tables[dataRelation._parentTableName, dataRelation._parentTableNamespace].Columns[dataRelation._parentColumnNames[j]];
							}
							if (dataRelation._childTableNamespace == null)
							{
								array2[j] = this._dataSet.Tables[dataRelation._childTableName].Columns[dataRelation._childColumnNames[j]];
							}
							else
							{
								array2[j] = this._dataSet.Tables[dataRelation._childTableName, dataRelation._childTableNamespace].Columns[dataRelation._childColumnNames[j]];
							}
						}
						base.Add(new DataRelation(dataRelation._relationName, array, array2, false)
						{
							Nested = dataRelation._nested
						});
					}
				}
				this._delayLoadingRelations = null;
			}

			// Token: 0x04000575 RID: 1397
			private readonly DataSet _dataSet;

			// Token: 0x04000576 RID: 1398
			private readonly ArrayList _relations;

			// Token: 0x04000577 RID: 1399
			private DataRelation[] _delayLoadingRelations;
		}
	}
}
