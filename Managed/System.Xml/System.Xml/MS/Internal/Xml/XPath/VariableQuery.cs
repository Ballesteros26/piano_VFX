using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000048 RID: 72
	internal sealed class VariableQuery : ExtensionQuery
	{
		// Token: 0x060001FE RID: 510 RVA: 0x000079D3 File Offset: 0x00005BD3
		public VariableQuery(string name, string prefix)
			: base(prefix, name)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000079DD File Offset: 0x00005BDD
		private VariableQuery(VariableQuery other)
			: base(other)
		{
			this.variable = other.variable;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000079F4 File Offset: 0x00005BF4
		public override void SetXsltContext(XsltContext context)
		{
			if (context == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			if (this.xsltContext != context)
			{
				this.xsltContext = context;
				this.variable = this.xsltContext.ResolveVariable(this.prefix, this.name);
				if (this.variable == null)
				{
					throw XPathException.Create("The variable '{0}' is undefined.", base.QName);
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007A55 File Offset: 0x00005C55
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			if (this.xsltContext == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			return base.ProcessResult(this.variable.Evaluate(this.xsltContext));
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00007A84 File Offset: 0x00005C84
		public override XPathResultType StaticType
		{
			get
			{
				if (this.variable != null)
				{
					return base.GetXPathType(this.Evaluate(null));
				}
				XPathResultType xpathResultType = ((this.variable != null) ? this.variable.VariableType : XPathResultType.Any);
				if (xpathResultType == XPathResultType.Error)
				{
					xpathResultType = XPathResultType.Any;
				}
				return xpathResultType;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007AC5 File Offset: 0x00005CC5
		public override XPathNodeIterator Clone()
		{
			return new VariableQuery(this);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", (this.prefix.Length != 0) ? (this.prefix + ":" + this.name) : this.name);
			w.WriteEndElement();
		}

		// Token: 0x0400010C RID: 268
		private IXsltContextVariable variable;
	}
}
