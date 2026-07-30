using System;
using System.Collections;
using Unity;

namespace System.Xml
{
	/// <summary>Represents a collection of nodes that can be accessed by name or index.</summary>
	// Token: 0x0200022E RID: 558
	public class XmlNamedNodeMap : IEnumerable
	{
		// Token: 0x0600152A RID: 5418 RVA: 0x00077E9F File Offset: 0x0007609F
		internal XmlNamedNodeMap(XmlNode parent)
		{
			this.parent = parent;
		}

		/// <summary>Retrieves an <see cref="T:System.Xml.XmlNode" /> specified by name.</summary>
		/// <returns>An XmlNode with the specified name or null if a matching node is not found.</returns>
		/// <param name="name">The qualified name of the node to retrieve. It is matched against the <see cref="P:System.Xml.XmlNode.Name" /> property of the matching node.</param>
		// Token: 0x0600152B RID: 5419 RVA: 0x00077EB0 File Offset: 0x000760B0
		public virtual XmlNode GetNamedItem(string name)
		{
			int num = this.FindNodeOffset(name);
			if (num >= 0)
			{
				return (XmlNode)this.nodes[num];
			}
			return null;
		}

		/// <summary>Adds an <see cref="T:System.Xml.XmlNode" /> using its <see cref="P:System.Xml.XmlNode.Name" /> property.</summary>
		/// <returns>If the <paramref name="node" /> replaces an existing node with the same name, the old node is returned; otherwise, null is returned.</returns>
		/// <param name="node">An XmlNode to store in the XmlNamedNodeMap. If a node with that name is already present in the map, it is replaced by the new one.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> was created from a different <see cref="T:System.Xml.XmlDocument" /> than the one that created the XmlNamedNodeMap; or the XmlNamedNodeMap is read-only.</exception>
		// Token: 0x0600152C RID: 5420 RVA: 0x00077EDC File Offset: 0x000760DC
		public virtual XmlNode SetNamedItem(XmlNode node)
		{
			if (node == null)
			{
				return null;
			}
			int num = this.FindNodeOffset(node.LocalName, node.NamespaceURI);
			if (num == -1)
			{
				this.AddNode(node);
				return null;
			}
			return this.ReplaceNodeAt(num, node);
		}

		/// <summary>Removes the node from the XmlNamedNodeMap.</summary>
		/// <returns>The XmlNode removed from this XmlNamedNodeMap or null if a matching node was not found.</returns>
		/// <param name="name">The qualified name of the node to remove. The name is matched against the <see cref="P:System.Xml.XmlNode.Name" /> property of the matching node.</param>
		// Token: 0x0600152D RID: 5421 RVA: 0x00077F18 File Offset: 0x00076118
		public virtual XmlNode RemoveNamedItem(string name)
		{
			int num = this.FindNodeOffset(name);
			if (num >= 0)
			{
				return this.RemoveNodeAt(num);
			}
			return null;
		}

		/// <summary>Gets the number of nodes in the XmlNamedNodeMap.</summary>
		/// <returns>The number of nodes.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00077F3A File Offset: 0x0007613A
		public virtual int Count
		{
			get
			{
				return this.nodes.Count;
			}
		}

		/// <summary>Retrieves the node at the specified index in the XmlNamedNodeMap.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> at the specified index. If <paramref name="index" /> is less than 0 or greater than or equal to the <see cref="P:System.Xml.XmlNamedNodeMap.Count" /> property, null is returned.</returns>
		/// <param name="index">The index position of the node to retrieve from the XmlNamedNodeMap. The index is zero-based; therefore, the index of the first node is 0 and the index of the last node is <see cref="P:System.Xml.XmlNamedNodeMap.Count" /> -1.</param>
		// Token: 0x0600152F RID: 5423 RVA: 0x00077F48 File Offset: 0x00076148
		public virtual XmlNode Item(int index)
		{
			if (index < 0 || index >= this.nodes.Count)
			{
				return null;
			}
			XmlNode xmlNode;
			try
			{
				xmlNode = (XmlNode)this.nodes[index];
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new IndexOutOfRangeException(Res.GetString("The index being passed in is out of range."));
			}
			return xmlNode;
		}

