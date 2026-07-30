using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000554 RID: 1364
	internal class WriterOutput : RecordOutput
	{
		// Token: 0x060036DE RID: 14046 RVA: 0x001329C5 File Offset: 0x00130BC5
		internal WriterOutput(Processor processor, XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.processor = processor;
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x001329EC File Offset: 0x00130BEC
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			switch (mainNode.NodeType)
			{
			case XmlNodeType.Element:
				this.writer.WriteStartElement(mainNode.Prefix, mainNode.LocalName, mainNode.NamespaceURI);
				this.WriteAttributes(record.AttributeList, record.AttributeCount);
				if (mainNode.IsEmptyTag)
				{
					this.writer.WriteEndElement();
				}
				break;
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.writer.WriteString(mainNode.Value);
				break;
			case XmlNodeType.CDATA:
				this.writer.WriteCData(mainNode.Value);
				break;
			case XmlNodeType.EntityReference:
				this.writer.WriteEntityRef(mainNode.LocalName);
				break;
			case XmlNodeType.ProcessingInstruction:
				this.writer.WriteProcessingInstruction(mainNode.LocalName, mainNode.Value);
				break;
			case XmlNodeType.Comment:
				this.writer.WriteComment(mainNode.Value);
				break;
			case XmlNodeType.DocumentType:
				this.writer.WriteRaw(mainNode.Value);
				break;
			case XmlNodeType.EndElement:
				this.writer.WriteFullEndElement();
				break;
			}
			record.Reset();
			return Processor.OutputResult.Continue;
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x00132B2A File Offset: 0x00130D2A
		public void TheEnd()
		{
			this.writer.Flush();
			this.writer = null;
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x00132B40 File Offset: 0x00130D40
		private void WriteAttributes(ArrayList list, int count)
		{
			for (int i = 0; i < count; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)list[i];
				this.writer.WriteAttributeString(builderInfo.Prefix, builderInfo.LocalName, builderInfo.NamespaceURI, builderInfo.Value);
			}
		}

		// Token: 0x0400232C RID: 9004
		private XmlWriter writer;

		// Token: 0x0400232D RID: 9005
		private Processor processor;
	}
}
