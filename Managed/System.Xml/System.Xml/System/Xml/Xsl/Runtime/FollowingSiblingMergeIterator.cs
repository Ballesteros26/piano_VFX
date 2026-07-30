using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005E0 RID: 1504
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct FollowingSiblingMergeIterator
	{
		// Token: 0x06003B28 RID: 15144 RVA: 0x0014D498 File Offset: 0x0014B698
		public void Create(XmlNavigatorFilter filter)
		{
			this.wrapped.Create(filter);
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x0014D4A6 File Offset: 0x0014B6A6
		public IteratorResult MoveNext(XPathNavigator navigator)
		{
			return this.wrapped.MoveNext(navigator, false);
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x0014D4B5 File Offset: 0x0014B6B5
		public XPathNavigator Current
		{
			get
			{
				return this.wrapped.Current;
			}
		}

		// Token: 0x040026DB RID: 9947
		private ContentMergeIterator wrapped;
	}
}
