using System;

namespace System.Xml
{
	/// <summary>Represents an entity reference node.</summary>
	// Token: 0x02000227 RID: 551
	public class XmlEntityReference : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlEntityReference" /> class.</summary>
		/// <param name="name">The name of the entity reference; see the <see cref="P:System.Xml.XmlEntityReference.Name" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x060014D6 RID: 5334 RVA: 0x00076068 File Offset: 0x00074268
		protected internal XmlEntityReference(string name, XmlDocument doc)
			: base(doc)
		{
			if (!doc.IsLoading && name.Length > 0 && name[0] == '#')
			{
				throw new ArgumentException(Res.GetString("Cannot create an 'EntityReference' node with a name starting with '#'."));
			}
			this.name = doc.NameTable.Add(name);
			doc.fEntRefNodesPresent = true;
		}

		/// <summary>Gets the name of the node.</summary>
		/// <returns>The name of the entity referenced.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x000760C1 File Offset: 0x000742C1
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlEntityReference nodes, this property returns the name of the entity referenced.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x000760C1 File Offset: 0x000742C1
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value of the node. For XmlEntityReference nodes, this property returns null.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">Setting the property. </exception>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0000365F File Offset: 0x0000185F
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x000760C9 File Offset: 0x000742C9
		public override string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("'EntityReference' nodes have no support for setting value."));
			}
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>The node type. For XmlEntityReference nodes, the value is XmlNodeType.EntityReference.</returns>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x000038E3 File Offset: 0x00001AE3
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.EntityReference;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For XmlEntityReference nodes, this method always returns an entity reference node with no children. The replacement text is set when the node is inserted into a parent. </param>
		// Token: 0x060014DC RID: 5340 RVA: 0x000760DA File Offset: 0x000742DA
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateEntityReference(this.name);
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because XmlEntityReference nodes are read-only, this property always returns true.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00003242 File Offset: 0x00001442
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000760ED File Offset: 0x000742ED
		internal override void SetParent(XmlNode node)
		{
			base.SetParent(node);
			if (this.LastNode == null && node != null && node != this.OwnerDocument)
			{
				new XmlLoader().ExpandEntityReference(this);
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00076115 File Offset: 0x00074315
		internal override void SetParentForLoad(XmlNode node)
		{
			this.SetParent(node);
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0007611E File Offset: 0x0007431E
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x00076126 File Offset: 0x00074326
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

		// Token: 0x060014E3 RID: 5347 RVA: 0x00076130 File Offset: 0x00074330
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

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060014E4 RID: 5348 RVA: 0x00076182 File Offset: 0x00074382
		public override void WriteTo(XmlWriter w)
		{
			w.WriteEntityRef(this.name);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060014E5 RID: 5349 RVA: 0x00076190 File Offset: 0x00074390
		public override void WriteContentTo(XmlWriter w)
		{
			foreach (object obj in this)
			{
				((XmlNode)obj).WriteTo(w);
			}
		}

		/// <summary>Gets the base Uniform Resource Identifier (URI) of the current node.</summary>
		/// <returns>The location from which the node was loaded.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x000761E4 File Offset: 0x000743E4
		public override string BaseURI
		{
			get
			{
				return this.OwnerDocument.BaseURI;
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000761F4 File Offset: 0x000743F4
		private string ConstructBaseURI(string baseURI, string systemId)
		{
			if (baseURI == null)
			{
				return systemId;
			}
			int num = baseURI.LastIndexOf('/') + 1;
			string text = baseURI;
			if (num > 0 && num < baseURI.Length)
			{
				text = baseURI.Substring(0, num);
			}
			else if (num == 0)
			{
				text += "\\";
			}
			return text + systemId.Replace('\\', '/');
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0007624C File Offset: 0x0007444C
		internal string ChildBaseURI
		{
			get
			{
				XmlEntity entityNode = this.OwnerDocument.GetEntityNode(this.name);
				if (entityNode == null)
				{
					return string.Empty;
				}
				if (entityNode.SystemId != null && entityNode.SystemId.Length > 0)
				{
					return this.ConstructBaseURI(entityNode.BaseURI, entityNode.SystemId);
				}
				return entityNode.BaseURI;
			}
		}

		// Token: 0x04000DDE RID: 3550
		private string name;

		// Token: 0x04000DDF RID: 3551
		private XmlLinkedNode lastChild;
	}
}
