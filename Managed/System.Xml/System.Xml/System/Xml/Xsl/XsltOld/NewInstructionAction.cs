using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000529 RID: 1321
	internal class NewInstructionAction : ContainerAction
	{
		// Token: 0x06003527 RID: 13607 RVA: 0x0012B734 File Offset: 0x00129934
		internal override void Compile(Compiler compiler)
		{
			XPathNavigator xpathNavigator = compiler.Input.Navigator.Clone();
			this.name = xpathNavigator.Name;
			xpathNavigator.MoveToParent();
			this.parent = xpathNavigator.Name;
			if (compiler.Recurse())
			{
				this.CompileSelectiveTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x0012B788 File Offset: 0x00129988
		internal void CompileSelectiveTemplate(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			do
			{
				if (Ref.Equal(input.NamespaceURI, input.Atoms.UriXsl) && Ref.Equal(input.LocalName, input.Atoms.Fallback))
				{
					this.fallback = true;
					if (compiler.Recurse())
					{
						base.CompileTemplate(compiler);
						compiler.ToParent();
					}
				}
			}
			while (compiler.Advance());
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x0012B7F4 File Offset: 0x001299F4
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
			}
			else
			{
				if (!this.fallback)
				{
					throw XsltException.Create("'{0}' is not a recognized extension element.", new string[] { this.name });
				}
				if (this.containedActions != null && this.containedActions.Count > 0)
				{
					processor.PushActionFrame(frame);
					frame.State = 1;
					return;
				}
			}
			frame.Finished();
		}

		// Token: 0x040021E1 RID: 8673
		private string name;

		// Token: 0x040021E2 RID: 8674
		private string parent;

		// Token: 0x040021E3 RID: 8675
		private bool fallback;
	}
}
