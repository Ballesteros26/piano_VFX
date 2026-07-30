using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents an element.</summary>
	// Token: 0x02000221 RID: 545
	public class XmlElement : XmlLinkedNode
	{
		// Token: 0x0600146B RID: 5227 RVA: 0x0007507C File Offset: 0x0007327C
		internal XmlElement(XmlName name, bool empty, XmlDocument doc)
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
				throw new ArgumentException(Res.GetString("The local name for elements or attributes cannot be null or an empty string."));
			}
			this.name = name;
			if (empty)
			{
				this.lastChild = this;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlElement" /> class.</summary>
		/// <param name="prefix">The namespace prefix; see the <see cref="P:System.Xml.XmlElement.Prefix" /> property.</param>
		/// <param name="localName">The local name; see the <see cref="P:System.Xml.XmlElement.LocalName" /> property.</param>
		/// <param name="namespaceURI">The namespace URI; see the <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x0600146C RID: 5228 RVA: 0x000750E3 File Offset: 0x000732E3
		protected internal XmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc)
			: this(doc.AddXmlName(prefix, localName, namespaceURI, null), true, doc)
		{
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x000750F9 File Offset: 0x000732F9
		// (set) Token: 0x0600146E RID: 5230 RVA: 0x00075101 File Offset: 0x00073301
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
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself (and its attributes if the node is an XmlElement). </param>
		// Token: 0x0600146F RID: 5231 RVA: 0x0007510C File Offset: 0x0007330C
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			bool isLoading = ownerDocument.IsLoading;
			ownerDocument.IsLoading = true;
			XmlElement xmlElement = ownerDocument.CreateElement(this.Prefix, this.LocalName, this.NamespaceURI);
			ownerDocument.IsLoading = isLoading;
			if (xmlElement.IsEmpty != this.IsEmpty)
			{
				xmlElement.IsEmpty = this.IsEmpty;
			}
			if (this.HasAttributes)
			{
				foreach (object obj in this.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					XmlAttribute xmlAttribute2 = (XmlAttribute)xmlAttribute.CloneNode(true);
					if (xmlAttribute is XmlUnspecifiedAttribute && !xmlAttribute.Specified)
					{
						((XmlUnspecifiedAttribute)xmlAttribute2).SetSpecified(false);
					}
					xmlElement.Attributes.InternalAppendAttribute(xmlAttribute2);
				}
			}
			if (deep)
			{
				xmlElement.CopyChildren(ownerDocument, this, deep);
			}
			return xmlElement;
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>The qualified name of the node. For XmlElement nodes, this is the tag name of the element.</returns>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00075204 File Offset: 0x00073404
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.</returns>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00075211 File Offset: 0x00073411
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		/// <summary>Gets the namespace URI of this node.</summary>
		/// <returns>The namespace URI of this node. If there is no namespace URI, this property returns String.Empty.</returns>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0007521E File Offset: 0x0007341E
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		/// <summary>Gets or sets the namespace prefix of this node.</summary>
		/// <returns>The namespace prefix of this node. If there is no prefix, this property returns String.Empty.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only </exception>
		/// <exception cref="T:System.Xml.XmlException">The specified prefix contains an invalid character.The specified prefix is malformed.The namespaceURI of this node is null.The specified prefix is "xml" and the namespaceURI of this node is different from http://www.w3.org/XML/1998/namespace. </exception>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0007522B File Offset: 0x0007342B
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x00075238 File Offset: 0x00073438
		public override string Prefix
		{
			get
			{
				return this.name.Prefix;
			}
			set
			{
				this.name = this.name.OwnerDocument.AddXmlName(value, this.LocalName, this.NamespaceURI, this.SchemaInfo);
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For XmlElement nodes, this value is XmlNodeType.Element.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x00003242 File Offset: 0x00001442
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Element;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00075263 File Offset: 0x00073463
		public override XmlNode ParentNode
		{
			get
			{
				return this.parentNode;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The XmlDocument to which this element belongs.</returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0007526B File Offset: 0x0007346B
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00075278 File Offset: 0x00073478
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null || this.lastChild == this)
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

		/// <summary>Gets or sets the tag format of the element.</summary>
		/// <returns>Returns true if the element is to be serialized in the short tag format "&lt;item/&gt;"; false for the long format "&lt;item&gt;&lt;/item&gt;".When setting this property, if set to true, the children of the element are removed and the element is serialized in the short tag format. If set to false, the value of the property is changed (regardless of whether or not the element has content); if the element is empty, it is serialized in the long format.This property is a Microsoft extension to the Document Object Model (DOM).</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00075313 File Offset: 0x00073513
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x0007531E File Offset: 0x0007351E
		public bool IsEmpty
		{
			get
			{
				return this.lastChild == this;
			}
			set
			{
				if (value)
				{
					if (this.lastChild != this)
					{
						this.RemoveAllChildren();
						this.lastChild = this;
						return;
					}
				}
				else if (this.lastChild == this)
				{
					this.lastChild = null;
				}
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0007534A File Offset: 0x0007354A
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x0007535D File Offset: 0x0007355D
		internal override XmlLinkedNode LastNode
		{
			get
			{
				if (this.lastChild != this)
				{
					return this.lastChild;
				}
				return null;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00075368 File Offset: 0x00073568
		internal override bool IsValidChildType(XmlNodeType type)
		{
			switch (type)
			{
			case XmlNodeType.Element:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.EntityReference:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return true;
			}
			return false;
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlAttributeCollection" /> containing the list of attributes for this node.</summary>
		/// <returns>
		///   <see cref="T:System.Xml.XmlAttributeCollection" /> containing the list of attributes for this node.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x000753BC File Offset: 0x000735BC
		public override XmlAttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					object objLock = this.OwnerDocument.objLock;
					lock (objLock)
					{
						if (this.attributes == null)
						{
							this.attributes = new XmlAttributeCollection(this);
						}
					}
				}
				return this.attributes;
			}
		}

		/// <summary>Gets a boolean value indicating whether the current node has any attributes.</summary>
		/// <returns>true if the current node has attributes; otherwise, false.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00075420 File Offset: 0x00073620
		public virtual bool HasAttributes
		{
			get
			{
				return this.attributes != null && this.attributes.Count > 0;
			}
		}

		/// <summary>Returns the value for the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. An empty string is returned if a matching attribute is not found or if the attribute does not have a specified or default value.</returns>
		/// <param name="name">The name of the attribute to retrieve. This is a qualified name. It is matched against the Name property of the matching node. </param>
		// Token: 0x06001481 RID: 5249 RVA: 0x0007543C File Offset: 0x0007363C
		public virtual string GetAttribute(string name)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		/// <summary>Sets the value of the attribute with the specified name.</summary>
		/// <param name="name">The name of the attribute to create or alter. This is a qualified name. If the name contains a colon it is parsed into prefix and local name components. </param>
		/// <param name="value">The value to set for the attribute. </param>
		/// <exception cref="T:System.Xml.XmlException">The specified name contains an invalid character. </exception>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x06001482 RID: 5250 RVA: 0x00075460 File Offset: 0x00073660
		public virtual void SetAttribute(string name, string value)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(name);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(name);
				xmlAttribute.Value = value;
				this.Attributes.InternalAppendAttribute(xmlAttribute);
				return;
			}
			xmlAttribute.Value = value;
		}

		/// <summary>Removes an attribute by name.</summary>
		/// <param name="name">The name of the attribute to remove.This is a qualified name. It is matched against the Name property of the matching node. </param>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x06001483 RID: 5251 RVA: 0x000754A1 File Offset: 0x000736A1
		public virtual void RemoveAttribute(string name)
		{
			if (this.HasAttributes)
			{
				this.Attributes.RemoveNamedItem(name);
			}
		}

		/// <summary>Returns the XmlAttribute with the specified name.</summary>
		/// <returns>The specified XmlAttribute or null if a matching attribute was not found.</returns>
		/// <param name="name">The name of the attribute to retrieve. This is a qualified name. It is matched against the Name property of the matching node. </param>
		// Token: 0x06001484 RID: 5252 RVA: 0x000754B8 File Offset: 0x000736B8
		public virtual XmlAttribute GetAttributeNode(string name)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[name];
			}
			return null;
		}

		/// <summary>Adds the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <returns>If the attribute replaces an existing attribute with the same name, the old XmlAttribute is returned; otherwise, null is returned.</returns>
		/// <param name="newAttr">The XmlAttribute node to add to the attribute collection for this element. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newAttr" /> was created from a different document than the one that created this node. Or this node is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="newAttr" /> is already an attribute of another XmlElement object. You must explicitly clone XmlAttribute nodes to re-use them in other XmlElement objects. </exception>
		// Token: 0x06001485 RID: 5253 RVA: 0x000754D0 File Offset: 0x000736D0
		public virtual XmlAttribute SetAttributeNode(XmlAttribute newAttr)
		{
			if (newAttr.OwnerElement != null)
			{
				throw new InvalidOperationException(Res.GetString("The 'Attribute' node cannot be inserted because it is already an attribute of another element."));
			}
			return (XmlAttribute)this.Attributes.SetNamedItem(newAttr);
		}

		/// <summary>Removes the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <returns>The removed XmlAttribute or null if <paramref name="oldAttr" /> is not an attribute node of the XmlElement.</returns>
		/// <param name="oldAttr">The XmlAttribute node to remove. If the removed attribute has a default value, it is immediately replaced. </param>
		/// <exception cref="T:System.ArgumentException">This node is read-only. </exception>
		// Token: 0x06001486 RID: 5254 RVA: 0x000754FB File Offset: 0x000736FB
		public virtual XmlAttribute RemoveAttributeNode(XmlAttribute oldAttr)
		{
			if (this.HasAttributes)
			{
				return this.Attributes.Remove(oldAttr);
			}
			return null;
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlElement.Name" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes.</returns>
		/// <param name="name">The name tag to match. This is a qualified name. It is matched against the Name property of the matching node. The asterisk (*) is a special value that matches all tags. </param>
		// Token: 0x06001487 RID: 5255 RVA: 0x00073CD1 File Offset: 0x00071ED1
		public virtual XmlNodeList GetElementsByTagName(string name)
		{
			return new XmlElementList(this, name);
		}

		/// <summary>Returns the value for the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The value of the specified attribute. An empty string is returned if a matching attribute is not found or if the attribute does not have a specified or default value.</returns>
		/// <param name="localName">The local name of the attribute to retrieve. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to retrieve. </param>
		// Token: 0x06001488 RID: 5256 RVA: 0x00075514 File Offset: 0x00073714
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(localName, namespaceURI);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		/// <summary>Sets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The attribute value.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <param name="value">The value to set for the attribute. </param>
		// Token: 0x06001489 RID: 5257 RVA: 0x0007553C File Offset: 0x0007373C
		public virtual string SetAttribute(string localName, string namespaceURI, string value)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(localName, namespaceURI);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(string.Empty, localName, namespaceURI);
				xmlAttribute.Value = value;
				this.Attributes.InternalAppendAttribute(xmlAttribute);
			}
			else
			{
				xmlAttribute.Value = value;
			}
			return value;
		}

		/// <summary>Removes an attribute with the specified local name and namespace URI. (If the removed attribute has a default value, it is immediately replaced).</summary>
		/// <param name="localName">The local name of the attribute to remove. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to remove. </param>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x0600148A RID: 5258 RVA: 0x00075586 File Offset: 0x00073786
		public virtual void RemoveAttribute(string localName, string namespaceURI)
		{
			this.RemoveAttributeNode(localName, namespaceURI);
		}

		/// <summary>Returns the <see cref="T:System.Xml.XmlAttribute" /> with the specified local name and namespace URI.</summary>
		/// <returns>The specified XmlAttribute or null if a matching attribute was not found.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x0600148B RID: 5259 RVA: 0x00075591 File Offset: 0x00073791
		public virtual XmlAttribute GetAttributeNode(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[localName, namespaceURI];
			}
			return null;
		}

		/// <summary>Adds the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <returns>The XmlAttribute to add.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x0600148C RID: 5260 RVA: 0x000755AC File Offset: 0x000737AC
		public virtual XmlAttribute SetAttributeNode(string localName, string namespaceURI)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(localName, namespaceURI);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(string.Empty, localName, namespaceURI);
				this.Attributes.InternalAppendAttribute(xmlAttribute);
			}
			return xmlAttribute;
		}

		/// <summary>Removes the <see cref="T:System.Xml.XmlAttribute" /> specified by the local name and namespace URI. (If the removed attribute has a default value, it is immediately replaced).</summary>
		/// <returns>The removed XmlAttribute or null if the XmlElement does not have a matching attribute node.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <exception cref="T:System.ArgumentException">This node is read-only. </exception>
		// Token: 0x0600148D RID: 5261 RVA: 0x000755E8 File Offset: 0x000737E8
		public virtual XmlAttribute RemoveAttributeNode(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				XmlAttribute attributeNode = this.GetAttributeNode(localName, namespaceURI);
				this.Attributes.Remove(attributeNode);
				return attributeNode;
			}
			return null;
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlElement.LocalName" /> and <see cref="P:System.Xml.XmlElement.NamespaceURI" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes.</returns>
		/// <param name="localName">The local name to match. The asterisk (*) is a special value that matches all tags. </param>
		/// <param name="namespaceURI">The namespace URI to match. </param>
		// Token: 0x0600148E RID: 5262 RVA: 0x00073D34 File Offset: 0x00071F34
		public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
		{
			return new XmlElementList(this, localName, namespaceURI);
		}

		/// <summary>Determines whether the current node has an attribute with the specified name.</summary>
		/// <returns>true if the current node has the specified attribute; otherwise, false.</returns>
		/// <param name="name">The name of the attribute to find. This is a qualified name. It is matched against the Name property of the matching node. </param>
		// Token: 0x0600148F RID: 5263 RVA: 0x00075616 File Offset: 0x00073816
		public virtual bool HasAttribute(string name)
		{
			return this.GetAttributeNode(name) != null;
		}

		/// <summary>Determines whether the current node has an attribute with the specified local name and namespace URI.</summary>
		/// <returns>true if the current node has the specified attribute; otherwise, false.</returns>
		/// <param name="localName">The local name of the attribute to find. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to find. </param>
		// Token: 0x06001490 RID: 5264 RVA: 0x00075622 File Offset: 0x00073822
		public virtual bool HasAttribute(string localName, string namespaceURI)
		{
			return this.GetAttributeNode(localName, namespaceURI) != null;
		}

		/// <summary>Saves the current node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001491 RID: 5265 RVA: 0x00075630 File Offset: 0x00073830
		public override void WriteTo(XmlWriter w)
		{
			if (base.GetType() == typeof(XmlElement))
			{
				XmlElement.WriteElementTo(w, this);
				return;
			}
			this.WriteStartElement(w);
			if (this.IsEmpty)
			{
				w.WriteEndElement();
				return;
			}
			this.WriteContentTo(w);
			w.WriteFullEndElement();
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00075680 File Offset: 0x00073880
		private static void WriteElementTo(XmlWriter writer, XmlElement e)
		{
			XmlNode xmlNode = e;
			XmlNode xmlNode2 = e;
			for (;;)
			{
				e = xmlNode2 as XmlElement;
				if (e != null && e.GetType() == typeof(XmlElement))
				{
					e.WriteStartElement(writer);
					if (e.IsEmpty)
					{
						writer.WriteEndElement();
					}
					else
					{
						if (e.lastChild != null)
						{
							xmlNode2 = e.FirstChild;
							continue;
						}
						writer.WriteFullEndElement();
					}
				}
				else
				{
					xmlNode2.WriteTo(writer);
				}
				while (xmlNode2 != xmlNode && xmlNode2 == xmlNode2.ParentNode.LastChild)
				{
					xmlNode2 = xmlNode2.ParentNode;
					writer.WriteFullEndElement();
				}
				if (xmlNode2 == xmlNode)
				{
					break;
				}
				xmlNode2 = xmlNode2.NextSibling;
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0007571C File Offset: 0x0007391C
		private void WriteStartElement(XmlWriter w)
		{
			w.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceURI);
			if (this.HasAttributes)
			{
				XmlAttributeCollection xmlAttributeCollection = this.Attributes;
				for (int i = 0; i < xmlAttributeCollection.Count; i++)
				{
					xmlAttributeCollection[i].WriteTo(w);
				}
			}
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001494 RID: 5268 RVA: 0x00075770 File Offset: 0x00073970
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		/// <summary>Removes the attribute node with the specified index from the element. (If the removed attribute has a default value, it is immediately replaced).</summary>
		/// <returns>The attribute node removed or null if there is no node at the given index.</returns>
		/// <param name="i">The index of the node to remove. The first node has index 0. </param>
		// Token: 0x06001495 RID: 5269 RVA: 0x00075797 File Offset: 0x00073997
		public virtual XmlNode RemoveAttributeAt(int i)
		{
			if (this.HasAttributes)
			{
				return this.attributes.RemoveAt(i);
			}
			return null;
		}

		/// <summary>Removes all specified attributes from the element. Default attributes are not removed.</summary>
		// Token: 0x06001496 RID: 5270 RVA: 0x000757AF File Offset: 0x000739AF
		public virtual void RemoveAllAttributes()
		{
			if (this.HasAttributes)
			{
				this.attributes.RemoveAll();
			}
		}

		/// <summary>Removes all specified attributes and children of the current node. Default attributes are not removed.</summary>
		// Token: 0x06001497 RID: 5271 RVA: 0x000757C4 File Offset: 0x000739C4
		public override void RemoveAll()
		{
			base.RemoveAll();
			this.RemoveAllAttributes();
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000757D2 File Offset: 0x000739D2
		internal void RemoveAllChildren()
		{
			base.RemoveAll();
		}

		/// <summary>Gets the post schema validation infoset that has been assigned to this node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the post schema validation infoset of this node.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x000750F9 File Offset: 0x000732F9
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the markup representing just the children of this node.</summary>
		/// <returns>The markup of the children of this node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00074573 File Offset: 0x00072773
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x000757DA File Offset: 0x000739DA
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAllChildren();
				new XmlLoader().LoadInnerXmlElement(this, value);
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x000757EE File Offset: 0x000739EE
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x000757F8 File Offset: 0x000739F8
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				XmlLinkedNode lastNode = this.LastNode;
				if (lastNode != null && lastNode.NodeType == XmlNodeType.Text && lastNode.next == lastNode)
				{
					lastNode.Value = value;
					return;
				}
				this.RemoveAllChildren();
				this.AppendChild(this.OwnerDocument.CreateTextNode(value));
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNode" /> immediately following this element.</summary>
		/// <returns>The XmlNode immediately following this element.</returns>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00075842 File Offset: 0x00073A42
		public override XmlNode NextSibling
		{
			get
			{
				if (this.parentNode != null && this.parentNode.LastNode != this)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00072002 File Offset: 0x00070202
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00003242 File Offset: 0x00001442
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Element;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00075862 File Offset: 0x00073A62
		internal override string XPLocalName
		{
			get
			{
				return this.LocalName;
			}
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0007586C File Offset: 0x00073A6C
		internal override string GetXPAttribute(string localName, string ns)
		{
			if (ns == this.OwnerDocument.strReservedXmlns)
			{
				return null;
			}
			XmlAttribute attributeNode = this.GetAttributeNode(localName, ns);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		// Token: 0x04000DC1 RID: 3521
		private XmlName name;

		// Token: 0x04000DC2 RID: 3522
		private XmlAttributeCollection attributes;

		// Token: 0x04000DC3 RID: 3523
		private XmlLinkedNode lastChild;
	}
}
