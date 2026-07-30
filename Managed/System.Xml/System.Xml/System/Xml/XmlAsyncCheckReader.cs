using System;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000C2 RID: 194
	internal class XmlAsyncCheckReader : XmlReader
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001B2DB File Offset: 0x000194DB
		internal XmlReader CoreReader
		{
			get
			{
				return this.coreReader;
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001B2E4 File Offset: 0x000194E4
		public static XmlAsyncCheckReader CreateAsyncCheckWrapper(XmlReader reader)
		{
			if (reader is IXmlLineInfo)
			{
				if (!(reader is IXmlNamespaceResolver))
				{
					return new XmlAsyncCheckReaderWithLineInfo(reader);
				}
				if (reader is IXmlSchemaInfo)
				{
					return new XmlAsyncCheckReaderWithLineInfoNSSchema(reader);
				}
				return new XmlAsyncCheckReaderWithLineInfoNS(reader);
			}
			else
			{
				if (reader is IXmlNamespaceResolver)
				{
					return new XmlAsyncCheckReaderWithNS(reader);
				}
				return new XmlAsyncCheckReader(reader);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001B333 File Offset: 0x00019533
		public XmlAsyncCheckReader(XmlReader reader)
		{
			this.coreReader = reader;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001B34D File Offset: 0x0001954D
		private void CheckAsync()
		{
			if (!this.lastTask.IsCompleted)
			{
				throw new InvalidOperationException(Res.GetString("An asynchronous operation is already in progress."));
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001B36C File Offset: 0x0001956C
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.coreReader.Settings;
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				else
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.Async = true;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001B3A6 File Offset: 0x000195A6
		public override XmlNodeType NodeType
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001B3B9 File Offset: 0x000195B9
		public override string Name
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Name;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001B3CC File Offset: 0x000195CC
		public override string LocalName
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0001B3DF File Offset: 0x000195DF
		public override string NamespaceURI
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001B3F2 File Offset: 0x000195F2
		public override string Prefix
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001B405 File Offset: 0x00019605
		public override bool HasValue
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.HasValue;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001B418 File Offset: 0x00019618
		public override string Value
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001B42B File Offset: 0x0001962B
		public override int Depth
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Depth;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001B43E File Offset: 0x0001963E
		public override string BaseURI
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001B451 File Offset: 0x00019651
		public override bool IsEmptyElement
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001B464 File Offset: 0x00019664
		public override bool IsDefault
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.IsDefault;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001B477 File Offset: 0x00019677
		public override char QuoteChar
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001B48A File Offset: 0x0001968A
		public override XmlSpace XmlSpace
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001B49D File Offset: 0x0001969D
		public override string XmlLang
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001B4B0 File Offset: 0x000196B0
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.SchemaInfo;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001B4C3 File Offset: 0x000196C3
		public override Type ValueType
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.ValueType;
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001B4D6 File Offset: 0x000196D6
		public override object ReadContentAsObject()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsObject();
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001B4E9 File Offset: 0x000196E9
		public override bool ReadContentAsBoolean()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBoolean();
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001B4FC File Offset: 0x000196FC
		public override DateTime ReadContentAsDateTime()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDateTime();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001B50F File Offset: 0x0001970F
		public override double ReadContentAsDouble()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDouble();
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001B522 File Offset: 0x00019722
		public override float ReadContentAsFloat()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsFloat();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001B535 File Offset: 0x00019735
		public override decimal ReadContentAsDecimal()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDecimal();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001B548 File Offset: 0x00019748
		public override int ReadContentAsInt()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsInt();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001B55B File Offset: 0x0001975B
		public override long ReadContentAsLong()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsLong();
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001B56E File Offset: 0x0001976E
		public override string ReadContentAsString()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsString();
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001B581 File Offset: 0x00019781
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001B596 File Offset: 0x00019796
		public override object ReadElementContentAsObject()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsObject();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001B5A9 File Offset: 0x000197A9
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001B5BE File Offset: 0x000197BE
		public override bool ReadElementContentAsBoolean()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001B5D1 File Offset: 0x000197D1
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001B5E6 File Offset: 0x000197E6
		public override DateTime ReadElementContentAsDateTime()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDateTime();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001B5F9 File Offset: 0x000197F9
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001B60E File Offset: 0x0001980E
		public override DateTimeOffset ReadContentAsDateTimeOffset()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDateTimeOffset();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001B621 File Offset: 0x00019821
		public override double ReadElementContentAsDouble()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDouble();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001B634 File Offset: 0x00019834
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001B649 File Offset: 0x00019849
		public override float ReadElementContentAsFloat()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsFloat();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001B65C File Offset: 0x0001985C
		public override float ReadElementContentAsFloat(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsFloat(localName, namespaceURI);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001B671 File Offset: 0x00019871
		public override decimal ReadElementContentAsDecimal()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDecimal();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001B684 File Offset: 0x00019884
		public override decimal ReadElementContentAsDecimal(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDecimal(localName, namespaceURI);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001B699 File Offset: 0x00019899
		public override int ReadElementContentAsInt()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsInt();
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001B6AC File Offset: 0x000198AC
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001B6C1 File Offset: 0x000198C1
		public override long ReadElementContentAsLong()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsLong();
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001B6D4 File Offset: 0x000198D4
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001B6E9 File Offset: 0x000198E9
		public override string ReadElementContentAsString()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsString();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001B6FC File Offset: 0x000198FC
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001B711 File Offset: 0x00019911
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001B726 File Offset: 0x00019926
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001B73E File Offset: 0x0001993E
		public override int AttributeCount
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.AttributeCount;
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001B751 File Offset: 0x00019951
		public override string GetAttribute(string name)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(name);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001B765 File Offset: 0x00019965
		public override string GetAttribute(string name, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001B77A File Offset: 0x0001997A
		public override string GetAttribute(int i)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(i);
		}

		// Token: 0x17000151 RID: 337
		public override string this[int i]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[i];
			}
		}

		// Token: 0x17000152 RID: 338
		public override string this[string name]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[name];
			}
		}

		// Token: 0x17000153 RID: 339
		public override string this[string name, string namespaceURI]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[name, namespaceURI];
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001B7CB File Offset: 0x000199CB
		public override bool MoveToAttribute(string name)
		{
			this.CheckAsync();
			return this.coreReader.MoveToAttribute(name);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001B7DF File Offset: 0x000199DF
		public override bool MoveToAttribute(string name, string ns)
		{
			this.CheckAsync();
			return this.coreReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001B7F4 File Offset: 0x000199F4
		public override void MoveToAttribute(int i)
		{
			this.CheckAsync();
			this.coreReader.MoveToAttribute(i);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001B808 File Offset: 0x00019A08
		public override bool MoveToFirstAttribute()
		{
			this.CheckAsync();
			return this.coreReader.MoveToFirstAttribute();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001B81B File Offset: 0x00019A1B
		public override bool MoveToNextAttribute()
		{
			this.CheckAsync();
			return this.coreReader.MoveToNextAttribute();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001B82E File Offset: 0x00019A2E
		public override bool MoveToElement()
		{
			this.CheckAsync();
			return this.coreReader.MoveToElement();
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001B841 File Offset: 0x00019A41
		public override bool ReadAttributeValue()
		{
			this.CheckAsync();
			return this.coreReader.ReadAttributeValue();
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001B854 File Offset: 0x00019A54
		public override bool Read()
		{
			this.CheckAsync();
			return this.coreReader.Read();
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001B867 File Offset: 0x00019A67
		public override bool EOF
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.EOF;
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001B87A File Offset: 0x00019A7A
		public override void Close()
		{
			this.CheckAsync();
			this.coreReader.Close();
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001B88D File Offset: 0x00019A8D
		public override ReadState ReadState
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.ReadState;
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001B8A0 File Offset: 0x00019AA0
		public override void Skip()
		{
			this.CheckAsync();
			this.coreReader.Skip();
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001B8B3 File Offset: 0x00019AB3
		public override XmlNameTable NameTable
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NameTable;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001B8C6 File Offset: 0x00019AC6
		public override string LookupNamespace(string prefix)
		{
			this.CheckAsync();
			return this.coreReader.LookupNamespace(prefix);
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001B8DA File Offset: 0x00019ADA
		public override bool CanResolveEntity
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanResolveEntity;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001B8ED File Offset: 0x00019AED
		public override void ResolveEntity()
		{
			this.CheckAsync();
			this.coreReader.ResolveEntity();
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001B900 File Offset: 0x00019B00
		public override bool CanReadBinaryContent
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanReadBinaryContent;
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001B913 File Offset: 0x00019B13
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001B929 File Offset: 0x00019B29
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001B93F File Offset: 0x00019B3F
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001B955 File Offset: 0x00019B55
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001B96B File Offset: 0x00019B6B
		public override bool CanReadValueChunk
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanReadValueChunk;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001B97E File Offset: 0x00019B7E
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001B994 File Offset: 0x00019B94
		public override string ReadString()
		{
			this.CheckAsync();
			return this.coreReader.ReadString();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001B9A7 File Offset: 0x00019BA7
		public override XmlNodeType MoveToContent()
		{
			this.CheckAsync();
			return this.coreReader.MoveToContent();
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001B9BA File Offset: 0x00019BBA
		public override void ReadStartElement()
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001B9CD File Offset: 0x00019BCD
		public override void ReadStartElement(string name)
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement(name);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001B9E1 File Offset: 0x00019BE1
		public override void ReadStartElement(string localname, string ns)
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement(localname, ns);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001B9F6 File Offset: 0x00019BF6
		public override string ReadElementString()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString();
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001BA09 File Offset: 0x00019C09
		public override string ReadElementString(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString(name);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001BA1D File Offset: 0x00019C1D
		public override string ReadElementString(string localname, string ns)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString(localname, ns);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001BA32 File Offset: 0x00019C32
		public override void ReadEndElement()
		{
			this.CheckAsync();
			this.coreReader.ReadEndElement();
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001BA45 File Offset: 0x00019C45
		public override bool IsStartElement()
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement();
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001BA58 File Offset: 0x00019C58
		public override bool IsStartElement(string name)
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement(name);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001BA6C File Offset: 0x00019C6C
		public override bool IsStartElement(string localname, string ns)
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement(localname, ns);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001BA81 File Offset: 0x00019C81
		public override bool ReadToFollowing(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToFollowing(name);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001BA95 File Offset: 0x00019C95
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001BAAA File Offset: 0x00019CAA
		public override bool ReadToDescendant(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToDescendant(name);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001BABE File Offset: 0x00019CBE
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001BAD3 File Offset: 0x00019CD3
		public override bool ReadToNextSibling(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToNextSibling(name);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001BAE7 File Offset: 0x00019CE7
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001BAFC File Offset: 0x00019CFC
		public override string ReadInnerXml()
		{
			this.CheckAsync();
			return this.coreReader.ReadInnerXml();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001BB0F File Offset: 0x00019D0F
		public override string ReadOuterXml()
		{
			this.CheckAsync();
			return this.coreReader.ReadOuterXml();
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001BB22 File Offset: 0x00019D22
		public override XmlReader ReadSubtree()
		{
			this.CheckAsync();
			return XmlAsyncCheckReader.CreateAsyncCheckWrapper(this.coreReader.ReadSubtree());
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001BB3A File Offset: 0x00019D3A
		public override bool HasAttributes
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.HasAttributes;
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001BB4D File Offset: 0x00019D4D
		protected override void Dispose(bool disposing)
		{
			this.CheckAsync();
			this.coreReader.Dispose();
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001BB60 File Offset: 0x00019D60
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NamespaceManager;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001BB73 File Offset: 0x00019D73
		internal override IDtdInfo DtdInfo
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.DtdInfo;
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001BB88 File Offset: 0x00019D88
		public override Task<string> GetValueAsync()
		{
			this.CheckAsync();
			Task<string> valueAsync = this.coreReader.GetValueAsync();
			this.lastTask = valueAsync;
			return valueAsync;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001BBB0 File Offset: 0x00019DB0
		public override Task<object> ReadContentAsObjectAsync()
		{
			this.CheckAsync();
			Task<object> task = this.coreReader.ReadContentAsObjectAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		public override Task<string> ReadContentAsStringAsync()
		{
			this.CheckAsync();
			Task<string> task = this.coreReader.ReadContentAsStringAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001BC00 File Offset: 0x00019E00
		public override Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			Task<object> task = this.coreReader.ReadContentAsAsync(returnType, namespaceResolver);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001BC2C File Offset: 0x00019E2C
		public override Task<object> ReadElementContentAsObjectAsync()
		{
			this.CheckAsync();
			Task<object> task = this.coreReader.ReadElementContentAsObjectAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001BC54 File Offset: 0x00019E54
		public override Task<string> ReadElementContentAsStringAsync()
		{
			this.CheckAsync();
			Task<string> task = this.coreReader.ReadElementContentAsStringAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001BC7C File Offset: 0x00019E7C
		public override Task<object> ReadElementContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			Task<object> task = this.coreReader.ReadElementContentAsAsync(returnType, namespaceResolver);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001BCA8 File Offset: 0x00019EA8
		public override Task<bool> ReadAsync()
		{
			this.CheckAsync();
			Task<bool> task = this.coreReader.ReadAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		public override Task SkipAsync()
		{
			this.CheckAsync();
			Task task = this.coreReader.SkipAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001BCF8 File Offset: 0x00019EF8
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> task = this.coreReader.ReadContentAsBase64Async(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001BD24 File Offset: 0x00019F24
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> task = this.coreReader.ReadElementContentAsBase64Async(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001BD50 File Offset: 0x00019F50
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> task = this.coreReader.ReadContentAsBinHexAsync(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001BD7C File Offset: 0x00019F7C
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> task = this.coreReader.ReadElementContentAsBinHexAsync(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		public override Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> task = this.coreReader.ReadValueChunkAsync(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001BDD4 File Offset: 0x00019FD4
		public override Task<XmlNodeType> MoveToContentAsync()
		{
			this.CheckAsync();
			Task<XmlNodeType> task = this.coreReader.MoveToContentAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001BDFC File Offset: 0x00019FFC
		public override Task<string> ReadInnerXmlAsync()
		{
			this.CheckAsync();
			Task<string> task = this.coreReader.ReadInnerXmlAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001BE24 File Offset: 0x0001A024
		public override Task<string> ReadOuterXmlAsync()
		{
			this.CheckAsync();
			Task<string> task = this.coreReader.ReadOuterXmlAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x040003DB RID: 987
		private readonly XmlReader coreReader;

		// Token: 0x040003DC RID: 988
		private Task lastTask = AsyncHelper.DoneTask;
	}
}
