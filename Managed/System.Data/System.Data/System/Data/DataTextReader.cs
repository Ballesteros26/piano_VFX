using System;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000115 RID: 277
	internal sealed class DataTextReader : XmlReader
	{
		// Token: 0x06000E21 RID: 3617 RVA: 0x0004B69B File Offset: 0x0004989B
		internal static XmlReader CreateReader(XmlReader xr)
		{
			return new DataTextReader(xr);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0004B6A3 File Offset: 0x000498A3
		private DataTextReader(XmlReader input)
		{
			this._xmlreader = input;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0004B6B2 File Offset: 0x000498B2
		public override XmlReaderSettings Settings
		{
			get
			{
				return this._xmlreader.Settings;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0004B6BF File Offset: 0x000498BF
		public override XmlNodeType NodeType
		{
			get
			{
				return this._xmlreader.NodeType;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0004B6CC File Offset: 0x000498CC
		public override string Name
		{
			get
			{
				return this._xmlreader.Name;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0004B6D9 File Offset: 0x000498D9
		public override string LocalName
		{
			get
			{
				return this._xmlreader.LocalName;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0004B6E6 File Offset: 0x000498E6
		public override string NamespaceURI
		{
			get
			{
				return this._xmlreader.NamespaceURI;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0004B6F3 File Offset: 0x000498F3
		public override string Prefix
		{
			get
			{
				return this._xmlreader.Prefix;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0004B700 File Offset: 0x00049900
		public override bool HasValue
		{
			get
			{
				return this._xmlreader.HasValue;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0004B70D File Offset: 0x0004990D
		public override string Value
		{
			get
			{
				return this._xmlreader.Value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0004B71A File Offset: 0x0004991A
		public override int Depth
		{
			get
			{
				return this._xmlreader.Depth;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0004B727 File Offset: 0x00049927
		public override string BaseURI
		{
			get
			{
				return this._xmlreader.BaseURI;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0004B734 File Offset: 0x00049934
		public override bool IsEmptyElement
		{
			get
			{
				return this._xmlreader.IsEmptyElement;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0004B741 File Offset: 0x00049941
		public override bool IsDefault
		{
			get
			{
				return this._xmlreader.IsDefault;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0004B74E File Offset: 0x0004994E
		public override char QuoteChar
		{
			get
			{
				return this._xmlreader.QuoteChar;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0004B75B File Offset: 0x0004995B
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmlreader.XmlSpace;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0004B768 File Offset: 0x00049968
		public override string XmlLang
		{
			get
			{
				return this._xmlreader.XmlLang;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0004B775 File Offset: 0x00049975
		public override int AttributeCount
		{
			get
			{
				return this._xmlreader.AttributeCount;
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0004B782 File Offset: 0x00049982
		public override string GetAttribute(string name)
		{
			return this._xmlreader.GetAttribute(name);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0004B790 File Offset: 0x00049990
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0004B79F File Offset: 0x0004999F
		public override string GetAttribute(int i)
		{
			return this._xmlreader.GetAttribute(i);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0004B7AD File Offset: 0x000499AD
		public override bool MoveToAttribute(string name)
		{
			return this._xmlreader.MoveToAttribute(name);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0004B7BB File Offset: 0x000499BB
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0004B7CA File Offset: 0x000499CA
		public override void MoveToAttribute(int i)
		{
			this._xmlreader.MoveToAttribute(i);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0004B7D8 File Offset: 0x000499D8
		public override bool MoveToFirstAttribute()
		{
			return this._xmlreader.MoveToFirstAttribute();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0004B7E5 File Offset: 0x000499E5
		public override bool MoveToNextAttribute()
		{
			return this._xmlreader.MoveToNextAttribute();
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0004B7F2 File Offset: 0x000499F2
		public override bool MoveToElement()
		{
			return this._xmlreader.MoveToElement();
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0004B7FF File Offset: 0x000499FF
		public override bool ReadAttributeValue()
		{
			return this._xmlreader.ReadAttributeValue();
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0004B80C File Offset: 0x00049A0C
		public override bool Read()
		{
			return this._xmlreader.Read();
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0004B819 File Offset: 0x00049A19
		public override bool EOF
		{
			get
			{
				return this._xmlreader.EOF;
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0004B826 File Offset: 0x00049A26
		public override void Close()
		{
			this._xmlreader.Close();
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0004B833 File Offset: 0x00049A33
		public override ReadState ReadState
		{
			get
			{
				return this._xmlreader.ReadState;
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0004B840 File Offset: 0x00049A40
		public override void Skip()
		{
			this._xmlreader.Skip();
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0004B84D File Offset: 0x00049A4D
		public override XmlNameTable NameTable
		{
			get
			{
				return this._xmlreader.NameTable;
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0004B85A File Offset: 0x00049A5A
		public override string LookupNamespace(string prefix)
		{
			return this._xmlreader.LookupNamespace(prefix);
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0004B868 File Offset: 0x00049A68
		public override bool CanResolveEntity
		{
			get
			{
				return this._xmlreader.CanResolveEntity;
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0004B875 File Offset: 0x00049A75
		public override void ResolveEntity()
		{
			this._xmlreader.ResolveEntity();
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0004B882 File Offset: 0x00049A82
		public override bool CanReadBinaryContent
		{
			get
			{
				return this._xmlreader.CanReadBinaryContent;
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0004B88F File Offset: 0x00049A8F
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0004B89F File Offset: 0x00049A9F
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0004B8AF File Offset: 0x00049AAF
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0004B8BF File Offset: 0x00049ABF
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0004B8CF File Offset: 0x00049ACF
		public override bool CanReadValueChunk
		{
			get
			{
				return this._xmlreader.CanReadValueChunk;
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0004B8DC File Offset: 0x00049ADC
		public override string ReadString()
		{
			return this._xmlreader.ReadString();
		}

		// Token: 0x040009EF RID: 2543
		private XmlReader _xmlreader;
	}
}
