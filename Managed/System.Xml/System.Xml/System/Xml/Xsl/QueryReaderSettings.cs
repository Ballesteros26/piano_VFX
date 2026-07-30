using System;
using System.IO;

namespace System.Xml.Xsl
{
	// Token: 0x020004BF RID: 1215
	internal class QueryReaderSettings
	{
		// Token: 0x0600314B RID: 12619 RVA: 0x0011CA00 File Offset: 0x0011AC00
		public QueryReaderSettings(XmlNameTable xmlNameTable)
		{
			this.xmlReaderSettings = new XmlReaderSettings();
			this.xmlReaderSettings.NameTable = xmlNameTable;
			this.xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			this.xmlReaderSettings.XmlResolver = null;
			this.xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			this.xmlReaderSettings.CloseInput = true;
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x0011CA5C File Offset: 0x0011AC5C
		public QueryReaderSettings(XmlReader reader)
		{
			XmlValidatingReader xmlValidatingReader = reader as XmlValidatingReader;
			if (xmlValidatingReader != null)
			{
				this.validatingReader = true;
				reader = xmlValidatingReader.Impl.Reader;
			}
			this.xmlReaderSettings = reader.Settings;
			if (this.xmlReaderSettings != null)
			{
				this.xmlReaderSettings = this.xmlReaderSettings.Clone();
				this.xmlReaderSettings.NameTable = reader.NameTable;
				this.xmlReaderSettings.CloseInput = true;
				this.xmlReaderSettings.LineNumberOffset = 0;
				this.xmlReaderSettings.LinePositionOffset = 0;
				XmlTextReaderImpl xmlTextReaderImpl = reader as XmlTextReaderImpl;
				if (xmlTextReaderImpl != null)
				{
					this.xmlReaderSettings.XmlResolver = xmlTextReaderImpl.GetResolver();
					return;
				}
			}
			else
			{
				this.xmlNameTable = reader.NameTable;
				XmlTextReader xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					XmlTextReaderImpl impl = xmlTextReader.Impl;
					this.entityHandling = impl.EntityHandling;
					this.namespaces = impl.Namespaces;
					this.normalization = impl.Normalization;
					this.prohibitDtd = impl.DtdProcessing == DtdProcessing.Prohibit;
					this.whitespaceHandling = impl.WhitespaceHandling;
					this.xmlResolver = impl.GetResolver();
					return;
				}
				this.entityHandling = EntityHandling.ExpandEntities;
				this.namespaces = true;
				this.normalization = true;
				this.prohibitDtd = true;
				this.whitespaceHandling = WhitespaceHandling.All;
				this.xmlResolver = null;
			}
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x0011CB9C File Offset: 0x0011AD9C
		public XmlReader CreateReader(Stream stream, string baseUri)
		{
			XmlReader xmlReader;
			if (this.xmlReaderSettings != null)
			{
				xmlReader = XmlReader.Create(stream, this.xmlReaderSettings, baseUri);
			}
			else
			{
				xmlReader = new XmlTextReaderImpl(baseUri, stream, this.xmlNameTable)
				{
					EntityHandling = this.entityHandling,
					Namespaces = this.namespaces,
					Normalization = this.normalization,
					DtdProcessing = (this.prohibitDtd ? DtdProcessing.Prohibit : DtdProcessing.Parse),
					WhitespaceHandling = this.whitespaceHandling,
					XmlResolver = this.xmlResolver
				};
			}
			if (this.validatingReader)
			{
				xmlReader = new XmlValidatingReader(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x0011CC2D File Offset: 0x0011AE2D
		public XmlNameTable NameTable
		{
			get
			{
				if (this.xmlReaderSettings == null)
				{
					return this.xmlNameTable;
				}
				return this.xmlReaderSettings.NameTable;
			}
		}

		// Token: 0x04002034 RID: 8244
		private bool validatingReader;

		// Token: 0x04002035 RID: 8245
		private XmlReaderSettings xmlReaderSettings;

		// Token: 0x04002036 RID: 8246
		private XmlNameTable xmlNameTable;

		// Token: 0x04002037 RID: 8247
		private EntityHandling entityHandling;

		// Token: 0x04002038 RID: 8248
		private bool namespaces;

		// Token: 0x04002039 RID: 8249
		private bool normalization;

		// Token: 0x0400203A RID: 8250
		private bool prohibitDtd;

		// Token: 0x0400203B RID: 8251
		private WhitespaceHandling whitespaceHandling;

		// Token: 0x0400203C RID: 8252
		private XmlResolver xmlResolver;
	}
}
