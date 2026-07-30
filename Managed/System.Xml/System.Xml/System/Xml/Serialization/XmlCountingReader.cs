using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200032F RID: 815
	internal class XmlCountingReader : XmlReader, IXmlTextParser, IXmlLineInfo
	{
		// Token: 0x06001ED7 RID: 7895 RVA: 0x000A9323 File Offset: 0x000A7523
		internal XmlCountingReader(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw new ArgumentNullException("xmlReader");
			}
			this.innerReader = xmlReader;
			this.advanceCount = 0;
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x000A9347 File Offset: 0x000A7547
		internal int AdvanceCount
		{
			get
			{
				return this.advanceCount;
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000A934F File Offset: 0x000A754F
		private void IncrementCount()
		{
			if (this.advanceCount == 2147483647)
			{
				this.advanceCount = 0;
				return;
			}
			this.advanceCount++;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x000A9374 File Offset: 0x000A7574
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.innerReader.Settings;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x000A9381 File Offset: 0x000A7581
		public override XmlNodeType NodeType
		{
			get
			{
				return this.innerReader.NodeType;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x000A938E File Offset: 0x000A758E
		public override string Name
		{
			get
			{
				return this.innerReader.Name;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x000A939B File Offset: 0x000A759B
		public override string LocalName
		{
			get
			{
				return this.innerReader.LocalName;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x000A93A8 File Offset: 0x000A75A8
		public override string NamespaceURI
		{
			get
			{
				return this.innerReader.NamespaceURI;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x000A93B5 File Offset: 0x000A75B5
		public override string Prefix
		{
			get
			{
				return this.innerReader.Prefix;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x000A93C2 File Offset: 0x000A75C2
		public override bool HasValue
		{
			get
			{
				return this.innerReader.HasValue;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000A93CF File Offset: 0x000A75CF
		public override string Value
		{
			get
			{
				return this.innerReader.Value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x000A93DC File Offset: 0x000A75DC
		public override int Depth
		{
			get
			{
				return this.innerReader.Depth;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x000A93E9 File Offset: 0x000A75E9
		public override string BaseURI
		{
			get
			{
				return this.innerReader.BaseURI;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x000A93F6 File Offset: 0x000A75F6
		public override bool IsEmptyElement
		{
			get
			{
				return this.innerReader.IsEmptyElement;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x000A9403 File Offset: 0x000A7603
		public override bool IsDefault
		{
			get
			{
				return this.innerReader.IsDefault;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x000A9410 File Offset: 0x000A7610
		public override char QuoteChar
		{
			get
			{
				return this.innerReader.QuoteChar;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000A941D File Offset: 0x000A761D
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.innerReader.XmlSpace;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x000A942A File Offset: 0x000A762A
		public override string XmlLang
		{
			get
			{
				return this.innerReader.XmlLang;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000A9437 File Offset: 0x000A7637
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.innerReader.SchemaInfo;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x000A9444 File Offset: 0x000A7644
		public override Type ValueType
		{
			get
			{
				return this.innerReader.ValueType;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x000A9451 File Offset: 0x000A7651
		public override int AttributeCount
		{
			get
			{
				return this.innerReader.AttributeCount;
			}
		}

		// Token: 0x1700064E RID: 1614
		public override string this[int i]
		{
			get
			{
				return this.innerReader[i];
			}
		}

		// Token: 0x1700064F RID: 1615
		public override string this[string name]
		{
			get
			{
				return this.innerReader[name];
			}
		}

		// Token: 0x17000650 RID: 1616
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.innerReader[name, namespaceURI];
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x000A9489 File Offset: 0x000A7689
		public override bool EOF
		{
			get
			{
				return this.innerReader.EOF;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x000A9496 File Offset: 0x000A7696
		public override ReadState ReadState
		{
			get
			{
				return this.innerReader.ReadState;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x000A94A3 File Offset: 0x000A76A3
		public override XmlNameTable NameTable
		{
			get
			{
				return this.innerReader.NameTable;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x000A94B0 File Offset: 0x000A76B0
		public override bool CanResolveEntity
		{
			get
			{
				return this.innerReader.CanResolveEntity;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x000A94BD File Offset: 0x000A76BD
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.innerReader.CanReadBinaryContent;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x000A94CA File Offset: 0x000A76CA
		public override bool CanReadValueChunk
		{
			get
			{
				return this.innerReader.CanReadValueChunk;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x000A94D7 File Offset: 0x000A76D7
		public override bool HasAttributes
		{
			get
			{
				return this.innerReader.HasAttributes;
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x000A94E4 File Offset: 0x000A76E4
		public override void Close()
		{
			this.innerReader.Close();
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000A94F1 File Offset: 0x000A76F1
		public override string GetAttribute(string name)
		{
			return this.innerReader.GetAttribute(name);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x000A94FF File Offset: 0x000A76FF
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.innerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000A950E File Offset: 0x000A770E
		public override string GetAttribute(int i)
		{
			return this.innerReader.GetAttribute(i);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000A951C File Offset: 0x000A771C
		public override bool MoveToAttribute(string name)
		{
			return this.innerReader.MoveToAttribute(name);
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000A952A File Offset: 0x000A772A
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.innerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000A9539 File Offset: 0x000A7739
		public override void MoveToAttribute(int i)
		{
			this.innerReader.MoveToAttribute(i);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000A9547 File Offset: 0x000A7747
		public override bool MoveToFirstAttribute()
		{
			return this.innerReader.MoveToFirstAttribute();
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000A9554 File Offset: 0x000A7754
		public override bool MoveToNextAttribute()
		{
			return this.innerReader.MoveToNextAttribute();
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000A9561 File Offset: 0x000A7761
		public override bool MoveToElement()
		{
			return this.innerReader.MoveToElement();
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000A956E File Offset: 0x000A776E
		public override string LookupNamespace(string prefix)
		{
			return this.innerReader.LookupNamespace(prefix);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000A957C File Offset: 0x000A777C
		public override bool ReadAttributeValue()
		{
			return this.innerReader.ReadAttributeValue();
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000A9589 File Offset: 0x000A7789
		public override void ResolveEntity()
		{
			this.innerReader.ResolveEntity();
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000A9596 File Offset: 0x000A7796
		public override bool IsStartElement()
		{
			return this.innerReader.IsStartElement();
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000A95A3 File Offset: 0x000A77A3
		public override bool IsStartElement(string name)
		{
			return this.innerReader.IsStartElement(name);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000A95B1 File Offset: 0x000A77B1
		public override bool IsStartElement(string localname, string ns)
		{
			return this.innerReader.IsStartElement(localname, ns);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000A95C0 File Offset: 0x000A77C0
		public override XmlReader ReadSubtree()
		{
			return this.innerReader.ReadSubtree();
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000A95CD File Offset: 0x000A77CD
		public override XmlNodeType MoveToContent()
		{
			return this.innerReader.MoveToContent();
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000A95DA File Offset: 0x000A77DA
		public override bool Read()
		{
			this.IncrementCount();
			return this.innerReader.Read();
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000A95ED File Offset: 0x000A77ED
		public override void Skip()
		{
			this.IncrementCount();
			this.innerReader.Skip();
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000A9600 File Offset: 0x000A7800
		public override string ReadInnerXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadInnerXml();
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000A9621 File Offset: 0x000A7821
		public override string ReadOuterXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadOuterXml();
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000A9642 File Offset: 0x000A7842
		public override object ReadContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsObject();
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000A9655 File Offset: 0x000A7855
		public override bool ReadContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBoolean();
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000A9668 File Offset: 0x000A7868
		public override DateTime ReadContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDateTime();
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000A967B File Offset: 0x000A787B
		public override double ReadContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDouble();
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000A968E File Offset: 0x000A788E
		public override int ReadContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsInt();
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000A96A1 File Offset: 0x000A78A1
		public override long ReadContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsLong();
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000A96B4 File Offset: 0x000A78B4
		public override string ReadContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsString();
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000A96C7 File Offset: 0x000A78C7
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000A96DC File Offset: 0x000A78DC
		public override object ReadElementContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject();
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000A96EF File Offset: 0x000A78EF
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000A9704 File Offset: 0x000A7904
		public override bool ReadElementContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000A9717 File Offset: 0x000A7917
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000A972C File Offset: 0x000A792C
		public override DateTime ReadElementContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime();
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000A973F File Offset: 0x000A793F
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x000A9754 File Offset: 0x000A7954
		public override double ReadElementContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble();
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000A9767 File Offset: 0x000A7967
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000A977C File Offset: 0x000A797C
		public override int ReadElementContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt();
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000A978F File Offset: 0x000A798F
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000A97A4 File Offset: 0x000A79A4
		public override long ReadElementContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong();
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000A97B7 File Offset: 0x000A79B7
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000A97CC File Offset: 0x000A79CC
		public override string ReadElementContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString();
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000A97DF File Offset: 0x000A79DF
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000A97F4 File Offset: 0x000A79F4
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000A9809 File Offset: 0x000A7A09
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000A9821 File Offset: 0x000A7A21
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x000A9837 File Offset: 0x000A7A37
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000A984D File Offset: 0x000A7A4D
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x000A9863 File Offset: 0x000A7A63
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000A9879 File Offset: 0x000A7A79
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000A988F File Offset: 0x000A7A8F
		public override string ReadString()
		{
			this.IncrementCount();
			return this.innerReader.ReadString();
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000A98A2 File Offset: 0x000A7AA2
		public override void ReadStartElement()
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement();
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000A98B5 File Offset: 0x000A7AB5
		public override void ReadStartElement(string name)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(name);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x000A98C9 File Offset: 0x000A7AC9
		public override void ReadStartElement(string localname, string ns)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(localname, ns);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000A98DE File Offset: 0x000A7ADE
		public override string ReadElementString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString();
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000A98F1 File Offset: 0x000A7AF1
		public override string ReadElementString(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(name);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000A9905 File Offset: 0x000A7B05
		public override string ReadElementString(string localname, string ns)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(localname, ns);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000A991A File Offset: 0x000A7B1A
		public override void ReadEndElement()
		{
			this.IncrementCount();
			this.innerReader.ReadEndElement();
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000A992D File Offset: 0x000A7B2D
		public override bool ReadToFollowing(string name)
		{
			this.IncrementCount();
			return this.ReadToFollowing(name);
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000A993C File Offset: 0x000A7B3C
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000A9951 File Offset: 0x000A7B51
		public override bool ReadToDescendant(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(name);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000A9965 File Offset: 0x000A7B65
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000A997A File Offset: 0x000A7B7A
		public override bool ReadToNextSibling(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(name);
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x000A998E File Offset: 0x000A7B8E
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000A99A4 File Offset: 0x000A7BA4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					IDisposable disposable = this.innerReader;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x000A99E0 File Offset: 0x000A7BE0
		// (set) Token: 0x06001F39 RID: 7993 RVA: 0x000A9A1C File Offset: 0x000A7C1C
		bool IXmlTextParser.Normalized
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					return xmlTextParser != null && xmlTextParser.Normalized;
				}
				return xmlTextReader.Normalization;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.Normalized = value;
						return;
					}
				}
				else
				{
					xmlTextReader.Normalization = value;
				}
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x000A9A58 File Offset: 0x000A7C58
		// (set) Token: 0x06001F3B RID: 7995 RVA: 0x000A9A94 File Offset: 0x000A7C94
		WhitespaceHandling IXmlTextParser.WhitespaceHandling
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader != null)
				{
					return xmlTextReader.WhitespaceHandling;
				}
				IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
				if (xmlTextParser != null)
				{
					return xmlTextParser.WhitespaceHandling;
				}
				return WhitespaceHandling.None;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.WhitespaceHandling = value;
						return;
					}
				}
				else
				{
					xmlTextReader.WhitespaceHandling = value;
				}
			}
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000A9AD0 File Offset: 0x000A7CD0
		bool IXmlLineInfo.HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x000A9AF4 File Offset: 0x000A7CF4
		int IXmlLineInfo.LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x000A9B18 File Offset: 0x000A7D18
		int IXmlLineInfo.LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x04001731 RID: 5937
		private XmlReader innerReader;

		// Token: 0x04001732 RID: 5938
		private int advanceCount;
	}
}
