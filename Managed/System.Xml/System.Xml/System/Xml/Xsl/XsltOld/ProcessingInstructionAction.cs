using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000531 RID: 1329
	internal class ProcessingInstructionAction : ContainerAction
	{
		// Token: 0x0600356F RID: 13679 RVA: 0x0012566C File Offset: 0x0012386C
		internal ProcessingInstructionAction()
		{
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x0012CCD8 File Offset: 0x0012AED8
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.nameAvt, "name");
			if (this.nameAvt.IsConstant)
			{
				this.name = this.nameAvt.Evaluate(null, null);
				this.nameAvt = null;
				if (!ProcessingInstructionAction.IsProcessingInstructionName(this.name))
				{
					this.name = null;
				}
			}
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x0012CD50 File Offset: 0x0012AF50
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.nameAvt = Avt.CompileAvt(compiler, value);
				return true;
			}
			return false;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x0012CD98 File Offset: 0x0012AF98
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
				if (this.nameAvt == null)
				{
					frame.StoredOutput = this.name;
					if (this.name == null)
					{
						frame.Finished();
						return;
					}
				}
				else
				{
					frame.StoredOutput = this.nameAvt.Evaluate(processor, frame);
					if (!ProcessingInstructionAction.IsProcessingInstructionName(frame.StoredOutput))
					{
						frame.Finished();
						return;
					}
				}
				break;
			case 1:
				if (!processor.EndEvent(XPathNodeType.ProcessingInstruction))
				{
					frame.State = 1;
					return;
				}
				frame.Finished();
				return;
			case 2:
				goto IL_00B5;
			case 3:
				break;
			default:
				goto IL_00B5;
			}
			if (!processor.BeginEvent(XPathNodeType.ProcessingInstruction, string.Empty, frame.StoredOutput, string.Empty, false))
			{
				frame.State = 3;
				return;
			}
			processor.PushActionFrame(frame);
			frame.State = 1;
			return;
			IL_00B5:
			frame.Finished();
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x0012CE60 File Offset: 0x0012B060
		internal static bool IsProcessingInstructionName(string name)
		{
			if (name == null)
			{
				return false;
			}
			int length = name.Length;
			int num = 0;
			XmlCharType instance = XmlCharType.Instance;
			while (num < length && instance.IsWhiteSpace(name[num]))
			{
				num++;
			}
			if (num >= length)
			{
				return false;
			}
			int num2 = ValidateNames.ParseNCName(name, num);
			if (num2 == 0)
			{
				return false;
			}
			num += num2;
			while (num < length && instance.IsWhiteSpace(name[num]))
			{
				num++;
			}
			return num >= length && (length != 3 || (name[0] != 'X' && name[0] != 'x') || (name[1] != 'M' && name[1] != 'm') || (name[2] != 'L' && name[2] != 'l'));
		}

		// Token: 0x0400221E RID: 8734
		private const int NameEvaluated = 2;

		// Token: 0x0400221F RID: 8735
		private const int NameReady = 3;

		// Token: 0x04002220 RID: 8736
		private Avt nameAvt;

		// Token: 0x04002221 RID: 8737
		private string name;

		// Token: 0x04002222 RID: 8738
		private const char CharX = 'X';

		// Token: 0x04002223 RID: 8739
		private const char Charx = 'x';

		// Token: 0x04002224 RID: 8740
		private const char CharM = 'M';

		// Token: 0x04002225 RID: 8741
		private const char Charm = 'm';

		// Token: 0x04002226 RID: 8742
		private const char CharL = 'L';

		// Token: 0x04002227 RID: 8743
		private const char Charl = 'l';
	}
}
