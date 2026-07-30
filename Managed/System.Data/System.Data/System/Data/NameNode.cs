using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x020000BD RID: 189
	internal sealed class NameNode : ExpressionNode
	{
		// Token: 0x06000AF2 RID: 2802 RVA: 0x000337C3 File Offset: 0x000319C3
		internal NameNode(DataTable table, char[] text, int start, int pos)
			: base(table)
		{
			this._name = NameNode.ParseName(text, start, pos);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000337DB File Offset: 0x000319DB
		internal NameNode(DataTable table, string name)
			: base(table)
		{
			this._name = name;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x000337EB File Offset: 0x000319EB
		internal override bool IsSqlColumn
		{
			get
			{
				return this._column.IsSqlType;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000337F8 File Offset: 0x000319F8
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.UnboundName(this._name);
			}
			try
			{
				this._column = table.Columns[this._name];
			}
			catch (Exception ex)
			{
				this._found = false;
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ExprException.UnboundName(this._name);
			}
			if (this._column == null)
			{
				throw ExprException.UnboundName(this._name);
			}
			this._name = this._column.ColumnName;
			this._found = true;
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
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0003374E File Offset: 0x0003194E
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000338C4 File Offset: 0x00031AC4
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (!this._found)
			{
				throw ExprException.UnboundName(this._name);
			}
			if (row != null)
			{
				return this._column[row.GetRecordFromVersion(version)];
			}
			if (this.IsTableConstant())
			{
				return this._column.DataExpression.Evaluate();
			}
			throw ExprException.UnboundName(this._name);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00032762 File Offset: 0x00030962
		internal override object Eval(int[] records)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0003391F File Offset: 0x00031B1F
		internal override bool IsTableConstant()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.IsTableAggregate();
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00033948 File Offset: 0x00031B48
		internal override bool HasLocalAggregate()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.HasLocalAggregate();
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00033971 File Offset: 0x00031B71
		internal override bool HasRemoteAggregate()
		{
			return this._column != null && this._column.Computed && this._column.DataExpression.HasRemoteAggregate();
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0003399A File Offset: 0x00031B9A
		internal override bool DependsOn(DataColumn column)
		{
			return this._column == column || (this._column.Computed && this._column.DataExpression.DependsOn(column));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00005D82 File Offset: 0x00003F82
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000339C8 File Offset: 0x00031BC8
		internal static string ParseName(char[] text, int start, int pos)
		{
			char c = '\0';
			string text2 = string.Empty;
			int num = start;
			int num2 = pos;
			checked
			{
				if (text[start] == '`')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "`";
				}
				else if (text[start] == '[')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "]\\";
				}
			}
			if (c != '\0')
			{
				int num3 = start;
				for (int i = start; i < pos; i++)
				{
					if (text[i] == c && i + 1 < pos && text2.IndexOf(text[i + 1]) >= 0)
					{
						i++;
					}
					text[num3] = text[i];
					num3++;
				}
				pos = num3;
			}
			if (pos == start)
			{
				throw ExprException.InvalidName(new string(text, num, num2 - num));
			}
			return new string(text, start, pos - start);
		}

		// Token: 0x04000770 RID: 1904
		internal char _open;

		// Token: 0x04000771 RID: 1905
		internal char _close;

		// Token: 0x04000772 RID: 1906
		internal string _name;

		// Token: 0x04000773 RID: 1907
		internal bool _found;

		// Token: 0x04000774 RID: 1908
		internal bool _type;

		// Token: 0x04000775 RID: 1909
		internal DataColumn _column;
	}
}
