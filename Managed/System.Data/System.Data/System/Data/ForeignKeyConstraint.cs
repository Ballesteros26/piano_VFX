using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	/// <summary>Represents an action restriction enforced on a set of columns in a primary key/foreign key relationship when a value or row is either deleted or updated.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000C1 RID: 193
	[DefaultProperty("ConstraintName")]
	public class ForeignKeyConstraint : Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000B1D RID: 2845 RVA: 0x00034018 File Offset: 0x00032218
		public ForeignKeyConstraint(DataColumn parentColumn, DataColumn childColumn)
			: this(null, parentColumn, childColumn)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified name, parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="constraintName">The name of the constraint. </param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000B1E RID: 2846 RVA: 0x00034024 File Offset: 0x00032224
		public ForeignKeyConstraint(string constraintName, DataColumn parentColumn, DataColumn childColumn)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			DataColumn[] array = new DataColumn[] { parentColumn };
			DataColumn[] array2 = new DataColumn[] { childColumn };
			this.Create(constraintName, array, array2);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000B1F RID: 2847 RVA: 0x00034064 File Offset: 0x00032264
		public ForeignKeyConstraint(DataColumn[] parentColumns, DataColumn[] childColumns)
			: this(null, parentColumns, childColumns)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified name, and arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="constraintName">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. If null or empty string, a default name will be given when added to the constraints collection. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000B20 RID: 2848 RVA: 0x0003406F File Offset: 0x0003226F
		public ForeignKeyConstraint(string constraintName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			this.Create(constraintName, parentColumns, childColumns);
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio  environment. <see cref="T:System.Data.ForeignKeyConstraint" /> objects created by using this constructor must then be added to the collection via <see cref="M:System.Data.ConstraintCollection.AddRange(System.Data.Constraint[])" />. Tables and columns with the specified names must exist at the time the method is called, or if <see cref="M:System.Data.DataTable.BeginInit" /> has been called prior to calling this constructor, the tables and columns with the specified names must exist at the time that <see cref="M:System.Data.DataTable.EndInit" /> is called.</summary>
		/// <param name="constraintName">The name of the constraint. </param>
		/// <param name="parentTableName">The name of the parent <see cref="T:System.Data.DataTable" /> that contains parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="parentColumnNames">An array of the names of parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="childColumnNames">An array of the names of child <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="acceptRejectRule">One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include None, Cascade, and Default. </param>
		/// <param name="deleteRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is deleted. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <param name="updateRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is updated. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000B21 RID: 2849 RVA: 0x00034090 File Offset: 0x00032290
		[Browsable(false)]
		public ForeignKeyConstraint(string constraintName, string parentTableName, string[] parentColumnNames, string[] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			this._constraintName = constraintName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._acceptRejectRule = acceptRejectRule;
			this._deleteRule = deleteRule;
			this._updateRule = updateRule;
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio  environment. <see cref="T:System.Data.ForeignKeyConstraint" /> objects created by using this constructor must then be added to the collection via <see cref="M:System.Data.ConstraintCollection.AddRange(System.Data.Constraint[])" />. Tables and columns with the specified names must exist at the time the method is called, or if <see cref="M:System.Data.DataTable.BeginInit" /> has been called prior to calling this constructor, the tables and columns with the specified names must exist at the time that <see cref="M:System.Data.DataTable.EndInit" /> is called.</summary>
		/// <param name="constraintName">The name of the constraint. </param>
		/// <param name="parentTableName">The name of the parent <see cref="T:System.Data.DataTable" /> that contains parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="parentTableNamespace">The name of the <see cref="P:System.Data.DataTable.Namespace" />. </param>
		/// <param name="parentColumnNames">An array of the names of parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="childColumnNames">An array of the names of child <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="acceptRejectRule">One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include None, Cascade, and Default. </param>
		/// <param name="deleteRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is deleted. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <param name="updateRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is updated. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000B22 RID: 2850 RVA: 0x000340E8 File Offset: 0x000322E8
		[Browsable(false)]
		public ForeignKeyConstraint(string constraintName, string parentTableName, string parentTableNamespace, string[] parentColumnNames, string[] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			this._constraintName = constraintName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._parentTableNamespace = parentTableNamespace;
			this._acceptRejectRule = acceptRejectRule;
			this._deleteRule = deleteRule;
			this._updateRule = updateRule;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00034146 File Offset: 0x00032346
		internal DataKey ChildKey
		{
			get
			{
				base.CheckStateForProperty();
				return this._childKey;
			}
		}

		/// <summary>Gets the child columns of this constraint.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the child columns of the constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00034154 File Offset: 0x00032354
		[ReadOnly(true)]
		public virtual DataColumn[] Columns
		{
			get
			{
				base.CheckStateForProperty();
				return this._childKey.ToArray();
			}
		}

		/// <summary>Gets the child table of this constraint.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the child table in the constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00034167 File Offset: 0x00032367
		[ReadOnly(true)]
		public override DataTable Table
		{
			get
			{
				base.CheckStateForProperty();
				return this._childKey.Table;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0003417A File Offset: 0x0003237A
		internal string[] ParentColumnNames
		{
			get
			{
				return this._parentKey.GetColumnNames();
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00034187 File Offset: 0x00032387
		internal string[] ChildColumnNames
		{
			get
			{
				return this._childKey.GetColumnNames();
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00034194 File Offset: 0x00032394
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
			if (this.Table != constraints.Table)
			{
				throw ExceptionBuilder.ConstraintAddFailed(constraints.Table);
			}
			if (this.Table.Locale.LCID != this.RelatedTable.Locale.LCID || this.Table.CaseSensitive != this.RelatedTable.CaseSensitive)
			{
				throw ExceptionBuilder.CaseLocaleMismatch();
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0000EF2B File Offset: 0x0000D12B
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			return true;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000341FC File Offset: 0x000323FC
		internal bool IsKeyNull(object[] values)
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

		// Token: 0x06000B2B RID: 2859 RVA: 0x00034224 File Offset: 0x00032424
		internal override bool IsConstraintViolated()
		{
			Index sortIndex = this._childKey.GetSortIndex();
			object[] uniqueKeyValues = sortIndex.GetUniqueKeyValues();
			bool flag = false;
			Index sortIndex2 = this._parentKey.GetSortIndex();
			foreach (object[] array in uniqueKeyValues)
			{
				if (!this.IsKeyNull(array) && !sortIndex2.IsKeyInIndex(array))
				{
					DataRow[] rows = sortIndex.GetRows(sortIndex.FindRecords(array));
					string text = SR.Format("ForeignKeyConstraint {0} requires the child key values ({1}) to exist in the parent table.", this.ConstraintName, ExceptionBuilder.KeysToString(array));
					for (int j = 0; j < rows.Length; j++)
					{
						rows[j].RowError = text;
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000342D0 File Offset: 0x000324D0
		internal override bool CanEnableConstraint()
		{
			if (this.Table.DataSet == null || !this.Table.DataSet.EnforceConstraints)
			{
				return true;
			}
			object[] uniqueKeyValues = this._childKey.GetSortIndex().GetUniqueKeyValues();
			Index sortIndex = this._parentKey.GetSortIndex();
			foreach (object[] array in uniqueKeyValues)
			{
				if (!this.IsKeyNull(array) && !sortIndex.IsKeyInIndex(array))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00034348 File Offset: 0x00032548
		internal void CascadeCommit(DataRow row)
		{
			if (row.RowState == DataRowState.Detached)
			{
				return;
			}
			if (this._acceptRejectRule == AcceptRejectRule.Cascade)
			{
				Index sortIndex = this._childKey.GetSortIndex((row.RowState == DataRowState.Deleted) ? DataViewRowState.Deleted : DataViewRowState.CurrentRows);
				object[] keyValues = row.GetKeyValues(this._parentKey, (row.RowState == DataRowState.Deleted) ? DataRowVersion.Original : DataRowVersion.Default);
				if (this.IsKeyNull(keyValues))
				{
					return;
				}
				Range range = sortIndex.FindRecords(keyValues);
				if (!range.IsNull)
				{
					foreach (DataRow dataRow in sortIndex.GetRows(range))
					{
						if (DataRowState.Detached != dataRow.RowState && !dataRow._inCascade)
						{
							dataRow.AcceptChanges();
						}
					}
				}
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000343FC File Offset: 0x000325FC
		internal void CascadeDelete(DataRow row)
		{
			if (-1 == row._newRecord)
			{
				return;
			}
			object[] keyValues = row.GetKeyValues(this._parentKey, DataRowVersion.Current);
			if (this.IsKeyNull(keyValues))
			{
				return;
			}
			Index sortIndex = this._childKey.GetSortIndex();
			switch (this.DeleteRule)
			{
			case Rule.None:
				if (row.Table.DataSet.EnforceConstraints)
				{
					Range range = sortIndex.FindRecords(keyValues);
					if (!range.IsNull)
					{
						if (range.Count == 1 && sortIndex.GetRow(range.Min) == row)
						{
							return;
						}
						throw ExceptionBuilder.FailedCascadeDelete(this.ConstraintName);
					}
				}
				break;
			case Rule.Cascade:
			{
				object[] keyValues2 = row.GetKeyValues(this._parentKey, DataRowVersion.Default);
				Range range2 = sortIndex.FindRecords(keyValues2);
				if (!range2.IsNull)
				{
					foreach (DataRow dataRow in sortIndex.GetRows(range2))
					{
						if (!dataRow._inCascade)
						{
							dataRow.Table.DeleteRow(dataRow);
						}
					}
					return;
				}
				break;
			}
			case Rule.SetNull:
			{
				object[] array = new object[this._childKey.ColumnsReference.Length];
				for (int j = 0; j < this._childKey.ColumnsReference.Length; j++)
				{
					array[j] = DBNull.Value;
				}
				Range range3 = sortIndex.FindRecords(keyValues);
				if (!range3.IsNull)
				{
					DataRow[] rows2 = sortIndex.GetRows(range3);
					for (int k = 0; k < rows2.Length; k++)
					{
						if (row != rows2[k])
						{
							rows2[k].SetKeyValues(this._childKey, array);
						}
					}
					return;
				}
				break;
			}
			case Rule.SetDefault:
			{
				object[] array2 = new object[this._childKey.ColumnsReference.Length];
				for (int l = 0; l < this._childKey.ColumnsReference.Length; l++)
				{
					array2[l] = this._childKey.ColumnsReference[l].DefaultValue;
				}
				Range range4 = sortIndex.FindRecords(keyValues);
				if (!range4.IsNull)
				{
					DataRow[] rows3 = sortIndex.GetRows(range4);
					for (int m = 0; m < rows3.Length; m++)
					{
						if (row != rows3[m])
						{
							rows3[m].SetKeyValues(this._childKey, array2);
						}
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00034628 File Offset: 0x00032828
		internal void CascadeRollback(DataRow row)
		{
			Index sortIndex = this._childKey.GetSortIndex((row.RowState == DataRowState.Deleted) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			object[] keyValues = row.GetKeyValues(this._parentKey, (row.RowState == DataRowState.Modified) ? DataRowVersion.Current : DataRowVersion.Default);
			if (this.IsKeyNull(keyValues))
			{
				return;
			}
			Range range = sortIndex.FindRecords(keyValues);
			if (this._acceptRejectRule == AcceptRejectRule.Cascade)
			{
				if (!range.IsNull)
				{
					DataRow[] rows = sortIndex.GetRows(range);
					for (int i = 0; i < rows.Length; i++)
					{
						if (!rows[i]._inCascade)
						{
							rows[i].RejectChanges();
						}
					}
					return;
				}
			}
			else if (row.RowState != DataRowState.Deleted && row.Table.DataSet.EnforceConstraints && !range.IsNull)
			{
				if (range.Count == 1 && sortIndex.GetRow(range.Min) == row)
				{
					return;
				}
				if (row.HasKeyChanged(this._parentKey))
				{
					throw ExceptionBuilder.FailedCascadeUpdate(this.ConstraintName);
				}
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00034724 File Offset: 0x00032924
		internal void CascadeUpdate(DataRow row)
		{
			if (-1 == row._newRecord)
			{
				return;
			}
			object[] keyValues = row.GetKeyValues(this._parentKey, DataRowVersion.Current);
			if (!this.Table.DataSet._fInReadXml && this.IsKeyNull(keyValues))
			{
				return;
			}
			Index sortIndex = this._childKey.GetSortIndex();
			switch (this.UpdateRule)
			{
			case Rule.None:
				if (row.Table.DataSet.EnforceConstraints && !sortIndex.FindRecords(keyValues).IsNull)
				{
					throw ExceptionBuilder.FailedCascadeUpdate(this.ConstraintName);
				}
				break;
			case Rule.Cascade:
			{
				Range range = sortIndex.FindRecords(keyValues);
				if (!range.IsNull)
				{
					object[] keyValues2 = row.GetKeyValues(this._parentKey, DataRowVersion.Proposed);
					DataRow[] rows = sortIndex.GetRows(range);
					for (int i = 0; i < rows.Length; i++)
					{
						rows[i].SetKeyValues(this._childKey, keyValues2);
					}
					return;
				}
				break;
			}
			case Rule.SetNull:
			{
				object[] array = new object[this._childKey.ColumnsReference.Length];
				for (int j = 0; j < this._childKey.ColumnsReference.Length; j++)
				{
					array[j] = DBNull.Value;
				}
				Range range2 = sortIndex.FindRecords(keyValues);
				if (!range2.IsNull)
				{
					DataRow[] rows2 = sortIndex.GetRows(range2);
					for (int k = 0; k < rows2.Length; k++)
					{
						rows2[k].SetKeyValues(this._childKey, array);
					}
					return;
				}
				break;
			}
			case Rule.SetDefault:
			{
				object[] array2 = new object[this._childKey.ColumnsReference.Length];
				for (int l = 0; l < this._childKey.ColumnsReference.Length; l++)
				{
					array2[l] = this._childKey.ColumnsReference[l].DefaultValue;
				}
				Range range3 = sortIndex.FindRecords(keyValues);
				if (!range3.IsNull)
				{
					DataRow[] rows3 = sortIndex.GetRows(range3);
					for (int m = 0; m < rows3.Length; m++)
					{
						rows3[m].SetKeyValues(this._childKey, array2);
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00034928 File Offset: 0x00032B28
		internal void CheckCanClearParentTable(DataTable table)
		{
			if (this.Table.DataSet.EnforceConstraints && this.Table.Rows.Count > 0)
			{
				throw ExceptionBuilder.FailedClearParentTable(table.TableName, this.ConstraintName, this.Table.TableName);
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00034977 File Offset: 0x00032B77
		internal void CheckCanRemoveParentRow(DataRow row)
		{
			if (!this.Table.DataSet.EnforceConstraints)
			{
				return;
			}
			if (DataRelation.GetChildRows(this.ParentKey, this.ChildKey, row, DataRowVersion.Default).Length != 0)
			{
				throw ExceptionBuilder.RemoveParentRow(this);
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000349B0 File Offset: 0x00032BB0
		internal void CheckCascade(DataRow row, DataRowAction action)
		{
			if (row._inCascade)
			{
				return;
			}
			row._inCascade = true;
			try
			{
				if (action == DataRowAction.Change)
				{
					if (row.HasKeyChanged(this._parentKey))
					{
						this.CascadeUpdate(row);
					}
				}
				else if (action == DataRowAction.Delete)
				{
					this.CascadeDelete(row);
				}
				else if (action == DataRowAction.Commit)
				{
					this.CascadeCommit(row);
				}
				else if (action == DataRowAction.Rollback)
				{
					this.CascadeRollback(row);
				}
			}
			finally
			{
				row._inCascade = false;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00034A30 File Offset: 0x00032C30
		internal override void CheckConstraint(DataRow childRow, DataRowAction action)
		{
			if ((action == DataRowAction.Change || action == DataRowAction.Add || action == DataRowAction.Rollback) && this.Table.DataSet != null && this.Table.DataSet.EnforceConstraints && childRow.HasKeyChanged(this._childKey))
			{
				DataRowVersion dataRowVersion = ((action == DataRowAction.Rollback) ? DataRowVersion.Original : DataRowVersion.Current);
				object[] keyValues = childRow.GetKeyValues(this._childKey);
				if (childRow.HasVersion(dataRowVersion))
				{
					DataRow parentRow = DataRelation.GetParentRow(this.ParentKey, this.ChildKey, childRow, dataRowVersion);
					if (parentRow != null && parentRow._inCascade)
					{
						object[] keyValues2 = parentRow.GetKeyValues(this._parentKey, (action == DataRowAction.Rollback) ? dataRowVersion : DataRowVersion.Default);
						int num = childRow.Table.NewRecord();
						childRow.Table.SetKeyValues(this._childKey, keyValues2, num);
						if (this._childKey.RecordsEqual(childRow._tempRecord, num))
						{
							return;
						}
					}
				}
				object[] keyValues3 = childRow.GetKeyValues(this._childKey);
				if (!this.IsKeyNull(keyValues3) && !this._parentKey.GetSortIndex().IsKeyInIndex(keyValues3))
				{
					if (this._childKey.Table == this._parentKey.Table && childRow._tempRecord != -1)
					{
						int i;
						for (i = 0; i < keyValues3.Length; i++)
						{
							DataColumn dataColumn = this._parentKey.ColumnsReference[i];
							object obj = dataColumn.ConvertValue(keyValues3[i]);
							if (dataColumn.CompareValueTo(childRow._tempRecord, obj) != 0)
							{
								break;
							}
						}
						if (i == keyValues3.Length)
						{
							return;
						}
					}
					throw ExceptionBuilder.ForeignKeyViolation(this.ConstraintName, keyValues);
				}
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00034BC0 File Offset: 0x00032DC0
		private void NonVirtualCheckState()
		{
			if (this._DataSet == null)
			{
				this._parentKey.CheckState();
				this._childKey.CheckState();
				if (this._parentKey.Table.DataSet != this._childKey.Table.DataSet)
				{
					throw ExceptionBuilder.TablesInDifferentSets();
				}
				for (int i = 0; i < this._parentKey.ColumnsReference.Length; i++)
				{
					if (this._parentKey.ColumnsReference[i].DataType != this._childKey.ColumnsReference[i].DataType || (this._parentKey.ColumnsReference[i].DataType == typeof(DateTime) && this._parentKey.ColumnsReference[i].DateTimeMode != this._childKey.ColumnsReference[i].DateTimeMode && (this._parentKey.ColumnsReference[i].DateTimeMode & this._childKey.ColumnsReference[i].DateTimeMode) != DataSetDateTime.Unspecified))
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
				}
				if (this._childKey.ColumnsEqual(this._parentKey))
				{
					throw ExceptionBuilder.KeyColumnsIdentical();
				}
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00034CEF File Offset: 0x00032EEF
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		/// <summary>Indicates the action that should take place across this constraint when <see cref="M:System.Data.DataTable.AcceptChanges" /> is invoked.</summary>
		/// <returns>One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include None, and Cascade. The default is None.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00034CF7 File Offset: 0x00032EF7
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x00034D05 File Offset: 0x00032F05
		[DefaultValue(AcceptRejectRule.None)]
		public virtual AcceptRejectRule AcceptRejectRule
		{
			get
			{
				base.CheckStateForProperty();
				return this._acceptRejectRule;
			}
			set
			{
				if (value <= AcceptRejectRule.Cascade)
				{
					this._acceptRejectRule = value;
					return;
				}
				throw ADP.InvalidAcceptRejectRule(value);
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00034D19 File Offset: 0x00032F19
		internal override bool ContainsColumn(DataColumn column)
		{
			return this._parentKey.ContainsColumn(column) || this._childKey.ContainsColumn(column);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00034D37 File Offset: 0x00032F37
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00034D44 File Offset: 0x00032F44
		internal override Constraint Clone(DataSet destination, bool ignorNSforTableLookup)
		{
			int num;
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.Table.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.Table.TableName, this.Table.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable = destination.Tables[num];
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.RelatedTable.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.RelatedTable.TableName, this.RelatedTable.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable2 = destination.Tables[num];
			int num2 = this.Columns.Length;
			DataColumn[] array = new DataColumn[num2];
			DataColumn[] array2 = new DataColumn[num2];
			for (int i = 0; i < num2; i++)
			{
				DataColumn dataColumn = this.Columns[i];
				num = dataTable.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array[i] = dataTable.Columns[num];
				dataColumn = this.RelatedColumnsReference[i];
				num = dataTable2.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array2[i] = dataTable2.Columns[num];
			}
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(this.ConstraintName, array2, array);
			foreignKeyConstraint.UpdateRule = this.UpdateRule;
			foreignKeyConstraint.DeleteRule = this.DeleteRule;
			foreignKeyConstraint.AcceptRejectRule = this.AcceptRejectRule;
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				foreignKeyConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return foreignKeyConstraint;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00034F2C File Offset: 0x0003312C
		internal ForeignKeyConstraint Clone(DataTable destination)
		{
			int num = this.Columns.Length;
			DataColumn[] array = new DataColumn[num];
			DataColumn[] array2 = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				DataColumn dataColumn = this.Columns[i];
				int num2 = destination.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array[i] = destination.Columns[num2];
				dataColumn = this.RelatedColumnsReference[i];
				num2 = destination.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array2[i] = destination.Columns[num2];
			}
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(this.ConstraintName, array2, array);
			foreignKeyConstraint.UpdateRule = this.UpdateRule;
			foreignKeyConstraint.DeleteRule = this.DeleteRule;
			foreignKeyConstraint.AcceptRejectRule = this.AcceptRejectRule;
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				foreignKeyConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return foreignKeyConstraint;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0003506C File Offset: 0x0003326C
		private void Create(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			if (parentColumns.Length == 0 || childColumns.Length == 0)
			{
				throw ExceptionBuilder.KeyLengthZero();
			}
			if (parentColumns.Length != childColumns.Length)
			{
				throw ExceptionBuilder.KeyLengthMismatch();
			}
			for (int i = 0; i < parentColumns.Length; i++)
			{
				if (parentColumns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(parentColumns[i]);
				}
				if (childColumns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(childColumns[i]);
				}
			}
			this._parentKey = new DataKey(parentColumns, true);
			this._childKey = new DataKey(childColumns, true);
			this.ConstraintName = relationName;
			this.NonVirtualCheckState();
		}

		/// <summary>Gets or sets the action that occurs across this constraint when a row is deleted.</summary>
		/// <returns>One of the <see cref="T:System.Data.Rule" /> values. The default is Cascade.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000350F0 File Offset: 0x000332F0
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x000350FE File Offset: 0x000332FE
		[DefaultValue(Rule.Cascade)]
		public virtual Rule DeleteRule
		{
			get
			{
				base.CheckStateForProperty();
				return this._deleteRule;
			}
			set
			{
				if (value <= Rule.SetDefault)
				{
					this._deleteRule = value;
					return;
				}
				throw ADP.InvalidRule(value);
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.ForeignKeyConstraint" /> is identical to the specified object.</summary>
		/// <returns>true, if the objects are identical; otherwise, false.</returns>
		/// <param name="key">The object to which this <see cref="T:System.Data.ForeignKeyConstraint" /> is compared. Two <see cref="T:System.Data.ForeignKeyConstraint" /> are equal if they constrain the same columns. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B40 RID: 2880 RVA: 0x00035114 File Offset: 0x00033314
		public override bool Equals(object key)
		{
			if (!(key is ForeignKeyConstraint))
			{
				return false;
			}
			ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)key;
			return this.ParentKey.ColumnsEqual(foreignKeyConstraint.ParentKey) && this.ChildKey.ColumnsEqual(foreignKeyConstraint.ChildKey);
		}

		/// <summary>Gets the hash code of this instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B41 RID: 2881 RVA: 0x0003515E File Offset: 0x0003335E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>The parent columns of this constraint.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of the constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00035166 File Offset: 0x00033366
		[ReadOnly(true)]
		public virtual DataColumn[] RelatedColumns
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey.ToArray();
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00035179 File Offset: 0x00033379
		internal DataColumn[] RelatedColumnsReference
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey.ColumnsReference;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0003518C File Offset: 0x0003338C
		internal DataKey ParentKey
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey;
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0003519C File Offset: 0x0003339C
		internal DataRelation FindParentRelation()
		{
			DataRelationCollection parentRelations = this.Table.ParentRelations;
			for (int i = 0; i < parentRelations.Count; i++)
			{
				if (parentRelations[i].ChildKeyConstraint == this)
				{
					return parentRelations[i];
				}
			}
			return null;
		}

		/// <summary>Gets the parent table of this constraint.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataTable" /> of this constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x000351DE File Offset: 0x000333DE
		[ReadOnly(true)]
		public virtual DataTable RelatedTable
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey.Table;
			}
		}

		/// <summary>Gets or sets the action that occurs across this constraint on when a row is updated.</summary>
		/// <returns>One of the <see cref="T:System.Data.Rule" /> values. The default is Cascade.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x000351F1 File Offset: 0x000333F1
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x000351FF File Offset: 0x000333FF
		[DefaultValue(Rule.Cascade)]
		public virtual Rule UpdateRule
		{
			get
			{
				base.CheckStateForProperty();
				return this._updateRule;
			}
			set
			{
				if (value <= Rule.SetDefault)
				{
					this._updateRule = value;
					return;
				}
				throw ADP.InvalidRule(value);
			}
		}

		// Token: 0x040007BD RID: 1981
		internal const Rule Rule_Default = Rule.Cascade;

		// Token: 0x040007BE RID: 1982
		internal const AcceptRejectRule AcceptRejectRule_Default = AcceptRejectRule.None;

		// Token: 0x040007BF RID: 1983
		internal Rule _deleteRule;

		// Token: 0x040007C0 RID: 1984
		internal Rule _updateRule;

		// Token: 0x040007C1 RID: 1985
		internal AcceptRejectRule _acceptRejectRule;

		// Token: 0x040007C2 RID: 1986
		private DataKey _childKey;

		// Token: 0x040007C3 RID: 1987
		private DataKey _parentKey;

		// Token: 0x040007C4 RID: 1988
		internal string _constraintName;

		// Token: 0x040007C5 RID: 1989
		internal string[] _parentColumnNames;

		// Token: 0x040007C6 RID: 1990
		internal string[] _childColumnNames;

		// Token: 0x040007C7 RID: 1991
		internal string _parentTableName;

		// Token: 0x040007C8 RID: 1992
		internal string _parentTableNamespace;
	}
}
