using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Data
{
	/// <summary>Represents a restriction on a set of columns in which all values must be unique.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000FD RID: 253
	[DefaultProperty("ConstraintName")]
	public class UniqueConstraint : Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name and <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		// Token: 0x06000CFD RID: 3325 RVA: 0x0003C27C File Offset: 0x0003A47C
		public UniqueConstraint(string name, DataColumn column)
		{
			this.Create(name, new DataColumn[] { column });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		// Token: 0x06000CFE RID: 3326 RVA: 0x0003C2A4 File Offset: 0x0003A4A4
		public UniqueConstraint(DataColumn column)
		{
			this.Create(null, new DataColumn[] { column });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name and array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		// Token: 0x06000CFF RID: 3327 RVA: 0x0003C2CA File Offset: 0x0003A4CA
		public UniqueConstraint(string name, DataColumn[] columns)
		{
			this.Create(name, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the given array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		// Token: 0x06000D00 RID: 3328 RVA: 0x0003C2DA File Offset: 0x0003A4DA
		public UniqueConstraint(DataColumn[] columns)
		{
			this.Create(null, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columnNames">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000D01 RID: 3329 RVA: 0x0003C2EA File Offset: 0x0003A4EA
		[Browsable(false)]
		public UniqueConstraint(string name, string[] columnNames, bool isPrimaryKey)
		{
			this._constraintName = name;
			this._columnNames = columnNames;
			this._bPrimaryKey = isPrimaryKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, the <see cref="T:System.Data.DataColumn" /> to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000D02 RID: 3330 RVA: 0x0003C308 File Offset: 0x0003A508
		public UniqueConstraint(string name, DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] array = new DataColumn[] { column };
			this._bPrimaryKey = isPrimaryKey;
			this.Create(name, array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the <see cref="T:System.Data.DataColumn" /> to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000D03 RID: 3331 RVA: 0x0003C338 File Offset: 0x0003A538
		public UniqueConstraint(DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] array = new DataColumn[] { column };
			this._bPrimaryKey = isPrimaryKey;
			this.Create(null, array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000D04 RID: 3332 RVA: 0x0003C365 File Offset: 0x0003A565
		public UniqueConstraint(string name, DataColumn[] columns, bool isPrimaryKey)
		{
			this._bPrimaryKey = isPrimaryKey;
			this.Create(name, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000D05 RID: 3333 RVA: 0x0003C37C File Offset: 0x0003A57C
		public UniqueConstraint(DataColumn[] columns, bool isPrimaryKey)
		{
			this._bPrimaryKey = isPrimaryKey;
			this.Create(null, columns);
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0003C393 File Offset: 0x0003A593
		internal string[] ColumnNames
		{
			get
			{
				return this._key.GetColumnNames();
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0003C3A0 File Offset: 0x0003A5A0
		internal Index ConstraintIndex
		{
			get
			{
				return this._constraintIndex;
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0003C3A8 File Offset: 0x0003A5A8
		[Conditional("DEBUG")]
		private void AssertConstraintAndKeyIndexes()
		{
			DataColumn[] array = new DataColumn[this._constraintIndex._indexFields.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._constraintIndex._indexFields[i].Column;
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0003C3EF File Offset: 0x0003A5EF
		internal void ConstraintIndexClear()
		{
			if (this._constraintIndex != null)
			{
				this._constraintIndex.RemoveRef();
				this._constraintIndex = null;
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0003C40C File Offset: 0x0003A60C
		internal void ConstraintIndexInitialize()
		{
			if (this._constraintIndex == null)
			{
				this._constraintIndex = this._key.GetSortIndex();
				this._constraintIndex.AddRef();
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0003C432 File Offset: 0x0003A632
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0003C43A File Offset: 0x0003A63A
		private void NonVirtualCheckState()
		{
			this._key.CheckState();
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00005E03 File Offset: 0x00004003
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003C448 File Offset: 0x0003A648
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			if (!this.Equals(constraints.Table._primaryKey))
			{
				ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.Table.DataSet, this.Table);
				while (parentForeignKeyConstraintEnumerator.GetNext())
				{
					ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
					if (this._key.ColumnsEqual(foreignKeyConstraint.ParentKey))
					{
						if (!fThrowException)
						{
							return false;
						}
						throw ExceptionBuilder.NeededForForeignKeyConstraint(this, foreignKeyConstraint);
					}
				}
				return true;
			}
			if (!fThrowException)
			{
				return false;
			}
			throw ExceptionBuilder.RemovePrimaryKey(constraints.Table);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003C4C2 File Offset: 0x0003A6C2
		internal override bool CanEnableConstraint()
		{
			return !this.Table.EnforceConstraints || this.ConstraintIndex.CheckUnique();
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003C4E0 File Offset: 0x0003A6E0
		internal override bool IsConstraintViolated()
		{
			bool flag = false;
			Index constraintIndex = this.ConstraintIndex;
			if (constraintIndex.HasDuplicates)
			{
				object[] uniqueKeyValues = constraintIndex.GetUniqueKeyValues();
				for (int i = 0; i < uniqueKeyValues.Length; i++)
				{
					Range range = constraintIndex.FindRecords((object[])uniqueKeyValues[i]);
					if (1 < range.Count)
					{
						DataRow[] rows = constraintIndex.GetRows(range);
						string text = ExceptionBuilder.UniqueConstraintViolationText(this._key.ColumnsReference, (object[])uniqueKeyValues[i]);
						for (int j = 0; j < rows.Length; j++)
						{
							rows[j].RowError = text;
							foreach (DataColumn dataColumn in this._key.ColumnsReference)
							{
								rows[j].SetColumnError(dataColumn, text);
							}
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0003C5B4 File Offset: 0x0003A7B4
		internal override void CheckConstraint(DataRow row, DataRowAction action)
		{
			if (this.Table.EnforceConstraints && (action == DataRowAction.Add || action == DataRowAction.Change || (action == DataRowAction.Rollback && row._tempRecord != -1)) && row.HaveValuesChanged(this.ColumnsReference) && this.ConstraintIndex.IsKeyRecordInIndex(row.GetDefaultRecord()))
			{
				object[] columnValues = row.GetColumnValues(this.ColumnsReference);
				throw ExceptionBuilder.ConstraintViolation(this.ColumnsReference, columnValues);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003C61F File Offset: 0x0003A81F
		internal override bool ContainsColumn(DataColumn column)
		{
			return this._key.ContainsColumn(column);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00034D37 File Offset: 0x00032F37
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003C630 File Offset: 0x0003A830
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
			int num2 = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num2];
			for (int i = 0; i < num2; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				num = dataTable.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array[i] = dataTable.Columns[num];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return uniqueConstraint;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003C75C File Offset: 0x0003A95C
		internal UniqueConstraint Clone(DataTable table)
		{
			int num = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				int num2 = table.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array[i] = table.Columns[num2];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return uniqueConstraint;
		}

		/// <summary>Gets the array of columns that this constraint affects.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0003C830 File Offset: 0x0003AA30
		[ReadOnly(true)]
		public virtual DataColumn[] Columns
		{
			get
			{
				return this._key.ToArray();
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0003C83D File Offset: 0x0003AA3D
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this._key.ColumnsReference;
			}
		}

		/// <summary>Gets a value indicating whether or not the constraint is on a primary key.</summary>
		/// <returns>true, if the constraint is on a primary key; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0003C84A File Offset: 0x0003AA4A
		public bool IsPrimaryKey
		{
			get
			{
				return this.Table != null && this == this.Table._primaryKey;
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003C864 File Offset: 0x0003AA64
		private void Create(string constraintName, DataColumn[] columns)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(columns[i]);
				}
			}
			this._key = new DataKey(columns, true);
			this.ConstraintName = constraintName;
			this.NonVirtualCheckState();
		}

		/// <summary>Compares this constraint to a second to determine if both are identical.</summary>
		/// <returns>true, if the contraints are equal; otherwise, false.</returns>
		/// <param name="key2">The object to which this <see cref="T:System.Data.UniqueConstraint" /> is compared. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D1A RID: 3354 RVA: 0x0003C8AC File Offset: 0x0003AAAC
		public override bool Equals(object key2)
		{
			return key2 is UniqueConstraint && this.Key.ColumnsEqual(((UniqueConstraint)key2).Key);
		}

		/// <summary>Gets the hash code of this instance of the <see cref="T:System.Data.UniqueConstraint" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D1B RID: 3355 RVA: 0x0003515E File Offset: 0x0003335E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x1700024C RID: 588
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x0003C8DC File Offset: 0x0003AADC
		internal override bool InCollection
		{
			set
			{
				base.InCollection = value;
				if (this._key.ColumnsReference.Length == 1)
				{
					this._key.ColumnsReference[0].InternalUnique(value);
				}
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0003C908 File Offset: 0x0003AB08
		internal DataKey Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Gets the table to which this constraint belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> to which the constraint belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0003C910 File Offset: 0x0003AB10
		[ReadOnly(true)]
		public override DataTable Table
		{
			get
			{
				if (this._key.HasValue)
				{
					return this._key.Table;
				}
				return null;
			}
		}

		// Token: 0x040008A8 RID: 2216
		private DataKey _key;

		// Token: 0x040008A9 RID: 2217
		private Index _constraintIndex;

		// Token: 0x040008AA RID: 2218
		internal bool _bPrimaryKey;

		// Token: 0x040008AB RID: 2219
		internal string _constraintName;

		// Token: 0x040008AC RID: 2220
		internal string[] _columnNames;
	}
}
