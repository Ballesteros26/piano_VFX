using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using Unity;

namespace System.Data
{
	/// <summary>Represents a collection of <see cref="T:System.Data.DataColumn" /> objects for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000065 RID: 101
	[DefaultEvent("CollectionChanged")]
	public sealed class DataColumnCollection : InternalDataCollectionBase
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00012D82 File Offset: 0x00010F82
		internal DataColumnCollection(DataTable table)
		{
			this._list = new ArrayList();
			this._defaultNameIndex = 1;
			this._columnsImplementingIChangeTracking = Array.Empty<DataColumn>();
			base..ctor();
			this._table = table;
			this._columnFromName = new Dictionary<string, DataColumn>();
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00012DB9 File Offset: 0x00010FB9
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00012DC1 File Offset: 0x00010FC1
		internal DataColumn[] ColumnsImplementingIChangeTracking
		{
			get
			{
				return this._columnsImplementingIChangeTracking;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00012DC9 File Offset: 0x00010FC9
		internal int ColumnsImplementingIChangeTrackingCount
		{
			get
			{
				return this._nColumnsImplementingIChangeTracking;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00012DD1 File Offset: 0x00010FD1
		internal int ColumnsImplementingIRevertibleChangeTrackingCount
		{
			get
			{
				return this._nColumnsImplementingIRevertibleChangeTracking;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the column to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000F2 RID: 242
		public DataColumn this[int index]
		{
			get
			{
				DataColumn dataColumn;
				try
				{
					dataColumn = (DataColumn)this._list[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw ExceptionBuilder.ColumnOutOfRange(index);
				}
				return dataColumn;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> from the collection with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> in the collection with the specified <see cref="P:System.Data.DataColumn.ColumnName" />; otherwise a null value if the <see cref="T:System.Data.DataColumn" /> does not exist.</returns>
		/// <param name="name">The <see cref="P:System.Data.DataColumn.ColumnName" /> of the column to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000F3 RID: 243
		public DataColumn this[string name]
		{
			get
			{
				if (name == null)
				{
					throw ExceptionBuilder.ArgumentNull("name");
				}
				DataColumn dataColumn;
				if (!this._columnFromName.TryGetValue(name, out dataColumn) || dataColumn == null)
				{
					int num = this.IndexOfCaseInsensitive(name);
					if (0 <= num)
					{
						dataColumn = (DataColumn)this._list[num];
					}
					else if (-2 == num)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
				}
				return dataColumn;
			}
		}

		// Token: 0x170000F4 RID: 244
		internal DataColumn this[string name, string ns]
		{
			get
			{
				DataColumn dataColumn;
				if (this._columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null && dataColumn.Namespace == ns)
				{
					return dataColumn;
				}
				return null;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00012EA5 File Offset: 0x000110A5
		internal void EnsureAdditionalCapacity(int capacity)
		{
			if (this._list.Capacity < capacity + this._list.Count)
			{
				this._list.Capacity = capacity + this._list.Count;
			}
		}

		/// <summary>Creates and adds the specified <see cref="T:System.Data.DataColumn" /> object to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The column already belongs to this collection, or to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003C7 RID: 967 RVA: 0x00012ED9 File Offset: 0x000110D9
		public void Add(DataColumn column)
		{
			this.AddAt(-1, column);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012EE4 File Offset: 0x000110E4
		internal void AddAt(int index, DataColumn column)
		{
			if (column != null && column.ColumnMapping == MappingType.SimpleContent)
			{
				if (this._table.XmlText != null && this._table.XmlText != column)
				{
					throw ExceptionBuilder.CannotAddColumn3();
				}
				if (this._table.ElementColumnCount > 0)
				{
					throw ExceptionBuilder.CannotAddColumn4(column.ColumnName);
				}
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
				this.BaseAdd(column);
				if (index != -1)
				{
					this.ArrayAdd(index, column);
				}
				else
				{
					this.ArrayAdd(column);
				}
				this._table.XmlText = column;
			}
			else
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
				this.BaseAdd(column);
				if (index != -1)
				{
					this.ArrayAdd(index, column);
				}
				else
				{
					this.ArrayAdd(column);
				}
				if (column.ColumnMapping == MappingType.Element)
				{
					DataTable table = this._table;
					int elementColumnCount = table.ElementColumnCount;
					table.ElementColumnCount = elementColumnCount + 1;
				}
			}
			if (!this._table.fInitInProgress && column != null && column.Computed)
			{
				column.Expression = column.Expression;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataColumn" /> array to the end of the collection.</summary>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003C9 RID: 969 RVA: 0x00012FE8 File Offset: 0x000111E8
		public void AddRange(DataColumn[] columns)
		{
			if (this._table.fInitInProgress)
			{
				this._delayedAddRangeColumns = columns;
				return;
			}
			if (columns != null)
			{
				foreach (DataColumn dataColumn in columns)
				{
					if (dataColumn != null)
					{
						this.Add(dataColumn);
					}
				}
			}
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name, type, and expression to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="columnName">The name to use when you create the column. </param>
		/// <param name="type">The <see cref="P:System.Data.DataColumn.DataType" /> of the new column. </param>
		/// <param name="expression">The expression to assign to the <see cref="P:System.Data.DataColumn.Expression" /> property. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003CA RID: 970 RVA: 0x0001302C File Offset: 0x0001122C
		public DataColumn Add(string columnName, Type type, string expression)
		{
			DataColumn dataColumn = new DataColumn(columnName, type, expression);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name and type to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="columnName">The <see cref="P:System.Data.DataColumn.ColumnName" /> to use when you create the column. </param>
		/// <param name="type">The <see cref="P:System.Data.DataColumn.DataType" /> of the new column. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003CB RID: 971 RVA: 0x0001304C File Offset: 0x0001124C
		public DataColumn Add(string columnName, Type type)
		{
			DataColumn dataColumn = new DataColumn(columnName, type);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003CC RID: 972 RVA: 0x0001306C File Offset: 0x0001126C
		public DataColumn Add(string columnName)
		{
			DataColumn dataColumn = new DataColumn(columnName);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003CD RID: 973 RVA: 0x00013088 File Offset: 0x00011288
		public DataColumn Add()
		{
			DataColumn dataColumn = new DataColumn();
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Occurs when the columns collection changes, either by adding or removing a column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060003CE RID: 974 RVA: 0x000130A4 File Offset: 0x000112A4
		// (remove) Token: 0x060003CF RID: 975 RVA: 0x000130DC File Offset: 0x000112DC
		public event CollectionChangeEventHandler CollectionChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060003D0 RID: 976 RVA: 0x00013114 File Offset: 0x00011314
		// (remove) Token: 0x060003D1 RID: 977 RVA: 0x0001314C File Offset: 0x0001134C
		internal event CollectionChangeEventHandler CollectionChanging;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060003D2 RID: 978 RVA: 0x00013184 File Offset: 0x00011384
		// (remove) Token: 0x060003D3 RID: 979 RVA: 0x000131BC File Offset: 0x000113BC
		internal event CollectionChangeEventHandler ColumnPropertyChanged;

		// Token: 0x060003D4 RID: 980 RVA: 0x000131F1 File Offset: 0x000113F1
		private void ArrayAdd(DataColumn column)
		{
			this._list.Add(column);
			column.SetOrdinalInternal(this._list.Count - 1);
			this.CheckIChangeTracking(column);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001321A File Offset: 0x0001141A
		private void ArrayAdd(int index, DataColumn column)
		{
			this._list.Insert(index, column);
			this.CheckIChangeTracking(column);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013230 File Offset: 0x00011430
		private void ArrayRemove(DataColumn column)
		{
			column.SetOrdinalInternal(-1);
			this._list.Remove(column);
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				((DataColumn)this._list[i]).SetOrdinalInternal(i);
			}
			if (column.ImplementsIChangeTracking)
			{
				this.RemoveColumnsImplementingIChangeTrackingList(column);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00013290 File Offset: 0x00011490
		internal string AssignName()
		{
			int num = this._defaultNameIndex;
			this._defaultNameIndex = num + 1;
			string text = this.MakeName(num);
			while (this._columnFromName.ContainsKey(text))
			{
				num = this._defaultNameIndex;
				this._defaultNameIndex = num + 1;
				text = this.MakeName(num);
			}
			return text;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000132E0 File Offset: 0x000114E0
		private void BaseAdd(DataColumn column)
		{
			if (column == null)
			{
				throw ExceptionBuilder.ArgumentNull("column");
			}
			if (column._table == this._table)
			{
				throw ExceptionBuilder.CannotAddColumn1(column.ColumnName);
			}
			if (column._table != null)
			{
				throw ExceptionBuilder.CannotAddColumn2(column.ColumnName);
			}
			if (column.ColumnName.Length == 0)
			{
				column.ColumnName = this.AssignName();
			}
			this.RegisterColumnName(column.ColumnName, column);
			try
			{
				column.SetTable(this._table);
				if (!this._table.fInitInProgress && column.Computed && column.DataExpression.DependsOn(column))
				{
					throw ExceptionBuilder.ExpressionCircular();
				}
				if (0 < this._table.RecordCapacity)
				{
					column.SetCapacity(this._table.RecordCapacity);
				}
				for (int i = 0; i < this._table.RecordCapacity; i++)
				{
					column.InitializeRecord(i);
				}
				if (this._table.DataSet != null)
				{
					column.OnSetDataSet();
				}
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				this.UnregisterName(column.ColumnName);
				throw;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001340C File Offset: 0x0001160C
		private void BaseGroupSwitch(DataColumn[] oldArray, int oldLength, DataColumn[] newArray, int newLength)
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
				if (!flag && oldArray[i].Table == this._table)
				{
					this.BaseRemove(oldArray[i]);
					this._list.Remove(oldArray[i]);
					oldArray[i].SetOrdinalInternal(-1);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (newArray[k].Table != this._table)
				{
					this.BaseAdd(newArray[k]);
					this._list.Add(newArray[k]);
				}
				newArray[k].SetOrdinalInternal(k);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000134C4 File Offset: 0x000116C4
		private void BaseRemove(DataColumn column)
		{
			if (this.CanRemove(column, true))
			{
				if (column._errors > 0)
				{
					for (int i = 0; i < this._table.Rows.Count; i++)
					{
						this._table.Rows[i].ClearError(column);
					}
				}
				this.UnregisterName(column.ColumnName);
				column.SetTable(null);
			}
		}

		/// <summary>Checks whether a specific column can be removed from the collection.</summary>
		/// <returns>true if the column can be removed. false if,The <paramref name="column" /> parameter is null.The column does not belong to this collection.The column is part of a relationship.Another column's expression depends on this column.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> in the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003DB RID: 987 RVA: 0x00013529 File Offset: 0x00011729
		public bool CanRemove(DataColumn column)
		{
			return this.CanRemove(column, false);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00013534 File Offset: 0x00011734
		internal bool CanRemove(DataColumn column, bool fThrowException)
		{
			if (column == null)
			{
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.ArgumentNull("column");
			}
			else if (column._table != this._table)
			{
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.CannotRemoveColumn();
			}
			else
			{
				this._table.OnRemoveColumnInternal(column);
				if (this._table._primaryKey == null || !this._table._primaryKey.Key.ContainsColumn(column))
				{
					int i = 0;
					while (i < this._table.ParentRelations.Count)
					{
						if (this._table.ParentRelations[i].ChildKey.ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveChildKey(this._table.ParentRelations[i].RelationName);
						}
						else
						{
							i++;
						}
					}
					int j = 0;
					while (j < this._table.ChildRelations.Count)
					{
						if (this._table.ChildRelations[j].ParentKey.ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveChildKey(this._table.ChildRelations[j].RelationName);
						}
						else
						{
							j++;
						}
					}
					int k = 0;
					while (k < this._table.Constraints.Count)
					{
						if (this._table.Constraints[k].ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveConstraint(this._table.Constraints[k].ConstraintName, this._table.Constraints[k].Table.TableName);
						}
						else
						{
							k++;
						}
					}
					if (this._table.DataSet != null)
					{
						ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this._table.DataSet, this._table);
						while (parentForeignKeyConstraintEnumerator.GetNext())
						{
							Constraint constraint = parentForeignKeyConstraintEnumerator.GetConstraint();
							if (((ForeignKeyConstraint)constraint).ParentKey.ContainsColumn(column))
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.CannotRemoveConstraint(constraint.ConstraintName, constraint.Table.TableName);
							}
						}
					}
					if (column._dependentColumns != null)
					{
						for (int l = 0; l < column._dependentColumns.Count; l++)
						{
							DataColumn dataColumn = column._dependentColumns[l];
							if ((!this._fInClear || (dataColumn.Table != this._table && dataColumn.Table != null)) && dataColumn.Table != null)
							{
								DataExpression dataExpression = dataColumn.DataExpression;
								if (dataExpression != null && dataExpression.DependsOn(column))
								{
									if (!fThrowException)
									{
										return false;
									}
									throw ExceptionBuilder.CannotRemoveExpression(dataColumn.ColumnName, dataColumn.Expression);
								}
							}
						}
					}
					foreach (Index index in this._table.LiveIndexes)
					{
					}
					return true;
				}
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.CannotRemovePrimaryKey();
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00013818 File Offset: 0x00011A18
		private void CheckIChangeTracking(DataColumn column)
		{
			if (column.ImplementsIRevertibleChangeTracking)
			{
				this._nColumnsImplementingIRevertibleChangeTracking++;
				this._nColumnsImplementingIChangeTracking++;
				this.AddColumnsImplementingIChangeTrackingList(column);
				return;
			}
			if (column.ImplementsIChangeTracking)
			{
				this._nColumnsImplementingIChangeTracking++;
				this.AddColumnsImplementingIChangeTrackingList(column);
			}
		}

		/// <summary>Clears the collection of any columns.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003DE RID: 990 RVA: 0x00013870 File Offset: 0x00011A70
		public void Clear()
		{
			int count = this._list.Count;
			DataColumn[] array = new DataColumn[this._list.Count];
			this._list.CopyTo(array, 0);
			this.OnCollectionChanging(InternalDataCollectionBase.s_refreshEventArgs);
			if (this._table.fInitInProgress && this._delayedAddRangeColumns != null)
			{
				this._delayedAddRangeColumns = null;
			}
			try
			{
				this._fInClear = true;
				this.BaseGroupSwitch(array, count, null, 0);
				this._fInClear = false;
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				this._fInClear = false;
				this.BaseGroupSwitch(null, 0, array, count);
				this._list.Clear();
				for (int i = 0; i < count; i++)
				{
					this._list.Add(array[i]);
				}
				throw;
			}
			this._list.Clear();
			this._table.ElementColumnCount = 0;
			this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
		}

		/// <summary>Checks whether the collection contains a column with the specified name.</summary>
		/// <returns>true if a column exists with this name; otherwise, false.</returns>
		/// <param name="name">The <see cref="P:System.Data.DataColumn.ColumnName" /> of the column to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003DF RID: 991 RVA: 0x0001396C File Offset: 0x00011B6C
		public bool Contains(string name)
		{
			DataColumn dataColumn;
			return (this._columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null) || this.IndexOfCaseInsensitive(name) >= 0;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001399C File Offset: 0x00011B9C
		internal bool Contains(string name, bool caseSensitive)
		{
			DataColumn dataColumn;
			return (this._columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null) || (!caseSensitive && this.IndexOfCaseInsensitive(name) >= 0);
		}

		/// <summary>Copies the entire collection into an existing array, starting at a specified index within the array.</summary>
		/// <param name="array">An array of <see cref="T:System.Data.DataColumn" /> objects to copy the collection into. </param>
		/// <param name="index">The index to start from.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003E1 RID: 993 RVA: 0x000139D0 File Offset: 0x00011BD0
		public void CopyTo(DataColumn[] array, int index)
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
				array[index + i] = (DataColumn)this._list[i];
			}
		}

		/// <summary>Gets the index of a column specified by name.</summary>
		/// <returns>The index of the column specified by <paramref name="column" /> if it is found; otherwise, -1.</returns>
		/// <param name="column">The name of the column to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E2 RID: 994 RVA: 0x00013A40 File Offset: 0x00011C40
		public int IndexOf(DataColumn column)
		{
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				if (column == (DataColumn)this._list[i])
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the index of the column with the specific name (the name is not case sensitive).</summary>
		/// <returns>The zero-based index of the column with the specified name, or -1 if the column does not exist in the collection.</returns>
		/// <param name="columnName">The name of the column to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003E3 RID: 995 RVA: 0x00013A7C File Offset: 0x00011C7C
		public int IndexOf(string columnName)
		{
			if (columnName != null && 0 < columnName.Length)
			{
				int count = this.Count;
				DataColumn dataColumn;
				if (this._columnFromName.TryGetValue(columnName, out dataColumn) && dataColumn != null)
				{
					for (int i = 0; i < count; i++)
					{
						if (dataColumn == this._list[i])
						{
							return i;
						}
					}
				}
				else
				{
					int num = this.IndexOfCaseInsensitive(columnName);
					if (num >= 0)
					{
						return num;
					}
					return -1;
				}
			}
			return -1;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00013AE0 File Offset: 0x00011CE0
		internal int IndexOfCaseInsensitive(string name)
		{
			int specialHashCode = this._table.GetSpecialHashCode(name);
			int num = -1;
			for (int i = 0; i < this.Count; i++)
			{
				DataColumn dataColumn = (DataColumn)this._list[i];
				if ((specialHashCode == 0 || dataColumn._hashCode == 0 || dataColumn._hashCode == specialHashCode) && base.NamesEqual(dataColumn.ColumnName, name, false, this._table.Locale) != 0)
				{
					if (num != -1)
					{
						return -2;
					}
					num = i;
				}
			}
			return num;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00013B5C File Offset: 0x00011D5C
		internal void FinishInitCollection()
		{
			if (this._delayedAddRangeColumns != null)
			{
				foreach (DataColumn dataColumn in this._delayedAddRangeColumns)
				{
					if (dataColumn != null)
					{
						this.Add(dataColumn);
					}
				}
				foreach (DataColumn dataColumn2 in this._delayedAddRangeColumns)
				{
					if (dataColumn2 != null)
					{
						dataColumn2.FinishInitInProgress();
					}
				}
				this._delayedAddRangeColumns = null;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00013BBD File Offset: 0x00011DBD
		private string MakeName(int index)
		{
			if (index != 1)
			{
				return "Column" + index.ToString(CultureInfo.InvariantCulture);
			}
			return "Column1";
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013BE0 File Offset: 0x00011DE0
		internal void MoveTo(DataColumn column, int newPosition)
		{
			if (0 > newPosition || newPosition > this.Count - 1)
			{
				throw ExceptionBuilder.InvalidOrdinal("ordinal", newPosition);
			}
			if (column.ImplementsIChangeTracking)
			{
				this.RemoveColumnsImplementingIChangeTrackingList(column);
			}
			this._list.Remove(column);
			this._list.Insert(newPosition, column);
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				((DataColumn)this._list[i]).SetOrdinalInternal(i);
			}
			this.CheckIChangeTracking(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, column));
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00013C74 File Offset: 0x00011E74
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			this._table.UpdatePropertyDescriptorCollectionCache();
			if (ccevent != null && !this._table.SchemaLoading && !this._table.fInitInProgress)
			{
				DataColumn dataColumn = (DataColumn)ccevent.Element;
			}
			CollectionChangeEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged(this, ccevent);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00013CC7 File Offset: 0x00011EC7
		private void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			CollectionChangeEventHandler collectionChanging = this.CollectionChanging;
			if (collectionChanging == null)
			{
				return;
			}
			collectionChanging(this, ccevent);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00013CDB File Offset: 0x00011EDB
		internal void OnColumnPropertyChanged(CollectionChangeEventArgs ccevent)
		{
			this._table.UpdatePropertyDescriptorCollectionCache();
			CollectionChangeEventHandler columnPropertyChanged = this.ColumnPropertyChanged;
			if (columnPropertyChanged == null)
			{
				return;
			}
			columnPropertyChanged(this, ccevent);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00013CFC File Offset: 0x00011EFC
		internal void RegisterColumnName(string name, DataColumn column)
		{
			try
			{
				this._columnFromName.Add(name, column);
				if (column != null)
				{
					column._hashCode = this._table.GetSpecialHashCode(name);
				}
			}
			catch (ArgumentException)
			{
				if (this._columnFromName[name] == null)
				{
					throw ExceptionBuilder.CannotAddDuplicate2(name);
				}
				if (column != null)
				{
					throw ExceptionBuilder.CannotAddDuplicate(name);
				}
				throw ExceptionBuilder.CannotAddDuplicate3(name);
			}
			if (column == null && base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, this._table.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex++;
				}
				while (this.Contains(this.MakeName(this._defaultNameIndex)));
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013DAC File Offset: 0x00011FAC
		internal bool CanRegisterName(string name)
		{
			return !this._columnFromName.ContainsKey(name);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataColumn" /> object from the collection.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The column does not belong to this collection.-Or- The column is part of a relationship.-Or- Another column's expression depends on this column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003ED RID: 1005 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public void Remove(DataColumn column)
		{
			this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
			this.BaseRemove(column);
			this.ArrayRemove(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
			if (column.ColumnMapping == MappingType.Element)
			{
				DataTable table = this._table;
				int elementColumnCount = table.ElementColumnCount;
				table.ElementColumnCount = elementColumnCount - 1;
			}
		}

		/// <summary>Removes the column at the specified index from the collection.</summary>
		/// <param name="index">The index of the column to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a column at the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003EE RID: 1006 RVA: 0x00013E14 File Offset: 0x00012014
		public void RemoveAt(int index)
		{
			DataColumn dataColumn = this[index];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnOutOfRange(index);
			}
			this.Remove(dataColumn);
		}

		/// <summary>Removes the <see cref="T:System.Data.DataColumn" /> object that has the specified name from the collection.</summary>
		/// <param name="name">The name of the column to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a column with the specified name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003EF RID: 1007 RVA: 0x00013E3C File Offset: 0x0001203C
		public void Remove(string name)
		{
			DataColumn dataColumn = this[name];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnNotInTheTable(name, this._table.TableName);
			}
			this.Remove(dataColumn);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00013E70 File Offset: 0x00012070
		internal void UnregisterName(string name)
		{
			this._columnFromName.Remove(name);
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this._table.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00013EDC File Offset: 0x000120DC
		private void AddColumnsImplementingIChangeTrackingList(DataColumn dataColumn)
		{
			DataColumn[] columnsImplementingIChangeTracking = this._columnsImplementingIChangeTracking;
			DataColumn[] array = new DataColumn[columnsImplementingIChangeTracking.Length + 1];
			columnsImplementingIChangeTracking.CopyTo(array, 0);
			array[columnsImplementingIChangeTracking.Length] = dataColumn;
			this._columnsImplementingIChangeTracking = array;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013F10 File Offset: 0x00012110
		private void RemoveColumnsImplementingIChangeTrackingList(DataColumn dataColumn)
		{
			DataColumn[] columnsImplementingIChangeTracking = this._columnsImplementingIChangeTracking;
			DataColumn[] array = new DataColumn[columnsImplementingIChangeTracking.Length - 1];
			int i = 0;
			int num = 0;
			while (i < columnsImplementingIChangeTracking.Length)
			{
				if (columnsImplementingIChangeTracking[i] != dataColumn)
				{
					array[num++] = columnsImplementingIChangeTracking[i];
				}
				i++;
			}
			this._columnsImplementingIChangeTracking = array;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010468 File Offset: 0x0000E668
		internal DataColumnCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000543 RID: 1347
		private readonly DataTable _table;

		// Token: 0x04000544 RID: 1348
		private readonly ArrayList _list;

		// Token: 0x04000545 RID: 1349
		private int _defaultNameIndex;

		// Token: 0x04000546 RID: 1350
		private DataColumn[] _delayedAddRangeColumns;

		// Token: 0x04000547 RID: 1351
		private readonly Dictionary<string, DataColumn> _columnFromName;

		// Token: 0x04000548 RID: 1352
		private bool _fInClear;

		// Token: 0x04000549 RID: 1353
		private DataColumn[] _columnsImplementingIChangeTracking;

		// Token: 0x0400054A RID: 1354
		private int _nColumnsImplementingIChangeTracking;

		// Token: 0x0400054B RID: 1355
		private int _nColumnsImplementingIRevertibleChangeTracking;
	}
}
