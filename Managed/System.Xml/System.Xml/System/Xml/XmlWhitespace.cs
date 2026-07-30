using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents white space in element content.</summary>
	// Token: 0x0200023E RID: 574
	public class XmlWhitespace : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlWhitespace" /> class.</summary>
		/// <param name="strData">The white space characters of the node.</param>
		/// <param name="doc">The <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06001661 RID: 5729 RVA: 0x0007BCFA File Offset: 0x00079EFA
		protected internal XmlWhitespace(string strData, XmlDocument doc)
			: base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("The string for white space contains an invalid character."));
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlWhitespace nodes, this property returns #whitespace.</returns>
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0007BFEA File Offset: 0x0007A1EA
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlWhitespace nodes, this property returns #whitespace.</returns>
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0007BFEA File Offset: 0x0007A1EA
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>For XmlWhitespace nodes, the value is <see cref="F:System.Xml.XmlNodeType.Whitespace" />.</returns>
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0007BFF7 File Offset: 0x0007A1F7
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Whitespace;
			}
		}

		/// <summary>Gets the parent of the current node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> parent node of the current node.</returns>
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0007BFFC File Offset: 0x0007A1FC
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

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The white space characters found in the node.</returns>
		/// <exception cref="T:System.ArgumentException">Setting <see cref="P:System.Xml.XmlWhitespace.Value" /> to invalid white space characters. </exception>
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0007296C File Offset: 0x00070B6C
		// (set) Token: 0x06001667 RID: 5735 RVA: 0x0007BDA4 File Offset: 0x00079FA4
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

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For white space nodes, the cloned node always includes the data value, regardless of the parameter setting. </param>
		// Token: 0x06001668 RID: 5736 RVA: 0x0007C055 File Offset: 0x0007A255
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateWhitespace(this.Data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see cref="T:System.Xml.XmlWriter" /> to which you want to save.</param>
		// Token: 0x06001669 RID: 5737 RVA: 0x0007C068 File Offset: 0x0007A268
		public override void WriteTo(XmlWriter w)
		{
			w.WriteWhitespace(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see cref="T:System.Xml.XmlWriter" /> to which you want to save. </param>
		// Token: 0x0600166A RID: 5738 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0007C078 File Offset: 0x0007A278
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType xpathNodeType = XPathNodeType.Whitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref xpathNodeType);
				return xpathNodeType;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00072945 File Offset: 0x00070B45
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
