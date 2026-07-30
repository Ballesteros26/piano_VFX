using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x020000BE RID: 190
	internal class TextUtf8RawTextWriter : XmlUtf8RawTextWriter
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x0001B066 File Offset: 0x00019266
		public TextUtf8RawTextWriter(Stream stream, XmlWriterSettings settings)
			: base(stream, settings)
		{
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void StartElementContent()
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001B070 File Offset: 0x00019270
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001B079 File Offset: 0x00019279
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000226C File Offset: 0x0000046C
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001B082 File Offset: 0x00019282
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteComment(string text)
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001B08B File Offset: 0x0001928B
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001B08B File Offset: 0x0001928B
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001B09C File Offset: 0x0001929C
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001B09C File Offset: 0x0001929C
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001B08B File Offset: 0x0001928B
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
