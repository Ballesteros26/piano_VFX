using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000BF RID: 191
	internal sealed class UnaryNode : ExpressionNode
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x00033C97 File Offset: 0x00031E97
		internal UnaryNode(DataTable table, int op, ExpressionNode right)
			: base(table)
		{
			this._op = op;
			this._right = right;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00033CAE File Offset: 0x00031EAE
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this._right.Bind(table, list);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002D3EE File Offset: 0x0002B5EE
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00033CC4 File Offset: 0x00031EC4
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.EvalUnaryOp(this._op, this._right.Eval(row, version));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00033CDF File Offset: 0x00031EDF
		internal override object Eval(int[] recordNos)
		{
			return this._right.Eval(recordNos);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00033CF0 File Offset: 0x00031EF0
		private object EvalUnaryOp(int op, object vl)
		{
			object value = DBNull.Value;
			if (DataExpression.IsUnknown(vl))
			{
				return DBNull.Value;
			}
			switch (op)
			{
			case 0:
				return vl;
			case 1:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					switch (storageType)
					{
					case StorageType.Byte:
						return (int)(-(int)((byte)vl));
					case StorageType.Int16:
						return (int)(-(int)((short)vl));
					case StorageType.UInt16:
					case StorageType.UInt32:
					case StorageType.UInt64:
						break;
					case StorageType.Int32:
						return -(int)vl;
					case StorageType.Int64:
						return -(long)vl;
					case StorageType.Single:
						return -(float)vl;
					case StorageType.Double:
						return -(double)vl;
					case StorageType.Decimal:
						return -(decimal)vl;
					default:
						switch (storageType)
						{
						case StorageType.SqlDecimal:
							return -(SqlDecimal)vl;
						case StorageType.SqlDouble:
							return -(SqlDouble)vl;
						case StorageType.SqlInt16:
							return -(SqlInt16)vl;
						case StorageType.SqlInt32:
							return -(SqlInt32)vl;
						case StorageType.SqlInt64:
							return -(SqlInt64)vl;
						case StorageType.SqlMoney:
							return -(SqlMoney)vl;
						case StorageType.SqlSingle:
							return -(SqlSingle)vl;
						}
						break;
					}
					return DBNull.Value;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 2:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					return vl;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 3:
				if (vl is SqlBoolean)
				{
					if (((SqlBoolean)vl).IsFalse)
					{
						return SqlBoolean.True;
					}
					if (((SqlBoolean)vl).IsTrue)
					{
						return SqlBoolean.False;
					}
					throw ExprException.UnsupportedOperator(op);
				}
				else
				{
					if (DataExpression.ToBoolean(vl))
					{
						return false;
					}
					return true;
				}
				break;
			default:
				throw ExprException.UnsupportedOperator(op);
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00033F41 File Offset: 0x00032141
		internal override bool IsConstant()
		{
			return this._right.IsConstant();
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00033F4E File Offset: 0x0003214E
		internal override bool IsTableConstant()
		{
			return this._right.IsTableConstant();
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00033F5B File Offset: 0x0003215B
		internal override bool HasLocalAggregate()
		{
			return this._right.HasLocalAggregate();
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00033F68 File Offset: 0x00032168
		internal override bool HasRemoteAggregate()
		{
			return this._right.HasRemoteAggregate();
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00033F75 File Offset: 0x00032175
		internal override bool DependsOn(DataColumn column)
		{
			return this._right.DependsOn(column);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00033F84 File Offset: 0x00032184
		internal override ExpressionNode Optimize()
		{
			this._right = this._right.Optimize();
			if (this.IsConstant())
			{
				object obj = this.Eval();
				return new ConstNode(base.table, ValueType.Object, obj, false);
			}
			return this;
		}

		// Token: 0x040007B7 RID: 1975
		internal readonly int _op;

		// Token: 0x040007B8 RID: 1976
		internal ExpressionNode _right;
	}
}
