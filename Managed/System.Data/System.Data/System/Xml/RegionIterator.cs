using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000030 RID: 48
	internal sealed class RegionIterator : BaseRegionIterator
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000073F7 File Offset: 0x000055F7
		internal RegionIterator(XmlBoundElement rowElement)
			: base(((XmlDataDocument)rowElement.OwnerDocument).Mapper)
		{
			this._rowElement = rowElement;
			this._currentNode = rowElement;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000741D File Offset: 0x0000561D
		internal override XmlNode CurrentNode
		{
			get
			{
				return this._currentNode;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007428 File Offset: 0x00005628
		internal override bool Next()
		{
			ElementState elementState = this._rowElement.ElementState;
			XmlNode firstChild = this._currentNode.FirstChild;
			if (firstChild != null)
			{
				this._currentNode = firstChild;
				this._rowElement.ElementState = elementState;
				return true;
			}
			return this.NextRight();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000746C File Offset: 0x0000566C
		internal override bool NextRight()
		{
			if (this._currentNode == this._rowElement)
			{
				this._currentNode = null;
				return false;
			}
			ElementState elementState = this._rowElement.ElementState;
			XmlNode xmlNode = this._currentNode.NextSibling;
			if (xmlNode != null)
			{
				this._currentNode = xmlNode;
				this._rowElement.ElementState = elementState;
				return true;
			}
			xmlNode = this._currentNode;
			while (xmlNode != this._rowElement && xmlNode.NextSibling == null)
			{
				xmlNode = xmlNode.ParentNode;
			}
			if (xmlNode == this._rowElement)
			{
				this._currentNode = null;
				this._rowElement.ElementState = elementState;
				return false;
			}
			this._currentNode = xmlNode.NextSibling;
			this._rowElement.ElementState = elementState;
			return true;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007518 File Offset: 0x00005718
		internal bool NextInitialTextLikeNodes(out string value)
		{
			ElementState elementState = this._rowElement.ElementState;
			XmlNode firstChild = this.CurrentNode.FirstChild;
			value = RegionIterator.GetInitialTextFromNodes(ref firstChild);
			if (firstChild == null)
			{
				this._rowElement.ElementState = elementState;
				return this.NextRight();
			}
			this._currentNode = firstChild;
			this._rowElement.ElementState = elementState;
			return true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007570 File Offset: 0x00005770
		private static string GetInitialTextFromNodes(ref XmlNode n)
		{
			string text = null;
			if (n != null)
			{
				while (n.NodeType == XmlNodeType.Whitespace)
				{
					n = n.NextSibling;
					if (n == null)
					{
						return string.Empty;
					}
				}
				if (XmlDataDocument.IsTextLikeNode(n) && (n.NextSibling == null || !XmlDataDocument.IsTextLikeNode(n.NextSibling)))
				{
					text = n.Value;
					n = n.NextSibling;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (n != null && XmlDataDocument.IsTextLikeNode(n))
					{
						if (n.NodeType != XmlNodeType.Whitespace)
						{
							stringBuilder.Append(n.Value);
						}
						n = n.NextSibling;
					}
					text = stringBuilder.ToString();
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x04000419 RID: 1049
		private XmlBoundElement _rowElement;

		// Token: 0x0400041A RID: 1050
		private XmlNode _currentNode;
	}
}
