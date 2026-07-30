using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents the content of an XML comment.</summary>
	// Token: 0x0200021B RID: 539
	public class XmlComment : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlComment" /> class.</summary>
		/// <param name="comment">The content of the comment element.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x060013A4 RID: 5028 RVA: 0x000728B7 File Offset: 0x00070AB7
		protected internal XmlComment(string comment, XmlDocument doc)
			: base(comment, doc)
		{
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For comment nodes, the value is #comment.</returns>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00072E30 File Offset: 0x00071030
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For comment nodes, the value is #comment.</returns>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00072E30 File Offset: 0x00071030
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For comment nodes, the value is XmlNodeType.Comment.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00072E3D File Offset: 0x0007103D
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Comment;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. Because comment nodes do not have children, the cloned node always includes the text content, regardless of the parameter setting. </param>
		// Token: 0x060013A8 RID: 5032 RVA: 0x00072E40 File Offset: 0x00071040
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateComment(this.Data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060013A9 RID: 5033 RVA: 0x00072E53 File Offset: 0x00071053
		public override void WriteTo(XmlWriter w)
		{
			w.WriteComment(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because comment nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060013AA RID: 5034 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00072E3D File Offset: 0x0007103D
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Comment;
			}
		}
	}
}
