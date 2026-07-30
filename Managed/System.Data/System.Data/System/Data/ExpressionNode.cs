using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000AD RID: 173
	internal abstract class ExpressionNode
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x000304F4 File Offset: 0x0002E6F4
		protected ExpressionNode(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00030504 File Offset: 0x0002E704
		internal IFormatProvider FormatProvider
		{
			get
			{
				if (this._table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._table.FormatProvider;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x000061D5 File Offset: 0x000043D5
		internal virtual bool IsSqlColumn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0003052C File Offset: 0x0002E72C
		protected DataTable table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00030534 File Offset: 0x0002E734
		protected void BindTable(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x06000A60 RID: 2656
		internal abstract void Bind(DataTable table, List<DataColumn> list);

		// Token: 0x06000A61 RID: 2657
		internal abstract object Eval();

		// Token: 0x06000A62 RID: 2658
		internal abstract object Eval(DataRow row, DataRowVersion version);

		// Token: 0x06000A63 RID: 2659
		internal abstract object Eval(int[] recordNos);

		// Token: 0x06000A64 RID: 2660
		internal abstract bool IsConstant();

		// Token: 0x06000A65 RID: 2661
		internal abstract bool IsTableConstant();

		// Token: 0x06000A66 RID: 2662
		internal abstract bool HasLocalAggregate();

		// Token: 0x06000A67 RID: 2663
		internal abstract bool HasRemoteAggregate();

		// Token: 0x06000A68 RID: 2664
		internal abstract ExpressionNode Optimize();

		// Token: 0x06000A69 RID: 2665 RVA: 0x000061D5 File Offset: 0x000043D5
		internal virtual bool DependsOn(DataColumn column)
		{
			return false;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0003053D File Offset: 0x0002E73D
		internal static bool IsInteger(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00030565 File Offset: 0x0002E765
		internal static bool IsIntegerSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || type == StorageType.SqlByte;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000305A1 File Offset: 0x0002E7A1
		internal static bool IsSigned(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || ExpressionNode.IsFloat(type);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000305BD File Offset: 0x0002E7BD
		internal static bool IsSignedSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || ExpressionNode.IsFloatSql(type);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000305E8 File Offset: 0x0002E7E8
		internal static bool IsUnsigned(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.Byte;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000305FE File Offset: 0x0002E7FE
		internal static bool IsUnsignedSql(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SqlByte || type == StorageType.Byte;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00030619 File Offset: 0x0002E819
		internal static bool IsNumeric(StorageType type)
		{
			return ExpressionNode.IsFloat(type) || ExpressionNode.IsInteger(type);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0003062B File Offset: 0x0002E82B
		internal static bool IsNumericSql(StorageType type)
		{
			return ExpressionNode.IsFloatSql(type) || ExpressionNode.IsIntegerSql(type);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0003063D File Offset: 0x0002E83D
		internal static bool IsFloat(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00030650 File Offset: 0x0002E850
		internal static bool IsFloatSql(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal || type == StorageType.SqlDouble || type == StorageType.SqlDecimal || type == StorageType.SqlMoney || type == StorageType.SqlSingle;
		}

		// Token: 0x040006EF RID: 1775
		private DataTable _table;
	}
}
