using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000528 RID: 1320
	internal class NavigatorOutput : RecordOutput
	{
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x0012B50C File Offset: 0x0012970C
		internal XPathNavigator Navigator
		{
			get
			{
				return ((IXPathNavigable)this.doc).CreateNavigator();
			}
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x0012B519 File Offset: 0x00129719
		internal NavigatorOutput(string baseUri)
		{
			this.doc = new XPathDocument();
			this.wr = this.doc.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames, baseUri);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x0012B540 File Offset: 0x00129740
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			this.documentIndex++;
			switch (mainNode.NodeType)
			{
			case XmlNodeType.Element:
			{
				this.wr.WriteStartElement(mainNode.Prefix, mainNode.LocalName, mainNode.NamespaceURI);
				for (int i = 0; i < record.AttributeCount; i++)
				{
					this.documentIndex++;
					BuilderInfo builderInfo = (BuilderInfo)record.AttributeList[i];
					if (builderInfo.NamespaceURI == "http://www.w3.org/2000/xmlns/")
					{
						if (builderInfo.Prefix.Length == 0)
						{
							this.wr.WriteNamespaceDeclaration(string.Empty, builderInfo.Value);
						}
						else
						{
							this.wr.WriteNamespaceDeclaration(builderInfo.LocalName, builderInfo.Value);
						}
					}
					else
					{
						this.wr.WriteAttributeString(builderInfo.Prefix, builderInfo.LocalName, builderInfo.NamespaceURI, builderInfo.Value);
					}
				}
				this.wr.StartElementContent();
				if (mainNode.IsEmptyTag)
				{
					this.wr.WriteEndElement(mainNode.Prefix, mainNode.LocalName, mainNode.NamespaceURI);
				}
				break;
			}
			case XmlNodeType.Text:
				this.wr.WriteString(mainNode.Value);
				break;
			case XmlNodeType.ProcessingInstruction:
				this.wr.WriteProcessingInstruction(mainNode.LocalName, mainNode.Value);
				break;
			case XmlNodeType.Comment:
				this.wr.WriteComment(mainNode.Value);
				break;
			case XmlNodeType.SignificantWhitespace:
				this.wr.WriteString(mainNode.Value);
				break;
			case XmlNodeType.EndElement:
				this.wr.WriteEndElement(mainNode.Prefix, mainNode.LocalName, mainNode.NamespaceURI);
				break;
			}
			record.Reset();
			return Processor.OutputResult.Continue;
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x0012B724 File Offset: 0x00129924
		public void TheEnd()
		{
			this.wr.Close();
		}

		// Token: 0x040021DE RID: 8670
		private XPathDocument doc;

		// Token: 0x040021DF RID: 8671
		private int documentIndex;

		// Token: 0x040021E0 RID: 8672
		private XmlRawWriter wr;
	}
}
