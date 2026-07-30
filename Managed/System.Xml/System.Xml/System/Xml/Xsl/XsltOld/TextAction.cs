using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000549 RID: 1353
	internal class TextAction : CompiledAction
	{
		// Token: 0x060036A9 RID: 13993 RVA: 0x00132090 File Offset: 0x00130290
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			this.CompileContent(compiler);
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x001320A0 File Offset: 0x001302A0
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.DisableOutputEscaping))
			{
				this.disableOutputEscaping = compiler.GetYesNo(value);
				return true;
			}
			return false;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x001320E8 File Offset: 0x001302E8
		private void CompileContent(Compiler compiler)
		{
			if (compiler.Recurse())
			{
				NavigatorInput input = compiler.Input;
				this.text = string.Empty;
				for (;;)
				{
					XPathNodeType nodeType = input.NodeType;
					if (nodeType - XPathNodeType.Text > 2)
					{
						if (nodeType - XPathNodeType.ProcessingInstruction > 1)
						{
							break;
						}
					}
					else
					{
						this.text += input.Value;
					}
					if (!compiler.Advance())
					{
						goto Block_4;
					}
				}
				throw compiler.UnexpectedKeyword();
				Block_4:
				compiler.ToParent();
			}
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x00132154 File Offset: 0x00130354
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			if (frame.State == 0 && processor.TextEvent(this.text, this.disableOutputEscaping))
			{
				frame.Finished();
			}
		}

		// Token: 0x0400230D RID: 8973
		private bool disableOutputEscaping;

		// Token: 0x0400230E RID: 8974
		private string text;
	}
}
