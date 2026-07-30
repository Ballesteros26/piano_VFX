using System;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004ED RID: 1261
	internal class BuilderInfo
	{
		// Token: 0x0600334D RID: 13133 RVA: 0x00125A0D File Offset: 0x00123C0D
		internal BuilderInfo()
		{
			this.Initialize(string.Empty, string.Empty, string.Empty);
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x00125A36 File Offset: 0x00123C36
		internal void Initialize(string prefix, string name, string nspace)
		{
			this.prefix = prefix;
			this.localName = name;
			this.namespaceURI = nspace;
			this.name = null;
			this.htmlProps = null;
			this.htmlAttrProps = null;
			this.TextInfoCount = 0;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x00125A6C File Offset: 0x00123C6C
		internal void Initialize(BuilderInfo src)
		{
			this.prefix = src.Prefix;
			this.localName = src.LocalName;
			this.namespaceURI = src.NamespaceURI;
			this.name = null;
			this.depth = src.Depth;
			this.nodeType = src.NodeType;
			this.htmlProps = src.htmlProps;
			this.htmlAttrProps = src.htmlAttrProps;
			this.TextInfoCount = 0;
			this.EnsureTextInfoSize(src.TextInfoCount);
			src.TextInfo.CopyTo(this.TextInfo, 0);
			this.TextInfoCount = src.TextInfoCount;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x00125B08 File Offset: 0x00123D08
		private void EnsureTextInfoSize(int newSize)
		{
			if (this.TextInfo.Length < newSize)
			{
				string[] array = new string[newSize * 2];
				Array.Copy(this.TextInfo, array, this.TextInfoCount);
				this.TextInfo = array;
			}
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x00125B42 File Offset: 0x00123D42
		internal BuilderInfo Clone()
		{
			BuilderInfo builderInfo = new BuilderInfo();
			builderInfo.Initialize(this);
			return builderInfo;
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x00125B50 File Offset: 0x00123D50
		internal string Name
		{
			get
			{
				if (this.name == null)
				{
					string text = this.Prefix;
					string text2 = this.LocalName;
					if (text != null && 0 < text.Length)
					{
						if (text2.Length > 0)
						{
							this.name = text + ":" + text2;
						}
						else
						{
							this.name = text;
						}
					}
					else
					{
						this.name = text2;
					}
				}
				return this.name;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x00125BB2 File Offset: 0x00123DB2
		// (set) Token: 0x06003354 RID: 13140 RVA: 0x00125BBA File Offset: 0x00123DBA
		internal string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x00125BC3 File Offset: 0x00123DC3
		// (set) Token: 0x06003356 RID: 13142 RVA: 0x00125BCB File Offset: 0x00123DCB
		internal string NamespaceURI
		{
			get
			{
				return this.namespaceURI;
			}
			set
			{
				this.namespaceURI = value;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x00125BD4 File Offset: 0x00123DD4
		// (set) Token: 0x06003358 RID: 13144 RVA: 0x00125BDC File Offset: 0x00123DDC
		internal string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06003359 RID: 13145 RVA: 0x00125BE8 File Offset: 0x00123DE8
		// (set) Token: 0x0600335A RID: 13146 RVA: 0x00125C79 File Offset: 0x00123E79
		internal string Value
		{
			get
			{
				int textInfoCount = this.TextInfoCount;
				if (textInfoCount == 0)
				{
					return string.Empty;
				}
				if (textInfoCount != 1)
				{
					int num = 0;
					for (int i = 0; i < this.TextInfoCount; i++)
					{
						string text = this.TextInfo[i];
						if (text != null)
						{
							num += text.Length;
						}
					}
					StringBuilder stringBuilder = new StringBuilder(num);
					for (int j = 0; j < this.TextInfoCount; j++)
					{
						string text2 = this.TextInfo[j];
						if (text2 != null)
						{
							stringBuilder.Append(text2);
						}
					}
					return stringBuilder.ToString();
				}
				return this.TextInfo[0];
			}
			set
			{
				this.TextInfoCount = 0;
				this.ValueAppend(value, false);
			}
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x00125C8C File Offset: 0x00123E8C
		internal void ValueAppend(string s, bool disableEscaping)
		{
			if (s == null || s.Length == 0)
			{
				return;
			}
			this.EnsureTextInfoSize(this.TextInfoCount + (disableEscaping ? 2 : 1));
			int num;
			if (disableEscaping)
			{
				string[] textInfo = this.TextInfo;
				num = this.TextInfoCount;
				this.TextInfoCount = num + 1;
				textInfo[num] = null;
			}
			string[] textInfo2 = this.TextInfo;
			num = this.TextInfoCount;
			this.TextInfoCount = num + 1;
			textInfo2[num] = s;
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x00125CEE File Offset: 0x00123EEE
		// (set) Token: 0x0600335D RID: 13149 RVA: 0x00125CF6 File Offset: 0x00123EF6
		internal XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType = value;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x00125CFF File Offset: 0x00123EFF
		// (set) Token: 0x0600335F RID: 13151 RVA: 0x00125D07 File Offset: 0x00123F07
		internal int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x00125D10 File Offset: 0x00123F10
		// (set) Token: 0x06003361 RID: 13153 RVA: 0x00125D18 File Offset: 0x00123F18
		internal bool IsEmptyTag
		{
			get
			{
				return this.isEmptyTag;
			}
			set
			{
				this.isEmptyTag = value;
			}
		}

		// Token: 0x0400212C RID: 8492
		private string name;

		// Token: 0x0400212D RID: 8493
		private string localName;

		// Token: 0x0400212E RID: 8494
		private string namespaceURI;

		// Token: 0x0400212F RID: 8495
		private string prefix;

		// Token: 0x04002130 RID: 8496
		private XmlNodeType nodeType;

		// Token: 0x04002131 RID: 8497
		private int depth;

		// Token: 0x04002132 RID: 8498
		private bool isEmptyTag;

		// Token: 0x04002133 RID: 8499
		internal string[] TextInfo = new string[4];

		// Token: 0x04002134 RID: 8500
		internal int TextInfoCount;

		// Token: 0x04002135 RID: 8501
		internal bool search;

		// Token: 0x04002136 RID: 8502
		internal HtmlElementProps htmlProps;

		// Token: 0x04002137 RID: 8503
		internal HtmlAttributeProps htmlAttrProps;
	}
}
