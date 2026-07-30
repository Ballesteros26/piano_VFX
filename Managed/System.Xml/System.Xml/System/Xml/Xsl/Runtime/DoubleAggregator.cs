using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005FA RID: 1530
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DoubleAggregator
	{
		// Token: 0x06003B8E RID: 15246 RVA: 0x0014E7E9 File Offset: 0x0014C9E9
		public void Create()
		{
			this.cnt = 0;
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x0014E7F2 File Offset: 0x0014C9F2
		public void Sum(double value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
				this.cnt = 1;
				return;
			}
			this.result += value;
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x0014E819 File Offset: 0x0014CA19
		public void Average(double value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
			}
			else
			{
				this.result += value;
			}
			this.cnt++;
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x0014E848 File Offset: 0x0014CA48
		public void Minimum(double value)
		{
			if (this.cnt == 0 || value < this.result || double.IsNaN(value))
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x0014E871 File Offset: 0x0014CA71
		public void Maximum(double value)
		{
			if (this.cnt == 0 || value > this.result || double.IsNaN(value))
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x0014E89A File Offset: 0x0014CA9A
		public double SumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x0014E8A2 File Offset: 0x0014CAA2
		public double AverageResult
		{
			get
			{
				return this.result / (double)this.cnt;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x0014E89A File Offset: 0x0014CA9A
		public double MinimumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x0014E89A File Offset: 0x0014CA9A
		public double MaximumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06003B97 RID: 15255 RVA: 0x0014E8B2 File Offset: 0x0014CAB2
		public bool IsEmpty
		{
			get
			{
				return this.cnt == 0;
			}
		}

		// Token: 0x04002738 RID: 10040
		private double result;

		// Token: 0x04002739 RID: 10041
		private int cnt;
	}
}
