using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x020000BD RID: 189
	internal class TextEncodedRawTextWriter : XmlEncodedRawTextWriter
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0001B013 File Offset: 0x00019213
		public TextEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings)
			: base(writer, settings)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001B01D File Offset: 0x0001921D
		public TextEncodedRawTextWriter(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void StartElementContent()
		{
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001B027 File Offset: 0x00019227
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001B030 File Offset: 0x00019230
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000226C File Offset: 0x0000046C
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001B039 File Offset: 0x00019239
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteComment(string text)
		{
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001B042 File Offset: 0x00019242
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001B042 File Offset: 0x00019242
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001B053 File Offset: 0x00019253
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001B053 File Offset: 0x00019253
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001B042 File Offset: 0x00019242
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
