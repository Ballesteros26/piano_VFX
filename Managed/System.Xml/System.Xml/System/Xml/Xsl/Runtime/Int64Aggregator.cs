using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005F8 RID: 1528
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct Int64Aggregator
	{
		// Token: 0x06003B7A RID: 15226 RVA: 0x0014E647 File Offset: 0x0014C847
		public void Create()
		{
			this.cnt = 0;
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x0014E650 File Offset: 0x0014C850
		public void Sum(long value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
				this.cnt = 1;
				return;
			}
			this.result += value;
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x0014E677 File Offset: 0x0014C877
		public void Average(long value)
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

		// Token: 0x06003B7D RID: 15229 RVA: 0x0014E6A6 File Offset: 0x0014C8A6
		public void Minimum(long value)
		{
			if (this.cnt == 0 || value < this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x0014E6C7 File Offset: 0x0014C8C7
		public void Maximum(long value)
		{
			if (this.cnt == 0 || value > this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x0014E6E8 File Offset: 0x0014C8E8
		public long SumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06003B80 RID: 15232 RVA: 0x0014E6F0 File Offset: 0x0014C8F0
		public long AverageResult
		{
			get
			{
				return this.result / (long)this.cnt;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x0014E6E8 File Offset: 0x0014C8E8
		public long MinimumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x0014E6E8 File Offset: 0x0014C8E8
		public long MaximumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x0014E700 File Offset: 0x0014C900
		public bool IsEmpty
		{
			get
			{
				return this.cnt == 0;
			}
		}

		// Token: 0x04002734 RID: 10036
		private long result;

		// Token: 0x04002735 RID: 10037
		private int cnt;
	}
}
