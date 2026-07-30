using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents the text content of an element or attribute.</summary>
	// Token: 0x0200023C RID: 572
	public class XmlText : XmlCharacterData
	{
		// Token: 0x06001647 RID: 5703 RVA: 0x0007BDEE File Offset: 0x00079FEE
		internal XmlText(string strData)
			: this(strData, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlText" /> class.</summary>
		/// <param name="strData">The content of the node; see the <see cref="P:System.Xml.XmlText.Value" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06001648 RID: 5704 RVA: 0x000728B7 File Offset: 0x00070AB7
		protected internal XmlText(string strData, XmlDocument doc)
			: base(strData, doc)
		{
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For text nodes, this property returns #text.</returns>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0007BDF8 File Offset: 0x00079FF8
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For text nodes, this property returns #text.</returns>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0007BDF8 File Offset: 0x00079FF8
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For text nodes, this value is XmlNodeType.Text.</returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0000226F File Offset: 0x0000046F
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0007BE08 File Offset: 0x0007A008
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.Document)
					{
						return null;
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
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x0600164D RID: 5709 RVA: 0x0007BE5C File Offset: 0x0007A05C
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateTextNode(this.Data);
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The content of the text node.</returns>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0007296C File Offset: 0x00070B6C
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x0007BE70 File Offset: 0x0007A070
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
				XmlNode parentNode = this.parentNode;
				if (parentNode != null && parentNode.NodeType == XmlNodeType.Attribute)
				{
					XmlUnspecifiedAttribute xmlUnspecifiedAttribute = parentNode as XmlUnspecifiedAttribute;
					if (xmlUnspecifiedAttribute != null && !xmlUnspecifiedAttribute.Specified)
					{
						xmlUnspecifiedAttribute.SetSpecified(true);
					}
				}
			}
		}

		/// <summary>Splits the node into two nodes at the specified offset, keeping both in the tree as siblings.</summary>
		/// <returns>The new node.</returns>
		/// <param name="offset">The offset at which to split the node. </param>
		// Token: 0x06001650 RID: 5712 RVA: 0x0007BEB0 File Offset: 0x0007A0B0
		public virtual XmlText SplitText(int offset)
		{
			XmlNode parentNode = this.ParentNode;
			int length = this.Length;
			if (offset > length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (parentNode == null)
			{
				throw new InvalidOperationException(Res.GetString("The 'Text' node is not connected in the DOM live tree. No 'SplitText' operation could be performed."));
			}
			int num = length - offset;
			string text = this.Substring(offset, num);
			this.DeleteData(offset, num);
			XmlText xmlText = this.OwnerDocument.CreateTextNode(text);
			parentNode.InsertAfter(xmlText, this);
			return xmlText;
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001651 RID: 5713 RVA: 0x0007BDC6 File Offset: 0x00079FC6
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. XmlText nodes do not have children, so this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001652 RID: 5714 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x00004107 File Offset: 0x00002307
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00072945 File Offset: 0x00070B45
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
