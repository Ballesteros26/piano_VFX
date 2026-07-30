using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a single node in the XML document. </summary>
	// Token: 0x02000231 RID: 561
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XmlNode : ICloneable, IEnumerable, IXPathNavigable
	{
		// Token: 0x06001545 RID: 5445 RVA: 0x000020FD File Offset: 0x000002FD
		internal XmlNode()
		{
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x000784C9 File Offset: 0x000766C9
		internal XmlNode(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new ArgumentException(Res.GetString("Cannot create a node without an owner document."));
			}
			this.parentNode = doc;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XPath.XPathNavigator" /> for navigating this object.</summary>
		/// <returns>An XPathNavigator object used to navigate the node. The XPathNavigator is positioned on the node from which the method was called. It is not positioned on the root of the document.</returns>
		// Token: 0x06001547 RID: 5447 RVA: 0x000784EC File Offset: 0x000766EC
		public virtual XPathNavigator CreateNavigator()
		{
			XmlDocument xmlDocument = this as XmlDocument;
			if (xmlDocument != null)
			{
				return xmlDocument.CreateNavigator(this);
			}
			return this.OwnerDocument.CreateNavigator(this);
		}

		/// <summary>Selects the first XmlNode that matches the XPath expression.</summary>
		/// <returns>The first XmlNode that matches the XPath query or null if no matching node is found. The XmlNode should not be expected to be connected "live" to the XML document. That is, changes that appear in the XML document may not appear in the XmlNode, and vice versa.</returns>
		/// <param name="xpath">The XPath expression. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression contains a prefix. </exception>
		// Token: 0x06001548 RID: 5448 RVA: 0x00078518 File Offset: 0x00076718
		public XmlNode SelectSingleNode(string xpath)
		{
			XmlNodeList xmlNodeList = this.SelectNodes(xpath);
			if (xmlNodeList == null)
			{
				return null;
			}
			return xmlNodeList[0];
		}

		/// <summary>Selects the first XmlNode that matches the XPath expression. Any prefixes found in the XPath expression are resolved using the supplied <see cref="T:System.Xml.XmlNamespaceManager" />.</summary>
		/// <returns>The first XmlNode that matches the XPath query or null if no matching node is found. The XmlNode should not be expected to be connected "live" to the XML document. That is, changes that appear in the XML document may not appear in the XmlNode, and vice versa.</returns>
		/// <param name="xpath">The XPath expression. </param>
		/// <param name="nsmgr">An <see cref="T:System.Xml.XmlNamespaceManager" /> to use for resolving namespaces for prefixes in the XPath expression. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression contains a prefix which is not defined in the XmlNamespaceManager. </exception>
		// Token: 0x06001549 RID: 5449 RVA: 0x0007853C File Offset: 0x0007673C
		public XmlNode SelectSingleNode(string xpath, XmlNamespaceManager nsmgr)
		{
			XPathNavigator xpathNavigator = this.CreateNavigator();
			if (xpathNavigator == null)
			{
				return null;
			}
			XPathExpression xpathExpression = xpathNavigator.Compile(xpath);
			xpathExpression.SetContext(nsmgr);
			return new XPathNodeList(xpathNavigator.Select(xpathExpression))[0];
		}

		/// <summary>Selects a list of nodes matching the XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a collection of nodes matching the XPath query.</returns>
		/// <param name="xpath">The XPath expression. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression contains a prefix. </exception>
		// Token: 0x0600154A RID: 5450 RVA: 0x00078578 File Offset: 0x00076778
		public XmlNodeList SelectNodes(string xpath)
		{
			XPathNavigator xpathNavigator = this.CreateNavigator();
			if (xpathNavigator == null)
			{
				return null;
			}
			return new XPathNodeList(xpathNavigator.Select(xpath));
		}

		/// <summary>Selects a list of nodes matching the XPath expression. Any prefixes found in the XPath expression are resolved using the supplied <see cref="T:System.Xml.XmlNamespaceManager" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a collection of nodes matching the XPath query.</returns>
		/// <param name="xpath">The XPath expression. </param>
		/// <param name="nsmgr">An <see cref="T:System.Xml.XmlNamespaceManager" /> to use for resolving namespaces for prefixes in the XPath expression. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression contains a prefix which is not defined in the XmlNamespaceManager. </exception>
		// Token: 0x0600154B RID: 5451 RVA: 0x000785A0 File Offset: 0x000767A0
		public XmlNodeList SelectNodes(string xpath, XmlNamespaceManager nsmgr)
		{
			XPathNavigator xpathNavigator = this.CreateNavigator();
			if (xpathNavigator == null)
			{
				return null;
			}
			XPathExpression xpathExpression = xpathNavigator.Compile(xpath);
			xpathExpression.SetContext(nsmgr);
			return new XPathNodeList(xpathNavigator.Select(xpathExpression));
		}

		/// <summary>Gets the qualified name of the node, when overridden in a derived class.</summary>
		/// <returns>The qualified name of the node. The name returned is dependent on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node: Type Name Attribute The qualified name of the attribute. CDATA #cdata-section Comment #comment Document #document DocumentFragment #document-fragment DocumentType The document type name. Element The qualified name of the element. Entity The name of the entity. EntityReference The name of the entity referenced. Notation The notation name. ProcessingInstruction The target of the processing instruction. Text #text Whitespace #whitespace SignificantWhitespace #significant-whitespace XmlDeclaration #xml-declaration </returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600154C RID: 5452
		public abstract string Name { get; }

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node: Type Value Attribute The value of the attribute. CDATASection The content of the CDATA Section. Comment The content of the comment. Document null. DocumentFragment null. DocumentType null. Element null. You can use the <see cref="P:System.Xml.XmlElement.InnerText" /> or <see cref="P:System.Xml.XmlElement.InnerXml" /> properties to access the value of the element node. Entity null. EntityReference null. Notation null. ProcessingInstruction The entire content excluding the target. Text The content of the text node. SignificantWhitespace The white space characters. White space can consist of one or more space characters, carriage returns, line feeds, or tabs. Whitespace The white space characters. White space can consist of one or more space characters, carriage returns, line feeds, or tabs. XmlDeclaration The content of the declaration (that is, everything between &lt;?xml and ?&gt;). </returns>
		/// <exception cref="T:System.ArgumentException">Setting the value of a node that is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">Setting the value of a node that is not supposed to have a value (for example, an Element node). </exception>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0000365F File Offset: 0x0000185F
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x000785D4 File Offset: 0x000767D4
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Res.GetString("Cannot set a value on node type '{0}'."), this.NodeType.ToString()));
			}
		}

		/// <summary>Gets the type of the current node, when overridden in a derived class.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600154F RID: 5455
		public abstract XmlNodeType NodeType { get; }

		/// <summary>Gets the parent of this node (for nodes that can have parents).</summary>
		/// <returns>The XmlNode that is the parent of the current node. If a node has just been created and not yet added to the tree, or if it has been removed from the tree, the parent is null. For all other nodes, the value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node. The following table describes the possible return values for the ParentNode property.NodeType Return Value of ParentNode Attribute, Document, DocumentFragment, Entity, Notation Returns null; these nodes do not have parents. CDATA Returns the element or entity reference containing the CDATA section. Comment Returns the element, entity reference, document type, or document containing the comment. DocumentType Returns the document node. Element Returns the parent node of the element. If the element is the root node in the tree, the parent is the document node. EntityReference Returns the element, attribute, or entity reference containing the entity reference. ProcessingInstruction Returns the document, element, document type, or entity reference containing the processing instruction. Text Returns the parent element, attribute, or entity reference containing the text node. </returns>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00078610 File Offset: 0x00076810
		public virtual XmlNode ParentNode
		{
			get
			{
				if (this.parentNode.NodeType != XmlNodeType.Document)
				{
					return this.parentNode;
				}
				XmlLinkedNode xmlLinkedNode = this.parentNode.FirstChild as XmlLinkedNode;
				if (xmlLinkedNode != null)
				{
					XmlLinkedNode xmlLinkedNode2 = xmlLinkedNode;
					while (xmlLinkedNode2 != this)
					{
						xmlLinkedNode2 = xmlLinkedNode2.next;
						if (xmlLinkedNode2 == null || xmlLinkedNode2 == xmlLinkedNode)
						{
							goto IL_0045;
						}
					}
					return this.parentNode;
				}
				IL_0045:
				return null;
			}
		}

		/// <summary>Gets all the child nodes of the node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> that contains all the child nodes of the node.If there are no child nodes, this property returns an empty <see cref="T:System.Xml.XmlNodeList" />.</returns>
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00078663 File Offset: 0x00076863
		public virtual XmlNodeList ChildNodes
		{
			get
			{
				return new XmlChildNodes(this);
			}
		}

		/// <summary>Gets the node immediately preceding this node.</summary>
		/// <returns>The preceding XmlNode. If there is no preceding node, null is returned.</returns>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual XmlNode PreviousSibling
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the node immediately following this node.</summary>
		/// <returns>The next XmlNode. If there is no next node, null is returned.</returns>
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual XmlNode NextSibling
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlAttributeCollection" /> containing the attributes of this node.</summary>
		/// <returns>An XmlAttributeCollection containing the attributes of the node.If the node is of type XmlNodeType.Element, the attributes of the node are returned. Otherwise, this property returns null.</returns>
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual XmlAttributeCollection Attributes
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.If the node is an <see cref="T:System.Xml.XmlDocument" /> (NodeType equals XmlNodeType.Document), this property returns null.</returns>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0007866B File Offset: 0x0007686B
		public virtual XmlDocument OwnerDocument
		{
			get
			{
				if (this.parentNode.NodeType == XmlNodeType.Document)
				{
					return (XmlDocument)this.parentNode;
				}
				return this.parentNode.OwnerDocument;
			}
		}

		/// <summary>Gets the first child of the node.</summary>
		/// <returns>The first child of the node. If there is no such node, null is returned.</returns>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00078694 File Offset: 0x00076894
		public virtual XmlNode FirstChild
		{
			get
			{
				XmlLinkedNode lastNode = this.LastNode;
				if (lastNode != null)
				{
					return lastNode.next;
				}
				return null;
			}
		}

		/// <summary>Gets the last child of the node.</summary>
		/// <returns>The last child of the node. If there is no such node, null is returned.</returns>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x000786B3 File Offset: 0x000768B3
		public virtual XmlNode LastChild
		{
			get
			{
				return this.LastNode;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0000226C File Offset: 0x0000046C
		internal virtual bool IsContainer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0000365F File Offset: 0x0000185F
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual XmlLinkedNode LastNode
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x000786BC File Offset: 0x000768BC
		internal bool AncestorNode(XmlNode node)
		{
			XmlNode xmlNode = this.ParentNode;
			while (xmlNode != null && xmlNode != this)
			{
				if (xmlNode == node)
				{
					return true;
				}
				xmlNode = xmlNode.ParentNode;
			}
			return false;
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x000786E8 File Offset: 0x000768E8
		internal bool IsConnected()
		{
			XmlNode xmlNode = this.ParentNode;
			while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Document)
			{
				xmlNode = xmlNode.ParentNode;
			}
			return xmlNode != null;
		}

		/// <summary>Inserts the specified node immediately before the specified reference node.</summary>
		/// <returns>The node being inserted.</returns>
		/// <param name="newChild">The XmlNode to insert. </param>
		/// <param name="refChild">The XmlNode that is the reference node. The <paramref name="newChild" /> is placed before this node. </param>
		/// <exception cref="T:System.InvalidOperationException">The current node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only. </exception>
		// Token: 0x0600155D RID: 5469 RVA: 0x00078718 File Offset: 0x00076918
		public virtual XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			if (this == newChild || this.AncestorNode(newChild))
			{
				throw new ArgumentException(Res.GetString("Cannot insert a node or any ancestor of that node as a child of itself."));
			}
			if (refChild == null)
			{
				return this.AppendChild(newChild);
			}
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("The current node cannot contain other nodes."));
			}
			if (refChild.ParentNode != this)
			{
				throw new ArgumentException(Res.GetString("The reference node is not a child of this node."));
			}
			if (newChild == refChild)
			{
				return newChild;
			}
			XmlDocument ownerDocument = newChild.OwnerDocument;
			XmlDocument ownerDocument2 = this.OwnerDocument;
			if (ownerDocument != null && ownerDocument != ownerDocument2 && ownerDocument != this)
			{
				throw new ArgumentException(Res.GetString("The node to be inserted is from a different document context."));
			}
			if (!this.CanInsertBefore(newChild, refChild))
			{
				throw new InvalidOperationException(Res.GetString("Cannot insert the node in the specified location."));
			}
			if (newChild.ParentNode != null)
			{
				newChild.ParentNode.RemoveChild(newChild);
			}
			if (newChild.NodeType == XmlNodeType.DocumentFragment)
			{
				XmlNode firstChild;
				XmlNode xmlNode = (firstChild = newChild.FirstChild);
				if (firstChild != null)
				{
					newChild.RemoveChild(firstChild);
					this.InsertBefore(firstChild, refChild);
					this.InsertAfter(newChild, firstChild);
				}
				return xmlNode;
			}
			if (!(newChild is XmlLinkedNode) || !this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type."));
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			XmlLinkedNode xmlLinkedNode2 = (XmlLinkedNode)refChild;
			string value = newChild.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(newChild, newChild.ParentNode, this, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			if (xmlLinkedNode2 == this.FirstChild)
			{
				xmlLinkedNode.next = xmlLinkedNode2;
				this.LastNode.next = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode.IsText && xmlLinkedNode2.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode, xmlLinkedNode2);
				}
			}
			else
			{
				XmlLinkedNode xmlLinkedNode3 = (XmlLinkedNode)xmlLinkedNode2.PreviousSibling;
				xmlLinkedNode.next = xmlLinkedNode2;
				xmlLinkedNode3.next = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode3.IsText)
				{
					if (xmlLinkedNode.IsText)
					{
						XmlNode.NestTextNodes(xmlLinkedNode3, xmlLinkedNode);
						if (xmlLinkedNode2.IsText)
						{
							XmlNode.NestTextNodes(xmlLinkedNode, xmlLinkedNode2);
						}
					}
					else if (xmlLinkedNode2.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode3, xmlLinkedNode2);
					}
				}
				else if (xmlLinkedNode.IsText && xmlLinkedNode2.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode, xmlLinkedNode2);
				}
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return xmlLinkedNode;
		}

		/// <summary>Inserts the specified node immediately after the specified reference node.</summary>
		/// <returns>The node being inserted.</returns>
		/// <param name="newChild">The XmlNode to insert. </param>
		/// <param name="refChild">The XmlNode that is the reference node. The <paramref name="newNode" /> is placed after the <paramref name="refNode" />. </param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only. </exception>
		// Token: 0x0600155E RID: 5470 RVA: 0x00078930 File Offset: 0x00076B30
		public virtual XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			if (this == newChild || this.AncestorNode(newChild))
			{
				throw new ArgumentException(Res.GetString("Cannot insert a node or any ancestor of that node as a child of itself."));
			}
			if (refChild == null)
			{
				return this.PrependChild(newChild);
			}
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("The current node cannot contain other nodes."));
			}
			if (refChild.ParentNode != this)
			{
				throw new ArgumentException(Res.GetString("The reference node is not a child of this node."));
			}
			if (newChild == refChild)
			{
				return newChild;
			}
			XmlDocument ownerDocument = newChild.OwnerDocument;
			XmlDocument ownerDocument2 = this.OwnerDocument;
			if (ownerDocument != null && ownerDocument != ownerDocument2 && ownerDocument != this)
			{
				throw new ArgumentException(Res.GetString("The node to be inserted is from a different document context."));
			}
			if (!this.CanInsertAfter(newChild, refChild))
			{
				throw new InvalidOperationException(Res.GetString("Cannot insert the node in the specified location."));
			}
			if (newChild.ParentNode != null)
			{
				newChild.ParentNode.RemoveChild(newChild);
			}
			if (newChild.NodeType == XmlNodeType.DocumentFragment)
			{
				XmlNode xmlNode = refChild;
				XmlNode firstChild = newChild.FirstChild;
				XmlNode nextSibling;
				for (XmlNode xmlNode2 = firstChild; xmlNode2 != null; xmlNode2 = nextSibling)
				{
					nextSibling = xmlNode2.NextSibling;
					newChild.RemoveChild(xmlNode2);
					this.InsertAfter(xmlNode2, xmlNode);
					xmlNode = xmlNode2;
				}
				return firstChild;
			}
			if (!(newChild is XmlLinkedNode) || !this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type."));
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			XmlLinkedNode xmlLinkedNode2 = (XmlLinkedNode)refChild;
			string value = newChild.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(newChild, newChild.ParentNode, this, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			if (xmlLinkedNode2 == this.LastNode)
			{
				xmlLinkedNode.next = xmlLinkedNode2.next;
				xmlLinkedNode2.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode2.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
				}
			}
			else
			{
				XmlLinkedNode next = xmlLinkedNode2.next;
				xmlLinkedNode.next = next;
				xmlLinkedNode2.next = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode2.IsText)
				{
					if (xmlLinkedNode.IsText)
					{
						XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
						if (next.IsText)
						{
							XmlNode.NestTextNodes(xmlLinkedNode, next);
						}
					}
					else if (next.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode2, next);
					}
				}
				else if (xmlLinkedNode.IsText && next.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode, next);
				}
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return xmlLinkedNode;
		}

		/// <summary>Replaces the child node <paramref name="oldChild" /> with <paramref name="newChild" /> node.</summary>
		/// <returns>The node replaced.</returns>
		/// <param name="newChild">The new node to put in the child list. </param>
		/// <param name="oldChild">The node being replaced in the list. </param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only.The <paramref name="oldChild" /> is not a child of this node. </exception>
		// Token: 0x0600155F RID: 5471 RVA: 0x00078B5C File Offset: 0x00076D5C
		public virtual XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode nextSibling = oldChild.NextSibling;
			this.RemoveChild(oldChild);
			this.InsertBefore(newChild, nextSibling);
			return oldChild;
		}

		/// <summary>Removes specified child node.</summary>
		/// <returns>The node removed.</returns>
		/// <param name="oldChild">The node being removed. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="oldChild" /> is not a child of this node. Or this node is read-only. </exception>
		// Token: 0x06001560 RID: 5472 RVA: 0x00078B84 File Offset: 0x00076D84
		public virtual XmlNode RemoveChild(XmlNode oldChild)
		{
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("The current node cannot contain other nodes, so the node to be removed is not its child."));
			}
			if (oldChild.ParentNode != this)
			{
				throw new ArgumentException(Res.GetString("The node to be removed is not a child of this node."));
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)oldChild;
			string value = xmlLinkedNode.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(xmlLinkedNode, this, null, value, value, XmlNodeChangedAction.Remove);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			XmlLinkedNode lastNode = this.LastNode;
			if (xmlLinkedNode == this.FirstChild)
			{
				if (xmlLinkedNode == lastNode)
				{
					this.LastNode = null;
					xmlLinkedNode.next = null;
					xmlLinkedNode.SetParent(null);
				}
				else
				{
					XmlLinkedNode next = xmlLinkedNode.next;
					if (next.IsText && xmlLinkedNode.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode, next);
					}
					lastNode.next = next;
					xmlLinkedNode.next = null;
					xmlLinkedNode.SetParent(null);
				}
			}
			else if (xmlLinkedNode == lastNode)
			{
				XmlLinkedNode xmlLinkedNode2 = (XmlLinkedNode)xmlLinkedNode.PreviousSibling;
				xmlLinkedNode2.next = xmlLinkedNode.next;
				this.LastNode = xmlLinkedNode2;
				xmlLinkedNode.next = null;
				xmlLinkedNode.SetParent(null);
			}
			else
			{
				XmlLinkedNode xmlLinkedNode3 = (XmlLinkedNode)xmlLinkedNode.PreviousSibling;
				XmlLinkedNode next2 = xmlLinkedNode.next;
				if (next2.IsText)
				{
					if (xmlLinkedNode3.IsText)
					{
						XmlNode.NestTextNodes(xmlLinkedNode3, next2);
					}
					else if (xmlLinkedNode.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode, next2);
					}
				}
				xmlLinkedNode3.next = next2;
				xmlLinkedNode.next = null;
				xmlLinkedNode.SetParent(null);
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return oldChild;
		}

		/// <summary>Adds the specified node to the beginning of the list of child nodes for this node.</summary>
		/// <returns>The node added.</returns>
		/// <param name="newChild">The node to add. All the contents of the node to be added are moved into the specified location.</param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only. </exception>
		// Token: 0x06001561 RID: 5473 RVA: 0x00078CEB File Offset: 0x00076EEB
		public virtual XmlNode PrependChild(XmlNode newChild)
		{
			return this.InsertBefore(newChild, this.FirstChild);
		}

		/// <summary>Adds the specified node to the end of the list of child nodes, of this node.</summary>
		/// <returns>The node added.</returns>
		/// <param name="newChild">The node to add. All the contents of the node to be added are moved into the specified location. </param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only. </exception>
		// Token: 0x06001562 RID: 5474 RVA: 0x00078CFC File Offset: 0x00076EFC
		public virtual XmlNode AppendChild(XmlNode newChild)
		{
			XmlDocument xmlDocument = this.OwnerDocument;
			if (xmlDocument == null)
			{
				xmlDocument = this as XmlDocument;
			}
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("The current node cannot contain other nodes."));
			}
			if (this == newChild || this.AncestorNode(newChild))
			{
				throw new ArgumentException(Res.GetString("Cannot insert a node or any ancestor of that node as a child of itself."));
			}
			if (newChild.ParentNode != null)
			{
				newChild.ParentNode.RemoveChild(newChild);
			}
			XmlDocument ownerDocument = newChild.OwnerDocument;
			if (ownerDocument != null && ownerDocument != xmlDocument && ownerDocument != this)
			{
				throw new ArgumentException(Res.GetString("The node to be inserted is from a different document context."));
			}
			if (newChild.NodeType == XmlNodeType.DocumentFragment)
			{
				XmlNode firstChild = newChild.FirstChild;
				XmlNode nextSibling;
				for (XmlNode xmlNode = firstChild; xmlNode != null; xmlNode = nextSibling)
				{
					nextSibling = xmlNode.NextSibling;
					newChild.RemoveChild(xmlNode);
					this.AppendChild(xmlNode);
				}
				return firstChild;
			}
			if (!(newChild is XmlLinkedNode) || !this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type."));
			}
			if (!this.CanInsertAfter(newChild, this.LastChild))
			{
				throw new InvalidOperationException(Res.GetString("Cannot insert the node in the specified location."));
			}
			string value = newChild.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(newChild, newChild.ParentNode, this, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			XmlLinkedNode lastNode = this.LastNode;
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (lastNode == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
			}
			else
			{
				xmlLinkedNode.next = lastNode.next;
				lastNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (lastNode.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(lastNode, xmlLinkedNode);
				}
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return xmlLinkedNode;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00078EA8 File Offset: 0x000770A8
		internal virtual XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode lastNode = this.LastNode;
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (lastNode == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParentForLoad(this);
			}
			else
			{
				xmlLinkedNode.next = lastNode.next;
				lastNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				if (lastNode.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(lastNode, xmlLinkedNode);
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

		// Token: 0x06001564 RID: 5476 RVA: 0x0000226C File Offset: 0x0000046C
		internal virtual bool IsValidChildType(XmlNodeType type)
		{
			return false;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00003242 File Offset: 0x00001442
		internal virtual bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			return true;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00003242 File Offset: 0x00001442
		internal virtual bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			return true;
		}

		/// <summary>Gets a value indicating whether this node has any child nodes.</summary>
		/// <returns>true if the node has child nodes; otherwise, false.</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00078F35 File Offset: 0x00077135
		public virtual bool HasChildNodes
		{
			get
			{
				return this.LastNode != null;
			}
		}

		/// <summary>Creates a duplicate of the node, when overridden in a derived class.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		/// <exception cref="T:System.InvalidOperationException">Calling this method on a node type that cannot be cloned. </exception>
		// Token: 0x06001568 RID: 5480
		public abstract XmlNode CloneNode(bool deep);

		// Token: 0x06001569 RID: 5481 RVA: 0x00078F40 File Offset: 0x00077140
		internal virtual void CopyChildren(XmlDocument doc, XmlNode container, bool deep)
		{
			for (XmlNode xmlNode = container.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.AppendChildForLoad(xmlNode.CloneNode(deep), doc);
			}
		}

		/// <summary>Puts all XmlText nodes in the full depth of the sub-tree underneath this XmlNode into a "normal" form where only markup (that is, tags, comments, processing instructions, CDATA sections, and entity references) separates XmlText nodes, that is, there are no adjacent XmlText nodes.</summary>
		// Token: 0x0600156A RID: 5482 RVA: 0x00078F70 File Offset: 0x00077170
		public virtual void Normalize()
		{
			XmlNode xmlNode = null;
			StringBuilder stringBuilder = new StringBuilder();
			XmlNode xmlNode2 = this.FirstChild;
			while (xmlNode2 != null)
			{
				XmlNode nextSibling = xmlNode2.NextSibling;
				XmlNodeType nodeType = xmlNode2.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					xmlNode2.Normalize();
					goto IL_0069;
				}
				if (nodeType != XmlNodeType.Text && nodeType - XmlNodeType.Whitespace > 1)
				{
					goto IL_0069;
				}
				stringBuilder.Append(xmlNode2.Value);
				if (this.NormalizeWinner(xmlNode, xmlNode2) == xmlNode)
				{
					this.RemoveChild(xmlNode2);
				}
				else
				{
					if (xmlNode != null)
					{
						this.RemoveChild(xmlNode);
					}
					xmlNode = xmlNode2;
				}
				IL_0088:
				xmlNode2 = nextSibling;
				continue;
				IL_0069:
				if (xmlNode != null)
				{
					xmlNode.Value = stringBuilder.ToString();
					xmlNode = null;
				}
				stringBuilder.Remove(0, stringBuilder.Length);
				goto IL_0088;
			}
			if (xmlNode != null && stringBuilder.Length > 0)
			{
				xmlNode.Value = stringBuilder.ToString();
			}
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00079024 File Offset: 0x00077224
		private XmlNode NormalizeWinner(XmlNode firstNode, XmlNode secondNode)
		{
			if (firstNode == null)
			{
				return secondNode;
			}
			if (firstNode.NodeType == XmlNodeType.Text)
			{
				return firstNode;
			}
			if (secondNode.NodeType == XmlNodeType.Text)
			{
				return secondNode;
			}
			if (firstNode.NodeType == XmlNodeType.SignificantWhitespace)
			{
				return firstNode;
			}
			if (secondNode.NodeType == XmlNodeType.SignificantWhitespace)
			{
				return secondNode;
			}
			if (firstNode.NodeType == XmlNodeType.Whitespace)
			{
				return firstNode;
			}
			if (secondNode.NodeType == XmlNodeType.Whitespace)
			{
				return secondNode;
			}
			return null;
		}

		/// <summary>Tests if the DOM implementation implements a specific feature.</summary>
		/// <returns>true if the feature is implemented in the specified version; otherwise, false. The following table describes the combinations that return true.Feature Version XML 1.0 XML 2.0 </returns>
		/// <param name="feature">The package name of the feature to test. This name is not case-sensitive. </param>
		/// <param name="version">The version number of the package name to test. If the version is not specified (null), supporting any version of the feature causes the method to return true. </param>
		// Token: 0x0600156C RID: 5484 RVA: 0x000762BF File Offset: 0x000744BF
		public virtual bool Supports(string feature, string version)
		{
			return string.Compare("XML", feature, StringComparison.OrdinalIgnoreCase) == 0 && (version == null || version == "1.0" || version == "2.0");
		}

		/// <summary>Gets the namespace URI of this node.</summary>
		/// <returns>The namespace URI of this node. If there is no namespace URI, this property returns String.Empty.</returns>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x00003065 File Offset: 0x00001265
		public virtual string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the namespace prefix of this node.</summary>
		/// <returns>The namespace prefix of this node. For example, Prefix is bk for the element &lt;bk:book&gt;. If there is no prefix, this property returns String.Empty.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only. </exception>
		/// <exception cref="T:System.Xml.XmlException">The specified prefix contains an invalid character.The specified prefix is malformed.The specified prefix is "xml" and the namespaceURI of this node is different from "http://www.w3.org/XML/1998/namespace".This node is an attribute and the specified prefix is "xmlns" and the namespaceURI of this node is different from "http://www.w3.org/2000/xmlns/ ".This node is an attribute and the qualifiedName of this node is "xmlns". </exception>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00003065 File Offset: 0x00001265
		// (set) Token: 0x0600156F RID: 5487 RVA: 0x00002F50 File Offset: 0x00001150
		public virtual string Prefix
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		/// <summary>Gets the local name of the node, when overridden in a derived class.</summary>
		/// <returns>The name of the node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node: Type Name Attribute The local name of the attribute. CDATA #cdata-section Comment #comment Document #document DocumentFragment #document-fragment DocumentType The document type name. Element The local name of the element. Entity The name of the entity. EntityReference The name of the entity referenced. Notation The notation name. ProcessingInstruction The target of the processing instruction. Text #text Whitespace #whitespace SignificantWhitespace #significant-whitespace XmlDeclaration #xml-declaration </returns>
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001570 RID: 5488
		public abstract string LocalName { get; }

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.</returns>
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0007907D File Offset: 0x0007727D
		public virtual bool IsReadOnly
		{
			get
			{
				XmlDocument ownerDocument = this.OwnerDocument;
				return XmlNode.HasReadOnlyParent(this);
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0007908C File Offset: 0x0007728C
		internal static bool HasReadOnlyParent(XmlNode n)
		{
			while (n != null)
			{
				XmlNodeType nodeType = n.NodeType;
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType - XmlNodeType.EntityReference <= 1)
					{
						return true;
					}
					n = n.ParentNode;
				}
				else
				{
					n = ((XmlAttribute)n).OwnerElement;
				}
			}
			return false;
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		// Token: 0x06001573 RID: 5491 RVA: 0x000790C9 File Offset: 0x000772C9
		public virtual XmlNode Clone()
		{
			return this.CloneNode(true);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.XmlNode.Clone" />.</summary>
		/// <returns>A copy of the node from which it is called.</returns>
		// Token: 0x06001574 RID: 5492 RVA: 0x000790C9 File Offset: 0x000772C9
		object ICloneable.Clone()
		{
			return this.CloneNode(true);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.XmlNode.GetEnumerator" />.</summary>
		/// <returns>Returns an enumerator for the collection.</returns>
		// Token: 0x06001575 RID: 5493 RVA: 0x000790D2 File Offset: 0x000772D2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new XmlChildEnumerator(this);
		}

		/// <summary>Get an enumerator that iterates through the child nodes in the current node.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the child nodes in the current node.</returns>
		// Token: 0x06001576 RID: 5494 RVA: 0x000790D2 File Offset: 0x000772D2
		public IEnumerator GetEnumerator()
		{
			return new XmlChildEnumerator(this);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000790DC File Offset: 0x000772DC
		private void AppendChildText(StringBuilder builder)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.FirstChild == null)
				{
					if (xmlNode.NodeType == XmlNodeType.Text || xmlNode.NodeType == XmlNodeType.CDATA || xmlNode.NodeType == XmlNodeType.Whitespace || xmlNode.NodeType == XmlNodeType.SignificantWhitespace)
					{
						builder.Append(xmlNode.InnerText);
					}
				}
				else
				{
					xmlNode.AppendChildText(builder);
				}
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all its child nodes.</summary>
		/// <returns>The concatenated values of the node and all its child nodes.</returns>
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00079140 File Offset: 0x00077340
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x00079194 File Offset: 0x00077394
		public virtual string InnerText
		{
			get
			{
				XmlNode firstChild = this.FirstChild;
				if (firstChild == null)
				{
					return string.Empty;
				}
				if (firstChild.NextSibling == null)
				{
					XmlNodeType nodeType = firstChild.NodeType;
					if (nodeType - XmlNodeType.Text <= 1 || nodeType - XmlNodeType.Whitespace <= 1)
					{
						return firstChild.Value;
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				this.AppendChildText(stringBuilder);
				return stringBuilder.ToString();
			}
			set
			{
				XmlNode firstChild = this.FirstChild;
				if (firstChild != null && firstChild.NextSibling == null && firstChild.NodeType == XmlNodeType.Text)
				{
					firstChild.Value = value;
					return;
				}
				this.RemoveAll();
				this.AppendChild(this.OwnerDocument.CreateTextNode(value));
			}
		}

		/// <summary>Gets the markup containing this node and all its child nodes.</summary>
		/// <returns>The markup containing this node and all its child nodes.NoteOuterXml does not return default attributes.</returns>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x000791E0 File Offset: 0x000773E0
		public virtual string OuterXml
		{
			get
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(stringWriter);
				try
				{
					this.WriteTo(xmlDOMTextWriter);
				}
				finally
				{
					xmlDOMTextWriter.Close();
				}
				return stringWriter.ToString();
			}
		}

		/// <summary>Gets or sets the markup representing only the child nodes of this node.</summary>
		/// <returns>The markup of the child nodes of this node.NoteInnerXml does not return default attributes.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property on a node that cannot have child nodes. </exception>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00079228 File Offset: 0x00077428
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x00076044 File Offset: 0x00074244
		public virtual string InnerXml
		{
			get
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(stringWriter);
				try
				{
					this.WriteContentTo(xmlDOMTextWriter);
				}
				finally
				{
					xmlDOMTextWriter.Close();
				}
				return stringWriter.ToString();
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("Cannot set the 'InnerXml' for the current node because it is either read-only or cannot have children."));
			}
		}

		/// <summary>Gets the post schema validation infoset that has been assigned to this node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the post schema validation infoset of this node.</returns>
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00079270 File Offset: 0x00077470
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return XmlDocument.NotKnownSchemaInfo;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The location from which the node was loaded or String.Empty if the node has no base URI.</returns>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00079278 File Offset: 0x00077478
		public virtual string BaseURI
		{
			get
			{
				for (XmlNode xmlNode = this.ParentNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
				{
					XmlNodeType nodeType = xmlNode.NodeType;
					if (nodeType == XmlNodeType.EntityReference)
					{
						return ((XmlEntityReference)xmlNode).ChildBaseURI;
					}
					if (nodeType == XmlNodeType.Document || nodeType == XmlNodeType.Entity || nodeType == XmlNodeType.Attribute)
					{
						return xmlNode.BaseURI;
					}
				}
				return string.Empty;
			}
		}

		/// <summary>Saves the current node to the specified <see cref="T:System.Xml.XmlWriter" />, when overridden in a derived class.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x0600157F RID: 5503
		public abstract void WriteTo(XmlWriter w);

		/// <summary>Saves all the child nodes of the node to the specified <see cref="T:System.Xml.XmlWriter" />, when overridden in a derived class.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001580 RID: 5504
		public abstract void WriteContentTo(XmlWriter w);

		/// <summary>Removes all the child nodes and/or attributes of the current node.</summary>
		// Token: 0x06001581 RID: 5505 RVA: 0x000792C8 File Offset: 0x000774C8
		public virtual void RemoveAll()
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				this.RemoveChild(xmlNode);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x000792F0 File Offset: 0x000774F0
		internal XmlDocument Document
		{
			get
			{
				if (this.NodeType == XmlNodeType.Document)
				{
					return (XmlDocument)this;
				}
				return this.OwnerDocument;
			}
		}

		/// <summary>Looks up the closest xmlns declaration for the given prefix that is in scope for the current node and returns the namespace URI in the declaration.</summary>
		/// <returns>The namespace URI of the specified prefix.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to find. </param>
		// Token: 0x06001583 RID: 5507 RVA: 0x0007930C File Offset: 0x0007750C
		public virtual string GetNamespaceOfPrefix(string prefix)
		{
			string namespaceOfPrefixStrict = this.GetNamespaceOfPrefixStrict(prefix);
			if (namespaceOfPrefixStrict == null)
			{
				return string.Empty;
			}
			return namespaceOfPrefixStrict;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0007932C File Offset: 0x0007752C
		internal string GetNamespaceOfPrefixStrict(string prefix)
		{
			XmlDocument document = this.Document;
			if (document != null)
			{
				prefix = document.NameTable.Get(prefix);
				if (prefix == null)
				{
					return null;
				}
				XmlNode xmlNode = this;
				while (xmlNode != null)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement.HasAttributes)
						{
							XmlAttributeCollection attributes = xmlElement.Attributes;
							if (prefix.Length == 0)
							{
								for (int i = 0; i < attributes.Count; i++)
								{
									XmlAttribute xmlAttribute = attributes[i];
									if (xmlAttribute.Prefix.Length == 0 && Ref.Equal(xmlAttribute.LocalName, document.strXmlns))
									{
										return xmlAttribute.Value;
									}
								}
							}
							else
							{
								for (int j = 0; j < attributes.Count; j++)
								{
									XmlAttribute xmlAttribute2 = attributes[j];
									if (Ref.Equal(xmlAttribute2.Prefix, document.strXmlns))
									{
										if (Ref.Equal(xmlAttribute2.LocalName, prefix))
										{
											return xmlAttribute2.Value;
										}
									}
									else if (Ref.Equal(xmlAttribute2.Prefix, prefix))
									{
										return xmlAttribute2.NamespaceURI;
									}
								}
							}
						}
						if (Ref.Equal(xmlNode.Prefix, prefix))
						{
							return xmlNode.NamespaceURI;
						}
						xmlNode = xmlNode.ParentNode;
					}
					else if (xmlNode.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				if (Ref.Equal(document.strXml, prefix))
				{
					return document.strReservedXml;
				}
				if (Ref.Equal(document.strXmlns, prefix))
				{
					return document.strReservedXmlns;
				}
			}
			return null;
		}

		/// <summary>Looks up the closest xmlns declaration for the given namespace URI that is in scope for the current node and returns the prefix defined in that declaration.</summary>
		/// <returns>The prefix for the specified namespace URI.</returns>
		/// <param name="namespaceURI">The namespace URI whose prefix you want to find. </param>
		// Token: 0x06001585 RID: 5509 RVA: 0x000794A8 File Offset: 0x000776A8
		public virtual string GetPrefixOfNamespace(string namespaceURI)
		{
			string prefixOfNamespaceStrict = this.GetPrefixOfNamespaceStrict(namespaceURI);
			if (prefixOfNamespaceStrict == null)
			{
				return string.Empty;
			}
			return prefixOfNamespaceStrict;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000794C8 File Offset: 0x000776C8
		internal string GetPrefixOfNamespaceStrict(string namespaceURI)
		{
			XmlDocument document = this.Document;
			if (document != null)
			{
				namespaceURI = document.NameTable.Add(namespaceURI);
				XmlNode xmlNode = this;
				while (xmlNode != null)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement.HasAttributes)
						{
							XmlAttributeCollection attributes = xmlElement.Attributes;
							for (int i = 0; i < attributes.Count; i++)
							{
								XmlAttribute xmlAttribute = attributes[i];
								if (xmlAttribute.Prefix.Length == 0)
								{
									if (Ref.Equal(xmlAttribute.LocalName, document.strXmlns) && xmlAttribute.Value == namespaceURI)
									{
										return string.Empty;
									}
								}
								else if (Ref.Equal(xmlAttribute.Prefix, document.strXmlns))
								{
									if (xmlAttribute.Value == namespaceURI)
									{
										return xmlAttribute.LocalName;
									}
								}
								else if (Ref.Equal(xmlAttribute.NamespaceURI, namespaceURI))
								{
									return xmlAttribute.Prefix;
								}
							}
						}
						if (Ref.Equal(xmlNode.NamespaceURI, namespaceURI))
						{
							return xmlNode.Prefix;
						}
						xmlNode = xmlNode.ParentNode;
					}
					else if (xmlNode.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				if (Ref.Equal(document.strReservedXml, namespaceURI))
				{
					return document.strXml;
				}
				if (Ref.Equal(document.strReservedXmlns, namespaceURI))
				{
					return document.strXmlns;
				}
			}
			return null;
		}

		/// <summary>Gets the first child element with the specified <see cref="P:System.Xml.XmlNode.Name" />.</summary>
		/// <returns>The first <see cref="T:System.Xml.XmlElement" /> that matches the specified name.</returns>
		/// <param name="name">The qualified name of the element to retrieve. </param>
		// Token: 0x1700041B RID: 1051
		public virtual XmlElement this[string name]
		{
			get
			{
				for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == name)
					{
						return (XmlElement)xmlNode;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the first child element with the specified <see cref="P:System.Xml.XmlNode.LocalName" /> and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>The first <see cref="T:System.Xml.XmlElement" /> with the matching <paramref name="localname" /> and <paramref name="ns" />.</returns>
		/// <param name="localname">The local name of the element. </param>
		/// <param name="ns">The namespace URI of the element. </param>
		// Token: 0x1700041C RID: 1052
		public virtual XmlElement this[string localname, string ns]
		{
			get
			{
				for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == localname && xmlNode.NamespaceURI == ns)
					{
						return (XmlElement)xmlNode;
					}
				}
				return null;
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000796B5 File Offset: 0x000778B5
		internal virtual void SetParent(XmlNode node)
		{
			if (node == null)
			{
				this.parentNode = this.OwnerDocument;
				return;
			}
			this.parentNode = node;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00072002 File Offset: 0x00070202
		internal virtual void SetParentForLoad(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x000796D0 File Offset: 0x000778D0
		internal static void SplitName(string name, out string prefix, out string localName)
		{
			int num = name.IndexOf(':');
			if (-1 == num || num == 0 || name.Length - 1 == num)
			{
				prefix = string.Empty;
				localName = name;
				return;
			}
			prefix = name.Substring(0, num);
			localName = name.Substring(num + 1);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00079718 File Offset: 0x00077918
		internal virtual XmlNode FindChild(XmlNodeType type)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == type)
				{
					return xmlNode;
				}
			}
			return null;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00079744 File Offset: 0x00077944
		internal virtual XmlNodeChangedEventArgs GetEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			if (ownerDocument == null)
			{
				return null;
			}
			if (!ownerDocument.IsLoading && ((newParent != null && newParent.IsReadOnly) || (oldParent != null && oldParent.IsReadOnly)))
			{
				throw new InvalidOperationException(Res.GetString("This node is read-only. It cannot be modified."));
			}
			return ownerDocument.GetEventArgs(node, oldParent, newParent, oldValue, newValue, action);
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0007979A File Offset: 0x0007799A
		internal virtual void BeforeEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				this.OwnerDocument.BeforeEvent(args);
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000797AB File Offset: 0x000779AB
		internal virtual void AfterEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				this.OwnerDocument.AfterEvent(args);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x000797BC File Offset: 0x000779BC
		internal virtual XmlSpace XmlSpace
		{
			get
			{
				XmlNode xmlNode = this;
				for (;;)
				{
					XmlElement xmlElement = xmlNode as XmlElement;
					if (xmlElement != null && xmlElement.HasAttribute("xml:space"))
					{
						string text = XmlConvert.TrimString(xmlElement.GetAttribute("xml:space"));
						if (text == "default")
						{
							break;
						}
						if (text == "preserve")
						{
							return XmlSpace.Preserve;
						}
					}
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						return XmlSpace.None;
					}
				}
				return XmlSpace.Default;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00079820 File Offset: 0x00077A20
		internal virtual string XmlLang
		{
			get
			{
				XmlNode xmlNode = this;
				XmlElement xmlElement;
				for (;;)
				{
					xmlElement = xmlNode as XmlElement;
					if (xmlElement != null && xmlElement.HasAttribute("xml:lang"))
					{
						break;
					}
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						goto Block_3;
					}
				}
				return xmlElement.GetAttribute("xml:lang");
				Block_3:
				return string.Empty;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x000182C2 File Offset: 0x000164C2
		internal virtual XPathNodeType XPNodeType
		{
			get
			{
				return (XPathNodeType)(-1);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00003065 File Offset: 0x00001265
		internal virtual string XPLocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00003065 File Offset: 0x00001265
		internal virtual string GetXPAttribute(string localName, string namespaceURI)
		{
			return string.Empty;
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0000226C File Offset: 0x0000046C
		internal virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0000365F File Offset: 0x0000185F
		public virtual XmlNode PreviousText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00079863 File Offset: 0x00077A63
		internal static void NestTextNodes(XmlNode prevNode, XmlNode nextNode)
		{
			nextNode.parentNode = prevNode;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0007986C File Offset: 0x00077A6C
		internal static void UnnestTextNodes(XmlNode prevNode, XmlNode nextNode)
		{
			nextNode.parentNode = prevNode.ParentNode;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x0007987A File Offset: 0x00077A7A
		private object debuggerDisplayProxy
		{
			get
			{
				return new DebuggerDisplayXmlNodeProxy(this);
			}
		}

		// Token: 0x04000DFC RID: 3580
		internal XmlNode parentNode;
	}
}
