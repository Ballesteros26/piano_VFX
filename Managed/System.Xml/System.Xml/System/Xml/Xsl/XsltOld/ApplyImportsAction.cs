using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004E6 RID: 1254
	internal class ApplyImportsAction : CompiledAction
	{
		// Token: 0x0600332B RID: 13099 RVA: 0x00124FE6 File Offset: 0x001231E6
		internal override void Compile(Compiler compiler)
		{
			base.CheckEmpty(compiler);
			if (!compiler.CanHaveApplyImports)
			{
				throw XsltException.Create("The 'xsl:apply-imports' instruction cannot be included within the content of an 'xsl:for-each' instruction or within an 'xsl:template' instruction without the 'match' attribute.", Array.Empty<string>());
			}
			this.mode = compiler.CurrentMode;
			this.stylesheet = compiler.CompiledStylesheet;
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x00125020 File Offset: 0x00123220
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state == 0)
			{
				processor.PushTemplateLookup(frame.NodeSet, this.mode, this.stylesheet);
				frame.State = 2;
				return;
			}
			if (state != 2)
			{
				return;
			}
			frame.Finished();
		}

		// Token: 0x04002111 RID: 8465
		private XmlQualifiedName mode;

		// Token: 0x04002112 RID: 8466
		private Stylesheet stylesheet;

		// Token: 0x04002113 RID: 8467
		private const int TemplateProcessed = 2;
	}
}
