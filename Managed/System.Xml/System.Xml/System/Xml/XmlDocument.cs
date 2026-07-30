using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents an XML document.</summary>
	// Token: 0x0200021D RID: 541
	public class XmlDocument : XmlNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocument" /> class.</summary>
		// Token: 0x060013BE RID: 5054 RVA: 0x00073124 File Offset: 0x00071324
		public XmlDocument()
			: this(new XmlImplementation())
		{
		}

		/// <summary>Initializes a new instance of the XmlDocument class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nt">The XmlNameTable to use. </param>
		// Token: 0x060013BF RID: 5055 RVA: 0x00073131 File Offset: 0x00071331
		public XmlDocument(XmlNameTable nt)
			: this(new XmlImplementation(nt))
		{
		}

		/// <summary>Initializes a new instance of the XmlDocument class with the specified <see cref="T:System.Xml.XmlImplementation" />.</summary>
		/// <param name="imp">The XmlImplementation to use. </param>
		// Token: 0x060013C0 RID: 5056 RVA: 0x00073140 File Offset: 0x00071340
		protected internal XmlDocument(XmlImplementation imp)
		{
			this.implementation = imp;
			this.domNameTable = new DomNameTable(this);
			XmlNameTable nameTable = this.NameTable;
			nameTable.Add(string.Empty);
			this.strDocumentName = nameTable.Add("#document");
			this.strDocumentFragmentName = nameTable.Add("#document-fragment");
			this.strCommentName = nameTable.Add("#comment");
			this.strTextName = nameTable.Add("#text");
			this.strCDataSectionName = nameTable.Add("#cdata-section");
			this.strEntityName = nameTable.Add("#entity");
			this.strID = nameTable.Add("id");
			this.strNonSignificantWhitespaceName = nameTable.Add("#whitespace");
			this.strSignificantWhitespaceName = nameTable.Add("#significant-whitespace");
			this.strXmlns = nameTable.Add("xmlns");
			this.strXml = nameTable.Add("xml");
			this.strSpace = nameTable.Add("space");
			this.strLang = nameTable.Add("lang");
			this.strReservedXmlns = nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.strReservedXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.strEmpty = nameTable.Add(string.Empty);
			this.baseURI = string.Empty;
			this.objLock = new object();
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0007329F File Offset: 0x0007149F
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x000732A7 File Offset: 0x000714A7
		internal SchemaInfo DtdSchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000732B0 File Offset: 0x000714B0
		internal static void CheckName(string name)
		{
			int num = ValidateNames.ParseNmtoken(name, 0);
			if (num < name.Length)
			{
				throw new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num));
			}
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x000732E0 File Offset: 0x000714E0
		internal XmlName AddXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			return this.domNameTable.AddName(prefix, localName, namespaceURI, schemaInfo);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x000732F2 File Offset: 0x000714F2
		internal XmlName GetXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			return this.domNameTable.GetName(prefix, localName, namespaceURI, schemaInfo);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00073304 File Offset: 0x00071504
		internal XmlName AddAttrXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			XmlName xmlName = this.AddXmlName(prefix, localName, namespaceURI, schemaInfo);
			if (!this.IsLoading)
			{
				object prefix2 = xmlName.Prefix;
				object namespaceURI2 = xmlName.NamespaceURI;
				object localName2 = xmlName.LocalName;
				if ((prefix2 == this.strXmlns || (prefix2 == this.strEmpty && localName2 == this.strXmlns)) ^ (namespaceURI2 == this.strReservedXmlns))
				{
					throw new ArgumentException(Res.GetString("The namespace declaration attribute has an incorrect 'namespaceURI': '{0}'.", new object[] { namespaceURI }));
				}
			}
			return xmlName;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00073382 File Offset: 0x00071582
		internal bool AddIdInfo(XmlName eleName, XmlName attrName)
		{
			if (this.htElementIDAttrDecl == null || this.htElementIDAttrDecl[eleName] == null)
			{
				if (this.htElementIDAttrDecl == null)
				{
					this.htElementIDAttrDecl = new Hashtable();
				}
				this.htElementIDAttrDecl.Add(eleName, attrName);
				return true;
			}
			return false;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000733C0 File Offset: 0x000715C0
		private XmlName GetIDInfoByElement_(XmlName eleName)
		{
			XmlName xmlName = this.GetXmlName(eleName.Prefix, eleName.LocalName, string.Empty, null);
			if (xmlName != null)
			{
				return (XmlName)this.htElementIDAttrDecl[xmlName];
			}
			return null;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000733FC File Offset: 0x000715FC
		internal XmlName GetIDInfoByElement(XmlName eleName)
		{
			if (this.htElementIDAttrDecl == null)
			{
				return null;
			}
			return this.GetIDInfoByElement_(eleName);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00073410 File Offset: 0x00071610
		private WeakReference GetElement(ArrayList elementList, XmlElement elem)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in elementList)
			{
				WeakReference weakReference = (WeakReference)obj;
				if (!weakReference.IsAlive)
				{
					arrayList.Add(weakReference);
				}
				else if ((XmlElement)weakReference.Target == elem)
				{
					return weakReference;
				}
			}
			foreach (object obj2 in arrayList)
			{
				WeakReference weakReference2 = (WeakReference)obj2;
				elementList.Remove(weakReference2);
			}
			return null;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x000734D8 File Offset: 0x000716D8
		internal void AddElementWithId(string id, XmlElement elem)
		{
			if (this.htElementIdMap == null || !this.htElementIdMap.Contains(id))
			{
				if (this.htElementIdMap == null)
				{
					this.htElementIdMap = new Hashtable();
				}
				ArrayList arrayList = new ArrayList();
				arrayList.Add(new WeakReference(elem));
				this.htElementIdMap.Add(id, arrayList);
				return;
			}
			ArrayList arrayList2 = (ArrayList)this.htElementIdMap[id];
			if (this.GetElement(arrayList2, elem) == null)
			{
				arrayList2.Add(new WeakReference(elem));
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00073558 File Offset: 0x00071758
		internal void RemoveElementWithId(string id, XmlElement elem)
		{
			if (this.htElementIdMap != null && this.htElementIdMap.Contains(id))
			{
				ArrayList arrayList = (ArrayList)this.htElementIdMap[id];
				WeakReference element = this.GetElement(arrayList, elem);
				if (element != null)
				{
					arrayList.Remove(element);
					if (arrayList.Count == 0)
					{
						this.htElementIdMap.Remove(id);
					}
				}
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned XmlDocument node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x060013CD RID: 5069 RVA: 0x000735B4 File Offset: 0x000717B4
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument xmlDocument = this.Implementation.CreateDocument();
			xmlDocument.SetBaseURI(this.baseURI);
			if (deep)
			{
				xmlDocument.ImportChildren(this, xmlDocument, deep);
			}
			return xmlDocument;
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For XmlDocument nodes, this value is XmlNodeType.Document.</returns>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x000735E6 File Offset: 0x000717E6
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Document;
			}
		}

		/// <summary>Gets the parent node of this node (for nodes that can have parents).</summary>
		/// <returns>Always returns null.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0000365F File Offset: 0x0000185F
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the node containing the DOCTYPE declaration.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> containing the DocumentType (DOCTYPE declaration).</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x000735EA File Offset: 0x000717EA
		public virtual XmlDocumentType DocumentType
		{
			get
			{
				return (XmlDocumentType)this.FindChild(XmlNodeType.DocumentType);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x000735F9 File Offset: 0x000717F9
		internal virtual XmlDeclaration Declaration
		{
			get
			{
				if (this.HasChildNodes)
				{
					return this.FirstChild as XmlDeclaration;
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlImplementation" /> object for the current document.</summary>
		/// <returns>The XmlImplementation object for the current document.</returns>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00073610 File Offset: 0x00071810
		public XmlImplementation Implementation
		{
			get
			{
				return this.implementation;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlDocument nodes, the name is #document.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00073618 File Offset: 0x00071818
		public override string Name
		{
			get
			{
				return this.strDocumentName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlDocument nodes, the local name is #document.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00073618 File Offset: 0x00071818
		public override string LocalName
		{
			get
			{
				return this.strDocumentName;
			}
		}

		/// <summary>Gets the root <see cref="T:System.Xml.XmlElement" /> for the document.</summary>
		/// <returns>The XmlElement that represents the root of the XML document tree. If no root exists, null is returned.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00073620 File Offset: 0x00071820
		public XmlElement DocumentElement
		{
			get
			{
				return (XmlElement)this.FindChild(XmlNodeType.Element);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0007362E File Offset: 0x0007182E
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x00073636 File Offset: 0x00071836
		internal override XmlLinkedNode LastNode
		{
			get
			{
				return this.lastChild;
			}
			set
			{
				this.lastChild = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which the current node belongs.</summary>
		/// <returns>For XmlDocument nodes (<see cref="P:System.Xml.XmlDocument.NodeType" /> equals XmlNodeType.Document), this property always returns null.</returns>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0000365F File Offset: 0x0000185F
		public override XmlDocument OwnerDocument
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object associated with this <see cref="T:System.Xml.XmlDocument" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object containing the XML Schema Definition Language (XSD) schemas associated with this <see cref="T:System.Xml.XmlDocument" />; otherwise, an empty <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object.</returns>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0007363F File Offset: 0x0007183F
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x00073660 File Offset: 0x00071860
		public XmlSchemaSet Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemaSet(this.NameTable);
				}
				return this.schemas;
			}
			set
			{
				this.schemas = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00073669 File Offset: 0x00071869
		internal bool CanReportValidity
		{
			get
			{
				return this.reportValidity;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00073671 File Offset: 0x00071871
		internal bool HasSetResolver
		{
			get
			{
				return this.bSetResolver;
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00073679 File Offset: 0x00071879
		internal XmlResolver GetResolver()
		{
			return this.resolver;
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> to use for resolving external resources.</summary>
		/// <returns>The XmlResolver to use.In version 1.1 of the.NET Framework, the caller must be fully trusted in order to specify an XmlResolver.</returns>
		/// <exception cref="T:System.Xml.XmlException">This property is set to null and an external DTD or entity is encountered. </exception>
		// Token: 0x17000395 RID: 917
		// (set) Token: 0x060013DF RID: 5087 RVA: 0x00073684 File Offset: 0x00071884
		public virtual XmlResolver XmlResolver
		{
			set
			{
				if (value != null)
				{
					try
					{
						new NamedPermissionSet("FullTrust").Demand();
					}
					catch (SecurityException ex)
					{
						throw new SecurityException(Res.GetString("XmlResolver can be set only by fully trusted code."), ex);
					}
				}
				this.resolver = value;
				if (!this.bSetResolver)
				{
					this.bSetResolver = true;
				}
				XmlDocumentType documentType = this.DocumentType;
				if (documentType != null)
				{
					documentType.DtdSchemaInfo = null;
				}
			}
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x000736F0 File Offset: 0x000718F0
		internal override bool IsValidChildType(XmlNodeType type)
		{
			if (type != XmlNodeType.Element)
			{
				switch (type)
				{
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					return true;
				case XmlNodeType.DocumentType:
					if (this.DocumentType != null)
					{
						throw new InvalidOperationException(Res.GetString("This document already has a 'DocumentType' node."));
					}
					return true;
				case XmlNodeType.XmlDeclaration:
					if (this.Declaration != null)
					{
						throw new InvalidOperationException(Res.GetString("This document already has an 'XmlDeclaration' node."));
					}
					return true;
				}
				return false;
			}
			if (this.DocumentElement != null)
			{
				throw new InvalidOperationException(Res.GetString("This document already has a 'DocumentElement' node."));
			}
			return true;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00073788 File Offset: 0x00071988
		private bool HasNodeTypeInPrevSiblings(XmlNodeType nt, XmlNode refNode)
		{
			if (refNode == null)
			{
				return false;
			}
			XmlNode xmlNode = null;
			if (refNode.ParentNode != null)
			{
				xmlNode = refNode.ParentNode.FirstChild;
			}
			while (xmlNode != null)
			{
				if (xmlNode.NodeType == nt)
				{
					return true;
				}
				if (xmlNode == refNode)
				{
					break;
				}
				xmlNode = xmlNode.NextSibling;
			}
			return false;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x000737CC File Offset: 0x000719CC
		private bool HasNodeTypeInNextSiblings(XmlNodeType nt, XmlNode refNode)
		{
			for (XmlNode xmlNode = refNode; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == nt)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x000737F4 File Offset: 0x000719F4
		internal override bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			if (refChild == null)
			{
				refChild = this.FirstChild;
			}
			if (refChild == null)
			{
				return true;
			}
			XmlNodeType nodeType = newChild.NodeType;
			if (nodeType <= XmlNodeType.Comment)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType - XmlNodeType.ProcessingInstruction <= 1)
					{
						return refChild.NodeType != XmlNodeType.XmlDeclaration;
					}
				}
				else if (refChild.NodeType != XmlNodeType.XmlDeclaration)
				{
					return !this.HasNodeTypeInNextSiblings(XmlNodeType.DocumentType, refChild);
				}
			}
			else if (nodeType != XmlNodeType.DocumentType)
			{
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					return refChild == this.FirstChild;
				}
			}
			else if (refChild.NodeType != XmlNodeType.XmlDeclaration)
			{
				return !this.HasNodeTypeInPrevSiblings(XmlNodeType.Element, refChild.PreviousSibling);
			}
			return false;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00073880 File Offset: 0x00071A80
		internal override bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			if (refChild == null)
			{
				refChild = this.LastChild;
			}
			if (refChild == null)
			{
				return true;
			}
			XmlNodeType nodeType = newChild.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				switch (nodeType)
				{
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					return true;
				case XmlNodeType.DocumentType:
					return !this.HasNodeTypeInPrevSiblings(XmlNodeType.Element, refChild);
				}
				return false;
			}
			return !this.HasNodeTypeInNextSiblings(XmlNodeType.DocumentType, refChild.NextSibling);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlAttribute" /> with the specified <see cref="P:System.Xml.XmlDocument.Name" />.</summary>
		/// <returns>The new XmlAttribute.</returns>
		/// <param name="name">The qualified name of the attribute. If the name contains a colon, the <see cref="P:System.Xml.XmlNode.Prefix" /> property reflects the part of the name preceding the first colon and the <see cref="P:System.Xml.XmlDocument.LocalName" /> property reflects the part of the name following the first colon. The <see cref="P:System.Xml.XmlNode.NamespaceURI" /> remains empty unless the prefix is a recognized built-in prefix such as xmlns. In this case NamespaceURI has a value of http://www.w3.org/2000/xmlns/. </param>
		// Token: 0x060013E5 RID: 5093 RVA: 0x000738F4 File Offset: 0x00071AF4
		public XmlAttribute CreateAttribute(string name)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			XmlNode.SplitName(name, out empty, out empty2);
			this.SetDefaultNamespace(empty, empty2, ref empty3);
			return this.CreateAttribute(empty, empty2, empty3);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00073930 File Offset: 0x00071B30
		internal void SetDefaultNamespace(string prefix, string localName, ref string namespaceURI)
		{
			if (prefix == this.strXmlns || (prefix.Length == 0 && localName == this.strXmlns))
			{
				namespaceURI = this.strReservedXmlns;
				return;
			}
			if (prefix == this.strXml)
			{
				namespaceURI = this.strReservedXml;
			}
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlCDataSection" /> containing the specified data.</summary>
		/// <returns>The new XmlCDataSection.</returns>
		/// <param name="data">The content of the new XmlCDataSection. </param>
		// Token: 0x060013E7 RID: 5095 RVA: 0x00073980 File Offset: 0x00071B80
		public virtual XmlCDataSection CreateCDataSection(string data)
		{
			this.fCDataNodesPresent = true;
			return new XmlCDataSection(data, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlComment" /> containing the specified data.</summary>
		/// <returns>The new XmlComment.</returns>
		/// <param name="data">The content of the new XmlComment. </param>
		// Token: 0x060013E8 RID: 5096 RVA: 0x00073990 File Offset: 0x00071B90
		public virtual XmlComment CreateComment(string data)
		{
			return new XmlComment(data, this);
		}

		/// <summary>Returns a new <see cref="T:System.Xml.XmlDocumentType" /> object.</summary>
		/// <returns>The new XmlDocumentType.</returns>
		/// <param name="name">Name of the document type. </param>
		/// <param name="publicId">The public identifier of the document type or null. You can specify a public URI and also a system identifier to identify the location of the external DTD subset.</param>
		/// <param name="systemId">The system identifier of the document type or null. Specifies the URL of the file location for the external DTD subset.</param>
		/// <param name="internalSubset">The DTD internal subset of the document type or null. </param>
		// Token: 0x060013E9 RID: 5097 RVA: 0x00073999 File Offset: 0x00071B99
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public virtual XmlDocumentType CreateDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XmlDocumentType(name, publicId, systemId, internalSubset, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlDocumentFragment" />.</summary>
		/// <returns>The new XmlDocumentFragment.</returns>
		// Token: 0x060013EA RID: 5098 RVA: 0x000739A6 File Offset: 0x00071BA6
		public virtual XmlDocumentFragment CreateDocumentFragment()
		{
			return new XmlDocumentFragment(this);
		}

		/// <summary>Creates an element with the specified name.</summary>
		/// <returns>The new XmlElement.</returns>
		/// <param name="name">The qualified name of the element. If the name contains a colon then the <see cref="P:System.Xml.XmlNode.Prefix" /> property reflects the part of the name preceding the colon and the <see cref="P:System.Xml.XmlDocument.LocalName" /> property reflects the part of the name after the colon. The qualified name cannot include a prefix of'xmlns'. </param>
		// Token: 0x060013EB RID: 5099 RVA: 0x000739B0 File Offset: 0x00071BB0
		public XmlElement CreateElement(string name)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(name, out empty, out empty2);
			return this.CreateElement(empty, empty2, string.Empty);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x000739E0 File Offset: 0x00071BE0
		internal void AddDefaultAttributes(XmlElement elem)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			SchemaElementDecl schemaElementDecl = this.GetSchemaElementDecl(elem);
			if (schemaElementDecl != null && schemaElementDecl.AttDefs != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = schemaElementDecl.AttDefs.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)dictionaryEnumerator.Value;
					if (schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed)
					{
						string text = string.Empty;
						string name = schemaAttDef.Name.Name;
						string text2 = string.Empty;
						if (dtdSchemaInfo.SchemaType == SchemaType.DTD)
						{
							text = schemaAttDef.Name.Namespace;
						}
						else
						{
							text = schemaAttDef.Prefix;
							text2 = schemaAttDef.Name.Namespace;
						}
						XmlAttribute xmlAttribute = this.PrepareDefaultAttribute(schemaAttDef, text, name, text2);
						elem.SetAttributeNode(xmlAttribute);
					}
				}
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00073AA8 File Offset: 0x00071CA8
		private SchemaElementDecl GetSchemaElementDecl(XmlElement elem)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			if (dtdSchemaInfo != null)
			{
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(elem.LocalName, (dtdSchemaInfo.SchemaType == SchemaType.DTD) ? elem.Prefix : elem.NamespaceURI);
				SchemaElementDecl schemaElementDecl;
				if (dtdSchemaInfo.ElementDecls.TryGetValue(xmlQualifiedName, out schemaElementDecl))
				{
					return schemaElementDecl;
				}
			}
			return null;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00073AF8 File Offset: 0x00071CF8
		private XmlAttribute PrepareDefaultAttribute(SchemaAttDef attdef, string attrPrefix, string attrLocalname, string attrNamespaceURI)
		{
			this.SetDefaultNamespace(attrPrefix, attrLocalname, ref attrNamespaceURI);
			XmlAttribute xmlAttribute = this.CreateDefaultAttribute(attrPrefix, attrLocalname, attrNamespaceURI);
			xmlAttribute.InnerXml = attdef.DefaultValueRaw;
			XmlUnspecifiedAttribute xmlUnspecifiedAttribute = xmlAttribute as XmlUnspecifiedAttribute;
			if (xmlUnspecifiedAttribute != null)
			{
				xmlUnspecifiedAttribute.SetSpecified(false);
			}
			return xmlAttribute;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlEntityReference" /> with the specified name.</summary>
		/// <returns>The new XmlEntityReference.</returns>
		/// <param name="name">The name of the entity reference. </param>
		/// <exception cref="T:System.ArgumentException">The name is invalid (for example, names starting with'#' are invalid.) </exception>
		// Token: 0x060013EF RID: 5103 RVA: 0x00073B36 File Offset: 0x00071D36
		public virtual XmlEntityReference CreateEntityReference(string name)
		{
			return new XmlEntityReference(name, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlProcessingInstruction" /> with the specified name and data.</summary>
		/// <returns>The new XmlProcessingInstruction.</returns>
		/// <param name="target">The name of the processing instruction. </param>
		/// <param name="data">The data for the processing instruction. </param>
		// Token: 0x060013F0 RID: 5104 RVA: 0x00073B3F File Offset: 0x00071D3F
		public virtual XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			return new XmlProcessingInstruction(target, data, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlDeclaration" /> node with the specified values.</summary>
		/// <returns>The new XmlDeclaration node.</returns>
		/// <param name="version">The version must be "1.0". </param>
		/// <param name="encoding">The value of the encoding attribute. This is the encoding that is used when you save the <see cref="T:System.Xml.XmlDocument" /> to a file or a stream; therefore, it must be set to a string supported by the <see cref="T:System.Text.Encoding" /> class, otherwise <see cref="M:System.Xml.XmlDocument.Save(System.String)" /> fails. If this is null or String.Empty, the Save method does not write an encoding attribute on the XML declaration and therefore the default encoding, UTF-8, is used.Note: If the XmlDocument is saved to either a <see cref="T:System.IO.TextWriter" /> or an <see cref="T:System.Xml.XmlTextWriter" />, this encoding value is discarded. Instead, the encoding of the TextWriter or the XmlTextWriter is used. This ensures that the XML written out can be read back using the correct encoding. </param>
		/// <param name="standalone">The value must be either "yes" or "no". If this is null or String.Empty, the Save method does not write a standalone attribute on the XML declaration. </param>
		/// <exception cref="T:System.ArgumentException">The values of <paramref name="version" /> or <paramref name="standalone" /> are something other than the ones specified above. </exception>
		// Token: 0x060013F1 RID: 5105 RVA: 0x00073B49 File Offset: 0x00071D49
		public virtual XmlDeclaration CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XmlDeclaration(version, encoding, standalone, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlText" /> with the specified text.</summary>
		/// <returns>The new XmlText node.</returns>
		/// <param name="text">The text for the Text node. </param>
		// Token: 0x060013F2 RID: 5106 RVA: 0x00073B54 File Offset: 0x00071D54
		public virtual XmlText CreateTextNode(string text)
		{
			return new XmlText(text, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlSignificantWhitespace" /> node.</summary>
		/// <returns>A new XmlSignificantWhitespace node.</returns>
		/// <param name="text">The string must contain only the following characters &amp;#20; &amp;#10; &amp;#13; and &amp;#9; </param>
		// Token: 0x060013F3 RID: 5107 RVA: 0x00073B5D File Offset: 0x00071D5D
		public virtual XmlSignificantWhitespace CreateSignificantWhitespace(string text)
		{
			return new XmlSignificantWhitespace(text, this);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating this document.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x060013F4 RID: 5108 RVA: 0x00073B66 File Offset: 0x00071D66
		public override XPathNavigator CreateNavigator()
		{
			return this.CreateNavigator(this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating this document positioned on the <see cref="T:System.Xml.XmlNode" /> specified.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> you want the navigator initially positioned on. </param>
		// Token: 0x060013F5 RID: 5109 RVA: 0x00073B70 File Offset: 0x00071D70
		protected internal virtual XPathNavigator CreateNavigator(XmlNode node)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.SignificantWhitespace:
			{
				XmlNode xmlNode = node.ParentNode;
				if (xmlNode != null)
				{
					for (;;)
					{
						XmlNodeType xmlNodeType = xmlNode.NodeType;
						if (xmlNodeType == XmlNodeType.Attribute)
						{
							break;
						}
						if (xmlNodeType != XmlNodeType.EntityReference)
						{
							goto IL_0074;
						}
						xmlNode = xmlNode.ParentNode;
						if (xmlNode == null)
						{
							goto IL_0074;
						}
					}
					return null;
				}
				IL_0074:
				node = this.NormalizeText(node);
				break;
			}
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return null;
			case XmlNodeType.Whitespace:
			{
				XmlNode xmlNode = node.ParentNode;
				if (xmlNode != null)
				{
					for (;;)
					{
						XmlNodeType xmlNodeType = xmlNode.NodeType;
						if (xmlNodeType == XmlNodeType.Document || xmlNodeType == XmlNodeType.Attribute)
						{
							break;
						}
						if (xmlNodeType != XmlNodeType.EntityReference)
						{
							goto IL_00A9;
						}
						xmlNode = xmlNode.ParentNode;
						if (xmlNode == null)
						{
							goto IL_00A9;
						}
					}
					return null;
				}
				IL_00A9:
				node = this.NormalizeText(node);
				break;
			}
			}
			return new DocumentXPathNavigator(this, node);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00073C36 File Offset: 0x00071E36
		internal static bool IsTextNode(XmlNodeType nt)
		{
			return nt - XmlNodeType.Text <= 1 || nt - XmlNodeType.Whitespace <= 1;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00073C48 File Offset: 0x00071E48
		private XmlNode NormalizeText(XmlNode n)
		{
			XmlNode xmlNode = null;
			while (XmlDocument.IsTextNode(n.NodeType))
			{
				xmlNode = n;
				n = n.PreviousSibling;
				if (n == null)
				{
					XmlNode xmlNode2 = xmlNode;
					while (xmlNode2.ParentNode != null && xmlNode2.ParentNode.NodeType == XmlNodeType.EntityReference)
					{
						if (xmlNode2.ParentNode.PreviousSibling != null)
						{
							n = xmlNode2.ParentNode.PreviousSibling;
							break;
						}
						xmlNode2 = xmlNode2.ParentNode;
						if (xmlNode2 == null)
						{
							break;
						}
					}
				}
				if (n == null)
				{
					break;
				}
				while (n.NodeType == XmlNodeType.EntityReference)
				{
					n = n.LastChild;
				}
			}
			return xmlNode;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlWhitespace" /> node.</summary>
		/// <returns>A new XmlWhitespace node.</returns>
		/// <param name="text">The string must contain only the following characters &amp;#20; &amp;#10; &amp;#13; and &amp;#9; </param>
		// Token: 0x060013F8 RID: 5112 RVA: 0x00073CC8 File Offset: 0x00071EC8
		public virtual XmlWhitespace CreateWhitespace(string text)
		{
			return new XmlWhitespace(text, this);
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlDocument.Name" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes. If no nodes match <paramref name="name" />, the returned collection will be empty.</returns>
		/// <param name="name">The qualified name to match. It is matched against the Name property of the matching node. The special value "*" matches all tags. </param>
		// Token: 0x060013F9 RID: 5113 RVA: 0x00073CD1 File Offset: 0x00071ED1
		public virtual XmlNodeList GetElementsByTagName(string name)
		{
			return new XmlElementList(this, name);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlAttribute" /> with the specified qualified name and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>The new XmlAttribute.</returns>
		/// <param name="qualifiedName">The qualified name of the attribute. If the name contains a colon then the <see cref="P:System.Xml.XmlNode.Prefix" /> property will reflect the part of the name preceding the colon and the <see cref="P:System.Xml.XmlDocument.LocalName" /> property will reflect the part of the name after the colon. </param>
		/// <param name="namespaceURI">The namespaceURI of the attribute. If the qualified name includes a prefix of xmlns, then this parameter must be http://www.w3.org/2000/xmlns/. </param>
		// Token: 0x060013FA RID: 5114 RVA: 0x00073CDC File Offset: 0x00071EDC
		public XmlAttribute CreateAttribute(string qualifiedName, string namespaceURI)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(qualifiedName, out empty, out empty2);
			return this.CreateAttribute(empty, empty2, namespaceURI);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlElement" /> with the qualified name and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>The new XmlElement.</returns>
		/// <param name="qualifiedName">The qualified name of the element. If the name contains a colon then the <see cref="P:System.Xml.XmlNode.Prefix" /> property will reflect the part of the name preceding the colon and the <see cref="P:System.Xml.XmlDocument.LocalName" /> property will reflect the part of the name after the colon. The qualified name cannot include a prefix of'xmlns'. </param>
		/// <param name="namespaceURI">The namespace URI of the element. </param>
		// Token: 0x060013FB RID: 5115 RVA: 0x00073D08 File Offset: 0x00071F08
		public XmlElement CreateElement(string qualifiedName, string namespaceURI)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(qualifiedName, out empty, out empty2);
			return this.CreateElement(empty, empty2, namespaceURI);
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlDocument.LocalName" /> and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes. If no nodes match the specified <paramref name="localName" /> and <paramref name="namespaceURI" />, the returned collection will be empty.</returns>
		/// <param name="localName">The LocalName to match. The special value "*" matches all tags. </param>
		/// <param name="namespaceURI">NamespaceURI to match. </param>
		// Token: 0x060013FC RID: 5116 RVA: 0x00073D34 File Offset: 0x00071F34
		public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
		{
			return new XmlElementList(this, localName, namespaceURI);
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlElement" /> with the specified ID.</summary>
		/// <returns>The XmlElement with the matching ID or null if no matching element is found.</returns>
		/// <param name="elementId">The attribute ID to match. </param>
		// Token: 0x060013FD RID: 5117 RVA: 0x00073D40 File Offset: 0x00071F40
		public virtual XmlElement GetElementById(string elementId)
		{
			if (this.htElementIdMap != null)
			{
				ArrayList arrayList = (ArrayList)this.htElementIdMap[elementId];
				if (arrayList != null)
				{
					foreach (object obj in arrayList)
					{
						XmlElement xmlElement = (XmlElement)((WeakReference)obj).Target;
						if (xmlElement != null && xmlElement.IsConnected())
						{
							return xmlElement;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Imports a node from another document to the current document.</summary>
		/// <returns>The imported <see cref="T:System.Xml.XmlNode" />.</returns>
		/// <param name="node">The node being imported. </param>
		/// <param name="deep">true to perform a deep clone; otherwise, false. </param>
		/// <exception cref="T:System.InvalidOperationException">Calling this method on a node type which cannot be imported. </exception>
		// Token: 0x060013FE RID: 5118 RVA: 0x00073DCC File Offset: 0x00071FCC
		public virtual XmlNode ImportNode(XmlNode node, bool deep)
		{
			return this.ImportNodeInternal(node, deep);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00073DD8 File Offset: 0x00071FD8
		private XmlNode ImportNodeInternal(XmlNode node, bool deep)
		{
			if (node == null)
			{
				throw new InvalidOperationException(Res.GetString("Cannot import a null node."));
			}
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
			{
				XmlNode xmlNode = this.CreateElement(node.Prefix, node.LocalName, node.NamespaceURI);
				this.ImportAttributes(node, xmlNode);
				if (deep)
				{
					this.ImportChildren(node, xmlNode, deep);
					return xmlNode;
				}
				return xmlNode;
			}
			case XmlNodeType.Attribute:
			{
				XmlNode xmlNode = this.CreateAttribute(node.Prefix, node.LocalName, node.NamespaceURI);
				this.ImportChildren(node, xmlNode, true);
				return xmlNode;
			}
			case XmlNodeType.Text:
				return this.CreateTextNode(node.Value);
			case XmlNodeType.CDATA:
				return this.CreateCDataSection(node.Value);
			case XmlNodeType.EntityReference:
				return this.CreateEntityReference(node.Name);
			case XmlNodeType.ProcessingInstruction:
				return this.CreateProcessingInstruction(node.Name, node.Value);
			case XmlNodeType.Comment:
				return this.CreateComment(node.Value);
			case XmlNodeType.DocumentType:
			{
				XmlDocumentType xmlDocumentType = (XmlDocumentType)node;
				return this.CreateDocumentType(xmlDocumentType.Name, xmlDocumentType.PublicId, xmlDocumentType.SystemId, xmlDocumentType.InternalSubset);
			}
			case XmlNodeType.DocumentFragment:
			{
				XmlNode xmlNode = this.CreateDocumentFragment();
				if (deep)
				{
					this.ImportChildren(node, xmlNode, deep);
					return xmlNode;
				}
				return xmlNode;
			}
			case XmlNodeType.Whitespace:
				return this.CreateWhitespace(node.Value);
			case XmlNodeType.SignificantWhitespace:
				return this.CreateSignificantWhitespace(node.Value);
			case XmlNodeType.XmlDeclaration:
			{
				XmlDeclaration xmlDeclaration = (XmlDeclaration)node;
				return this.CreateXmlDeclaration(xmlDeclaration.Version, xmlDeclaration.Encoding, xmlDeclaration.Standalone);
			}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Res.GetString("Cannot import nodes of type '{0}'."), node.NodeType.ToString()));
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00073FBC File Offset: 0x000721BC
		private void ImportAttributes(XmlNode fromElem, XmlNode toElem)
		{
			int count = fromElem.Attributes.Count;
			for (int i = 0; i < count; i++)
			{
				if (fromElem.Attributes[i].Specified)
				{
					toElem.Attributes.SetNamedItem(this.ImportNodeInternal(fromElem.Attributes[i], true));
				}
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00074014 File Offset: 0x00072214
		private void ImportChildren(XmlNode fromNode, XmlNode toNode, bool deep)
		{
			for (XmlNode xmlNode = fromNode.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				toNode.AppendChild(this.ImportNodeInternal(xmlNode, deep));
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>An XmlNameTable enabling you to get the atomized version of a string within the document.</returns>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00074043 File Offset: 0x00072243
		public XmlNameTable NameTable
		{
			get
			{
				return this.implementation.NameTable;
			}
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlAttribute" /> with the specified <see cref="P:System.Xml.XmlNode.Prefix" />, <see cref="P:System.Xml.XmlDocument.LocalName" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>The new XmlAttribute.</returns>
		/// <param name="prefix">The prefix of the attribute (if any). String.Empty and null are equivalent. </param>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute (if any). String.Empty and null are equivalent. If <paramref name="prefix" /> is xmlns, then this parameter must be http://www.w3.org/2000/xmlns/; otherwise an exception is thrown. </param>
		// Token: 0x06001403 RID: 5123 RVA: 0x00074050 File Offset: 0x00072250
		public virtual XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
		{
			return new XmlAttribute(this.AddAttrXmlName(prefix, localName, namespaceURI, null), this);
		}

		/// <summary>Creates a default attribute with the specified prefix, local name and namespace URI.</summary>
		/// <returns>The new <see cref="T:System.Xml.XmlAttribute" />.</returns>
		/// <param name="prefix">The prefix of the attribute (if any). </param>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute (if any). </param>
		// Token: 0x06001404 RID: 5124 RVA: 0x00074062 File Offset: 0x00072262
		protected internal virtual XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return new XmlUnspecifiedAttribute(prefix, localName, namespaceURI, this);
		}

		/// <summary>Creates an element with the specified <see cref="P:System.Xml.XmlNode.Prefix" />, <see cref="P:System.Xml.XmlDocument.LocalName" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>The new <see cref="T:System.Xml.XmlElement" />.</returns>
		/// <param name="prefix">The prefix of the new element (if any). String.Empty and null are equivalent. </param>
		/// <param name="localName">The local name of the new element. </param>
		/// <param name="namespaceURI">The namespace URI of the new element (if any). String.Empty and null are equivalent. </param>
		// Token: 0x06001405 RID: 5125 RVA: 0x00074070 File Offset: 0x00072270
		public virtual XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			XmlElement xmlElement = new XmlElement(this.AddXmlName(prefix, localName, namespaceURI, null), true, this);
			if (!this.IsLoading)
			{
				this.AddDefaultAttributes(xmlElement);
			}
			return xmlElement;
		}

		/// <summary>Gets or sets a value indicating whether to preserve white space in element content.</summary>
		/// <returns>true to preserve white space; otherwise false. The default is false.</returns>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0007409F File Offset: 0x0007229F
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x000740A7 File Offset: 0x000722A7
		public bool PreserveWhitespace
		{
			get
			{
				return this.preserveWhitespace;
			}
			set
			{
				this.preserveWhitespace = value;
			}
		}

		/// <summary>Gets a value indicating whether the current node is read-only.</summary>
		/// <returns>true if the current node is read-only; otherwise false. XmlDocument nodes always return false.</returns>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x000740B0 File Offset: 0x000722B0
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x000740CC File Offset: 0x000722CC
		internal XmlNamedNodeMap Entities
		{
			get
			{
				if (this.entities == null)
				{
					this.entities = new XmlNamedNodeMap(this);
				}
				return this.entities;
			}
			set
			{
				this.entities = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x000740D5 File Offset: 0x000722D5
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x000740DD File Offset: 0x000722DD
		internal bool IsLoading
		{
			get
			{
				return this.isLoading;
			}
			set
			{
				this.isLoading = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x000740E6 File Offset: 0x000722E6
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x000740EE File Offset: 0x000722EE
		internal bool ActualLoadingStatus
		{
			get
			{
				return this.actualLoadingStatus;
			}
			set
			{
				this.actualLoadingStatus = value;
			}
		}

		/// <summary>Creates a <see cref="T:System.Xml.XmlNode" /> with the specified <see cref="T:System.Xml.XmlNodeType" />, <see cref="P:System.Xml.XmlNode.Prefix" />, <see cref="P:System.Xml.XmlDocument.Name" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>The new XmlNode.</returns>
		/// <param name="type">The XmlNodeType of the new node. </param>
		/// <param name="prefix">The prefix of the new node. </param>
		/// <param name="name">The local name of the new node. </param>
		/// <param name="namespaceURI">The namespace URI of the new node. </param>
		/// <exception cref="T:System.ArgumentException">The name was not provided and the XmlNodeType requires a name. </exception>
		// Token: 0x0600140F RID: 5135 RVA: 0x000740F8 File Offset: 0x000722F8
		public virtual XmlNode CreateNode(XmlNodeType type, string prefix, string name, string namespaceURI)
		{
			switch (type)
			{
			case XmlNodeType.Element:
				if (prefix != null)
				{
					return this.CreateElement(prefix, name, namespaceURI);
				}
				return this.CreateElement(name, namespaceURI);
			case XmlNodeType.Attribute:
				if (prefix != null)
				{
					return this.CreateAttribute(prefix, name, namespaceURI);
				}
				return this.CreateAttribute(name, namespaceURI);
			case XmlNodeType.Text:
				return this.CreateTextNode(string.Empty);
			case XmlNodeType.CDATA:
				return this.CreateCDataSection(string.Empty);
			case XmlNodeType.EntityReference:
				return this.CreateEntityReference(name);
			case XmlNodeType.ProcessingInstruction:
				return this.CreateProcessingInstruction(name, string.Empty);
			case XmlNodeType.Comment:
				return this.CreateComment(string.Empty);
			case XmlNodeType.Document:
				return new XmlDocument();
			case XmlNodeType.DocumentType:
				return this.CreateDocumentType(name, string.Empty, string.Empty, string.Empty);
			case XmlNodeType.DocumentFragment:
				return this.CreateDocumentFragment();
			case XmlNodeType.Whitespace:
				return this.CreateWhitespace(string.Empty);
			case XmlNodeType.SignificantWhitespace:
				return this.CreateSignificantWhitespace(string.Empty);
			case XmlNodeType.XmlDeclaration:
				return this.CreateXmlDeclaration("1.0", null, null);
			}
			throw new ArgumentException(Res.GetString("Cannot create node of type {0}.", new object[] { type }));
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlNode" /> with the specified node type, <see cref="P:System.Xml.XmlDocument.Name" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>The new XmlNode.</returns>
		/// <param name="nodeTypeString">String version of the <see cref="T:System.Xml.XmlNodeType" /> of the new node. This parameter must be one of the values listed in the table below. </param>
		/// <param name="name">The qualified name of the new node. If the name contains a colon, it is parsed into <see cref="P:System.Xml.XmlNode.Prefix" /> and <see cref="P:System.Xml.XmlDocument.LocalName" /> components. </param>
		/// <param name="namespaceURI">The namespace URI of the new node. </param>
		/// <exception cref="T:System.ArgumentException">The name was not provided and the XmlNodeType requires a name; or <paramref name="nodeTypeString" /> is not one of the strings listed below. </exception>
		// Token: 0x06001410 RID: 5136 RVA: 0x00074227 File Offset: 0x00072427
		public virtual XmlNode CreateNode(string nodeTypeString, string name, string namespaceURI)
		{
			return this.CreateNode(this.ConvertToNodeType(nodeTypeString), name, namespaceURI);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlNode" /> with the specified <see cref="T:System.Xml.XmlNodeType" />, <see cref="P:System.Xml.XmlDocument.Name" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>The new XmlNode.</returns>
		/// <param name="type">The XmlNodeType of the new node. </param>
		/// <param name="name">The qualified name of the new node. If the name contains a colon then it is parsed into <see cref="P:System.Xml.XmlNode.Prefix" /> and <see cref="P:System.Xml.XmlDocument.LocalName" /> components. </param>
		/// <param name="namespaceURI">The namespace URI of the new node. </param>
		/// <exception cref="T:System.ArgumentException">The name was not provided and the XmlNodeType requires a name. </exception>
		// Token: 0x06001411 RID: 5137 RVA: 0x00074238 File Offset: 0x00072438
		public virtual XmlNode CreateNode(XmlNodeType type, string name, string namespaceURI)
		{
			return this.CreateNode(type, null, name, namespaceURI);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlNode" /> object based on the information in the <see cref="T:System.Xml.XmlReader" />. The reader must be positioned on a node or attribute.</summary>
		/// <returns>The new XmlNode or null if no more nodes exist.</returns>
		/// <param name="reader">The XML source </param>
		/// <exception cref="T:System.NullReferenceException">The reader is positioned on a node type that does not translate to a valid DOM node (for example, EndElement or EndEntity). </exception>
		// Token: 0x06001412 RID: 5138 RVA: 0x00074244 File Offset: 0x00072444
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public virtual XmlNode ReadNode(XmlReader reader)
		{
			XmlNode xmlNode = null;
			try
			{
				this.IsLoading = true;
				xmlNode = new XmlLoader().ReadCurrentNode(this, reader);
			}
			finally
			{
				this.IsLoading = false;
			}
			return xmlNode;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00074284 File Offset: 0x00072484
		internal XmlNodeType ConvertToNodeType(string nodeTypeString)
		{
			if (nodeTypeString == "element")
			{
				return XmlNodeType.Element;
			}
			if (nodeTypeString == "attribute")
			{
				return XmlNodeType.Attribute;
			}
			if (nodeTypeString == "text")
			{
				return XmlNodeType.Text;
			}
			if (nodeTypeString == "cdatasection")
			{
				return XmlNodeType.CDATA;
			}
			if (nodeTypeString == "entityreference")
			{
				return XmlNodeType.EntityReference;
			}
			if (nodeTypeString == "entity")
			{
				return XmlNodeType.Entity;
			}
			if (nodeTypeString == "processinginstruction")
			{
				return XmlNodeType.ProcessingInstruction;
			}
			if (nodeTypeString == "comment")
			{
				return XmlNodeType.Comment;
			}
			if (nodeTypeString == "document")
			{
				return XmlNodeType.Document;
			}
			if (nodeTypeString == "documenttype")
			{
				return XmlNodeType.DocumentType;
			}
			if (nodeTypeString == "documentfragment")
			{
				return XmlNodeType.DocumentFragment;
			}
			if (nodeTypeString == "notation")
			{
				return XmlNodeType.Notation;
			}
			if (nodeTypeString == "significantwhitespace")
			{
				return XmlNodeType.SignificantWhitespace;
			}
			if (nodeTypeString == "whitespace")
			{
				return XmlNodeType.Whitespace;
			}
			throw new ArgumentException(Res.GetString("'{0}' does not represent any 'XmlNodeType'.", new object[] { nodeTypeString }));
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00074382 File Offset: 0x00072582
		private XmlTextReader SetupReader(XmlTextReader tr)
		{
			tr.XmlValidatingReaderCompatibilityMode = true;
			tr.EntityHandling = EntityHandling.ExpandCharEntities;
			if (this.HasSetResolver)
			{
				tr.XmlResolver = this.GetResolver();
			}
			return tr;
		}

		/// <summary>Loads the XML document from the specified URL.</summary>
		/// <param name="filename">URL for the file containing the XML document to load. The URL can be either a local file or an HTTP URL (a Web address).</param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, a <see cref="T:System.IO.FileNotFoundException" /> is raised. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="filename" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="filename" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="filename" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="filename" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="filename" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06001415 RID: 5141 RVA: 0x000743A8 File Offset: 0x000725A8
		public virtual void Load(string filename)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(filename, this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Loads the XML document from the specified stream.</summary>
		/// <param name="inStream">The stream containing the XML document to load. </param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, a <see cref="T:System.IO.FileNotFoundException" /> is raised. </exception>
		// Token: 0x06001416 RID: 5142 RVA: 0x000743E8 File Offset: 0x000725E8
		public virtual void Load(Stream inStream)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(inStream, this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Impl.Close(false);
			}
		}

		/// <summary>Loads the XML document from the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="txtReader">The TextReader used to feed the XML data into the document. </param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, the document remains empty. </exception>
		// Token: 0x06001417 RID: 5143 RVA: 0x00074430 File Offset: 0x00072630
		public virtual void Load(TextReader txtReader)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(txtReader, this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Impl.Close(false);
			}
		}

		/// <summary>Loads the XML document from the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The XmlReader used to feed the XML data into the document. </param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, the document remains empty. </exception>
		// Token: 0x06001418 RID: 5144 RVA: 0x00074478 File Offset: 0x00072678
		public virtual void Load(XmlReader reader)
		{
			try
			{
				this.IsLoading = true;
				this.actualLoadingStatus = true;
				this.RemoveAll();
				this.fEntRefNodesPresent = false;
				this.fCDataNodesPresent = false;
				this.reportValidity = true;
				new XmlLoader().Load(this, reader, this.preserveWhitespace);
			}
			finally
			{
				this.IsLoading = false;
				this.actualLoadingStatus = false;
				this.reportValidity = true;
			}
		}

		/// <summary>Loads the XML document from the specified string.</summary>
		/// <param name="xml">String containing the XML document to load. </param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, the document remains empty. </exception>
		// Token: 0x06001419 RID: 5145 RVA: 0x000744E8 File Offset: 0x000726E8
		public virtual void LoadXml(string xml)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(new StringReader(xml), this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00074530 File Offset: 0x00072730
		internal Encoding TextEncoding
		{
			get
			{
				if (this.Declaration != null)
				{
					string encoding = this.Declaration.Encoding;
					if (encoding.Length > 0)
					{
						return global::System.Text.Encoding.GetEncoding(encoding);
					}
				}
				return null;
			}
		}

		/// <summary>Gets the concatenated values of the node and all its child nodes.</summary>
		/// <returns>The concatenated values of the node and all its child nodes.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting the value on the <see cref="P:System.Xml.XmlDocument.InnerText" /> property is not allowed.</exception>
		// Token: 0x1700039D RID: 925
		// (set) Token: 0x0600141B RID: 5147 RVA: 0x00074562 File Offset: 0x00072762
		public override string InnerText
		{
			set
			{
				throw new InvalidOperationException(Res.GetString("The 'InnerText' of a 'Document' node is read-only and cannot be set."));
			}
		}

		/// <summary>Gets or sets the markup representing the children of the current node.</summary>
		/// <returns>The markup of the children of the current node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00074573 File Offset: 0x00072773
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x0007457B File Offset: 0x0007277B
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.LoadXml(value);
			}
		}

		/// <summary>Saves the XML document to the specified file.</summary>
		/// <param name="filename">The location of the file where you want to save the document. </param>
		/// <exception cref="T:System.Xml.XmlException">The operation would not result in a well formed XML document (for example, no document element or duplicate XML declarations). </exception>
		// Token: 0x0600141E RID: 5150 RVA: 0x00074584 File Offset: 0x00072784
		public virtual void Save(string filename)
		{
			if (this.DocumentElement == null)
			{
				throw new XmlException("Invalid XML document. {0}", Res.GetString("The document does not have a root element."));
			}
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(filename, this.TextEncoding);
			try
			{
				if (!this.preserveWhitespace)
				{
					xmlDOMTextWriter.Formatting = Formatting.Indented;
				}
				this.WriteTo(xmlDOMTextWriter);
				xmlDOMTextWriter.Flush();
			}
			finally
			{
				xmlDOMTextWriter.Close();
			}
		}

		/// <summary>Saves the XML document to the specified stream.</summary>
		/// <param name="outStream">The stream to which you want to save. </param>
		/// <exception cref="T:System.Xml.XmlException">The operation would not result in a well formed XML document (for example, no document element or duplicate XML declarations). </exception>
		// Token: 0x0600141F RID: 5151 RVA: 0x000745F0 File Offset: 0x000727F0
		public virtual void Save(Stream outStream)
		{
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(outStream, this.TextEncoding);
			if (!this.preserveWhitespace)
			{
				xmlDOMTextWriter.Formatting = Formatting.Indented;
			}
			this.WriteTo(xmlDOMTextWriter);
			xmlDOMTextWriter.Flush();
		}

		/// <summary>Saves the XML document to the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The TextWriter to which you want to save. </param>
		/// <exception cref="T:System.Xml.XmlException">The operation would not result in a well formed XML document (for example, no document element or duplicate XML declarations). </exception>
		// Token: 0x06001420 RID: 5152 RVA: 0x00074628 File Offset: 0x00072828
		public virtual void Save(TextWriter writer)
		{
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(writer);
			if (!this.preserveWhitespace)
			{
				xmlDOMTextWriter.Formatting = Formatting.Indented;
			}
			this.Save(xmlDOMTextWriter);
		}

		/// <summary>Saves the XML document to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		/// <exception cref="T:System.Xml.XmlException">The operation would not result in a well formed XML document (for example, no document element or duplicate XML declarations). </exception>
		// Token: 0x06001421 RID: 5153 RVA: 0x00074654 File Offset: 0x00072854
		public virtual void Save(XmlWriter w)
		{
			XmlNode xmlNode = this.FirstChild;
			if (xmlNode == null)
			{
				return;
			}
			if (w.WriteState == WriteState.Start)
			{
				if (xmlNode is XmlDeclaration)
				{
					if (this.Standalone.Length == 0)
					{
						w.WriteStartDocument();
					}
					else if (this.Standalone == "yes")
					{
						w.WriteStartDocument(true);
					}
					else if (this.Standalone == "no")
					{
						w.WriteStartDocument(false);
					}
					xmlNode = xmlNode.NextSibling;
				}
				else
				{
					w.WriteStartDocument();
				}
			}
			while (xmlNode != null)
			{
				xmlNode.WriteTo(w);
				xmlNode = xmlNode.NextSibling;
			}
			w.Flush();
		}

		/// <summary>Saves the XmlDocument node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001422 RID: 5154 RVA: 0x000746ED File Offset: 0x000728ED
		public override void WriteTo(XmlWriter w)
		{
			this.WriteContentTo(w);
		}

		/// <summary>Saves all the children of the XmlDocument node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="xw">The XmlWriter to which you want to save. </param>
		// Token: 0x06001423 RID: 5155 RVA: 0x000746F8 File Offset: 0x000728F8
		public override void WriteContentTo(XmlWriter xw)
		{
			foreach (object obj in this)
			{
				((XmlNode)obj).WriteTo(xw);
			}
		}

		/// <summary>Validates the <see cref="T:System.Xml.XmlDocument" /> against the XML Schema Definition Language (XSD) schemas contained in the <see cref="P:System.Xml.XmlDocument.Schemas" /> property.</summary>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> object that receives information about schema validation warnings and errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">A schema validation event occurred and no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> object was specified.</exception>
		// Token: 0x06001424 RID: 5156 RVA: 0x0007474C File Offset: 0x0007294C
		public void Validate(ValidationEventHandler validationEventHandler)
		{
			this.Validate(validationEventHandler, this);
		}

		/// <summary>Validates the <see cref="T:System.Xml.XmlNode" /> object specified against the XML Schema Definition Language (XSD) schemas in the <see cref="P:System.Xml.XmlDocument.Schemas" /> property.</summary>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> object that receives information about schema validation warnings and errors.</param>
		/// <param name="nodeToValidate">The <see cref="T:System.Xml.XmlNode" /> object created from an <see cref="T:System.Xml.XmlDocument" /> to validate.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlNode" /> object parameter was not created from an <see cref="T:System.Xml.XmlDocument" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlNode" /> object parameter is not an element, attribute, document fragment, or the root node.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">A schema validation event occurred and no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> object was specified.</exception>
		// Token: 0x06001425 RID: 5157 RVA: 0x00074758 File Offset: 0x00072958
		public void Validate(ValidationEventHandler validationEventHandler, XmlNode nodeToValidate)
		{
			if (this.schemas == null || this.schemas.Count == 0)
			{
				throw new InvalidOperationException(Res.GetString("The XmlSchemaSet on the document is either null or has no schemas in it. Provide schema information before calling Validate."));
			}
			if (nodeToValidate.Document != this)
			{
				throw new ArgumentException(Res.GetString("Cannot validate '{0}' because its owner document is not the current document.", new object[] { "nodeToValidate" }));
			}
			if (nodeToValidate == this)
			{
				this.reportValidity = false;
			}
			new DocumentSchemaValidator(this, this.schemas, validationEventHandler).Validate(nodeToValidate);
			if (nodeToValidate == this)
			{
				this.reportValidity = true;
			}
		}

		/// <summary>Occurs when a node belonging to this document is about to be inserted into another node.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06001426 RID: 5158 RVA: 0x000747DB File Offset: 0x000729DB
		// (remove) Token: 0x06001427 RID: 5159 RVA: 0x000747F4 File Offset: 0x000729F4
		public event XmlNodeChangedEventHandler NodeInserting
		{
			add
			{
				this.onNodeInsertingDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeInsertingDelegate, value);
			}
			remove
			{
				this.onNodeInsertingDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeInsertingDelegate, value);
			}
		}

		/// <summary>Occurs when a node belonging to this document has been inserted into another node.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001428 RID: 5160 RVA: 0x0007480D File Offset: 0x00072A0D
		// (remove) Token: 0x06001429 RID: 5161 RVA: 0x00074826 File Offset: 0x00072A26
		public event XmlNodeChangedEventHandler NodeInserted
		{
			add
			{
				this.onNodeInsertedDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeInsertedDelegate, value);
			}
			remove
			{
				this.onNodeInsertedDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeInsertedDelegate, value);
			}
		}

		/// <summary>Occurs when a node belonging to this document is about to be removed from the document.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600142A RID: 5162 RVA: 0x0007483F File Offset: 0x00072A3F
		// (remove) Token: 0x0600142B RID: 5163 RVA: 0x00074858 File Offset: 0x00072A58
		public event XmlNodeChangedEventHandler NodeRemoving
		{
			add
			{
				this.onNodeRemovingDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeRemovingDelegate, value);
			}
			remove
			{
				this.onNodeRemovingDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeRemovingDelegate, value);
			}
		}

		/// <summary>Occurs when a node belonging to this document has been removed from its parent.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600142C RID: 5164 RVA: 0x00074871 File Offset: 0x00072A71
		// (remove) Token: 0x0600142D RID: 5165 RVA: 0x0007488A File Offset: 0x00072A8A
		public event XmlNodeChangedEventHandler NodeRemoved
		{
			add
			{
				this.onNodeRemovedDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeRemovedDelegate, value);
			}
			remove
			{
				this.onNodeRemovedDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeRemovedDelegate, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Xml.XmlNode.Value" /> of a node belonging to this document is about to be changed.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600142E RID: 5166 RVA: 0x000748A3 File Offset: 0x00072AA3
		// (remove) Token: 0x0600142F RID: 5167 RVA: 0x000748BC File Offset: 0x00072ABC
		public event XmlNodeChangedEventHandler NodeChanging
		{
			add
			{
				this.onNodeChangingDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeChangingDelegate, value);
			}
			remove
			{
				this.onNodeChangingDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeChangingDelegate, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Xml.XmlNode.Value" /> of a node belonging to this document has been changed.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001430 RID: 5168 RVA: 0x000748D5 File Offset: 0x00072AD5
		// (remove) Token: 0x06001431 RID: 5169 RVA: 0x000748EE File Offset: 0x00072AEE
		public event XmlNodeChangedEventHandler NodeChanged
		{
			add
			{
				this.onNodeChangedDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeChangedDelegate, value);
			}
			remove
			{
				this.onNodeChangedDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeChangedDelegate, value);
			}
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00074908 File Offset: 0x00072B08
		internal override XmlNodeChangedEventArgs GetEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			this.reportValidity = false;
			switch (action)
			{
			case XmlNodeChangedAction.Insert:
				if (this.onNodeInsertingDelegate == null && this.onNodeInsertedDelegate == null)
				{
					return null;
				}
				break;
			case XmlNodeChangedAction.Remove:
				if (this.onNodeRemovingDelegate == null && this.onNodeRemovedDelegate == null)
				{
					return null;
				}
				break;
			case XmlNodeChangedAction.Change:
				if (this.onNodeChangingDelegate == null && this.onNodeChangedDelegate == null)
				{
					return null;
				}
				break;
			}
			return new XmlNodeChangedEventArgs(node, oldParent, newParent, oldValue, newValue, action);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00074978 File Offset: 0x00072B78
		internal XmlNodeChangedEventArgs GetInsertEventArgsForLoad(XmlNode node, XmlNode newParent)
		{
			if (this.onNodeInsertingDelegate == null && this.onNodeInsertedDelegate == null)
			{
				return null;
			}
			string value = node.Value;
			return new XmlNodeChangedEventArgs(node, null, newParent, value, value, XmlNodeChangedAction.Insert);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x000749AC File Offset: 0x00072BAC
		internal override void BeforeEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				switch (args.Action)
				{
				case XmlNodeChangedAction.Insert:
					if (this.onNodeInsertingDelegate != null)
					{
						this.onNodeInsertingDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Remove:
					if (this.onNodeRemovingDelegate != null)
					{
						this.onNodeRemovingDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Change:
					if (this.onNodeChangingDelegate != null)
					{
						this.onNodeChangingDelegate(this, args);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00074A18 File Offset: 0x00072C18
		internal override void AfterEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				switch (args.Action)
				{
				case XmlNodeChangedAction.Insert:
					if (this.onNodeInsertedDelegate != null)
					{
						this.onNodeInsertedDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Remove:
					if (this.onNodeRemovedDelegate != null)
					{
						this.onNodeRemovedDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Change:
					if (this.onNodeChangedDelegate != null)
					{
						this.onNodeChangedDelegate(this, args);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00074A84 File Offset: 0x00072C84
		internal XmlAttribute GetDefaultAttribute(XmlElement elem, string attrPrefix, string attrLocalname, string attrNamespaceURI)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			SchemaElementDecl schemaElementDecl = this.GetSchemaElementDecl(elem);
			if (schemaElementDecl != null && schemaElementDecl.AttDefs != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = schemaElementDecl.AttDefs.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)dictionaryEnumerator.Value;
					if ((schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed) && schemaAttDef.Name.Name == attrLocalname && ((dtdSchemaInfo.SchemaType == SchemaType.DTD && schemaAttDef.Name.Namespace == attrPrefix) || (dtdSchemaInfo.SchemaType != SchemaType.DTD && schemaAttDef.Name.Namespace == attrNamespaceURI)))
					{
						return this.PrepareDefaultAttribute(schemaAttDef, attrPrefix, attrLocalname, attrNamespaceURI);
					}
				}
			}
			return null;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00074B44 File Offset: 0x00072D44
		internal string Version
		{
			get
			{
				XmlDeclaration declaration = this.Declaration;
				if (declaration != null)
				{
					return declaration.Version;
				}
				return null;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00074B64 File Offset: 0x00072D64
		internal string Encoding
		{
			get
			{
				XmlDeclaration declaration = this.Declaration;
				if (declaration != null)
				{
					return declaration.Encoding;
				}
				return null;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00074B84 File Offset: 0x00072D84
		internal string Standalone
		{
			get
			{
				XmlDeclaration declaration = this.Declaration;
				if (declaration != null)
				{
					return declaration.Standalone;
				}
				return null;
			}
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00074BA4 File Offset: 0x00072DA4
		internal XmlEntity GetEntityNode(string name)
		{
			if (this.DocumentType != null)
			{
				XmlNamedNodeMap xmlNamedNodeMap = this.DocumentType.Entities;
				if (xmlNamedNodeMap != null)
				{
					return (XmlEntity)xmlNamedNodeMap.GetNamedItem(name);
				}
			}
			return null;
		}

		/// <summary>Returns the Post-Schema-Validation-Infoset (PSVI) of the node.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object representing the PSVI of the node.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00074BD8 File Offset: 0x00072DD8
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (this.reportValidity)
				{
					XmlElement documentElement = this.DocumentElement;
					if (documentElement != null)
					{
						XmlSchemaValidity validity = documentElement.SchemaInfo.Validity;
						if (validity == XmlSchemaValidity.Valid)
						{
							return XmlDocument.ValidSchemaInfo;
						}
						if (validity == XmlSchemaValidity.Invalid)
						{
							return XmlDocument.InvalidSchemaInfo;
						}
					}
				}
				return XmlDocument.NotKnownSchemaInfo;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The location from which the node was loaded.</returns>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00074C1E File Offset: 0x00072E1E
		public override string BaseURI
		{
			get
			{
				return this.baseURI;
			}
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00074C26 File Offset: 0x00072E26
		internal void SetBaseURI(string inBaseURI)
		{
			this.baseURI = inBaseURI;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00074C30 File Offset: 0x00072E30
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			if (!this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type."));
			}
			if (!this.CanInsertAfter(newChild, this.LastChild))
			{
				throw new InvalidOperationException(Res.GetString("Cannot insert the node in the specified location."));
			}
			XmlNodeChangedEventArgs insertEventArgsForLoad = this.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				this.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
			}
			else
			{
				xmlLinkedNode.next = this.lastChild.next;
				this.lastChild.next = xmlLinkedNode;
			}
			this.lastChild = xmlLinkedNode;
			xmlLinkedNode.SetParentForLoad(this);
			if (insertEventArgsForLoad != null)
			{
				this.AfterEvent(insertEventArgsForLoad);
			}
			return xmlLinkedNode;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0000226C File Offset: 0x0000046C
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00074CDB File Offset: 0x00072EDB
		internal bool HasEntityReferences
		{
			get
			{
				return this.fEntRefNodesPresent;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x00074CE4 File Offset: 0x00072EE4
		internal XmlAttribute NamespaceXml
		{
			get
			{
				if (this.namespaceXml == null)
				{
					this.namespaceXml = new XmlAttribute(this.AddAttrXmlName(this.strXmlns, this.strXml, this.strReservedXmlns, null), this);
					this.namespaceXml.Value = this.strReservedXml;
				}
				return this.namespaceXml;
			}
		}

		// Token: 0x04000D8B RID: 3467
		private XmlImplementation implementation;

		// Token: 0x04000D8C RID: 3468
		private DomNameTable domNameTable;

		// Token: 0x04000D8D RID: 3469
		private XmlLinkedNode lastChild;

		// Token: 0x04000D8E RID: 3470
		private XmlNamedNodeMap entities;

		// Token: 0x04000D8F RID: 3471
		private Hashtable htElementIdMap;

		// Token: 0x04000D90 RID: 3472
		private Hashtable htElementIDAttrDecl;

		// Token: 0x04000D91 RID: 3473
		private SchemaInfo schemaInfo;

		// Token: 0x04000D92 RID: 3474
		private XmlSchemaSet schemas;

		// Token: 0x04000D93 RID: 3475
		private bool reportValidity;

		// Token: 0x04000D94 RID: 3476
		private bool actualLoadingStatus;

		// Token: 0x04000D95 RID: 3477
		private XmlNodeChangedEventHandler onNodeInsertingDelegate;

		// Token: 0x04000D96 RID: 3478
		private XmlNodeChangedEventHandler onNodeInsertedDelegate;

		// Token: 0x04000D97 RID: 3479
		private XmlNodeChangedEventHandler onNodeRemovingDelegate;

		// Token: 0x04000D98 RID: 3480
		private XmlNodeChangedEventHandler onNodeRemovedDelegate;

		// Token: 0x04000D99 RID: 3481
		private XmlNodeChangedEventHandler onNodeChangingDelegate;

		// Token: 0x04000D9A RID: 3482
		private XmlNodeChangedEventHandler onNodeChangedDelegate;

		// Token: 0x04000D9B RID: 3483
		internal bool fEntRefNodesPresent;

		// Token: 0x04000D9C RID: 3484
		internal bool fCDataNodesPresent;

		// Token: 0x04000D9D RID: 3485
		private bool preserveWhitespace;

		// Token: 0x04000D9E RID: 3486
		private bool isLoading;

		// Token: 0x04000D9F RID: 3487
		internal string strDocumentName;

		// Token: 0x04000DA0 RID: 3488
		internal string strDocumentFragmentName;

		// Token: 0x04000DA1 RID: 3489
		internal string strCommentName;

		// Token: 0x04000DA2 RID: 3490
		internal string strTextName;

		// Token: 0x04000DA3 RID: 3491
		internal string strCDataSectionName;

		// Token: 0x04000DA4 RID: 3492
		internal string strEntityName;

		// Token: 0x04000DA5 RID: 3493
		internal string strID;

		// Token: 0x04000DA6 RID: 3494
		internal string strXmlns;

		// Token: 0x04000DA7 RID: 3495
		internal string strXml;

		// Token: 0x04000DA8 RID: 3496
		internal string strSpace;

		// Token: 0x04000DA9 RID: 3497
		internal string strLang;

		// Token: 0x04000DAA RID: 3498
		internal string strEmpty;

		// Token: 0x04000DAB RID: 3499
		internal string strNonSignificantWhitespaceName;

		// Token: 0x04000DAC RID: 3500
		internal string strSignificantWhitespaceName;

		// Token: 0x04000DAD RID: 3501
		internal string strReservedXmlns;

		// Token: 0x04000DAE RID: 3502
		internal string strReservedXml;

		// Token: 0x04000DAF RID: 3503
		internal string baseURI;

		// Token: 0x04000DB0 RID: 3504
		private XmlResolver resolver;

		// Token: 0x04000DB1 RID: 3505
		internal bool bSetResolver;

		// Token: 0x04000DB2 RID: 3506
		internal object objLock;

		// Token: 0x04000DB3 RID: 3507
		private XmlAttribute namespaceXml;

		// Token: 0x04000DB4 RID: 3508
		internal static EmptyEnumerator EmptyEnumerator = new EmptyEnumerator();

		// Token: 0x04000DB5 RID: 3509
		internal static IXmlSchemaInfo NotKnownSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.NotKnown);

		// Token: 0x04000DB6 RID: 3510
		internal static IXmlSchemaInfo ValidSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.Valid);

		// Token: 0x04000DB7 RID: 3511
		internal static IXmlSchemaInfo InvalidSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.Invalid);
	}
}
