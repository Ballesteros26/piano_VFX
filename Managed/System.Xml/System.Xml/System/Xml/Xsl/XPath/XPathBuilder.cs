using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x020005B8 RID: 1464
	internal class XPathBuilder : IXPathBuilder<QilNode>, IXPathEnvironment, IFocus
	{
		// Token: 0x06003A2D RID: 14893 RVA: 0x001485FD File Offset: 0x001467FD
		QilNode IFocus.GetCurrent()
		{
			return this.GetCurrentNode();
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x00148605 File Offset: 0x00146805
		QilNode IFocus.GetPosition()
		{
			return this.GetCurrentPosition();
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x0014860D File Offset: 0x0014680D
		QilNode IFocus.GetLast()
		{
			return this.GetLastPosition();
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x00148615 File Offset: 0x00146815
		XPathQilFactory IXPathEnvironment.Factory
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x0014861D File Offset: 0x0014681D
		QilNode IXPathEnvironment.ResolveVariable(string prefix, string name)
		{
			return this.Variable(prefix, name);
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x0000365F File Offset: 0x0000185F
		QilNode IXPathEnvironment.ResolveFunction(string prefix, string name, IList<QilNode> args, IFocus env)
		{
			return null;
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x00148627 File Offset: 0x00146827
		string IXPathEnvironment.ResolvePrefix(string prefix)
		{
			return this.environment.ResolvePrefix(prefix);
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x00148638 File Offset: 0x00146838
		public XPathBuilder(IXPathEnvironment environment)
		{
			this.environment = environment;
			this.f = this.environment.Factory;
			this.fixupCurrent = this.f.Unknown(XmlQueryTypeFactory.NodeNotRtf);
			this.fixupPosition = this.f.Unknown(XmlQueryTypeFactory.DoubleX);
			this.fixupLast = this.f.Unknown(XmlQueryTypeFactory.DoubleX);
			this.fixupVisitor = new XPathBuilder.FixupVisitor(this.f, this.fixupCurrent, this.fixupPosition, this.fixupLast);
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x001486C8 File Offset: 0x001468C8
		public virtual void StartBuild()
		{
			this.inTheBuild = true;
			this.numFixupCurrent = (this.numFixupPosition = (this.numFixupLast = 0));
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x001486F8 File Offset: 0x001468F8
		public virtual QilNode EndBuild(QilNode result)
		{
			if (result == null)
			{
				this.inTheBuild = false;
				return result;
			}
			if (result.XmlType.MaybeMany && result.XmlType.IsNode && result.XmlType.IsNotRtf)
			{
				result = this.f.DocOrderDistinct(result);
			}
			result = this.fixupVisitor.Fixup(result, this.environment);
			this.numFixupCurrent -= this.fixupVisitor.numCurrent;
			this.numFixupPosition -= this.fixupVisitor.numPosition;
			this.numFixupLast -= this.fixupVisitor.numLast;
			this.inTheBuild = false;
			return result;
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x001487AA File Offset: 0x001469AA
		private QilNode GetCurrentNode()
		{
			this.numFixupCurrent++;
			return this.fixupCurrent;
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x001487C0 File Offset: 0x001469C0
		private QilNode GetCurrentPosition()
		{
			this.numFixupPosition++;
			return this.fixupPosition;
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x001487D6 File Offset: 0x001469D6
		private QilNode GetLastPosition()
		{
			this.numFixupLast++;
			return this.fixupLast;
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x001487EC File Offset: 0x001469EC
		public virtual QilNode String(string value)
		{
			return this.f.String(value);
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x001487FA File Offset: 0x001469FA
		public virtual QilNode Number(double value)
		{
			return this.f.Double(value);
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x00148808 File Offset: 0x00146A08
		public virtual QilNode Operator(XPathOperator op, QilNode left, QilNode right)
		{
			switch (XPathBuilder.OperatorGroup[(int)op])
			{
			case XPathBuilder.XPathOperatorGroup.Logical:
				return this.LogicalOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Equality:
				return this.EqualityOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Relational:
				return this.RelationalOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Arithmetic:
				return this.ArithmeticOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Negate:
				return this.NegateOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Union:
				return this.UnionOperator(op, left, right);
			default:
				return null;
			}
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x0014887C File Offset: 0x00146A7C
		private QilNode LogicalOperator(XPathOperator op, QilNode left, QilNode right)
		{
			left = this.f.ConvertToBoolean(left);
			right = this.f.ConvertToBoolean(right);
			if (op != XPathOperator.Or)
			{
				return this.f.And(left, right);
			}
			return this.f.Or(left, right);
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x001488BC File Offset: 0x00146ABC
		private QilNode CompareValues(XPathOperator op, QilNode left, QilNode right, XmlTypeCode compType)
		{
			left = this.f.ConvertToType(compType, left);
			right = this.f.ConvertToType(compType, right);
			switch (op)
			{
			case XPathOperator.Eq:
				return this.f.Eq(left, right);
			case XPathOperator.Ne:
				return this.f.Ne(left, right);
			case XPathOperator.Lt:
				return this.f.Lt(left, right);
			case XPathOperator.Le:
				return this.f.Le(left, right);
			case XPathOperator.Gt:
				return this.f.Gt(left, right);
			case XPathOperator.Ge:
				return this.f.Ge(left, right);
			default:
				return null;
			}
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x00148960 File Offset: 0x00146B60
		private QilNode CompareNodeSetAndValue(XPathOperator op, QilNode nodeset, QilNode val, XmlTypeCode compType)
		{
			if (compType == XmlTypeCode.Boolean || nodeset.XmlType.IsSingleton)
			{
				return this.CompareValues(op, nodeset, val, compType);
			}
			QilIterator qilIterator = this.f.For(nodeset);
			return this.f.Not(this.f.IsEmpty(this.f.Filter(qilIterator, this.CompareValues(op, this.f.XPathNodeValue(qilIterator), val, compType))));
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x001489D1 File Offset: 0x00146BD1
		private static XPathOperator InvertOp(XPathOperator op)
		{
			if (op == XPathOperator.Lt)
			{
				return XPathOperator.Gt;
			}
			if (op == XPathOperator.Le)
			{
				return XPathOperator.Ge;
			}
			if (op == XPathOperator.Gt)
			{
				return XPathOperator.Lt;
			}
			if (op != XPathOperator.Ge)
			{
				return op;
			}
			return XPathOperator.Le;
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x001489EC File Offset: 0x00146BEC
		private QilNode CompareNodeSetAndNodeSet(XPathOperator op, QilNode left, QilNode right, XmlTypeCode compType)
		{
			if (right.XmlType.IsSingleton)
			{
				return this.CompareNodeSetAndValue(op, left, right, compType);
			}
			if (left.XmlType.IsSingleton)
			{
				op = XPathBuilder.InvertOp(op);
				return this.CompareNodeSetAndValue(op, right, left, compType);
			}
			QilIterator qilIterator = this.f.For(left);
			QilIterator qilIterator2 = this.f.For(right);
			return this.f.Not(this.f.IsEmpty(this.f.Loop(qilIterator, this.f.Filter(qilIterator2, this.CompareValues(op, this.f.XPathNodeValue(qilIterator), this.f.XPathNodeValue(qilIterator2), compType)))));
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x00148A9C File Offset: 0x00146C9C
		private QilNode EqualityOperator(XPathOperator op, QilNode left, QilNode right)
		{
			XmlQueryType xmlType = left.XmlType;
			XmlQueryType xmlType2 = right.XmlType;
			if (this.f.IsAnyType(left) || this.f.IsAnyType(right))
			{
				return this.f.InvokeEqualityOperator(XPathBuilder.QilOperator[(int)op], left, right);
			}
			if (xmlType.IsNode && xmlType2.IsNode)
			{
				return this.CompareNodeSetAndNodeSet(op, left, right, XmlTypeCode.String);
			}
			if (xmlType.IsNode)
			{
				return this.CompareNodeSetAndValue(op, left, right, xmlType2.TypeCode);
			}
			if (xmlType2.IsNode)
			{
				return this.CompareNodeSetAndValue(op, right, left, xmlType.TypeCode);
			}
			XmlTypeCode xmlTypeCode = ((xmlType.TypeCode == XmlTypeCode.Boolean || xmlType2.TypeCode == XmlTypeCode.Boolean) ? XmlTypeCode.Boolean : ((xmlType.TypeCode == XmlTypeCode.Double || xmlType2.TypeCode == XmlTypeCode.Double) ? XmlTypeCode.Double : XmlTypeCode.String));
			return this.CompareValues(op, left, right, xmlTypeCode);
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x00148B74 File Offset: 0x00146D74
		private QilNode RelationalOperator(XPathOperator op, QilNode left, QilNode right)
		{
			XmlQueryType xmlType = left.XmlType;
			XmlQueryType xmlType2 = right.XmlType;
			if (this.f.IsAnyType(left) || this.f.IsAnyType(right))
			{
				return this.f.InvokeRelationalOperator(XPathBuilder.QilOperator[(int)op], left, right);
			}
			if (xmlType.IsNode && xmlType2.IsNode)
			{
				return this.CompareNodeSetAndNodeSet(op, left, right, XmlTypeCode.Double);
			}
			if (xmlType.IsNode)
			{
				XmlTypeCode xmlTypeCode = ((xmlType2.TypeCode == XmlTypeCode.Boolean) ? XmlTypeCode.Boolean : XmlTypeCode.Double);
				return this.CompareNodeSetAndValue(op, left, right, xmlTypeCode);
			}
			if (xmlType2.IsNode)
			{
				XmlTypeCode xmlTypeCode2 = ((xmlType.TypeCode == XmlTypeCode.Boolean) ? XmlTypeCode.Boolean : XmlTypeCode.Double);
				op = XPathBuilder.InvertOp(op);
				return this.CompareNodeSetAndValue(op, right, left, xmlTypeCode2);
			}
			return this.CompareValues(op, left, right, XmlTypeCode.Double);
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x00148C37 File Offset: 0x00146E37
		private QilNode NegateOperator(XPathOperator op, QilNode left, QilNode right)
		{
			return this.f.Negate(this.f.ConvertToNumber(left));
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x00148C50 File Offset: 0x00146E50
		private QilNode ArithmeticOperator(XPathOperator op, QilNode left, QilNode right)
		{
			left = this.f.ConvertToNumber(left);
			right = this.f.ConvertToNumber(right);
			switch (op)
			{
			case XPathOperator.Plus:
				return this.f.Add(left, right);
			case XPathOperator.Minus:
				return this.f.Subtract(left, right);
			case XPathOperator.Multiply:
				return this.f.Multiply(left, right);
			case XPathOperator.Divide:
				return this.f.Divide(left, right);
			case XPathOperator.Modulo:
				return this.f.Modulo(left, right);
			default:
				return null;
			}
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x00148CE0 File Offset: 0x00146EE0
		private QilNode UnionOperator(XPathOperator op, QilNode left, QilNode right)
		{
			if (left == null)
			{
				return this.f.EnsureNodeSet(right);
			}
			left = this.f.EnsureNodeSet(left);
			right = this.f.EnsureNodeSet(right);
			if (left.NodeType == QilNodeType.Sequence)
			{
				((QilList)left).Add(right);
				return left;
			}
			return this.f.Union(left, right);
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x00148D3E File Offset: 0x00146F3E
		public static XmlNodeKindFlags AxisTypeMask(XmlNodeKindFlags inputTypeMask, XPathNodeType nodeType, XPathAxis xpathAxis)
		{
			return inputTypeMask & XPathBuilder.XPathNodeType2QilXmlNodeKind[(int)nodeType] & XPathBuilder.XPathAxisMask[(int)xpathAxis];
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x00148D54 File Offset: 0x00146F54
		private QilNode BuildAxisFilter(QilNode qilAxis, XPathAxis xpathAxis, XPathNodeType nodeType, string name, string nsUri)
		{
			XmlNodeKindFlags nodeKinds = qilAxis.XmlType.NodeKinds;
			XmlNodeKindFlags xmlNodeKindFlags = XPathBuilder.AxisTypeMask(nodeKinds, nodeType, xpathAxis);
			if (xmlNodeKindFlags == XmlNodeKindFlags.None)
			{
				return this.f.Sequence();
			}
			QilIterator qilIterator;
			if (xmlNodeKindFlags != nodeKinds)
			{
				qilAxis = this.f.Filter(qilIterator = this.f.For(qilAxis), this.f.IsType(qilIterator, XmlQueryTypeFactory.NodeChoice(xmlNodeKindFlags)));
				qilAxis.XmlType = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeChoice(xmlNodeKindFlags), qilAxis.XmlType.Cardinality);
				if (qilAxis.NodeType == QilNodeType.Filter)
				{
					QilLoop qilLoop = (QilLoop)qilAxis;
					qilLoop.Body = this.f.And(qilLoop.Body, (name != null && nsUri != null) ? this.f.Eq(this.f.NameOf(qilIterator), this.f.QName(name, nsUri)) : ((nsUri != null) ? this.f.Eq(this.f.NamespaceUriOf(qilIterator), this.f.String(nsUri)) : ((name != null) ? this.f.Eq(this.f.LocalNameOf(qilIterator), this.f.String(name)) : this.f.True())));
					return qilLoop;
				}
			}
			return this.f.Filter(qilIterator = this.f.For(qilAxis), (name != null && nsUri != null) ? this.f.Eq(this.f.NameOf(qilIterator), this.f.QName(name, nsUri)) : ((nsUri != null) ? this.f.Eq(this.f.NamespaceUriOf(qilIterator), this.f.String(nsUri)) : ((name != null) ? this.f.Eq(this.f.LocalNameOf(qilIterator), this.f.String(name)) : this.f.True())));
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x00148F3C File Offset: 0x0014713C
		private QilNode BuildAxis(XPathAxis xpathAxis, XPathNodeType nodeType, string nsUri, string name)
		{
			QilNode currentNode = this.GetCurrentNode();
			QilNode qilNode;
			switch (xpathAxis)
			{
			case XPathAxis.Ancestor:
				qilNode = this.f.Ancestor(currentNode);
				break;
			case XPathAxis.AncestorOrSelf:
				qilNode = this.f.AncestorOrSelf(currentNode);
				break;
			case XPathAxis.Attribute:
				qilNode = this.f.Content(currentNode);
				break;
			case XPathAxis.Child:
				qilNode = this.f.Content(currentNode);
				break;
			case XPathAxis.Descendant:
				qilNode = this.f.Descendant(currentNode);
				break;
			case XPathAxis.DescendantOrSelf:
				qilNode = this.f.DescendantOrSelf(currentNode);
				break;
			case XPathAxis.Following:
				qilNode = this.f.XPathFollowing(currentNode);
				break;
			case XPathAxis.FollowingSibling:
				qilNode = this.f.FollowingSibling(currentNode);
				break;
			case XPathAxis.Namespace:
				qilNode = this.f.XPathNamespace(currentNode);
				break;
			case XPathAxis.Parent:
				qilNode = this.f.Parent(currentNode);
				break;
			case XPathAxis.Preceding:
				qilNode = this.f.XPathPreceding(currentNode);
				break;
			case XPathAxis.PrecedingSibling:
				qilNode = this.f.PrecedingSibling(currentNode);
				break;
			case XPathAxis.Self:
				qilNode = currentNode;
				break;
			case XPathAxis.Root:
				return this.f.Root(currentNode);
			default:
				qilNode = null;
				break;
			}
			QilNode qilNode2 = this.BuildAxisFilter(qilNode, xpathAxis, nodeType, name, nsUri);
			if (xpathAxis == XPathAxis.Ancestor || xpathAxis == XPathAxis.Preceding || xpathAxis == XPathAxis.AncestorOrSelf || xpathAxis == XPathAxis.PrecedingSibling)
			{
				qilNode2 = this.f.BaseFactory.DocOrderDistinct(qilNode2);
			}
			return qilNode2;
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x0014909C File Offset: 0x0014729C
		public virtual QilNode Axis(XPathAxis xpathAxis, XPathNodeType nodeType, string prefix, string name)
		{
			string text = ((prefix == null) ? null : this.environment.ResolvePrefix(prefix));
			return this.BuildAxis(xpathAxis, nodeType, text, name);
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x001490C8 File Offset: 0x001472C8
		public virtual QilNode JoinStep(QilNode left, QilNode right)
		{
			QilIterator qilIterator = this.f.For(this.f.EnsureNodeSet(left));
			right = this.fixupVisitor.Fixup(right, qilIterator, null);
			this.numFixupCurrent -= this.fixupVisitor.numCurrent;
			this.numFixupPosition -= this.fixupVisitor.numPosition;
			this.numFixupLast -= this.fixupVisitor.numLast;
			return this.f.DocOrderDistinct(this.f.Loop(qilIterator, right));
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x00149160 File Offset: 0x00147360
		public virtual QilNode Predicate(QilNode nodeset, QilNode predicate, bool isReverseStep)
		{
			if (isReverseStep)
			{
				nodeset = ((QilUnary)nodeset).Child;
			}
			predicate = XPathBuilder.PredicateToBoolean(predicate, this.f, this);
			return XPathBuilder.BuildOnePredicate(nodeset, predicate, isReverseStep, this.f, this.fixupVisitor, ref this.numFixupCurrent, ref this.numFixupPosition, ref this.numFixupLast);
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x001491B4 File Offset: 0x001473B4
		public static QilNode PredicateToBoolean(QilNode predicate, XPathQilFactory f, IXPathEnvironment env)
		{
			if (!f.IsAnyType(predicate))
			{
				if (predicate.XmlType.TypeCode == XmlTypeCode.Double)
				{
					predicate = f.Eq(env.GetPosition(), predicate);
				}
				else
				{
					predicate = f.ConvertToBoolean(predicate);
				}
			}
			else
			{
				QilIterator qilIterator;
				predicate = f.Loop(qilIterator = f.Let(predicate), f.Conditional(f.IsType(qilIterator, XmlQueryTypeFactory.Double), f.Eq(env.GetPosition(), f.TypeAssert(qilIterator, XmlQueryTypeFactory.DoubleX)), f.ConvertToBoolean(qilIterator)));
			}
			return predicate;
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x00149238 File Offset: 0x00147438
		public static QilNode BuildOnePredicate(QilNode nodeset, QilNode predicate, bool isReverseStep, XPathQilFactory f, XPathBuilder.FixupVisitor fixupVisitor, ref int numFixupCurrent, ref int numFixupPosition, ref int numFixupLast)
		{
			nodeset = f.EnsureNodeSet(nodeset);
			QilNode qilNode;
			if (numFixupLast != 0 && fixupVisitor.CountUnfixedLast(predicate) != 0)
			{
				QilIterator qilIterator = f.Let(nodeset);
				QilIterator qilIterator2 = f.Let(f.XsltConvert(f.Length(qilIterator), XmlQueryTypeFactory.DoubleX));
				QilIterator qilIterator3 = f.For(qilIterator);
				predicate = fixupVisitor.Fixup(predicate, qilIterator3, qilIterator2);
				numFixupCurrent -= fixupVisitor.numCurrent;
				numFixupPosition -= fixupVisitor.numPosition;
				numFixupLast -= fixupVisitor.numLast;
				qilNode = f.Loop(qilIterator, f.Loop(qilIterator2, f.Filter(qilIterator3, predicate)));
			}
			else
			{
				QilIterator qilIterator4 = f.For(nodeset);
				predicate = fixupVisitor.Fixup(predicate, qilIterator4, null);
				numFixupCurrent -= fixupVisitor.numCurrent;
				numFixupPosition -= fixupVisitor.numPosition;
				numFixupLast -= fixupVisitor.numLast;
				qilNode = f.Filter(qilIterator4, predicate);
			}
			if (isReverseStep)
			{
				qilNode = f.DocOrderDistinct(qilNode);
			}
			return qilNode;
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x0014932E File Offset: 0x0014752E
		public virtual QilNode Variable(string prefix, string name)
		{
			return this.environment.ResolveVariable(prefix, name);
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x00149340 File Offset: 0x00147540
		public virtual QilNode Function(string prefix, string name, IList<QilNode> args)
		{
			XPathBuilder.FunctionInfo<XPathBuilder.FuncId> functionInfo;
			if (prefix.Length != 0 || !XPathBuilder.FunctionTable.TryGetValue(name, out functionInfo))
			{
				return this.environment.ResolveFunction(prefix, name, args, this);
			}
			functionInfo.CastArguments(args, name, this.f);
			switch (functionInfo.id)
			{
			case XPathBuilder.FuncId.Last:
				return this.GetLastPosition();
			case XPathBuilder.FuncId.Position:
				return this.GetCurrentPosition();
			case XPathBuilder.FuncId.Count:
				return this.f.XsltConvert(this.f.Length(this.f.DocOrderDistinct(args[0])), XmlQueryTypeFactory.DoubleX);
			case XPathBuilder.FuncId.LocalName:
				if (args.Count != 0)
				{
					return this.LocalNameOfFirstNode(args[0]);
				}
				return this.f.LocalNameOf(this.GetCurrentNode());
			case XPathBuilder.FuncId.NamespaceUri:
				if (args.Count != 0)
				{
					return this.NamespaceOfFirstNode(args[0]);
				}
				return this.f.NamespaceUriOf(this.GetCurrentNode());
			case XPathBuilder.FuncId.Name:
				if (args.Count != 0)
				{
					return this.NameOfFirstNode(args[0]);
				}
				return this.NameOf(this.GetCurrentNode());
			case XPathBuilder.FuncId.String:
				if (args.Count != 0)
				{
					return this.f.ConvertToString(args[0]);
				}
				return this.f.XPathNodeValue(this.GetCurrentNode());
			case XPathBuilder.FuncId.Number:
				if (args.Count != 0)
				{
					return this.f.ConvertToNumber(args[0]);
				}
				return this.f.XsltConvert(this.f.XPathNodeValue(this.GetCurrentNode()), XmlQueryTypeFactory.DoubleX);
			case XPathBuilder.FuncId.Boolean:
				return this.f.ConvertToBoolean(args[0]);
			case XPathBuilder.FuncId.True:
				return this.f.True();
			case XPathBuilder.FuncId.False:
				return this.f.False();
			case XPathBuilder.FuncId.Not:
				return this.f.Not(args[0]);
			case XPathBuilder.FuncId.Id:
				return this.f.DocOrderDistinct(this.f.Id(this.GetCurrentNode(), args[0]));
			case XPathBuilder.FuncId.Concat:
				return this.f.StrConcat(args);
			case XPathBuilder.FuncId.StartsWith:
				return this.f.InvokeStartsWith(args[0], args[1]);
			case XPathBuilder.FuncId.Contains:
				return this.f.InvokeContains(args[0], args[1]);
			case XPathBuilder.FuncId.SubstringBefore:
				return this.f.InvokeSubstringBefore(args[0], args[1]);
			case XPathBuilder.FuncId.SubstringAfter:
				return this.f.InvokeSubstringAfter(args[0], args[1]);
			case XPathBuilder.FuncId.Substring:
				if (args.Count != 2)
				{
					return this.f.InvokeSubstring(args[0], args[1], args[2]);
				}
				return this.f.InvokeSubstring(args[0], args[1]);
			case XPathBuilder.FuncId.StringLength:
				return this.f.XsltConvert(this.f.StrLength((args.Count == 0) ? this.f.XPathNodeValue(this.GetCurrentNode()) : args[0]), XmlQueryTypeFactory.DoubleX);
			case XPathBuilder.FuncId.Normalize:
				return this.f.InvokeNormalizeSpace((args.Count == 0) ? this.f.XPathNodeValue(this.GetCurrentNode()) : args[0]);
			case XPathBuilder.FuncId.Translate:
				return this.f.InvokeTranslate(args[0], args[1], args[2]);
			case XPathBuilder.FuncId.Lang:
				return this.f.InvokeLang(args[0], this.GetCurrentNode());
			case XPathBuilder.FuncId.Sum:
				return this.Sum(this.f.DocOrderDistinct(args[0]));
			case XPathBuilder.FuncId.Floor:
				return this.f.InvokeFloor(args[0]);
			case XPathBuilder.FuncId.Ceiling:
				return this.f.InvokeCeiling(args[0]);
			case XPathBuilder.FuncId.Round:
				return this.f.InvokeRound(args[0]);
			default:
				return null;
			}
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x00149734 File Offset: 0x00147934
		private QilNode LocalNameOfFirstNode(QilNode arg)
		{
			if (arg.XmlType.IsSingleton)
			{
				return this.f.LocalNameOf(arg);
			}
			QilIterator qilIterator;
			return this.f.StrConcat(this.f.Loop(qilIterator = this.f.FirstNode(arg), this.f.LocalNameOf(qilIterator)));
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x0014978C File Offset: 0x0014798C
		private QilNode NamespaceOfFirstNode(QilNode arg)
		{
			if (arg.XmlType.IsSingleton)
			{
				return this.f.NamespaceUriOf(arg);
			}
			QilIterator qilIterator;
			return this.f.StrConcat(this.f.Loop(qilIterator = this.f.FirstNode(arg), this.f.NamespaceUriOf(qilIterator)));
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x001497E4 File Offset: 0x001479E4
		private QilNode NameOf(QilNode arg)
		{
			if (arg is QilIterator)
			{
				QilIterator qilIterator;
				QilIterator qilIterator2;
				return this.f.Loop(qilIterator = this.f.Let(this.f.PrefixOf(arg)), this.f.Loop(qilIterator2 = this.f.Let(this.f.LocalNameOf(arg)), this.f.Conditional(this.f.Eq(this.f.StrLength(qilIterator), this.f.Int32(0)), qilIterator2, this.f.StrConcat(new QilNode[]
				{
					qilIterator,
					this.f.String(":"),
					qilIterator2
				}))));
			}
			QilIterator qilIterator3 = this.f.Let(arg);
			return this.f.Loop(qilIterator3, this.NameOf(qilIterator3));
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x001498C0 File Offset: 0x00147AC0
		private QilNode NameOfFirstNode(QilNode arg)
		{
			if (arg.XmlType.IsSingleton)
			{
				return this.NameOf(arg);
			}
			QilIterator qilIterator;
			return this.f.StrConcat(this.f.Loop(qilIterator = this.f.FirstNode(arg), this.NameOf(qilIterator)));
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x00149910 File Offset: 0x00147B10
		private QilNode Sum(QilNode arg)
		{
			QilIterator qilIterator;
			return this.f.Sum(this.f.Sequence(this.f.Double(0.0), this.f.Loop(qilIterator = this.f.For(arg), this.f.ConvertToNumber(qilIterator))));
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x0014996C File Offset: 0x00147B6C
		private static Dictionary<string, XPathBuilder.FunctionInfo<XPathBuilder.FuncId>> CreateFunctionTable()
		{
			return new Dictionary<string, XPathBuilder.FunctionInfo<XPathBuilder.FuncId>>(36)
			{
				{
					"last",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Last, 0, 0, null)
				},
				{
					"position",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Position, 0, 0, null)
				},
				{
					"name",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Name, 0, 1, XPathBuilder.argNodeSet)
				},
				{
					"namespace-uri",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.NamespaceUri, 0, 1, XPathBuilder.argNodeSet)
				},
				{
					"local-name",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.LocalName, 0, 1, XPathBuilder.argNodeSet)
				},
				{
					"count",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Count, 1, 1, XPathBuilder.argNodeSet)
				},
				{
					"id",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Id, 1, 1, XPathBuilder.argAny)
				},
				{
					"string",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.String, 0, 1, XPathBuilder.argAny)
				},
				{
					"concat",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Concat, 2, int.MaxValue, null)
				},
				{
					"starts-with",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.StartsWith, 2, 2, XPathBuilder.argString2)
				},
				{
					"contains",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Contains, 2, 2, XPathBuilder.argString2)
				},
				{
					"substring-before",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.SubstringBefore, 2, 2, XPathBuilder.argString2)
				},
				{
					"substring-after",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.SubstringAfter, 2, 2, XPathBuilder.argString2)
				},
				{
					"substring",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Substring, 2, 3, XPathBuilder.argFnSubstr)
				},
				{
					"string-length",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.StringLength, 0, 1, XPathBuilder.argString)
				},
				{
					"normalize-space",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Normalize, 0, 1, XPathBuilder.argString)
				},
				{
					"translate",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Translate, 3, 3, XPathBuilder.argString3)
				},
				{
					"boolean",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Boolean, 1, 1, XPathBuilder.argAny)
				},
				{
					"not",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Not, 1, 1, XPathBuilder.argBoolean)
				},
				{
					"true",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.True, 0, 0, null)
				},
				{
					"false",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.False, 0, 0, null)
				},
				{
					"lang",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Lang, 1, 1, XPathBuilder.argString)
				},
				{
					"number",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Number, 0, 1, XPathBuilder.argAny)
				},
				{
					"sum",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Sum, 1, 1, XPathBuilder.argNodeSet)
				},
				{
					"floor",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Floor, 1, 1, XPathBuilder.argDouble)
				},
				{
					"ceiling",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Ceiling, 1, 1, XPathBuilder.argDouble)
				},
				{
					"round",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Round, 1, 1, XPathBuilder.argDouble)
				}
			};
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x00149C0A File Offset: 0x00147E0A
		public static bool IsFunctionAvailable(string localName, string nsUri)
		{
			return nsUri.Length == 0 && XPathBuilder.FunctionTable.ContainsKey(localName);
		}

		// Token: 0x040025A3 RID: 9635
		private XPathQilFactory f;

		// Token: 0x040025A4 RID: 9636
		private IXPathEnvironment environment;

		// Token: 0x040025A5 RID: 9637
		private bool inTheBuild;

		// Token: 0x040025A6 RID: 9638
		protected QilNode fixupCurrent;

		// Token: 0x040025A7 RID: 9639
		protected QilNode fixupPosition;

		// Token: 0x040025A8 RID: 9640
		protected QilNode fixupLast;

		// Token: 0x040025A9 RID: 9641
		protected int numFixupCurrent;

		// Token: 0x040025AA RID: 9642
		protected int numFixupPosition;

		// Token: 0x040025AB RID: 9643
		protected int numFixupLast;

		// Token: 0x040025AC RID: 9644
		private XPathBuilder.FixupVisitor fixupVisitor;

		// Token: 0x040025AD RID: 9645
		private static XmlNodeKindFlags[] XPathNodeType2QilXmlNodeKind = new XmlNodeKindFlags[]
		{
			XmlNodeKindFlags.Document,
			XmlNodeKindFlags.Element,
			XmlNodeKindFlags.Attribute,
			XmlNodeKindFlags.Namespace,
			XmlNodeKindFlags.Text,
			XmlNodeKindFlags.Text,
			XmlNodeKindFlags.Text,
			XmlNodeKindFlags.PI,
			XmlNodeKindFlags.Comment,
			XmlNodeKindFlags.Any
		};

		// Token: 0x040025AE RID: 9646
		private static XPathBuilder.XPathOperatorGroup[] OperatorGroup = new XPathBuilder.XPathOperatorGroup[]
		{
			XPathBuilder.XPathOperatorGroup.Unknown,
			XPathBuilder.XPathOperatorGroup.Logical,
			XPathBuilder.XPathOperatorGroup.Logical,
			XPathBuilder.XPathOperatorGroup.Equality,
			XPathBuilder.XPathOperatorGroup.Equality,
			XPathBuilder.XPathOperatorGroup.Relational,
			XPathBuilder.XPathOperatorGroup.Relational,
			XPathBuilder.XPathOperatorGroup.Relational,
			XPathBuilder.XPathOperatorGroup.Relational,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Negate,
			XPathBuilder.XPathOperatorGroup.Union
		};

		// Token: 0x040025AF RID: 9647
		private static QilNodeType[] QilOperator = new QilNodeType[]
		{
			QilNodeType.Unknown,
			QilNodeType.Or,
			QilNodeType.And,
			QilNodeType.Eq,
			QilNodeType.Ne,
			QilNodeType.Lt,
			QilNodeType.Le,
			QilNodeType.Gt,
			QilNodeType.Ge,
			QilNodeType.Add,
			QilNodeType.Subtract,
			QilNodeType.Multiply,
			QilNodeType.Divide,
			QilNodeType.Modulo,
			QilNodeType.Negate,
			QilNodeType.Sequence
		};

		// Token: 0x040025B0 RID: 9648
		private static XmlNodeKindFlags[] XPathAxisMask = new XmlNodeKindFlags[]
		{
			XmlNodeKindFlags.None,
			XmlNodeKindFlags.Document | XmlNodeKindFlags.Element,
			XmlNodeKindFlags.Any,
			XmlNodeKindFlags.Attribute,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Any,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Namespace,
			XmlNodeKindFlags.Document | XmlNodeKindFlags.Element,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Any,
			XmlNodeKindFlags.Document
		};

		// Token: 0x040025B1 RID: 9649
		public static readonly XmlTypeCode[] argAny = new XmlTypeCode[] { XmlTypeCode.Item };

		// Token: 0x040025B2 RID: 9650
		public static readonly XmlTypeCode[] argNodeSet = new XmlTypeCode[] { XmlTypeCode.Node };

		// Token: 0x040025B3 RID: 9651
		public static readonly XmlTypeCode[] argBoolean = new XmlTypeCode[] { XmlTypeCode.Boolean };

		// Token: 0x040025B4 RID: 9652
		public static readonly XmlTypeCode[] argDouble = new XmlTypeCode[] { XmlTypeCode.Double };

		// Token: 0x040025B5 RID: 9653
		public static readonly XmlTypeCode[] argString = new XmlTypeCode[] { XmlTypeCode.String };

		// Token: 0x040025B6 RID: 9654
		public static readonly XmlTypeCode[] argString2 = new XmlTypeCode[]
		{
			XmlTypeCode.String,
			XmlTypeCode.String
		};

		// Token: 0x040025B7 RID: 9655
		public static readonly XmlTypeCode[] argString3 = new XmlTypeCode[]
		{
			XmlTypeCode.String,
			XmlTypeCode.String,
			XmlTypeCode.String
		};

		// Token: 0x040025B8 RID: 9656
		public static readonly XmlTypeCode[] argFnSubstr = new XmlTypeCode[]
		{
			XmlTypeCode.String,
			XmlTypeCode.Double,
			XmlTypeCode.Double
		};

		// Token: 0x040025B9 RID: 9657
		public static Dictionary<string, XPathBuilder.FunctionInfo<XPathBuilder.FuncId>> FunctionTable = XPathBuilder.CreateFunctionTable();

		// Token: 0x020005B9 RID: 1465
		private enum XPathOperatorGroup
		{
			// Token: 0x040025BB RID: 9659
			Unknown,
			// Token: 0x040025BC RID: 9660
			Logical,
			// Token: 0x040025BD RID: 9661
			Equality,
			// Token: 0x040025BE RID: 9662
			Relational,
			// Token: 0x040025BF RID: 9663
			Arithmetic,
			// Token: 0x040025C0 RID: 9664
			Negate,
			// Token: 0x040025C1 RID: 9665
			Union
		}

		// Token: 0x020005BA RID: 1466
		internal enum FuncId
		{
			// Token: 0x040025C3 RID: 9667
			Last,
			// Token: 0x040025C4 RID: 9668
			Position,
			// Token: 0x040025C5 RID: 9669
			Count,
			// Token: 0x040025C6 RID: 9670
			LocalName,
			// Token: 0x040025C7 RID: 9671
			NamespaceUri,
			// Token: 0x040025C8 RID: 9672
			Name,
			// Token: 0x040025C9 RID: 9673
			String,
			// Token: 0x040025CA RID: 9674
			Number,
			// Token: 0x040025CB RID: 9675
			Boolean,
			// Token: 0x040025CC RID: 9676
			True,
			// Token: 0x040025CD RID: 9677
			False,
			// Token: 0x040025CE RID: 9678
			Not,
			// Token: 0x040025CF RID: 9679
			Id,
			// Token: 0x040025D0 RID: 9680
			Concat,
			// Token: 0x040025D1 RID: 9681
			StartsWith,
			// Token: 0x040025D2 RID: 9682
			Contains,
			// Token: 0x040025D3 RID: 9683
			SubstringBefore,
			// Token: 0x040025D4 RID: 9684
			SubstringAfter,
			// Token: 0x040025D5 RID: 9685
			Substring,
			// Token: 0x040025D6 RID: 9686
			StringLength,
			// Token: 0x040025D7 RID: 9687
			Normalize,
			// Token: 0x040025D8 RID: 9688
			Translate,
			// Token: 0x040025D9 RID: 9689
			Lang,
			// Token: 0x040025DA RID: 9690
			Sum,
			// Token: 0x040025DB RID: 9691
			Floor,
			// Token: 0x040025DC RID: 9692
			Ceiling,
			// Token: 0x040025DD RID: 9693
			Round
		}

		// Token: 0x020005BB RID: 1467
		internal class FixupVisitor : QilReplaceVisitor
		{
			// Token: 0x06003A59 RID: 14937 RVA: 0x00149D26 File Offset: 0x00147F26
			public FixupVisitor(QilPatternFactory f, QilNode fixupCurrent, QilNode fixupPosition, QilNode fixupLast)
				: base(f.BaseFactory)
			{
				this.f = f;
				this.fixupCurrent = fixupCurrent;
				this.fixupPosition = fixupPosition;
				this.fixupLast = fixupLast;
			}

			// Token: 0x06003A5A RID: 14938 RVA: 0x00149D54 File Offset: 0x00147F54
			public QilNode Fixup(QilNode inExpr, QilIterator current, QilNode last)
			{
				QilDepthChecker.Check(inExpr);
				this.current = current;
				this.last = last;
				this.justCount = false;
				this.environment = null;
				this.numCurrent = (this.numPosition = (this.numLast = 0));
				inExpr = this.VisitAssumeReference(inExpr);
				return inExpr;
			}

			// Token: 0x06003A5B RID: 14939 RVA: 0x00149DA8 File Offset: 0x00147FA8
			public QilNode Fixup(QilNode inExpr, IXPathEnvironment environment)
			{
				QilDepthChecker.Check(inExpr);
				this.justCount = false;
				this.current = null;
				this.environment = environment;
				this.numCurrent = (this.numPosition = (this.numLast = 0));
				inExpr = this.VisitAssumeReference(inExpr);
				return inExpr;
			}

			// Token: 0x06003A5C RID: 14940 RVA: 0x00149DF4 File Offset: 0x00147FF4
			public int CountUnfixedLast(QilNode inExpr)
			{
				this.justCount = true;
				this.numCurrent = (this.numPosition = (this.numLast = 0));
				this.VisitAssumeReference(inExpr);
				return this.numLast;
			}

			// Token: 0x06003A5D RID: 14941 RVA: 0x00149E30 File Offset: 0x00148030
			protected override QilNode VisitUnknown(QilNode unknown)
			{
				if (unknown == this.fixupCurrent)
				{
					this.numCurrent++;
					if (!this.justCount)
					{
						if (this.environment != null)
						{
							unknown = this.environment.GetCurrent();
						}
						else if (this.current != null)
						{
							unknown = this.current;
						}
					}
				}
				else if (unknown == this.fixupPosition)
				{
					this.numPosition++;
					if (!this.justCount)
					{
						if (this.environment != null)
						{
							unknown = this.environment.GetPosition();
						}
						else if (this.current != null)
						{
							unknown = this.f.XsltConvert(this.f.PositionOf(this.current), XmlQueryTypeFactory.DoubleX);
						}
					}
				}
				else if (unknown == this.fixupLast)
				{
					this.numLast++;
					if (!this.justCount)
					{
						if (this.environment != null)
						{
							unknown = this.environment.GetLast();
						}
						else if (this.current != null)
						{
							unknown = this.last;
						}
					}
				}
				return unknown;
			}

			// Token: 0x040025DE RID: 9694
			private new QilPatternFactory f;

			// Token: 0x040025DF RID: 9695
			private QilNode fixupCurrent;

			// Token: 0x040025E0 RID: 9696
			private QilNode fixupPosition;

			// Token: 0x040025E1 RID: 9697
			private QilNode fixupLast;

			// Token: 0x040025E2 RID: 9698
			private QilIterator current;

			// Token: 0x040025E3 RID: 9699
			private QilNode last;

			// Token: 0x040025E4 RID: 9700
			private bool justCount;

			// Token: 0x040025E5 RID: 9701
			private IXPathEnvironment environment;

			// Token: 0x040025E6 RID: 9702
			public int numCurrent;

			// Token: 0x040025E7 RID: 9703
			public int numPosition;

			// Token: 0x040025E8 RID: 9704
			public int numLast;
		}

		// Token: 0x020005BC RID: 1468
		internal class FunctionInfo<T>
		{
			// Token: 0x06003A5E RID: 14942 RVA: 0x00149F3E File Offset: 0x0014813E
			public FunctionInfo(T id, int minArgs, int maxArgs, XmlTypeCode[] argTypes)
			{
				this.id = id;
				this.minArgs = minArgs;
				this.maxArgs = maxArgs;
				this.argTypes = argTypes;
			}

			// Token: 0x06003A5F RID: 14943 RVA: 0x00149F64 File Offset: 0x00148164
			public static void CheckArity(int minArgs, int maxArgs, string name, int numArgs)
			{
				if (minArgs <= numArgs && numArgs <= maxArgs)
				{
					return;
				}
				string text;
				if (minArgs == maxArgs)
				{
					text = "Function '{0}()' must have {1} argument(s).";
				}
				else if (maxArgs == minArgs + 1)
				{
					text = "Function '{0}()' must have {1} or {2} argument(s).";
				}
				else if (numArgs < minArgs)
				{
					text = "Function '{0}()' must have at least {1} argument(s).";
				}
				else
				{
					text = "Function '{0}()' must have no more than {2} arguments.";
				}
				throw new XPathCompileException(text, new string[]
				{
					name,
					minArgs.ToString(CultureInfo.InvariantCulture),
					maxArgs.ToString(CultureInfo.InvariantCulture)
				});
			}

			// Token: 0x06003A60 RID: 14944 RVA: 0x00149FD4 File Offset: 0x001481D4
			public void CastArguments(IList<QilNode> args, string name, XPathQilFactory f)
			{
				XPathBuilder.FunctionInfo<T>.CheckArity(this.minArgs, this.maxArgs, name, args.Count);
				if (this.maxArgs == 2147483647)
				{
					for (int i = 0; i < args.Count; i++)
					{
						args[i] = f.ConvertToType(XmlTypeCode.String, args[i]);
					}
					return;
				}
				for (int j = 0; j < args.Count; j++)
				{
					if (this.argTypes[j] == XmlTypeCode.Node && f.CannotBeNodeSet(args[j]))
					{
						throw new XPathCompileException("Argument {1} of function '{0}()' cannot be converted to a node-set.", new string[]
						{
							name,
							(j + 1).ToString(CultureInfo.InvariantCulture)
						});
					}
					args[j] = f.ConvertToType(this.argTypes[j], args[j]);
				}
			}

			// Token: 0x040025E9 RID: 9705
			public T id;

			// Token: 0x040025EA RID: 9706
			public int minArgs;

			// Token: 0x040025EB RID: 9707
			public int maxArgs;

			// Token: 0x040025EC RID: 9708
			public XmlTypeCode[] argTypes;

			// Token: 0x040025ED RID: 9709
			public const int Infinity = 2147483647;
		}
	}
}
