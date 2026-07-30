using System;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x02000232 RID: 562
	[DebuggerDisplay("{ToString()}")]
	internal struct DebuggerDisplayXmlNodeProxy
	{
		// Token: 0x0600159A RID: 5530 RVA: 0x00079887 File Offset: 0x00077A87
		public DebuggerDisplayXmlNodeProxy(XmlNode node)
		{
			this.node = node;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00079890 File Offset: 0x00077A90
		public override string ToString()
		{
			XmlNodeType nodeType = this.node.NodeType;
			string text = nodeType.ToString();
			switch (nodeType)
			{
			case XmlNodeType.Element:
			case XmlNodeType.EntityReference:
				text = text + ", Name=\"" + this.node.Name + "\"";
				break;
			case XmlNodeType.Attribute:
			case XmlNodeType.ProcessingInstruction:
				text = string.Concat(new string[]
				{
					text,
					", Name=\"",
					this.node.Name,
					"\", Value=\"",
					XmlConvert.EscapeValueForDebuggerDisplay(this.node.Value),
					"\""
				});
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.XmlDeclaration:
				text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.node.Value) + "\"";
				break;
			case XmlNodeType.DocumentType:
			{
				XmlDocumentType xmlDocumentType = (XmlDocumentType)this.node;
				text = string.Concat(new string[]
				{
					text,
					", Name=\"",
					xmlDocumentType.Name,
					"\", SYSTEM=\"",
					xmlDocumentType.SystemId,
					"\", PUBLIC=\"",
					xmlDocumentType.PublicId,
					"\", Value=\"",
					XmlConvert.EscapeValueForDebuggerDisplay(xmlDocumentType.InternalSubset),
					"\""
				});
				break;
			}
			}
			return text;
		}

		// Token: 0x04000DFD RID: 3581
		private XmlNode node;
	}
}
