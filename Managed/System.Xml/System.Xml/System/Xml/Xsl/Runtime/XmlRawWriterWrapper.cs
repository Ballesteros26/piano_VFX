using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000616 RID: 1558
	internal sealed class XmlRawWriterWrapper : XmlRawWriter
	{
		// Token: 0x06003D25 RID: 15653 RVA: 0x0015319B File Offset: 0x0015139B
		public XmlRawWriterWrapper(XmlWriter writer)
		{
			this.wrapped = writer;
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x001531AA File Offset: 0x001513AA
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.wrapped.Settings;
			}
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x001531B7 File Offset: 0x001513B7
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.wrapped.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x001531C9 File Offset: 0x001513C9
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x001531D9 File Offset: 0x001513D9
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x001531E9 File Offset: 0x001513E9
		public override void WriteEndAttribute()
		{
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x001531F6 File Offset: 0x001513F6
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x00153204 File Offset: 0x00151404
		public override void WriteComment(string text)
		{
			this.wrapped.WriteComment(text);
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x00153212 File Offset: 0x00151412
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x00153221 File Offset: 0x00151421
		public override void WriteWhitespace(string ws)
		{
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x0015322F File Offset: 0x0015142F
		public override void WriteString(string text)
		{
			this.wrapped.WriteString(text);
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x0015323D File Offset: 0x0015143D
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x0015324D File Offset: 0x0015144D
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x0015325D File Offset: 0x0015145D
		public override void WriteRaw(string data)
		{
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x0015326B File Offset: 0x0015146B
		public override void WriteEntityRef(string name)
		{
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x00153279 File Offset: 0x00151479
		public override void WriteCharEntity(char ch)
		{
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x00153287 File Offset: 0x00151487
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x00153296 File Offset: 0x00151496
		public override void Close()
		{
			this.wrapped.Close();
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x001532A3 File Offset: 0x001514A3
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x001532B0 File Offset: 0x001514B0
		public override void WriteValue(object value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x001532BE File Offset: 0x001514BE
		public override void WriteValue(string value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x001532CC File Offset: 0x001514CC
		public override void WriteValue(bool value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x001532DA File Offset: 0x001514DA
		public override void WriteValue(DateTime value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x001532E8 File Offset: 0x001514E8
		public override void WriteValue(float value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x001532F6 File Offset: 0x001514F6
		public override void WriteValue(decimal value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x00153304 File Offset: 0x00151504
		public override void WriteValue(double value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x00153312 File Offset: 0x00151512
		public override void WriteValue(int value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x00153320 File Offset: 0x00151520
		public override void WriteValue(long value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x00153330 File Offset: 0x00151530
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((IDisposable)this.wrapped).Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void StartElementContent()
		{
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x00153368 File Offset: 0x00151568
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteEndElement();
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x00153375 File Offset: 0x00151575
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteFullEndElement();
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x00153382 File Offset: 0x00151582
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			if (prefix.Length == 0)
			{
				this.wrapped.WriteAttributeString(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/", ns);
				return;
			}
			this.wrapped.WriteAttributeString("xmlns", prefix, "http://www.w3.org/2000/xmlns/", ns);
		}

		// Token: 0x040027C5 RID: 10181
		private XmlWriter wrapped;
	}
}
