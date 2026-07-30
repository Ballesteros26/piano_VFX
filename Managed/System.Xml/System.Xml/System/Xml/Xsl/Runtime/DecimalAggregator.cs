using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005F9 RID: 1529
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DecimalAggregator
	{
		// Token: 0x06003B84 RID: 15236 RVA: 0x0014E70B File Offset: 0x0014C90B
		public void Create()
		{
			this.cnt = 0;
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x0014E714 File Offset: 0x0014C914
		public void Sum(decimal value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
				this.cnt = 1;
				return;
			}
			this.result += value;
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x0014E73F File Offset: 0x0014C93F
		public void Average(decimal value)
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

		// Token: 0x06003B87 RID: 15239 RVA: 0x0014E772 File Offset: 0x0014C972
		public void Minimum(decimal value)
		{
			if (this.cnt == 0 || value < this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x0014E798 File Offset: 0x0014C998
		public void Maximum(decimal value)
		{
			if (this.cnt == 0 || value > this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06003B89 RID: 15241 RVA: 0x0014E7BE File Offset: 0x0014C9BE
		public decimal SumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06003B8A RID: 15242 RVA: 0x0014E7C6 File Offset: 0x0014C9C6
		public decimal AverageResult
		{
			get
			{
				return this.result / this.cnt;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06003B8B RID: 15243 RVA: 0x0014E7BE File Offset: 0x0014C9BE
		public decimal MinimumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06003B8C RID: 15244 RVA: 0x0014E7BE File Offset: 0x0014C9BE
		public decimal MaximumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x0014E7DE File Offset: 0x0014C9DE
		public bool IsEmpty
		{
			get
			{
				return this.cnt == 0;
			}
		}

		// Token: 0x04002736 RID: 10038
		private decimal result;

		// Token: 0x04002737 RID: 10039
		private int cnt;
	}
}
