using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020005A4 RID: 1444
	internal class XslAstAnalyzer : XslVisitor<XslFlags>
	{
		// Token: 0x060038EC RID: 14572 RVA: 0x0013F3C4 File Offset: 0x0013D5C4
		public XslFlags Analyze(Compiler compiler)
		{
			this.compiler = compiler;
			this.scope = new CompilerScopeManager<VarPar>();
			this.xpathAnalyzer = new XslAstAnalyzer.XPathAnalyzer(compiler, this.scope);
			foreach (VarPar varPar in compiler.ExternalPars)
			{
				this.scope.AddVariable(varPar.Name, varPar);
			}
			foreach (VarPar varPar2 in compiler.GlobalVars)
			{
				this.scope.AddVariable(varPar2.Name, varPar2);
			}
			foreach (VarPar varPar3 in compiler.ExternalPars)
			{
				this.Visit(varPar3);
				varPar3.Flags |= XslFlags.TypeFilter;
			}
			foreach (VarPar varPar4 in compiler.GlobalVars)
			{
				this.Visit(varPar4);
			}
			XslFlags xslFlags = XslFlags.None;
			foreach (ProtoTemplate protoTemplate in compiler.AllTemplates)
			{
				this.currentTemplate = protoTemplate;
				xslFlags |= this.Visit(protoTemplate);
			}
			foreach (ProtoTemplate protoTemplate2 in compiler.AllTemplates)
			{
				foreach (XslNode xslNode in protoTemplate2.Content)
				{
					if (xslNode.NodeType != XslNodeType.Text)
					{
						if (xslNode.NodeType != XslNodeType.Param)
						{
							break;
						}
						VarPar varPar5 = (VarPar)xslNode;
						if ((varPar5.Flags & XslFlags.MayBeDefault) != XslFlags.None)
						{
							varPar5.Flags |= varPar5.DefValueFlags;
						}
					}
				}
			}
			for (int num = 32; num != 0; num >>= 1)
			{
				this.dataFlow.PropagateFlag((XslFlags)num);
			}
			this.dataFlow = null;
			foreach (KeyValuePair<Template, Stylesheet> keyValuePair in this.fwdApplyImportsGraph)
			{
				foreach (Stylesheet stylesheet in keyValuePair.Value.Imports)
				{
					this.AddImportDependencies(stylesheet, keyValuePair.Key);
				}
			}
			this.fwdApplyImportsGraph = null;
			if ((xslFlags & XslFlags.Current) != XslFlags.None)
			{
				this.revCall0Graph.PropagateFlag(XslFlags.Current);
			}
			if ((xslFlags & XslFlags.Position) != XslFlags.None)
			{
				this.revCall0Graph.PropagateFlag(XslFlags.Position);
			}
			if ((xslFlags & XslFlags.Last) != XslFlags.None)
			{
				this.revCall0Graph.PropagateFlag(XslFlags.Last);
			}
			if ((xslFlags & XslFlags.SideEffects) != XslFlags.None)
			{
				this.PropagateSideEffectsFlag();
			}
			this.revCall0Graph = null;
			this.revCall1Graph = null;
			this.revApplyTemplatesGraph = null;
			this.FillModeFlags(compiler.Root.ModeFlags, compiler.Root.Imports[0]);
			this.TraceResults();
			return xslFlags;
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x0013F770 File Offset: 0x0013D970
		private void AddImportDependencies(Stylesheet sheet, Template focusDonor)
		{
			foreach (Template template in sheet.Templates)
			{
				if (template.Mode.Equals(focusDonor.Mode))
				{
					this.revCall0Graph.AddEdge(template, focusDonor);
				}
			}
			foreach (Stylesheet stylesheet in sheet.Imports)
			{
				this.AddImportDependencies(stylesheet, focusDonor);
			}
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x0013F800 File Offset: 0x0013DA00
		private void FillModeFlags(Dictionary<QilName, XslFlags> parentModeFlags, Stylesheet sheet)
		{
			foreach (Stylesheet stylesheet in sheet.Imports)
			{
				this.FillModeFlags(sheet.ModeFlags, stylesheet);
			}
			foreach (KeyValuePair<QilName, XslFlags> keyValuePair in sheet.ModeFlags)
			{
				XslFlags xslFlags;
				if (!parentModeFlags.TryGetValue(keyValuePair.Key, out xslFlags))
				{
					xslFlags = XslFlags.None;
				}
				parentModeFlags[keyValuePair.Key] = xslFlags | keyValuePair.Value;
			}
			foreach (Template template in sheet.Templates)
			{
				XslFlags xslFlags2 = template.Flags & (XslFlags.Current | XslFlags.Position | XslFlags.Last | XslFlags.SideEffects);
				if (xslFlags2 != XslFlags.None)
				{
					XslFlags xslFlags3;
					if (!parentModeFlags.TryGetValue(template.Mode, out xslFlags3))
					{
						xslFlags3 = XslFlags.None;
					}
					parentModeFlags[template.Mode] = xslFlags3 | xslFlags2;
				}
			}
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x00002F50 File Offset: 0x00001150
		private void TraceResults()
		{
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x0013F914 File Offset: 0x0013DB14
		protected override XslFlags Visit(XslNode node)
		{
			this.scope.EnterScope(node.Namespaces);
			XslFlags xslFlags = base.Visit(node);
			this.scope.ExitScope();
			if (this.currentTemplate != null && (node.NodeType == XslNodeType.Variable || node.NodeType == XslNodeType.Param))
			{
				this.scope.AddVariable(node.Name, (VarPar)node);
			}
			return xslFlags;
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x0013F978 File Offset: 0x0013DB78
		protected override XslFlags VisitChildren(XslNode node)
		{
			XslFlags xslFlags = XslFlags.None;
			foreach (XslNode xslNode in node.Content)
			{
				xslFlags |= this.Visit(xslNode);
			}
			return xslFlags;
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x0013F9CC File Offset: 0x0013DBCC
		protected override XslFlags VisitAttributeSet(AttributeSet node)
		{
			node.Flags = this.VisitChildren(node);
			return node.Flags;
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x0013F9CC File Offset: 0x0013DBCC
		protected override XslFlags VisitTemplate(Template node)
		{
			node.Flags = this.VisitChildren(node);
			return node.Flags;
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x0013F9E1 File Offset: 0x0013DBE1
		protected override XslFlags VisitApplyImports(XslNode node)
		{
			this.fwdApplyImportsGraph[(Template)this.currentTemplate] = (Stylesheet)node.Arg;
			return XslFlags.Rtf | XslFlags.Current | XslFlags.HasCalls;
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x0013FA0C File Offset: 0x0013DC0C
		protected override XslFlags VisitApplyTemplates(XslNode node)
		{
			XslFlags xslFlags = this.ProcessExpr(node.Select);
			foreach (XslNode xslNode in node.Content)
			{
				xslFlags |= this.Visit(xslNode);
				if (xslNode.NodeType == XslNodeType.WithParam)
				{
					XslAstAnalyzer.ModeName modeName = new XslAstAnalyzer.ModeName(node.Name, xslNode.Name);
					VarPar varPar;
					if (!this.applyTemplatesParams.TryGetValue(modeName, out varPar))
					{
						varPar = (this.applyTemplatesParams[modeName] = AstFactory.WithParam(xslNode.Name));
					}
					if (this.typeDonor != null)
					{
						this.dataFlow.AddEdge(this.typeDonor, varPar);
					}
					else
					{
						varPar.Flags |= xslNode.Flags & XslFlags.TypeFilter;
					}
				}
			}
			if (this.currentTemplate != null)
			{
				this.AddApplyTemplatesEdge(node.Name, this.currentTemplate);
			}
			return XslFlags.Rtf | XslFlags.HasCalls | xslFlags;
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x0013FB10 File Offset: 0x0013DD10
		protected override XslFlags VisitAttribute(NodeCtor node)
		{
			return XslFlags.Rtf | this.ProcessAvt(node.NameAvt) | this.ProcessAvt(node.NsAvt) | this.VisitChildren(node);
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x0013FB38 File Offset: 0x0013DD38
		protected override XslFlags VisitCallTemplate(XslNode node)
		{
			XslFlags xslFlags = XslFlags.None;
			Template template;
			if (this.compiler.NamedTemplates.TryGetValue(node.Name, out template) && this.currentTemplate != null)
			{
				if (this.forEachDepth == 0)
				{
					this.revCall0Graph.AddEdge(template, this.currentTemplate);
				}
				else
				{
					this.revCall1Graph.AddEdge(template, this.currentTemplate);
				}
			}
			VarPar[] array = new VarPar[node.Content.Count];
			int num = 0;
			foreach (XslNode xslNode in node.Content)
			{
				xslFlags |= this.Visit(xslNode);
				array[num++] = this.typeDonor;
			}
			if (template != null)
			{
				foreach (XslNode xslNode2 in template.Content)
				{
					if (xslNode2.NodeType != XslNodeType.Text)
					{
						if (xslNode2.NodeType != XslNodeType.Param)
						{
							break;
						}
						VarPar varPar = (VarPar)xslNode2;
						VarPar varPar2 = null;
						num = 0;
						foreach (XslNode xslNode3 in node.Content)
						{
							if (xslNode3.Name.Equals(varPar.Name))
							{
								varPar2 = (VarPar)xslNode3;
								this.typeDonor = array[num];
								break;
							}
							num++;
						}
						if (varPar2 != null)
						{
							if (this.typeDonor != null)
							{
								this.dataFlow.AddEdge(this.typeDonor, varPar);
							}
							else
							{
								varPar.Flags |= varPar2.Flags & XslFlags.TypeFilter;
							}
						}
						else
						{
							varPar.Flags |= XslFlags.MayBeDefault;
						}
					}
				}
			}
			return XslFlags.Rtf | XslFlags.HasCalls | xslFlags;
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x0013FD34 File Offset: 0x0013DF34
		protected override XslFlags VisitComment(XslNode node)
		{
			return XslFlags.Rtf | this.VisitChildren(node);
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x0013FD40 File Offset: 0x0013DF40
		protected override XslFlags VisitCopy(XslNode node)
		{
			return XslFlags.Rtf | XslFlags.Current | this.VisitChildren(node);
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x0013FD4F File Offset: 0x0013DF4F
		protected override XslFlags VisitCopyOf(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessExpr(node.Select);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x0013FB10 File Offset: 0x0013DD10
		protected override XslFlags VisitElement(NodeCtor node)
		{
			return XslFlags.Rtf | this.ProcessAvt(node.NameAvt) | this.ProcessAvt(node.NsAvt) | this.VisitChildren(node);
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x0013FD60 File Offset: 0x0013DF60
		protected override XslFlags VisitError(XslNode node)
		{
			return (this.VisitChildren(node) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslFlags.SideEffects;
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x0013FD74 File Offset: 0x0013DF74
		protected override XslFlags VisitForEach(XslNode node)
		{
			XslFlags xslFlags = this.ProcessExpr(node.Select);
			this.forEachDepth++;
			foreach (XslNode xslNode in node.Content)
			{
				if (xslNode.NodeType == XslNodeType.Sort)
				{
					xslFlags |= this.Visit(xslNode);
				}
				else
				{
					xslFlags |= this.Visit(xslNode) & ~(XslFlags.Current | XslFlags.Position | XslFlags.Last);
				}
			}
			this.forEachDepth--;
			return xslFlags;
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x0013FE0C File Offset: 0x0013E00C
		protected override XslFlags VisitIf(XslNode node)
		{
			return this.ProcessExpr(node.Select) | this.VisitChildren(node);
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x0013FE22 File Offset: 0x0013E022
		protected override XslFlags VisitLiteralAttribute(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessAvt(node.Select) | this.VisitChildren(node);
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x0013FD34 File Offset: 0x0013DF34
		protected override XslFlags VisitLiteralElement(XslNode node)
		{
			return XslFlags.Rtf | this.VisitChildren(node);
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x0013FD60 File Offset: 0x0013DF60
		protected override XslFlags VisitMessage(XslNode node)
		{
			return (this.VisitChildren(node) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslFlags.SideEffects;
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x0013FE3C File Offset: 0x0013E03C
		protected override XslFlags VisitNumber(Number node)
		{
			return XslFlags.Rtf | this.ProcessPattern(node.Count) | this.ProcessPattern(node.From) | ((node.Value != null) ? this.ProcessExpr(node.Value) : XslFlags.Current) | this.ProcessAvt(node.Format) | this.ProcessAvt(node.Lang) | this.ProcessAvt(node.LetterValue) | this.ProcessAvt(node.GroupingSeparator) | this.ProcessAvt(node.GroupingSize);
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x0013FE22 File Offset: 0x0013E022
		protected override XslFlags VisitPI(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessAvt(node.Select) | this.VisitChildren(node);
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x0013FEC4 File Offset: 0x0013E0C4
		protected override XslFlags VisitSort(Sort node)
		{
			return (this.ProcessExpr(node.Select) & ~(XslFlags.Current | XslFlags.Position | XslFlags.Last)) | this.ProcessAvt(node.Lang) | this.ProcessAvt(node.DataType) | this.ProcessAvt(node.Order) | this.ProcessAvt(node.CaseOrder);
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x0013FD34 File Offset: 0x0013DF34
		protected override XslFlags VisitText(Text node)
		{
			return XslFlags.Rtf | this.VisitChildren(node);
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x0013FF18 File Offset: 0x0013E118
		protected override XslFlags VisitUseAttributeSet(XslNode node)
		{
			AttributeSet attributeSet;
			if (this.compiler.AttributeSets.TryGetValue(node.Name, out attributeSet) && this.currentTemplate != null)
			{
				if (this.forEachDepth == 0)
				{
					this.revCall0Graph.AddEdge(attributeSet, this.currentTemplate);
				}
				else
				{
					this.revCall1Graph.AddEdge(attributeSet, this.currentTemplate);
				}
			}
			return XslFlags.Rtf | XslFlags.HasCalls;
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x0013FD4F File Offset: 0x0013DF4F
		protected override XslFlags VisitValueOf(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessExpr(node.Select);
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x0013FD4F File Offset: 0x0013DF4F
		protected override XslFlags VisitValueOfDoe(XslNode node)
		{
			return XslFlags.Rtf | this.ProcessExpr(node.Select);
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x0013FF7C File Offset: 0x0013E17C
		protected override XslFlags VisitParam(VarPar node)
		{
			Template template = this.currentTemplate as Template;
			if (template != null && template.Match != null)
			{
				node.Flags |= XslFlags.MayBeDefault;
				XslAstAnalyzer.ModeName modeName = new XslAstAnalyzer.ModeName(template.Mode, node.Name);
				VarPar varPar;
				if (!this.applyTemplatesParams.TryGetValue(modeName, out varPar))
				{
					varPar = (this.applyTemplatesParams[modeName] = AstFactory.WithParam(node.Name));
				}
				this.dataFlow.AddEdge(varPar, node);
			}
			node.DefValueFlags = this.ProcessVarPar(node);
			return node.DefValueFlags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x00140013 File Offset: 0x0013E213
		protected override XslFlags VisitVariable(VarPar node)
		{
			node.Flags = this.ProcessVarPar(node);
			return node.Flags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x00140013 File Offset: 0x0013E213
		protected override XslFlags VisitWithParam(VarPar node)
		{
			node.Flags = this.ProcessVarPar(node);
			return node.Flags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x0014002C File Offset: 0x0013E22C
		private XslFlags ProcessVarPar(VarPar node)
		{
			XslFlags xslFlags;
			if (node.Select != null)
			{
				if (node.Content.Count != 0)
				{
					xslFlags = this.xpathAnalyzer.Analyze(node.Select) | this.VisitChildren(node) | XslFlags.TypeFilter;
					this.typeDonor = null;
				}
				else
				{
					xslFlags = this.xpathAnalyzer.Analyze(node.Select);
					this.typeDonor = this.xpathAnalyzer.TypeDonor;
					if (this.typeDonor != null && node.NodeType != XslNodeType.WithParam)
					{
						this.dataFlow.AddEdge(this.typeDonor, node);
					}
				}
			}
			else if (node.Content.Count != 0)
			{
				xslFlags = XslFlags.Rtf | this.VisitChildren(node);
				this.typeDonor = null;
			}
			else
			{
				xslFlags = XslFlags.String;
				this.typeDonor = null;
			}
			return xslFlags;
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x001400E8 File Offset: 0x0013E2E8
		private XslFlags ProcessExpr(string expr)
		{
			return this.xpathAnalyzer.Analyze(expr) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x001400F9 File Offset: 0x0013E2F9
		private XslFlags ProcessAvt(string avt)
		{
			return this.xpathAnalyzer.AnalyzeAvt(avt) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x0014010A File Offset: 0x0013E30A
		private XslFlags ProcessPattern(string pattern)
		{
			return this.xpathAnalyzer.Analyze(pattern) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf) & ~(XslFlags.Current | XslFlags.Position | XslFlags.Last);
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x00140124 File Offset: 0x0013E324
		private void AddApplyTemplatesEdge(QilName mode, ProtoTemplate dependentTemplate)
		{
			List<ProtoTemplate> list;
			if (!this.revApplyTemplatesGraph.TryGetValue(mode, out list))
			{
				list = new List<ProtoTemplate>();
				this.revApplyTemplatesGraph.Add(mode, list);
			}
			else if (list[list.Count - 1] == dependentTemplate)
			{
				return;
			}
			list.Add(dependentTemplate);
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x00140170 File Offset: 0x0013E370
		private void PropagateSideEffectsFlag()
		{
			foreach (ProtoTemplate protoTemplate in this.revCall0Graph.Keys)
			{
				protoTemplate.Flags &= ~XslFlags.Stop;
			}
			foreach (ProtoTemplate protoTemplate2 in this.revCall1Graph.Keys)
			{
				protoTemplate2.Flags &= ~XslFlags.Stop;
			}
			foreach (ProtoTemplate protoTemplate3 in this.revCall0Graph.Keys)
			{
				if ((protoTemplate3.Flags & XslFlags.Stop) == XslFlags.None && (protoTemplate3.Flags & XslFlags.SideEffects) != XslFlags.None)
				{
					this.DepthFirstSearch(protoTemplate3);
				}
			}
			foreach (ProtoTemplate protoTemplate4 in this.revCall1Graph.Keys)
			{
				if ((protoTemplate4.Flags & XslFlags.Stop) == XslFlags.None && (protoTemplate4.Flags & XslFlags.SideEffects) != XslFlags.None)
				{
					this.DepthFirstSearch(protoTemplate4);
				}
			}
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x001402E8 File Offset: 0x0013E4E8
		private void DepthFirstSearch(ProtoTemplate t)
		{
			t.Flags |= XslFlags.SideEffects | XslFlags.Stop;
			foreach (ProtoTemplate protoTemplate in this.revCall0Graph.GetAdjList(t))
			{
				if ((protoTemplate.Flags & XslFlags.Stop) == XslFlags.None)
				{
					this.DepthFirstSearch(protoTemplate);
				}
			}
			foreach (ProtoTemplate protoTemplate2 in this.revCall1Graph.GetAdjList(t))
			{
				if ((protoTemplate2.Flags & XslFlags.Stop) == XslFlags.None)
				{
					this.DepthFirstSearch(protoTemplate2);
				}
			}
			Template template = t as Template;
			List<ProtoTemplate> list;
			if (template != null && this.revApplyTemplatesGraph.TryGetValue(template.Mode, out list))
			{
				this.revApplyTemplatesGraph.Remove(template.Mode);
				foreach (ProtoTemplate protoTemplate3 in list)
				{
					if ((protoTemplate3.Flags & XslFlags.Stop) == XslFlags.None)
					{
						this.DepthFirstSearch(protoTemplate3);
					}
				}
			}
		}

		// Token: 0x04002519 RID: 9497
		private CompilerScopeManager<VarPar> scope;

		// Token: 0x0400251A RID: 9498
		private Compiler compiler;

		// Token: 0x0400251B RID: 9499
		private int forEachDepth;

		// Token: 0x0400251C RID: 9500
		private XslAstAnalyzer.XPathAnalyzer xpathAnalyzer;

		// Token: 0x0400251D RID: 9501
		private ProtoTemplate currentTemplate;

		// Token: 0x0400251E RID: 9502
		private VarPar typeDonor;

		// Token: 0x0400251F RID: 9503
		private XslAstAnalyzer.Graph<ProtoTemplate> revCall0Graph = new XslAstAnalyzer.Graph<ProtoTemplate>();

		// Token: 0x04002520 RID: 9504
		private XslAstAnalyzer.Graph<ProtoTemplate> revCall1Graph = new XslAstAnalyzer.Graph<ProtoTemplate>();

		// Token: 0x04002521 RID: 9505
		private Dictionary<Template, Stylesheet> fwdApplyImportsGraph = new Dictionary<Template, Stylesheet>();

		// Token: 0x04002522 RID: 9506
		private Dictionary<QilName, List<ProtoTemplate>> revApplyTemplatesGraph = new Dictionary<QilName, List<ProtoTemplate>>();

		// Token: 0x04002523 RID: 9507
		private XslAstAnalyzer.Graph<VarPar> dataFlow = new XslAstAnalyzer.Graph<VarPar>();

		// Token: 0x04002524 RID: 9508
		private Dictionary<XslAstAnalyzer.ModeName, VarPar> applyTemplatesParams = new Dictionary<XslAstAnalyzer.ModeName, VarPar>();

		// Token: 0x020005A5 RID: 1445
		internal class Graph<V> : Dictionary<V, List<V>> where V : XslNode
		{
			// Token: 0x06003914 RID: 14612 RVA: 0x00140488 File Offset: 0x0013E688
			public IEnumerable<V> GetAdjList(V v)
			{
				List<V> list;
				if (base.TryGetValue(v, out list) && list != null)
				{
					return list;
				}
				return XslAstAnalyzer.Graph<V>.empty;
			}

			// Token: 0x06003915 RID: 14613 RVA: 0x001404AC File Offset: 0x0013E6AC
			public void AddEdge(V v1, V v2)
			{
				if (v1 == v2)
				{
					return;
				}
				List<V> list;
				if (!base.TryGetValue(v1, out list) || list == null)
				{
					list = (base[v1] = new List<V>());
				}
				list.Add(v2);
				if (!base.TryGetValue(v2, out list))
				{
					base[v2] = null;
				}
			}

			// Token: 0x06003916 RID: 14614 RVA: 0x00140500 File Offset: 0x0013E700
			public void PropagateFlag(XslFlags flag)
			{
				foreach (V v in base.Keys)
				{
					v.Flags &= ~XslFlags.Stop;
				}
				foreach (V v2 in base.Keys)
				{
					if ((v2.Flags & XslFlags.Stop) == XslFlags.None && (v2.Flags & flag) != XslFlags.None)
					{
						this.DepthFirstSearch(v2, flag);
					}
				}
			}

			// Token: 0x06003917 RID: 14615 RVA: 0x001405C8 File Offset: 0x0013E7C8
			private void DepthFirstSearch(V v, XslFlags flag)
			{
				v.Flags |= flag | XslFlags.Stop;
				foreach (V v2 in this.GetAdjList(v))
				{
					if ((v2.Flags & XslFlags.Stop) == XslFlags.None)
					{
						this.DepthFirstSearch(v2, flag);
					}
				}
			}

			// Token: 0x04002525 RID: 9509
			private static IList<V> empty = new List<V>().AsReadOnly();
		}

		// Token: 0x020005A6 RID: 1446
		internal struct ModeName
		{
			// Token: 0x0600391A RID: 14618 RVA: 0x0014065D File Offset: 0x0013E85D
			public ModeName(QilName mode, QilName name)
			{
				this.Mode = mode;
				this.Name = name;
			}

			// Token: 0x0600391B RID: 14619 RVA: 0x0014066D File Offset: 0x0013E86D
			public override int GetHashCode()
			{
				return this.Mode.GetHashCode() ^ this.Name.GetHashCode();
			}

			// Token: 0x04002526 RID: 9510
			public QilName Mode;

			// Token: 0x04002527 RID: 9511
			public QilName Name;
		}

		// Token: 0x020005A7 RID: 1447
		internal struct NullErrorHelper : IErrorHelper
		{
			// Token: 0x0600391C RID: 14620 RVA: 0x00002F50 File Offset: 0x00001150
			public void ReportError(string res, params string[] args)
			{
			}

			// Token: 0x0600391D RID: 14621 RVA: 0x00002F50 File Offset: 0x00001150
			public void ReportWarning(string res, params string[] args)
			{
			}
		}

		// Token: 0x020005A8 RID: 1448
		internal class XPathAnalyzer : IXPathBuilder<XslFlags>
		{
			// Token: 0x17000BCA RID: 3018
			// (get) Token: 0x0600391E RID: 14622 RVA: 0x00140686 File Offset: 0x0013E886
			public VarPar TypeDonor
			{
				get
				{
					return this.typeDonor;
				}
			}

			// Token: 0x0600391F RID: 14623 RVA: 0x0014068E File Offset: 0x0013E88E
			public XPathAnalyzer(Compiler compiler, CompilerScopeManager<VarPar> scope)
			{
				this.compiler = compiler;
				this.scope = scope;
			}

			// Token: 0x06003920 RID: 14624 RVA: 0x001406B0 File Offset: 0x0013E8B0
			public XslFlags Analyze(string xpathExpr)
			{
				this.typeDonor = null;
				if (xpathExpr == null)
				{
					return XslFlags.None;
				}
				XslFlags xslFlags2;
				try
				{
					this.xsltCurrentNeeded = false;
					XPathScanner xpathScanner = new XPathScanner(xpathExpr);
					XslFlags xslFlags = this.xpathParser.Parse(xpathScanner, this, LexKind.Eof);
					if (this.xsltCurrentNeeded)
					{
						xslFlags |= XslFlags.Current;
					}
					xslFlags2 = xslFlags;
				}
				catch (XslLoadException)
				{
					xslFlags2 = XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf | XslFlags.Current | XslFlags.Position | XslFlags.Last;
				}
				return xslFlags2;
			}

			// Token: 0x06003921 RID: 14625 RVA: 0x00140718 File Offset: 0x0013E918
			public XslFlags AnalyzeAvt(string source)
			{
				this.typeDonor = null;
				if (source == null)
				{
					return XslFlags.None;
				}
				XslFlags xslFlags2;
				try
				{
					this.xsltCurrentNeeded = false;
					XslFlags xslFlags = XslFlags.None;
					int i = 0;
					while (i < source.Length)
					{
						i = source.IndexOf('{', i);
						if (i == -1)
						{
							break;
						}
						i++;
						if (i < source.Length && source[i] == '{')
						{
							i++;
						}
						else if (i < source.Length)
						{
							XPathScanner xpathScanner = new XPathScanner(source, i);
							xslFlags |= this.xpathParser.Parse(xpathScanner, this, LexKind.RBrace);
							i = xpathScanner.LexStart + 1;
						}
					}
					if (this.xsltCurrentNeeded)
					{
						xslFlags |= XslFlags.Current;
					}
					xslFlags2 = xslFlags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf);
				}
				catch (XslLoadException)
				{
					xslFlags2 = XslFlags.FocusFilter;
				}
				return xslFlags2;
			}

			// Token: 0x06003922 RID: 14626 RVA: 0x001407D4 File Offset: 0x0013E9D4
			private VarPar ResolveVariable(string prefix, string name)
			{
				string text = this.ResolvePrefix(prefix);
				if (text == null)
				{
					return null;
				}
				return this.scope.LookupVariable(name, text);
			}

			// Token: 0x06003923 RID: 14627 RVA: 0x001407FB File Offset: 0x0013E9FB
			private string ResolvePrefix(string prefix)
			{
				if (prefix.Length == 0)
				{
					return string.Empty;
				}
				return this.scope.LookupNamespace(prefix);
			}

			// Token: 0x06003924 RID: 14628 RVA: 0x00002F50 File Offset: 0x00001150
			public virtual void StartBuild()
			{
			}

			// Token: 0x06003925 RID: 14629 RVA: 0x0000206B File Offset: 0x0000026B
			public virtual XslFlags EndBuild(XslFlags result)
			{
				return result;
			}

			// Token: 0x06003926 RID: 14630 RVA: 0x00140817 File Offset: 0x0013EA17
			public virtual XslFlags String(string value)
			{
				this.typeDonor = null;
				return XslFlags.String;
			}

			// Token: 0x06003927 RID: 14631 RVA: 0x00140821 File Offset: 0x0013EA21
			public virtual XslFlags Number(double value)
			{
				this.typeDonor = null;
				return XslFlags.Number;
			}

			// Token: 0x06003928 RID: 14632 RVA: 0x0014082B File Offset: 0x0013EA2B
			public virtual XslFlags Operator(XPathOperator op, XslFlags left, XslFlags right)
			{
				this.typeDonor = null;
				return ((left | right) & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslAstAnalyzer.XPathAnalyzer.OperatorType[(int)op];
			}

			// Token: 0x06003929 RID: 14633 RVA: 0x00140842 File Offset: 0x0013EA42
			public virtual XslFlags Axis(XPathAxis xpathAxis, XPathNodeType nodeType, string prefix, string name)
			{
				this.typeDonor = null;
				if (xpathAxis == XPathAxis.Self && nodeType == XPathNodeType.All && prefix == null && name == null)
				{
					return XslFlags.Node | XslFlags.Current;
				}
				return XslFlags.Nodeset | XslFlags.Current;
			}

			// Token: 0x0600392A RID: 14634 RVA: 0x00140867 File Offset: 0x0013EA67
			public virtual XslFlags JoinStep(XslFlags left, XslFlags right)
			{
				this.typeDonor = null;
				return (left & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslFlags.Nodeset;
			}

			// Token: 0x0600392B RID: 14635 RVA: 0x00140877 File Offset: 0x0013EA77
			public virtual XslFlags Predicate(XslFlags nodeset, XslFlags predicate, bool isReverseStep)
			{
				this.typeDonor = null;
				return (nodeset & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | XslFlags.Nodeset | (predicate & XslFlags.SideEffects);
			}

			// Token: 0x0600392C RID: 14636 RVA: 0x0014088F File Offset: 0x0013EA8F
			public virtual XslFlags Variable(string prefix, string name)
			{
				this.typeDonor = this.ResolveVariable(prefix, name);
				if (this.typeDonor == null)
				{
					return XslFlags.TypeFilter;
				}
				return XslFlags.None;
			}

			// Token: 0x0600392D RID: 14637 RVA: 0x001408AC File Offset: 0x0013EAAC
			public virtual XslFlags Function(string prefix, string name, IList<XslFlags> args)
			{
				this.typeDonor = null;
				XslFlags xslFlags = XslFlags.None;
				foreach (XslFlags xslFlags2 in args)
				{
					xslFlags |= xslFlags2;
				}
				XslFlags xslFlags3 = XslFlags.None;
				if (prefix.Length == 0)
				{
					XPathBuilder.FunctionInfo<XPathBuilder.FuncId> functionInfo;
					XPathBuilder.FunctionInfo<QilGenerator.FuncId> functionInfo2;
					if (XPathBuilder.FunctionTable.TryGetValue(name, out functionInfo))
					{
						XPathBuilder.FuncId id = functionInfo.id;
						xslFlags3 = XslAstAnalyzer.XPathAnalyzer.XPathFunctionFlags[(int)id];
						if (args.Count == 0 && (id == XPathBuilder.FuncId.LocalName || id == XPathBuilder.FuncId.NamespaceUri || id == XPathBuilder.FuncId.Name || id == XPathBuilder.FuncId.String || id == XPathBuilder.FuncId.Number || id == XPathBuilder.FuncId.StringLength || id == XPathBuilder.FuncId.Normalize))
						{
							xslFlags3 |= XslFlags.Current;
						}
					}
					else if (QilGenerator.FunctionTable.TryGetValue(name, out functionInfo2))
					{
						QilGenerator.FuncId id2 = functionInfo2.id;
						xslFlags3 = XslAstAnalyzer.XPathAnalyzer.XsltFunctionFlags[(int)id2];
						if (id2 == QilGenerator.FuncId.Current)
						{
							this.xsltCurrentNeeded = true;
						}
						else if (id2 == QilGenerator.FuncId.GenerateId && args.Count == 0)
						{
							xslFlags3 |= XslFlags.Current;
						}
					}
				}
				else
				{
					string text = this.ResolvePrefix(prefix);
					if (text == "urn:schemas-microsoft-com:xslt")
					{
						uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
						if (num <= 1033099933U)
						{
							if (num <= 467038368U)
							{
								if (num != 325300801U)
								{
									if (num == 467038368U)
									{
										if (name == "number")
										{
											xslFlags3 = XslFlags.Number;
										}
									}
								}
								else if (name == "format-date")
								{
									xslFlags3 = XslFlags.String;
								}
							}
							else if (num != 999037500U)
							{
								if (num == 1033099933U)
								{
									if (name == "utc")
									{
										xslFlags3 = XslFlags.String;
									}
								}
							}
							else if (name == "local-name")
							{
								xslFlags3 = XslFlags.String;
							}
						}
						else if (num <= 2518485839U)
						{
							if (num != 2056321742U)
							{
								if (num == 2518485839U)
								{
									if (name == "namespace-uri")
									{
										xslFlags3 = XslFlags.String | XslFlags.Current;
									}
								}
							}
							else if (name == "string-compare")
							{
								xslFlags3 = XslFlags.Number;
							}
						}
						else if (num != 3208980016U)
						{
							if (num == 3804234668U)
							{
								if (name == "format-time")
								{
									xslFlags3 = XslFlags.String;
								}
							}
						}
						else if (name == "node-set")
						{
							xslFlags3 = XslFlags.Nodeset;
						}
					}
					else if (text == "http://exslt.org/common")
					{
						if (!(name == "node-set"))
						{
							if (name == "object-type")
							{
								xslFlags3 = XslFlags.String;
							}
						}
						else
						{
							xslFlags3 = XslFlags.Nodeset;
						}
					}
					if (xslFlags3 == XslFlags.None)
					{
						xslFlags3 = XslFlags.TypeFilter;
						if (this.compiler.Settings.EnableScript && text != null)
						{
							XmlExtensionFunction xmlExtensionFunction = this.compiler.Scripts.ResolveFunction(name, text, args.Count, default(XslAstAnalyzer.NullErrorHelper));
							if (xmlExtensionFunction != null)
							{
								XmlQueryType xmlReturnType = xmlExtensionFunction.XmlReturnType;
								if (xmlReturnType == XmlQueryTypeFactory.StringX)
								{
									xslFlags3 = XslFlags.String;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.DoubleX)
								{
									xslFlags3 = XslFlags.Number;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.BooleanX)
								{
									xslFlags3 = XslFlags.Boolean;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.NodeNotRtf)
								{
									xslFlags3 = XslFlags.Node;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.NodeSDod)
								{
									xslFlags3 = XslFlags.Nodeset;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.ItemS)
								{
									xslFlags3 = XslFlags.TypeFilter;
								}
								else if (xmlReturnType == XmlQueryTypeFactory.Empty)
								{
									xslFlags3 = XslFlags.Nodeset;
								}
							}
						}
						xslFlags3 |= XslFlags.SideEffects;
					}
				}
				return (xslFlags & ~(XslFlags.String | XslFlags.Number | XslFlags.Boolean | XslFlags.Node | XslFlags.Nodeset | XslFlags.Rtf)) | xslFlags3;
			}

			// Token: 0x04002528 RID: 9512
			private XPathParser<XslFlags> xpathParser = new XPathParser<XslFlags>();

			// Token: 0x04002529 RID: 9513
			private CompilerScopeManager<VarPar> scope;

			// Token: 0x0400252A RID: 9514
			private Compiler compiler;

			// Token: 0x0400252B RID: 9515
			private bool xsltCurrentNeeded;

			// Token: 0x0400252C RID: 9516
			private VarPar typeDonor;

			// Token: 0x0400252D RID: 9517
			private static XslFlags[] OperatorType = new XslFlags[]
			{
				XslFlags.TypeFilter,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Nodeset
			};

			// Token: 0x0400252E RID: 9518
			private static XslFlags[] XPathFunctionFlags = new XslFlags[]
			{
				XslFlags.Number | XslFlags.Last,
				XslFlags.Number | XslFlags.Position,
				XslFlags.Number,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String,
				XslFlags.Number,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.Nodeset | XslFlags.Current,
				XslFlags.String,
				XslFlags.Boolean,
				XslFlags.Boolean,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String,
				XslFlags.Number,
				XslFlags.String,
				XslFlags.String,
				XslFlags.Boolean | XslFlags.Current,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number,
				XslFlags.Number
			};

			// Token: 0x0400252F RID: 9519
			private static XslFlags[] XsltFunctionFlags = new XslFlags[]
			{
				XslFlags.Node,
				XslFlags.Nodeset,
				XslFlags.Nodeset | XslFlags.Current,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String,
				XslFlags.String | XslFlags.Number,
				XslFlags.Boolean,
				XslFlags.Boolean
			};
		}
	}
}
