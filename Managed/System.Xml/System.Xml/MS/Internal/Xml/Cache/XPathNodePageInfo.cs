using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000065 RID: 101
	internal sealed class XPathNodePageInfo
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0000CBA3 File Offset: 0x0000ADA3
		public XPathNodePageInfo(XPathNode[] pagePrev, int pageNum)
		{
			this.pagePrev = pagePrev;
			this.pageNum = pageNum;
			this.nodeCount = 1;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		public int PageNumber
		{
			get
			{
				return this.pageNum;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		public int NodeCount
		{
			get
			{
				return this.nodeCount;
			}
			set
			{
				this.nodeCount = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000CBD9 File Offset: 0x0000ADD9
		public XPathNode[] PreviousPage
		{
			get
			{
				return this.pagePrev;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000CBE1 File Offset: 0x0000ADE1
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
		public XPathNode[] NextPage
		{
			get
			{
				return this.pageNext;
			}
			set
			{
				this.pageNext = value;
			}
		}

		// Token: 0x040001A6 RID: 422
		private int pageNum;

		// Token: 0x040001A7 RID: 423
		private int nodeCount;

		// Token: 0x040001A8 RID: 424
		private XPathNode[] pagePrev;

		// Token: 0x040001A9 RID: 425
		private XPathNode[] pageNext;
	}
}
