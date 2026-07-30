using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004E7 RID: 1255
	internal class ApplyTemplatesAction : ContainerAction
	{
		// Token: 0x0600332E RID: 13102 RVA: 0x0012506A File Offset: 0x0012326A
		internal static ApplyTemplatesAction BuiltInRule()
		{
			return ApplyTemplatesAction.s_BuiltInRule;
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x00125071 File Offset: 0x00123271
		internal static ApplyTemplatesAction BuiltInRule(XmlQualifiedName mode)
		{
			if (!(mode == null) && !mode.IsEmpty)
			{
				return new ApplyTemplatesAction(mode);
			}
			return ApplyTemplatesAction.BuiltInRule();
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x00125090 File Offset: 0x00123290
		internal ApplyTemplatesAction()
		{
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x0012509F File Offset: 0x0012329F
		private ApplyTemplatesAction(XmlQualifiedName mode)
		{
			this.mode = mode;
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x001250B5 File Offset: 0x001232B5
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			this.CompileContent(compiler);
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x001250C8 File Offset: 0x001232C8
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Select))
			{
				this.selectKey = compiler.AddQuery(value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.Mode))
				{
					return false;
				}
				if (compiler.AllowBuiltInMode && value == "*")
				{
					this.mode = Compiler.BuiltInMode;
				}
				else
				{
					this.mode = compiler.CreateXPathQName(value);
				}
			}
			return true;
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x00125158 File Offset: 0x00123358
		private void CompileContent(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			if (compiler.Recurse())
			{
				for (;;)
				{
					XPathNodeType nodeType = input.NodeType;
					if (nodeType != XPathNodeType.Element)
					{
						if (nodeType - XPathNodeType.SignificantWhitespace > 3)
						{
							break;
						}
					}
					else
					{
						compiler.PushNamespaceScope();
						string namespaceURI = input.NamespaceURI;
						string localName = input.LocalName;
						if (!Ref.Equal(namespaceURI, input.Atoms.UriXsl))
						{
							goto IL_00A7;
						}
						if (Ref.Equal(localName, input.Atoms.Sort))
						{
							base.AddAction(compiler.CreateSortAction());
						}
						else
						{
							if (!Ref.Equal(localName, input.Atoms.WithParam))
							{
								goto IL_00A0;
							}
							WithParamAction withParamAction = compiler.CreateWithParamAction();
							base.CheckDuplicateParams(withParamAction.Name);
							base.AddAction(withParamAction);
						}
						compiler.PopScope();
					}
					if (!compiler.Advance())
					{
						goto Block_6;
					}
				}
				throw XsltException.Create("The contents of '{0}' are invalid.", new string[] { "apply-templates" });
				IL_00A0:
				throw compiler.UnexpectedKeyword();
				IL_00A7:
				throw compiler.UnexpectedKeyword();
				Block_6:
				compiler.ToParent();
			}
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x00125248 File Offset: 0x00123448
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
				processor.ResetParams();
				processor.InitSortArray();
				if (this.containedActions != null && this.containedActions.Count > 0)
				{
					processor.PushActionFrame(frame);
					frame.State = 2;
					return;
				}
				break;
			case 1:
				return;
			case 2:
				break;
			case 3:
				goto IL_00C2;
			case 4:
				goto IL_00DB;
			case 5:
				frame.State = 3;
				goto IL_00C2;
			default:
				return;
			}
			if (this.selectKey == -1)
			{
				if (!frame.Node.HasChildren)
				{
					frame.Finished();
					return;
				}
				frame.InitNewNodeSet(frame.Node.SelectChildren(XPathNodeType.All));
			}
			else
			{
				frame.InitNewNodeSet(processor.StartQuery(frame.NodeSet, this.selectKey));
			}
			if (processor.SortArray.Count != 0)
			{
				frame.SortNewNodeSet(processor, processor.SortArray);
			}
			frame.State = 3;
			IL_00C2:
			if (!frame.NewNextNode(processor))
			{
				frame.Finished();
				return;
			}
			frame.State = 4;
			IL_00DB:
			processor.PushTemplateLookup(frame.NewNodeSet, this.mode, null);
			frame.State = 5;
		}

		// Token: 0x04002114 RID: 8468
		private const int ProcessedChildren = 2;

		// Token: 0x04002115 RID: 8469
		private const int ProcessNextNode = 3;

		// Token: 0x04002116 RID: 8470
		private const int PositionAdvanced = 4;

		// Token: 0x04002117 RID: 8471
		private const int TemplateProcessed = 5;

		// Token: 0x04002118 RID: 8472
		private int selectKey = -1;

		// Token: 0x04002119 RID: 8473
		private XmlQualifiedName mode;

		// Token: 0x0400211A RID: 8474
		private static ApplyTemplatesAction s_BuiltInRule = new ApplyTemplatesAction();
	}
}
