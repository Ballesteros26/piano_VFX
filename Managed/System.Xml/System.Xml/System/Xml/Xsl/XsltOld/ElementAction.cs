using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200051A RID: 1306
	internal class ElementAction : ContainerAction
	{
		// Token: 0x060034B2 RID: 13490 RVA: 0x0012566C File Offset: 0x0012386C
		internal ElementAction()
		{
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x00129E74 File Offset: 0x00128074
		private static PrefixQName CreateElementQName(string name, string nsUri, InputScopeManager manager)
		{
			if (nsUri == "http://www.w3.org/2000/xmlns/")
			{
				throw XsltException.Create("Elements and attributes cannot belong to the reserved namespace '{0}'.", new string[] { nsUri });
			}
			PrefixQName prefixQName = new PrefixQName();
			prefixQName.SetQName(name);
			if (nsUri == null)
			{
				prefixQName.Namespace = manager.ResolveXmlNamespace(prefixQName.Prefix);
			}
			else
			{
				prefixQName.Namespace = nsUri;
			}
			return prefixQName;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x00129ED0 File Offset: 0x001280D0
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.nameAvt, "name");
			this.name = CompiledAction.PrecalculateAvt(ref this.nameAvt);
			this.nsUri = CompiledAction.PrecalculateAvt(ref this.nsAvt);
			if (this.nameAvt == null && this.nsAvt == null)
			{
				if (this.name != "xmlns")
				{
					this.qname = ElementAction.CreateElementQName(this.name, this.nsUri, compiler.CloneScopeManager());
				}
			}
			else
			{
				this.manager = compiler.CloneScopeManager();
			}
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
			this.empty = this.containedActions == null;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x00129F8C File Offset: 0x0012818C
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.nameAvt = Avt.CompileAvt(compiler, value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Namespace))
			{
				this.nsAvt = Avt.CompileAvt(compiler, value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.UseAttributeSets))
				{
					return false;
				}
				base.AddAction(compiler.CreateUseAttributeSetsAction());
			}
			return true;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x0012A01C File Offset: 0x0012821C
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
				if (this.qname != null)
				{
					frame.CalulatedName = this.qname;
				}
				else
				{
					frame.CalulatedName = ElementAction.CreateElementQName((this.nameAvt == null) ? this.name : this.nameAvt.Evaluate(processor, frame), (this.nsAvt == null) ? this.nsUri : this.nsAvt.Evaluate(processor, frame), this.manager);
				}
				break;
			case 1:
				goto IL_00C2;
			case 2:
				break;
			default:
				return;
			}
			PrefixQName calulatedName = frame.CalulatedName;
			if (!processor.BeginEvent(XPathNodeType.Element, calulatedName.Prefix, calulatedName.Name, calulatedName.Namespace, this.empty))
			{
				frame.State = 2;
				return;
			}
			if (!this.empty)
			{
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
			IL_00C2:
			if (!processor.EndEvent(XPathNodeType.Element))
			{
				frame.State = 1;
				return;
			}
			frame.Finished();
		}

		// Token: 0x040021A7 RID: 8615
		private const int NameDone = 2;

		// Token: 0x040021A8 RID: 8616
		private Avt nameAvt;

		// Token: 0x040021A9 RID: 8617
		private Avt nsAvt;

		// Token: 0x040021AA RID: 8618
		private bool empty;

		// Token: 0x040021AB RID: 8619
		private InputScopeManager manager;

		// Token: 0x040021AC RID: 8620
		private string name;

		// Token: 0x040021AD RID: 8621
		private string nsUri;

		// Token: 0x040021AE RID: 8622
		private PrefixQName qname;
	}
}
