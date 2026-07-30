using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Xml
{
	/// <summary>Represents a collection of attributes that can be accessed by name or index.</summary>
	// Token: 0x02000216 RID: 534
	public sealed class XmlAttributeCollection : XmlNamedNodeMap, ICollection, IEnumerable
	{
		// Token: 0x06001362 RID: 4962 RVA: 0x000720A8 File Offset: 0x000702A8
		internal XmlAttributeCollection(XmlNode parent)
			: base(parent)
		{
		}

		/// <summary>Gets the attribute with the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlAttribute" /> at the specified index.</returns>
		/// <param name="i">The index of the attribute. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index being passed in is out of range. </exception>
		// Token: 0x17000366 RID: 870
		[IndexerName("ItemOf")]
		public XmlAttribute this[int i]
		{
			get
			{
				XmlAttribute xmlAttribute;
				try
				{
					xmlAttribute = (XmlAttribute)this.nodes[i];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new IndexOutOfRangeException(Res.GetString("The index being passed in is out of range."));
				}
				return xmlAttribute;
			}
		}

		/// <summary>Gets the attribute with the specified name.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlAttribute" /> with the specified name. If the attribute does not exist, this property returns null.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x17000367 RID: 871
		[IndexerName("ItemOf")]
		public XmlAttribute this[string name]
		{
			get
			{
				int hashCode = XmlName.GetHashCode(name);
				for (int i = 0; i < this.nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && name == xmlAttribute.Name)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the attribute with the specified local name and namespace Uniform Resource Identifier (URI).</summary>
		/// <returns>The <see cref="T:System.Xml.XmlAttribute" /> with the specified local name and namespace URI. If the attribute does not exist, this property returns null.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x17000368 RID: 872
		[IndexerName("ItemOf")]
		public XmlAttribute this[string localName, string namespaceURI]
		{
			get
			{
				int hashCode = XmlName.GetHashCode(localName);
				for (int i = 0; i < this.nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && localName == xmlAttribute.LocalName && namespaceURI == xmlAttribute.NamespaceURI)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000721B4 File Offset: 0x000703B4
		internal int FindNodeOffset(XmlAttribute node)
		{
			for (int i = 0; i < this.nodes.Count; i++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
				if (xmlAttribute.LocalNameHash == node.LocalNameHash && xmlAttribute.Name == node.Name && xmlAttribute.NamespaceURI == node.NamespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00072220 File Offset: 0x00070420
		internal int FindNodeOffsetNS(XmlAttribute node)
		{
			for (int i = 0; i < this.nodes.Count; i++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
				if (xmlAttribute.LocalNameHash == node.LocalNameHash && xmlAttribute.LocalName == node.LocalName && xmlAttribute.NamespaceURI == node.NamespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Adds a <see cref="T:System.Xml.XmlNode" /> using its <see cref="P:System.Xml.XmlNode.Name" /> property </summary>
		/// <returns>If the <paramref name="node" /> replaces an existing node with the same name, the old node is returned; otherwise, the added node is returned.</returns>
		/// <param name="node">An attribute node to store in this collection. The node will later be accessible using the name of the node. If a node with that name is already present in the collection, it is replaced by the new one; otherwise, the node is appended to the end of the collection. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="node" /> was created from a different <see cref="T:System.Xml.XmlDocument" /> than the one that created this collection.This XmlAttributeCollection is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is an <see cref="T:System.Xml.XmlAttribute" /> that is already an attribute of another <see cref="T:System.Xml.XmlElement" /> object. To re-use attributes in other elements, you must clone the XmlAttribute objects you want to re-use. </exception>
		// Token: 0x06001368 RID: 4968 RVA: 0x0007228C File Offset: 0x0007048C
		public override XmlNode SetNamedItem(XmlNode node)
		{
			if (node != null && !(node is XmlAttribute))
			{
				throw new ArgumentException(Res.GetString("An 'Attributes' collection can only contain 'Attribute' objects."));
			}
			int num = base.FindNodeOffset(node.LocalName, node.NamespaceURI);
			if (num == -1)
			{
				return this.InternalAppendAttribute((XmlAttribute)node);
			}
			XmlNode xmlNode = base.RemoveNodeAt(num);
			this.InsertNodeAt(num, node);
			return xmlNode;
		}

		/// <summary>Inserts the specified attribute as the first node in the collection.</summary>
		/// <returns>The XmlAttribute added to the collection.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlAttribute" /> to insert. </param>
		// Token: 0x06001369 RID: 4969 RVA: 0x000722E8 File Offset: 0x000704E8
		public XmlAttribute Prepend(XmlAttribute node)
		{
			if (node.OwnerDocument != null && node.OwnerDocument != this.parent.OwnerDocument)
			{
				throw new ArgumentException(Res.GetString("The named node is from a different document context."));
			}
			if (node.OwnerElement != null)
			{
				this.Detach(node);
			}
			this.RemoveDuplicateAttribute(node);
			this.InsertNodeAt(0, node);
			return node;
		}

		/// <summary>Inserts the specified attribute as the last node in the collection.</summary>
		/// <returns>The XmlAttribute to append to the collection.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlAttribute" /> to insert. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="node" /> was created from a document different from the one that created this collection. </exception>
		// Token: 0x0600136A RID: 4970 RVA: 0x00072344 File Offset: 0x00070544
		public XmlAttribute Append(XmlAttribute node)
		{
			XmlDocument ownerDocument = node.OwnerDocument;
			if (ownerDocument == null || !ownerDocument.IsLoading)
			{
				if (ownerDocument != null && ownerDocument != this.parent.OwnerDocument)
				{
					throw new ArgumentException(Res.GetString("The named node is from a different document context."));
				}
				if (node.OwnerElement != null)
				{
					this.Detach(node);
				}
				this.AddNode(node);
			}
			else
			{
				base.AddNodeForLoad(node, ownerDocument);
				this.InsertParentIntoElementIdAttrMap(node);
			}
			return node;
		}

		/// <summary>Inserts the specified attribute immediately before the specified reference attribute.</summary>
		/// <returns>The XmlAttribute to insert into the collection.</returns>
		/// <param name="newNode">The <see cref="T:System.Xml.XmlAttribute" /> to insert. </param>
		/// <param name="refNode">The <see cref="T:System.Xml.XmlAttribute" /> that is the reference attribute. <paramref name="newNode" /> is placed before the <paramref name="refNode" />. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newNode" /> was created from a document different from the one that created this collection. Or the <paramref name="refNode" /> is not a member of this collection. </exception>
		// Token: 0x0600136B RID: 4971 RVA: 0x000723B0 File Offset: 0x000705B0
		public XmlAttribute InsertBefore(XmlAttribute newNode, XmlAttribute refNode)
		{
			if (newNode == refNode)
			{
				return newNode;
			}
			if (refNode == null)
			{
				return this.Append(newNode);
			}
			if (refNode.OwnerElement != this.parent)
			{
				throw new ArgumentException(Res.GetString("The reference node must be a child of the current node."));
			}
			if (newNode.OwnerDocument != null && newNode.OwnerDocument != this.parent.OwnerDocument)
			{
				throw new ArgumentException(Res.GetString("The named node is from a different document context."));
			}
			if (newNode.OwnerElement != null)
			{
				this.Detach(newNode);
			}
			int num = base.FindNodeOffset(refNode.LocalName, refNode.NamespaceURI);
			int num2 = this.RemoveDuplicateAttribute(newNode);
			if (num2 >= 0 && num2 < num)
			{
				num--;
			}
			this.InsertNodeAt(num, newNode);
			return newNode;
		}

		/// <summary>Inserts the specified attribute immediately after the specified reference attribute.</summary>
		/// <returns>The XmlAttribute to insert into the collection.</returns>
		/// <param name="newNode">The <see cref="T:System.Xml.XmlAttribute" /> to insert. </param>
		/// <param name="refNode">The <see cref="T:System.Xml.XmlAttribute" /> that is the reference attribute. <paramref name="newNode" /> is placed after the <paramref name="refNode" />. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newNode" /> was created from a document different from the one that created this collection. Or the <paramref name="refNode" /> is not a member of this collection. </exception>
		// Token: 0x0600136C RID: 4972 RVA: 0x00072458 File Offset: 0x00070658
		public XmlAttribute InsertAfter(XmlAttribute newNode, XmlAttribute refNode)
		{
			if (newNode == refNode)
			{
				return newNode;
			}
			if (refNode == null)
			{
				return this.Prepend(newNode);
			}
			if (refNode.OwnerElement != this.parent)
			{
				throw new ArgumentException(Res.GetString("The reference node must be a child of the current node."));
			}
			if (newNode.OwnerDocument != null && newNode.OwnerDocument != this.parent.OwnerDocument)
			{
				throw new ArgumentException(Res.GetString("The named node is from a different document context."));
			}
			if (newNode.OwnerElement != null)
			{
				this.Detach(newNode);
			}
			int num = base.FindNodeOffset(refNode.LocalName, refNode.NamespaceURI);
			int num2 = this.RemoveDuplicateAttribute(newNode);
			if (num2 >= 0 && num2 < num)
			{
				num--;
			}
			this.InsertNodeAt(num + 1, newNode);
			return newNode;
		}

		/// <summary>Removes the specified attribute from the collection.</summary>
		/// <returns>The node removed or null if it is not found in the collection.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlAttribute" /> to remove. </param>
		// Token: 0x0600136D RID: 4973 RVA: 0x00072504 File Offset: 0x00070704
		public XmlAttribute Remove(XmlAttribute node)
		{
			int count = this.nodes.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.nodes[i] == node)
				{
					this.RemoveNodeAt(i);
					return node;
				}
			}
			return null;
		}

		/// <summary>Removes the attribute corresponding to the specified index from the collection.</summary>
		/// <returns>Returns null if there is no attribute at the specified index.</returns>
		/// <param name="i">The index of the node to remove. The first node has index 0. </param>
		// Token: 0x0600136E RID: 4974 RVA: 0x00072543 File Offset: 0x00070743
		public XmlAttribute RemoveAt(int i)
		{
			if (i < 0 || i >= this.Count)
			{
				return null;
			}
			return (XmlAttribute)this.RemoveNodeAt(i);
		}

		/// <summary>Removes all attributes from the collection.</summary>
		// Token: 0x0600136F RID: 4975 RVA: 0x00072560 File Offset: 0x00070760
		public void RemoveAll()
		{
			int i = this.Count;
			while (i > 0)
			{
				i--;
				this.RemoveAt(i);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.XmlAttributeCollection.CopyTo(System.Xml.XmlAttribute[],System.Int32)" />.</summary>
		/// <param name="array">The array that is the destination of the objects copied from this collection. </param>
		/// <param name="index">The index in the array where copying begins. </param>
		// Token: 0x06001370 RID: 4976 RVA: 0x00072588 File Offset: 0x00070788
		void ICollection.CopyTo(Array array, int index)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				array.SetValue(this.nodes[i], index);
				i++;
				index++;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.XmlAttributeCollection.System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>Returns true if the collection is synchronized.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0000226C File Offset: 0x0000046C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.XmlAttributeCollection.System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Returns the <see cref="T:System.Object" /> that is the root of the collection.</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.XmlAttributeCollection.System.Collections.ICollection.Count" />.</summary>
		/// <returns>Returns an int that contains the count of the attributes.</returns>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x000725C0 File Offset: 0x000707C0
		int ICollection.Count
		{
			get
			{
				return base.Count;
			}
		}

		/// <summary>Copies all the <see cref="T:System.Xml.XmlAttribute" /> objects from this collection into the given array.</summary>
		/// <param name="array">The array that is the destination of the objects copied from this collection. </param>
		/// <param name="index">The index in the array where copying begins. </param>
		// Token: 0x06001374 RID: 4980 RVA: 0x000725C8 File Offset: 0x000707C8
		public void CopyTo(XmlAttribute[] array, int index)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				array[index] = (XmlAttribute)((XmlNode)this.nodes[i]).CloneNode(true);
				i++;
				index++;
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0007260C File Offset: 0x0007080C
		internal override XmlNode AddNode(XmlNode node)
		{
			this.RemoveDuplicateAttribute((XmlAttribute)node);
			XmlNode xmlNode = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return xmlNode;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0007262E File Offset: 0x0007082E
		internal override XmlNode InsertNodeAt(int i, XmlNode node)
		{
			XmlNode xmlNode = base.InsertNodeAt(i, node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return xmlNode;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00072644 File Offset: 0x00070844
		internal override XmlNode RemoveNodeAt(int i)
		{
			XmlNode xmlNode = base.RemoveNodeAt(i);
			this.RemoveParentFromElementIdAttrMap((XmlAttribute)xmlNode);
			XmlAttribute defaultAttribute = this.parent.OwnerDocument.GetDefaultAttribute((XmlElement)this.parent, xmlNode.Prefix, xmlNode.LocalName, xmlNode.NamespaceURI);
			if (defaultAttribute != null)
			{
				this.InsertNodeAt(i, defaultAttribute);
			}
			return xmlNode;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000726A0 File Offset: 0x000708A0
		internal void Detach(XmlAttribute attr)
		{
			attr.OwnerElement.Attributes.Remove(attr);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000726B4 File Offset: 0x000708B4
		internal void InsertParentIntoElementIdAttrMap(XmlAttribute attr)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			if (xmlElement != null)
			{
				if (this.parent.OwnerDocument == null)
				{
					return;
				}
				XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
				if (idinfoByElement != null && idinfoByElement.Prefix == attr.XmlName.Prefix && idinfoByElement.LocalName == attr.XmlName.LocalName)
				{
					this.parent.OwnerDocument.AddElementWithId(attr.Value, xmlElement);
				}
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00072740 File Offset: 0x00070940
		internal void RemoveParentFromElementIdAttrMap(XmlAttribute attr)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			if (xmlElement != null)
			{
				if (this.parent.OwnerDocument == null)
				{
					return;
				}
				XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
				if (idinfoByElement != null && idinfoByElement.Prefix == attr.XmlName.Prefix && idinfoByElement.LocalName == attr.XmlName.LocalName)
				{
					this.parent.OwnerDocument.RemoveElementWithId(attr.Value, xmlElement);
				}
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000727CC File Offset: 0x000709CC
		internal int RemoveDuplicateAttribute(XmlAttribute attr)
		{
			int num = base.FindNodeOffset(attr.LocalName, attr.NamespaceURI);
			if (num != -1)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[num];
				base.RemoveNodeAt(num);
				this.RemoveParentFromElementIdAttrMap(xmlAttribute);
			}
			return num;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00072814 File Offset: 0x00070A14
		internal bool PrepareParentInElementIdAttrMap(string attrPrefix, string attrLocalName)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
			return idinfoByElement != null && idinfoByElement.Prefix == attrPrefix && idinfoByElement.LocalName == attrLocalName;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00072868 File Offset: 0x00070A68
		internal void ResetParentInElementIdAttrMap(string oldVal, string newVal)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			XmlDocument ownerDocument = this.parent.OwnerDocument;
			ownerDocument.RemoveElementWithId(oldVal, xmlElement);
			ownerDocument.AddElementWithId(newVal, xmlElement);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0007289B File Offset: 0x00070A9B
		internal XmlAttribute InternalAppendAttribute(XmlAttribute node)
		{
			XmlNode xmlNode = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap(node);
			return (XmlAttribute)xmlNode;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlAttributeCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
