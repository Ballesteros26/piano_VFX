using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200033E RID: 830
	internal class RecursionLimiter
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x000AEF71 File Offset: 0x000AD171
		internal RecursionLimiter()
		{
			this.depth = 0;
			this.maxDepth = (DiagnosticsSwitches.NonRecursiveTypeLoading.Enabled ? 1 : int.MaxValue);
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x000AEF9A File Offset: 0x000AD19A
		internal bool IsExceededLimit
		{
			get
			{
				return this.depth > this.maxDepth;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x000AEFAA File Offset: 0x000AD1AA
		// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x000AEFB2 File Offset: 0x000AD1B2
		internal int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x000AEFBB File Offset: 0x000AD1BB
		internal WorkItems DeferredWorkItems
		{
			get
			{
				if (this.deferredWorkItems == null)
				{
					this.deferredWorkItems = new WorkItems();
				}
				return this.deferredWorkItems;
			}
		}

		// Token: 0x04001762 RID: 5986
		private int maxDepth;

		// Token: 0x04001763 RID: 5987
		private int depth;

		// Token: 0x04001764 RID: 5988
		private WorkItems deferredWorkItems;
	}
}
