using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x020000C8 RID: 200
	internal class XmlAutoDetectWriter : XmlRawWriter, IRemovableWriter
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x0001C83D File Offset: 0x0001AA3D
		private XmlAutoDetectWriter(XmlWriterSettings writerSettings)
		{
			this.writerSettings = writerSettings.Clone();
			this.writerSettings.ReadOnly = true;
			this.eventCache = new XmlEventCache(string.Empty, true);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001C86E File Offset: 0x0001AA6E
		public XmlAutoDetectWriter(TextWriter textWriter, XmlWriterSettings writerSettings)
			: this(writerSettings)
		{
			this.textWriter = textWriter;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001C87E File Offset: 0x0001AA7E
		public XmlAutoDetectWriter(Stream strm, XmlWriterSettings writerSettings)
			: this(writerSettings)
		{
			this.strm = strm;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001C88E File Offset: 0x0001AA8E
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001C896 File Offset: 0x0001AA96
		public OnRemoveWriter OnRemoveWriterEvent
		{
			get
			{
				return this.onRemove;
			}
			set
			{
				this.onRemove = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001C89F File Offset: 0x0001AA9F
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writerSettings;
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001C8A7 File Offset: 0x0001AAA7
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001C8C0 File Offset: 0x0001AAC0
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.wrapped == null)
			{
				if (ns.Length == 0 && XmlAutoDetectWriter.IsHtmlTag(localName))
				{
					this.CreateWrappedWriter(XmlOutputMethod.Html);
				}
				else
				{
					this.CreateWrappedWriter(XmlOutputMethod.Xml);
				}
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001C8F8 File Offset: 0x0001AAF8
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001C90F File Offset: 0x0001AB0F
		public override void WriteEndAttribute()
		{
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001C91C File Offset: 0x0001AB1C
		public override void WriteCData(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.eventCache.WriteCData(text);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001C940 File Offset: 0x0001AB40
		public override void WriteComment(string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteComment(text);
				return;
			}
			this.wrapped.WriteComment(text);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001C963 File Offset: 0x0001AB63
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteProcessingInstruction(name, text);
				return;
			}
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001C988 File Offset: 0x0001AB88
		public override void WriteWhitespace(string ws)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteWhitespace(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001C9AB File Offset: 0x0001ABAB
		public override void WriteString(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteString(text);
				return;
			}
			this.eventCache.WriteString(text);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001C9DF File Offset: 0x0001ABDF
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteRaw(new string(buffer, index, count));
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001C9EF File Offset: 0x0001ABEF
		public override void WriteRaw(string data)
		{
			if (this.TextBlockCreatesWriter(data))
			{
				this.wrapped.WriteRaw(data);
				return;
			}
			this.eventCache.WriteRaw(data);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001CA13 File Offset: 0x0001AC13
		public override void WriteEntityRef(string name)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001CA28 File Offset: 0x0001AC28
		public override void WriteCharEntity(char ch)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001CA3D File Offset: 0x0001AC3D
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001CA53 File Offset: 0x0001AC53
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBase64(buffer, index, count);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001CA6A File Offset: 0x0001AC6A
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBinHex(buffer, index, count);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001CA81 File Offset: 0x0001AC81
		public override void Close()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Close();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001CA95 File Offset: 0x0001AC95
		public override void Flush()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Flush();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001CAA9 File Offset: 0x0001ACA9
		public override void WriteValue(object value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001CABE File Offset: 0x0001ACBE
		public override void WriteValue(string value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001CAD3 File Offset: 0x0001ACD3
		public override void WriteValue(bool value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
		public override void WriteValue(DateTime value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001CAFD File Offset: 0x0001ACFD
		public override void WriteValue(DateTimeOffset value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001CB12 File Offset: 0x0001AD12
		public override void WriteValue(double value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001CB27 File Offset: 0x0001AD27
		public override void WriteValue(float value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		public override void WriteValue(decimal value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001CB51 File Offset: 0x0001AD51
		public override void WriteValue(int value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001CB66 File Offset: 0x0001AD66
		public override void WriteValue(long value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x000184B8 File Offset: 0x000166B8
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001CB7B File Offset: 0x0001AD7B
		internal override IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
				if (this.wrapped == null)
				{
					this.eventCache.NamespaceResolver = value;
					return;
				}
				this.wrapped.NamespaceResolver = value;
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001CBA5 File Offset: 0x0001ADA5
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001CBBA File Offset: 0x0001ADBA
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001CBCF File Offset: 0x0001ADCF
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001CBDC File Offset: 0x0001ADDC
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001CBEC File Offset: 0x0001ADEC
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001CC12 File Offset: 0x0001AE12
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return this.wrapped.SupportsNamespaceDeclarationInChunks;
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001CC1F File Offset: 0x0001AE1F
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteStartNamespaceDeclaration(prefix);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001CC34 File Offset: 0x0001AE34
		internal override void WriteEndNamespaceDeclaration()
		{
			this.wrapped.WriteEndNamespaceDeclaration();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001CC44 File Offset: 0x0001AE44
		private static bool IsHtmlTag(string tagName)
		{
			return tagName.Length == 4 && (tagName[0] == 'H' || tagName[0] == 'h') && (tagName[1] == 'T' || tagName[1] == 't') && (tagName[2] == 'M' || tagName[2] == 'm') && (tagName[3] == 'L' || tagName[3] == 'l');
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001CCBD File Offset: 0x0001AEBD
		private void EnsureWrappedWriter(XmlOutputMethod outMethod)
		{
			if (this.wrapped == null)
			{
				this.CreateWrappedWriter(outMethod);
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001CCD0 File Offset: 0x0001AED0
		private bool TextBlockCreatesWriter(string textBlock)
		{
			if (this.wrapped == null)
			{
				if (XmlCharType.Instance.IsOnlyWhitespace(textBlock))
				{
					return false;
				}
				this.CreateWrappedWriter(XmlOutputMethod.Xml);
			}
			return true;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001CD00 File Offset: 0x0001AF00
		private void CreateWrappedWriter(XmlOutputMethod outMethod)
		{
			this.writerSettings.ReadOnly = false;
			this.writerSettings.OutputMethod = outMethod;
			if (outMethod == XmlOutputMethod.Html && this.writerSettings.IndentInternal == TriState.Unknown)
			{
				this.writerSettings.Indent = true;
			}
			this.writerSettings.ReadOnly = true;
			if (this.textWriter != null)
			{
				this.wrapped = ((XmlWellFormedWriter)XmlWriter.Create(this.textWriter, this.writerSettings)).RawWriter;
			}
			else
			{
				this.wrapped = ((XmlWellFormedWriter)XmlWriter.Create(this.strm, this.writerSettings)).RawWriter;
			}
			this.eventCache.EndEvents();
			this.eventCache.EventsToWriter(this.wrapped);
			if (this.onRemove != null)
			{
				this.onRemove(this.wrapped);
			}
		}

		// Token: 0x040003E3 RID: 995
		private XmlRawWriter wrapped;

		// Token: 0x040003E4 RID: 996
		private OnRemoveWriter onRemove;

		// Token: 0x040003E5 RID: 997
		private XmlWriterSettings writerSettings;

		// Token: 0x040003E6 RID: 998
		private XmlEventCache eventCache;

		// Token: 0x040003E7 RID: 999
		private TextWriter textWriter;

		// Token: 0x040003E8 RID: 1000
		private Stream strm;
	}
}
