using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000A7 RID: 167
	internal sealed class AggregateNode : ExpressionNode
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x0002D1B6 File Offset: 0x0002B3B6
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName)
			: this(table, aggregateType, columnName, true, null)
		{
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002D1C4 File Offset: 0x0002B3C4
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName, bool local, string relationName)
			: base(table)
		{
			this._aggregate = (Aggregate)aggregateType;
			if (aggregateType == FunctionId.Sum)
			{
				this._type = AggregateType.Sum;
			}
			else if (aggregateType == FunctionId.Avg)
			{
				this._type = AggregateType.Mean;
			}
			else if (aggregateType == FunctionId.Min)
			{
				this._type = AggregateType.Min;
			}
			else if (aggregateType == FunctionId.Max)
			{
				this._type = AggregateType.Max;
			}
			else if (aggregateType == FunctionId.Count)
			{
				this._type = AggregateType.Count;
			}
			else if (aggregateType == FunctionId.Var)
			{
				this._type = AggregateType.Var;
			}
			else
			{
				if (aggregateType != FunctionId.StDev)
				{
					throw ExprException.UndefinedFunction(Function.s_functionName[(int)aggregateType]);
				}
				this._type = AggregateType.StDev;
			}
			this._local = local;
			this._relationName = relationName;
			this._columnName = columnName;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002D268 File Offset: 0x0002B468
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (this._local)
			{
				this._relation = null;
			}
			else
			{
				DataRelationCollection childRelations = table.ChildRelations;
				if (this._relationName == null)
				{
					if (childRelations.Count > 1)
					{
						throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
					}
					if (childRelations.Count != 1)
					{
						throw ExprException.AggregateUnbound(this.ToString());
					}
					this._relation = childRelations[0];
				}
				else
				{
					this._relation = childRelations[this._relationName];
				}
			}
			this._childTable = ((this._relation == null) ? table : this._relation.ChildTable);
			this._column = this._childTable.Columns[this._columnName];
			if (this._column == null)
			{
				throw ExprException.UnboundName(this._columnName);
			}
			int i;
			for (i = 0; i < list.Count; i++)
			{
				DataColumn dataColumn = list[i];
				if (this._column == dataColumn)
				{
					break;
				}
			}
			if (i >= list.Count)
			{
				list.Add(this._column);
			}
			AggregateNode.Bind(this._relation, list);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002D38C File Offset: 0x0002B58C
		internal static void Bind(DataRelation relation, List<DataColumn> list)
		{
			if (relation != null)
			{
				foreach (DataColumn dataColumn in relation.ChildColumnsReference)
				{
					if (!list.Contains(dataColumn))
					{
						list.Add(dataColumn);
					}
				}
				foreach (DataColumn dataColumn2 in relation.ParentColumnsReference)
				{
					if (!list.Contains(dataColumn2))
					{
						list.Add(dataColumn2);
					}
				}
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002D3EE File Offset: 0x0002B5EE
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002D3FC File Offset: 0x0002B5FC
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this._childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			DataRow[] array;
			if (this._local)
			{
				array = new DataRow[this._childTable.Rows.Count];
				this._childTable.Rows.CopyTo(array, 0);
			}
			else
			{
				if (row == null)
				{
					throw ExprException.EvalNoContext();
				}
				if (this._relation == null)
				{
					throw ExprException.AggregateUnbound(this.ToString());
				}
				array = row.GetChildRows(this._relation, version);
			}
			if (version == DataRowVersion.Proposed)
			{
				version = DataRowVersion.Default;
			}
			List<int> list = new List<int>();
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].RowState == DataRowState.Deleted)
				{
					if (DataRowAction.Rollback == array[i]._action)
					{
						version = DataRowVersion.Original;
						goto IL_00BF;
					}
				}
				else if (DataRowAction.Rollback != array[i]._action || array[i].RowState != DataRowState.Added)
				{
					goto IL_00BF;
				}
				IL_00E1:
				i++;
				continue;
				IL_00BF:
				if (version != DataRowVersion.Original || array[i]._oldRecord != -1)
				{
					list.Add(array[i].GetRecordFromVersion(version));
					goto IL_00E1;
				}
				goto IL_00E1;
			}
			int[] array2 = list.ToArray();
			return this._column.GetAggregateValue(array2, this._type);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002D50D File Offset: 0x0002B70D
		internal override object Eval(int[] records)
		{
			if (this._childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (!this._local)
			{
				throw ExprException.ComputeNotAggregate(this.ToString());
			}
			return this._column.GetAggregateValue(records, this._type);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002D549 File Offset: 0x0002B749
		internal override bool IsTableConstant()
		{
			return this._local;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002D549 File Offset: 0x0002B749
		internal override bool HasLocalAggregate()
		{
			return this._local;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002D551 File Offset: 0x0002B751
		internal override bool HasRemoteAggregate()
		{
			return !this._local;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002D55C File Offset: 0x0002B75C
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column || (this._column.Computed && this._column.DataExpression.DependsOn(column));
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00005D82 File Offset: 0x00003F82
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x040006B1 RID: 1713
		private readonly AggregateType _type;

		// Token: 0x040006B2 RID: 1714
		private readonly Aggregate _aggregate;

		// Token: 0x040006B3 RID: 1715
		private readonly bool _local;

		// Token: 0x040006B4 RID: 1716
		private readonly string _relationName;

		// Token: 0x040006B5 RID: 1717
		private readonly string _columnName;

		// Token: 0x040006B6 RID: 1718
		private DataTable _childTable;

		// Token: 0x040006B7 RID: 1719
		private DataColumn _column;

		// Token: 0x040006B8 RID: 1720
		private DataRelation _relation;
	}
}
