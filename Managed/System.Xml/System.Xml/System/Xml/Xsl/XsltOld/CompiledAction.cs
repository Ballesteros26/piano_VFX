using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004F1 RID: 1265
	internal abstract class CompiledAction : Action
	{
		// Token: 0x0600336D RID: 13165
		internal abstract void Compile(Compiler compiler);

		// Token: 0x0600336E RID: 13166 RVA: 0x0000226C File Offset: 0x0000046C
		internal virtual bool CompileAttribute(Compiler compiler)
		{
			return false;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x0012609C File Offset: 0x0012429C
		public void CompileAttributes(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			string localName = input.LocalName;
			if (input.MoveToFirstAttribute())
			{
				do
				{
					if (input.NamespaceURI.Length == 0)
					{
						try
						{
							if (!this.CompileAttribute(compiler))
							{
								throw XsltException.Create("'{0}' is an invalid attribute for the '{1}' element.", new string[] { input.LocalName, localName });
							}
						}
						catch
						{
							if (!compiler.ForwardCompatibility)
							{
								throw;
							}
						}
					}
				}
				while (input.MoveToNextAttribute());
				input.ToParent();
			}
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x00126124 File Offset: 0x00124324
		internal static string PrecalculateAvt(ref Avt avt)
		{
			string text = null;
			if (avt != null && avt.IsConstant)
			{
				text = avt.Evaluate(null, null);
				avt = null;
			}
			return text;
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x00126150 File Offset: 0x00124350
		public void CheckEmpty(Compiler compiler)
		{
			string name = compiler.Input.Name;
			if (compiler.Recurse())
			{
				for (;;)
				{
					XPathNodeType nodeType = compiler.Input.NodeType;
					if (nodeType != XPathNodeType.Whitespace && nodeType != XPathNodeType.Comment && nodeType != XPathNodeType.ProcessingInstruction)
					{
						break;
					}
					if (!compiler.Advance())
					{
						goto Block_4;
					}
				}
				throw XsltException.Create("The contents of '{0}' must be empty.", new string[] { name });
				Block_4:
				compiler.ToParent();
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x001261AD File Offset: 0x001243AD
		public void CheckRequiredAttribute(Compiler compiler, object attrValue, string attrName)
		{
			this.CheckRequiredAttribute(compiler, attrValue != null, attrName);
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x001261BB File Offset: 0x001243BB
		public void CheckRequiredAttribute(Compiler compiler, bool attr, string attrName)
		{
			if (!attr)
			{
				throw XsltException.Create("Missing mandatory attribute '{0}'.", new string[] { attrName });
			}
		}
	}
}
