using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000669 RID: 1641
	internal class XmlILStateAnalyzer
	{
		// Token: 0x06004211 RID: 16913 RVA: 0x00160465 File Offset: 0x0015E665
		public XmlILStateAnalyzer(QilFactory fac)
		{
			this.fac = fac;
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x00160474 File Offset: 0x0015E674
		public virtual QilNode Analyze(QilNode ndConstr, QilNode ndContent)
		{
			if (ndConstr == null)
			{
				this.parentInfo = null;
				this.xstates = PossibleXmlStates.WithinSequence;
				this.withinElem = false;
				ndContent = this.AnalyzeContent(ndContent);
			}
			else
			{
				this.parentInfo = XmlILConstructInfo.Write(ndConstr);
				if (ndConstr.NodeType == QilNodeType.Function)
				{
					this.parentInfo.ConstructMethod = XmlILConstructMethod.Writer;
					PossibleXmlStates possibleXmlStates = PossibleXmlStates.None;
					foreach (object obj in this.parentInfo.CallersInfo)
					{
						XmlILConstructInfo xmlILConstructInfo = (XmlILConstructInfo)obj;
						if (possibleXmlStates == PossibleXmlStates.None)
						{
							possibleXmlStates = xmlILConstructInfo.InitialStates;
						}
						else if (possibleXmlStates != xmlILConstructInfo.InitialStates)
						{
							possibleXmlStates = PossibleXmlStates.Any;
						}
						xmlILConstructInfo.PushToWriterFirst = true;
					}
					this.parentInfo.InitialStates = possibleXmlStates;
				}
				else
				{
					if (ndConstr.NodeType != QilNodeType.Choice)
					{
						this.parentInfo.InitialStates = (this.parentInfo.FinalStates = PossibleXmlStates.WithinSequence);
					}
					if (ndConstr.NodeType != QilNodeType.RtfCtor)
					{
						this.parentInfo.ConstructMethod = XmlILConstructMethod.WriterThenIterator;
					}
				}
				this.withinElem = ndConstr.NodeType == QilNodeType.ElementCtor;
				QilNodeType nodeType = ndConstr.NodeType;
				if (nodeType <= QilNodeType.Function)
				{
					if (nodeType != QilNodeType.Choice)
					{
						if (nodeType == QilNodeType.Function)
						{
							this.xstates = this.parentInfo.InitialStates;
						}
					}
					else
					{
						this.xstates = PossibleXmlStates.Any;
					}
				}
				else
				{
					switch (nodeType)
					{
					case QilNodeType.ElementCtor:
						this.xstates = PossibleXmlStates.EnumAttrs;
						break;
					case QilNodeType.AttributeCtor:
						this.xstates = PossibleXmlStates.WithinAttr;
						break;
					case QilNodeType.CommentCtor:
						this.xstates = PossibleXmlStates.WithinComment;
						break;
					case QilNodeType.PICtor:
						this.xstates = PossibleXmlStates.WithinPI;
						break;
					case QilNodeType.TextCtor:
					case QilNodeType.RawTextCtor:
					case QilNodeType.NamespaceDecl:
						break;
					case QilNodeType.DocumentCtor:
						this.xstates = PossibleXmlStates.WithinContent;
						break;
					case QilNodeType.RtfCtor:
						this.xstates = PossibleXmlStates.WithinContent;
						break;
					default:
						if (nodeType != QilNodeType.XsltCopy)
						{
							if (nodeType != QilNodeType.XsltCopyOf)
							{
							}
						}
						else
						{
							this.xstates = PossibleXmlStates.Any;
						}
						break;
					}
				}
				if (ndContent != null)
				{
					ndContent = this.AnalyzeContent(ndContent);
				}
				if (ndConstr.NodeType == QilNodeType.Choice)
				{
					this.AnalyzeChoice(ndConstr as QilChoice, this.parentInfo);
				}
				if (ndConstr.NodeType == QilNodeType.Function)
				{
					this.parentInfo.FinalStates = this.xstates;
				}
			}
			return ndContent;
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x00160688 File Offset: 0x0015E888
		protected virtual QilNode AnalyzeContent(QilNode nd)
		{
			QilNodeType qilNodeType = nd.NodeType;
			if (qilNodeType - QilNodeType.For <= 2)
			{
				nd = this.fac.Nop(nd);
			}
			XmlILConstructInfo xmlILConstructInfo = XmlILConstructInfo.Write(nd);
			xmlILConstructInfo.ParentInfo = this.parentInfo;
			xmlILConstructInfo.PushToWriterLast = true;
			xmlILConstructInfo.InitialStates = this.xstates;
			qilNodeType = nd.NodeType;
			if (qilNodeType <= QilNodeType.Warning)
			{
				if (qilNodeType != QilNodeType.Nop)
				{
					if (qilNodeType - QilNodeType.Error <= 1)
					{
						xmlILConstructInfo.ConstructMethod = XmlILConstructMethod.Writer;
						goto IL_00FF;
					}
				}
				else
				{
					QilNode child = (nd as QilUnary).Child;
					qilNodeType = child.NodeType;
					if (qilNodeType - QilNodeType.For <= 2)
					{
						this.AnalyzeCopy(nd, xmlILConstructInfo);
						goto IL_00FF;
					}
					xmlILConstructInfo.ConstructMethod = XmlILConstructMethod.Writer;
					this.AnalyzeContent(child);
					goto IL_00FF;
				}
			}
			else
			{
				switch (qilNodeType)
				{
				case QilNodeType.Conditional:
					this.AnalyzeConditional(nd as QilTernary, xmlILConstructInfo);
					goto IL_00FF;
				case QilNodeType.Choice:
					this.AnalyzeChoice(nd as QilChoice, xmlILConstructInfo);
					goto IL_00FF;
				case QilNodeType.Length:
					break;
				case QilNodeType.Sequence:
					this.AnalyzeSequence(nd as QilList, xmlILConstructInfo);
					goto IL_00FF;
				default:
					if (qilNodeType == QilNodeType.Loop)
					{
						this.AnalyzeLoop(nd as QilLoop, xmlILConstructInfo);
						goto IL_00FF;
					}
					break;
				}
			}
			this.AnalyzeCopy(nd, xmlILConstructInfo);
			IL_00FF:
			xmlILConstructInfo.FinalStates = this.xstates;
			return nd;
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x001607A4 File Offset: 0x0015E9A4
		protected virtual void AnalyzeLoop(QilLoop ndLoop, XmlILConstructInfo info)
		{
			XmlQueryType xmlType = ndLoop.XmlType;
			info.ConstructMethod = XmlILConstructMethod.Writer;
			if (!xmlType.IsSingleton)
			{
				this.StartLoop(xmlType, info);
			}
			ndLoop.Body = this.AnalyzeContent(ndLoop.Body);
			if (!xmlType.IsSingleton)
			{
				this.EndLoop(xmlType, info);
			}
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x001607F4 File Offset: 0x0015E9F4
		protected virtual void AnalyzeSequence(QilList ndSeq, XmlILConstructInfo info)
		{
			info.ConstructMethod = XmlILConstructMethod.Writer;
			for (int i = 0; i < ndSeq.Count; i++)
			{
				ndSeq[i] = this.AnalyzeContent(ndSeq[i]);
			}
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x00160830 File Offset: 0x0015EA30
		protected virtual void AnalyzeConditional(QilTernary ndCond, XmlILConstructInfo info)
		{
			info.ConstructMethod = XmlILConstructMethod.Writer;
			ndCond.Center = this.AnalyzeContent(ndCond.Center);
			PossibleXmlStates possibleXmlStates = this.xstates;
			this.xstates = info.InitialStates;
			ndCond.Right = this.AnalyzeContent(ndCond.Right);
			if (possibleXmlStates != this.xstates)
			{
				this.xstates = PossibleXmlStates.Any;
			}
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x0016088C File Offset: 0x0015EA8C
		protected virtual void AnalyzeChoice(QilChoice ndChoice, XmlILConstructInfo info)
		{
			int num = ndChoice.Branches.Count - 1;
			ndChoice.Branches[num] = this.AnalyzeContent(ndChoice.Branches[num]);
			PossibleXmlStates possibleXmlStates = this.xstates;
			while (--num >= 0)
			{
				this.xstates = info.InitialStates;
				ndChoice.Branches[num] = this.AnalyzeContent(ndChoice.Branches[num]);
				if (possibleXmlStates != this.xstates)
				{
					possibleXmlStates = PossibleXmlStates.Any;
				}
			}
			this.xstates = possibleXmlStates;
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x00160914 File Offset: 0x0015EB14
		protected virtual void AnalyzeCopy(QilNode ndCopy, XmlILConstructInfo info)
		{
			XmlQueryType xmlType = ndCopy.XmlType;
			if (!xmlType.IsSingleton)
			{
				this.StartLoop(xmlType, info);
			}
			if (this.MaybeContent(xmlType))
			{
				if (this.MaybeAttrNmsp(xmlType))
				{
					if (this.xstates == PossibleXmlStates.EnumAttrs)
					{
						this.xstates = PossibleXmlStates.Any;
					}
				}
				else if (this.xstates == PossibleXmlStates.EnumAttrs || this.withinElem)
				{
					this.xstates = PossibleXmlStates.WithinContent;
				}
			}
			if (!xmlType.IsSingleton)
			{
				this.EndLoop(xmlType, info);
			}
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x00160984 File Offset: 0x0015EB84
		private void StartLoop(XmlQueryType typ, XmlILConstructInfo info)
		{
			info.BeginLoopStates = this.xstates;
			if (typ.MaybeMany && this.xstates == PossibleXmlStates.EnumAttrs && this.MaybeContent(typ))
			{
				info.BeginLoopStates = (this.xstates = PossibleXmlStates.Any);
			}
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x001609C7 File Offset: 0x0015EBC7
		private void EndLoop(XmlQueryType typ, XmlILConstructInfo info)
		{
			info.EndLoopStates = this.xstates;
			if (typ.MaybeEmpty && info.InitialStates != this.xstates)
			{
				this.xstates = PossibleXmlStates.Any;
			}
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x001609F2 File Offset: 0x0015EBF2
		private bool MaybeAttrNmsp(XmlQueryType typ)
		{
			return (typ.NodeKinds & (XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace)) > XmlNodeKindFlags.None;
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x00160A00 File Offset: 0x0015EC00
		private bool MaybeContent(XmlQueryType typ)
		{
			return !typ.IsNode || (typ.NodeKinds & ~(XmlNodeKindFlags.Attribute | XmlNodeKindFlags.Namespace)) > XmlNodeKindFlags.None;
		}

		// Token: 0x04002A62 RID: 10850
		protected XmlILConstructInfo parentInfo;

		// Token: 0x04002A63 RID: 10851
		protected QilFactory fac;

		// Token: 0x04002A64 RID: 10852
		protected PossibleXmlStates xstates;

		// Token: 0x04002A65 RID: 10853
		protected bool withinElem;
	}
}
