using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000520 RID: 1312
	internal class IfAction : ContainerAction
	{
		// Token: 0x060034D7 RID: 13527 RVA: 0x0012ACAB File Offset: 0x00128EAB
		internal IfAction(IfAction.ConditionType type)
		{
			this.type = type;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x0012ACC1 File Offset: 0x00128EC1
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			if (this.type != IfAction.ConditionType.ConditionOtherwise)
			{
				base.CheckRequiredAttribute(compiler, this.testKey != -1, "test");
			}
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x0012AD04 File Offset: 0x00128F04
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (!Ref.Equal(localName, compiler.Atoms.Test))
			{
				return false;
			}
			if (this.type == IfAction.ConditionType.ConditionOtherwise)
			{
				return false;
			}
			this.testKey = compiler.AddBooleanQuery(value);
			return true;
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x0012AD58 File Offset: 0x00128F58
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			int state = frame.State;
			if (state != 0)
			{
				if (state != 1)
				{
					return;
				}
				if (this.type == IfAction.ConditionType.ConditionWhen || this.type == IfAction.ConditionType.ConditionOtherwise)
				{
					frame.Exit();
				}
				frame.Finished();
				return;
			}
			else
			{
				if ((this.type == IfAction.ConditionType.ConditionIf || this.type == IfAction.ConditionType.ConditionWhen) && !processor.EvaluateBoolean(frame, this.testKey))
				{
					frame.Finished();
					return;
				}
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
		}

		// Token: 0x040021C2 RID: 8642
		private IfAction.ConditionType type;

		// Token: 0x040021C3 RID: 8643
		private int testKey = -1;

		// Token: 0x02000521 RID: 1313
		internal enum ConditionType
		{
			// Token: 0x040021C5 RID: 8645
			ConditionIf,
			// Token: 0x040021C6 RID: 8646
			ConditionWhen,
			// Token: 0x040021C7 RID: 8647
			ConditionOtherwise
		}
	}
}
