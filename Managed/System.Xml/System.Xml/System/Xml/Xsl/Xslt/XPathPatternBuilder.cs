using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200058D RID: 1421
	internal class XPathPatternBuilder : XPathPatternParser.IPatternBuilder, IXPathBuilder<QilNode>
	{
		// Token: 0x0600387F RID: 14463 RVA: 0x0013DF6C File Offset: 0x0013C16C
		public XPathPatternBuilder(IXPathEnvironment environment)
		{
			this.environment = environment;
			this.f = environment.Factory;
			this.predicateEnvironment = new XPathPatternBuilder.XPathPredicateEnvironment(environment);
			this.predicateBuilder = new XPathBuilder(this.predicateEnvironment);
			this.fixupNode = this.f.Unknown(XmlQueryTypeFactory.NodeNotRtfS);
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x0013DFC5 File Offset: 0x0013C1C5
		public QilNode FixupNode
		{
			get
			{
				return this.fixupNode;
			}
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x0013DFCD File Offset: 0x0013C1CD
		public virtual void StartBuild()
		{
			this.inTheBuild = true;
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void AssertFilter(QilLoop filter)
		{
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x0013DFD6 File Offset: 0x0013C1D6
		private void FixupFilterBinding(QilLoop filter, QilNode newBinding)
		{
			filter.Variable.Binding = newBinding;
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x0013DFE4 File Offset: 0x0013C1E4
		public virtual QilNode EndBuild(QilNode result)
		{
			this.inTheBuild = false;
			return result;
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x0013DFF0 File Offset: 0x0013C1F0
		public QilNode Operator(XPathOperator op, QilNode left, QilNode right)
		{
			if (left.NodeType == QilNodeType.Sequence)
			{
				((QilList)left).Add(right);
				return left;
			}
			return this.f.Sequence(left, right);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x0013E018 File Offset: 0x0013C218
		private static QilLoop BuildAxisFilter(QilPatternFactory f, QilIterator itr, XPathAxis xpathAxis, XPathNodeType nodeType, string name, string nsUri)
		{
			QilNode qilNode = ((name != null && nsUri != null) ? f.Eq(f.NameOf(itr), f.QName(name, nsUri)) : ((nsUri != null) ? f.Eq(f.NamespaceUriOf(itr), f.String(nsUri)) : ((name != null) ? f.Eq(f.LocalNameOf(itr), f.String(name)) : f.True())));
			XmlNodeKindFlags xmlNodeKindFlags = XPathBuilder.AxisTypeMask(itr.XmlType.NodeKinds, nodeType, xpathAxis);
			QilNode qilNode2 = ((xmlNodeKindFlags == XmlNodeKindFlags.None) ? f.False() : ((xmlNodeKindFlags == itr.XmlType.NodeKinds) ? f.True() : f.IsType(itr, XmlQueryTypeFactory.NodeChoice(xmlNodeKindFlags))));
			QilLoop qilLoop = f.BaseFactory.Filter(itr, f.And(qilNode2, qilNode));
			qilLoop.XmlType = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeChoice(xmlNodeKindFlags), qilLoop.XmlType.Cardinality);
			return qilLoop;
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x0013E0F8 File Offset: 0x0013C2F8
		public QilNode Axis(XPathAxis xpathAxis, XPathNodeType nodeType, string prefix, string name)
		{
			if (xpathAxis != XPathAxis.DescendantOrSelf)
			{
				QilLoop qilLoop;
				double num;
				if (xpathAxis != XPathAxis.Root)
				{
					string text = ((prefix == null) ? null : this.environment.ResolvePrefix(prefix));
					qilLoop = XPathPatternBuilder.BuildAxisFilter(this.f, this.f.For(this.fixupNode), xpathAxis, nodeType, name, text);
					if (nodeType - XPathNodeType.Element > 1)
					{
						if (nodeType != XPathNodeType.ProcessingInstruction)
						{
							num = -0.5;
						}
						else
						{
							num = ((name != null) ? 0.0 : (-0.5));
						}
					}
					else if (name != null)
					{
						num = 0.0;
					}
					else if (prefix != null)
					{
						num = -0.25;
					}
					else
					{
						num = -0.5;
					}
				}
				else
				{
					QilIterator qilIterator;
					qilLoop = this.f.BaseFactory.Filter(qilIterator = this.f.For(this.fixupNode), this.f.IsType(qilIterator, XmlQueryTypeFactory.Document));
					num = 0.5;
				}
				XPathPatternBuilder.SetPriority(qilLoop, num);
				XPathPatternBuilder.SetLastParent(qilLoop, qilLoop);
				return qilLoop;
			}
			return this.f.Nop(this.fixupNode);
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x0013E208 File Offset: 0x0013C408
		public QilNode JoinStep(QilNode left, QilNode right)
		{
			if (left.NodeType == QilNodeType.Nop)
			{
				QilUnary qilUnary = (QilUnary)left;
				qilUnary.Child = right;
				return qilUnary;
			}
			XPathPatternBuilder.CleanAnnotation(left);
			QilLoop qilLoop = (QilLoop)left;
			bool flag = false;
			if (right.NodeType == QilNodeType.Nop)
			{
				flag = true;
				right = ((QilUnary)right).Child;
			}
			QilLoop lastParent = XPathPatternBuilder.GetLastParent(right);
			this.FixupFilterBinding(qilLoop, flag ? this.f.Ancestor(lastParent.Variable) : this.f.Parent(lastParent.Variable));
			lastParent.Body = this.f.And(lastParent.Body, this.f.Not(this.f.IsEmpty(qilLoop)));
			XPathPatternBuilder.SetPriority(right, 0.5);
			XPathPatternBuilder.SetLastParent(right, qilLoop);
			return right;
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x0000365F File Offset: 0x0000185F
		QilNode IXPathBuilder<QilNode>.Predicate(QilNode node, QilNode condition, bool isReverseStep)
		{
			return null;
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x0013E2D0 File Offset: 0x0013C4D0
		public QilNode BuildPredicates(QilNode nodeset, List<QilNode> predicates)
		{
			List<QilNode> list = new List<QilNode>(predicates.Count);
			foreach (QilNode qilNode in predicates)
			{
				list.Add(XPathBuilder.PredicateToBoolean(qilNode, this.f, this.predicateEnvironment));
			}
			QilLoop qilLoop = (QilLoop)nodeset;
			QilIterator variable = qilLoop.Variable;
			if (this.predicateEnvironment.numFixupLast == 0 && this.predicateEnvironment.numFixupPosition == 0)
			{
				foreach (QilNode qilNode2 in list)
				{
					qilLoop.Body = this.f.And(qilLoop.Body, qilNode2);
				}
				qilLoop.Body = this.predicateEnvironment.fixupVisitor.Fixup(qilLoop.Body, variable, null);
			}
			else
			{
				QilIterator qilIterator = this.f.For(this.f.Parent(variable));
				QilNode qilNode3 = this.f.Content(qilIterator);
				QilLoop qilLoop2 = (QilLoop)nodeset.DeepClone(this.f.BaseFactory);
				qilLoop2.Variable.Binding = qilNode3;
				qilLoop2 = (QilLoop)this.f.Loop(qilIterator, qilLoop2);
				QilNode qilNode4 = qilLoop2;
				foreach (QilNode qilNode5 in list)
				{
					qilNode4 = XPathBuilder.BuildOnePredicate(qilNode4, qilNode5, false, this.f, this.predicateEnvironment.fixupVisitor, ref this.predicateEnvironment.numFixupCurrent, ref this.predicateEnvironment.numFixupPosition, ref this.predicateEnvironment.numFixupLast);
				}
				QilIterator qilIterator2 = this.f.For(qilNode4);
				QilNode qilNode6 = this.f.Filter(qilIterator2, this.f.Is(qilIterator2, variable));
				qilLoop.Body = this.f.Not(this.f.IsEmpty(qilNode6));
				qilLoop.Body = this.f.And(this.f.IsType(variable, qilLoop.XmlType), qilLoop.Body);
			}
			XPathPatternBuilder.SetPriority(nodeset, 0.5);
			return nodeset;
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x0013E538 File Offset: 0x0013C738
		public QilNode Function(string prefix, string name, IList<QilNode> args)
		{
			QilIterator qilIterator = this.f.For(this.fixupNode);
			QilNode qilNode;
			if (name == "id")
			{
				qilNode = this.f.Id(qilIterator, args[0]);
			}
			else
			{
				qilNode = this.environment.ResolveFunction(prefix, name, args, new XPathPatternBuilder.XsltFunctionFocus(qilIterator));
			}
			QilIterator qilIterator2;
			QilLoop qilLoop = this.f.BaseFactory.Filter(qilIterator, this.f.Not(this.f.IsEmpty(this.f.Filter(qilIterator2 = this.f.For(qilNode), this.f.Is(qilIterator2, qilIterator)))));
			XPathPatternBuilder.SetPriority(qilLoop, 0.5);
			XPathPatternBuilder.SetLastParent(qilLoop, qilLoop);
			return qilLoop;
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x0013E5F4 File Offset: 0x0013C7F4
		public QilNode String(string value)
		{
			return this.f.String(value);
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x0013E602 File Offset: 0x0013C802
		public QilNode Number(double value)
		{
			return this.UnexpectedToken("Literal number");
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x0013E60F File Offset: 0x0013C80F
		public QilNode Variable(string prefix, string name)
		{
			return this.UnexpectedToken("Variable");
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x0013E61C File Offset: 0x0013C81C
		private QilNode UnexpectedToken(string tokenName)
		{
			throw new Exception(string.Format(CultureInfo.InvariantCulture, "Internal Error: {0} is not allowed in XSLT pattern outside of predicate.", tokenName));
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x0013E634 File Offset: 0x0013C834
		public static void SetPriority(QilNode node, double priority)
		{
			XPathPatternBuilder.Annotation annotation = ((XPathPatternBuilder.Annotation)node.Annotation) ?? new XPathPatternBuilder.Annotation();
			annotation.Priority = priority;
			node.Annotation = annotation;
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x0013E664 File Offset: 0x0013C864
		public static double GetPriority(QilNode node)
		{
			return ((XPathPatternBuilder.Annotation)node.Annotation).Priority;
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x0013E678 File Offset: 0x0013C878
		private static void SetLastParent(QilNode node, QilLoop parent)
		{
			XPathPatternBuilder.Annotation annotation = ((XPathPatternBuilder.Annotation)node.Annotation) ?? new XPathPatternBuilder.Annotation();
			annotation.Parent = parent;
			node.Annotation = annotation;
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x0013E6A8 File Offset: 0x0013C8A8
		private static QilLoop GetLastParent(QilNode node)
		{
			return ((XPathPatternBuilder.Annotation)node.Annotation).Parent;
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x0013E6BA File Offset: 0x0013C8BA
		public static void CleanAnnotation(QilNode node)
		{
			node.Annotation = null;
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x0013E6C3 File Offset: 0x0013C8C3
		public IXPathBuilder<QilNode> GetPredicateBuilder(QilNode ctx)
		{
			QilLoop qilLoop = (QilLoop)ctx;
			return this.predicateBuilder;
		}

		// Token: 0x040024AE RID: 9390
		private XPathPatternBuilder.XPathPredicateEnvironment predicateEnvironment;

		// Token: 0x040024AF RID: 9391
		private XPathBuilder predicateBuilder;

		// Token: 0x040024B0 RID: 9392
		private bool inTheBuild;

		// Token: 0x040024B1 RID: 9393
		private XPathQilFactory f;

		// Token: 0x040024B2 RID: 9394
		private QilNode fixupNode;

		// Token: 0x040024B3 RID: 9395
		private IXPathEnvironment environment;

		// Token: 0x0200058E RID: 1422
		private class Annotation
		{
			// Token: 0x040024B4 RID: 9396
			public double Priority;

			// Token: 0x040024B5 RID: 9397
			public QilLoop Parent;
		}

		// Token: 0x0200058F RID: 1423
		private class XPathPredicateEnvironment : IXPathEnvironment, IFocus
		{
			// Token: 0x06003897 RID: 14487 RVA: 0x0013E6D4 File Offset: 0x0013C8D4
			public XPathPredicateEnvironment(IXPathEnvironment baseEnvironment)
			{
				this.baseEnvironment = baseEnvironment;
				this.f = baseEnvironment.Factory;
				this.fixupCurrent = this.f.Unknown(XmlQueryTypeFactory.NodeNotRtf);
				this.fixupPosition = this.f.Unknown(XmlQueryTypeFactory.DoubleX);
				this.fixupLast = this.f.Unknown(XmlQueryTypeFactory.DoubleX);
				this.fixupVisitor = new XPathBuilder.FixupVisitor(this.f, this.fixupCurrent, this.fixupPosition, this.fixupLast);
			}

			// Token: 0x17000BC5 RID: 3013
			// (get) Token: 0x06003898 RID: 14488 RVA: 0x0013E75F File Offset: 0x0013C95F
			public XPathQilFactory Factory
			{
				get
				{
					return this.f;
				}
			}

			// Token: 0x06003899 RID: 14489 RVA: 0x0013E767 File Offset: 0x0013C967
			public QilNode ResolveVariable(string prefix, string name)
			{
				return this.baseEnvironment.ResolveVariable(prefix, name);
			}

			// Token: 0x0600389A RID: 14490 RVA: 0x0013E776 File Offset: 0x0013C976
			public QilNode ResolveFunction(string prefix, string name, IList<QilNode> args, IFocus env)
			{
				return this.baseEnvironment.ResolveFunction(prefix, name, args, env);
			}

			// Token: 0x0600389B RID: 14491 RVA: 0x0013E788 File Offset: 0x0013C988
			public string ResolvePrefix(string prefix)
			{
				return this.baseEnvironment.ResolvePrefix(prefix);
			}

			// Token: 0x0600389C RID: 14492 RVA: 0x0013E796 File Offset: 0x0013C996
			public QilNode GetCurrent()
			{
				this.numFixupCurrent++;
				return this.fixupCurrent;
			}

			// Token: 0x0600389D RID: 14493 RVA: 0x0013E7AC File Offset: 0x0013C9AC
			public QilNode GetPosition()
			{
				this.numFixupPosition++;
				return this.fixupPosition;
			}

			// Token: 0x0600389E RID: 14494 RVA: 0x0013E7C2 File Offset: 0x0013C9C2
			public QilNode GetLast()
			{
				this.numFixupLast++;
				return this.fixupLast;
			}

			// Token: 0x040024B6 RID: 9398
			private readonly IXPathEnvironment baseEnvironment;

			// Token: 0x040024B7 RID: 9399
			private readonly XPathQilFactory f;

			// Token: 0x040024B8 RID: 9400
			public readonly XPathBuilder.FixupVisitor fixupVisitor;

			// Token: 0x040024B9 RID: 9401
			private readonly QilNode fixupCurrent;

			// Token: 0x040024BA RID: 9402
			private readonly QilNode fixupPosition;

			// Token: 0x040024BB RID: 9403
			private readonly QilNode fixupLast;

			// Token: 0x040024BC RID: 9404
			public int numFixupCurrent;

			// Token: 0x040024BD RID: 9405
			public int numFixupPosition;

			// Token: 0x040024BE RID: 9406
			public int numFixupLast;
		}

		// Token: 0x02000590 RID: 1424
		private class XsltFunctionFocus : IFocus
		{
			// Token: 0x0600389F RID: 14495 RVA: 0x0013E7D8 File Offset: 0x0013C9D8
			public XsltFunctionFocus(QilIterator current)
			{
				this.current = current;
			}

			// Token: 0x060038A0 RID: 14496 RVA: 0x0013E7E7 File Offset: 0x0013C9E7
			public QilNode GetCurrent()
			{
				return this.current;
			}

			// Token: 0x060038A1 RID: 14497 RVA: 0x0000365F File Offset: 0x0000185F
			public QilNode GetPosition()
			{
				return null;
			}

			// Token: 0x060038A2 RID: 14498 RVA: 0x0000365F File Offset: 0x0000185F
			public QilNode GetLast()
			{
				return null;
			}

			// Token: 0x040024BF RID: 9407
			private QilIterator current;
		}
	}
}
