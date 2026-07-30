using System;
using Unity;

namespace System.Xml
{
	/// <summary>Represents an entity declaration, such as &lt;!ENTITY... &gt;.</summary>
	// Token: 0x02000226 RID: 550
	public class XmlEntity : XmlNode
	{
		// Token: 0x060014BF RID: 5311 RVA: 0x00075F5C File Offset: 0x0007415C
		internal XmlEntity(string name, string strdata, string publicId, string systemId, string notationName, XmlDocument doc)
			: base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.publicId = publicId;
			this.systemId = systemId;
			this.notationName = notationName;
			this.unparsedReplacementStr = strdata;
			this.childrenFoliating = false;
		}

		/// <summary>Creates a duplicate of this node. Entity nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlEntity" /> object throws an exception.</summary>
		/// <returns>Returns a copy of the <see cref="T:System.Xml.XmlNode" /> from which the method is called.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself.</param>
		/// <exception cref="T:System.InvalidOperationException">Entity nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlEntity" /> object throws an exception.</exception>
		// Token: 0x060014C0 RID: 5312 RVA: 0x00075FA9 File Offset: 0x000741A9
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException(Res.GetString("'Entity' and 'Notation' nodes cannot be cloned."));
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because XmlEntity nodes are read-only, this property always returns true.</returns>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00003242 File Offset: 0x00001442
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the name of the node.</summary>
		/// <returns>The name of the entity.</returns>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00075FBA File Offset: 0x000741BA
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the name of the node without the namespace prefix.</summary>
		/// <returns>For XmlEntity nodes, this property returns the name of the entity.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00075FBA File Offset: 0x000741BA
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the concatenated values of the entity node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x000757EE File Offset: 0x000739EE
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x00075FC2 File Offset: 0x000741C2
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("The 'InnerText' of an 'Entity' node is read-only and cannot be set."));
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00075FD3 File Offset: 0x000741D3
		// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00075FFD File Offset: 0x000741FD
		internal override XmlLinkedNode LastNode
		{
			get
			{
				if (this.lastChild == null && !this.childrenFoliating)
				{
					this.childrenFoliating = true;
					new XmlLoader().ExpandEntity(this);
				}
				return this.lastChild;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00076006 File Offset: 0x00074206
		internal override bool IsValidChildType(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.Element || type == XmlNodeType.ProcessingInstruction || type == XmlNodeType.Comment || type == XmlNodeType.CDATA || type == XmlNodeType.Whitespace || type == XmlNodeType.SignificantWhitespace || type == XmlNodeType.EntityReference;
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>The node type. For XmlEntity nodes, the value is XmlNodeType.Entity.</returns>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00006B15 File Offset: 0x00004D15
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Entity;
			}
		}

		/// <summary>Gets the value of the public identifier on the entity declaration.</summary>
		/// <returns>The public identifier on the entity. If there is no public identifier, null is returned.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0007602C File Offset: 0x0007422C
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		/// <summary>Gets the value of the system identifier on the entity declaration.</summary>
		/// <returns>The system identifier on the entity. If there is no system identifier, null is returned.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00076034 File Offset: 0x00074234
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		/// <summary>Gets the name of the optional NDATA attribute on the entity declaration.</summary>
		/// <returns>The name of the NDATA attribute. If there is no NDATA, null is returned.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0007603C File Offset: 0x0007423C
		public string NotationName
		{
			get
			{
				return this.notationName;
			}
		}

		/// <summary>Gets the markup representing this node and all its children.</summary>
		/// <returns>For XmlEntity nodes, String.Empty is returned.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00003065 File Offset: 0x00001265
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets the markup representing the children of this node.</summary>
		/// <returns>For XmlEntity nodes, String.Empty is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00003065 File Offset: 0x00001265
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x00076044 File Offset: 0x00074244
		public override string InnerXml
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("Cannot set the 'InnerXml' for the current node because it is either read-only or cannot have children."));
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />. For XmlEntity nodes, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060014D1 RID: 5329 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteTo(XmlWriter w)
		{
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. For XmlEntity nodes, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060014D2 RID: 5330 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Gets the base Uniform Resource Identifier (URI) of the current node.</summary>
		/// <returns>The location from which the node was loaded.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00076055 File Offset: 0x00074255
		public override string BaseURI
		{
			get
			{
				return this.baseURI;
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0007605D File Offset: 0x0007425D
		internal void SetBaseURI(string inBaseURI)
		{
			this.baseURI = inBaseURI;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlEntity()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000DD6 RID: 3542
		private string publicId;

		// Token: 0x04000DD7 RID: 3543
		private string systemId;

		// Token: 0x04000DD8 RID: 3544
		private string notationName;

		// Token: 0x04000DD9 RID: 3545
		private string name;

		// Token: 0x04000DDA RID: 3546
		private string unparsedReplacementStr;

		// Token: 0x04000DDB RID: 3547
		private string baseURI;

		// Token: 0x04000DDC RID: 3548
		private XmlLinkedNode lastChild;

		// Token: 0x04000DDD RID: 3549
		private bool childrenFoliating;
	}
}
