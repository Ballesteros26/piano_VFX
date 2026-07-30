using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a lightweight object that is useful for tree insert operations.</summary>
	// Token: 0x0200021E RID: 542
	public class XmlDocumentFragment : XmlNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocumentFragment" /> class.</summary>
		/// <param name="ownerDocument">The XML document that is the source of the fragment.</param>
		// Token: 0x06001443 RID: 5187 RVA: 0x00074D62 File Offset: 0x00072F62
		protected internal XmlDocumentFragment(XmlDocument ownerDocument)
		{
			if (ownerDocument == null)
			{
				throw new ArgumentException(Res.GetString("Cannot create a node without an owner document."));
			}
			this.parentNode = ownerDocument;
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlDocumentFragment, the name is #document-fragment.</returns>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00074D84 File Offset: 0x00072F84
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlDocumentFragment nodes, the local name is #document-fragment.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00074D84 File Offset: 0x00072F84
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlDocumentFragment nodes, this value is XmlNodeType.DocumentFragment.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00074D91 File Offset: 0x00072F91
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentFragment;
			}
		}

		/// <summary>Gets the parent of this node (for nodes that can have parents).</summary>
		/// <returns>The parent of this node.For XmlDocumentFragment nodes, this property is always null.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0000365F File Offset: 0x0000185F
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The XmlDocument to which this node belongs.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00074D95 File Offset: 0x00072F95
		public override XmlDocument OwnerDocument
		{
			get
			{
				return (XmlDocument)this.parentNode;
			}
		}

		/// <summary>Gets or sets the markup representing the children of this node.</summary>
		/// <returns>The markup of the children of this node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x00074573 File Offset: 0x00072773
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x00074DA2 File Offset: 0x00072FA2
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAll();
				new XmlLoader().ParsePartialContent(this, value, XmlNodeType.Element);
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x0600144B RID: 5195 RVA: 0x00074DB8 File Offset: 0x00072FB8
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlDocumentFragment xmlDocumentFragment = ownerDocument.CreateDocumentFragment();
			if (deep)
			{
				xmlDocumentFragment.CopyChildren(ownerDocument, this, deep);
			}
			return xmlDocumentFragment;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00074DE0 File Offset: 0x00072FE0
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00074DE8 File Offset: 0x00072FE8
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

		// Token: 0x0600144F RID: 5199 RVA: 0x00074DF4 File Offset: 0x00072FF4
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
			case XmlNodeType.XmlDeclaration:
			{
				XmlNode firstChild = this.FirstChild;
				return firstChild == null || firstChild.NodeType != XmlNodeType.XmlDeclaration;
			}
			}
			return false;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00074E6A File Offset: 0x0007306A
		internal override bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || (refChild == null && this.LastNode == null);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00074E86 File Offset: 0x00073086
		internal override bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || refChild == null || refChild == this.FirstChild;
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001452 RID: 5202 RVA: 0x000746ED File Offset: 0x000728ED
		public override void WriteTo(XmlWriter w)
		{
			this.WriteContentTo(w);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001453 RID: 5203 RVA: 0x00074EA4 File Offset: 0x000730A4
		public override void WriteContentTo(XmlWriter w)
		{
			foreach (object obj in this)
			{
				((XmlNode)obj).WriteTo(w);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0000226C File Offset: 0x0000046C
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x04000DB8 RID: 3512
		private XmlLinkedNode lastChild;
	}
}
