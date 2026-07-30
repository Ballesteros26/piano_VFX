using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200057C RID: 1404
	internal class TemplateMatch
	{
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x001362EB File Offset: 0x001344EB
		public XmlNodeKindFlags NodeKind
		{
			get
			{
				return this.nodeKind;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x001362F3 File Offset: 0x001344F3
		public QilName QName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x001362FB File Offset: 0x001344FB
		public QilIterator Iterator
		{
			get
			{
				return this.iterator;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x00136303 File Offset: 0x00134503
		public QilNode Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x0013630B File Offset: 0x0013450B
		public QilFunction TemplateFunction
		{
			get
			{
				return this.template.Function;
			}
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x00136318 File Offset: 0x00134518
		public TemplateMatch(Template template, QilLoop filter)
		{
			this.template = template;
			this.priority = (double.IsNaN(template.Priority) ? XPathPatternBuilder.GetPriority(filter) : template.Priority);
			this.iterator = filter.Variable;
			this.condition = filter.Body;
			XPathPatternBuilder.CleanAnnotation(filter);
			this.NipOffTypeNameCheck();
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x00136378 File Offset: 0x00134578
		private void NipOffTypeNameCheck()
		{
			QilBinary[] array = new QilBinary[4];
			int num = -1;
			QilNode left = this.condition;
			this.nodeKind = XmlNodeKindFlags.None;
			this.qname = null;
			while (left.NodeType == QilNodeType.And)
			{
				left = (array[++num & 3] = (QilBinary)left).Left;
			}
			if (left.NodeType != QilNodeType.IsType)
			{
				return;
			}
			QilBinary qilBinary = (QilBinary)left;
			if (qilBinary.Left != this.iterator || qilBinary.Right.NodeType != QilNodeType.LiteralType)
			{
				return;
			}
			XmlNodeKindFlags nodeKinds = qilBinary.Right.XmlType.NodeKinds;
			if (!Bits.ExactlyOne((uint)nodeKinds))
			{
				return;
			}
			this.nodeKind = nodeKinds;
			QilBinary qilBinary2 = array[num & 3];
			if (qilBinary2 != null && qilBinary2.Right.NodeType == QilNodeType.Eq)
			{
				QilBinary qilBinary3 = (QilBinary)qilBinary2.Right;
				if (qilBinary3.Left.NodeType == QilNodeType.NameOf && ((QilUnary)qilBinary3.Left).Child == this.iterator && qilBinary3.Right.NodeType == QilNodeType.LiteralQName)
				{
					this.qname = (QilName)((QilLiteral)qilBinary3.Right).Value;
					num--;
				}
			}
			QilBinary qilBinary4 = array[num & 3];
			QilBinary qilBinary5 = array[(num - 1) & 3];
			if (qilBinary5 != null)
			{
				qilBinary5.Left = qilBinary4.Right;
				return;
			}
			if (qilBinary4 != null)
			{
				this.condition = qilBinary4.Right;
				return;
			}
			this.condition = null;
		}

		// Token: 0x04002434 RID: 9268
		public static readonly TemplateMatch.TemplateMatchComparer Comparer = new TemplateMatch.TemplateMatchComparer();

		// Token: 0x04002435 RID: 9269
		private Template template;

		// Token: 0x04002436 RID: 9270
		private double priority;

		// Token: 0x04002437 RID: 9271
		private XmlNodeKindFlags nodeKind;

		// Token: 0x04002438 RID: 9272
		private QilName qname;

		// Token: 0x04002439 RID: 9273
		private QilIterator iterator;

		// Token: 0x0400243A RID: 9274
		private QilNode condition;

		// Token: 0x0200057D RID: 1405
		internal class TemplateMatchComparer : IComparer<TemplateMatch>
		{
			// Token: 0x060037B6 RID: 14262 RVA: 0x001364E9 File Offset: 0x001346E9
			public int Compare(TemplateMatch x, TemplateMatch y)
			{
				if (x.priority > y.priority)
				{
					return 1;
				}
				if (x.priority >= y.priority)
				{
					return x.template.OrderNumber - y.template.OrderNumber;
				}
				return -1;
			}
		}
	}
}
