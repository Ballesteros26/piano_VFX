using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004E9 RID: 1257
	internal class AttributeSetAction : ContainerAction
	{
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x00125674 File Offset: 0x00123874
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x0012567C File Offset: 0x0012387C
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.name, "name");
			this.CompileContent(compiler);
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x001256A0 File Offset: 0x001238A0
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.name = compiler.CreateXPathQName(value);
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

		// Token: 0x0600333F RID: 13119 RVA: 0x0012570C File Offset: 0x0012390C
		private void CompileContent(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			if (compiler.Recurse())
			{
				for (;;)
				{
					XPathNodeType nodeType = input.NodeType;
					if (nodeType != XPathNodeType.Element)
					{
						if (nodeType - XPathNodeType.SignificantWhitespace > 3)
						{
							break;
						}
					}
					else
					{
						compiler.PushNamespaceScope();
						string namespaceURI = input.NamespaceURI;
						string localName = input.LocalName;
						if (!Ref.Equal(namespaceURI, input.Atoms.UriXsl) || !Ref.Equal(localName, input.Atoms.Attribute))
						{
							goto IL_006B;
						}
						base.AddAction(compiler.CreateAttributeAction());
						compiler.PopScope();
					}
					if (!compiler.Advance())
					{
						goto Block_5;
					}
				}
				throw XsltException.Create("The contents of '{0}' are invalid.", new string[] { "attribute-set" });
				IL_006B:
				throw compiler.UnexpectedKeyword();
				Block_5:
				compiler.ToParent();
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x001257C0 File Offset: 0x001239C0
		internal void Merge(AttributeSetAction attributeAction)
		{
			int num = 0;
			Action action;
			while ((action = attributeAction.GetAction(num)) != null)
			{
				base.AddAction(action);
				num++;
			}
		}

		// Token: 0x04002122 RID: 8482
		internal XmlQualifiedName name;
	}
}
