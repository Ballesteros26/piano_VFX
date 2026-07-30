using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using Unity;

namespace System.Data
{
	/// <summary>Represents a collection of constraints for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000058 RID: 88
	[DefaultEvent("CollectionChanged")]
	public sealed class ConstraintCollection : InternalDataCollectionBase
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000F5CF File Offset: 0x0000D7CF
		internal ConstraintCollection(DataTable table)
		{
			this._list = new ArrayList();
			this._defaultNameIndex = 1;
			base..ctor();
			this._table = table;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Constraint" /> from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Constraint" /> at the specified index.</returns>
		/// <param name="index">The index of the constraint to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000AE RID: 174
		public Constraint this[int index]
		{
			get
			{
				if (index >= 0 && index < this.List.Count)
				{
					return (Constraint)this.List[index];
				}
				throw ExceptionBuilder.ConstraintOutOfRange(index);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000F624 File Offset: 0x0000D824
		internal DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Constraint" /> from the collection with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.Constraint" /> with the specified name; otherwise a null value if the <see cref="T:System.Data.Constraint" /> does not exist.</returns>
		/// <param name="name">The <see cref="P:System.Data.Constraint.ConstraintName" /> of the constraint to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000B0 RID: 176
		public Constraint this[string name]
		{
			get
			{
				int num = this.InternalIndexOf(name);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num >= 0)
				{
					return (Constraint)this.List[num];
				}
				return null;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Constraint" /> object to the collection.</summary>
		/// <param name="constraint">The Constraint to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="constraint" /> argument is null. </exception>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection, or belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the same name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002CD RID: 717 RVA: 0x0000F664 File Offset: 0x0000D864
		public void Add(Constraint constraint)
		{
			this.Add(constraint, true);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000F670 File Offset: 0x0000D870
		internal void Add(Constraint constraint, bool addUniqueWhenAddingForeign)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (this.FindConstraint(constraint) != null)
			{
				throw ExceptionBuilder.DuplicateConstraint(this.FindConstraint(constraint).ConstraintName);
			}
			if (1 < this._table.NestedParentRelations.Length && !this.AutoGenerated(constraint))
			{
				throw ExceptionBuilder.CantAddConstraintToMultipleNestedTable(this._table.TableName);
			}
			if (constraint is UniqueConstraint)
			{
				if (((UniqueConstraint)constraint)._bPrimaryKey && this.Table._primaryKey != null)
				{
					throw ExceptionBuilder.AddPrimaryKeyConstraint();
				}
				this.AddUniqueConstraint((UniqueConstraint)constraint);
			}
			else if (constraint is ForeignKeyConstraint)
			{
				ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraint;
				if (addUniqueWhenAddingForeign && foreignKeyConstraint.RelatedTable.Constraints.FindKeyConstraint(foreignKeyConstraint.RelatedColumnsReference) == null)
				{
					if (constraint.ConstraintName.Length == 0)
					{
						constraint.ConstraintName = this.AssignName();
					}
					else
					{
						this.RegisterName(constraint.ConstraintName);
					}
					UniqueConstraint uniqueConstraint = new UniqueConstraint(foreignKeyConstraint.RelatedColumnsReference);
					foreignKeyConstraint.RelatedTable.Constraints.Add(uniqueConstraint);
				}
				this.AddForeignKeyConstraint((ForeignKeyConstraint)constraint);
			}
			this.BaseAdd(constraint);
			this.ArrayAdd(constraint);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, constraint));
			if (constraint is UniqueConstraint && ((UniqueConstraint)constraint)._bPrimaryKey)
			{
				this.Table.PrimaryKey = ((UniqueConstraint)constraint).ColumnsReference;
			}
		}

		/// <summary>Constructs a new <see cref="T:System.Data.UniqueConstraint" /> with the specified name, array of <see cref="T:System.Data.DataColumn" /> objects, and value that indicates whether the column is a primary key, and adds it to the collection.</summary>
		/// <returns>A new UniqueConstraint.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.UniqueConstraint" />. </param>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to which the constraint applies. </param>
		/// <param name="primaryKey">Specifies whether the column should be the primary key. If true, the column will be a primary key column.</param>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection.-Or- The constraint belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002CF RID: 719 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public Constraint Add(string name, DataColumn[] columns, bool primaryKey)
		{
			UniqueConstraint uniqueConstraint = new UniqueConstraint(name, columns);
			this.Add(uniqueConstraint);
			if (primaryKey)
			{
				this.Table.PrimaryKey = columns;
			}
			return uniqueConstraint;
		}

		/// <summary>Constructs a new <see cref="T:System.Data.UniqueConstraint" /> with the specified name, <see cref="T:System.Data.DataColumn" />, and value that indicates whether the column is a primary key, and adds it to the collection.</summary>
		/// <returns>A new UniqueConstraint.</returns>
		/// <param name="name">The name of the UniqueConstraint. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to which the constraint applies. </param>
		/// <param name="primaryKey">Specifies whether the column should be the primary key. If true, the column will be a primary key column. </param>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection.-Or- The constraint belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002D0 RID: 720 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		public Constraint Add(string name, DataColumn column, bool primaryKey)
		{
			UniqueConstraint uniqueConstraint = new UniqueConstraint(name, column);
			this.Add(uniqueConstraint);
			if (primaryKey)
			{
				this.Table.PrimaryKey = uniqueConstraint.ColumnsReference;
			}
			return uniqueConstraint;
		}

		/// <summary>Constructs a new <see cref="T:System.Data.ForeignKeyConstraint" /> with the specified name, parent column, and child column, and adds the constraint to the collection.</summary>
		/// <returns>A new ForeignKeyConstraint.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. </param>
		/// <param name="primaryKeyColumn">The primary key, or parent, <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="foreignKeyColumn">The foreign key, or child, <see cref="T:System.Data.DataColumn" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002D1 RID: 721 RVA: 0x0000F828 File Offset: 0x0000DA28
		public Constraint Add(string name, DataColumn primaryKeyColumn, DataColumn foreignKeyColumn)
		{
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(name, primaryKeyColumn, foreignKeyColumn);
			this.Add(foreignKeyConstraint);
			return foreignKeyConstraint;
		}

		/// <summary>Constructs a new <see cref="T:System.Data.ForeignKeyConstraint" />, with the specified arrays of parent columns and child columns, and adds the constraint to the collection.</summary>
		/// <returns>A new ForeignKeyConstraint.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. </param>
		/// <param name="primaryKeyColumns">An array of <see cref="T:System.Data.DataColumn" /> objects that are the primary key, or parent, columns. </param>
		/// <param name="foreignKeyColumns">An array of <see cref="T:System.Data.DataColumn" /> objects that are the foreign key, or child, columns. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002D2 RID: 722 RVA: 0x0000F848 File Offset: 0x0000DA48
		public Constraint Add(string name, DataColumn[] primaryKeyColumns, DataColumn[] foreignKeyColumns)
		{
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(name, primaryKeyColumns, foreignKeyColumns);
			this.Add(foreignKeyConstraint);
			return foreignKeyConstraint;
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.ConstraintCollection" /> array to the end of the collection.</summary>
		/// <param name="constraints">An array of <see cref="T:System.Data.ConstraintCollection" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002D3 RID: 723 RVA: 0x0000F868 File Offset: 0x0000DA68
		public void AddRange(Constraint[] constraints)
		{
			if (this._table.fInitInProgress)
			{
				this._delayLoadingConstraints = constraints;
				this._fLoadForeignKeyConstraintsOnly = false;
				return;
			}
			if (constraints != null)
			{
				foreach (Constraint constraint in constraints)
				{
					if (constraint != null)
					{
						this.Add(constraint);
					}
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		private void AddUniqueConstraint(UniqueConstraint constraint)
		{
			DataColumn[] columnsReference = constraint.ColumnsReference;
			for (int i = 0; i < columnsReference.Length; i++)
			{
				if (columnsReference[i].Table != this._table)
				{
					throw ExceptionBuilder.ConstraintForeignTable();
				}
			}
			constraint.ConstraintIndexInitialize();
			if (!constraint.CanEnableConstraint())
			{
				constraint.ConstraintIndexClear();
				throw ExceptionBuilder.UniqueConstraintViolation();
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000F906 File Offset: 0x0000DB06
		private void AddForeignKeyConstraint(ForeignKeyConstraint constraint)
		{
			if (!constraint.CanEnableConstraint())
			{
				throw ExceptionBuilder.ConstraintParentValues();
			}
			constraint.CheckCanAddToCollection(this);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000F920 File Offset: 0x0000DB20
		private bool AutoGenerated(Constraint constraint)
		{
			ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
			if (foreignKeyConstraint != null)
			{
				return XmlTreeGen.AutoGenerated(foreignKeyConstraint, false);
			}
			return XmlTreeGen.AutoGenerated((UniqueConstraint)constraint);
		}

		/// <summary>Occurs whenever the <see cref="T:System.Data.ConstraintCollection" /> is changed because of <see cref="T:System.Data.Constraint" /> objects being added or removed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002D7 RID: 727 RVA: 0x0000F94A File Offset: 0x0000DB4A
		// (remove) Token: 0x060002D8 RID: 728 RVA: 0x0000F963 File Offset: 0x0000DB63
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this._onCollectionChanged = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChanged, value);
			}
			remove
			{
				this._onCollectionChanged = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChanged, value);
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000F97C File Offset: 0x0000DB7C
		private void ArrayAdd(Constraint constraint)
		{
			this.List.Add(constraint);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000F98B File Offset: 0x0000DB8B
		private void ArrayRemove(Constraint constraint)
		{
			this.List.Remove(constraint);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F999 File Offset: 0x0000DB99
		internal string AssignName()
		{
			string text = this.MakeName(this._defaultNameIndex);
			this._defaultNameIndex++;
			return text;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000F9B5 File Offset: 0x0000DBB5
		private void BaseAdd(Constraint constraint)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (constraint.ConstraintName.Length == 0)
			{
				constraint.ConstraintName = this.AssignName();
			}
			else
			{
				this.RegisterName(constraint.ConstraintName);
			}
			constraint.InCollection = true;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		private void BaseGroupSwitch(Constraint[] oldArray, int oldLength, Constraint[] newArray, int newLength)
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
				if (!flag)
				{
					this.BaseRemove(oldArray[i]);
					this.List.Remove(oldArray[i]);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (!newArray[k].InCollection)
				{
					this.BaseAdd(newArray[k]);
				}
				this.List.Add(newArray[k]);
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000FA84 File Offset: 0x0000DC84
		private void BaseRemove(Constraint constraint)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (constraint.Table != this._table)
			{
				throw ExceptionBuilder.ConstraintRemoveFailed();
			}
			this.UnregisterName(constraint.ConstraintName);
			constraint.InCollection = false;
			if (constraint is UniqueConstraint)
			{
				for (int i = 0; i < this.Table.ChildRelations.Count; i++)
				{
					DataRelation dataRelation = this.Table.ChildRelations[i];
					if (dataRelation.ParentKeyConstraint == constraint)
					{
						dataRelation.SetParentKeyConstraint(null);
					}
				}
				((UniqueConstraint)constraint).ConstraintIndexClear();
				return;
			}
			if (constraint is ForeignKeyConstraint)
			{
				for (int j = 0; j < this.Table.ParentRelations.Count; j++)
				{
					DataRelation dataRelation2 = this.Table.ParentRelations[j];
					if (dataRelation2.ChildKeyConstraint == constraint)
					{
						dataRelation2.SetChildKeyConstraint(null);
					}
				}
			}
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.Constraint" /> can be removed.</summary>
		/// <returns>true if the <see cref="T:System.Data.Constraint" /> can be removed from collection; otherwise, false.</returns>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to be tested for removal from the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002DF RID: 735 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public bool CanRemove(Constraint constraint)
		{
			return this.CanRemove(constraint, false);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000FB66 File Offset: 0x0000DD66
		internal bool CanRemove(Constraint constraint, bool fThrowException)
		{
			return constraint.CanBeRemovedFromCollection(this, fThrowException);
		}

		/// <summary>Clears the collection of any <see cref="T:System.Data.Constraint" /> objects.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002E1 RID: 737 RVA: 0x0000FB70 File Offset: 0x0000DD70
		public void Clear()
		{
			if (this._table != null)
			{
				this._table.PrimaryKey = null;
				for (int i = 0; i < this._table.ParentRelations.Count; i++)
				{
					this._table.ParentRelations[i].SetChildKeyConstraint(null);
				}
				for (int j = 0; j < this._table.ChildRelations.Count; j++)
				{
					this._table.ChildRelations[j].SetParentKeyConstraint(null);
				}
			}
			if (this._table.fInitInProgress && this._delayLoadingConstraints != null)
			{
				this._delayLoadingConstraints = null;
				this._fLoadForeignKeyConstraintsOnly = false;
			}
			int count = this.List.Count;
			Constraint[] array = new Constraint[this.List.Count];
			this.List.CopyTo(array, 0);
			try
			{
				this.BaseGroupSwitch(array, count, null, 0);
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				this.BaseGroupSwitch(null, 0, array, count);
				this.List.Clear();
				for (int k = 0; k < count; k++)
				{
					this.List.Add(array[k]);
				}
				throw;
			}
			this.List.Clear();
			this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
		}

		/// <summary>Indicates whether the <see cref="T:System.Data.Constraint" /> object specified by name exists in the collection.</summary>
		/// <returns>true if the collection contains the specified constraint; otherwise, false.</returns>
		/// <param name="name">The <see cref="P:System.Data.Constraint.ConstraintName" /> of the constraint. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002E2 RID: 738 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		public bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		internal bool Contains(string name, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.Contains(name);
			}
			int num = this.InternalIndexOf(name);
			return num >= 0 && name == ((Constraint)this.List[num]).ConstraintName;
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002E4 RID: 740 RVA: 0x0000FD18 File Offset: 0x0000DF18
		public void CopyTo(Constraint[] array, int index)
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
				array[index + i] = (Constraint)this._list[i];
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000FD88 File Offset: 0x0000DF88
		internal Constraint FindConstraint(Constraint constraint)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				if (((Constraint)this.List[i]).Equals(constraint))
				{
					return (Constraint)this.List[i];
				}
			}
			return null;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		internal UniqueConstraint FindKeyConstraint(DataColumn[] columns)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				UniqueConstraint uniqueConstraint = this.List[i] as UniqueConstraint;
				if (uniqueConstraint != null && ConstraintCollection.CompareArrays(uniqueConstraint.Key.ColumnsReference, columns))
				{
					return uniqueConstraint;
				}
			}
			return null;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000FE30 File Offset: 0x0000E030
		internal UniqueConstraint FindKeyConstraint(DataColumn column)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				UniqueConstraint uniqueConstraint = this.List[i] as UniqueConstraint;
				if (uniqueConstraint != null && uniqueConstraint.Key.ColumnsReference.Length == 1 && uniqueConstraint.Key.ColumnsReference[0] == column)
				{
					return uniqueConstraint;
				}
			}
			return null;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000FE94 File Offset: 0x0000E094
		internal ForeignKeyConstraint FindForeignKeyConstraint(DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				ForeignKeyConstraint foreignKeyConstraint = this.List[i] as ForeignKeyConstraint;
				if (foreignKeyConstraint != null && ConstraintCollection.CompareArrays(foreignKeyConstraint.ParentKey.ColumnsReference, parentColumns) && ConstraintCollection.CompareArrays(foreignKeyConstraint.ChildKey.ColumnsReference, childColumns))
				{
					return foreignKeyConstraint;
				}
			}
			return null;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000FF00 File Offset: 0x0000E100
		private static bool CompareArrays(DataColumn[] a1, DataColumn[] a2)
		{
			if (a1.Length != a2.Length)
			{
				return false;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < a2.Length; j++)
				{
					if (a1[i] == a2[j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.Constraint" />.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Data.Constraint" /> if it is in the collection; otherwise, -1.</returns>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to search for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002EA RID: 746 RVA: 0x0000FF48 File Offset: 0x0000E148
		public int IndexOf(Constraint constraint)
		{
			if (constraint != null)
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (constraint == (Constraint)this.List[i])
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the index of the <see cref="T:System.Data.Constraint" /> specified by name.</summary>
		/// <returns>The index of the <see cref="T:System.Data.Constraint" /> if it is in the collection; otherwise, -1.</returns>
		/// <param name="constraintName">The name of the <see cref="T:System.Data.Constraint" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002EB RID: 747 RVA: 0x0000FF84 File Offset: 0x0000E184
		public int IndexOf(string constraintName)
		{
			int num = this.InternalIndexOf(constraintName);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000FFA0 File Offset: 0x0000E1A0
		internal int InternalIndexOf(string constraintName)
		{
			int num = -1;
			if (constraintName != null && 0 < constraintName.Length)
			{
				int count = this.List.Count;
				for (int i = 0; i < count; i++)
				{
					Constraint constraint = (Constraint)this.List[i];
					int num2 = base.NamesEqual(constraint.ConstraintName, constraintName, false, this._table.Locale);
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

		// Token: 0x060002ED RID: 749 RVA: 0x00010018 File Offset: 0x0000E218
		private string MakeName(int index)
		{
			if (1 == index)
			{
				return "Constraint1";
			}
			return "Constraint" + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001003A File Offset: 0x0000E23A
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			CollectionChangeEventHandler onCollectionChanged = this._onCollectionChanged;
			if (onCollectionChanged == null)
			{
				return;
			}
			onCollectionChanged(this, ccevent);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00010050 File Offset: 0x0000E250
		internal void RegisterName(string name)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				if (base.NamesEqual(name, ((Constraint)this.List[i]).ConstraintName, true, this._table.Locale) != 0)
				{
					throw ExceptionBuilder.DuplicateConstraintName(((Constraint)this.List[i]).ConstraintName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, this._table.Locale) != 0)
			{
				this._defaultNameIndex++;
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Constraint" /> from the collection.</summary>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="constraint" /> argument is null. </exception>
		/// <exception cref="T:System.ArgumentException">The constraint does not belong to the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002F0 RID: 752 RVA: 0x000100EC File Offset: 0x0000E2EC
		public void Remove(Constraint constraint)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (this.CanRemove(constraint, true))
			{
				this.BaseRemove(constraint);
				this.ArrayRemove(constraint);
				if (constraint is UniqueConstraint && ((UniqueConstraint)constraint).IsPrimaryKey)
				{
					this.Table.PrimaryKey = null;
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, constraint));
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.Constraint" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The collection does not have a constraint at this index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002F1 RID: 753 RVA: 0x00010150 File Offset: 0x0000E350
		public void RemoveAt(int index)
		{
			Constraint constraint = this[index];
			if (constraint == null)
			{
				throw ExceptionBuilder.ConstraintOutOfRange(index);
			}
			this.Remove(constraint);
		}

		/// <summary>Removes the <see cref="T:System.Data.Constraint" /> object specified by name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002F2 RID: 754 RVA: 0x00010178 File Offset: 0x0000E378
		public void Remove(string name)
		{
			Constraint constraint = this[name];
			if (constraint == null)
			{
				throw ExceptionBuilder.ConstraintNotInTheTable(name);
			}
			this.Remove(constraint);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000101A0 File Offset: 0x0000E3A0
		internal void UnregisterName(string name)
		{
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this._table.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00010200 File Offset: 0x0000E400
		internal void FinishInitConstraints()
		{
			if (this._delayLoadingConstraints == null)
			{
				return;
			}
			for (int i = 0; i < this._delayLoadingConstraints.Length; i++)
			{
				if (this._delayLoadingConstraints[i] is UniqueConstraint)
				{
					if (!this._fLoadForeignKeyConstraintsOnly)
					{
						UniqueConstraint uniqueConstraint = (UniqueConstraint)this._delayLoadingConstraints[i];
						if (uniqueConstraint._columnNames == null)
						{
							this.Add(uniqueConstraint);
						}
						else
						{
							int num = uniqueConstraint._columnNames.Length;
							DataColumn[] array = new DataColumn[num];
							for (int j = 0; j < num; j++)
							{
								array[j] = this._table.Columns[uniqueConstraint._columnNames[j]];
							}
							if (uniqueConstraint._bPrimaryKey)
							{
								if (this._table._primaryKey != null)
								{
									throw ExceptionBuilder.AddPrimaryKeyConstraint();
								}
								this.Add(uniqueConstraint.ConstraintName, array, true);
							}
							else
							{
								UniqueConstraint uniqueConstraint2 = new UniqueConstraint(uniqueConstraint._constraintName, array);
								if (this.FindConstraint(uniqueConstraint2) == null)
								{
									this.Add(uniqueConstraint2);
								}
							}
						}
					}
				}
				else
				{
					ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)this._delayLoadingConstraints[i];
					if (foreignKeyConstraint._parentColumnNames == null || foreignKeyConstraint._childColumnNames == null)
					{
						this.Add(foreignKeyConstraint);
					}
					else if (this._table.DataSet == null)
					{
						this._fLoadForeignKeyConstraintsOnly = true;
					}
					else
					{
						int num = foreignKeyConstraint._parentColumnNames.Length;
						DataColumn[] array = new DataColumn[num];
						DataColumn[] array2 = new DataColumn[num];
						for (int k = 0; k < num; k++)
						{
							if (foreignKeyConstraint._parentTableNamespace == null)
							{
								array[k] = this._table.DataSet.Tables[foreignKeyConstraint._parentTableName].Columns[foreignKeyConstraint._parentColumnNames[k]];
							}
							else
							{
								array[k] = this._table.DataSet.Tables[foreignKeyConstraint._parentTableName, foreignKeyConstraint._parentTableNamespace].Columns[foreignKeyConstraint._parentColumnNames[k]];
							}
							array2[k] = this._table.Columns[foreignKeyConstraint._childColumnNames[k]];
						}
						this.Add(new ForeignKeyConstraint(foreignKeyConstraint._constraintName, array, array2)
						{
							AcceptRejectRule = foreignKeyConstraint._acceptRejectRule,
							DeleteRule = foreignKeyConstraint._deleteRule,
							UpdateRule = foreignKeyConstraint._updateRule
						});
					}
				}
			}
			if (!this._fLoadForeignKeyConstraintsOnly)
			{
				this._delayLoadingConstraints = null;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00010468 File Offset: 0x0000E668
		internal ConstraintCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400050A RID: 1290
		private readonly DataTable _table;

		// Token: 0x0400050B RID: 1291
		private readonly ArrayList _list;

		// Token: 0x0400050C RID: 1292
		private int _defaultNameIndex;

		// Token: 0x0400050D RID: 1293
		private CollectionChangeEventHandler _onCollectionChanged;

		// Token: 0x0400050E RID: 1294
		private Constraint[] _delayLoadingConstraints;

		// Token: 0x0400050F RID: 1295
		private bool _fLoadForeignKeyConstraintsOnly;
	}
}
