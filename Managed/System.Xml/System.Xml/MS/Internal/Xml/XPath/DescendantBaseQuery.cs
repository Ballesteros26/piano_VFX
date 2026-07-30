using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000018 RID: 24
	internal abstract class DescendantBaseQuery : BaseAxisQuery
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003245 File Offset: 0x00001445
		public DescendantBaseQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis)
			: base(qyParent, Name, Prefix, Type)
		{
			this.matchSelf = matchSelf;
			this.abbrAxis = abbrAxis;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003262 File Offset: 0x00001462
		public DescendantBaseQuery(DescendantBaseQuery other)
			: base(other)
		{
			this.matchSelf = other.matchSelf;
			this.abbrAxis = other.abbrAxis;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003284 File Offset: 0x00001484
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null)
			{
				if (!this.abbrAxis)
				{
					throw XPathException.Create("'{0}' is an invalid XSLT pattern.");
				}
				if (this.matches(context))
				{
					XPathNavigator xpathNavigator;
					if (this.matchSelf && (xpathNavigator = this.qyInput.MatchNode(context)) != null)
					{
						return xpathNavigator;
					}
					XPathNavigator xpathNavigator2 = context.Clone();
					while (xpathNavigator2.MoveToParent())
					{
						if ((xpathNavigator = this.qyInput.MatchNode(xpathNavigator2)) != null)
						{
							return xpathNavigator;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000032F0 File Offset: 0x000014F0
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

		// Token: 0x04000078 RID: 120
		protected bool matchSelf;

		// Token: 0x04000079 RID: 121
		protected bool abbrAxis;
	}
}
