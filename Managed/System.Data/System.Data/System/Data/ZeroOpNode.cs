using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000C0 RID: 192
	internal sealed class ZeroOpNode : ExpressionNode
	{
		// Token: 0x06000B13 RID: 2835 RVA: 0x00033FC1 File Offset: 0x000321C1
		internal ZeroOpNode(int op)
			: base(null)
		{
			this._op = op;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00005E03 File Offset: 0x00004003
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00033FD4 File Offset: 0x000321D4
		internal override object Eval()
		{
			switch (this._op)
			{
			case 32:
				return DBNull.Value;
			case 33:
				return true;
			case 34:
				return false;
			default:
				return DBNull.Value;
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002FE0E File Offset: 0x0002E00E
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002FE0E File Offset: 0x0002E00E
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0000EF2B File Offset: 0x0000D12B
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0000EF2B File Offset: 0x0000D12B
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00005D82 File Offset: 0x00003F82
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x040007B9 RID: 1977
		internal readonly int _op;

		// Token: 0x040007BA RID: 1978
		internal const int zop_True = 1;

		// Token: 0x040007BB RID: 1979
		internal const int zop_False = 0;

		// Token: 0x040007BC RID: 1980
		internal const int zop_Null = -1;
	}
}
