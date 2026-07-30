using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000553 RID: 1363
	internal class WithParamAction : VariableAction
	{
		// Token: 0x060036DB RID: 14043 RVA: 0x001328A5 File Offset: 0x00130AA5
		internal WithParamAction()
			: base(VariableType.WithParameter)
		{
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x001328B0 File Offset: 0x00130AB0
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.name, "name");
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
				if (this.selectKey != -1 && this.containedActions != null)
				{
					throw XsltException.Create("The variable or parameter '{0}' cannot have both a 'select' attribute and non-empty content.", new string[] { this.nameStr });
				}
			}
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x00132918 File Offset: 0x00130B18
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
				RecordOutput recordOutput = processor.PopOutput();
				processor.SetParameter(this.name, ((NavigatorOutput)recordOutput).Navigator);
				frame.Finished();
				return;
			}
			else
			{
				if (this.selectKey != -1)
				{
					object obj = processor.RunQuery(frame, this.selectKey);
					processor.SetParameter(this.name, obj);
					frame.Finished();
					return;
				}
				if (this.containedActions == null)
				{
					processor.SetParameter(this.name, string.Empty);
					frame.Finished();
					return;
				}
				NavigatorOutput navigatorOutput = new NavigatorOutput(this.baseUri);
				processor.PushOutput(navigatorOutput);
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
		}
	}
}