		/// <summary>Retrieves a node with the matching <see cref="P:System.Xml.XmlNode.LocalName" /> and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> with the matching local name and namespace URI or null if a matching node was not found.</returns>
		/// <param name="localName">The local name of the node to retrieve.</param>
		/// <param name="namespaceURI">The namespace Uniform Resource Identifier (URI) of the node to retrieve.</param>
		// Token: 0x06001530 RID: 5424 RVA: 0x00077FA0 File Offset: 0x000761A0
		public virtual XmlNode GetNamedItem(string localName, string namespaceURI)
		{
			int num = this.FindNodeOffset(localName, namespaceURI);
			if (num >= 0)
			{
				return (XmlNode)this.nodes[num];
			}
			return null;
		}

		/// <summary>Removes a node with the matching <see cref="P:System.Xml.XmlNode.LocalName" /> and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> removed or null if a matching node was not found.</returns>
		/// <param name="localName">The local name of the node to remove.</param>
		/// <param name="namespaceURI">The namespace URI of the node to remove.</param>
		// Token: 0x06001531 RID: 5425 RVA: 0x00077FD0 File Offset: 0x000761D0
		public virtual XmlNode RemoveNamedItem(string localName, string namespaceURI)
		{
			int num = this.FindNodeOffset(localName, namespaceURI);
			if (num >= 0)
			{
				return this.RemoveNodeAt(num);
			}
			return null;
		}

