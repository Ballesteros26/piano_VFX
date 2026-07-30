using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000BC RID: 188
	internal sealed class LookupNode : ExpressionNode
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0003362B File Offset: 0x0003182B
		internal LookupNode(DataTable table, string columnName, string relationName)
			: base(table)
		{
			this._relationName = relationName;
			this._columnName = columnName;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00033644 File Offset: 0x00031844
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this._column = null;
			this._relation = null;
			if (table == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRelationCollection parentRelations = table.ParentRelations;
			if (this._relationName == null)
			{
				if (parentRelations.Count > 1)
				{
					throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
				}
				this._relation = parentRelations[0];
			}
			else
			{
				this._relation = parentRelations[this._relationName];
			}
			if (this._relation == null)
			{
				throw ExprException.BindFailure(this._relationName);
			}
			DataTable parentTable = this._relation.ParentTable;
			this._column = parentTable.Columns[this._columnName];
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

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0003374E File Offset: 0x0003194E
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00033758 File Offset: 0x00031958
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this._column == null || this._relation == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRow parentRow = row.GetParentRow(this._relation, version);
			if (parentRow == null)
			{
				return DBNull.Value;
			}
			return parentRow[this._column, parentRow.HasVersion(version) ? version : DataRowVersion.Current];
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00032762 File Offset: 0x00030962
		internal override object Eval(int[] recordNos)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool IsTableConstant()
		{
			return false;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000337B5 File Offset: 0x000319B5
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00005D82 File Offset: 0x00003F82
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x0400076C RID: 1900
		private readonly string _relationName;

		// Token: 0x0400076D RID: 1901
		private readonly string _columnName;

		// Token: 0x0400076E RID: 1902
		private DataColumn _column;

		// Token: 0x0400076F RID: 1903
		private DataRelation _relation;
	}
}
