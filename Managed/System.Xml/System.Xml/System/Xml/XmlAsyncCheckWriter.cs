using System;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C7 RID: 199
	internal class XmlAsyncCheckWriter : XmlWriter
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001BF75 File Offset: 0x0001A175
		internal XmlWriter CoreWriter
		{
			get
			{
				return this.coreWriter;
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001BF7D File Offset: 0x0001A17D
		public XmlAsyncCheckWriter(XmlWriter writer)
		{
			this.coreWriter = writer;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001BF97 File Offset: 0x0001A197
		private void CheckAsync()
		{
			if (!this.lastTask.IsCompleted)
			{
				throw new InvalidOperationException(Res.GetString("An asynchronous operation is already in progress."));
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings xmlWriterSettings = this.coreWriter.Settings;
				if (xmlWriterSettings != null)
				{
					xmlWriterSettings = xmlWriterSettings.Clone();
				}
				else
				{
					xmlWriterSettings = new XmlWriterSettings();
				}
				xmlWriterSettings.Async = true;
				xmlWriterSettings.ReadOnly = true;
				return xmlWriterSettings;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001BFF2 File Offset: 0x0001A1F2
		public override void WriteStartDocument()
		{
			this.CheckAsync();
			this.coreWriter.WriteStartDocument();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001C005 File Offset: 0x0001A205
		public override void WriteStartDocument(bool standalone)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartDocument(standalone);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001C019 File Offset: 0x0001A219
		public override void WriteEndDocument()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndDocument();
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001C02C File Offset: 0x0001A22C
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.CheckAsync();
			this.coreWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001C044 File Offset: 0x0001A244
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001C05A File Offset: 0x0001A25A
		public override void WriteEndElement()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndElement();
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001C06D File Offset: 0x0001A26D
		public override void WriteFullEndElement()
		{
			this.CheckAsync();
			this.coreWriter.WriteFullEndElement();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001C080 File Offset: 0x0001A280
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001C096 File Offset: 0x0001A296
		public override void WriteEndAttribute()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndAttribute();
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001C0A9 File Offset: 0x0001A2A9
		public override void WriteCData(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteCData(text);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001C0BD File Offset: 0x0001A2BD
		public override void WriteComment(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteComment(text);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001C0D1 File Offset: 0x0001A2D1
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001C0E6 File Offset: 0x0001A2E6
		public override void WriteEntityRef(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteEntityRef(name);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001C0FA File Offset: 0x0001A2FA
		public override void WriteCharEntity(char ch)
		{
			this.CheckAsync();
			this.coreWriter.WriteCharEntity(ch);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001C10E File Offset: 0x0001A30E
		public override void WriteWhitespace(string ws)
		{
			this.CheckAsync();
			this.coreWriter.WriteWhitespace(ws);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001C122 File Offset: 0x0001A322
		public override void WriteString(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteString(text);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001C136 File Offset: 0x0001A336
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.CheckAsync();
			this.coreWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001C14B File Offset: 0x0001A34B
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001C161 File Offset: 0x0001A361
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001C177 File Offset: 0x0001A377
		public override void WriteRaw(string data)
		{
			this.CheckAsync();
			this.coreWriter.WriteRaw(data);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001C18B File Offset: 0x0001A38B
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001C1A1 File Offset: 0x0001A3A1
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001C1B7 File Offset: 0x0001A3B7
		public override WriteState WriteState
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.WriteState;
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001C1CA File Offset: 0x0001A3CA
		public override void Close()
		{
			this.CheckAsync();
			this.coreWriter.Close();
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001C1DD File Offset: 0x0001A3DD
		public override void Flush()
		{
			this.CheckAsync();
			this.coreWriter.Flush();
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001C1F0 File Offset: 0x0001A3F0
		public override string LookupPrefix(string ns)
		{
			this.CheckAsync();
			return this.coreWriter.LookupPrefix(ns);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001C204 File Offset: 0x0001A404
		public override XmlSpace XmlSpace
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.XmlSpace;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001C217 File Offset: 0x0001A417
		public override string XmlLang
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.XmlLang;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001C22A File Offset: 0x0001A42A
		public override void WriteNmToken(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteNmToken(name);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001C23E File Offset: 0x0001A43E
		public override void WriteName(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteName(name);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001C252 File Offset: 0x0001A452
		public override void WriteQualifiedName(string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001C267 File Offset: 0x0001A467
		public override void WriteValue(object value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001C27B File Offset: 0x0001A47B
		public override void WriteValue(string value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001C28F File Offset: 0x0001A48F
		public override void WriteValue(bool value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001C2A3 File Offset: 0x0001A4A3
		public override void WriteValue(DateTime value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001C2B7 File Offset: 0x0001A4B7
		public override void WriteValue(DateTimeOffset value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001C2CB File Offset: 0x0001A4CB
		public override void WriteValue(double value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001C2DF File Offset: 0x0001A4DF
		public override void WriteValue(float value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001C2F3 File Offset: 0x0001A4F3
		public override void WriteValue(decimal value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001C307 File Offset: 0x0001A507
		public override void WriteValue(int value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001C31B File Offset: 0x0001A51B
		public override void WriteValue(long value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001C32F File Offset: 0x0001A52F
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteAttributes(reader, defattr);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001C344 File Offset: 0x0001A544
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteNode(reader, defattr);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001C359 File Offset: 0x0001A559
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteNode(navigator, defattr);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001C36E File Offset: 0x0001A56E
		protected override void Dispose(bool disposing)
		{
			this.CheckAsync();
			this.coreWriter.Dispose();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001C384 File Offset: 0x0001A584
		public override Task WriteStartDocumentAsync()
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteStartDocumentAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteStartDocumentAsync(standalone);
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001C3D4 File Offset: 0x0001A5D4
		public override Task WriteEndDocumentAsync()
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteEndDocumentAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteDocTypeAsync(name, pubid, sysid, subset);
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001C428 File Offset: 0x0001A628
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteStartElementAsync(prefix, localName, ns);
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001C454 File Offset: 0x0001A654
		public override Task WriteEndElementAsync()
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteEndElementAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001C47C File Offset: 0x0001A67C
		public override Task WriteFullEndElementAsync()
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteFullEndElementAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteStartAttributeAsync(prefix, localName, ns);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteEndAttributeAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		public override Task WriteCDataAsync(string text)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteCDataAsync(text);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001C520 File Offset: 0x0001A720
		public override Task WriteCommentAsync(string text)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteCommentAsync(text);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001C548 File Offset: 0x0001A748
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteProcessingInstructionAsync(name, text);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001C574 File Offset: 0x0001A774
		public override Task WriteEntityRefAsync(string name)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteEntityRefAsync(name);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001C59C File Offset: 0x0001A79C
		public override Task WriteCharEntityAsync(char ch)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteCharEntityAsync(ch);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001C5C4 File Offset: 0x0001A7C4
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteWhitespaceAsync(ws);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001C5EC File Offset: 0x0001A7EC
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteStringAsync(text);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001C614 File Offset: 0x0001A814
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteSurrogateCharEntityAsync(lowChar, highChar);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001C640 File Offset: 0x0001A840
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteCharsAsync(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001C66C File Offset: 0x0001A86C
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteRawAsync(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001C698 File Offset: 0x0001A898
		public override Task WriteRawAsync(string data)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteRawAsync(data);
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteBase64Async(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001C6EC File Offset: 0x0001A8EC
		public override Task WriteBinHexAsync(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteBinHexAsync(buffer, index, count);
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001C718 File Offset: 0x0001A918
		public override Task FlushAsync()
		{
			this.CheckAsync();
			Task task = this.coreWriter.FlushAsync();
			this.lastTask = task;
			return task;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001C740 File Offset: 0x0001A940
		public override Task WriteNmTokenAsync(string name)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteNmTokenAsync(name);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001C768 File Offset: 0x0001A968
		public override Task WriteNameAsync(string name)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteNameAsync(name);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001C790 File Offset: 0x0001A990
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteQualifiedNameAsync(localName, ns);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		public override Task WriteAttributesAsync(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteAttributesAsync(reader, defattr);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public override Task WriteNodeAsync(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteNodeAsync(reader, defattr);
			this.lastTask = task;
			return task;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001C814 File Offset: 0x0001AA14
		public override Task WriteNodeAsync(XPathNavigator navigator, bool defattr)
		{
			this.CheckAsync();
			Task task = this.coreWriter.WriteNodeAsync(navigator, defattr);
			this.lastTask = task;
			return task;
		}

		// Token: 0x040003E1 RID: 993
		private readonly XmlWriter coreWriter;

		// Token: 0x040003E2 RID: 994
		private Task lastTask = AsyncHelper.DoneTask;
	}
}