		/// <summary>Provides support for the "foreach" style iteration over the collection of nodes in the XmlNamedNodeMap.</summary>
		/// <returns>An enumerator object.</returns>
		// Token: 0x06001532 RID: 5426 RVA: 0x00077FF3 File Offset: 0x000761F3
		public virtual IEnumerator GetEnumerator()
		{
			return this.nodes.GetEnumerator();
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00078000 File Offset: 0x00076200
		internal int FindNodeOffset(string name)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.nodes[i];
				if (name == xmlNode.Name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00078044 File Offset: 0x00076244
		internal int FindNodeOffset(string localName, string namespaceURI)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.nodes[i];
				if (xmlNode.LocalName == localName && xmlNode.NamespaceURI == namespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00078098 File Offset: 0x00076298
		internal virtual XmlNode AddNode(XmlNode node)
		{
			XmlNode xmlNode;
			if (node.NodeType == XmlNodeType.Attribute)
			{
				xmlNode = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				xmlNode = node.ParentNode;
			}
			string value = node.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(node, xmlNode, this.parent, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.Add(node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00078118 File Offset: 0x00076318
		internal virtual XmlNode AddNodeForLoad(XmlNode node, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(node, this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			this.nodes.Add(node);
			node.SetParent(this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return node;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00078160 File Offset: 0x00076360
		internal virtual XmlNode RemoveNodeAt(int i)
		{
			XmlNode xmlNode = (XmlNode)this.nodes[i];
			string value = xmlNode.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(xmlNode, this.parent, null, value, value, XmlNodeChangedAction.Remove);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.RemoveAt(i);
			xmlNode.SetParent(null);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return xmlNode;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000781CF File Offset: 0x000763CF
		internal XmlNode ReplaceNodeAt(int i, XmlNode node)
		{
			XmlNode xmlNode = this.RemoveNodeAt(i);
			this.InsertNodeAt(i, node);
			return xmlNode;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000781E4 File Offset: 0x000763E4
		internal virtual XmlNode InsertNodeAt(int i, XmlNode node)
		{
			XmlNode xmlNode;
			if (node.NodeType == XmlNodeType.Attribute)
			{
				xmlNode = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				xmlNode = node.ParentNode;
			}
			string value = node.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(node, xmlNode, this.parent, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.Insert(i, node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlNamedNodeMap()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000DF7 RID: 3575
		internal XmlNode parent;

		// Token: 0x04000DF8 RID: 3576
		internal XmlNamedNodeMap.SmallXmlNodeList nodes;

		// Token: 0x0200022F RID: 559
		internal struct SmallXmlNodeList
		{
			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x0600153B RID: 5435 RVA: 0x00078268 File Offset: 0x00076468
			public int Count
			{
				get
				{
					if (this.field == null)
					{
						return 0;
					}
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						return arrayList.Count;
					}
					return 1;
				}
			}

			// Token: 0x17000401 RID: 1025
			public object this[int index]
			{
				get
				{
					if (this.field == null)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						return arrayList[index];
					}
					if (index != 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.field;
				}
			}

			// Token: 0x0600153D RID: 5437 RVA: 0x000782E4 File Offset: 0x000764E4
			public void Add(object value)
			{
				if (this.field == null)
				{
					if (value == null)
					{
						this.field = new ArrayList { null };
						return;
					}
					this.field = value;
					return;
				}
				else
				{
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						arrayList.Add(value);
						return;
					}
					this.field = new ArrayList { this.field, value };
					return;
				}
			}

			// Token: 0x0600153E RID: 5438 RVA: 0x00078354 File Offset: 0x00076554
			public void RemoveAt(int index)
			{
				if (this.field == null)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				ArrayList arrayList = this.field as ArrayList;
				if (arrayList != null)
				{
					arrayList.RemoveAt(index);
					return;
				}
				if (index != 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.field = null;
			}

			// Token: 0x0600153F RID: 5439 RVA: 0x000783A0 File Offset: 0x000765A0
			public void Insert(int index, object value)
			{
				if (this.field == null)
				{
					if (index != 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.Add(value);
					return;
				}
				else
				{
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						arrayList.Insert(index, value);
						return;
					}
					if (index == 0)
					{
						this.field = new ArrayList { value, this.field };
						return;
					}
					if (index == 1)
					{
						this.field = new ArrayList { this.field, value };
						return;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x06001540 RID: 5440 RVA: 0x0007843C File Offset: 0x0007663C
			public IEnumerator GetEnumerator()
			{
				if (this.field == null)
				{
					return XmlDocument.EmptyEnumerator;
				}
				ArrayList arrayList = this.field as ArrayList;
				if (arrayList != null)
				{
					return arrayList.GetEnumerator();
				}
				return new XmlNamedNodeMap.SmallXmlNodeList.SingleObjectEnumerator(this.field);
			}

			// Token: 0x04000DF9 RID: 3577
			private object field;

			// Token: 0x02000230 RID: 560
			private class SingleObjectEnumerator : IEnumerator
			{
				// Token: 0x06001541 RID: 5441 RVA: 0x00078478 File Offset: 0x00076678
				public SingleObjectEnumerator(object value)
				{
					this.loneValue = value;
				}

				// Token: 0x17000402 RID: 1026
				// (get) Token: 0x06001542 RID: 5442 RVA: 0x0007848E File Offset: 0x0007668E
				public object Current
				{
					get
					{
						if (this.position != 0)
						{
							throw new InvalidOperationException();
						}
						return this.loneValue;
					}
				}

				// Token: 0x06001543 RID: 5443 RVA: 0x000784A4 File Offset: 0x000766A4
				public bool MoveNext()
				{
					if (this.position < 0)
					{
						this.position = 0;
						return true;
					}
					this.position = 1;
					return false;
				}

				// Token: 0x06001544 RID: 5444 RVA: 0x000784C0 File Offset: 0x000766C0
				public void Reset()
				{
					this.position = -1;
				}

				// Token: 0x04000DFA RID: 3578
				private object loneValue;

				// Token: 0x04000DFB RID: 3579
				private int position = -1;
			}
		}
	}
}
