using System;
using Unity;

namespace System.Xml
{
	/// <summary>Represents a notation declaration, such as &lt;!NOTATION... &gt;.</summary>
	// Token: 0x02000239 RID: 569
	public class XmlNotation : XmlNode
	{
		// Token: 0x0600161C RID: 5660 RVA: 0x0007BC03 File Offset: 0x00079E03
		internal XmlNotation(string name, string publicId, string systemId, XmlDocument doc)
			: base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.publicId = publicId;
			this.systemId = systemId;
		}

		/// <summary>Gets the name of the current node.</summary>
		/// <returns>The name of the notation.</returns>
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x0007BC2E File Offset: 0x00079E2E
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the name of the current node without the namespace prefix.</summary>
		/// <returns>For XmlNotation nodes, this property returns the name of the notation.</returns>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0007BC2E File Offset: 0x00079E2E
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For XmlNotation nodes, the value is XmlNodeType.Notation.</returns>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x000163C5 File Offset: 0x000145C5
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Notation;
			}
		}

		/// <summary>Creates a duplicate of this node. Notation nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlNotation" /> object throws an exception.</summary>
		/// <returns>Returns a <see cref="T:System.Xml.XmlNode" /> copy of the node from which the method is called.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself.</param>
		/// <exception cref="T:System.InvalidOperationException">Notation nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlNotation" /> object throws an exception.</exception>
		// Token: 0x06001620 RID: 5664 RVA: 0x00075FA9 File Offset: 0x000741A9
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException(Res.GetString("'Entity' and 'Notation' nodes cannot be cloned."));
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because XmlNotation nodes are read-only, this property always returns true.</returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00003242 File Offset: 0x00001442
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the value of the public identifier on the notation declaration.</summary>
		/// <returns>The public identifier on the notation. If there is no public identifier, null is returned.</returns>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0007BC36 File Offset: 0x00079E36
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		/// <summary>Gets the value of the system identifier on the notation declaration.</summary>
		/// <returns>The system identifier on the notation. If there is no system identifier, null is returned.</returns>
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x0007BC3E File Offset: 0x00079E3E
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		/// <summary>Gets the markup representing this node and all its children.</summary>
		/// <returns>For XmlNotation nodes, String.Empty is returned.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00003065 File Offset: 0x00001265
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets the markup representing the children of this node.</summary>
		/// <returns>For XmlNotation nodes, String.Empty is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00003065 File Offset: 0x00001265
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x00076044 File Offset: 0x00074244
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

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />. This method has no effect on XmlNotation nodes.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001627 RID: 5671 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteTo(XmlWriter w)
		{
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. This method has no effect on XmlNotation nodes.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001628 RID: 5672 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlNotation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000E25 RID: 3621
		private string publicId;

		// Token: 0x04000E26 RID: 3622
		private string systemId;

		// Token: 0x04000E27 RID: 3623
		private string name;
	}
}
