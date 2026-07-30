using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000546 RID: 1350
	internal class TemplateLookupActionDbg : TemplateLookupAction
	{
		// Token: 0x0600369F RID: 13983 RVA: 0x00131E7C File Offset: 0x0013007C
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			if (this.mode == Compiler.BuiltInMode)
			{
				this.mode = processor.GetPrevioseMode();
			}
			processor.SetCurrentMode(this.mode);
			Action action;
			if (this.mode != null)
			{
				action = ((this.importsOf == null) ? processor.Stylesheet.FindTemplate(processor, frame.Node, this.mode) : this.importsOf.FindTemplateImports(processor, frame.Node, this.mode));
			}
			else
			{
				action = ((this.importsOf == null) ? processor.Stylesheet.FindTemplate(processor, frame.Node) : this.importsOf.FindTemplateImports(processor, frame.Node));
			}
			if (action == null && processor.RootAction.builtInSheet != null)
			{
				action = processor.RootAction.builtInSheet.FindTemplate(processor, frame.Node, Compiler.BuiltInMode);
			}
			if (action == null)
			{
				action = base.BuiltInTemplate(frame.Node);
			}
			if (action != null)
			{
				frame.SetAction(action);
				return;
			}
			frame.Finished();
		}
	}
}
