using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020005A9 RID: 1449
	internal sealed class XslAstRewriter
	{
		// Token: 0x0600392F RID: 14639 RVA: 0x00140C80 File Offset: 0x0013EE80
		public void Rewrite(Compiler compiler)
		{
			this.compiler = compiler;
			this.scope = new CompilerScopeManager<VarPar>();
			this.newTemplates = new Stack<Template>();
			using (List<ProtoTemplate>.Enumerator enumerator = compiler.AllTemplates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ProtoTemplate protoTemplate = enumerator.Current;
					this.scope.EnterScope();
					this.CheckNodeCost(protoTemplate);
				}
				goto IL_009C;
			}
			IL_005F:
			Template template = this.newTemplates.Pop();
			compiler.AllTemplates.Add(template);
			compiler.NamedTemplates.Add(template.Name, template);
			this.scope.EnterScope();
			this.CheckNodeCost(template);
			IL_009C:
			if (this.newTemplates.Count <= 0)
			{
				return;
			}
			goto IL_005F;
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x00140D48 File Offset: 0x0013EF48
		private static int NodeCostForXPath(string xpath)
		{
			int num = 0;
			if (xpath != null)
			{
				num = 2;
				for (int i = 2; i < xpath.Length; i += 2)
				{
					if (xpath[i] == '/' || xpath[i - 1] == '/')
					{
						num += 2;
					}
				}
			}
			return num;
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x00140D8A File Offset: 0x0013EF8A
		private static bool NodeTypeTest(XslNodeType nodetype, int flags)
		{
			return ((flags >> (int)nodetype) & 1) != 0;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x00140D98 File Offset: 0x0013EF98
		private int CheckNodeCost(XslNode node)
		{
			this.scope.EnterScope(node.Namespaces);
			bool flag = false;
			int num = 1;
			if (XslAstRewriter.NodeTypeTest(node.NodeType, -247451132))
			{
				num += XslAstRewriter.NodeCostForXPath(node.Select);
			}
			IList<XslNode> content = node.Content;
			int num2 = content.Count - 1;
			int i = 0;
			while (i <= num2)
			{
				XslNode xslNode = content[i];
				int num3 = this.CheckNodeCost(xslNode);
				num += num3;
				if (flag && num > 100)
				{
					if (i < num2 || num3 > 1)
					{
						this.Refactor(node, i);
						num -= num3;
						num++;
						break;
					}
					break;
				}
				else
				{
					if (xslNode.NodeType == XslNodeType.Variable || xslNode.NodeType == XslNodeType.Param)
					{
						this.scope.AddVariable(xslNode.Name, (VarPar)xslNode);
						if (xslNode.NodeType == XslNodeType.Param)
						{
							num -= num3;
						}
					}
					else if (!flag)
					{
						flag = XslAstRewriter.NodeTypeTest(node.NodeType, -1025034872);
					}
					i++;
				}
			}
			this.scope.ExitScope();
			return num;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00140EA4 File Offset: 0x0013F0A4
		private void Refactor(XslNode parent, int split)
		{
			List<XslNode> list = (List<XslNode>)parent.Content;
			XslNode xslNode = list[split];
			QilName qilName = AstFactory.QName("generated", this.compiler.CreatePhantomNamespace(), "compiler");
			XsltInput.ContextInfo contextInfo = new XsltInput.ContextInfo(xslNode.SourceLine);
			XslNodeEx xslNodeEx = AstFactory.CallTemplate(qilName, contextInfo);
			XsltLoader.SetInfo(xslNodeEx, null, contextInfo);
			Template template = AstFactory.Template(qilName, null, XsltLoader.nullMode, double.NaN, xslNode.XslVersion);
			XsltLoader.SetInfo(template, null, contextInfo);
			this.newTemplates.Push(template);
			template.SetContent(new List<XslNode>(list.Count - split + 8));
			foreach (CompilerScopeManager<VarPar>.ScopeRecord scopeRecord in this.scope.GetActiveRecords())
			{
				if (!scopeRecord.IsVariable)
				{
					template.Namespaces = new NsDecl(template.Namespaces, scopeRecord.ncName, scopeRecord.nsUri);
				}
				else
				{
					VarPar value = scopeRecord.value;
					if (!this.compiler.IsPhantomNamespace(value.Name.NamespaceUri))
					{
						QilName qilName2 = AstFactory.QName(value.Name.LocalName, value.Name.NamespaceUri, value.Name.Prefix);
						VarPar varPar = AstFactory.VarPar(XslNodeType.WithParam, qilName2, "$" + qilName2.QualifiedName, XslVersion.Version10);
						XsltLoader.SetInfo(varPar, null, contextInfo);
						varPar.Namespaces = value.Namespaces;
						xslNodeEx.AddContent(varPar);
						VarPar varPar2 = AstFactory.VarPar(XslNodeType.Param, qilName2, null, XslVersion.Version10);
						XsltLoader.SetInfo(varPar2, null, contextInfo);
						varPar2.Namespaces = value.Namespaces;
						template.AddContent(varPar2);
					}
				}
			}
			for (int i = split; i < list.Count; i++)
			{
				template.AddContent(list[i]);
			}
			list[split] = xslNodeEx;
			list.RemoveRange(split + 1, list.Count - split - 1);
		}

		// Token: 0x04002530 RID: 9520
		private static readonly QilName nullMode = AstFactory.QName(string.Empty);

		// Token: 0x04002531 RID: 9521
		private CompilerScopeManager<VarPar> scope;

		// Token: 0x04002532 RID: 9522
		private Stack<Template> newTemplates;

		// Token: 0x04002533 RID: 9523
		private Compiler compiler;

		// Token: 0x04002534 RID: 9524
		private const int FixedNodeCost = 1;

		// Token: 0x04002535 RID: 9525
		private const int IteratorNodeCost = 2;

		// Token: 0x04002536 RID: 9526
		private const int CallTemplateCost = 1;

		// Token: 0x04002537 RID: 9527
		private const int RewriteThreshold = 100;

		// Token: 0x04002538 RID: 9528
		private const int NodesWithSelect = -247451132;

		// Token: 0x04002539 RID: 9529
		private const int ParentsOfCallTemplate = -1025034872;
	}
}
