using System;
using System.Diagnostics;
using System.Xml.Schema;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000646 RID: 1606
	internal class QilTypeChecker
	{
		// Token: 0x06004008 RID: 16392 RVA: 0x00158ED0 File Offset: 0x001570D0
		public XmlQueryType Check(QilNode n)
		{
			switch (n.NodeType)
			{
			case QilNodeType.QilExpression:
				return this.CheckQilExpression((QilExpression)n);
			case QilNodeType.FunctionList:
				return this.CheckFunctionList((QilList)n);
			case QilNodeType.GlobalVariableList:
				return this.CheckGlobalVariableList((QilList)n);
			case QilNodeType.GlobalParameterList:
				return this.CheckGlobalParameterList((QilList)n);
			case QilNodeType.ActualParameterList:
				return this.CheckActualParameterList((QilList)n);
			case QilNodeType.FormalParameterList:
				return this.CheckFormalParameterList((QilList)n);
			case QilNodeType.SortKeyList:
				return this.CheckSortKeyList((QilList)n);
			case QilNodeType.BranchList:
				return this.CheckBranchList((QilList)n);
			case QilNodeType.OptimizeBarrier:
				return this.CheckOptimizeBarrier((QilUnary)n);
			case QilNodeType.Unknown:
				return this.CheckUnknown(n);
			case QilNodeType.DataSource:
				return this.CheckDataSource((QilDataSource)n);
			case QilNodeType.Nop:
				return this.CheckNop((QilUnary)n);
			case QilNodeType.Error:
				return this.CheckError((QilUnary)n);
			case QilNodeType.Warning:
				return this.CheckWarning((QilUnary)n);
			case QilNodeType.For:
				return this.CheckFor((QilIterator)n);
			case QilNodeType.Let:
				return this.CheckLet((QilIterator)n);
			case QilNodeType.Parameter:
				return this.CheckParameter((QilParameter)n);
			case QilNodeType.PositionOf:
				return this.CheckPositionOf((QilUnary)n);
			case QilNodeType.True:
				return this.CheckTrue(n);
			case QilNodeType.False:
				return this.CheckFalse(n);
			case QilNodeType.LiteralString:
				return this.CheckLiteralString((QilLiteral)n);
			case QilNodeType.LiteralInt32:
				return this.CheckLiteralInt32((QilLiteral)n);
			case QilNodeType.LiteralInt64:
				return this.CheckLiteralInt64((QilLiteral)n);
			case QilNodeType.LiteralDouble:
				return this.CheckLiteralDouble((QilLiteral)n);
			case QilNodeType.LiteralDecimal:
				return this.CheckLiteralDecimal((QilLiteral)n);
			case QilNodeType.LiteralQName:
				return this.CheckLiteralQName((QilName)n);
			case QilNodeType.LiteralType:
				return this.CheckLiteralType((QilLiteral)n);
			case QilNodeType.LiteralObject:
				return this.CheckLiteralObject((QilLiteral)n);
			case QilNodeType.And:
				return this.CheckAnd((QilBinary)n);
			case QilNodeType.Or:
				return this.CheckOr((QilBinary)n);
			case QilNodeType.Not:
				return this.CheckNot((QilUnary)n);
			case QilNodeType.Conditional:
				return this.CheckConditional((QilTernary)n);
			case QilNodeType.Choice:
				return this.CheckChoice((QilChoice)n);
			case QilNodeType.Length:
				return this.CheckLength((QilUnary)n);
			case QilNodeType.Sequence:
				return this.CheckSequence((QilList)n);
			case QilNodeType.Union:
				return this.CheckUnion((QilBinary)n);
			case QilNodeType.Intersection:
				return this.CheckIntersection((QilBinary)n);
			case QilNodeType.Difference:
				return this.CheckDifference((QilBinary)n);
			case QilNodeType.Average:
				return this.CheckAverage((QilUnary)n);
			case QilNodeType.Sum:
				return this.CheckSum((QilUnary)n);
			case QilNodeType.Minimum:
				return this.CheckMinimum((QilUnary)n);
			case QilNodeType.Maximum:
				return this.CheckMaximum((QilUnary)n);
			case QilNodeType.Negate:
				return this.CheckNegate((QilUnary)n);
			case QilNodeType.Add:
				return this.CheckAdd((QilBinary)n);
			case QilNodeType.Subtract:
				return this.CheckSubtract((QilBinary)n);
			case QilNodeType.Multiply:
				return this.CheckMultiply((QilBinary)n);
			case QilNodeType.Divide:
				return this.CheckDivide((QilBinary)n);
			case QilNodeType.Modulo:
				return this.CheckModulo((QilBinary)n);
			case QilNodeType.StrLength:
				return this.CheckStrLength((QilUnary)n);
			case QilNodeType.StrConcat:
				return this.CheckStrConcat((QilStrConcat)n);
			case QilNodeType.StrParseQName:
				return this.CheckStrParseQName((QilBinary)n);
			case QilNodeType.Ne:
				return this.CheckNe((QilBinary)n);
			case QilNodeType.Eq:
				return this.CheckEq((QilBinary)n);
			case QilNodeType.Gt:
				return this.CheckGt((QilBinary)n);
			case QilNodeType.Ge:
				return this.CheckGe((QilBinary)n);
			case QilNodeType.Lt:
				return this.CheckLt((QilBinary)n);
			case QilNodeType.Le:
				return this.CheckLe((QilBinary)n);
			case QilNodeType.Is:
				return this.CheckIs((QilBinary)n);
			case QilNodeType.After:
				return this.CheckAfter((QilBinary)n);
			case QilNodeType.Before:
				return this.CheckBefore((QilBinary)n);
			case QilNodeType.Loop:
				return this.CheckLoop((QilLoop)n);
			case QilNodeType.Filter:
				return this.CheckFilter((QilLoop)n);
			case QilNodeType.Sort:
				return this.CheckSort((QilLoop)n);
			case QilNodeType.SortKey:
				return this.CheckSortKey((QilSortKey)n);
			case QilNodeType.DocOrderDistinct:
				return this.CheckDocOrderDistinct((QilUnary)n);
			case QilNodeType.Function:
				return this.CheckFunction((QilFunction)n);
			case QilNodeType.Invoke:
				return this.CheckInvoke((QilInvoke)n);
			case QilNodeType.Content:
				return this.CheckContent((QilUnary)n);
			case QilNodeType.Attribute:
				return this.CheckAttribute((QilBinary)n);
			case QilNodeType.Parent:
				return this.CheckParent((QilUnary)n);
			case QilNodeType.Root:
				return this.CheckRoot((QilUnary)n);
			case QilNodeType.XmlContext:
				return this.CheckXmlContext(n);
			case QilNodeType.Descendant:
				return this.CheckDescendant((QilUnary)n);
			case QilNodeType.DescendantOrSelf:
				return this.CheckDescendantOrSelf((QilUnary)n);
			case QilNodeType.Ancestor:
				return this.CheckAncestor((QilUnary)n);
			case QilNodeType.AncestorOrSelf:
				return this.CheckAncestorOrSelf((QilUnary)n);
			case QilNodeType.Preceding:
				return this.CheckPreceding((QilUnary)n);
			case QilNodeType.FollowingSibling:
				return this.CheckFollowingSibling((QilUnary)n);
			case QilNodeType.PrecedingSibling:
				return this.CheckPrecedingSibling((QilUnary)n);
			case QilNodeType.NodeRange:
				return this.CheckNodeRange((QilBinary)n);
			case QilNodeType.Deref:
				return this.CheckDeref((QilBinary)n);
			case QilNodeType.ElementCtor:
				return this.CheckElementCtor((QilBinary)n);
			case QilNodeType.AttributeCtor:
				return this.CheckAttributeCtor((QilBinary)n);
			case QilNodeType.CommentCtor:
				return this.CheckCommentCtor((QilUnary)n);
			case QilNodeType.PICtor:
				return this.CheckPICtor((QilBinary)n);
			case QilNodeType.TextCtor:
				return this.CheckTextCtor((QilUnary)n);
			case QilNodeType.RawTextCtor:
				return this.CheckRawTextCtor((QilUnary)n);
			case QilNodeType.DocumentCtor:
				return this.CheckDocumentCtor((QilUnary)n);
			case QilNodeType.NamespaceDecl:
				return this.CheckNamespaceDecl((QilBinary)n);
			case QilNodeType.RtfCtor:
				return this.CheckRtfCtor((QilBinary)n);
			case QilNodeType.NameOf:
				return this.CheckNameOf((QilUnary)n);
			case QilNodeType.LocalNameOf:
				return this.CheckLocalNameOf((QilUnary)n);
			case QilNodeType.NamespaceUriOf:
				return this.CheckNamespaceUriOf((QilUnary)n);
			case QilNodeType.PrefixOf:
				return this.CheckPrefixOf((QilUnary)n);
			case QilNodeType.TypeAssert:
				return this.CheckTypeAssert((QilTargetType)n);
			case QilNodeType.IsType:
				return this.CheckIsType((QilTargetType)n);
			case QilNodeType.IsEmpty:
				return this.CheckIsEmpty((QilUnary)n);
			case QilNodeType.XPathNodeValue:
				return this.CheckXPathNodeValue((QilUnary)n);
			case QilNodeType.XPathFollowing:
				return this.CheckXPathFollowing((QilUnary)n);
			case QilNodeType.XPathPreceding:
				return this.CheckXPathPreceding((QilUnary)n);
			case QilNodeType.XPathNamespace:
				return this.CheckXPathNamespace((QilUnary)n);
			case QilNodeType.XsltGenerateId:
				return this.CheckXsltGenerateId((QilUnary)n);
			case QilNodeType.XsltInvokeLateBound:
				return this.CheckXsltInvokeLateBound((QilInvokeLateBound)n);
			case QilNodeType.XsltInvokeEarlyBound:
				return this.CheckXsltInvokeEarlyBound((QilInvokeEarlyBound)n);
			case QilNodeType.XsltCopy:
				return this.CheckXsltCopy((QilBinary)n);
			case QilNodeType.XsltCopyOf:
				return this.CheckXsltCopyOf((QilUnary)n);
			case QilNodeType.XsltConvert:
				return this.CheckXsltConvert((QilTargetType)n);
			default:
				return this.CheckUnknown(n);
			}
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x001595FD File Offset: 0x001577FD
		public XmlQueryType CheckQilExpression(QilExpression node)
		{
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x00159604 File Offset: 0x00157804
		public XmlQueryType CheckFunctionList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x0015964C File Offset: 0x0015784C
		public XmlQueryType CheckGlobalVariableList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x00159694 File Offset: 0x00157894
		public XmlQueryType CheckGlobalParameterList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x001596DC File Offset: 0x001578DC
		public XmlQueryType CheckActualParameterList(QilList node)
		{
			return node.XmlType;
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x001596E4 File Offset: 0x001578E4
		public XmlQueryType CheckFormalParameterList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x0015972C File Offset: 0x0015792C
		public XmlQueryType CheckSortKeyList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x001596DC File Offset: 0x001578DC
		public XmlQueryType CheckBranchList(QilList node)
		{
			return node.XmlType;
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x00159774 File Offset: 0x00157974
		public XmlQueryType CheckOptimizeBarrier(QilUnary node)
		{
			return node.Child.XmlType;
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x001596DC File Offset: 0x001578DC
		public XmlQueryType CheckUnknown(QilNode node)
		{
			return node.XmlType;
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x00159781 File Offset: 0x00157981
		public XmlQueryType CheckDataSource(QilDataSource node)
		{
			return XmlQueryTypeFactory.NodeNotRtfQ;
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x00159774 File Offset: 0x00157974
		public XmlQueryType CheckNop(QilUnary node)
		{
			return node.Child.XmlType;
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x00159788 File Offset: 0x00157988
		public XmlQueryType CheckError(QilUnary node)
		{
			return XmlQueryTypeFactory.None;
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x0015978F File Offset: 0x0015798F
		public XmlQueryType CheckWarning(QilUnary node)
		{
			return XmlQueryTypeFactory.Empty;
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x00159796 File Offset: 0x00157996
		public XmlQueryType CheckFor(QilIterator node)
		{
			return node.Binding.XmlType.Prime;
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x001597A8 File Offset: 0x001579A8
		public XmlQueryType CheckLet(QilIterator node)
		{
			return node.Binding.XmlType;
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x001596DC File Offset: 0x001578DC
		public XmlQueryType CheckParameter(QilParameter node)
		{
			return node.XmlType;
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x001597B5 File Offset: 0x001579B5
		public XmlQueryType CheckPositionOf(QilUnary node)
		{
			return XmlQueryTypeFactory.IntX;
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x001597BC File Offset: 0x001579BC
		public XmlQueryType CheckTrue(QilNode node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x001597BC File Offset: 0x001579BC
		public XmlQueryType CheckFalse(QilNode node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x001597C3 File Offset: 0x001579C3
		public XmlQueryType CheckLiteralString(QilLiteral node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x001597B5 File Offset: 0x001579B5
		public XmlQueryType CheckLiteralInt32(QilLiteral node)
		{
			return XmlQueryTypeFactory.IntX;
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x001597CA File Offset: 0x001579CA
		public XmlQueryType CheckLiteralInt64(QilLiteral node)
		{
			return XmlQueryTypeFactory.IntegerX;
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x001597D1 File Offset: 0x001579D1
		public XmlQueryType CheckLiteralDouble(QilLiteral node)
		{
			return XmlQueryTypeFactory.DoubleX;
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x001597D8 File Offset: 0x001579D8
		public XmlQueryType CheckLiteralDecimal(QilLiteral node)
		{
			return XmlQueryTypeFactory.DecimalX;
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x001597DF File Offset: 0x001579DF
		public XmlQueryType CheckLiteralQName(QilName node)
		{
			return XmlQueryTypeFactory.QNameX;
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x001597E6 File Offset: 0x001579E6
		public XmlQueryType CheckLiteralType(QilLiteral node)
		{
			return node;
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x001595FD File Offset: 0x001577FD
		public XmlQueryType CheckLiteralObject(QilLiteral node)
		{
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x001597BC File Offset: 0x001579BC
		public XmlQueryType CheckAnd(QilBinary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x001597EE File Offset: 0x001579EE
		public XmlQueryType CheckOr(QilBinary node)
		{
			return this.CheckAnd(node);
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x001597BC File Offset: 0x001579BC
		public XmlQueryType CheckNot(QilUnary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x001597F7 File Offset: 0x001579F7
		public XmlQueryType CheckConditional(QilTernary node)
		{
			return XmlQueryTypeFactory.Choice(node.Center.XmlType, node.Right.XmlType);
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x00159814 File Offset: 0x00157A14
		public XmlQueryType CheckChoice(QilChoice node)
		{
			return node.Branches.XmlType;
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x001597B5 File Offset: 0x001579B5
		public XmlQueryType CheckLength(QilUnary node)
		{
			return XmlQueryTypeFactory.IntX;
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x001596DC File Offset: 0x001578DC
		public XmlQueryType CheckSequence(QilList node)
		{
			return node.XmlType;
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x00159821 File Offset: 0x00157A21
		public XmlQueryType CheckUnion(QilBinary node)
		{
			return this.DistinctType(XmlQueryTypeFactory.Sequence(node.Left.XmlType, node.Right.XmlType));
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x00159844 File Offset: 0x00157A44
		public XmlQueryType CheckIntersection(QilBinary node)
		{
			return this.CheckUnion(node);
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x0015984D File Offset: 0x00157A4D
		public XmlQueryType CheckDifference(QilBinary node)
		{
			return XmlQueryTypeFactory.AtMost(node.Left.XmlType, node.Left.XmlType.Cardinality);
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x0015986F File Offset: 0x00157A6F
		public XmlQueryType CheckAverage(QilUnary node)
		{
			XmlQueryType xmlType = node.Child.XmlType;
			return XmlQueryTypeFactory.PrimeProduct(xmlType, xmlType.MaybeEmpty ? XmlQueryCardinality.ZeroOrOne : XmlQueryCardinality.One);
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x00159895 File Offset: 0x00157A95
		public XmlQueryType CheckSum(QilUnary node)
		{
			return this.CheckAverage(node);
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x00159895 File Offset: 0x00157A95
		public XmlQueryType CheckMinimum(QilUnary node)
		{
			return this.CheckAverage(node);
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x00159895 File Offset: 0x00157A95
		public XmlQueryType CheckMaximum(QilUnary node)
		{
			return this.CheckAverage(node);
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x00159774 File Offset: 0x00157974
		public XmlQueryType CheckNegate(QilUnary node)
		{
			return node.Child.XmlType;
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x0015989E File Offset: 0x00157A9E
		public XmlQueryType CheckAdd(QilBinary node)
		{
			if (node.Left.XmlType.TypeCode != XmlTypeCode.None)
			{
				return node.Left.XmlType;
			}
			return node.Right.XmlType;
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x001598C9 File Offset: 0x00157AC9
		public XmlQueryType CheckSubtract(QilBinary node)
		{
			return this.CheckAdd(node);
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x001598C9 File Offset: 0x00157AC9
		public XmlQueryType CheckMultiply(QilBinary node)
		{
			return this.CheckAdd(node);
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x001598C9 File Offset: 0x00157AC9
		public XmlQueryType CheckDivide(QilBinary node)
		{
			return this.CheckAdd(node);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x001598C9 File Offset: 0x00157AC9
		public XmlQueryType CheckModulo(QilBinary node)
		{
			return this.CheckAdd(node);
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x001597B5 File Offset: 0x001579B5
		public XmlQueryType CheckStrLength(QilUnary node)
		{
			return XmlQueryTypeFactory.IntX;
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x001597C3 File Offset: 0x001579C3
		public XmlQueryType CheckStrConcat(QilStrConcat node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x001597DF File Offset: 0x001579DF
		public XmlQueryType CheckStrParseQName(QilBinary node)
		{
			return XmlQueryTypeFactory.QNameX;
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x001597BC File Offset: 0x001579BC
		public XmlQueryType CheckNe(QilBinary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x001598D2 File Offset: 0x00157AD2
		public XmlQueryType CheckEq(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x001598D2 File Offset: 0x00157AD2
		public XmlQueryType CheckGt(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x001598D2 File Offset: 0x00157AD2
		public XmlQueryType CheckGe(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x001598D2 File Offset: 0x00157AD2
		public XmlQueryType CheckLt(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x001598D2 File Offset: 0x00157AD2
		public XmlQueryType CheckLe(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x001597BC File Offset: 0x001579BC
		public XmlQueryType CheckIs(QilBinary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x001598DB File Offset: 0x00157ADB
		public XmlQueryType CheckAfter(QilBinary node)
		{
			return this.CheckIs(node);
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x001598DB File Offset: 0x00157ADB
		public XmlQueryType CheckBefore(QilBinary node)
		{
			return this.CheckIs(node);
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x001598E4 File Offset: 0x00157AE4
		public XmlQueryType CheckLoop(QilLoop node)
		{
			XmlQueryType xmlType = node.Body.XmlType;
			XmlQueryCardinality xmlQueryCardinality = ((node.Variable.NodeType == QilNodeType.Let) ? XmlQueryCardinality.One : node.Variable.Binding.XmlType.Cardinality);
			return XmlQueryTypeFactory.PrimeProduct(xmlType, xmlQueryCardinality * xmlType.Cardinality);
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x0015993C File Offset: 0x00157B3C
		public XmlQueryType CheckFilter(QilLoop node)
		{
			XmlQueryType xmlQueryType = this.FindFilterType(node.Variable, node.Body);
			if (xmlQueryType != null)
			{
				return xmlQueryType;
			}
			return XmlQueryTypeFactory.AtMost(node.Variable.Binding.XmlType, node.Variable.Binding.XmlType.Cardinality);
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x00159991 File Offset: 0x00157B91
		public XmlQueryType CheckSort(QilLoop node)
		{
			XmlQueryType xmlType = node.Variable.Binding.XmlType;
			return XmlQueryTypeFactory.PrimeProduct(xmlType, xmlType.Cardinality);
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x001599AE File Offset: 0x00157BAE
		public XmlQueryType CheckSortKey(QilSortKey node)
		{
			return node.Key.XmlType;
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x001599BB File Offset: 0x00157BBB
		public XmlQueryType CheckDocOrderDistinct(QilUnary node)
		{
			return this.DistinctType(node.Child.XmlType);
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x001596DC File Offset: 0x001578DC
		public XmlQueryType CheckFunction(QilFunction node)
		{
			return node.XmlType;
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x001599CE File Offset: 0x00157BCE
		public XmlQueryType CheckInvoke(QilInvoke node)
		{
			return node.Function.XmlType;
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x001599DB File Offset: 0x00157BDB
		public XmlQueryType CheckContent(QilUnary node)
		{
			return XmlQueryTypeFactory.AttributeOrContentS;
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x001599E2 File Offset: 0x00157BE2
		public XmlQueryType CheckAttribute(QilBinary node)
		{
			return XmlQueryTypeFactory.AttributeQ;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x001599E9 File Offset: 0x00157BE9
		public XmlQueryType CheckParent(QilUnary node)
		{
			return XmlQueryTypeFactory.DocumentOrElementQ;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x001599F0 File Offset: 0x00157BF0
		public XmlQueryType CheckRoot(QilUnary node)
		{
			return XmlQueryTypeFactory.NodeNotRtf;
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x001599F0 File Offset: 0x00157BF0
		public XmlQueryType CheckXmlContext(QilNode node)
		{
			return XmlQueryTypeFactory.NodeNotRtf;
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x001599F7 File Offset: 0x00157BF7
		public XmlQueryType CheckDescendant(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x001599FE File Offset: 0x00157BFE
		public XmlQueryType CheckDescendantOrSelf(QilUnary node)
		{
			return XmlQueryTypeFactory.Choice(node.Child.XmlType, XmlQueryTypeFactory.ContentS);
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x00159A15 File Offset: 0x00157C15
		public XmlQueryType CheckAncestor(QilUnary node)
		{
			return XmlQueryTypeFactory.DocumentOrElementS;
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x00159A1C File Offset: 0x00157C1C
		public XmlQueryType CheckAncestorOrSelf(QilUnary node)
		{
			return XmlQueryTypeFactory.Choice(node.Child.XmlType, XmlQueryTypeFactory.DocumentOrElementS);
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x00159A33 File Offset: 0x00157C33
		public XmlQueryType CheckPreceding(QilUnary node)
		{
			return XmlQueryTypeFactory.DocumentOrContentS;
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x001599F7 File Offset: 0x00157BF7
		public XmlQueryType CheckFollowingSibling(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x001599F7 File Offset: 0x00157BF7
		public XmlQueryType CheckPrecedingSibling(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x00159A3A File Offset: 0x00157C3A
		public XmlQueryType CheckNodeRange(QilBinary node)
		{
			return XmlQueryTypeFactory.Choice(new XmlQueryType[]
			{
				node.Left.XmlType,
				XmlQueryTypeFactory.ContentS,
				node.Right.XmlType
			});
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x00159A6B File Offset: 0x00157C6B
		public XmlQueryType CheckDeref(QilBinary node)
		{
			return XmlQueryTypeFactory.ElementS;
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x00159A72 File Offset: 0x00157C72
		public XmlQueryType CheckElementCtor(QilBinary node)
		{
			return XmlQueryTypeFactory.UntypedElement;
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x00159A79 File Offset: 0x00157C79
		public XmlQueryType CheckAttributeCtor(QilBinary node)
		{
			return XmlQueryTypeFactory.UntypedAttribute;
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x00159A80 File Offset: 0x00157C80
		public XmlQueryType CheckCommentCtor(QilUnary node)
		{
			return XmlQueryTypeFactory.Comment;
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x00159A87 File Offset: 0x00157C87
		public XmlQueryType CheckPICtor(QilBinary node)
		{
			return XmlQueryTypeFactory.PI;
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x00159A8E File Offset: 0x00157C8E
		public XmlQueryType CheckTextCtor(QilUnary node)
		{
			return XmlQueryTypeFactory.Text;
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x00159A8E File Offset: 0x00157C8E
		public XmlQueryType CheckRawTextCtor(QilUnary node)
		{
			return XmlQueryTypeFactory.Text;
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x00159A95 File Offset: 0x00157C95
		public XmlQueryType CheckDocumentCtor(QilUnary node)
		{
			return XmlQueryTypeFactory.UntypedDocument;
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x00159A9C File Offset: 0x00157C9C
		public XmlQueryType CheckNamespaceDecl(QilBinary node)
		{
			return XmlQueryTypeFactory.Namespace;
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x00159AA3 File Offset: 0x00157CA3
		public XmlQueryType CheckRtfCtor(QilBinary node)
		{
			return XmlQueryTypeFactory.Node;
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x001597DF File Offset: 0x001579DF
		public XmlQueryType CheckNameOf(QilUnary node)
		{
			return XmlQueryTypeFactory.QNameX;
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x001597C3 File Offset: 0x001579C3
		public XmlQueryType CheckLocalNameOf(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x001597C3 File Offset: 0x001579C3
		public XmlQueryType CheckNamespaceUriOf(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x001597C3 File Offset: 0x001579C3
		public XmlQueryType CheckPrefixOf(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x001596DC File Offset: 0x001578DC
		public XmlQueryType CheckDeepCopy(QilUnary node)
		{
			return node.XmlType;
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x00159AAA File Offset: 0x00157CAA
		public XmlQueryType CheckTypeAssert(QilTargetType node)
		{
			return node.TargetType;
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x001597BC File Offset: 0x001579BC
		public XmlQueryType CheckIsType(QilTargetType node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x001597BC File Offset: 0x001579BC
		public XmlQueryType CheckIsEmpty(QilUnary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x001597C3 File Offset: 0x001579C3
		public XmlQueryType CheckXPathNodeValue(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x001599F7 File Offset: 0x00157BF7
		public XmlQueryType CheckXPathFollowing(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x001599F7 File Offset: 0x00157BF7
		public XmlQueryType CheckXPathPreceding(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x00159AB2 File Offset: 0x00157CB2
		public XmlQueryType CheckXPathNamespace(QilUnary node)
		{
			return XmlQueryTypeFactory.NamespaceS;
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x001597C3 File Offset: 0x001579C3
		public XmlQueryType CheckXsltGenerateId(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x001595FD File Offset: 0x001577FD
		public XmlQueryType CheckXsltInvokeLateBound(QilInvokeLateBound node)
		{
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x001596DC File Offset: 0x001578DC
		public XmlQueryType CheckXsltInvokeEarlyBound(QilInvokeEarlyBound node)
		{
			return node.XmlType;
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x00159AB9 File Offset: 0x00157CB9
		public XmlQueryType CheckXsltCopy(QilBinary node)
		{
			return XmlQueryTypeFactory.Choice(node.Left.XmlType, node.Right.XmlType);
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x00159AD6 File Offset: 0x00157CD6
		public XmlQueryType CheckXsltCopyOf(QilUnary node)
		{
			if ((node.Child.XmlType.NodeKinds & XmlNodeKindFlags.Document) != XmlNodeKindFlags.None)
			{
				return XmlQueryTypeFactory.NodeNotRtfS;
			}
			return node.Child.XmlType;
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x00159AAA File Offset: 0x00157CAA
		public XmlQueryType CheckXsltConvert(QilTargetType node)
		{
			return node.TargetType;
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x00159AFD File Offset: 0x00157CFD
		[Conditional("DEBUG")]
		private void Check(bool value, QilNode node, string message)
		{
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x00159B01 File Offset: 0x00157D01
		[Conditional("DEBUG")]
		private void CheckLiteralValue(QilNode node, Type clrTypeValue)
		{
			((QilLiteral)node).Value.GetType();
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckClass(QilNode node, Type clrTypeClass)
		{
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckClassAndNodeType(QilNode node, Type clrTypeClass, QilNodeType nodeType)
		{
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckXmlType(QilNode node, XmlQueryType xmlType)
		{
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckNumericX(QilNode node)
		{
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckNumericXS(QilNode node)
		{
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckAtomicX(QilNode node)
		{
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckNotDisjoint(QilBinary node)
		{
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x00159B14 File Offset: 0x00157D14
		private XmlQueryType DistinctType(XmlQueryType type)
		{
			if (type.Cardinality == XmlQueryCardinality.More)
			{
				return XmlQueryTypeFactory.PrimeProduct(type, XmlQueryCardinality.OneOrMore);
			}
			if (type.Cardinality == XmlQueryCardinality.NotOne)
			{
				return XmlQueryTypeFactory.PrimeProduct(type, XmlQueryCardinality.ZeroOrMore);
			}
			return type;
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x00159B54 File Offset: 0x00157D54
		private XmlQueryType FindFilterType(QilIterator variable, QilNode body)
		{
			if (body.XmlType.TypeCode == XmlTypeCode.None)
			{
				return XmlQueryTypeFactory.None;
			}
			QilNodeType nodeType = body.NodeType;
			if (nodeType <= QilNodeType.And)
			{
				if (nodeType == QilNodeType.False)
				{
					return XmlQueryTypeFactory.Empty;
				}
				if (nodeType == QilNodeType.And)
				{
					XmlQueryType xmlQueryType = this.FindFilterType(variable, ((QilBinary)body).Left);
					if (xmlQueryType != null)
					{
						return xmlQueryType;
					}
					return this.FindFilterType(variable, ((QilBinary)body).Right);
				}
			}
			else if (nodeType != QilNodeType.Eq)
			{
				if (nodeType == QilNodeType.IsType)
				{
					if (((QilTargetType)body).Source == variable)
					{
						return XmlQueryTypeFactory.AtMost(((QilTargetType)body).TargetType, variable.Binding.XmlType.Cardinality);
					}
				}
			}
			else
			{
				QilBinary qilBinary = (QilBinary)body;
				if (qilBinary.Left.NodeType == QilNodeType.PositionOf && ((QilUnary)qilBinary.Left).Child == variable)
				{
					return XmlQueryTypeFactory.AtMost(variable.Binding.XmlType, XmlQueryCardinality.ZeroOrOne);
				}
			}
			return null;
		}
	}
}
