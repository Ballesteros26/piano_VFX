using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents an attribute. Valid and default values for the attribute are defined in a document type definition (DTD) or schema.</summary>
	// Token: 0x02000215 RID: 533
	public class XmlAttribute : XmlNode
	{
		// Token: 0x06001337 RID: 4919 RVA: 0x00071B94 File Offset: 0x0006FD94
		internal XmlAttribute(XmlName name, XmlDocument doc)
			: base(doc)
		{
			this.parentNode = null;
			if (!doc.IsLoading)
			{
				XmlDocument.CheckName(name.Prefix);
				XmlDocument.CheckName(name.LocalName);
			}
			if (name.LocalName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The attribute local name cannot be empty."));
			}
			this.name = name;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00071BF1 File Offset: 0x0006FDF1
		internal int LocalNameHash
		{
			get
			{
				return this.name.HashCode;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlAttribute" /> class.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace uniform resource identifier (URI).</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06001339 RID: 4921 RVA: 0x00071BFE File Offset: 0x0006FDFE
		protected internal XmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc)
			: this(doc.AddAttrXmlName(prefix, localName, namespaceURI, null), doc)
		{
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00071C13 File Offset: 0x0006FE13
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x00071C1B File Offset: 0x0006FE1B
		internal XmlName XmlName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The duplicate node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself </param>
		// Token: 0x0600133C RID: 4924 RVA: 0x00071C24 File Offset: 0x0006FE24
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlAttribute xmlAttribute = ownerDocument.CreateAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlAttribute.CopyChildren(ownerDocument, this, true);
			return xmlAttribute;
		}

		/// <summary>Gets the parent of this node. For XmlAttribute nodes, this property always returns null.</summary>
		/// <returns>For XmlAttribute nodes, this property always returns null.</returns>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0000365F File Offset: 0x0000185F
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>The qualified name of the attribute node.</returns>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00071C59 File Offset: 0x0006FE59
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>The name of the attribute node with the prefix removed. In the following example &lt;book bk:genre= 'novel'&gt;, the LocalName of the attribute is genre.</returns>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00071C66 File Offset: 0x0006FE66
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		/// <summary>Gets the namespace URI of this node.</summary>
		/// <returns>The namespace URI of this node. If the attribute is not explicitly given a namespace, this property returns String.Empty.</returns>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00071C73 File Offset: 0x0006FE73
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		/// <summary>Gets or sets the namespace prefix of this node.</summary>
		/// <returns>The namespace prefix of this node. If there is no prefix, this property returns String.Empty.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only.</exception>
		/// <exception cref="T:System.Xml.XmlException">The specified prefix contains an invalid character.The specified prefix is malformed.The namespaceURI of this node is null.The specified prefix is "xml", and the namespaceURI of this node is different from "http://www.w3.org/XML/1998/namespace".This node is an attribute, the specified prefix is "xmlns", and the namespaceURI of this node is different from "http://www.w3.org/2000/xmlns/".This node is an attribute, and the qualifiedName of this node is "xmlns" [Namespaces].</exception>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00071C80 File Offset: 0x0006FE80
		// (set) Token: 0x06001342 RID: 4930 RVA: 0x00071C8D File Offset: 0x0006FE8D
		public override string Prefix
		{
			get
			{
				return this.name.Prefix;
			}
			set
			{
				this.name = this.name.OwnerDocument.AddAttrXmlName(value, this.LocalName, this.NamespaceURI, this.SchemaInfo);
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type for XmlAttribute nodes is XmlNodeType.Attribute.</returns>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x000026AE File Offset: 0x000008AE
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Attribute;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>An XML document to which this node belongs.</returns>
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00071CB8 File Offset: 0x0006FEB8
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node. For XmlAttribute nodes, this property is the value of attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The node is read-only and a set operation is called.</exception>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00071CC5 File Offset: 0x0006FEC5
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x00071CCD File Offset: 0x0006FECD
		public override string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		/// <summary>Gets the post-schema-validation-infoset that has been assigned to this node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> containing the post-schema-validation-infoset of this node.</returns>
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00071C13 File Offset: 0x0006FE13
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children. For attribute nodes, this property has the same functionality as the <see cref="P:System.Xml.XmlAttribute.Value" /> property.</returns>
		// Token: 0x1700035A RID: 858
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x00071CD8 File Offset: 0x0006FED8
		public override string InnerText
		{
			set
			{
				if (this.PrepareOwnerElementInElementIdAttrMap())
				{
					string innerText = base.InnerText;
					base.InnerText = value;
					this.ResetOwnerElementInElementIdAttrMap(innerText);
					return;
				}
				base.InnerText = value;
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00071D0C File Offset: 0x0006FF0C
		internal bool PrepareOwnerElementInElementIdAttrMap()
		{
			if (this.OwnerDocument.DtdSchemaInfo != null)
			{
				XmlElement ownerElement = this.OwnerElement;
				if (ownerElement != null)
				{
					return ownerElement.Attributes.PrepareParentInElementIdAttrMap(this.Prefix, this.LocalName);
				}
			}
			return false;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00071D4C File Offset: 0x0006FF4C
		internal void ResetOwnerElementInElementIdAttrMap(string oldInnerText)
		{
			XmlElement ownerElement = this.OwnerElement;
			if (ownerElement != null)
			{
				ownerElement.Attributes.ResetParentInElementIdAttrMap(oldInnerText, this.InnerText);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00071D78 File Offset: 0x0006FF78
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.lastChild = xmlLinkedNode;
				xmlLinkedNode.SetParentForLoad(this);
			}
			else
			{
				XmlLinkedNode xmlLinkedNode2 = this.lastChild;
				xmlLinkedNode.next = xmlLinkedNode2.next;
				xmlLinkedNode2.next = xmlLinkedNode;
				this.lastChild = xmlLinkedNode;
				if (xmlLinkedNode2.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
				}
				else
				{
					xmlLinkedNode.SetParentForLoad(this);
				}
			}
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return xmlLinkedNode;
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00071E0A File Offset: 0x0007000A
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x00071E12 File Offset: 0x00070012
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

		// Token: 0x0600134F RID: 4943 RVA: 0x00071E1B File Offset: 0x0007001B
		internal override bool IsValidChildType(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.EntityReference;
		}

		/// <summary>Gets a value indicating whether the attribute value was explicitly set.</summary>
		/// <returns>true if this attribute was explicitly given a value in the original instance document; otherwise, false. A value of false indicates that the value of the attribute came from the DTD.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x00003242 File Offset: 0x00001442
		public virtual bool Specified
		{
			get
			{
				return true;
			}
		}

		/// <summary>Inserts the specified node immediately before the specified reference node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> inserted.</returns>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to insert.</param>
		/// <param name="refChild">The <see cref="T:System.Xml.XmlNode" /> that is the reference node. The <paramref name="newChild" /> is placed before this node.</param>
		/// <exception cref="T:System.InvalidOperationException">The current node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only.</exception>
		// Token: 0x06001351 RID: 4945 RVA: 0x00071E28 File Offset: 0x00070028
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.InsertBefore(newChild, refChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.InsertBefore(newChild, refChild);
			}
			return xmlNode;
		}

		/// <summary>Inserts the specified node immediately after the specified reference node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> inserted.</returns>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to insert.</param>
		/// <param name="refChild">The <see cref="T:System.Xml.XmlNode" /> that is the reference node. The <paramref name="newChild" /> is placed after the <paramref name="refChild" />.</param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only.</exception>
		// Token: 0x06001352 RID: 4946 RVA: 0x00071E60 File Offset: 0x00070060
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.InsertAfter(newChild, refChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.InsertAfter(newChild, refChild);
			}
			return xmlNode;
		}

		/// <summary>Replaces the child node specified with the new child node specified.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> replaced.</returns>
		/// <param name="newChild">The new child <see cref="T:System.Xml.XmlNode" />.</param>
		/// <param name="oldChild">The <see cref="T:System.Xml.XmlNode" /> to replace.</param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only.The <paramref name="oldChild" /> is not a child of this node.</exception>
		// Token: 0x06001353 RID: 4947 RVA: 0x00071E98 File Offset: 0x00070098
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.ReplaceChild(newChild, oldChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.ReplaceChild(newChild, oldChild);
			}
			return xmlNode;
		}

		/// <summary>Removes the specified child node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> removed.</returns>
		/// <param name="oldChild">The <see cref="T:System.Xml.XmlNode" /> to remove.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="oldChild" /> is not a child of this node. Or this node is read-only.</exception>
		// Token: 0x06001354 RID: 4948 RVA: 0x00071ED0 File Offset: 0x000700D0
		public override XmlNode RemoveChild(XmlNode oldChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.RemoveChild(oldChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.RemoveChild(oldChild);
			}
			return xmlNode;
		}

		/// <summary>Adds the specified node to the beginning of the list of child nodes for this node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> added.</returns>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to add. If it is an <see cref="T:System.Xml.XmlDocumentFragment" />, the entire contents of the document fragment are moved into the child list of this node.</param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only.</exception>
		// Token: 0x06001355 RID: 4949 RVA: 0x00071F08 File Offset: 0x00070108
		public override XmlNode PrependChild(XmlNode newChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.PrependChild(newChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.PrependChild(newChild);
			}
			return xmlNode;
		}

		/// <summary>Adds the specified node to the end of the list of child nodes, of this node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> added.</returns>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to add.</param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only.</exception>
		// Token: 0x06001356 RID: 4950 RVA: 0x00071F40 File Offset: 0x00070140
		public override XmlNode AppendChild(XmlNode newChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.AppendChild(newChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.AppendChild(newChild);
			}
			return xmlNode;
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlElement" /> to which the attribute belongs.</summary>
		/// <returns>The XmlElement that the attribute belongs to or null if this attribute is not part of an XmlElement.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x00071F76 File Offset: 0x00070176
		public virtual XmlElement OwnerElement
		{
			get
			{
				return this.parentNode as XmlElement;
			}
		}

		/// <summary>Sets the value of the attribute.</summary>
		/// <returns>The attribute value.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed.</exception>
		// Token: 0x1700035F RID: 863
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x00071F83 File Offset: 0x00070183
		public override string InnerXml
		{
			set
			{
				this.RemoveAll();
				new XmlLoader().LoadInnerXmlAttribute(this, value);
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save.</param>
		// Token: 0x06001359 RID: 4953 RVA: 0x00071F97 File Offset: 0x00070197
		public override void WriteTo(XmlWriter w)
		{
			w.WriteStartAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			this.WriteContentTo(w);
			w.WriteEndAttribute();
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save.</param>
		// Token: 0x0600135A RID: 4954 RVA: 0x00071FC0 File Offset: 0x000701C0
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		/// <summary>Gets the base Uniform Resource Identifier (URI) of the node.</summary>
		/// <returns>The location from which the node was loaded or String.Empty if the node has no base URI. Attribute nodes have the same base URI as their owner element. If an attribute node does not have an owner element, BaseURI returns String.Empty.</returns>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x00071FE7 File Offset: 0x000701E7
		public override string BaseURI
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.BaseURI;
				}
				return string.Empty;
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00072002 File Offset: 0x00070202
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0007200B File Offset: 0x0007020B
		internal override XmlSpace XmlSpace
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.XmlSpace;
				}
				return XmlSpace.None;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00072022 File Offset: 0x00070222
		internal override string XmlLang
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.XmlLang;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0007203D File Offset: 0x0007023D
		internal override XPathNodeType XPNodeType
		{
			get
			{
				if (this.IsNamespace)
				{
					return XPathNodeType.Namespace;
				}
				return XPathNodeType.Attribute;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0007204A File Offset: 0x0007024A
		internal override string XPLocalName
		{
			get
			{
				if (this.name.Prefix.Length == 0 && this.name.LocalName == "xmlns")
				{
					return string.Empty;
				}
				return this.name.LocalName;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x00072086 File Offset: 0x00070286
		internal bool IsNamespace
		{
			get
			{
				return Ref.Equal(this.name.NamespaceURI, this.name.OwnerDocument.strReservedXmlns);
			}
		}

		// Token: 0x04000D7F RID: 3455
		private XmlName name;

		// Token: 0x04000D80 RID: 3456
		private XmlLinkedNode lastChild;
	}
}
