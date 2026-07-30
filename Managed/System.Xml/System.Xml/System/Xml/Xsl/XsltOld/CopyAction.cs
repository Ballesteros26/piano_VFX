using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004F8 RID: 1272
	internal class CopyAction : ContainerAction
	{
		// Token: 0x06003402 RID: 13314 RVA: 0x00128CA3 File Offset: 0x00126EA3
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
			if (this.containedActions == null)
			{
				this.empty = true;
			}
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x00128CD4 File Offset: 0x00126ED4
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.UseAttributeSets))
			{
				this.useAttributeSets = value;
				base.AddAction(compiler.CreateUseAttributeSetsAction());
				return true;
			}
			return false;
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x00128D24 File Offset: 0x00126F24
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			while (processor.CanContinue)
			{
				switch (frame.State)
				{
				case 0:
					if (Processor.IsRoot(frame.Node))
					{
						processor.PushActionFrame(frame);
						frame.State = 8;
						return;
					}
					if (!processor.CopyBeginEvent(frame.Node, this.empty))
					{
						return;
					}
					frame.State = 5;
					break;
				case 1:
				case 2:
				case 3:
				case 4:
					return;
				case 5:
					frame.State = 6;
					if (frame.Node.NodeType == XPathNodeType.Element)
					{
						processor.PushActionFrame(CopyNamespacesAction.GetAction(), frame.NodeSet);
						return;
					}
					break;
				case 6:
					if (frame.Node.NodeType == XPathNodeType.Element && !this.empty)
					{
						processor.PushActionFrame(frame);
						frame.State = 7;
						return;
					}
					if (!processor.CopyTextEvent(frame.Node))
					{
						return;
					}
					frame.State = 7;
					break;
				case 7:
					if (processor.CopyEndEvent(frame.Node))
					{
						frame.Finished();
						return;
					}
					return;
				case 8:
					frame.Finished();
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x04002168 RID: 8552
		private const int CopyText = 4;

		// Token: 0x04002169 RID: 8553
		private const int NamespaceCopy = 5;

		// Token: 0x0400216A RID: 8554
		private const int ContentsCopy = 6;

		// Token: 0x0400216B RID: 8555
		private const int ProcessChildren = 7;

		// Token: 0x0400216C RID: 8556
		private const int ChildrenOnly = 8;

		// Token: 0x0400216D RID: 8557
		private string useAttributeSets;

		// Token: 0x0400216E RID: 8558
		private bool empty;
	}
}
