using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200061A RID: 1562
	internal abstract class XmlSortKey : IComparable
	{
		// Token: 0x17000C69 RID: 3177
		// (set) Token: 0x06003D59 RID: 15705 RVA: 0x00153768 File Offset: 0x00151968
		public int Priority
		{
			set
			{
				for (XmlSortKey xmlSortKey = this; xmlSortKey != null; xmlSortKey = xmlSortKey.nextKey)
				{
					xmlSortKey.priority = value;
				}
			}
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x0015378A File Offset: 0x0015198A
		public XmlSortKey AddSortKey(XmlSortKey sortKey)
		{
			if (this.nextKey != null)
			{
				this.nextKey.AddSortKey(sortKey);
			}
			else
			{
				this.nextKey = sortKey;
			}
			return this;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x001537AB File Offset: 0x001519AB
		protected int BreakSortingTie(XmlSortKey that)
		{
			if (this.nextKey != null)
			{
				return this.nextKey.CompareTo(that.nextKey);
			}
			if (this.priority >= that.priority)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x001537D8 File Offset: 0x001519D8
		protected int CompareToEmpty(object obj)
		{
			if (!(obj as XmlEmptySortKey).IsEmptyGreatest)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06003D5D RID: 15709
		public abstract int CompareTo(object that);

		// Token: 0x040027CB RID: 10187
		private int priority;

		// Token: 0x040027CC RID: 10188
		private XmlSortKey nextKey;
	}
}
