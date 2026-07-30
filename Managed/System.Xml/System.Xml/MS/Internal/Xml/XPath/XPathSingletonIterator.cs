using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000057 RID: 87
	internal class XPathSingletonIterator : ResetableIterator
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00009E76 File Offset: 0x00008076
		public XPathSingletonIterator(XPathNavigator nav)
		{
			this.nav = nav;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009E85 File Offset: 0x00008085
		public XPathSingletonIterator(XPathNavigator nav, bool moved)
			: this(nav)
		{
			if (moved)
			{
				this.position = 1;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009E98 File Offset: 0x00008098
		public XPathSingletonIterator(XPathSingletonIterator it)
		{
			this.nav = it.nav.Clone();
			this.position = it.position;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009EBD File Offset: 0x000080BD
		public override XPathNodeIterator Clone()
		{
			return new XPathSingletonIterator(this);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00009EC5 File Offset: 0x000080C5
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00009ECD File Offset: 0x000080CD
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00003242 File Offset: 0x00001442
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009ED5 File Offset: 0x000080D5
		public override bool MoveNext()
		{
			if (this.position == 0)
			{
				this.position = 1;
				return true;
			}
			return false;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009EE9 File Offset: 0x000080E9
		public override void Reset()
		{
			this.position = 0;
		}

		// Token: 0x0400015B RID: 347
		private XPathNavigator nav;

		// Token: 0x0400015C RID: 348
		private int position;
	}
}
