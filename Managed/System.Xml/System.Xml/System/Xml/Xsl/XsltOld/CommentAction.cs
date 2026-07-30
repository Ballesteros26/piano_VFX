using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004F0 RID: 1264
	internal class CommentAction : ContainerAction
	{
		// Token: 0x0600336A RID: 13162 RVA: 0x00126029 File Offset: 0x00124229
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x00126048 File Offset: 0x00124248
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
				if (processor.EndEvent(XPathNodeType.Comment))
				{
					frame.Finished();
				}
			}
			else if (processor.BeginEvent(XPathNodeType.Comment, string.Empty, string.Empty, string.Empty, false))
			{
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
		}
	}
}
