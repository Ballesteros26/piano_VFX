using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a CDATA section.</summary>
	// Token: 0x02000217 RID: 535
	public class XmlCDataSection : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlCDataSection" /> class.</summary>
		/// <param name="data">
		///   <see cref="T:System.String" /> that contains character data.</param>
		/// <param name="doc">
		///   <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06001380 RID: 4992 RVA: 0x000728B7 File Offset: 0x00070AB7
		protected internal XmlCDataSection(string data, XmlDocument doc)
			: base(data, doc)
		{
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For CDATA nodes, the name is #cdata-section.</returns>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x000728C1 File Offset: 0x00070AC1
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For CDATA nodes, the local name is #cdata-section.</returns>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x000728C1 File Offset: 0x00070AC1
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For CDATA nodes, the value is XmlNodeType.CDATA.</returns>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x00004107 File Offset: 0x00002307
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x000728D0 File Offset: 0x00070AD0
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
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. Because CDATA nodes do not have children, regardless of the parameter setting, the cloned node will always include the data content. </param>
		// Token: 0x06001385 RID: 4997 RVA: 0x00072924 File Offset: 0x00070B24
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateCDataSection(this.Data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001386 RID: 4998 RVA: 0x00072937 File Offset: 0x00070B37
		public override void WriteTo(XmlWriter w)
		{
			w.WriteCData(this.Data);
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06001387 RID: 4999 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x00004107 File Offset: 0x00002307
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x00003242 File Offset: 0x00001442
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00072945 File Offset: 0x00070B45
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
