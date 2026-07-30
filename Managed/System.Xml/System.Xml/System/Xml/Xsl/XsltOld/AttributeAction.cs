using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004E8 RID: 1256
	internal class AttributeAction : ContainerAction
	{
		// Token: 0x06003337 RID: 13111 RVA: 0x00125360 File Offset: 0x00123560
		private static PrefixQName CreateAttributeQName(string name, string nsUri, InputScopeManager manager)
		{
			if (name == "xmlns")
			{
				return null;
			}
			if (nsUri == "http://www.w3.org/2000/xmlns/")
			{
				throw XsltException.Create("Elements and attributes cannot belong to the reserved namespace '{0}'.", new string[] { nsUri });
			}
			PrefixQName prefixQName = new PrefixQName();
			prefixQName.SetQName(name);
			prefixQName.Namespace = ((nsUri != null) ? nsUri : manager.ResolveXPathNamespace(prefixQName.Prefix));
			if (prefixQName.Prefix.StartsWith("xml", StringComparison.Ordinal))
			{
				if (prefixQName.Prefix.Length == 3)
				{
					if (!(prefixQName.Namespace == "http://www.w3.org/XML/1998/namespace") || (!(prefixQName.Name == "lang") && !(prefixQName.Name == "space")))
					{
						prefixQName.ClearPrefix();
					}
				}
				else if (prefixQName.Prefix == "xmlns")
				{
					if (prefixQName.Namespace == "http://www.w3.org/2000/xmlns/")
					{
						throw XsltException.Create("Prefix '{0}' is not defined.", new string[] { prefixQName.Prefix });
					}
					prefixQName.ClearPrefix();
				}
			}
			return prefixQName;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x0012546C File Offset: 0x0012366C
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
					this.qname = AttributeAction.CreateAttributeQName(this.name, this.nsUri, compiler.CloneScopeManager());
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
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x00125518 File Offset: 0x00123718
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.nameAvt = Avt.CompileAvt(compiler, value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.Namespace))
				{
					return false;
				}
				this.nsAvt = Avt.CompileAvt(compiler, value);
			}
			return true;
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x00125584 File Offset: 0x00123784
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
					frame.CalulatedName = AttributeAction.CreateAttributeQName((this.nameAvt == null) ? this.name : this.nameAvt.Evaluate(processor, frame), (this.nsAvt == null) ? this.nsUri : this.nsAvt.Evaluate(processor, frame), this.manager);
					if (frame.CalulatedName == null)
					{
						frame.Finished();
						return;
					}
				}
				break;
			case 1:
				if (!processor.EndEvent(XPathNodeType.Attribute))
				{
					frame.State = 1;
					return;
				}
				frame.Finished();
				return;
			case 2:
				break;
			default:
				return;
			}
			PrefixQName calulatedName = frame.CalulatedName;
			if (!processor.BeginEvent(XPathNodeType.Attribute, calulatedName.Prefix, calulatedName.Name, calulatedName.Namespace, false))
			{
				frame.State = 2;
				return;
			}
			processor.PushActionFrame(frame);
			frame.State = 1;
		}

		// Token: 0x0400211B RID: 8475
		private const int NameDone = 2;

		// Token: 0x0400211C RID: 8476
		private Avt nameAvt;

		// Token: 0x0400211D RID: 8477
		private Avt nsAvt;

		// Token: 0x0400211E RID: 8478
		private InputScopeManager manager;

		// Token: 0x0400211F RID: 8479
		private string name;

		// Token: 0x04002120 RID: 8480
		private string nsUri;

		// Token: 0x04002121 RID: 8481
		private PrefixQName qname;
	}
}
