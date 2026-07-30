using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200051D RID: 1309
	internal class ForEachAction : ContainerAction
	{
		// Token: 0x060034BE RID: 13502 RVA: 0x0012A128 File Offset: 0x00128328
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.selectKey != -1, "select");
			compiler.CanHaveApplyImports = false;
			if (compiler.Recurse())
			{
				this.CompileSortElements(compiler);
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x0012A178 File Offset: 0x00128378
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Select))
			{
				this.selectKey = compiler.AddQuery(value);
				return true;
			}
			return false;
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x0012A1C0 File Offset: 0x001283C0
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
				if (this.sortContainer != null)
				{
					processor.InitSortArray();
					processor.PushActionFrame(this.sortContainer, frame.NodeSet);
					frame.State = 2;
					return;
				}
				break;
			case 1:
				return;
			case 2:
				break;
			case 3:
				goto IL_0082;
			case 4:
				goto IL_009B;
			case 5:
				frame.State = 3;
				goto IL_0082;
			default:
				return;
			}
			frame.InitNewNodeSet(processor.StartQuery(frame.NodeSet, this.selectKey));
			if (this.sortContainer != null)
			{
				frame.SortNewNodeSet(processor, processor.SortArray);
			}
			frame.State = 3;
			IL_0082:
			if (!frame.NewNextNode(processor))
			{
				frame.Finished();
				return;
			}
			frame.State = 4;
			IL_009B:
			processor.PushActionFrame(frame, frame.NewNodeSet);
			frame.State = 5;
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x0012A288 File Offset: 0x00128488
		protected void CompileSortElements(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			for (;;)
			{
				switch (input.NodeType)
				{
				case XPathNodeType.Element:
					if (!Ref.Equal(input.NamespaceURI, input.Atoms.UriXsl) || !Ref.Equal(input.LocalName, input.Atoms.Sort))
					{
						return;
					}
					if (this.sortContainer == null)
					{
						this.sortContainer = new ContainerAction();
					}
					this.sortContainer.AddAction(compiler.CreateSortAction());
					break;
				case XPathNodeType.Text:
					return;
				case XPathNodeType.SignificantWhitespace:
					base.AddEvent(compiler.CreateTextEvent());
					break;
				}
				if (!input.Advance())
				{
					return;
				}
			}
		}

		// Token: 0x040021B0 RID: 8624
		private const int ProcessedSort = 2;

		// Token: 0x040021B1 RID: 8625
		private const int ProcessNextNode = 3;

		// Token: 0x040021B2 RID: 8626
		private const int PositionAdvanced = 4;

		// Token: 0x040021B3 RID: 8627
		private const int ContentsProcessed = 5;

		// Token: 0x040021B4 RID: 8628
		private int selectKey = -1;

		// Token: 0x040021B5 RID: 8629
		private ContainerAction sortContainer;
	}
}
