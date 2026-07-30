using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200054F RID: 1359
	internal class ValueOfAction : CompiledAction
	{
		// Token: 0x060036C4 RID: 14020 RVA: 0x0013245E File Offset: 0x0013065E
		internal static Action BuiltInRule()
		{
			return ValueOfAction.s_BuiltInRule;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x00132465 File Offset: 0x00130665
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.selectKey != -1, "select");
			base.CheckEmpty(compiler);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x00132490 File Offset: 0x00130690
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Select))
			{
				this.selectKey = compiler.AddQuery(value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.DisableOutputEscaping))
				{
					return false;
				}
				this.disableOutputEscaping = compiler.GetYesNo(value);
			}
			return true;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x001324FC File Offset: 0x001306FC
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
				processor.TextEvent(frame.StoredOutput);
				frame.Finished();
				return;
			}
			else
			{
				string text = processor.ValueOf(frame, this.selectKey);
				if (processor.TextEvent(text, this.disableOutputEscaping))
				{
					frame.Finished();
					return;
				}
				frame.StoredOutput = text;
				frame.State = 2;
				return;
			}
		}

		// Token: 0x04002318 RID: 8984
		private const int ResultStored = 2;

		// Token: 0x04002319 RID: 8985
		private int selectKey = -1;

		// Token: 0x0400231A RID: 8986
		private bool disableOutputEscaping;

		// Token: 0x0400231B RID: 8987
		private static Action s_BuiltInRule = new BuiltInRuleTextAction();
	}
}
