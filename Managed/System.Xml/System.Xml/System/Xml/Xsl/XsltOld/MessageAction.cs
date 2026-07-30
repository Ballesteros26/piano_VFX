using System;
using System.Globalization;
using System.IO;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000524 RID: 1316
	internal class MessageAction : ContainerAction
	{
		// Token: 0x060034FB RID: 13563 RVA: 0x00126029 File Offset: 0x00124229
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x0012B19C File Offset: 0x0012939C
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Terminate))
			{
				this._Terminate = compiler.GetYesNo(value);
				return true;
			}
			return false;
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x0012B1E4 File Offset: 0x001293E4
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state == 0)
			{
				TextOnlyOutput textOnlyOutput = new TextOnlyOutput(processor, new StringWriter(CultureInfo.InvariantCulture));
				processor.PushOutput(textOnlyOutput);
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
			if (state != 1)
			{
				return;
			}
			TextOnlyOutput textOnlyOutput2 = processor.PopOutput() as TextOnlyOutput;
			Console.WriteLine(textOnlyOutput2.Writer.ToString());
			if (this._Terminate)
			{
				throw XsltException.Create("Transform terminated: '{0}'.", new string[] { textOnlyOutput2.Writer.ToString() });
			}
			frame.Finished();
		}

		// Token: 0x040021D1 RID: 8657
		private bool _Terminate;
	}
}
