using System;
using System.Globalization;

namespace System.Xml
{
	// Token: 0x0200007F RID: 127
	internal struct BinXmlSqlMoney
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000F612 File Offset: 0x0000D812
		public BinXmlSqlMoney(int v)
		{
			this.data = (long)v;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000F61C File Offset: 0x0000D81C
		public BinXmlSqlMoney(long v)
		{
			this.data = v;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000F628 File Offset: 0x0000D828
		public decimal ToDecimal()
		{
			bool flag;
			ulong num;
			if (this.data < 0L)
			{
				flag = true;
				num = (ulong)(-(ulong)this.data);
			}
			else
			{
				flag = false;
				num = (ulong)this.data;
			}
			return new decimal((int)num, (int)(num >> 32), 0, flag, 4);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000F664 File Offset: 0x0000D864
		public override string ToString()
		{
			return this.ToDecimal().ToString("#0.00##", CultureInfo.InvariantCulture);
		}

		// Token: 0x04000296 RID: 662
		private long data;
	}
}
