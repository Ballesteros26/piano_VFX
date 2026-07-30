using System;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Represents the document type declaration.</summary>
	// Token: 0x0200021F RID: 543
	public class XmlDocumentType : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocumentType" /> class.</summary>
		/// <param name="name">The qualified name; see the <see cref="P:System.Xml.XmlDocumentType.Name" /> property.</param>
		/// <param name="publicId">The public identifier; see the <see cref="P:System.Xml.XmlDocumentType.PublicId" /> property.</param>
		/// <param name="systemId">The system identifier; see the <see cref="P:System.Xml.XmlDocumentType.SystemId" /> property.</param>
		/// <param name="internalSubset">The DTD internal subset; see the <see cref="P:System.Xml.XmlDocumentType.InternalSubset" /> property.</param>
		/// <param name="doc">The parent document.</param>
		// Token: 0x06001455 RID: 5205 RVA: 0x00074EF8 File Offset: 0x000730F8
		protected internal XmlDocumentType(string name, string publicId, string systemId, string internalSubset, XmlDocument doc)
			: base(doc)
		{
			this.name = name;
			this.publicId = publicId;
			this.systemId = systemId;
			this.namespaces = true;
			this.internalSubset = internalSubset;
			if (!doc.IsLoading)
			{
				doc.IsLoading = true;
				new XmlLoader().ParseDocumentType(this);
				doc.IsLoading = false;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For DocumentType nodes, this property returns the name of the document type.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00074F55 File Offset: 0x00073155
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For DocumentType nodes, this property returns the name of the document type.</returns>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00074F55 File Offset: 0x00073155
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For DocumentType nodes, this value is XmlNodeType.DocumentType.</returns>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00074F5D File Offset: 0x0007315D
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For document type nodes, the cloned node always includes the subtree, regardless of the parameter setting. </param>
		// Token: 0x06001459 RID: 5209 RVA: 0x00074F61 File Offset: 0x00073161
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateDocumentType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because DocumentType nodes are read-only, this property always returns true.</returns>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00003242 File Offset: 0x00001442
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.XmlEntity" /> nodes declared in the document type declaration.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNamedNodeMap" /> containing the XmlEntity nodes. The returned XmlNamedNodeMap is read-only.</returns>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00074F86 File Offset: 0x00073186
		public XmlNamedNodeMap Entities
		{
			get
			{
				if (this.entities == null)
				{
					this.entities = new XmlNamedNodeMap(this);
				}
				return this.entities;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.XmlNotation" /> nodes present in the document type declaration.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNamedNodeMap" /> containing the XmlNotation nodes. The returned XmlNamedNodeMap is read-only.</returns>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00074FA2 File Offset: 0x000731A2
		public XmlNamedNodeMap Notations
		{
			get
			{
				if (this.notations == null)
				{
					this.notations = new XmlNamedNodeMap(this);
				}
				return this.notations;
			}
		}

		/// <summary>Gets the value of the public identifier on the DOCTYPE declaration.</summary>
		/// <returns>The public identifier on the DOCTYPE. If there is no public identifier, null is returned.</returns>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00074FBE File Offset: 0x000731BE
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		/// <summary>Gets the value of the system identifier on the DOCTYPE declaration.</summary>
		/// <returns>The system identifier on the DOCTYPE. If there is no system identifier, null is returned.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00074FC6 File Offset: 0x000731C6
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		/// <summary>Gets the value of the document type definition (DTD) internal subset on the DOCTYPE declaration.</summary>
		/// <returns>The DTD internal subset on the DOCTYPE. If there is no DTD internal subset, String.Empty is returned.</returns>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00074FCE File Offset: 0x000731CE
		public string InternalSubset
		{
			get
			{
				return this.internalSubset;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00074FD6 File Offset: 0x000731D6
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x00074FDE File Offset: 0x000731DE
		internal bool ParseWithNamespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001462 RID: 5218 RVA: 0x00074FE7 File Offset: 0x000731E7
		public override void WriteTo(XmlWriter w)
		{
			w.WriteDocType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. For XmlDocumentType nodes, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001463 RID: 5219 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00075007 File Offset: 0x00073207
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x0007500F File Offset: 0x0007320F
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

		// Token: 0x04000DB9 RID: 3513
		private string name;

		// Token: 0x04000DBA RID: 3514
		private string publicId;

		// Token: 0x04000DBB RID: 3515
		private string systemId;

		// Token: 0x04000DBC RID: 3516
		private string internalSubset;

		// Token: 0x04000DBD RID: 3517
		private bool namespaces;

		// Token: 0x04000DBE RID: 3518
		private XmlNamedNodeMap entities;

		// Token: 0x04000DBF RID: 3519
		private XmlNamedNodeMap notations;

		// Token: 0x04000DC0 RID: 3520
		private SchemaInfo schemaInfo;
	}
}
