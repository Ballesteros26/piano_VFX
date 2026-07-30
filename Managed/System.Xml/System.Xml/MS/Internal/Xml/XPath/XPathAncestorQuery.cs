using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200004A RID: 74
	internal sealed class XPathAncestorQuery : CacheAxisQuery
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00007BAF File Offset: 0x00005DAF
		public XPathAncestorQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest, bool matchSelf)
			: base(qyInput, name, prefix, typeTest)
		{
			this.matchSelf = matchSelf;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007BC4 File Offset: 0x00005DC4
		private XPathAncestorQuery(XPathAncestorQuery other)
			: base(other)
		{
			this.matchSelf = other.matchSelf;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007BDC File Offset: 0x00005DDC
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator = null;
			XPathNavigator xpathNavigator2;
			while ((xpathNavigator2 = this.qyInput.Advance()) != null)
			{
				if (!this.matchSelf || !this.matches(xpathNavigator2) || base.Insert(this.outputBuffer, xpathNavigator2))
				{
					if (xpathNavigator == null || !xpathNavigator.MoveTo(xpathNavigator2))
					{
						xpathNavigator = xpathNavigator2.Clone();
					}
					while (xpathNavigator.MoveToParent() && (!this.matches(xpathNavigator) || base.Insert(this.outputBuffer, xpathNavigator)))
					{
					}
				}
			}
			return this;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007C5A File Offset: 0x00005E5A
		public override XPathNodeIterator Clone()
		{
			return new XPathAncestorQuery(this);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00007C62 File Offset: 0x00005E62
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00005CB7 File Offset: 0x00003EB7
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007C78 File Offset: 0x00005E78
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			if (this.matchSelf)
			{
				w.WriteAttributeString("self", "yes");
			}
			if (base.NameTest)
			{
				w.WriteAttributeString("name", (base.Prefix.Length != 0) ? (base.Prefix + ":" + base.Name) : base.Name);
			}
			if (base.TypeTest != XPathNodeType.Element)
			{
				w.WriteAttributeString("nodeType", base.TypeTest.ToString());
			}
			this.qyInput.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x0400010D RID: 269
		private bool matchSelf;
	}
}
