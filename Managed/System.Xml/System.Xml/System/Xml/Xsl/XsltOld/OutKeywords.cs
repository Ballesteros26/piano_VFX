using System;
using System.Diagnostics;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200052D RID: 1325
	internal class OutKeywords
	{
		// Token: 0x06003544 RID: 13636 RVA: 0x0012C708 File Offset: 0x0012A908
		internal OutKeywords(XmlNameTable nameTable)
		{
			this._AtomEmpty = nameTable.Add(string.Empty);
			this._AtomLang = nameTable.Add("lang");
			this._AtomSpace = nameTable.Add("space");
			this._AtomXmlns = nameTable.Add("xmlns");
			this._AtomXml = nameTable.Add("xml");
			this._AtomXmlNamespace = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this._AtomXmlnsNamespace = nameTable.Add("http://www.w3.org/2000/xmlns/");
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x0012C792 File Offset: 0x0012A992
		internal string Empty
		{
			get
			{
				return this._AtomEmpty;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x0012C79A File Offset: 0x0012A99A
		internal string Lang
		{
			get
			{
				return this._AtomLang;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x0012C7A2 File Offset: 0x0012A9A2
		internal string Space
		{
			get
			{
				return this._AtomSpace;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x0012C7AA File Offset: 0x0012A9AA
		internal string Xmlns
		{
			get
			{
				return this._AtomXmlns;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x0012C7B2 File Offset: 0x0012A9B2
		internal string Xml
		{
			get
			{
				return this._AtomXml;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x0012C7BA File Offset: 0x0012A9BA
		internal string XmlNamespace
		{
			get
			{
				return this._AtomXmlNamespace;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600354B RID: 13643 RVA: 0x0012C7C2 File Offset: 0x0012A9C2
		internal string XmlnsNamespace
		{
			get
			{
				return this._AtomXmlnsNamespace;
			}
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void CheckKeyword(string keyword)
		{
		}

		// Token: 0x04002206 RID: 8710
		private string _AtomEmpty;

		// Token: 0x04002207 RID: 8711
		private string _AtomLang;

		// Token: 0x04002208 RID: 8712
		private string _AtomSpace;

		// Token: 0x04002209 RID: 8713
		private string _AtomXmlns;

		// Token: 0x0400220A RID: 8714
		private string _AtomXml;

		// Token: 0x0400220B RID: 8715
		private string _AtomXmlNamespace;

		// Token: 0x0400220C RID: 8716
		private string _AtomXmlnsNamespace;
	}
}
