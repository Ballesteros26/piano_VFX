using System;
using System.Collections.Generic;
using System.IO;
using MS.Internal.Xml.Cache;

namespace System.Xml.XPath
{
	/// <summary>Provides a fast, read-only, in-memory representation of an XML document by using the XPath data model.</summary>
	// Token: 0x020002AF RID: 687
	public class XPathDocument : IXPathNavigable
	{
		// Token: 0x0600192B RID: 6443 RVA: 0x0009041D File Offset: 0x0008E61D
		internal XPathDocument()
		{
			this.nameTable = new NameTable();
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00090430 File Offset: 0x0008E630
		internal XPathDocument(XmlNameTable nameTable)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			this.nameTable = nameTable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data that is contained in the specified <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that contains the XML data. </param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object passed as a parameter is null.</exception>
		// Token: 0x0600192D RID: 6445 RVA: 0x0009044D File Offset: 0x0008E64D
		public XPathDocument(XmlReader reader)
			: this(reader, XmlSpace.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data that is contained in the specified <see cref="T:System.Xml.XmlReader" /> object with the specified white space handling.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that contains the XML data.</param>
		/// <param name="space">An <see cref="T:System.Xml.XmlSpace" /> object.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlReader" /> object parameter or <see cref="T:System.Xml.XmlSpace" /> object parameter is null.</exception>
		// Token: 0x0600192E RID: 6446 RVA: 0x00090457 File Offset: 0x0008E657
		public XPathDocument(XmlReader reader, XmlSpace space)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.LoadFromReader(reader, space);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data that is contained in the specified <see cref="T:System.IO.TextReader" /> object.</summary>
		/// <param name="textReader">The <see cref="T:System.IO.TextReader" /> object that contains the XML data.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.IO.TextReader" /> object passed as a parameter is null.</exception>
		// Token: 0x0600192F RID: 6447 RVA: 0x00090478 File Offset: 0x0008E678
		public XPathDocument(TextReader textReader)
		{
			XmlTextReaderImpl xmlTextReaderImpl = this.SetupReader(new XmlTextReaderImpl(string.Empty, textReader));
			try
			{
				this.LoadFromReader(xmlTextReaderImpl, XmlSpace.Default);
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data in the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> object that contains the XML data.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.IO.Stream" /> object passed as a parameter is null.</exception>
		// Token: 0x06001930 RID: 6448 RVA: 0x000904C0 File Offset: 0x0008E6C0
		public XPathDocument(Stream stream)
		{
			XmlTextReaderImpl xmlTextReaderImpl = this.SetupReader(new XmlTextReaderImpl(string.Empty, stream));
			try
			{
				this.LoadFromReader(xmlTextReaderImpl, XmlSpace.Default);
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data in the specified file.</summary>
		/// <param name="uri">The path of the file that contains the XML data.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The file path parameter is null.</exception>
		// Token: 0x06001931 RID: 6449 RVA: 0x00090508 File Offset: 0x0008E708
		public XPathDocument(string uri)
			: this(uri, XmlSpace.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathDocument" /> class from the XML data in the file specified with the white space handling specified.</summary>
		/// <param name="uri">The path of the file that contains the XML data.</param>
		/// <param name="space">An <see cref="T:System.Xml.XmlSpace" /> object.</param>
		/// <exception cref="T:System.Xml.XmlException">An error was encountered in the XML data. The <see cref="T:System.Xml.XPath.XPathDocument" /> remains empty. </exception>
		/// <exception cref="T:System.ArgumentNullException">The file path parameter or <see cref="T:System.Xml.XmlSpace" /> object parameter is null.</exception>
		// Token: 0x06001932 RID: 6450 RVA: 0x00090514 File Offset: 0x0008E714
		public XPathDocument(string uri, XmlSpace space)
		{
			XmlTextReaderImpl xmlTextReaderImpl = this.SetupReader(new XmlTextReaderImpl(uri));
			try
			{
				this.LoadFromReader(xmlTextReaderImpl, space);
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00090558 File Offset: 0x0008E758
		internal XmlRawWriter LoadFromWriter(XPathDocument.LoadFlags flags, string baseUri)
		{
			return new XPathDocumentBuilder(this, null, baseUri, flags);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00090564 File Offset: 0x0008E764
		internal void LoadFromReader(XmlReader reader, XmlSpace space)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo == null || !xmlLineInfo.HasLineInfo())
			{
				xmlLineInfo = null;
			}
			this.hasLineInfo = xmlLineInfo != null;
			this.nameTable = reader.NameTable;
			XPathDocumentBuilder xpathDocumentBuilder = new XPathDocumentBuilder(this, xmlLineInfo, reader.BaseURI, XPathDocument.LoadFlags.None);
			try
			{
				bool flag = reader.ReadState == ReadState.Initial;
				int depth = reader.Depth;
				string text = this.nameTable.Get("http://www.w3.org/2000/xmlns/");
				if (!flag || reader.Read())
				{
					while (flag || reader.Depth >= depth)
					{
						switch (reader.NodeType)
						{
						case XmlNodeType.Element:
						{
							bool isEmptyElement = reader.IsEmptyElement;
							xpathDocumentBuilder.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI, reader.BaseURI);
							while (reader.MoveToNextAttribute())
							{
								string namespaceURI = reader.NamespaceURI;
								if (namespaceURI == text)
								{
									if (reader.Prefix.Length == 0)
									{
										xpathDocumentBuilder.WriteNamespaceDeclaration(string.Empty, reader.Value);
									}
									else
									{
										xpathDocumentBuilder.WriteNamespaceDeclaration(reader.LocalName, reader.Value);
									}
								}
								else
								{
									xpathDocumentBuilder.WriteStartAttribute(reader.Prefix, reader.LocalName, namespaceURI);
									xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.Text);
									xpathDocumentBuilder.WriteEndAttribute();
								}
							}
							if (isEmptyElement)
							{
								xpathDocumentBuilder.WriteEndElement(true);
							}
							break;
						}
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
							xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.Text);
							break;
						case XmlNodeType.EntityReference:
							reader.ResolveEntity();
							break;
						case XmlNodeType.ProcessingInstruction:
							xpathDocumentBuilder.WriteProcessingInstruction(reader.LocalName, reader.Value, reader.BaseURI);
							break;
						case XmlNodeType.Comment:
							xpathDocumentBuilder.WriteComment(reader.Value);
							break;
						case XmlNodeType.DocumentType:
						{
							IDtdInfo dtdInfo = reader.DtdInfo;
							if (dtdInfo != null)
							{
								xpathDocumentBuilder.CreateIdTables(dtdInfo);
							}
							break;
						}
						case XmlNodeType.Whitespace:
							goto IL_01C6;
						case XmlNodeType.SignificantWhitespace:
							if (reader.XmlSpace != XmlSpace.Preserve)
							{
								goto IL_01C6;
							}
							xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.SignificantWhitespace);
							break;
						case XmlNodeType.EndElement:
							xpathDocumentBuilder.WriteEndElement(false);
							break;
						}
						IL_0228:
						if (!reader.Read())
						{
							break;
						}
						continue;
						IL_01C6:
						if (space == XmlSpace.Preserve && (!flag || reader.Depth != 0))
						{
							xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.Whitespace);
							goto IL_0228;
						}
						goto IL_0228;
					}
				}
			}
			finally
			{
				xpathDocumentBuilder.Close();
			}
		}

		/// <summary>Initializes a read-only <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating through nodes in this <see cref="T:System.Xml.XPath.XPathDocument" />.</summary>
		/// <returns>A read-only <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x06001935 RID: 6453 RVA: 0x000907CC File Offset: 0x0008E9CC
		public XPathNavigator CreateNavigator()
		{
			return new XPathDocumentNavigator(this.pageRoot, this.idxRoot, null, 0);
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x000907E1 File Offset: 0x0008E9E1
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x000907E9 File Offset: 0x0008E9E9
		internal bool HasLineInfo
		{
			get
			{
				return this.hasLineInfo;
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000907F1 File Offset: 0x0008E9F1
		internal int GetCollapsedTextNode(out XPathNode[] pageText)
		{
			pageText = this.pageText;
			return this.idxText;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00090801 File Offset: 0x0008EA01
		internal void SetCollapsedTextNode(XPathNode[] pageText, int idxText)
		{
			this.pageText = pageText;
			this.idxText = idxText;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00090811 File Offset: 0x0008EA11
		internal int GetRootNode(out XPathNode[] pageRoot)
		{
			pageRoot = this.pageRoot;
			return this.idxRoot;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00090821 File Offset: 0x0008EA21
		internal void SetRootNode(XPathNode[] pageRoot, int idxRoot)
		{
			this.pageRoot = pageRoot;
			this.idxRoot = idxRoot;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00090831 File Offset: 0x0008EA31
		internal int GetXmlNamespaceNode(out XPathNode[] pageXmlNmsp)
		{
			pageXmlNmsp = this.pageXmlNmsp;
			return this.idxXmlNmsp;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00090841 File Offset: 0x0008EA41
		internal void SetXmlNamespaceNode(XPathNode[] pageXmlNmsp, int idxXmlNmsp)
		{
			this.pageXmlNmsp = pageXmlNmsp;
			this.idxXmlNmsp = idxXmlNmsp;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00090851 File Offset: 0x0008EA51
		internal void AddNamespace(XPathNode[] pageElem, int idxElem, XPathNode[] pageNmsp, int idxNmsp)
		{
			if (this.mapNmsp == null)
			{
				this.mapNmsp = new Dictionary<XPathNodeRef, XPathNodeRef>();
			}
			this.mapNmsp.Add(new XPathNodeRef(pageElem, idxElem), new XPathNodeRef(pageNmsp, idxNmsp));
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00090880 File Offset: 0x0008EA80
		internal int LookupNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			XPathNodeRef xpathNodeRef = new XPathNodeRef(pageElem, idxElem);
			if (this.mapNmsp == null || !this.mapNmsp.ContainsKey(xpathNodeRef))
			{
				pageNmsp = null;
				return 0;
			}
			xpathNodeRef = this.mapNmsp[xpathNodeRef];
			pageNmsp = xpathNodeRef.Page;
			return xpathNodeRef.Index;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000908CE File Offset: 0x0008EACE
		internal void AddIdElement(string id, XPathNode[] pageElem, int idxElem)
		{
			if (this.idValueMap == null)
			{
				this.idValueMap = new Dictionary<string, XPathNodeRef>();
			}
			if (!this.idValueMap.ContainsKey(id))
			{
				this.idValueMap.Add(id, new XPathNodeRef(pageElem, idxElem));
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00090904 File Offset: 0x0008EB04
		internal int LookupIdElement(string id, out XPathNode[] pageElem)
		{
			if (this.idValueMap == null || !this.idValueMap.ContainsKey(id))
			{
				pageElem = null;
				return 0;
			}
			XPathNodeRef xpathNodeRef = this.idValueMap[id];
			pageElem = xpathNodeRef.Page;
			return xpathNodeRef.Index;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00090949 File Offset: 0x0008EB49
		private XmlTextReaderImpl SetupReader(XmlTextReaderImpl reader)
		{
			reader.EntityHandling = EntityHandling.ExpandEntities;
			reader.XmlValidatingReaderCompatibilityMode = true;
			return reader;
		}

		// Token: 0x04001522 RID: 5410
		private XPathNode[] pageText;

		// Token: 0x04001523 RID: 5411
		private XPathNode[] pageRoot;

		// Token: 0x04001524 RID: 5412
		private XPathNode[] pageXmlNmsp;

		// Token: 0x04001525 RID: 5413
		private int idxText;

		// Token: 0x04001526 RID: 5414
		private int idxRoot;

		// Token: 0x04001527 RID: 5415
		private int idxXmlNmsp;

		// Token: 0x04001528 RID: 5416
		private XmlNameTable nameTable;

		// Token: 0x04001529 RID: 5417
		private bool hasLineInfo;

		// Token: 0x0400152A RID: 5418
		private Dictionary<XPathNodeRef, XPathNodeRef> mapNmsp;

		// Token: 0x0400152B RID: 5419
		private Dictionary<string, XPathNodeRef> idValueMap;

		// Token: 0x020002B0 RID: 688
		internal enum LoadFlags
		{
			// Token: 0x0400152D RID: 5421
			None,
			// Token: 0x0400152E RID: 5422
			AtomizeNames,
			// Token: 0x0400152F RID: 5423
			Fragment
		}
	}
}
