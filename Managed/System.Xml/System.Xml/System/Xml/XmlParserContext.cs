using System;
using System.Text;

namespace System.Xml
{
	/// <summary>Provides all the context information required by the <see cref="T:System.Xml.XmlReader" /> to parse an XML fragment.</summary>
	// Token: 0x020000F9 RID: 249
	public class XmlParserContext
	{
		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, xml:lang, and xml:space values.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information about atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x060008C7 RID: 2247 RVA: 0x0002903C File Offset: 0x0002723C
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string xmlLang, XmlSpace xmlSpace)
			: this(nt, nsMgr, null, null, null, null, string.Empty, xmlLang, xmlSpace)
		{
		}

		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, xml:lang, xml:space, and encoding.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information on atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <param name="enc">An <see cref="T:System.Text.Encoding" /> object indicating the encoding setting. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x060008C8 RID: 2248 RVA: 0x00029060 File Offset: 0x00027260
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string xmlLang, XmlSpace xmlSpace, Encoding enc)
			: this(nt, nsMgr, null, null, null, null, string.Empty, xmlLang, xmlSpace, enc)
		{
		}

		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, base URI, xml:lang, xml:space, and document type values.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information about atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="docTypeName">The name of the document type declaration. </param>
		/// <param name="pubId">The public identifier. </param>
		/// <param name="sysId">The system identifier. </param>
		/// <param name="internalSubset">The internal DTD subset. The DTD subset is used for entity resolution, not for document validation.</param>
		/// <param name="baseURI">The base URI for the XML fragment (the location from which the fragment was loaded). </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x060008C9 RID: 2249 RVA: 0x00029084 File Offset: 0x00027284
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace)
			: this(nt, nsMgr, docTypeName, pubId, sysId, internalSubset, baseURI, xmlLang, xmlSpace, null)
		{
		}

		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, base URI, xml:lang, xml:space, encoding, and document type values.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information about atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="docTypeName">The name of the document type declaration. </param>
		/// <param name="pubId">The public identifier. </param>
		/// <param name="sysId">The system identifier. </param>
		/// <param name="internalSubset">The internal DTD subset. The DTD is used for entity resolution, not for document validation.</param>
		/// <param name="baseURI">The base URI for the XML fragment (the location from which the fragment was loaded). </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <param name="enc">An <see cref="T:System.Text.Encoding" /> object indicating the encoding setting. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x060008CA RID: 2250 RVA: 0x000290A8 File Offset: 0x000272A8
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace, Encoding enc)
		{
			if (nsMgr != null)
			{
				if (nt == null)
				{
					this._nt = nsMgr.NameTable;
				}
				else
				{
					if (nt != nsMgr.NameTable)
					{
						throw new XmlException("Not the same name table.", string.Empty);
					}
					this._nt = nt;
				}
			}
			else
			{
				this._nt = nt;
			}
			this._nsMgr = nsMgr;
			this._docTypeName = ((docTypeName == null) ? string.Empty : docTypeName);
			this._pubId = ((pubId == null) ? string.Empty : pubId);
			this._sysId = ((sysId == null) ? string.Empty : sysId);
			this._internalSubset = ((internalSubset == null) ? string.Empty : internalSubset);
			this._baseURI = ((baseURI == null) ? string.Empty : baseURI);
			this._xmlLang = ((xmlLang == null) ? string.Empty : xmlLang);
			this._xmlSpace = xmlSpace;
			this._encoding = enc;
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> used to atomize strings. For more information on atomized strings, see <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <returns>The XmlNameTable.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x000291C1 File Offset: 0x000273C1
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x000291C9 File Offset: 0x000273C9
		public XmlNameTable NameTable
		{
			get
			{
				return this._nt;
			}
			set
			{
				this._nt = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlNamespaceManager" />.</summary>
		/// <returns>The XmlNamespaceManager.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x000291D2 File Offset: 0x000273D2
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x000291DA File Offset: 0x000273DA
		public XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this._nsMgr;
			}
			set
			{
				this._nsMgr = value;
			}
		}

		/// <summary>Gets or sets the name of the document type declaration.</summary>
		/// <returns>The name of the document type declaration.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x000291E3 File Offset: 0x000273E3
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x000291EB File Offset: 0x000273EB
		public string DocTypeName
		{
			get
			{
				return this._docTypeName;
			}
			set
			{
				this._docTypeName = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the public identifier.</summary>
		/// <returns>The public identifier.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x000291FE File Offset: 0x000273FE
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00029206 File Offset: 0x00027406
		public string PublicId
		{
			get
			{
				return this._pubId;
			}
			set
			{
				this._pubId = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the system identifier.</summary>
		/// <returns>The system identifier.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00029219 File Offset: 0x00027419
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x00029221 File Offset: 0x00027421
		public string SystemId
		{
			get
			{
				return this._sysId;
			}
			set
			{
				this._sysId = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the base URI.</summary>
		/// <returns>The base URI to use to resolve the DTD file.</returns>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00029234 File Offset: 0x00027434
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0002923C File Offset: 0x0002743C
		public string BaseURI
		{
			get
			{
				return this._baseURI;
			}
			set
			{
				this._baseURI = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the internal DTD subset.</summary>
		/// <returns>The internal DTD subset. For example, this property returns everything between the square brackets &lt;!DOCTYPE doc [...]&gt;.</returns>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0002924F File Offset: 0x0002744F
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x00029257 File Offset: 0x00027457
		public string InternalSubset
		{
			get
			{
				return this._internalSubset;
			}
			set
			{
				this._internalSubset = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope. If there is no xml:lang in scope, String.Empty is returned.</returns>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002926A File Offset: 0x0002746A
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x00029272 File Offset: 0x00027472
		public string XmlLang
		{
			get
			{
				return this._xmlLang;
			}
			set
			{
				this._xmlLang = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the current xml:space scope.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope.</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00029285 File Offset: 0x00027485
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0002928D File Offset: 0x0002748D
		public XmlSpace XmlSpace
		{
			get
			{
				return this._xmlSpace;
			}
			set
			{
				this._xmlSpace = value;
			}
		}

		/// <summary>Gets or sets the encoding type.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object indicating the encoding type.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00029296 File Offset: 0x00027496
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x0002929E File Offset: 0x0002749E
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x000292A7 File Offset: 0x000274A7
		internal bool HasDtdInfo
		{
			get
			{
				return this._internalSubset != string.Empty || this._pubId != string.Empty || this._sysId != string.Empty;
			}
		}

		// Token: 0x04000558 RID: 1368
		private XmlNameTable _nt;

		// Token: 0x04000559 RID: 1369
		private XmlNamespaceManager _nsMgr;

		// Token: 0x0400055A RID: 1370
		private string _docTypeName = string.Empty;

		// Token: 0x0400055B RID: 1371
		private string _pubId = string.Empty;

		// Token: 0x0400055C RID: 1372
		private string _sysId = string.Empty;

		// Token: 0x0400055D RID: 1373
		private string _internalSubset = string.Empty;

		// Token: 0x0400055E RID: 1374
		private string _xmlLang = string.Empty;

		// Token: 0x0400055F RID: 1375
		private XmlSpace _xmlSpace;

		// Token: 0x04000560 RID: 1376
		private string _baseURI = string.Empty;

		// Token: 0x04000561 RID: 1377
		private Encoding _encoding;
	}
}
