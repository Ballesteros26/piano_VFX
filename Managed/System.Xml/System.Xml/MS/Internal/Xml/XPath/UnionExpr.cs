using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000045 RID: 69
	internal sealed class UnionExpr : Query
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x000076D1 File Offset: 0x000058D1
		public UnionExpr(Query query1, Query query2)
		{
			this.qy1 = query1;
			this.qy2 = query2;
			this.advance1 = true;
			this.advance2 = true;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000076F8 File Offset: 0x000058F8
		private UnionExpr(UnionExpr other)
			: base(other)
		{
			this.qy1 = Query.Clone(other.qy1);
			this.qy2 = Query.Clone(other.qy2);
			this.advance1 = other.advance1;
			this.advance2 = other.advance2;
			this.currentNode = Query.Clone(other.currentNode);
			this.nextNode = Query.Clone(other.nextNode);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007768 File Offset: 0x00005968
		public override void Reset()
		{
			this.qy1.Reset();
			this.qy2.Reset();
			this.advance1 = true;
			this.advance2 = true;
			this.nextNode = null;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007795 File Offset: 0x00005995
		public override void SetXsltContext(XsltContext xsltContext)
		{
			this.qy1.SetXsltContext(xsltContext);
			this.qy2.SetXsltContext(xsltContext);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000077AF File Offset: 0x000059AF
		public override object Evaluate(XPathNodeIterator context)
		{
			this.qy1.Evaluate(context);
			this.qy2.Evaluate(context);
			this.advance1 = true;
			this.advance2 = true;
			this.nextNode = null;
			base.ResetCount();
			return this;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000077E8 File Offset: 0x000059E8
		private XPathNavigator ProcessSamePosition(XPathNavigator result)
		{
			this.currentNode = result;
			this.advance1 = (this.advance2 = true);
			return result;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000780D File Offset: 0x00005A0D
		private XPathNavigator ProcessBeforePosition(XPathNavigator res1, XPathNavigator res2)
		{
			this.nextNode = res2;
			this.advance2 = false;
			this.advance1 = true;
			this.currentNode = res1;
			return res1;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000782C File Offset: 0x00005A2C
		private XPathNavigator ProcessAfterPosition(XPathNavigator res1, XPathNavigator res2)
		{
			this.nextNode = res1;
			this.advance1 = false;
			this.advance2 = true;
			this.currentNode = res2;
			return res2;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000784C File Offset: 0x00005A4C
		public override XPathNavigator Advance()
		{
			XPathNavigator xpathNavigator;
			if (this.advance1)
			{
				xpathNavigator = this.qy1.Advance();
			}
			else
			{
				xpathNavigator = this.nextNode;
			}
			XPathNavigator xpathNavigator2;
			if (this.advance2)
			{
				xpathNavigator2 = this.qy2.Advance();
			}
			else
			{
				xpathNavigator2 = this.nextNode;
			}
			if (xpathNavigator != null && xpathNavigator2 != null)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(xpathNavigator, xpathNavigator2);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					return this.ProcessBeforePosition(xpathNavigator, xpathNavigator2);
				}
				if (xmlNodeOrder == XmlNodeOrder.After)
				{
					return this.ProcessAfterPosition(xpathNavigator, xpathNavigator2);
				}
				return this.ProcessSamePosition(xpathNavigator);
			}
			else
			{
				if (xpathNavigator2 == null)
				{
					this.advance1 = true;
					this.advance2 = false;
					this.currentNode = xpathNavigator;
					this.nextNode = null;
					return xpathNavigator;
				}
				this.advance1 = false;
				this.advance2 = true;
				this.currentNode = xpathNavigator2;
				this.nextNode = null;
				return xpathNavigator2;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007904 File Offset: 0x00005B04
		public override XPathNavigator MatchNode(XPathNavigator xsltContext)
		{
			if (xsltContext == null)
			{
				return null;
			}
			XPathNavigator xpathNavigator = this.qy1.MatchNode(xsltContext);
			if (xpathNavigator != null)
			{
				return xpathNavigator;
			}
			return this.qy2.MatchNode(xsltContext);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007934 File Offset: 0x00005B34
		public override XPathNodeIterator Clone()
		{
			return new UnionExpr(this);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000793C File Offset: 0x00005B3C
		public override XPathNavigator Current
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007944 File Offset: 0x00005B44
		public override int CurrentPosition
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000794C File Offset: 0x00005B4C
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			if (this.qy1 != null)
			{
				this.qy1.PrintQuery(w);
			}
			if (this.qy2 != null)
			{
				this.qy2.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x04000104 RID: 260
		internal Query qy1;

		// Token: 0x04000105 RID: 261
		internal Query qy2;

		// Token: 0x04000106 RID: 262
		private bool advance1;

		// Token: 0x04000107 RID: 263
		private bool advance2;

		// Token: 0x04000108 RID: 264
		private XPathNavigator currentNode;

		// Token: 0x04000109 RID: 265
		private XPathNavigator nextNode;
	}
}
