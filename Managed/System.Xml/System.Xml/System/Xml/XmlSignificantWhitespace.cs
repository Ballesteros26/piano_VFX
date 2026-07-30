using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents white space between markup in a mixed content node or white space within an xml:space= 'preserve' scope. This is also referred to as significant white space.</summary>
	// Token: 0x0200023B RID: 571
	public class XmlSignificantWhitespace : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlSignificantWhitespace" /> class.</summary>
		/// <param name="strData">The white space characters of the node.</param>
		/// <param name="doc">The <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x0600163A RID: 5690 RVA: 0x0007BCFA File Offset: 0x00079EFA
		protected internal XmlSignificantWhitespace(string strData, XmlDocument doc)
			: base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("The string for white space contains an invalid character."));
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlSignificantWhitespace nodes, this property returns #significant-whitespace.</returns>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x0007BD25 File Offset: 0x00079F25
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlSignificantWhitespace nodes, this property returns #significant-whitespace.</returns>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0007BD25 File Offset: 0x00079F25
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlSignificantWhitespace nodes, this value is XmlNodeType.SignificantWhitespace.</returns>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0007BD32 File Offset: 0x00079F32
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.SignificantWhitespace;
			}
		}

		/// <summary>Gets the parent of the current node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> parent node of the current node.</returns>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0007BD38 File Offset: 0x00079F38
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.Document)
					{
						return base.ParentNode;
					}
					if (nodeType - XmlNodeType.Whitespace > 1)
					{
						return this.parentNode;
					}
				}
				XmlNode xmlNode = this.parentNode.parentNode;
				while (xmlNode.IsText)
				{
					xmlNode = xmlNode.parentNode;
				}
				return xmlNode;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For significant white space nodes, the cloned node always includes the data value, regardless of the parameter setting. </param>
		// Token: 0x0600163F RID: 5695 RVA: 0x0007BD91 File Offset: 0x00079F91
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateSignificantWhitespace(this.Data);
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The white space characters found in the node.</returns>
		/// <exception cref="T:System.ArgumentException">Setting Value to invalid white space characters. </exception>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0007296C File Offset: 0x00070B6C
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x0007BDA4 File Offset: 0x00079FA4
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				if (base.CheckOnData(value))
				{
					this.Data = value;
					return;
				}
				throw new ArgumentException(Res.GetString("The string for white space contains an invalid character."));
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001642 RID: 5698 RVA: 0x0007BDC6 File Offset: 0x00079FC6
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001643 RID: 5699 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0007BDD4 File Offset: 0x00079FD4
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType xpathNodeType = XPathNodeType.SignificantWhitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref xpathNodeType);
				return xpathNodeType;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00072945 File Offset: 0x00070B45
		public override XmlNode PreviousText
		{
			get
			{
				if (this.parentNode.IsText)
				{
					return this.parentNode;
				}
				return null;
			}
		}
	}
}
