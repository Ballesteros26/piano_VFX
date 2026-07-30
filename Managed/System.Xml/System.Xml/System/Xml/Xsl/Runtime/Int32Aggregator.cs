using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005F7 RID: 1527
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct Int32Aggregator
	{
		// Token: 0x06003B70 RID: 15216 RVA: 0x0014E584 File Offset: 0x0014C784
		public void Create()
		{
			this.cnt = 0;
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x0014E58D File Offset: 0x0014C78D
		public void Sum(int value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
				this.cnt = 1;
				return;
			}
			this.result += value;
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x0014E5B4 File Offset: 0x0014C7B4
		public void Average(int value)
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

		// Token: 0x06003B73 RID: 15219 RVA: 0x0014E5E3 File Offset: 0x0014C7E3
		public void Minimum(int value)
		{
			if (this.cnt == 0 || value < this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x0014E604 File Offset: 0x0014C804
		public void Maximum(int value)
		{
			if (this.cnt == 0 || value > this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x0014E625 File Offset: 0x0014C825
		public int SumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06003B76 RID: 15222 RVA: 0x0014E62D File Offset: 0x0014C82D
		public int AverageResult
		{
			get
			{
				return this.result / this.cnt;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x0014E625 File Offset: 0x0014C825
		public int MinimumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x0014E625 File Offset: 0x0014C825
		public int MaximumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x0014E63C File Offset: 0x0014C83C
		public bool IsEmpty
		{
			get
			{
				return this.cnt == 0;
			}
		}

		// Token: 0x04002732 RID: 10034
		private int result;

		// Token: 0x04002733 RID: 10035
		private int cnt;
	}
}
