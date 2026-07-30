using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000207 RID: 519
	internal abstract class DocumentXPathNodeIterator_ElemDescendants : XPathNodeIterator
	{
		// Token: 0x060012D9 RID: 4825 RVA: 0x00070ACD File Offset: 0x0006ECCD
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNavigator nav)
		{
			this.nav = (DocumentXPathNavigator)nav.Clone();
			this.level = 0;
			this.position = 0;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00070AF4 File Offset: 0x0006ECF4
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNodeIterator_ElemDescendants other)
		{
			this.nav = (DocumentXPathNavigator)other.nav.Clone();
			this.level = other.level;
			this.position = other.position;
		}

		// Token: 0x060012DB RID: 4827
		protected abstract bool Match(XmlNode node);

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00070B2A File Offset: 0x0006ED2A
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00070B32 File Offset: 0x0006ED32
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00070B3A File Offset: 0x0006ED3A
		protected void SetPosition(int pos)
		{
			this.position = pos;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00070B44 File Offset: 0x0006ED44
		public override bool MoveNext()
		{
			for (;;)
			{
				if (this.nav.MoveToFirstChild())
				{
					this.level++;
				}
				else
				{
					if (this.level == 0)
					{
						break;
					}
					while (!this.nav.MoveToNext())
					{
						this.level--;
						if (this.level == 0)
						{
							return false;
						}
						if (!this.nav.MoveToParent())
						{
							return false;
						}
					}
				}
				XmlNode xmlNode = (XmlNode)this.nav.UnderlyingObject;
				if (xmlNode.NodeType == XmlNodeType.Element && this.Match(xmlNode))
				{
					goto Block_5;
				}
			}
			return false;
			Block_5:
			this.position++;
			return true;
		}

		// Token: 0x04000D41 RID: 3393
		private DocumentXPathNavigator nav;

		// Token: 0x04000D42 RID: 3394
		private int level;

		// Token: 0x04000D43 RID: 3395
		private int position;
	}
}
