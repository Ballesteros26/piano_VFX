using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000545 RID: 1349
	internal class TemplateLookupAction : Action
	{
		// Token: 0x0600369B RID: 13979 RVA: 0x00131D61 File Offset: 0x0012FF61
		internal void Initialize(XmlQualifiedName mode, Stylesheet importsOf)
		{
			this.mode = mode;
			this.importsOf = importsOf;
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x00131D74 File Offset: 0x0012FF74
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			Action action;
			if (this.mode != null)
			{
				action = ((this.importsOf == null) ? processor.Stylesheet.FindTemplate(processor, frame.Node, this.mode) : this.importsOf.FindTemplateImports(processor, frame.Node, this.mode));
			}
			else
			{
				action = ((this.importsOf == null) ? processor.Stylesheet.FindTemplate(processor, frame.Node) : this.importsOf.FindTemplateImports(processor, frame.Node));
			}
			if (action == null)
			{
				action = this.BuiltInTemplate(frame.Node);
			}
			if (action != null)
			{
				frame.SetAction(action);
				return;
			}
			frame.Finished();
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x00131E20 File Offset: 0x00130020
		internal Action BuiltInTemplate(XPathNavigator node)
		{
			Action action = null;
			switch (node.NodeType)
			{
			case XPathNodeType.Root:
			case XPathNodeType.Element:
				action = ApplyTemplatesAction.BuiltInRule(this.mode);
				break;
			case XPathNodeType.Attribute:
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
				action = ValueOfAction.BuiltInRule();
				break;
			}
			return action;
		}

		// Token: 0x04002307 RID: 8967
		protected XmlQualifiedName mode;

		// Token: 0x04002308 RID: 8968
		protected Stylesheet importsOf;
	}
}
