using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000649 RID: 1609
	internal abstract class QilVisitor
	{
		// Token: 0x06004089 RID: 16521 RVA: 0x00159CFF File Offset: 0x00157EFF
		protected virtual QilNode VisitAssumeReference(QilNode expr)
		{
			if (expr is QilReference)
			{
				return this.VisitReference(expr);
			}
			return this.Visit(expr);
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x00159D18 File Offset: 0x00157F18
		protected virtual QilNode VisitChildren(QilNode parent)
		{
			for (int i = 0; i < parent.Count; i++)
			{
				if (this.IsReference(parent, i))
				{
					this.VisitReference(parent[i]);
				}
				else
				{
					this.Visit(parent[i]);
				}
			}
			return parent;
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x00159D60 File Offset: 0x00157F60
		protected virtual bool IsReference(QilNode parent, int childNum)
		{
			QilNode qilNode = parent[childNum];
			if (qilNode != null)
			{
				QilNodeType qilNodeType = qilNode.NodeType;
				if (qilNodeType - QilNodeType.For <= 2)
				{
					qilNodeType = parent.NodeType;
					return qilNodeType - QilNodeType.GlobalVariableList > 1 && qilNodeType != QilNodeType.FormalParameterList && (qilNodeType - QilNodeType.Loop > 2 || childNum == 1);
				}
				if (qilNodeType == QilNodeType.Function)
				{
					return parent.NodeType == QilNodeType.Invoke;
				}
			}
			return false;
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x00159DBC File Offset: 0x00157FBC
		protected virtual QilNode Visit(QilNode n)
		{
			if (n == null)
			{
				return this.VisitNull();
			}
			switch (n.NodeType)
			{
			case QilNodeType.QilExpression:
				return this.VisitQilExpression((QilExpression)n);
			case QilNodeType.FunctionList:
				return this.VisitFunctionList((QilList)n);
			case QilNodeType.GlobalVariableList:
				return this.VisitGlobalVariableList((QilList)n);
			case QilNodeType.GlobalParameterList:
				return this.VisitGlobalParameterList((QilList)n);
			case QilNodeType.ActualParameterList:
				return this.VisitActualParameterList((QilList)n);
			case QilNodeType.FormalParameterList:
				return this.VisitFormalParameterList((QilList)n);
			case QilNodeType.SortKeyList:
				return this.VisitSortKeyList((QilList)n);
			case QilNodeType.BranchList:
				return this.VisitBranchList((QilList)n);
			case QilNodeType.OptimizeBarrier:
				return this.VisitOptimizeBarrier((QilUnary)n);
			case QilNodeType.Unknown:
				return this.VisitUnknown(n);
			case QilNodeType.DataSource:
				return this.VisitDataSource((QilDataSource)n);
			case QilNodeType.Nop:
				return this.VisitNop((QilUnary)n);
			case QilNodeType.Error:
				return this.VisitError((QilUnary)n);
			case QilNodeType.Warning:
				return this.VisitWarning((QilUnary)n);
			case QilNodeType.For:
				return this.VisitFor((QilIterator)n);
			case QilNodeType.Let:
				return this.VisitLet((QilIterator)n);
			case QilNodeType.Parameter:
				return this.VisitParameter((QilParameter)n);
			case QilNodeType.PositionOf:
				return this.VisitPositionOf((QilUnary)n);
			case QilNodeType.True:
				return this.VisitTrue(n);
			case QilNodeType.False:
				return this.VisitFalse(n);
			case QilNodeType.LiteralString:
				return this.VisitLiteralString((QilLiteral)n);
			case QilNodeType.LiteralInt32:
				return this.VisitLiteralInt32((QilLiteral)n);
			case QilNodeType.LiteralInt64:
				return this.VisitLiteralInt64((QilLiteral)n);
			case QilNodeType.LiteralDouble:
				return this.VisitLiteralDouble((QilLiteral)n);
			case QilNodeType.LiteralDecimal:
				return this.VisitLiteralDecimal((QilLiteral)n);
			case QilNodeType.LiteralQName:
				return this.VisitLiteralQName((QilName)n);
			case QilNodeType.LiteralType:
				return this.VisitLiteralType((QilLiteral)n);
			case QilNodeType.LiteralObject:
				return this.VisitLiteralObject((QilLiteral)n);
			case QilNodeType.And:
				return this.VisitAnd((QilBinary)n);
			case QilNodeType.Or:
				return this.VisitOr((QilBinary)n);
			case QilNodeType.Not:
				return this.VisitNot((QilUnary)n);
			case QilNodeType.Conditional:
				return this.VisitConditional((QilTernary)n);
			case QilNodeType.Choice:
				return this.VisitChoice((QilChoice)n);
			case QilNodeType.Length:
				return this.VisitLength((QilUnary)n);
			case QilNodeType.Sequence:
				return this.VisitSequence((QilList)n);
			case QilNodeType.Union:
				return this.VisitUnion((QilBinary)n);
			case QilNodeType.Intersection:
				return this.VisitIntersection((QilBinary)n);
			case QilNodeType.Difference:
				return this.VisitDifference((QilBinary)n);
			case QilNodeType.Average:
				return this.VisitAverage((QilUnary)n);
			case QilNodeType.Sum:
				return this.VisitSum((QilUnary)n);
			case QilNodeType.Minimum:
				return this.VisitMinimum((QilUnary)n);
			case QilNodeType.Maximum:
				return this.VisitMaximum((QilUnary)n);
			case QilNodeType.Negate:
				return this.VisitNegate((QilUnary)n);
			case QilNodeType.Add:
				return this.VisitAdd((QilBinary)n);
			case QilNodeType.Subtract:
				return this.VisitSubtract((QilBinary)n);
			case QilNodeType.Multiply:
				return this.VisitMultiply((QilBinary)n);
			case QilNodeType.Divide:
				return this.VisitDivide((QilBinary)n);
			case QilNodeType.Modulo:
				return this.VisitModulo((QilBinary)n);
			case QilNodeType.StrLength:
				return this.VisitStrLength((QilUnary)n);
			case QilNodeType.StrConcat:
				return this.VisitStrConcat((QilStrConcat)n);
			case QilNodeType.StrParseQName:
				return this.VisitStrParseQName((QilBinary)n);
			case QilNodeType.Ne:
				return this.VisitNe((QilBinary)n);
			case QilNodeType.Eq:
				return this.VisitEq((QilBinary)n);
			case QilNodeType.Gt:
				return this.VisitGt((QilBinary)n);
			case QilNodeType.Ge:
				return this.VisitGe((QilBinary)n);
			case QilNodeType.Lt:
				return this.VisitLt((QilBinary)n);
			case QilNodeType.Le:
				return this.VisitLe((QilBinary)n);
			case QilNodeType.Is:
				return this.VisitIs((QilBinary)n);
			case QilNodeType.After:
				return this.VisitAfter((QilBinary)n);
			case QilNodeType.Before:
				return this.VisitBefore((QilBinary)n);
			case QilNodeType.Loop:
				return this.VisitLoop((QilLoop)n);
			case QilNodeType.Filter:
				return this.VisitFilter((QilLoop)n);
			case QilNodeType.Sort:
				return this.VisitSort((QilLoop)n);
			case QilNodeType.SortKey:
				return this.VisitSortKey((QilSortKey)n);
			case QilNodeType.DocOrderDistinct:
				return this.VisitDocOrderDistinct((QilUnary)n);
			case QilNodeType.Function:
				return this.VisitFunction((QilFunction)n);
			case QilNodeType.Invoke:
				return this.VisitInvoke((QilInvoke)n);
			case QilNodeType.Content:
				return this.VisitContent((QilUnary)n);
			case QilNodeType.Attribute:
				return this.VisitAttribute((QilBinary)n);
			case QilNodeType.Parent:
				return this.VisitParent((QilUnary)n);
			case QilNodeType.Root:
				return this.VisitRoot((QilUnary)n);
			case QilNodeType.XmlContext:
				return this.VisitXmlContext(n);
			case QilNodeType.Descendant:
				return this.VisitDescendant((QilUnary)n);
			case QilNodeType.DescendantOrSelf:
				return this.VisitDescendantOrSelf((QilUnary)n);
			case QilNodeType.Ancestor:
				return this.VisitAncestor((QilUnary)n);
			case QilNodeType.AncestorOrSelf:
				return this.VisitAncestorOrSelf((QilUnary)n);
			case QilNodeType.Preceding:
				return this.VisitPreceding((QilUnary)n);
			case QilNodeType.FollowingSibling:
				return this.VisitFollowingSibling((QilUnary)n);
			case QilNodeType.PrecedingSibling:
				return this.VisitPrecedingSibling((QilUnary)n);
			case QilNodeType.NodeRange:
				return this.VisitNodeRange((QilBinary)n);
			case QilNodeType.Deref:
				return this.VisitDeref((QilBinary)n);
			case QilNodeType.ElementCtor:
				return this.VisitElementCtor((QilBinary)n);
			case QilNodeType.AttributeCtor:
				return this.VisitAttributeCtor((QilBinary)n);
			case QilNodeType.CommentCtor:
				return this.VisitCommentCtor((QilUnary)n);
			case QilNodeType.PICtor:
				return this.VisitPICtor((QilBinary)n);
			case QilNodeType.TextCtor:
				return this.VisitTextCtor((QilUnary)n);
			case QilNodeType.RawTextCtor:
				return this.VisitRawTextCtor((QilUnary)n);
			case QilNodeType.DocumentCtor:
				return this.VisitDocumentCtor((QilUnary)n);
			case QilNodeType.NamespaceDecl:
				return this.VisitNamespaceDecl((QilBinary)n);
			case QilNodeType.RtfCtor:
				return this.VisitRtfCtor((QilBinary)n);
			case QilNodeType.NameOf:
				return this.VisitNameOf((QilUnary)n);
			case QilNodeType.LocalNameOf:
				return this.VisitLocalNameOf((QilUnary)n);
			case QilNodeType.NamespaceUriOf:
				return this.VisitNamespaceUriOf((QilUnary)n);
			case QilNodeType.PrefixOf:
				return this.VisitPrefixOf((QilUnary)n);
			case QilNodeType.TypeAssert:
				return this.VisitTypeAssert((QilTargetType)n);
			case QilNodeType.IsType:
				return this.VisitIsType((QilTargetType)n);
			case QilNodeType.IsEmpty:
				return this.VisitIsEmpty((QilUnary)n);
			case QilNodeType.XPathNodeValue:
				return this.VisitXPathNodeValue((QilUnary)n);
			case QilNodeType.XPathFollowing:
				return this.VisitXPathFollowing((QilUnary)n);
			case QilNodeType.XPathPreceding:
				return this.VisitXPathPreceding((QilUnary)n);
			case QilNodeType.XPathNamespace:
				return this.VisitXPathNamespace((QilUnary)n);
			case QilNodeType.XsltGenerateId:
				return this.VisitXsltGenerateId((QilUnary)n);
			case QilNodeType.XsltInvokeLateBound:
				return this.VisitXsltInvokeLateBound((QilInvokeLateBound)n);
			case QilNodeType.XsltInvokeEarlyBound:
				return this.VisitXsltInvokeEarlyBound((QilInvokeEarlyBound)n);
			case QilNodeType.XsltCopy:
				return this.VisitXsltCopy((QilBinary)n);
			case QilNodeType.XsltCopyOf:
				return this.VisitXsltCopyOf((QilUnary)n);
			case QilNodeType.XsltConvert:
				return this.VisitXsltConvert((QilTargetType)n);
			default:
				return this.VisitUnknown(n);
			}
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x0015A4F4 File Offset: 0x001586F4
		protected virtual QilNode VisitReference(QilNode n)
		{
			if (n == null)
			{
				return this.VisitNull();
			}
			QilNodeType nodeType = n.NodeType;
			switch (nodeType)
			{
			case QilNodeType.For:
				return this.VisitForReference((QilIterator)n);
			case QilNodeType.Let:
				return this.VisitLetReference((QilIterator)n);
			case QilNodeType.Parameter:
				return this.VisitParameterReference((QilParameter)n);
			default:
				if (nodeType != QilNodeType.Function)
				{
					return this.VisitUnknown(n);
				}
				return this.VisitFunctionReference((QilFunction)n);
			}
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x0000365F File Offset: 0x0000185F
		protected virtual QilNode VisitNull()
		{
			return null;
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitQilExpression(QilExpression n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitFunctionList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitGlobalVariableList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitGlobalParameterList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitActualParameterList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitFormalParameterList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitSortKeyList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitBranchList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitOptimizeBarrier(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitUnknown(QilNode n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitDataSource(QilDataSource n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitNop(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitError(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitWarning(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitFor(QilIterator n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode VisitForReference(QilIterator n)
		{
			return n;
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLet(QilIterator n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode VisitLetReference(QilIterator n)
		{
			return n;
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitParameter(QilParameter n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode VisitParameterReference(QilParameter n)
		{
			return n;
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitPositionOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitTrue(QilNode n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitFalse(QilNode n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLiteralString(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLiteralInt32(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLiteralInt64(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLiteralDouble(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLiteralDecimal(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLiteralQName(QilName n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLiteralType(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLiteralObject(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitAnd(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitOr(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitNot(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitConditional(QilTernary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitChoice(QilChoice n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLength(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitSequence(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitUnion(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitIntersection(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitDifference(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitAverage(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitSum(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitMinimum(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitMaximum(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitNegate(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitAdd(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitSubtract(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitMultiply(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitDivide(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitModulo(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitStrLength(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitStrConcat(QilStrConcat n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitStrParseQName(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitNe(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitEq(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitGt(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitGe(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLt(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLe(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitIs(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitAfter(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitBefore(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLoop(QilLoop n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitFilter(QilLoop n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitSort(QilLoop n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitSortKey(QilSortKey n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitDocOrderDistinct(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitFunction(QilFunction n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode VisitFunctionReference(QilFunction n)
		{
			return n;
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitInvoke(QilInvoke n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitContent(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitAttribute(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitParent(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitRoot(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXmlContext(QilNode n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitDescendant(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitDescendantOrSelf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitAncestor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitAncestorOrSelf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitPreceding(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitFollowingSibling(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitPrecedingSibling(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitNodeRange(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitDeref(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitElementCtor(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitAttributeCtor(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitCommentCtor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitPICtor(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitTextCtor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitRawTextCtor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitDocumentCtor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitNamespaceDecl(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitRtfCtor(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitNameOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitLocalNameOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitNamespaceUriOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitPrefixOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitTypeAssert(QilTargetType n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitIsType(QilTargetType n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitIsEmpty(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXPathNodeValue(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXPathFollowing(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXPathPreceding(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXPathNamespace(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXsltGenerateId(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXsltInvokeLateBound(QilInvokeLateBound n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXsltInvokeEarlyBound(QilInvokeEarlyBound n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXsltCopy(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXsltCopyOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x0015A569 File Offset: 0x00158769
		protected virtual QilNode VisitXsltConvert(QilTargetType n)
		{
			return this.VisitChildren(n);
		}
	}
}
