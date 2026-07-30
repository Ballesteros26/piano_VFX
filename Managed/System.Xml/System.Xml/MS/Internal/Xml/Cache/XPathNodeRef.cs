using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000063 RID: 99
	internal struct XPathNodeRef
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C338 File Offset: 0x0000A538
		public static XPathNodeRef Null
		{
			get
			{
				return default(XPathNodeRef);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C34E File Offset: 0x0000A54E
		public XPathNodeRef(XPathNode[] page, int idx)
		{
			this.page = page;
			this.idx = idx;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000C35E File Offset: 0x0000A55E
		public bool IsNull
		{
			get
			{
				return this.page == null;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000C369 File Offset: 0x0000A569
		public XPathNode[] Page
		{
			get
			{
				return this.page;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000C371 File Offset: 0x0000A571
		public int Index
		{
			get
			{
				return this.idx;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C379 File Offset: 0x0000A579
		public override int GetHashCode()
		{
			return XPathNodeHelper.GetLocation(this.page, this.idx);
		}

		// Token: 0x040001A4 RID: 420
		private XPathNode[] page;

		// Token: 0x040001A5 RID: 421
		private int idx;
	}
}
