using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000055 RID: 85
	internal class XPathSelectionIterator : ResetableIterator
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00009D5D File Offset: 0x00007F5D
		internal XPathSelectionIterator(XPathNavigator nav, Query query)
		{
			this.nav = nav.Clone();
			this.query = query;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009D78 File Offset: 0x00007F78
		protected XPathSelectionIterator(XPathSelectionIterator it)
		{
			this.nav = it.nav.Clone();
			this.query = (Query)it.query.Clone();
			this.position = it.position;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009DB3 File Offset: 0x00007FB3
		public override void Reset()
		{
			this.query.Reset();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009DC0 File Offset: 0x00007FC0
		public override bool MoveNext()
		{
			XPathNavigator xpathNavigator = this.query.Advance();
			if (xpathNavigator != null)
			{
				this.position++;
				if (!this.nav.MoveTo(xpathNavigator))
				{
					this.nav = xpathNavigator.Clone();
				}
				return true;
			}
			return false;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00009E07 File Offset: 0x00008007
		public override int Count
		{
			get
			{
				return this.query.Count;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00009E14 File Offset: 0x00008014
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00009E1C File Offset: 0x0000801C
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009E24 File Offset: 0x00008024
		public override XPathNodeIterator Clone()
		{
			return new XPathSelectionIterator(this);
		}

		// Token: 0x04000158 RID: 344
		private XPathNavigator nav;

		// Token: 0x04000159 RID: 345
		private Query query;

		// Token: 0x0400015A RID: 346
		private int position;
	}
}
