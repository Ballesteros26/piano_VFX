using System;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020001D7 RID: 471
	internal class XmlWrappingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x00062DE6 File Offset: 0x00060FE6
		internal XmlWrappingReader(XmlReader baseReader)
		{
			this.reader = baseReader;
			this.readerAsIXmlLineInfo = baseReader as IXmlLineInfo;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00062E01 File Offset: 0x00061001
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.reader.Settings;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00062E0E File Offset: 0x0006100E
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00062E1B File Offset: 0x0006101B
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00062E28 File Offset: 0x00061028
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x00062E35 File Offset: 0x00061035
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00062E42 File Offset: 0x00061042
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x00062E4F File Offset: 0x0006104F
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00062E5C File Offset: 0x0006105C
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00062E69 File Offset: 0x00061069
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0002D81A File Offset: 0x0002BA1A
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0002D827 File Offset: 0x0002BA27
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00062E76 File Offset: 0x00061076
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00062E83 File Offset: 0x00061083
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x00062E90 File Offset: 0x00061090
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x00062E9D File Offset: 0x0006109D
		public override Type ValueType
		{
			get
			{
				return this.reader.ValueType;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00062EAA File Offset: 0x000610AA
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00062EB7 File Offset: 0x000610B7
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00062EC4 File Offset: 0x000610C4
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00062ED1 File Offset: 0x000610D1
		public override bool HasAttributes
		{
			get
			{
				return this.reader.HasAttributes;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0002D86D File Offset: 0x0002BA6D
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00062EDE File Offset: 0x000610DE
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00062EEB File Offset: 0x000610EB
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.reader.SchemaInfo;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x00062EF8 File Offset: 0x000610F8
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00062F05 File Offset: 0x00061105
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00062F13 File Offset: 0x00061113
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.reader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00062F22 File Offset: 0x00061122
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00062F30 File Offset: 0x00061130
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00062F3E File Offset: 0x0006113E
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00062F4D File Offset: 0x0006114D
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00062F5B File Offset: 0x0006115B
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00062F68 File Offset: 0x00061168
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00062F75 File Offset: 0x00061175
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00062F82 File Offset: 0x00061182
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00062F8F File Offset: 0x0006118F
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00062F9C File Offset: 0x0006119C
		public override void Skip()
		{
			this.reader.Skip();
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00062FA9 File Offset: 0x000611A9
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00062FB7 File Offset: 0x000611B7
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00062FC4 File Offset: 0x000611C4
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00062FD1 File Offset: 0x000611D1
		public virtual bool HasLineInfo()
		{
			return this.readerAsIXmlLineInfo != null && this.readerAsIXmlLineInfo.HasLineInfo();
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00062FE8 File Offset: 0x000611E8
		public virtual int LineNumber
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00062FFF File Offset: 0x000611FF
		public virtual int LinePosition
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00063016 File Offset: 0x00061216
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.reader.DtdInfo;
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00063023 File Offset: 0x00061223
		public override Task<string> GetValueAsync()
		{
			return this.reader.GetValueAsync();
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00063030 File Offset: 0x00061230
		public override Task<bool> ReadAsync()
		{
			return this.reader.ReadAsync();
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0006303D File Offset: 0x0006123D
		public override Task SkipAsync()
		{
			return this.reader.SkipAsync();
		}

		// Token: 0x04000BE4 RID: 3044
		protected XmlReader reader;

		// Token: 0x04000BE5 RID: 3045
		protected IXmlLineInfo readerAsIXmlLineInfo;
	}
}
