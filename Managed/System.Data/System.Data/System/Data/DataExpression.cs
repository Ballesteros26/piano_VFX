using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000AC RID: 172
	internal sealed class DataExpression : IFilter
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x00030108 File Offset: 0x0002E308
		internal DataExpression(DataTable table, string expression)
			: this(table, expression, null)
		{
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00030114 File Offset: 0x0002E314
		internal DataExpression(DataTable table, string expression, Type type)
		{
			ExpressionParser expressionParser = new ExpressionParser(table);
			expressionParser.LoadExpression(expression);
			this._originalExpression = expression;
			this._expr = null;
			if (expression != null)
			{
				this._storageType = DataStorage.GetStorageType(type);
				if (this._storageType == StorageType.BigInteger)
				{
					throw ExprException.UnsupportedDataType(type);
				}
				this._dataType = type;
				this._expr = expressionParser.Parse();
				this._parsed = true;
				if (this._expr != null && table != null)
				{
					this.Bind(table);
					return;
				}
				this._bound = false;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x000301A2 File Offset: 0x0002E3A2
		internal string Expression
		{
			get
			{
				if (this._originalExpression == null)
				{
					return "";
				}
				return this._originalExpression;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x000301B8 File Offset: 0x0002E3B8
		internal ExpressionNode ExpressionNode
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x000301C0 File Offset: 0x0002E3C0
		internal bool HasValue
		{
			get
			{
				return this._expr != null;
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000301CC File Offset: 0x0002E3CC
		internal void Bind(DataTable table)
		{
			this._table = table;
			if (table == null)
			{
				return;
			}
			if (this._expr != null)
			{
				List<DataColumn> list = new List<DataColumn>();
				this._expr.Bind(table, list);
				this._expr = this._expr.Optimize();
				this._table = table;
				this._bound = true;
				this._dependency = list.ToArray();
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0003022A File Offset: 0x0002E42A
		internal bool DependsOn(DataColumn column)
		{
			return this._expr != null && this._expr.DependsOn(column);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00030242 File Offset: 0x0002E442
		internal object Evaluate()
		{
			return this.Evaluate(null, DataRowVersion.Default);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00030250 File Offset: 0x0002E450
		internal object Evaluate(DataRow row, DataRowVersion version)
		{
			if (!this._bound)
			{
				this.Bind(this._table);
			}
			object obj;
			if (this._expr != null)
			{
				obj = this._expr.Eval(row, version);
				if (obj == DBNull.Value && StorageType.Uri >= this._storageType)
				{
					return obj;
				}
				try
				{
					if (StorageType.Object != this._storageType)
					{
						obj = SqlConvert.ChangeType2(obj, this._storageType, this._dataType, this._table.FormatProvider);
					}
					return obj;
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(obj, this._dataType, ex);
				}
			}
			obj = null;
			return obj;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00030304 File Offset: 0x0002E504
		internal object Evaluate(DataRow[] rows)
		{
			return this.Evaluate(rows, DataRowVersion.Default);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00030314 File Offset: 0x0002E514
		internal object Evaluate(DataRow[] rows, DataRowVersion version)
		{
			if (!this._bound)
			{
				this.Bind(this._table);
			}
			if (this._expr != null)
			{
				List<int> list = new List<int>();
				foreach (DataRow dataRow in rows)
				{
					if (dataRow.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow._oldRecord != -1))
					{
						list.Add(dataRow.GetRecordFromVersion(version));
					}
				}
				int[] array = list.ToArray();
				return this._expr.Eval(array);
			}
			return DBNull.Value;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0003039C File Offset: 0x0002E59C
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			if (this._expr == null)
			{
				return true;
			}
			if (row == null)
			{
				throw ExprException.InvokeArgument();
			}
			object obj = this._expr.Eval(row, version);
			bool flag;
			try
			{
				flag = DataExpression.ToBoolean(obj);
			}
			catch (EvaluateException)
			{
				throw ExprException.FilterConvertion(this.Expression);
			}
			return flag;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000303F4 File Offset: 0x0002E5F4
		internal DataColumn[] GetDependency()
		{
			return this._dependency;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000303FC File Offset: 0x0002E5FC
		internal bool IsTableAggregate()
		{
			return this._expr != null && this._expr.IsTableConstant();
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00030413 File Offset: 0x0002E613
		internal static bool IsUnknown(object value)
		{
			return DataStorage.IsObjectNull(value);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0003041B File Offset: 0x0002E61B
		internal bool HasLocalAggregate()
		{
			return this._expr != null && this._expr.HasLocalAggregate();
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00030432 File Offset: 0x0002E632
		internal bool HasRemoteAggregate()
		{
			return this._expr != null && this._expr.HasRemoteAggregate();
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0003044C File Offset: 0x0002E64C
		internal static bool ToBoolean(object value)
		{
			if (DataExpression.IsUnknown(value))
			{
				return false;
			}
			if (value is bool)
			{
				return (bool)value;
			}
			if (value is SqlBoolean)
			{
				return ((SqlBoolean)value).IsTrue;
			}
			if (value is string)
			{
				try
				{
					return bool.Parse((string)value);
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(value, typeof(bool), ex);
				}
			}
			throw ExprException.DatavalueConvertion(value, typeof(bool), null);
		}

		// Token: 0x040006E7 RID: 1767
		internal string _originalExpression;

		// Token: 0x040006E8 RID: 1768
		private bool _parsed;

		// Token: 0x040006E9 RID: 1769
		private bool _bound;

		// Token: 0x040006EA RID: 1770
		private ExpressionNode _expr;

		// Token: 0x040006EB RID: 1771
		private DataTable _table;

		// Token: 0x040006EC RID: 1772
		private readonly StorageType _storageType;

		// Token: 0x040006ED RID: 1773
		private readonly Type _dataType;

		// Token: 0x040006EE RID: 1774
		private DataColumn[] _dependency = Array.Empty<DataColumn>();
	}
}
